using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CyUSB;

/* Rev 1.2.0 (7/10/17)
    -- Add count down beep
    -- Change how wireless concede buttons effect readUSB() and rxDelay value (from 20 to 40).
   Rev 1.2.1 (10/23/17)
    -- Adjust rx_pause delay to eliminate spurious concede -> timeout events
    -- Default setting is dual match
    -- On concede, inc score and stop clock
   Rev 1.2.3 (05/21/18)
    -- Fix timeout issue where clock is not stopped on wireless push
   Rev 1.2.5 (03/20/19)
    -- Add button to start an overtime match.
*/   

namespace Scoreboardv6 {
	public partial class ScoreForm : Form {
		const int usbBUF_SIZE = 64;             // Go into control panel and tweak advanced settings. Make buffer size 64 and polling around 2mSec
		const String SW_VER = "1.2.5 (032019)";

		// USB (FW) Out (to Ctrl) Buffer positions  (not really bits as array element locations)
		const int CMD_BIT     = 0x01;
		const int POM_BIT     = 0x02;
		const int PTS_BIT     = 0x03;
		const int POS_BIT     = 0x04;
		const int GTM_BIT     = 0x05;
		const int GOM_BIT     = 0x06;
		const int GTS_BIT     = 0x07;
		const int GOS_BIT     = 0x08;
		const int HOM_BIT     = 0x09;
		const int VIS_BIT     = 0x0A;
		const int ACH_BIT	  = 0x0B;
		const int AEN_BIT	  = 0x0C;
		const int MUL_BIT     = 0x10;
		const int SIR_BIT     = 0x11;	// Siren or tone mode
		const int DIR_BIT	  = 0x12;
		const int RAN_BIT     = 0x13;

		// USB (FW) In (to PC) Buffer positions
		const int PS_STAT_IN  = 0x07;
		const int RX_IN       = 0x0D;
		const int RX_ADDR	  = 0x0E;
		const int FW_START_IN = 0x33;
		const int FW_SIG_IN   = 0x3D;

		const byte HOME_HANG = 0x01;
		const byte HOME_CONC = 0x02;
		const byte VIS_HANG  = 0x04;
		const byte VIS_CONC  = 0x08;
		const byte RX_PAUSE  = 0x10;
		const byte RX_STOP   = 0x20;

		// Scoreboard display character values.
		const byte SB_OFF      = 0x0A;	// Position in FW character array (not digit value itself).
		const byte SB_DASH     = 0x0B;

		// FW Commands
		const int AUDIO_ON    = 0x0B;
		const int AUDIO_OFF	  = 0x0C;
		const int PS_ON	      = 0x19;
		const int PS_OFF	  = 0x1A;
		const int UPDATE_DISP = 0x1F;
		const int FW_RESET    = 0x20;
		const int REQ_FW	  = 0x15;
		const int SIREN_MODE  = 1;
		const int TONE_MODE   = 0;

		USBDeviceList usbDevices = null;
		CyHidDevice scoreboardDevice = null;
		public int VID = 0x04B4, PID = 0x01CB;
		public int preGameSeconds, preGameMinutes, gameSeconds, gameMinutes, homeScore, visitorScore, audioChannel;
        public int swapHomeScore, swapVisitScore, swapPreGameMin, swapPreGameSec, swapGameMin, swapGameSec, rxDelay;
		public int clockInterval, raceTo, audioMult, audioWave, audioVal, audioMode, hornTicks, audioRange, hstatPtr, vstatPtr;
		public int configHomeTimeout, configVisitorTimeout, configTimeoutMinutes, configTimeoutSeconds, configDbgTimer, configAudioRange;
		public int configGameMin, configGameSec, configDbgPreGameMin, configDbgPreGameSec, configAudioMult, configRaceTo;
		public int configTimer, configAudioChannel, configPreGameSec, configPreGameMin, configDbgGameMin, configDbgGameSec;
        public int dualMatchPreGameMin, dualMatchPreGameSec, dualMatchGameMin, dualMatchGameSec, configDualMatchPreGameMin, configDualMatchPreGameSec;
        public int configDualMatchGameSec, configDualMatchGameMin, countDownFrom, audioCountdownStart;
		public int configNewGameMin, configNewGameSec, configMinTimeout, configNormTimer, configAudioDirValue, configEndOfGameChannel;
		public bool configRelayEn, configAudioEn, configSoundOnStop, configSoundOnReset, configSoundOnTimeout, configSoundOnHang;
		public bool configSoundOnEndOfPreGame, configSoundOnConcede, configSoundOnEndOfGame, configEndOfGameSiren, configPwrUp;
		public bool configPwrOff, HWAttached = false;		// If hardware is attached we don't require a license.
		public bool SWLicense = false, warningStatus = false, homeVicButtonisStop, visVicButtonisStop, sw_clk_override;
		public bool validLicense = false, dualMatch = true, swapGameOver = false;
		public bool PSon = false, hornOn = false, audioMute = false, countDown = false;
		public bool homeTimeoutTaken = false, visitorTimeoutTaken = false, processRx = false, pwrUpOnStart, pwrOffEnd;
        public bool swapHomeTimeoutTaken, swapVisitTimeoutTaken, swapHomeTimeoutButtonEnabled, swapVisitTimeoutButtonEnabled;
		public bool preGameRunning = false, gameClockRunning = false, gameOver = false, gamePaused = false;
        public bool audioCountdownEnabled = false;
        public Color swapPreGameTimeColor, swapGameTimeColor, swapHomeColor, swapVisitorColor;
        public string swapHomeTeam, swapVisitTeam;
        byte[] usbInBuf  = new byte[usbBUF_SIZE];			// USB In buffer (from device).
		byte[] usbOutBuf = new byte[usbBUF_SIZE];			// USB Out buffer (to device).

		public string[] homeStats = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
		public string[] visitorStats = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };

		public String pid, radioAddress = "", Firmware = "N/A", homeTeam, visitorTeam;
		List<String> homeStatList = new List<String>();
		List<String> visitorStatList = new List<String>();

		public ScoreForm() {
			bool mutexCreated = false;
            sw_clk_override = false;


            System.Threading.Mutex mutex = new System.Threading.Mutex(true, "scoreboard4x", out mutexCreated);
			if (!mutexCreated) {
				MessageBox.Show("Another instance of the application is already running (or starting).");
				Environment.Exit(0);
			}

			InitializeComponent();
			this.Text = "Scoreboardv6 (Version: " + SW_VER + ")";

			if (scoreboardDevice == null) {
				usbDevices = new USBDeviceList(CyConst.DEVICES_HID);
				usbDevices.DeviceAttached += new EventHandler(usbDevices_DeviceAttached);
				usbDevices.DeviceRemoved += new EventHandler(usbDevices_DeviceRemoved);
				getUSBDevice();
			}

            readConfigs();
            setDefaults();

			usbTimer.Tick += new EventHandler(usbTimer_Tick);
			gameClockTimer.Tick += new EventHandler(gameClock_Tick);
			hornTimer.Tick += new EventHandler(hornTimer_Tick);

			startButton.Select();	                    // Put cursor here
			if (scoreboardDevice != null) {
				System.Threading.Thread.Sleep(200);		// Give USB some time.
				if (pwrUpOnStart == true) {
					scoreboardDevice.Outputs.DataBuf[CMD_BIT] = PS_ON;	// Tell FW to turn PS on.
					writeUSB();
					System.Threading.Thread.Sleep(200);		// Give USB some time.
				}
				readUSB();
				if (scoreboardDevice.Inputs.DataBuf[PS_STAT_IN] != 0) {
					displayPowerToolStripMenuItem.Checked = true;
				} else {
					displayPowerToolStripMenuItem.Checked = false;
				}
				updateDisplay();
			}
		}

		private void getUSBDevice() {
			//String pid;

			scoreboardDevice = usbDevices[VID, PID] as CyHidDevice;

			pid = PID.ToString("X");
			if (File.Exists(@"C:\BostonPaintball\.ebplic")) {	// See if this file exists. If so allow us to run.
                // I lost a thumbdrive with this program on it and I need a way to only allow computers I authorize to run this app.
                // The way I'm doing it is; if the ebplic file exists AND the license.txt file exists (from the installer), then
                // I do not authorize this. To know it is me, I will remove, or rename, the license.txt file.. Then we will run.

                // The following could be removed.. I found the missing thumbdrive (4/32/18)
                if (File.Exists(@"C:\BostonPaintball\license.txt")) { // This is a bogus file. If it exists (by default with installer) then someone else is using this
                    SWLicense = false;          // so, no license for you!
                    licenseBox.Text = "HACK";
                    System.Threading.Thread.Sleep(500);
                } else {
                    if (File.Exists(@"C:\BostonPaintball\.ebplic-clk")) {  // I need away to override the faster clock speed for software only license.
                        SWLicense = true;
                        sw_clk_override = true; // Keep the clock at 950mSec
                    }
                }
                //SWLicense = true;
            }
			if (scoreboardDevice != null) {
				usbBox.Text = "Controller @ 0x0" + pid + "; Ver: " + Firmware;
				usbBox.BackColor = Color.GreenYellow;
				HWAttached = true;
				usbTimer.Enabled = true;					// Enable USB polling
			} else {
				//usbBox.Text = "Controller Not Attached" + pid;
				usbBox.Text = "Controller Not Attached @ 0x0" + pid;
				usbBox.BackColor = Color.Red;
				licenseBox.Text = "No License";
				licenseBox.BackColor = Color.LightCoral;
				HWAttached = false;
				usbTimer.Enabled = false;					// No need to poll if there's no device.
			}
			if (HWAttached == true) {
				licenseBox.Text = "HW License";
				licenseBox.BackColor = Color.GreenYellow;
				validLicense = true;
			} else if (SWLicense == true) {
				licenseBox.Text = "SW License";
				licenseBox.BackColor = Color.GreenYellow;
				validLicense = true;
			} else {
				licenseBox.Text = "No License";
				licenseBox.BackColor = Color.LightCoral;
				validLicense = false;
				startButton.Enabled = false;
			}
			updateDisplay();
		}

		private void usbDevices_DeviceAttached(object sender, EventArgs e) {

			if (scoreboardDevice == null) {
				getUSBDevice();
			}
		}

		private void usbDevices_DeviceRemoved(object sender, EventArgs e) {
			USBEventArgs usbEvent = e as USBEventArgs;

			if ((usbEvent.ProductID == PID) && (usbEvent.VendorID == VID)) {
				scoreboardDevice = null;		// No device
				usbBox.Text = "No Controller Detected";
				usbBox.BackColor = Color.Red;
				MessageBox.Show("USB connection has disconnected!");
				System.Threading.Thread.Sleep(500);
				getUSBDevice();
			}
		}

        private void homeTimeTakenBox_CheckedChanged(object sender, EventArgs e) {

        }

        private void clrHomeTOButton_Click(object sender, EventArgs e) {
            if (homeTimeoutTaken == false) {  // No timeout taken, so now make it look like we took it
                homeTimeoutTaken = true;
                homeTimeoutButton.Enabled = false;
            } else {
                homeTimeoutTaken = false;
                homeTimeoutButton.Enabled = true;
            }
        }

        private void clrVisitorTOButton_Click(object sender, EventArgs e) {
            if (visitorTimeoutTaken == false) {  // No timeout taken, so now make it look like we took it
                visitorTimeoutTaken = true;
                visitorTimeoutButton.Enabled = false;
            } else {
                visitorTimeoutTaken = false;
                visitorTimeoutButton.Enabled = true;
            }
        }

        private void addOneMinButton_Click(object sender, EventArgs e) {
            preGameMinutes += 1;
            startButton.Select();
            updateAppTimes();
        }

        private void OTButton_Click(object sender, EventArgs e)
        {
            if (gameOver == false) {
                DialogResult input = MessageBox.Show("Are you sure you want set the match for OT?", "Start OT match", MessageBoxButtons.YesNo);
                if (input == DialogResult.No) {
                    return;
                }
                preGameMinutes = 2;
                preGameSeconds = 0;
                gameMinutes = 5;
                gameSeconds = 0;
                visitorTimeoutTaken = false;
                visitorTimeoutButton.Enabled = true;
                homeTimeoutTaken = false;
                homeTimeoutButton.Enabled = true;
                updateDisplay();
                updateAppTimes();
            }
        }

        private void decOneMinButtun_Click(object sender, EventArgs e) {
            if (preGameMinutes > 0) {
                preGameMinutes -= 1;
                startButton.Select();
                updateAppTimes();
            }
        }

        /* 
		 * The gameClockTimer event happened. Update displays with new times   *
		 */
        private void gameClock_Tick(object sender, EventArgs e) {
			if (gameOver == false) {
				if (preGameRunning == true) {
					homeHangButton.Enabled = false;
					homeConcedeButton.Enabled = false;
					visitorHangButton.Enabled = false;
					visitorConcedeButton.Enabled = false;
					if (preGameMinutes > 0) {
						if (preGameSeconds > 0) {
							preGameSeconds--;
						} else {
							preGameSeconds = 59;
							preGameMinutes--;
						}
					} else {
                        if (countdownBeepToolStripMenuItem.Checked == true) {
                            messageBox.Text = "Countdown beep..";
                            if (preGameSeconds <= (countDownFrom + 1)) {
                                countDownSoundCtl();
                                updateDisplay();
                            }
                        }
						if (preGameSeconds <= 10) {
							preGameTimeBox.BackColor = Color.LightCoral;
							dec10Button.Enabled = false;
							set10Button.Enabled = false;
							homeTimeoutButton.Enabled = false;
							visitorTimeoutButton.Enabled = false;
						} else {
							if (homeTimeoutTaken == false)
								homeTimeoutButton.Enabled = true;
							if (visitorTimeoutTaken == false)
								visitorTimeoutButton.Enabled = true;
						}

						if (preGameSeconds > 0) {
							preGameSeconds--;
							if (preGameSeconds == 0) {
                                //preGameRunning = false; // added 5/22
                                //gameClockRunning = false; // added 5/22
                                //gameClockTimer.Stop(); // added 5/22
                                endOfPreGameSoundCtl();
								updateDisplay();
							}
						} else {
							preGameRunning = false;
							//goto updateit;		// Uncomment this line if we want to see a delay between pregame end and game start times.
						}
					}
				}
				if (preGameRunning == false) {
					homeHangButton.Enabled = true;
					homeConcedeButton.Enabled = true;
					visitorHangButton.Enabled = true;
					visitorConcedeButton.Enabled = true;
					if (gameMinutes > 0) {
						if (gameSeconds > 0) {
							gameSeconds--;
						} else {
							gameSeconds = 59;
							gameMinutes--;
						}
					} else {
						if (gameSeconds <= 10) {
							gameTimeBox.BackColor = Color.LightCoral;
						}
						if (gameSeconds > 0) {
							gameSeconds--;
							if (gameSeconds == 0) {
                                preGameRunning = false; // added 5/22
                                gameClockRunning = false; // added 5/22
                                gameClockTimer.Stop(); // added 5/22
                                endOfGameSoundCtl();
								updateDisplay();
                                //MessageBox.Show("Game over");
                                startStopFunction();   // 5/23
                            }
						} else {
							gameOver = true;
							homeHangButton.Enabled = false;
							homeConcedeButton.Enabled = false;
							homeTimeoutButton.Enabled = false;
							visitorHangButton.Enabled = false;
							visitorConcedeButton.Enabled = false;
							visitorTimeoutButton.Enabled = false;
						}
					}
				}
			}
			updateDisplay();
			updateAppTimes();
		}

        private void countdownBeepToolStripMenuItem_Click(object sender, EventArgs e) {
            if (scoreboardDevice != null) {
                countdownBeepToolStripMenuItem.Enabled = true;
                if (countdownBeepToolStripMenuItem.Checked) {
                    countDown = true;
                    countdownBeepToolStripMenuItem.Checked = false;
                    messageBox.Text = "Countdown beep disabled";
                    //MessageBox.Show("Countdown beep is disabled");
                } else {
                    countDown = false;
                    countdownBeepToolStripMenuItem.Checked = true;
                    messageBox.Text = "Countdown beep enabled, starts at " + countDownFrom + " seconds";
                    //MessageBox.Show("Countdown beep is enabled");
                }
            } else {
                countdownBeepToolStripMenuItem.Enabled = false;
            }
        }

        /* 
		 * The usbTimer event happened. Check USB IN buffer for any data from Controller   *
		 */
        private void usbTimer_Tick(object sender, EventArgs e) {

            // If "processRX is false then it's ok to read the USB. If it's tru then we just rev'd USB data.
            // BUT, does USB buffer ignored requests? Should we always read and just not process the data???
            if (processRx == true) { // We just processed a button push
                rxDelay++;
                messageBox.Text = "RX data delay";
                if (rxDelay > 180) { // 20 == approx 2 seconds
                    rxDelay = 0;
                    processRx = false;
                    homeHangButton.BackColor = SystemColors.Control; // Reset button to normal color once we accept wireless pushes.
                    homeConcedeButton.BackColor = SystemColors.Control;
                    visitorHangButton.BackColor = SystemColors.Control;
                    visitorConcedeButton.BackColor = SystemColors.Control;
                }
                // 10/23/17; add else clause and comment the line below
                //readUSB();  // Here we read the USB regardless of processRX flag (and ignore input in readUSB() ).

            } else {  // Here we only read the USB if the processRX flag is false
                readUSB();  // Only read USB if it's ok to.
            }
        }

		/* 
		 * Prepare USB "Outputs" buffer to send to controller.
		 */
		private void writeUSB() {
			int x;

			if (scoreboardDevice == null) {
				return;
			}
			scoreboardDevice.WriteOutput();			// Send the data to controller

			for (x = 0; x < usbBUF_SIZE; x++) {
				usbOutBuf[x] = scoreboardDevice.Outputs.DataBuf[x];
			}
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			for (x = 0; x < usbBUF_SIZE; x++) { // All 64 bytes
				sb.Append(usbOutBuf[x]);
			}
			outBufferBox.Text = "2FW " + sb.ToString();
		}

        private void dualMatchToolStripMenuItem_Click(object sender, EventArgs e) {
            toggleDualMatchItems();
        }

        private void toggleDualMatchItems() { 
            if (dualMatchToolStripMenuItem.Checked == true) {
                dualMatchToolStripMenuItem.Checked = false;
                dualMatch = false;
                showStatsToolStripMenuItem.Checked = false;
                showStatsToolStripMenuItem.Enabled = false;
                homeStatBox.Visible = false;
                visitorStatBox.Visible = false;
                homeTeam2ComboBox.Visible = false;
                visitorTeam2ComboBox.Visible = false;
                toggleTeamsCheckBox.Visible = false;
                altGameHome.Visible = false;
                altGameTime.Visible = false;
                altGameVisitor.Visible = false;
                preGameMinutes = configPreGameMin;
                preGameSeconds = configPreGameSec;
            } else {
                dualMatch = true;
                dualMatchToolStripMenuItem.Checked = true;
                showStatsToolStripMenuItem.Checked = false;
                showStatsToolStripMenuItem.Enabled = true;
                homeStatBox.Visible = false;
                visitorStatBox.Visible = false;
                homeTeam2ComboBox.Visible = true;
                visitorTeam2ComboBox.Visible = true;
                toggleTeamsCheckBox.Visible = true;
                altGameHome.Visible = true;
                altGameTime.Visible = true;
                altGameVisitor.Visible = true;
                preGameMinutes = configDualMatchPreGameMin;
                preGameSeconds = configDualMatchPreGameSec;
            }
            updateAppTimes();
            updateDisplay();
        }

        /* 
		 * Populate 'usbInBuf' with data sent FROM device.
		 */
        private void readUSB() {
			int x;

			if (scoreboardDevice == null) {
				return;
			}
			scoreboardDevice.ReadInput();
			for (x = 0; x < usbBUF_SIZE; x++) {
				usbInBuf[x] = scoreboardDevice.Inputs.DataBuf[x];
			}
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			for (x = 0; x < usbBUF_SIZE; x++) { // All 64 bytes
				sb.Append(usbInBuf[x]);
			}
			inputBufferBox.Text = "2PC " + sb.ToString();
			inputBufferBox.Refresh();
			if (usbInBuf[FW_SIG_IN] != 'C' || usbInBuf[FW_SIG_IN + 1] != 'B') {
				messageBox.Text = "Invalid checksum recv'd from controller";
			}
			System.Text.StringBuilder firmware = new System.Text.StringBuilder();
			for (x = FW_START_IN; x < (FW_START_IN + 10); x++) { // Read the firmware version portion of buffer
				firmware.Append(Convert.ToChar(usbInBuf[x]));	// Convert decimal value to ascii character and append to "firmware"
			}
			Firmware = firmware.ToString();						// Keep FW value
			// Remove any non-printable characters in Firmware string.
			Firmware = System.Text.RegularExpressions.Regex.Replace(Firmware, @"[^\u0020-\u007F]", "");

			if (scoreboardDevice.Inputs.DataBuf[PS_STAT_IN] != 0) {
				displayPowerToolStripMenuItem.Checked = true;
				if (warningBox.TextLength > 0)
					warningBox.Text = "";
			} else {
				displayPowerToolStripMenuItem.Checked = false;
				warningBox.Text = "Display power supply is OFF";
			}
			sb.Clear();
			//sb.Append(Convert.ToChar(usbInBuf[RX_ADDR]));
			sb.Append(usbInBuf[RX_ADDR]);
			radioAddress = sb.ToString();
			usbBox.Text = "Controller Ver: \"" + Firmware + "\",   Radio Addr: " + radioAddress;
			
			if (usbInBuf[RX_IN] != (byte)0) {		// Some Rx data is here
				if (processRx == false) {			// We can only process one command at a time.
					processRx = true;
					messageBox.Text = "RX data is available: " + usbInBuf[RX_IN].ToString();
					switch (usbInBuf[RX_IN]) {
						case HOME_HANG:
                            homeHangButton.BackColor = Color.Red; // So we can test the buttons
                            if (preGameRunning == false && gameClockRunning == true) {
                                homeScoreMgr(true, true, true);
								homeStatsAddHang();
								updatePoint();
							}
							break;
						case HOME_CONC:
                            homeConcedeButton.BackColor = Color.Red; // So we can test the buttons w/o game running
                            if (preGameRunning == true) {		     // If pregame is running this is a timeout.
                                if (gameClockRunning == true) {
                                    if (homeTimeoutTaken == false) {
                                        homeTimeoutMgr();   // add 5/21/18
                                        updateDisplay();    // add 5/21/18
                                        updateAppTimes();   // add 5/21/18
                                        startStopFunction(); // add 5/21/18
                                        MessageBox.Show(homeTeam + " pressed timeout");
                                    } else {
                                        MessageBox.Show(homeTeam + " pressed timeout, but they already took their timeout!");
                                    }
                                    //homeTimeoutMgr();  // remove 5/21/18
                                }
							} else {
                                visitorScoreMgr(true, true, true);
                                homeStatsAddConc();
								updatePoint();
                                MessageBox.Show(homeTeam + " has conceded");
							}
							break;
						case VIS_HANG:
                            visitorHangButton.BackColor = Color.Red; // So we can test the buttons w/o game running
                            if (preGameRunning == false && gameClockRunning == true) {
								visitorScoreMgr(true, true, true);
								visitorStatsAddHang();
								updatePoint();
							}
							break;
						case VIS_CONC:
                            visitorConcedeButton.BackColor = Color.Red; // So we can test the buttons
                            if (preGameRunning == true) {		// If pregame is running this is a timeout.
                                if (gameClockRunning == true) {
                                    if (visitorTimeoutTaken == false) {
                                        visitorTimeoutMgr();   // add 5/21/18
                                        updateDisplay();    // add 5/21/18
                                        updateAppTimes();   // add 5/21/18
                                        startStopFunction(); // add 5/21/18
                                        MessageBox.Show(visitorTeam + " pressed timeout");
                                    } else {
                                        MessageBox.Show(visitorTeam + " pressed timeout, but they already took their timeout!");
                                    }
                                    //visitorTimeoutMgr(); // Remove 5/21/18
                                }
							} else {
                                homeScoreMgr(true, true, true);
                                visitorStatsAddConc();
								updatePoint();
                                MessageBox.Show(visitorTeam + " has conceded");
                            }
							break;
						case RX_PAUSE:
							pauseMgr();
							break;
						case RX_STOP:
							startStopFunction();
							break;
						default:
							break;
					}
				} else {
                    messageBox.Text = "RX data delay";
                }
			}
		}

        private void homeTeam2ComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            swapHomeTeam = homeTeam2ComboBox.SelectedItem.ToString();
            if (toggleTeamsCheckBox.Checked == false) {
                altGameHome.Text = swapHomeTeam;
            } else {
                altGameHome.Text = homeTeam;
            }
        }

        private void homeTeam2ComboBox_TextChanged(object sender, EventArgs e) {    // If user manually enters team name in dropdown field
            swapHomeTeam = homeTeam2ComboBox.Text;
            if (toggleTeamsCheckBox.Checked == false) {
                altGameHome.Text = swapHomeTeam;
            } else {
                altGameHome.Text = homeTeam;
            }
        }

        private void visitorTeam2ComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            swapVisitTeam = visitorTeam2ComboBox.SelectedItem.ToString();
            if (toggleTeamsCheckBox.Checked == false) {
                altGameVisitor.Text = swapVisitTeam;
            } else {
                altGameVisitor.Text = visitorTeam;
            }
        }

        private void visitorTeam2ComboBox_TextChanged(object sender, EventArgs e) { // If user manually enters team name in dropdown field
            swapVisitTeam = visitorTeam2ComboBox.Text;
            if (toggleTeamsCheckBox.Checked == false) {
                altGameVisitor.Text = swapVisitTeam;
            } else {
                altGameVisitor.Text = visitorTeam;
            }
        }

        private void exitButton_Click(object sender, EventArgs e) {
			if (pwrOffEnd == true) {
				if (scoreboardDevice != null) {
					scoreboardDevice.Outputs.DataBuf[CMD_BIT] = 0x1A;	// Tell FW to turn PS off.
					System.Threading.Thread.Sleep(150);
					writeUSB();
					System.Threading.Thread.Sleep(150);
				}
			}
			Application.Exit();
		}

		private void showStatsToolStripMenuItem_Click(object sender, EventArgs e) {
            if (dualMatch == true) {
                showStatsToolStripMenuItem.Checked = false;
                showStatsToolStripMenuItem.Enabled = false;
                homeStatBox.Visible = false;
                visitorStatBox.Visible = false;
                return;
            }
			if (showStatsToolStripMenuItem.Checked == true) {
				showStatsToolStripMenuItem.Checked = false;
				homeStatBox.Visible = false;
				visitorStatBox.Visible = false;
			} else {
				showStatsToolStripMenuItem.Checked = true;
				homeStatBox.Visible = true;
				visitorStatBox.Visible = true;
			}
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
			if (scoreboardDevice == null) {
				resetToolStripMenuItem.Enabled = false;
			} else {
				warningBox.Text = "This option is not functional at this time";
			}
		}

		private void readTimeBoxes() {
			string boxStr, secs, mins;
			string[] tokens;

			boxStr = preGameTimeBox.Text;
			try {
				tokens = boxStr.Split(new[] { ':' }, StringSplitOptions.None);
				mins = tokens[0];
				secs = tokens[1];
				int.TryParse(mins, out preGameMinutes);
				int.TryParse(secs, out preGameSeconds);
			} catch (Exception ex) {
				MessageBox.Show("Invalid Time entry. Try something like \"2:00\" ");
			}

			try {
				boxStr = gameTimeBox.Text;
				tokens = boxStr.Split(new[] { ':' });
				mins = tokens[0];
				secs = tokens[1];
				int.TryParse(mins, out gameMinutes);
				int.TryParse(secs, out gameSeconds);
			} catch (Exception ex) {
				MessageBox.Show("Invalid TIme entry. Try something like \"12:00\" ");
			}
			updateAppTimes();
		}

		private void updateDisplay() {
			int tmin, omin, tsec, osec;

			if (scoreboardDevice == null)
				return;

            messageBox.Text = "Update display ..";
			omin = preGameMinutes;
			tsec = preGameSeconds / 10;
			osec = preGameSeconds % 10;

			scoreboardDevice.Outputs.DataBuf[CMD_BIT] = (byte)UPDATE_DISP;
			if (preGameRunning == true) {		// Show times if preGame clock is running
				scoreboardDevice.Outputs.DataBuf[POM_BIT] = (byte)omin;
				scoreboardDevice.Outputs.DataBuf[PTS_BIT] = (byte)tsec;
				scoreboardDevice.Outputs.DataBuf[POS_BIT] = (byte)osec;
			} else {							// Show dashes if (main) Game is running
				scoreboardDevice.Outputs.DataBuf[POM_BIT] = SB_DASH;
				scoreboardDevice.Outputs.DataBuf[PTS_BIT] = SB_DASH;
				scoreboardDevice.Outputs.DataBuf[POS_BIT] = SB_DASH;
			}

			tmin = gameMinutes / 10;
			omin = gameMinutes % 10;
			tsec = gameSeconds / 10;
			osec = gameSeconds % 10;

			if (gameMinutes < 10) {
				tmin = SB_OFF;
			}
			if (gameOver == false) {	// Show time values
				scoreboardDevice.Outputs.DataBuf[GTM_BIT] = (byte)tmin;
				scoreboardDevice.Outputs.DataBuf[GOM_BIT] = (byte)omin;
				scoreboardDevice.Outputs.DataBuf[GTS_BIT] = (byte)tsec;
				scoreboardDevice.Outputs.DataBuf[GOS_BIT] = (byte)osec;
			} else {
				scoreboardDevice.Outputs.DataBuf[GTM_BIT] = SB_DASH;
				scoreboardDevice.Outputs.DataBuf[GOM_BIT] = SB_DASH;
				scoreboardDevice.Outputs.DataBuf[GTS_BIT] = SB_DASH;
				scoreboardDevice.Outputs.DataBuf[GOS_BIT] = SB_DASH;
			}
            scoreboardDevice.Outputs.DataBuf[HOM_BIT] = (byte)homeScore;
            scoreboardDevice.Outputs.DataBuf[VIS_BIT] = (byte)visitorScore;
            writeUSB();
		}

        private void ScoreForm_Load(object sender, EventArgs e)
        {

        }

        private void homeLabel_Click(object sender, EventArgs e) {
            
        }

        private void toggleTeamsCheckBox_CheckedChanged(object sender, EventArgs e) {
            swapParams();
            showSwappedScreen();
        }

        private void showSwappedScreen() {
            string mBuf, sBuf;

            startButton.Text = "Start";
            startButton.BackColor = Color.GreenYellow;

            if (homeTimeoutTaken == true) {
                homeTimeoutButton.Enabled = false;
            } else {
                homeTimeoutButton.Enabled = true;
            }

            if (visitorTimeoutTaken == true) {
                visitorTimeoutButton.Enabled = false;
            } else {
                visitorTimeoutButton.Enabled = true;
            }

            gameClockRunning = false;
            gameClockTimer.Stop();
            setStartItems();

            // Display current stats in swapped boxes.
            mBuf = swapGameMin.ToString();
            sBuf = swapGameSec.ToString();
            sBuf = sBuf.PadLeft(2, '0');
            altGameHome.Text = swapHomeTeam + ": " + swapHomeScore + " --- TO avail: "; //  homeScoreBox.Text.ToString();
            if (swapHomeTimeoutTaken == true) {
                altGameHome.Text += " no";
            } else {
                altGameHome.Text += " yes";
            }
            altGameVisitor.Text = swapVisitTeam + ": " + swapVisitScore + " --- TO avail: "; //  visitorScoreBox.Text.ToString();
            if (swapVisitTimeoutTaken == true) {
                altGameVisitor.Text += " no";
            } else {
                altGameVisitor.Text += " yes";
            }
            altGameTime.Text = "Game time  " + mBuf + ":" + sBuf;
            homeLabel.Text = homeTeam;
            visitorLabel.Text = visitorTeam;
            mBuf = preGameMinutes.ToString();
            sBuf = preGameSeconds.ToString();
            sBuf = sBuf.PadLeft(2, '0');
            preGameTimeBox.Text = mBuf + ":" + sBuf;
            if (homeScore - visitorScore >= raceTo) {
                homeScoreBox.BackColor = Color.Red;
            } else {
                homeScoreBox.BackColor = Color.LightYellow;
            }
            if (visitorScore - homeScore >= raceTo) {
                visitorScoreBox.BackColor = Color.Red;
            } else {
                visitorScoreBox.BackColor = Color.LightYellow;
            }
            updateAppTimes();
            updateDisplay();
        }

        private void swapParams() {
            string tHteam, tVteam;
            int tHscore, tVscore, tPGM, tPGS, tGM, tGS;
            bool tHTOtaken, tVTOtaken, tgameOver;
            Color tPreGameTimeColor, tGameTimeColor, tHomeColor, tVisitorColor;

        // Put current team stats in tmp
            tHteam = homeTeam;
            tVteam = visitorTeam;
            tHscore = homeScore;
            tVscore = visitorScore;
            tPGM = preGameMinutes;
            tPGS = preGameSeconds;
            tGM = gameMinutes;
            tGS = gameSeconds;
            tHTOtaken = homeTimeoutTaken;
            tVTOtaken = visitorTimeoutTaken;
            tgameOver = gameOver;
            tPreGameTimeColor = preGameTimeBox.BackColor;
            tGameTimeColor = gameTimeBox.BackColor;
            tHomeColor = homeScoreBox.BackColor;
            tVisitorColor = visitorScoreBox.BackColor;

            // Move alternate team (in swap) to current stats.
            homeTeam = swapHomeTeam;
            visitorTeam = swapVisitTeam;
            homeScore = swapHomeScore;
            visitorScore = swapVisitScore;
            preGameMinutes = dualMatchPreGameMin;
            preGameSeconds = dualMatchPreGameSec;
            gameMinutes = swapGameMin;
            gameSeconds = swapGameSec;
            gameOver = swapGameOver;
            homeTimeoutTaken = swapHomeTimeoutTaken;
            visitorTimeoutTaken = swapVisitTimeoutTaken;
            preGameTimeBox.BackColor = swapPreGameTimeColor;
            gameTimeBox.BackColor = swapGameTimeColor;
            homeScoreBox.BackColor = swapHomeColor;
            visitorScoreBox.BackColor = swapVisitorColor;

            homeTimeoutButton.Enabled = !homeTimeoutTaken;
            if (homeTimeoutTaken == true) {
                homeTimeoutButton.Enabled = false;
            } else {
                homeTimeoutButton.Enabled = true;
            }

            // Save original stats (now in tmp) into swap.
            swapHomeTeam = tHteam;
            swapVisitTeam = tVteam;
            swapHomeScore = tHscore;
            swapVisitScore = tVscore;
            swapPreGameMin = tPGM;
            swapPreGameSec = tPGS;
            swapGameMin = tGM;
            swapGameSec = tGS;
            swapGameOver = tgameOver;
            swapHomeTimeoutTaken = tHTOtaken;
            swapVisitTimeoutTaken = tVTOtaken;
            swapPreGameTimeColor = tPreGameTimeColor;
            swapGameTimeColor = tGameTimeColor;
            swapHomeColor = tHomeColor;
            swapVisitorColor = tVisitorColor;
        }

        private void showDualParams() {
            string cTime, cScore, cFlags, tFlags, sTime, sScore, sFlags, xFlags;

            cTime = "Current: PreGame: " + preGameMinutes + ":" + preGameSeconds + " -- Game: " + gameMinutes + ":" + gameSeconds + "\n\r";
            cScore = "Home: (" + homeTeam + "):   " + homeScore + "   -- Visitor: (" + visitorTeam + "): " + visitorScore + "\n\r";
            if (homeTimeoutButton.Enabled == true) {
                cFlags = "homeTimeoutButton.Enabled = true";
            } else {
                cFlags = "homeTimeoutButton.Enabled = false";
            }
            if (visitorTimeoutButton.Enabled == true) {
                cFlags += " -- visitorTimeoutButton.Enabled = true";
            } else {
                cFlags += " -- visitorTimeoutButton.Enabled = false";
            }
            if (homeTimeoutTaken == true) {
                tFlags = "homeTimeoutTaken = true";
            } else {
                tFlags = "homeTimeoutTakn = false";
            }
            if (visitorTimeoutTaken == true) {
                tFlags += " -- visitorTimeoutTaken = true";
            } else {
                tFlags += " -- visitorTimeoutTaken = false";
            }

            sTime = "Swapped: PreGame: " + swapPreGameMin + ":" + swapPreGameSec + " -- Game: " + swapGameMin + ":" + swapGameSec + "\n\r";
            sScore = "Home: (" + swapHomeTeam + "): " + swapHomeScore + " -- Visitor: (" + swapVisitTeam + "): " + swapVisitScore + "\n\r";
            if (swapHomeTimeoutButtonEnabled == true) {
                sFlags = "homeTimeoutButton.Enabled: true";
            } else {
                sFlags = "homeTimeoutButton.Enabled: false";
            }
            if (swapVisitTimeoutButtonEnabled == true) {
                sFlags += " -- visitorTimeoutButton.Enabled: true";
            } else {
                sFlags += " -- visitorTimeoutButton.Enabled: false";
            }
            if (swapHomeTimeoutTaken == true) {
                xFlags = "homeTimeoutTaken: true";
            } else {
                xFlags = "homeTimeoutTakn: false";
            }
            if (swapVisitTimeoutTaken == true) {
                xFlags += " -- visitorTimeoutTaken: true";
            } else {
                xFlags += " -- visitorTimeoutTaken: false";
            }

            MessageBox.Show(cTime + cScore + cFlags + tFlags + "\n\r -----------------------------\n\r" + sTime + sScore + sFlags + xFlags);
        }

        private void updateAppTimes() {
            string mBuf, sBuf;

            if (preGameRunning == false) {
                if (dualMatch == false) {
                    preGameTimeBox.Text = ("0:00");
                    preGameTimeBox.BackColor = Color.WhiteSmoke;
                    startButton.Select();
                } else {
                    preGameTimeBox.BackColor = Color.LightYellow;
                }
            } else {
                mBuf = preGameMinutes.ToString();
                sBuf = preGameSeconds.ToString();
                sBuf = sBuf.PadLeft(2, '0');
                preGameTimeBox.Text = mBuf + ":" + sBuf;
            }
            mBuf = gameMinutes.ToString();
            sBuf = gameSeconds.ToString();
            sBuf = sBuf.PadLeft(2, '0');
            gameTimeBox.Text = mBuf + ":" + sBuf;
            mBuf = homeScore.ToString();
            sBuf = visitorScore.ToString();
            homeScoreBox.Text = mBuf;
            visitorScoreBox.Text = sBuf;
            startButton.Select();
        }
        

		private void setDefaults() {
			messageBox.Text = "Reading Application Defaults";
			clockInterval = 1000;
            rxDelay = 0;
            countDown = false;

            hstatPtr = vstatPtr = 0;
            if (dualMatch == false) {
                preGameSeconds = configPreGameSec;
                preGameMinutes = configPreGameMin;
                gameSeconds = configGameSec;
                gameMinutes = configGameMin;
            } else {
                preGameSeconds = configDualMatchPreGameSec;
                preGameMinutes = configDualMatchPreGameMin;
                gameSeconds = configDualMatchGameSec;
                gameMinutes = configDualMatchGameMin;
            }
            dualMatchPreGameMin = configDualMatchPreGameMin;
            dualMatchPreGameSec = configDualMatchPreGameSec;
            dualMatchGameMin = configDualMatchGameMin;
            dualMatchGameSec = configDualMatchGameSec;
            raceTo         = configRaceTo;			// If score spread is >= (n) is game over?
			audioMult      = configAudioMult;
			audioMode      = (byte)00;				// Solid tone
			audioRange     = configAudioRange;
			audioChannel   = configAudioChannel;
			audioVal       = configAudioDirValue;
            
			if (scoreboardDevice != null) {
				scoreboardDevice.Outputs.DataBuf[SIR_BIT] = (byte)audioMode;
				if (audioRange > 0)
					scoreboardDevice.Outputs.DataBuf[RAN_BIT] = (byte)audioRange;
				scoreboardDevice.Outputs.DataBuf[DIR_BIT] = (byte)audioVal;
			}
            toggleTeamsCheckBox.Checked = false;
            
            if (configAudioEn == true) {
				audioEnabledToolStripMenuItem.Checked = true;
				audioMute = false;
				hornButton.Enabled = true;
			} else {
				audioEnabledToolStripMenuItem.Checked = false;
				audioMute = true;
				hornButton.Enabled = false;
			}
			pwrUpOnStart = configPwrUp;			// Power up the display as app starts
			pwrOffEnd    = configPwrOff;
			homeScore    = 0;
			visitorScore = 0;
			hornTicks    = 0;
			preGameRunning = true;
			gameOver       = false;
            swapGameOver = false;

			setStartItems();
			homeTimeoutTaken    = false;
			visitorTimeoutTaken = false;
			dec10Button.Enabled = false;
			set10Button.Enabled = false;
            homeVicButtonisStop = true;         // Does home hang  button issue a "Stop" or "Increment" the point
            visVicButtonisStop  = true;         // Does visitor hang button stop clock or inc the point

            homeTimeoutButton.Enabled = false;
			visitorTimeoutButton.Enabled = false;
            preGameTimeBox.BackColor = Color.LightYellow;
            gameTimeBox.BackColor = Color.LightYellow;
            homeHangButton.BackColor = SystemColors.Control; // So we can test the buttons
            homeConcedeButton.BackColor = SystemColors.Control;
            visitorHangButton.BackColor = SystemColors.Control;
            visitorConcedeButton.BackColor = SystemColors.Control;
            homeScoreBox.BackColor = Color.LightYellow;
			visitorScoreBox.BackColor = Color.LightYellow;
            swapPreGameTimeColor = preGameTimeBox.BackColor;
            swapGameTimeColor = gameTimeBox.BackColor;
            swapHomeColor = homeScoreBox.BackColor;
            swapVisitorColor = visitorScoreBox.BackColor;

            swapVisitTimeoutButtonEnabled = false;
            swapHomeTimeoutButtonEnabled = false;
            swapVisitTimeoutTaken = false;
            swapHomeTimeoutTaken = false;
            swapGameSec = dualMatchGameSec;
            swapGameMin = dualMatchGameMin;
            swapPreGameSec = dualMatchPreGameSec;
            swapPreGameMin = dualMatchPreGameMin;
            swapVisitScore = visitorScore;
            swapHomeScore = homeScore;

            for (int x = 0; x < 20; x++) {
				homeStats[x] = "";
				visitorStats[x] = "";
			}

            if (File.Exists("C:\\BostonPaintball\\teams.txt")) {
                messageBox.Text = "Reading teams file";
                homeTeamComboBox.DataSource = File.ReadAllLines("C:\\BostonPaintball\\teams.txt");
                var hlist = homeTeamComboBox.Items.Cast<string>().ToList();
                //homeTeamComboBox.SelectedIndex = hlist.FindIndex(c => c.StartsWith("<"));  // Select the "< no name >" entry
                homeTeamComboBox.SelectedIndex = 1;   // Select the 1st element
                homeTeam2ComboBox.DataSource = File.ReadAllLines("C:\\BostonPaintball\\teams.txt");
                var hlist2 = homeTeam2ComboBox.Items.Cast<string>().ToList();
                //homeTeam2ComboBox.SelectedIndex = hlist2.FindIndex(c => c.StartsWith("<"));
                homeTeam2ComboBox.SelectedIndex = 2; // Select the 2nd element
                visitorTeamComboBox.DataSource = File.ReadAllLines("C:\\BostonPaintball\\teams.txt");
                var vlist = visitorTeamComboBox.Items.Cast<string>().ToList();
                //visitorTeamComboBox.SelectedIndex = vlist.FindIndex(c => c.StartsWith("<"));
                visitorTeamComboBox.SelectedIndex = 3;
                visitorTeam2ComboBox.DataSource = File.ReadAllLines("C:\\BostonPaintball\\teams.txt");
                var vlist2 = visitorTeam2ComboBox.Items.Cast<string>().ToList();
                //visitorTeam2ComboBox.SelectedIndex = vlist2.FindIndex(c => c.StartsWith("<"));
                visitorTeam2ComboBox.SelectedIndex = 4;
            }

            //gameClockTimer.Interval = clockInterval;
            if (SWLicense == true) {
                if (sw_clk_override == false) {
                    configNormTimer = 650;  // Using SW license has its disadvantages :)
                    MessageBox.Show("Software license only. Clock set to 650");
                } else {
                    configNormTimer = 950;  // Using SW license has its disadvantages :)
                    MessageBox.Show("Software license with clock override!");
                }
            }
			gameClockTimer.Interval = configNormTimer;
			clockInterval = configNormTimer;
			clockSpeedBox.Text = "Clock: " + configNormTimer;
			startButton.Select();
			updateAppTimes();
		}

        public void readTeamsFile() {
            StreamReader sr = new StreamReader(@"C:\BostonPaintball\teams.txt");
            string line = sr.ReadLine();

            messageBox.Text = "Reading teams file";
            homeTeamComboBox = null;
            while (line != null) {
                homeTeamComboBox.Items.Add(line);
                //visitorTeamComboBox.Items.Add(line);
                line = sr.ReadLine();
            }
        }
		private void editTimesToolStripMenuItem_Click(object sender, EventArgs e) {
			if (editTimesToolStripMenuItem.Checked == false) {
				editTimesToolStripMenuItem.Checked = true;
				preGameTimeBox.Select();
				preGameTimeBox.ReadOnly = false;
				preGameTimeBox.BackColor = Color.LightBlue;
				gameTimeBox.ReadOnly = false;
				gameTimeBox.BackColor = Color.LightBlue;
				if (homeTimeoutTaken == true)
					homeTimeTakenBox.Checked = true;
				homeTimeTakenBox.Visible = true;
				if (visitorTimeoutTaken == true)
					visTimeTakenBox.Checked = true;
				visTimeTakenBox.Visible = true;
				readTimeBoxes();
			} else {
				clearEditBoxes();
			}
		}

		private void clearEditBoxes() {
			editTimesToolStripMenuItem.Checked = false;
			preGameTimeBox.ReadOnly = true;
			preGameTimeBox.BackColor = Color.LightYellow;
			gameTimeBox.ReadOnly = true;
			gameTimeBox.BackColor = Color.LightYellow;
			if (homeTimeTakenBox.Checked == true)
				homeTimeoutTaken = true;
			else
				homeTimeoutTaken = false;
			homeTimeTakenBox.Visible = false;
			if (visTimeTakenBox.Checked == true)
				visitorTimeoutTaken = true;
			else
				visitorTimeoutTaken = false;
			visTimeTakenBox.Visible = false;
			readTimeBoxes();
		}

		private void startButton_Click(object sender, EventArgs e) {
			startStopFunction();
		}

		private void setStartItems() {
			homeHangButton.Enabled = false;
			homeConcedeButton.Enabled = false;
			visitorHangButton.Enabled = false;
			visitorConcedeButton.Enabled = false;

            if (dualMatchToolStripMenuItem.Checked == false) {
                dualMatch = false;
                showStatsToolStripMenuItem.Checked = false;
                showStatsToolStripMenuItem.Enabled = false;
                homeStatBox.Visible = false;
                visitorStatBox.Visible = false;
                homeTeam2ComboBox.Visible = false;
                visitorTeam2ComboBox.Visible = false;
                toggleTeamsCheckBox.Visible = false;
                altGameHome.Visible = false;
                altGameTime.Visible = false;
                altGameVisitor.Visible = false;
            } else {
                dualMatch = true;
                dualMatchToolStripMenuItem.Checked = true;
                showStatsToolStripMenuItem.Checked = false;
                showStatsToolStripMenuItem.Enabled = true;
                homeStatBox.Visible = false;
                visitorStatBox.Visible = false;
                homeTeam2ComboBox.Visible = true;
                visitorTeam2ComboBox.Visible = true;
                toggleTeamsCheckBox.Visible = true;
                altGameHome.Visible = true;
                altGameTime.Visible = true;
                altGameVisitor.Visible = true;
            }
		}

		private void startStopFunction() {
			/* 
			 * First, if we were editing the times.. set the boxes as readonly and adjust time; then continue
			 */
			if (editTimesToolStripMenuItem.Checked == true) {
				editTimesToolStripMenuItem.Checked = false;
				clearEditBoxes();
			}

			if (startButton.Text == "Start") {
				startButton.Text = "Stop";
				startButton.BackColor = Color.LightCoral;
				dec10Button.Enabled = true;
				set10Button.Enabled = true;
				if (homeTimeoutTaken == false)
					homeTimeoutButton.Enabled = true;
				if (visitorTimeoutTaken == false)
					visitorTimeoutButton.Enabled = true;
				gameClockRunning = true;
				gameClockTimer.Start();
			} else {
				if (preGameRunning == false) {      // Actuall game is running so, reset pregame and start it.
					stopSoundCtl();
                    if (dualMatch == false) {
                        preGameRunning = true;
                        preGameMinutes = configPreGameMin;
                        preGameSeconds = configPreGameSec;
                        dec10Button.Enabled = true;
                        set10Button.Enabled = true;
                        preGameTimeBox.BackColor = Color.LightYellow;
                        if (homeTimeoutTaken == false)
                            homeTimeoutButton.Enabled = true;
                        if (visitorTimeoutTaken == false)
                            visitorTimeoutButton.Enabled = true;
                    } else {
                        startButton.Text = "Start";
                        startButton.BackColor = Color.GreenYellow;

                        homeTimeoutButton.Enabled = false;
                        visitorTimeoutButton.Enabled = false;

                        preGameRunning = true;
                        gameClockRunning = false;
                        gameClockTimer.Stop();
                        setStartItems();

                        preGameMinutes = configDualMatchPreGameMin;
                        preGameSeconds = configDualMatchPreGameSec;
                        dec10Button.Enabled = true;
                        set10Button.Enabled = true;
                        preGameTimeBox.BackColor = Color.LightYellow;
                    }
                } else {                            // Pregame is running so stop everything.
					startButton.Text = "Start";
					startButton.BackColor = Color.GreenYellow;

					homeTimeoutButton.Enabled = false;
					visitorTimeoutButton.Enabled = false;

					gameClockRunning = false;
					gameClockTimer.Stop();
					setStartItems();
				}
			}
		}

		private void ScoreForm_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Space) {
                startStopFunction();
            }

        }

        private void set10Button_Click(object sender, EventArgs e) {
			preGameSeconds = 10;
			preGameMinutes = 0;
			startButton.Select();
			updateAppTimes();
		}

		private void dec10Button_Click(object sender, EventArgs e) {
			if (preGameSeconds > 10) {
				preGameSeconds -= 10;
			}
			startButton.Select();
			updateAppTimes();
		}

		private void resetButton_Click(object sender, EventArgs e) {
            if (gameClockRunning == true) {
                DialogResult input = MessageBox.Show("Are you sure you want to reset the game?", "Reset Game", MessageBoxButtons.YesNo);
                if (input == DialogResult.No)
                {
                    return;
                }
            }
			for (int x = 0; x < 20; x++) {
				homeStats[x] = "";
				visitorStats[x] = "";
			}
			vstatPtr = 0;
			hstatPtr = 0;
			homeStatBox.Clear();
			visitorStatBox.Clear();

			gameClockTimer.Stop();
			startButton.Text = "Start";
			startButton.BackColor = Color.GreenYellow;
			startButton.Select();
			setDefaults();
            readConfigs(); // Added 7/10/17
            updateAppTimes();
			updateDisplay();
		}

		private void displayPowerToolStripMenuItem_Click(object sender, EventArgs e) {
			if (scoreboardDevice != null) {
				displayPowerToolStripMenuItem.Enabled = true;
				if (displayPowerToolStripMenuItem.Checked == false) {
					scoreboardDevice.Outputs.DataBuf[CMD_BIT] = PS_ON;	// Tell FW to turn PS on.
				} else {
					scoreboardDevice.Outputs.DataBuf[CMD_BIT] = PS_OFF;	// Tell FW to turn PS off.
				}
				writeUSB();
			} else {
				displayPowerToolStripMenuItem.Enabled = false;
			}
		}

		/*
		 * Update tool bar when we click it
		 */
		private void toolsToolStripMenuItem_Click(object sender, EventArgs e) {
			if (scoreboardDevice != null) {
				displayPowerToolStripMenuItem.Enabled = true;
				audioChannel1ToolStripMenuItem.Enabled = true;
				audioEnabledToolStripMenuItem.Enabled = true;
				readDeviceToolStripMenuItem.Enabled = true;
                countdownBeepToolStripMenuItem.Enabled = true;
				readUSB();	// Get power supply actual status.
				System.Threading.Thread.Sleep(100);
				if (scoreboardDevice.Inputs.DataBuf[PS_STAT_IN] != 0) {
					displayPowerToolStripMenuItem.Checked = true;
				} else {
					displayPowerToolStripMenuItem.Checked = false;
				}
			} else {
				displayPowerToolStripMenuItem.Enabled = false;
				audioChannel1ToolStripMenuItem.Enabled = false;
				audioEnabledToolStripMenuItem.Enabled = false;
				readDeviceToolStripMenuItem.Enabled = false;
                countdownBeepToolStripMenuItem.Enabled = false;
            }
		}

		private void showUSBBuffersToolStripMenuItem_Click(object sender, EventArgs e) {
			if (showUSBBuffersToolStripMenuItem.Checked == false) {
				showUSBBuffersToolStripMenuItem.Checked = true;
				inputBufferBox.Visible = true;
				outBufferBox.Visible = true;
			} else {
				showUSBBuffersToolStripMenuItem.Checked = false;
				inputBufferBox.Visible = false;
				outBufferBox.Visible = false;
			}
		}

		private void homeScoreMgr(bool incDec, bool horn, bool wireless) {
			if (horn == true) {
				hangSoundCtl();
			}
			if (incDec == true) {	// Increment
                // Move this to botom so the score is incremented before we check if it was a wireless push
                //if (homeVicButtonisStop == true && wireless == true) {  // The victory/concede button cause a STOP not an automatic INC to the score.
                //    startStopFunction();
                //    return;
                //}
				if (homeScore >= 9) {
					return;
				}
                if (wireless == false) {
                    homeScore++;  // Dont inc the score if wireless button push (we wont know which team it was that pushed it)
                }
                if (scoreboardDevice != null) {
                    scoreboardDevice.Outputs.DataBuf[HOM_BIT] = (byte)homeScore;
                    writeUSB();
                }
				if (homeScore - visitorScore >= raceTo) {
					homeScoreBox.BackColor = Color.Red;
				} else {
					homeScoreBox.BackColor = Color.LightYellow;
				}
                if (homeVicButtonisStop == true && wireless == true) {  // The victory/concede button cause a STOP not an automatic INC to the score.
                    startStopFunction();
                    return;
                }
            } else {
				if (homeScore <= 0) {
					return;
				}
				homeScore--;
                if (scoreboardDevice != null) {
                    scoreboardDevice.Outputs.DataBuf[HOM_BIT] = (byte)homeScore;
                    writeUSB();
                }
				homeScoreBox.Text = homeScore.ToString();
				if (homeScore - visitorScore >= raceTo) {
					homeScoreBox.BackColor = Color.Red;
				} else {
					homeScoreBox.BackColor = Color.LightYellow;
				}
			}
			updateAppTimes();
		}

		private void visitorScoreMgr(bool incDec, bool horn, bool wireless) {
			if (horn == true) {
				hangSoundCtl();
			}
			if (incDec == true) {	// Increment
                // Move this to botom so the score is icremented before we check if it was a wireless push
                //if (visVicButtonisStop == true && wireless == true) {  // The victory/concede button cause a STOP not an automatic INC to the score.
                //    startStopFunction();
                //    return;
                //}
                if (visitorScore >= 9) {
					return;
				}
                if (wireless == false) {
                    visitorScore++;  // Dont inc the score if wireless button push (we wont know which team it was that pushed it)
                }
                if (scoreboardDevice != null) {
                    scoreboardDevice.Outputs.DataBuf[VIS_BIT] = (byte)visitorScore;
                    writeUSB();
                }
				visitorScoreBox.Text = visitorScore.ToString();
				if (visitorScore - homeScore >= raceTo) {
					visitorScoreBox.BackColor = Color.Red;
				} else {
					visitorScoreBox.BackColor = Color.LightYellow;
				}
                if (visVicButtonisStop == true && wireless == true) {  // The victory/concede button cause a STOP not an automatic INC to the score.
                    startStopFunction();
                    return;
                }
            } else {
				if (visitorScore <= 0) {
					return;
				}
				visitorScore--;

                if (scoreboardDevice != null) {
                    scoreboardDevice.Outputs.DataBuf[VIS_BIT] = (byte)visitorScore;
                    writeUSB();
                }
				visitorScoreBox.Text = visitorScore.ToString();
				if (visitorScore - homeScore >= raceTo) {
					visitorScoreBox.BackColor = Color.Red;
				} else {
					visitorScoreBox.BackColor = Color.LightYellow;
				}
			}
			updateAppTimes();
		}

		private void visitorScoreIncButton_Click(object sender, EventArgs e) {
			visitorScoreMgr(true, false, false);
			visitorStatsAddHang();
		}

		private void visitorScoreDecButton_Click(object sender, EventArgs e) {
			visitorScoreMgr(false, false, false);
			visitorStatsDecHang();
		}

		private void exitToolStripMenuItem1_Click(object sender, EventArgs e) {
			if (pwrOffEnd == true) {
				if (scoreboardDevice != null)
					scoreboardDevice.Outputs.DataBuf[CMD_BIT] = 0x1A;	// Tell FW to turn PS off.
				System.Threading.Thread.Sleep(250);
				writeUSB();
				System.Threading.Thread.Sleep(250);
			}
			Application.Exit();
		}

		private void sirenBox_CheckedChanged(object sender, EventArgs e) {
			//if (sirenBox.Checked == true) {
			//	audioMode = 1;
			//} else {
			//	audioMode = 0;
			//}
            audioMode = (byte)0;
            if (scoreboardDevice != null) {
				if (scoreboardDevice != null)
					scoreboardDevice.Outputs.DataBuf[SIR_BIT] = (byte)audioMode;
				writeUSB();
			}
		}

        private void homeHangButton_Click(object sender, EventArgs e) {
            homeScoreMgr(true, true, true);   // Inc score and sound horn and say we're a wireless device to stop clock (true, true, true)
            homeStatsAddHang();
            updatePoint();
        }

		private void homeStatsAddHang() {
			String bufm, bufs;

			if (hstatPtr > 9) {
				return;
			}
			bufm = gameMinutes.ToString();
			bufs = gameSeconds.ToString();
			bufs = bufs.PadLeft(2, '0');
			homeStats[hstatPtr] = homeTeam + " Scored @ " + bufm + ":" + bufs + "\r\n";
			homeStatBox.Text += homeStats[hstatPtr++];
			//updatePoint();
		}

		private void homeStatsDecHang() {
			if (hstatPtr <= 0) {
				return;
			}

			homeStats[--hstatPtr] = "";
			homeStatBox.Clear();
			for (int x = 0; x < hstatPtr; x++) {
				homeStatBox.Text += homeStats[x];
			}
		}

		private void homeStatsAddConc() {
			String bufm, bufs;

			if (vstatPtr > 9) {
				return;
			}
			bufm = gameMinutes.ToString();
			bufs = gameSeconds.ToString();
			bufs = bufs.PadLeft(2, '0');
			visitorStats[vstatPtr] = homeTeam + " Conceded @ " + bufm + ":" + bufs + "\r\n";
			visitorStatBox.Text += visitorStats[vstatPtr++];
		}

		private void visitorStatsAddHang() {
			String bufm, bufs;

			if (vstatPtr > 9) {
				return;
			}
			bufm = gameMinutes.ToString();
			bufs = gameSeconds.ToString();
			bufs = bufs.PadLeft(2, '0');
			visitorStats[vstatPtr] = visitorTeam + " Scored @ " + bufm + ":" + bufs + "\r\n";
			visitorStatBox.Text += visitorStats[vstatPtr++];
		}

		private void visitorStatsAddConc() {
			String bufm, bufs;

			if (hstatPtr > 9) {
				return;
			}
			bufm = gameMinutes.ToString();
			bufs = gameSeconds.ToString();
			bufs = bufs.PadLeft(2, '0');
			homeStats[hstatPtr] = visitorTeam + " Conceded @ " + bufm + ":" + bufs + "\r\n";
			homeStatBox.Text += homeStats[hstatPtr++];
		}

		private void visitorStatsDecHang() {
			if (vstatPtr <= 0) {
				return;
			}
			visitorStats[--vstatPtr] = "";
			visitorStatBox.Clear();
			for (int x = 0; x < vstatPtr; x++) {
				visitorStatBox.Text += visitorStats[x];
			}
		}

		private void updatePoint() {
            if (dualMatch == false) {
                preGameMinutes = configPreGameMin;
                preGameSeconds = configPreGameSec;
            } else {
                preGameMinutes = configDualMatchPreGameMin;
                preGameSeconds = configDualMatchPreGameSec;
            }
			preGameTimeBox.BackColor = Color.LightYellow;
			preGameRunning = true;
			dec10Button.Enabled = true;
			set10Button.Enabled = true;
			if (homeTimeoutTaken == false)
				homeTimeoutButton.Enabled = true;
			if (visitorTimeoutTaken == false)
				visitorTimeoutButton.Enabled = true;
			updateAppTimes();
			updateDisplay();
		}

		private void homeScoreIncButton_Click(object sender, EventArgs e) {
			homeScoreMgr(true, false, false);	// Inc score no sound
			homeStatsAddHang();
		}

		private void homeConcedeButton_Click(object sender, EventArgs e) {
			//timeoutSoundCtl();
			visitorScoreMgr(true, true, false);	// Inc score with sound
			homeStatsAddConc();
			updatePoint();
            startStopFunction(); // 5/23
        }

		private void homeScoreDecButton_Click(object sender, EventArgs e) {
			homeScoreMgr(false, false, false);
			homeStatsDecHang();
		}

		private void visitorConcedeButton_Click(object sender, EventArgs e) {
			//timeoutSoundCtl();
			homeScoreMgr(true, true, false);
			visitorStatsAddConc();
			updatePoint();
            startStopFunction();   // 5/23
        }

		private void visitorHangButton_Click(object sender, EventArgs e) {
			//hangSoundCtl();
			visitorScoreMgr(true, true, true);
			visitorStatsAddHang();
            updatePoint();
        }

		private void homeTimeoutMgr() {
			String bufm, bufs;

			if (homeTimeoutTaken == false) {
				if (preGameMinutes == 0) {
					if (preGameSeconds <= configMinTimeout) {
						return;		// No timeout at this point.
					}
				}

				timeoutSoundCtl();
				homeTimeoutTaken = true;
				homeTimeoutButton.Enabled = false;
				preGameMinutes += configTimeoutMinutes;

				bufm = gameMinutes.ToString();
				bufs = gameSeconds.ToString();
				bufs = bufs.PadLeft(2, '0');
				homeStats[hstatPtr] = homeTeam + " Timeout @ " + bufm + ":" + bufs + "\r\n";
				homeStatBox.Text += homeStats[hstatPtr++];
				visitorStats[vstatPtr] = homeTeam + " Timeout @ " + bufm + ":" + bufs + "\r\n";
				visitorStatBox.Text += visitorStats[vstatPtr++];
            }
		}

		private void homeTimeoutButton_Click(object sender, EventArgs e) {
            /* TEST TO SIMULATE WIRELESS BUTTON PUSH  -- REMOVE -- */
            //if (preGameRunning == true) {            // If pregame is running this is a timeout.
            //    if (gameClockRunning == true) {
            //        if (homeTimeoutTaken == false) {
            //            homeTimeoutMgr();
            //            updateDisplay();
            //            updateAppTimes();
            //            startStopFunction();
            //            MessageBox.Show(homeTeam + " takes timeout");
            //        } else {
            //            MessageBox.Show(homeTeam + " pressed timeout, but they already took their timeout!");
            //        }
            //        //homeTimeoutMgr();
            //    }
            //} else {
            //    visitorScoreMgr(true, true, true);
            //    homeStatsAddConc();
            //    updatePoint();
            //    MessageBox.Show(homeTeam + " has conceded");
            //}

            homeTimeoutMgr();
        }

		private void visitorTimeoutMgr() {
			String bufm, bufs;

			if (visitorTimeoutTaken == false) {
				if (preGameMinutes == 0) {
					if (preGameSeconds <= configMinTimeout) {
						return;		// No timeout at this point.
					}
				}
				timeoutSoundCtl();
				visitorTimeoutTaken = true;
				visitorTimeoutButton.Enabled = false;
				preGameMinutes += configTimeoutMinutes;

				bufm = gameMinutes.ToString();
				bufs = gameSeconds.ToString();
				bufs = bufs.PadLeft(2, '0');
				visitorStats[vstatPtr] = visitorTeam + " Timeout @ " + bufm + ":" + bufs + "\r\n";
				visitorStatBox.Text += visitorStats[vstatPtr++];
				homeStats[hstatPtr] = visitorTeam + " Timeout @ " + bufm + ":" + bufs + "\r\n";
				homeStatBox.Text += homeStats[hstatPtr++];
			}
		}

		private void visitorTimeoutButton_Click(object sender, EventArgs e)
		{
			visitorTimeoutMgr();
		}

		private void pauseButton_Click(object sender, EventArgs e) {
			pauseMgr();
		}

		private void pauseMgr() {
			if (gamePaused == false) {
				gameClockTimer.Enabled = false;
				pauseButton.Text = "Resume";
				pauseButton.BackColor = Color.Coral;
				gamePaused = true;
			} else {
				if (gameClockRunning == true)
					gameClockTimer.Enabled = true;
				pauseButton.Text = "Pause";
				pauseButton.BackColor = Color.LightBlue;
				gamePaused = false;
			}
		}

		private void newGameButton_Click(object sender, EventArgs e) {
			if (validLicense == false)
				return;
			
			DialogResult input = MessageBox.Show("Are you sure you want to end this game?", "Abort Game", MessageBoxButtons.YesNo);
			if (input == DialogResult.No) {
				return;
			}
			setDefaults();
			preGameSeconds = configNewGameSec;
			preGameMinutes = configNewGameMin;

			updateAppTimes();
			updateDisplay();
			startStopFunction();
		}

		private void audioEnabledToolStripMenuItem_Click(object sender, EventArgs e) {
			if (scoreboardDevice != null) {
				audioEnabledToolStripMenuItem.Enabled = true;
				if (audioEnabledToolStripMenuItem.Checked) {
					audioMute = true;
					audioEnabledToolStripMenuItem.Checked = false;
					hornButton.Enabled = false;
				} else {
					audioMute = false;
					audioEnabledToolStripMenuItem.Checked = true;
					hornButton.Enabled = true;
				}
				updateDisplay();
			} else {
				audioEnabledToolStripMenuItem.Enabled = false;
			}
		}

		private void audioChannel1ToolStripMenuItem_Click(object sender, EventArgs e) {
			if (scoreboardDevice != null) {
				audioChannel1ToolStripMenuItem.Enabled = true;
				if (audioChannel1ToolStripMenuItem.Checked == true) {		// Is set to chan 1, so reset and uncheck
					audioChannel = 0;
					audioChannel1ToolStripMenuItem.Checked = false;
				} else {													// Is set to chan 0, set to chan 1 and check
					audioChannel = 1;
					audioChannel1ToolStripMenuItem.Checked = true;
				}
				scoreboardDevice.Outputs.DataBuf[ACH_BIT] = (byte)audioChannel;
				updateDisplay();
			} else {
				audioChannel1ToolStripMenuItem.Enabled = false;
			}
		}

		private void configurationToolStripMenuItem_Click(object sender, EventArgs e) {
			Stream myStream = null;
			OpenFileDialog configDialog = new OpenFileDialog();

			configDialog.InitialDirectory = "c:\\BostonPaintball";
			configDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
			configDialog.FilterIndex = 2;
			configDialog.RestoreDirectory = true;

			if (configDialog.ShowDialog() == DialogResult.OK) {
				try {
					if ((myStream = configDialog.OpenFile()) != null) {
						using (myStream) {
							string filename = configDialog.FileName;
							String[] lines = File.ReadAllLines(filename);
						}
					}
				} catch (Exception ex) {
					MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
				}
			}
		}

		// In case we can't read the 'config' file we set default values here.
		private void setConfigDefaults() {
			configTimeoutMinutes   =  2;
			configTimeoutSeconds   =  0;
			configMinTimeout       = 10;		// No timeouts <= these seconds
			configPreGameMin       =  2;
			configPreGameSec       =  0;
			configGameMin          = 12;
			configGameSec          =  0;
			configNewGameMin       =  7;
			configNewGameSec       =  0;			
			configAudioRange       = 40;
			configAudioMult        = 24;
			configRaceTo           =  4;
			configAudioChannel     =  0;
			configAudioDirValue    =  2;
			configEndOfGameChannel =  1;
			configTimer            = 1000;
			configNormTimer        = 1000;
            configDualMatchGameMin = 12;
            configDualMatchGameSec = 0;
            configDualMatchPreGameMin = 1;
            configDualMatchPreGameSec = 0;
            audioCountdownStart = 10;

            configRelayEn			  = false;
			configAudioEn			  = true;
			configSoundOnStop		  = true;
			configSoundOnReset		  = true;
			configSoundOnTimeout	  = true;
			configSoundOnHang		  = true;
			configSoundOnEndOfPreGame = true;
			configSoundOnConcede	  = true;
			configSoundOnEndOfGame    = true;
			configEndOfGameSiren	  = false;
            audioCountdownEnabled     = false;

            countDownFrom = 10;

			//configDbgTimer;
			//configDbgPreGameMin = 0;
			//configDbgPreGameSec = 12;
			//configDbgGameMin = 10;
			//configDbgGameSec = 0;
		}

		private void readConfigs() {
			string line, tmp;
			string[] parse, newline;
            int lineNo = 0;

            setConfigDefaults();

            if (File.Exists("C:\\BostonPaintball\\config.txt")) {
				var file = new StreamReader("C:\\BostonPaintball\\config.txt");
				while ((line = file.ReadLine()) != null) {
                    lineNo++;
					if (line.Length < 1)
						continue;
					tmp = line.Substring(0, 1);
					if (tmp.Equals("#") || tmp.Equals(""))
						continue;
					newline = line.Split(null);		// Split on any white space
					parse = newline[0].Split(':');	// Now split on ':' for actual value
                    if (parse.Length < 2) {
                        MessageBox.Show("Config file at line: " + lineNo + " is invalid");
                        continue;
                    }
					if (String.Compare(parse[1], "") == 0)          // Dont process invalid string pairs.
						continue;
                    if (String.Compare(parse[0], "countDownFrom") == 0)
                        countDownFrom = Convert.ToInt16(parse[1]);
                    if (String.Compare(parse[0], "dualMatchPreGameMin") == 0)
                        configDualMatchPreGameMin = Convert.ToInt16(parse[1]);
                    if (String.Compare(parse[0], "dualMatchPreGameSec") == 0)
                        configDualMatchPreGameSec = Convert.ToInt16(parse[1]);
                    if (String.Compare(parse[0], "dualMatchGameMin") == 0)
                        configDualMatchGameMin = Convert.ToInt16(parse[1]);
                    if (String.Compare(parse[0], "dualMatchGameSec") == 0)
                        configDualMatchGameSec = Convert.ToInt16(parse[1]);
                    if (String.Compare(parse[0], "preGameMin") == 0)
						configPreGameMin = Convert.ToInt16(parse[1]);
					if (String.Compare(parse[0], "preGameSec") == 0)
						configPreGameSec = Convert.ToInt16(parse[1]);
					if (String.Compare(parse[0], "newGameMin") == 0)
						configNewGameMin = Convert.ToInt16(parse[1]);
					if (String.Compare(parse[0], "newGameSec") == 0)
						configNewGameSec = Convert.ToInt16(parse[1]);
					if (String.Compare(parse[0], "GameMin") == 0)
						configGameMin = Convert.ToInt16(parse[1]);
					if (String.Compare(parse[0], "GameSec") == 0)
						configGameSec = Convert.ToInt16(parse[1]);
					if (String.Compare(parse[0], "timeoutMin") == 0)
						configTimeoutMinutes = Convert.ToInt16(parse[1]);
					if (String.Compare(parse[0], "timeoutSec") == 0)
						configTimeoutSeconds = Convert.ToInt16(parse[1]);
					if (String.Compare(parse[0], "timeoutMinSec") == 0)		// Don't allow a timeout at this preGame time (no timeouts at 0:10)
						configMinTimeout = Convert.ToInt16(parse[1]);
					if (String.Compare(parse[0], "normTimer") == 0)			// Game clock interval
						configNormTimer = Convert.ToInt16(parse[1]);
					if (String.Compare(parse[0], "audioChannel") == 0)		// Default audio channel
						configAudioChannel = Convert.ToInt16(parse[1]);
					if (String.Compare(parse[0], "audioDirValue") == 0)		// Value FW uses to inc/dec audio siren tone (0 == solid tone, higher #'s make faster tone wobble
						configAudioDirValue = Convert.ToInt16(parse[1]);
					if (String.Compare(parse[0], "audioMult") == 0)			// Value FW uses as "aPeriod" multiplier (24 is a good default)
						configAudioMult = Convert.ToInt16(parse[1]);
					if (String.Compare(parse[0], "audioRange") == 0)		// How hi/lo to count from 'aPeriod'
						configAudioRange = Convert.ToInt16(parse[1]);
					if (String.Compare(parse[0], "raceTo") == 0)			// If the difference between scores is >= 'raceTo' change background color to indicate match over.
						configRaceTo = Convert.ToInt16(parse[1]);
					if (String.Compare(parse[0], "endOfGameChannel") == 0)	// Default channel to use at end of game sound
						configEndOfGameChannel = Convert.ToInt16(parse[1]);

					if (String.Compare(parse[0], "powerOnStart") == 0)		// Power up display when APP starts
						if (String.Compare(parse[1], "no") == 0)
							configPwrUp = false;
						else
							configPwrUp = true;
					if (String.Compare(parse[0], "powerOffEnd") == 0)		// Power off display when APP exits
						if (String.Compare(parse[1], "no") == 0)
							configPwrOff = false;
						else
							configPwrOff = true;
					if (String.Compare(parse[0], "soundOnStop") == 0)		// Do we make sound when "Stop" is pushed
						if (String.Compare(parse[1], "no") == 0)
							configSoundOnStop = false;
						else
							configSoundOnStop = true;
					if (String.Compare(parse[0], "soundOnReset") == 0)		// Do we make sound when "Reset" is pushed
						if (String.Compare(parse[1], "no") == 0)
							configSoundOnReset = false;
						else
							configSoundOnReset = true;
					if (String.Compare(parse[0], "soundOnTimeout") == 0)	// Do we make sound when "Timeout" is pushed
						if (String.Compare(parse[1], "no") == 0) {
							configSoundOnTimeout = false;
						} else {
							configSoundOnTimeout = true;
						}
					if (String.Compare(parse[0], "soundOnConcede") == 0)	// Do we make sound when "Concede" is pushed
						if (String.Compare(parse[1], "no") == 0)
							configSoundOnConcede = false;
						else
							configSoundOnConcede = true;
					if (String.Compare(parse[0], "soundOnHang") == 0)		// Do we make sound when "Concede" is pushed
						if (String.Compare(parse[1], "no") == 0)
							configSoundOnHang = false;
						else
							configSoundOnHang = true;
					if (String.Compare(parse[0], "endOfGameSiren") == 0)	// Use siren (instead of tone) at end of game
						if (String.Compare(parse[1], "no") == 0)
							configEndOfGameSiren = false;
						else
							configEndOfGameSiren = true;
					if (String.Compare(parse[0], "soundOnEndOfPreGame") == 0)	// Do we make sound at end of PRE-game
						if (String.Compare(parse[1], "no") == 0)
							configSoundOnEndOfPreGame = false;
						else
							configSoundOnEndOfPreGame = true;
					if (String.Compare(parse[0], "soundOnEndOfGame") == 0)	// Do we make sound at end of game
						if (String.Compare(parse[1], "no") == 0)
							configSoundOnEndOfGame = false;
						else
							configSoundOnEndOfGame = true;

					if (String.Compare(parse[0], "relayEnabled") == 0)		// N/A with this FW
						if (String.Compare(parse[1], "no") == 0)
							configRelayEn = false;
						else
							configRelayEn = true;
					if (String.Compare(parse[0], "audioEnabled") == 0)		// Is audio enabled by default
						if (String.Compare(parse[1], "no") == 0) {
							configAudioEn = false;
						} else {
							configAudioEn = true;
						}
                    //if (String.Compare(parse[0], "audioCountdownEnabled") == 0)      // Is audio enabled by default
                    //    if (String.Compare(parse[1], "no") == 0) {
                    //        audioCountdownEnabled = false;
                    //    } else {
                    //        audioCountdownEnabled = true;
                    //    }
                }
				file.Close();
			} else {
				MessageBox.Show("No Config file found. Using internal defaults.");
                configNewGameMin = 7;
                configNewGameSec = 0;
                configGameMin = 12;
                configGameSec = 0;
                configPreGameMin = 2;
                configPreGameSec = 0;
                configTimeoutMinutes = 2;
                configTimeoutSeconds = 0;
                configNormTimer = 1000;
                configMinTimeout = 10;
                countDownFrom = 10;
                configAudioEn = true;
                configRelayEn = false;
                configSoundOnEndOfGame = true;
                configSoundOnEndOfPreGame = true;
                configEndOfGameSiren = false;
                configSoundOnConcede = true;
                configSoundOnTimeout = true;
                configSoundOnReset = false;
                configSoundOnStop = true;
                configPwrUp = true;
                configPwrOff = true;
                audioCountdownEnabled = false;
                audioCountdownStart = 10;
            }
		}

		private void editToolStripMenuItem_Click(object sender, EventArgs e) {
			if (gameClockRunning == true) {
				editTimesToolStripMenuItem.Enabled = false;
				teamsToolStripMenuItem.Enabled = false;
				configurationToolStripMenuItem.Enabled = false;
			} else {
				editTimesToolStripMenuItem.Enabled = true;
				teamsToolStripMenuItem.Enabled = true;
				configurationToolStripMenuItem.Enabled = true;
			}
		}

		private void teamsToolStripMenuItem_Click(object sender, EventArgs e) {

		}

        private void countDownSoundCtl() {
            if (countdownBeepToolStripMenuItem.Checked == true) {
                messageBox.Text = "Count down beep ..";
                hornTicks = 1;
                audioChannel = 0;
                audioMode = TONE_MODE;
                hornOn = true;
                hornCtl();
            }
        }
        private void stopSoundCtl() {
			messageBox.Text = "Stopsnd ctl()";
			if (configSoundOnStop == true) {
				hornTicks = 10;
				audioChannel = 0;
//				audioMode = SIREN_MODE;
				audioMode = TONE_MODE;
				hornOn = true;
				hornCtl();
			}
		}

		private void timeoutSoundCtl() {
			if (configSoundOnTimeout == true) {
				messageBox.Text = "Timeout sound";
				hornTicks = 10;
				audioChannel = 0;
                audioMode = TONE_MODE;
				hornOn = true;
				hornCtl();
			}
		}

		private void hangSoundCtl() {
			if (configSoundOnTimeout == true) {
				messageBox.Text = "Hang sound";
				hornTicks = 10;
				//audioVal = 4;
				//audioChannel = 1;
				audioMode = TONE_MODE;
				hornOn = true;
				hornCtl();
			}
		}

		private void endOfGameSoundCtl() {
			if (configSoundOnEndOfGame) {
				hornTicks = 14;
				//audioChannel = configEndOfGameChannel;
				//if (configEndOfGameSiren == true) {
				//	audioMult = SIREN_MODE;
				//} else {
				//	audioMult = TONE_MODE;
				//}
				hornOn = true;
				hornCtl();
			}
		}

		private void endOfPreGameSoundCtl() {
			if (configSoundOnEndOfPreGame == true) {
				hornTicks = 6;
				audioChannel = 0;
				//audioVal = 4;
				audioMode = TONE_MODE;
				hornOn = true;
				hornCtl();
			}
		}

		private void hornTimer_Tick(object sender, EventArgs e) {
			if (scoreboardDevice == null)
				return;
			if (audioMute == false) {
				if (hornTicks > 0) {
					hornTicks--;
					hornCtl();
				} else {
					hornOn = false;
					scoreboardDevice.Outputs.DataBuf[CMD_BIT] = AUDIO_OFF;	// Turn buzzer off.
					scoreboardDevice.Outputs.DataBuf[AEN_BIT] = (byte)0;	// Turn buzzer off.
					writeUSB();
					hornTimer.Stop();
				}
			} else {
				scoreboardDevice.Outputs.DataBuf[AEN_BIT] = (byte)0;	// Turn buzzer off.
				messageBox.Text = "Audio MUTE is set.";
			}
		}

		private void hornButton_Click(object sender, EventArgs e) {
			if (scoreboardDevice == null)
				return;
			if (hornButton.Text == "Horn") {
				if (audioMute == false) {
					scoreboardDevice.Outputs.DataBuf[CMD_BIT] = AUDIO_ON;	// Turn buzzer on.
					scoreboardDevice.Outputs.DataBuf[AEN_BIT] = (byte)1;	// Turn buzzer on.
                    audioMode = (byte)0;
     //               if (sirenBox.Checked == true) {
					//	audioMode = (byte)1;
					//} else {
					//	audioMode = (byte)0;
					//}
					scoreboardDevice.Outputs.DataBuf[SIR_BIT] = (byte)audioMode;
					hornButton.Text = "Horn Off";
					hornButton.BackColor = Color.LightCoral;
				}
			} else {
				scoreboardDevice.Outputs.DataBuf[CMD_BIT] = AUDIO_OFF;	// Turn buzzer off.
				scoreboardDevice.Outputs.DataBuf[AEN_BIT] = (byte)0;	// Turn buzzer off.
				hornButton.Text = "Horn";
				hornButton.BackColor = Color.LightBlue;
			}
			writeUSB();
		}

		private void hornCtl() {
            if (scoreboardDevice == null) {
                return;
            }
			if (audioMute == false) {
				if (hornOn == true) {
					if (hornTimer.Enabled == false) {
						scoreboardDevice.Outputs.DataBuf[ACH_BIT] = (byte)audioChannel;
						scoreboardDevice.Outputs.DataBuf[SIR_BIT] = (byte)audioMode;	// Solid tone or siren type sound
						scoreboardDevice.Outputs.DataBuf[DIR_BIT] = (byte)audioVal;		// Increment rate in audio sinewave (hi val gives higher tome).
						scoreboardDevice.Outputs.DataBuf[RAN_BIT] = (byte)audioRange;	// How hi/lo to count to make sinewave
						scoreboardDevice.Outputs.DataBuf[CMD_BIT] = AUDIO_ON;	// Turn buzzer on.
						scoreboardDevice.Outputs.DataBuf[AEN_BIT] = (byte)1;	// Turn buzzer on.
						hornTimer.Start();
						writeUSB();
					}
				}
			}
		}

		private void homeTeamComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            homeTeam = homeTeamComboBox.SelectedItem.ToString();
            homeLabel.Text = homeTeam;
        }

        private void homeTeamComboBox_TextChanged(object sender, EventArgs e) { // If the user manually enters a new team
            homeTeam = homeTeamComboBox.Text;
            homeLabel.Text = homeTeam;
        }

        private void visitorTeamComboBox_SelectedIndexChanged(object sender, EventArgs e) {
			visitorTeam = visitorTeamComboBox.SelectedItem.ToString();
            visitorLabel.Text = visitorTeam;
		}

        private void visitorTeamComboBox_TextChanged(object sender, EventArgs e) {  // If the user manually enters a new team
            visitorTeam = visitorTeamComboBox.Text;
            visitorLabel.Text = visitorTeam;
        }

        private void readDeviceToolStripMenuItem_Click(object sender, EventArgs e) {
			if (scoreboardDevice != null) {
				readDeviceToolStripMenuItem.Enabled = true;
				scoreboardDevice.Outputs.DataBuf[CMD_BIT] = REQ_FW;
				writeUSB();
				inputBufferBox.Text = "Reading Controller Inputs "; // +sb.ToString();
				inputBufferBox.Refresh();
				System.Threading.Thread.Sleep(200);		// Give USB some time.
				readUSB();
			} else {
				readDeviceToolStripMenuItem.Enabled = false;
			}
		}

		private void fileToolStripMenuItem_Click(object sender, EventArgs e) {
			if (scoreboardDevice != null) {
				resetToolStripMenuItem.Enabled = true;
			} else {
				resetToolStripMenuItem.Enabled = false;
			}
		}

		private void helpToolStripMenuItem_Click(object sender, EventArgs e) {
			//Process.Start(@"c:\BostonPaintball\help.doc");
			System.Diagnostics.Process.Start("wordpad.exe", @"C:\BostonPaintball\help.rtf");
		}

		//private void resetControllerToolStripMenuItem_Click(object sender, EventArgs e) {
		//    //if (scoreboardDevice != null) {
		//    //    warningBox.Text = "This operation is not functional yet";
		//    //} else {
		//    //    warningBox.Text = "No USB Connection .. Remove/Insert USB Cable";
		//    //    warningBox.BackColor = Color.LightCoral;
		//    //}
		//}
	}
}
