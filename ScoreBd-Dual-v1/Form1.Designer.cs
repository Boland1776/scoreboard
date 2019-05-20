namespace Scoreboardv6 {
	partial class ScoreForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScoreForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editTimesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.teamsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showStatsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayPowerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showUSBBuffersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.audioEnabledToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.audioChannel1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.readDeviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dualMatchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.countdownBeepToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preGameTimeBox = new System.Windows.Forms.TextBox();
            this.gameTimeBox = new System.Windows.Forms.TextBox();
            this.usbTimer = new System.Windows.Forms.Timer(this.components);
            this.gameClockTimer = new System.Windows.Forms.Timer(this.components);
            this.homeScoreBox = new System.Windows.Forms.TextBox();
            this.visitorScoreBox = new System.Windows.Forms.TextBox();
            this.startButton = new System.Windows.Forms.Button();
            this.pauseButton = new System.Windows.Forms.Button();
            this.hornButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.resetButton = new System.Windows.Forms.Button();
            this.newGameButton = new System.Windows.Forms.Button();
            this.clockSpeedBox = new System.Windows.Forms.TextBox();
            this.licenseBox = new System.Windows.Forms.TextBox();
            this.homeStatBox = new System.Windows.Forms.TextBox();
            this.visitorStatBox = new System.Windows.Forms.TextBox();
            this.homeTimeoutButton = new System.Windows.Forms.Button();
            this.visitorTimeoutButton = new System.Windows.Forms.Button();
            this.visitorScoreIncButton = new System.Windows.Forms.Button();
            this.visitorScoreDecButton = new System.Windows.Forms.Button();
            this.homeScoreDecButton = new System.Windows.Forms.Button();
            this.homeScoreIncButton = new System.Windows.Forms.Button();
            this.homeLocation = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.outBufferBox = new System.Windows.Forms.TextBox();
            this.messageBox = new System.Windows.Forms.TextBox();
            this.usbBox = new System.Windows.Forms.TextBox();
            this.set10Button = new System.Windows.Forms.Button();
            this.dec10Button = new System.Windows.Forms.Button();
            this.inputBufferBox = new System.Windows.Forms.TextBox();
            this.hornTimer = new System.Windows.Forms.Timer(this.components);
            this.homeHangButton = new System.Windows.Forms.Button();
            this.homeConcedeButton = new System.Windows.Forms.Button();
            this.visitorConcedeButton = new System.Windows.Forms.Button();
            this.visitorHangButton = new System.Windows.Forms.Button();
            this.homeTimeTakenBox = new System.Windows.Forms.CheckBox();
            this.visTimeTakenBox = new System.Windows.Forms.CheckBox();
            this.homeTeamComboBox = new System.Windows.Forms.ComboBox();
            this.visitorTeamComboBox = new System.Windows.Forms.ComboBox();
            this.homeLabel = new System.Windows.Forms.Label();
            this.visitorLabel = new System.Windows.Forms.Label();
            this.warningBox = new System.Windows.Forms.TextBox();
            this.homeTeam2ComboBox = new System.Windows.Forms.ComboBox();
            this.visitorTeam2ComboBox = new System.Windows.Forms.ComboBox();
            this.toggleTeamsCheckBox = new System.Windows.Forms.CheckBox();
            this.altGameHome = new System.Windows.Forms.TextBox();
            this.altGameVisitor = new System.Windows.Forms.TextBox();
            this.altGameTime = new System.Windows.Forms.TextBox();
            this.clrHomeTOButton = new System.Windows.Forms.Button();
            this.clrVisitorTOButton = new System.Windows.Forms.Button();
            this.addOneMinButton = new System.Windows.Forms.Button();
            this.decOneMinButtun = new System.Windows.Forms.Button();
            this.OTButton = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(892, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetToolStripMenuItem,
            this.exitToolStripMenuItem1});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // resetToolStripMenuItem
            // 
            this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            this.resetToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.resetToolStripMenuItem.Text = "Reset Contoller";
            this.resetToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(154, 22);
            this.exitToolStripMenuItem1.Text = "Exit";
            this.exitToolStripMenuItem1.Click += new System.EventHandler(this.exitToolStripMenuItem1_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editTimesToolStripMenuItem,
            this.teamsToolStripMenuItem,
            this.configurationToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // editTimesToolStripMenuItem
            // 
            this.editTimesToolStripMenuItem.Name = "editTimesToolStripMenuItem";
            this.editTimesToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.editTimesToolStripMenuItem.Text = "Edit Times";
            this.editTimesToolStripMenuItem.Click += new System.EventHandler(this.editTimesToolStripMenuItem_Click);
            // 
            // teamsToolStripMenuItem
            // 
            this.teamsToolStripMenuItem.Name = "teamsToolStripMenuItem";
            this.teamsToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.teamsToolStripMenuItem.Text = "Teams";
            this.teamsToolStripMenuItem.Click += new System.EventHandler(this.teamsToolStripMenuItem_Click);
            // 
            // configurationToolStripMenuItem
            // 
            this.configurationToolStripMenuItem.Name = "configurationToolStripMenuItem";
            this.configurationToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.configurationToolStripMenuItem.Text = "Configuration";
            this.configurationToolStripMenuItem.Click += new System.EventHandler(this.configurationToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.Checked = true;
            this.toolsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showStatsToolStripMenuItem,
            this.displayPowerToolStripMenuItem,
            this.showUSBBuffersToolStripMenuItem,
            this.audioEnabledToolStripMenuItem,
            this.audioChannel1ToolStripMenuItem,
            this.readDeviceToolStripMenuItem,
            this.dualMatchToolStripMenuItem,
            this.countdownBeepToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            this.toolsToolStripMenuItem.Click += new System.EventHandler(this.toolsToolStripMenuItem_Click);
            // 
            // showStatsToolStripMenuItem
            // 
            this.showStatsToolStripMenuItem.Name = "showStatsToolStripMenuItem";
            this.showStatsToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.showStatsToolStripMenuItem.Text = "Show Stats";
            this.showStatsToolStripMenuItem.Click += new System.EventHandler(this.showStatsToolStripMenuItem_Click);
            // 
            // displayPowerToolStripMenuItem
            // 
            this.displayPowerToolStripMenuItem.Name = "displayPowerToolStripMenuItem";
            this.displayPowerToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.displayPowerToolStripMenuItem.Text = "Display Power";
            this.displayPowerToolStripMenuItem.Click += new System.EventHandler(this.displayPowerToolStripMenuItem_Click);
            // 
            // showUSBBuffersToolStripMenuItem
            // 
            this.showUSBBuffersToolStripMenuItem.Name = "showUSBBuffersToolStripMenuItem";
            this.showUSBBuffersToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.showUSBBuffersToolStripMenuItem.Text = "Show USB buffers";
            this.showUSBBuffersToolStripMenuItem.Click += new System.EventHandler(this.showUSBBuffersToolStripMenuItem_Click);
            // 
            // audioEnabledToolStripMenuItem
            // 
            this.audioEnabledToolStripMenuItem.Checked = true;
            this.audioEnabledToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.audioEnabledToolStripMenuItem.Name = "audioEnabledToolStripMenuItem";
            this.audioEnabledToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.audioEnabledToolStripMenuItem.Text = "Audio Enabled";
            this.audioEnabledToolStripMenuItem.Click += new System.EventHandler(this.audioEnabledToolStripMenuItem_Click);
            // 
            // audioChannel1ToolStripMenuItem
            // 
            this.audioChannel1ToolStripMenuItem.Name = "audioChannel1ToolStripMenuItem";
            this.audioChannel1ToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.audioChannel1ToolStripMenuItem.Text = "Audio Channel 1";
            this.audioChannel1ToolStripMenuItem.Click += new System.EventHandler(this.audioChannel1ToolStripMenuItem_Click);
            // 
            // readDeviceToolStripMenuItem
            // 
            this.readDeviceToolStripMenuItem.Name = "readDeviceToolStripMenuItem";
            this.readDeviceToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.readDeviceToolStripMenuItem.Text = "Read Device";
            this.readDeviceToolStripMenuItem.Click += new System.EventHandler(this.readDeviceToolStripMenuItem_Click);
            // 
            // dualMatchToolStripMenuItem
            // 
            this.dualMatchToolStripMenuItem.Checked = true;
            this.dualMatchToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.dualMatchToolStripMenuItem.Name = "dualMatchToolStripMenuItem";
            this.dualMatchToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.dualMatchToolStripMenuItem.Text = "Dual Match";
            this.dualMatchToolStripMenuItem.Click += new System.EventHandler(this.dualMatchToolStripMenuItem_Click);
            // 
            // countdownBeepToolStripMenuItem
            // 
            this.countdownBeepToolStripMenuItem.Checked = true;
            this.countdownBeepToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.countdownBeepToolStripMenuItem.Name = "countdownBeepToolStripMenuItem";
            this.countdownBeepToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.countdownBeepToolStripMenuItem.Text = "Countdown Beep";
            this.countdownBeepToolStripMenuItem.Click += new System.EventHandler(this.countdownBeepToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // preGameTimeBox
            // 
            this.preGameTimeBox.AcceptsReturn = true;
            this.preGameTimeBox.AcceptsTab = true;
            this.preGameTimeBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.preGameTimeBox.BackColor = System.Drawing.Color.LightYellow;
            this.preGameTimeBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 93.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.preGameTimeBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.preGameTimeBox.Location = new System.Drawing.Point(321, 56);
            this.preGameTimeBox.Name = "preGameTimeBox";
            this.preGameTimeBox.ReadOnly = true;
            this.preGameTimeBox.Size = new System.Drawing.Size(280, 149);
            this.preGameTimeBox.TabIndex = 1;
            this.preGameTimeBox.Text = "0:00";
            this.preGameTimeBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // gameTimeBox
            // 
            this.gameTimeBox.AcceptsReturn = true;
            this.gameTimeBox.AcceptsTab = true;
            this.gameTimeBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.gameTimeBox.BackColor = System.Drawing.Color.LightYellow;
            this.gameTimeBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 99.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gameTimeBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.gameTimeBox.Location = new System.Drawing.Point(284, 214);
            this.gameTimeBox.Name = "gameTimeBox";
            this.gameTimeBox.ReadOnly = true;
            this.gameTimeBox.Size = new System.Drawing.Size(350, 158);
            this.gameTimeBox.TabIndex = 2;
            this.gameTimeBox.Text = "00:00";
            this.gameTimeBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // usbTimer
            // 
            this.usbTimer.Interval = 10;
            // 
            // gameClockTimer
            // 
            this.gameClockTimer.Interval = 1000;
            // 
            // homeScoreBox
            // 
            this.homeScoreBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.homeScoreBox.BackColor = System.Drawing.Color.LightYellow;
            this.homeScoreBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 96F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.homeScoreBox.Location = new System.Drawing.Point(83, 402);
            this.homeScoreBox.Name = "homeScoreBox";
            this.homeScoreBox.Size = new System.Drawing.Size(100, 152);
            this.homeScoreBox.TabIndex = 4;
            this.homeScoreBox.TabStop = false;
            this.homeScoreBox.Text = "0";
            this.homeScoreBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // visitorScoreBox
            // 
            this.visitorScoreBox.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.visitorScoreBox.BackColor = System.Drawing.Color.LightYellow;
            this.visitorScoreBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 96F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.visitorScoreBox.Location = new System.Drawing.Point(726, 402);
            this.visitorScoreBox.Name = "visitorScoreBox";
            this.visitorScoreBox.Size = new System.Drawing.Size(100, 152);
            this.visitorScoreBox.TabIndex = 5;
            this.visitorScoreBox.TabStop = false;
            this.visitorScoreBox.Text = "0";
            this.visitorScoreBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // startButton
            // 
            this.startButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.startButton.BackColor = System.Drawing.Color.GreenYellow;
            this.startButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startButton.Location = new System.Drawing.Point(410, 482);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(108, 43);
            this.startButton.TabIndex = 4;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = false;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // pauseButton
            // 
            this.pauseButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pauseButton.BackColor = System.Drawing.Color.LightBlue;
            this.pauseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pauseButton.Location = new System.Drawing.Point(543, 492);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(75, 33);
            this.pauseButton.TabIndex = 6;
            this.pauseButton.Text = "Pause";
            this.pauseButton.UseVisualStyleBackColor = false;
            this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
            // 
            // hornButton
            // 
            this.hornButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.hornButton.BackColor = System.Drawing.Color.LightBlue;
            this.hornButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hornButton.Location = new System.Drawing.Point(311, 492);
            this.hornButton.Name = "hornButton";
            this.hornButton.Size = new System.Drawing.Size(75, 33);
            this.hornButton.TabIndex = 5;
            this.hornButton.Text = "Horn";
            this.hornButton.UseVisualStyleBackColor = false;
            this.hornButton.Click += new System.EventHandler(this.hornButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.exitButton.Location = new System.Drawing.Point(424, 561);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(75, 23);
            this.exitButton.TabIndex = 9;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // resetButton
            // 
            this.resetButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.resetButton.Location = new System.Drawing.Point(335, 531);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(75, 23);
            this.resetButton.TabIndex = 7;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // newGameButton
            // 
            this.newGameButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.newGameButton.Location = new System.Drawing.Point(516, 531);
            this.newGameButton.Name = "newGameButton";
            this.newGameButton.Size = new System.Drawing.Size(75, 23);
            this.newGameButton.TabIndex = 8;
            this.newGameButton.Text = "Next Match";
            this.newGameButton.UseVisualStyleBackColor = true;
            this.newGameButton.Click += new System.EventHandler(this.newGameButton_Click);
            // 
            // clockSpeedBox
            // 
            this.clockSpeedBox.BackColor = System.Drawing.Color.LightSkyBlue;
            this.clockSpeedBox.Location = new System.Drawing.Point(301, 4);
            this.clockSpeedBox.Name = "clockSpeedBox";
            this.clockSpeedBox.ReadOnly = true;
            this.clockSpeedBox.Size = new System.Drawing.Size(109, 20);
            this.clockSpeedBox.TabIndex = 12;
            this.clockSpeedBox.TabStop = false;
            this.clockSpeedBox.Text = "Clock: 1000 mSec";
            this.clockSpeedBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // licenseBox
            // 
            this.licenseBox.BackColor = System.Drawing.Color.LightCoral;
            this.licenseBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.licenseBox.Location = new System.Drawing.Point(424, 4);
            this.licenseBox.Name = "licenseBox";
            this.licenseBox.ReadOnly = true;
            this.licenseBox.Size = new System.Drawing.Size(158, 20);
            this.licenseBox.TabIndex = 13;
            this.licenseBox.TabStop = false;
            this.licenseBox.Text = "No License";
            this.licenseBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // homeStatBox
            // 
            this.homeStatBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.homeStatBox.Location = new System.Drawing.Point(29, 59);
            this.homeStatBox.MaxLength = 128;
            this.homeStatBox.Multiline = true;
            this.homeStatBox.Name = "homeStatBox";
            this.homeStatBox.ReadOnly = true;
            this.homeStatBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.homeStatBox.Size = new System.Drawing.Size(229, 152);
            this.homeStatBox.TabIndex = 14;
            this.homeStatBox.TabStop = false;
            this.homeStatBox.Visible = false;
            // 
            // visitorStatBox
            // 
            this.visitorStatBox.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.visitorStatBox.BackColor = System.Drawing.SystemColors.Control;
            this.visitorStatBox.Location = new System.Drawing.Point(651, 59);
            this.visitorStatBox.MaxLength = 128;
            this.visitorStatBox.Multiline = true;
            this.visitorStatBox.Name = "visitorStatBox";
            this.visitorStatBox.ReadOnly = true;
            this.visitorStatBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.visitorStatBox.Size = new System.Drawing.Size(229, 152);
            this.visitorStatBox.TabIndex = 15;
            this.visitorStatBox.TabStop = false;
            this.visitorStatBox.Visible = false;
            // 
            // homeTimeoutButton
            // 
            this.homeTimeoutButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.homeTimeoutButton.Location = new System.Drawing.Point(100, 563);
            this.homeTimeoutButton.Name = "homeTimeoutButton";
            this.homeTimeoutButton.Size = new System.Drawing.Size(75, 23);
            this.homeTimeoutButton.TabIndex = 16;
            this.homeTimeoutButton.TabStop = false;
            this.homeTimeoutButton.Text = "Timeout";
            this.homeTimeoutButton.UseVisualStyleBackColor = true;
            this.homeTimeoutButton.Click += new System.EventHandler(this.homeTimeoutButton_Click);
            // 
            // visitorTimeoutButton
            // 
            this.visitorTimeoutButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.visitorTimeoutButton.Location = new System.Drawing.Point(737, 563);
            this.visitorTimeoutButton.Name = "visitorTimeoutButton";
            this.visitorTimeoutButton.Size = new System.Drawing.Size(75, 23);
            this.visitorTimeoutButton.TabIndex = 17;
            this.visitorTimeoutButton.TabStop = false;
            this.visitorTimeoutButton.Text = "Timeout";
            this.visitorTimeoutButton.UseVisualStyleBackColor = true;
            this.visitorTimeoutButton.Click += new System.EventHandler(this.visitorTimeoutButton_Click);
            // 
            // visitorScoreIncButton
            // 
            this.visitorScoreIncButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.visitorScoreIncButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.visitorScoreIncButton.Location = new System.Drawing.Point(832, 418);
            this.visitorScoreIncButton.Name = "visitorScoreIncButton";
            this.visitorScoreIncButton.Size = new System.Drawing.Size(40, 40);
            this.visitorScoreIncButton.TabIndex = 18;
            this.visitorScoreIncButton.TabStop = false;
            this.visitorScoreIncButton.Text = "+";
            this.visitorScoreIncButton.UseVisualStyleBackColor = true;
            this.visitorScoreIncButton.Click += new System.EventHandler(this.visitorScoreIncButton_Click);
            // 
            // visitorScoreDecButton
            // 
            this.visitorScoreDecButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.visitorScoreDecButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.visitorScoreDecButton.Location = new System.Drawing.Point(832, 508);
            this.visitorScoreDecButton.Name = "visitorScoreDecButton";
            this.visitorScoreDecButton.Size = new System.Drawing.Size(40, 40);
            this.visitorScoreDecButton.TabIndex = 19;
            this.visitorScoreDecButton.TabStop = false;
            this.visitorScoreDecButton.Text = "-";
            this.visitorScoreDecButton.UseVisualStyleBackColor = true;
            this.visitorScoreDecButton.Click += new System.EventHandler(this.visitorScoreDecButton_Click);
            // 
            // homeScoreDecButton
            // 
            this.homeScoreDecButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.homeScoreDecButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.homeScoreDecButton.Location = new System.Drawing.Point(37, 508);
            this.homeScoreDecButton.Name = "homeScoreDecButton";
            this.homeScoreDecButton.Size = new System.Drawing.Size(40, 40);
            this.homeScoreDecButton.TabIndex = 20;
            this.homeScoreDecButton.TabStop = false;
            this.homeScoreDecButton.Text = "-";
            this.homeScoreDecButton.UseVisualStyleBackColor = true;
            this.homeScoreDecButton.Click += new System.EventHandler(this.homeScoreDecButton_Click);
            // 
            // homeScoreIncButton
            // 
            this.homeScoreIncButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.homeScoreIncButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.homeScoreIncButton.Location = new System.Drawing.Point(37, 418);
            this.homeScoreIncButton.Name = "homeScoreIncButton";
            this.homeScoreIncButton.Size = new System.Drawing.Size(40, 40);
            this.homeScoreIncButton.TabIndex = 21;
            this.homeScoreIncButton.TabStop = false;
            this.homeScoreIncButton.Text = "+";
            this.homeScoreIncButton.UseVisualStyleBackColor = true;
            this.homeScoreIncButton.Click += new System.EventHandler(this.homeScoreIncButton_Click);
            // 
            // homeLocation
            // 
            this.homeLocation.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.homeLocation.Location = new System.Drawing.Point(29, 232);
            this.homeLocation.Name = "homeLocation";
            this.homeLocation.Size = new System.Drawing.Size(229, 20);
            this.homeLocation.TabIndex = 4;
            this.homeLocation.TabStop = false;
            this.homeLocation.Text = "<============";
            this.homeLocation.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.textBox1.Location = new System.Drawing.Point(651, 232);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(229, 20);
            this.textBox1.TabIndex = 5;
            this.textBox1.TabStop = false;
            this.textBox1.Text = "===========>";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // outBufferBox
            // 
            this.outBufferBox.Location = new System.Drawing.Point(29, 27);
            this.outBufferBox.Name = "outBufferBox";
            this.outBufferBox.ReadOnly = true;
            this.outBufferBox.Size = new System.Drawing.Size(408, 20);
            this.outBufferBox.TabIndex = 28;
            this.outBufferBox.TabStop = false;
            this.outBufferBox.Visible = false;
            // 
            // messageBox
            // 
            this.messageBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messageBox.Location = new System.Drawing.Point(12, 592);
            this.messageBox.Name = "messageBox";
            this.messageBox.ReadOnly = true;
            this.messageBox.Size = new System.Drawing.Size(476, 20);
            this.messageBox.TabIndex = 29;
            this.messageBox.TabStop = false;
            // 
            // usbBox
            // 
            this.usbBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.usbBox.BackColor = System.Drawing.Color.LightCoral;
            this.usbBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usbBox.Location = new System.Drawing.Point(599, 4);
            this.usbBox.Name = "usbBox";
            this.usbBox.Size = new System.Drawing.Size(279, 20);
            this.usbBox.TabIndex = 30;
            this.usbBox.TabStop = false;
            this.usbBox.Text = "No Controller Detected";
            this.usbBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // set10Button
            // 
            this.set10Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.set10Button.Location = new System.Drawing.Point(603, 100);
            this.set10Button.Name = "set10Button";
            this.set10Button.Size = new System.Drawing.Size(40, 23);
            this.set10Button.TabIndex = 31;
            this.set10Button.Text = "\"10\"";
            this.set10Button.UseVisualStyleBackColor = true;
            this.set10Button.Click += new System.EventHandler(this.set10Button_Click);
            // 
            // dec10Button
            // 
            this.dec10Button.Location = new System.Drawing.Point(603, 151);
            this.dec10Button.Name = "dec10Button";
            this.dec10Button.Size = new System.Drawing.Size(40, 23);
            this.dec10Button.TabIndex = 32;
            this.dec10Button.Text = "-10";
            this.dec10Button.UseVisualStyleBackColor = true;
            this.dec10Button.Click += new System.EventHandler(this.dec10Button_Click);
            // 
            // inputBufferBox
            // 
            this.inputBufferBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.inputBufferBox.Location = new System.Drawing.Point(472, 27);
            this.inputBufferBox.Name = "inputBufferBox";
            this.inputBufferBox.ReadOnly = true;
            this.inputBufferBox.Size = new System.Drawing.Size(408, 20);
            this.inputBufferBox.TabIndex = 33;
            this.inputBufferBox.TabStop = false;
            this.inputBufferBox.Visible = false;
            // 
            // hornTimer
            // 
            this.hornTimer.Interval = 150;
            // 
            // homeHangButton
            // 
            this.homeHangButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.homeHangButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.homeHangButton.Location = new System.Drawing.Point(189, 418);
            this.homeHangButton.Name = "homeHangButton";
            this.homeHangButton.Size = new System.Drawing.Size(69, 40);
            this.homeHangButton.TabIndex = 35;
            this.homeHangButton.Text = "Hang";
            this.homeHangButton.UseVisualStyleBackColor = true;
            this.homeHangButton.Click += new System.EventHandler(this.homeHangButton_Click);
            // 
            // homeConcedeButton
            // 
            this.homeConcedeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.homeConcedeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.homeConcedeButton.Location = new System.Drawing.Point(189, 508);
            this.homeConcedeButton.Name = "homeConcedeButton";
            this.homeConcedeButton.Size = new System.Drawing.Size(79, 40);
            this.homeConcedeButton.TabIndex = 36;
            this.homeConcedeButton.Text = "Concede";
            this.homeConcedeButton.UseVisualStyleBackColor = true;
            this.homeConcedeButton.Click += new System.EventHandler(this.homeConcedeButton_Click);
            // 
            // visitorConcedeButton
            // 
            this.visitorConcedeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.visitorConcedeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.visitorConcedeButton.Location = new System.Drawing.Point(634, 508);
            this.visitorConcedeButton.Name = "visitorConcedeButton";
            this.visitorConcedeButton.Size = new System.Drawing.Size(79, 40);
            this.visitorConcedeButton.TabIndex = 37;
            this.visitorConcedeButton.Text = "Concede";
            this.visitorConcedeButton.UseVisualStyleBackColor = true;
            this.visitorConcedeButton.Click += new System.EventHandler(this.visitorConcedeButton_Click);
            // 
            // visitorHangButton
            // 
            this.visitorHangButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.visitorHangButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.visitorHangButton.Location = new System.Drawing.Point(644, 418);
            this.visitorHangButton.Name = "visitorHangButton";
            this.visitorHangButton.Size = new System.Drawing.Size(69, 40);
            this.visitorHangButton.TabIndex = 38;
            this.visitorHangButton.Text = "Hang";
            this.visitorHangButton.UseVisualStyleBackColor = true;
            this.visitorHangButton.Click += new System.EventHandler(this.visitorHangButton_Click);
            // 
            // homeTimeTakenBox
            // 
            this.homeTimeTakenBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.homeTimeTakenBox.AutoSize = true;
            this.homeTimeTakenBox.Location = new System.Drawing.Point(196, 567);
            this.homeTimeTakenBox.Name = "homeTimeTakenBox";
            this.homeTimeTakenBox.Size = new System.Drawing.Size(94, 17);
            this.homeTimeTakenBox.TabIndex = 39;
            this.homeTimeTakenBox.Text = "Timeout taken";
            this.homeTimeTakenBox.UseVisualStyleBackColor = true;
            this.homeTimeTakenBox.Visible = false;
            this.homeTimeTakenBox.CheckedChanged += new System.EventHandler(this.homeTimeTakenBox_CheckedChanged);
            // 
            // visTimeTakenBox
            // 
            this.visTimeTakenBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.visTimeTakenBox.AutoSize = true;
            this.visTimeTakenBox.Location = new System.Drawing.Point(631, 567);
            this.visTimeTakenBox.Name = "visTimeTakenBox";
            this.visTimeTakenBox.Size = new System.Drawing.Size(98, 17);
            this.visTimeTakenBox.TabIndex = 40;
            this.visTimeTakenBox.Text = "Timeout Taken";
            this.visTimeTakenBox.UseVisualStyleBackColor = true;
            this.visTimeTakenBox.Visible = false;
            // 
            // homeTeamComboBox
            // 
            this.homeTeamComboBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.homeTeamComboBox.FormattingEnabled = true;
            this.homeTeamComboBox.Location = new System.Drawing.Point(29, 274);
            this.homeTeamComboBox.Name = "homeTeamComboBox";
            this.homeTeamComboBox.Size = new System.Drawing.Size(229, 21);
            this.homeTeamComboBox.Sorted = true;
            this.homeTeamComboBox.TabIndex = 41;
            this.homeTeamComboBox.SelectedIndexChanged += new System.EventHandler(this.homeTeamComboBox_SelectedIndexChanged);
            this.homeTeamComboBox.TextUpdate += new System.EventHandler(this.homeTeamComboBox_TextChanged);
            // 
            // visitorTeamComboBox
            // 
            this.visitorTeamComboBox.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.visitorTeamComboBox.FormattingEnabled = true;
            this.visitorTeamComboBox.Location = new System.Drawing.Point(651, 274);
            this.visitorTeamComboBox.Name = "visitorTeamComboBox";
            this.visitorTeamComboBox.Size = new System.Drawing.Size(229, 21);
            this.visitorTeamComboBox.Sorted = true;
            this.visitorTeamComboBox.TabIndex = 42;
            this.visitorTeamComboBox.SelectedIndexChanged += new System.EventHandler(this.visitorTeamComboBox_SelectedIndexChanged);
            this.visitorTeamComboBox.TextUpdate += new System.EventHandler(this.visitorTeamComboBox_TextChanged);
            // 
            // homeLabel
            // 
            this.homeLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.homeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.homeLabel.Location = new System.Drawing.Point(24, 374);
            this.homeLabel.Name = "homeLabel";
            this.homeLabel.Size = new System.Drawing.Size(257, 25);
            this.homeLabel.TabIndex = 43;
            this.homeLabel.Text = "Home";
            this.homeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.homeLabel.Click += new System.EventHandler(this.homeLabel_Click);
            // 
            // visitorLabel
            // 
            this.visitorLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.visitorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.visitorLabel.Location = new System.Drawing.Point(635, 374);
            this.visitorLabel.Name = "visitorLabel";
            this.visitorLabel.Size = new System.Drawing.Size(257, 25);
            this.visitorLabel.TabIndex = 44;
            this.visitorLabel.Text = "Visitor";
            this.visitorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // warningBox
            // 
            this.warningBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.warningBox.BackColor = System.Drawing.SystemColors.Control;
            this.warningBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.warningBox.Location = new System.Drawing.Point(534, 592);
            this.warningBox.Name = "warningBox";
            this.warningBox.Size = new System.Drawing.Size(346, 20);
            this.warningBox.TabIndex = 45;
            this.warningBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // homeTeam2ComboBox
            // 
            this.homeTeam2ComboBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.homeTeam2ComboBox.FormattingEnabled = true;
            this.homeTeam2ComboBox.Location = new System.Drawing.Point(29, 301);
            this.homeTeam2ComboBox.Name = "homeTeam2ComboBox";
            this.homeTeam2ComboBox.Size = new System.Drawing.Size(229, 21);
            this.homeTeam2ComboBox.Sorted = true;
            this.homeTeam2ComboBox.TabIndex = 46;
            this.homeTeam2ComboBox.SelectedIndexChanged += new System.EventHandler(this.homeTeam2ComboBox_SelectedIndexChanged);
            this.homeTeam2ComboBox.TextUpdate += new System.EventHandler(this.homeTeam2ComboBox_TextChanged);
            // 
            // visitorTeam2ComboBox
            // 
            this.visitorTeam2ComboBox.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.visitorTeam2ComboBox.FormattingEnabled = true;
            this.visitorTeam2ComboBox.Location = new System.Drawing.Point(651, 301);
            this.visitorTeam2ComboBox.Name = "visitorTeam2ComboBox";
            this.visitorTeam2ComboBox.Size = new System.Drawing.Size(229, 21);
            this.visitorTeam2ComboBox.Sorted = true;
            this.visitorTeam2ComboBox.TabIndex = 47;
            this.visitorTeam2ComboBox.SelectedIndexChanged += new System.EventHandler(this.visitorTeam2ComboBox_SelectedIndexChanged);
            this.visitorTeam2ComboBox.TextUpdate += new System.EventHandler(this.visitorTeam2ComboBox_TextChanged);
            // 
            // toggleTeamsCheckBox
            // 
            this.toggleTeamsCheckBox.AutoSize = true;
            this.toggleTeamsCheckBox.Location = new System.Drawing.Point(410, 378);
            this.toggleTeamsCheckBox.Name = "toggleTeamsCheckBox";
            this.toggleTeamsCheckBox.Size = new System.Drawing.Size(94, 17);
            this.toggleTeamsCheckBox.TabIndex = 48;
            this.toggleTeamsCheckBox.Text = "Toggle Teams";
            this.toggleTeamsCheckBox.UseVisualStyleBackColor = true;
            this.toggleTeamsCheckBox.CheckedChanged += new System.EventHandler(this.toggleTeamsCheckBox_CheckedChanged);
            // 
            // altGameHome
            // 
            this.altGameHome.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.altGameHome.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.altGameHome.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.altGameHome.Location = new System.Drawing.Point(335, 404);
            this.altGameHome.Name = "altGameHome";
            this.altGameHome.Size = new System.Drawing.Size(266, 20);
            this.altGameHome.TabIndex = 49;
            this.altGameHome.TabStop = false;
            this.altGameHome.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // altGameVisitor
            // 
            this.altGameVisitor.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.altGameVisitor.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.altGameVisitor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.altGameVisitor.Location = new System.Drawing.Point(335, 430);
            this.altGameVisitor.Name = "altGameVisitor";
            this.altGameVisitor.Size = new System.Drawing.Size(266, 20);
            this.altGameVisitor.TabIndex = 50;
            this.altGameVisitor.TabStop = false;
            this.altGameVisitor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // altGameTime
            // 
            this.altGameTime.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.altGameTime.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.altGameTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.altGameTime.Location = new System.Drawing.Point(386, 456);
            this.altGameTime.Name = "altGameTime";
            this.altGameTime.Size = new System.Drawing.Size(178, 20);
            this.altGameTime.TabIndex = 51;
            this.altGameTime.TabStop = false;
            this.altGameTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // clrHomeTOButton
            // 
            this.clrHomeTOButton.Location = new System.Drawing.Point(37, 563);
            this.clrHomeTOButton.Name = "clrHomeTOButton";
            this.clrHomeTOButton.Size = new System.Drawing.Size(40, 23);
            this.clrHomeTOButton.TabIndex = 52;
            this.clrHomeTOButton.Text = "Tog";
            this.clrHomeTOButton.UseVisualStyleBackColor = true;
            this.clrHomeTOButton.Click += new System.EventHandler(this.clrHomeTOButton_Click);
            // 
            // clrVisitorTOButton
            // 
            this.clrVisitorTOButton.Location = new System.Drawing.Point(832, 563);
            this.clrVisitorTOButton.Name = "clrVisitorTOButton";
            this.clrVisitorTOButton.Size = new System.Drawing.Size(40, 23);
            this.clrVisitorTOButton.TabIndex = 53;
            this.clrVisitorTOButton.Text = "Tog";
            this.clrVisitorTOButton.UseVisualStyleBackColor = true;
            this.clrVisitorTOButton.Click += new System.EventHandler(this.clrVisitorTOButton_Click);
            // 
            // addOneMinButton
            // 
            this.addOneMinButton.Location = new System.Drawing.Point(275, 100);
            this.addOneMinButton.Name = "addOneMinButton";
            this.addOneMinButton.Size = new System.Drawing.Size(42, 23);
            this.addOneMinButton.TabIndex = 54;
            this.addOneMinButton.Text = "+1:00";
            this.addOneMinButton.UseVisualStyleBackColor = true;
            this.addOneMinButton.Click += new System.EventHandler(this.addOneMinButton_Click);
            // 
            // decOneMinButtun
            // 
            this.decOneMinButtun.Location = new System.Drawing.Point(275, 151);
            this.decOneMinButtun.Name = "decOneMinButtun";
            this.decOneMinButtun.Size = new System.Drawing.Size(42, 23);
            this.decOneMinButtun.TabIndex = 55;
            this.decOneMinButtun.Text = "-1:00";
            this.decOneMinButtun.UseVisualStyleBackColor = true;
            this.decOneMinButtun.Click += new System.EventHandler(this.decOneMinButtun_Click);
            // 
            // OTButton
            // 
            this.OTButton.Location = new System.Drawing.Point(424, 531);
            this.OTButton.Name = "OTButton";
            this.OTButton.Size = new System.Drawing.Size(75, 23);
            this.OTButton.TabIndex = 56;
            this.OTButton.Text = "Overtime";
            this.OTButton.UseVisualStyleBackColor = true;
            this.OTButton.Click += new System.EventHandler(this.OTButton_Click);
            // 
            // ScoreForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 618);
            this.Controls.Add(this.OTButton);
            this.Controls.Add(this.decOneMinButtun);
            this.Controls.Add(this.addOneMinButton);
            this.Controls.Add(this.clrVisitorTOButton);
            this.Controls.Add(this.clrHomeTOButton);
            this.Controls.Add(this.altGameTime);
            this.Controls.Add(this.altGameVisitor);
            this.Controls.Add(this.altGameHome);
            this.Controls.Add(this.toggleTeamsCheckBox);
            this.Controls.Add(this.visitorTeam2ComboBox);
            this.Controls.Add(this.homeTeam2ComboBox);
            this.Controls.Add(this.warningBox);
            this.Controls.Add(this.visitorLabel);
            this.Controls.Add(this.homeLabel);
            this.Controls.Add(this.visitorTeamComboBox);
            this.Controls.Add(this.homeTeamComboBox);
            this.Controls.Add(this.visTimeTakenBox);
            this.Controls.Add(this.homeTimeTakenBox);
            this.Controls.Add(this.visitorHangButton);
            this.Controls.Add(this.visitorConcedeButton);
            this.Controls.Add(this.homeConcedeButton);
            this.Controls.Add(this.homeHangButton);
            this.Controls.Add(this.inputBufferBox);
            this.Controls.Add(this.dec10Button);
            this.Controls.Add(this.set10Button);
            this.Controls.Add(this.usbBox);
            this.Controls.Add(this.messageBox);
            this.Controls.Add(this.outBufferBox);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.homeLocation);
            this.Controls.Add(this.homeScoreIncButton);
            this.Controls.Add(this.homeScoreDecButton);
            this.Controls.Add(this.visitorScoreDecButton);
            this.Controls.Add(this.visitorScoreIncButton);
            this.Controls.Add(this.visitorTimeoutButton);
            this.Controls.Add(this.homeTimeoutButton);
            this.Controls.Add(this.visitorStatBox);
            this.Controls.Add(this.homeStatBox);
            this.Controls.Add(this.licenseBox);
            this.Controls.Add(this.clockSpeedBox);
            this.Controls.Add(this.newGameButton);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.hornButton);
            this.Controls.Add(this.pauseButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.visitorScoreBox);
            this.Controls.Add(this.homeScoreBox);
            this.Controls.Add(this.gameTimeBox);
            this.Controls.Add(this.preGameTimeBox);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ScoreForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Scoreboard Dual - Rev 1.2.6 (052019)";
            this.Load += new System.EventHandler(this.ScoreForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ScoreForm_KeyDown);
//            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.keyDownEvent);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.TextBox preGameTimeBox;
		private System.Windows.Forms.TextBox gameTimeBox;
		private System.Windows.Forms.Timer usbTimer;
		private System.Windows.Forms.Timer gameClockTimer;
		private System.Windows.Forms.TextBox homeScoreBox;
		private System.Windows.Forms.TextBox visitorScoreBox;
		private System.Windows.Forms.Button startButton;
		private System.Windows.Forms.Button pauseButton;
		private System.Windows.Forms.Button hornButton;
		private System.Windows.Forms.Button exitButton;
		private System.Windows.Forms.Button resetButton;
		private System.Windows.Forms.Button newGameButton;
		private System.Windows.Forms.TextBox clockSpeedBox;
		private System.Windows.Forms.TextBox licenseBox;
		private System.Windows.Forms.TextBox homeStatBox;
		private System.Windows.Forms.TextBox visitorStatBox;
		private System.Windows.Forms.ToolStripMenuItem showStatsToolStripMenuItem;
		private System.Windows.Forms.Button homeTimeoutButton;
		private System.Windows.Forms.Button visitorTimeoutButton;
		private System.Windows.Forms.Button visitorScoreIncButton;
		private System.Windows.Forms.Button visitorScoreDecButton;
		private System.Windows.Forms.Button homeScoreDecButton;
		private System.Windows.Forms.Button homeScoreIncButton;
		private System.Windows.Forms.TextBox homeLocation;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox outBufferBox;
		private System.Windows.Forms.TextBox messageBox;
		private System.Windows.Forms.TextBox usbBox;
		private System.Windows.Forms.ToolStripMenuItem editTimesToolStripMenuItem;
		private System.Windows.Forms.Button set10Button;
		private System.Windows.Forms.Button dec10Button;
		private System.Windows.Forms.ToolStripMenuItem displayPowerToolStripMenuItem;
		private System.Windows.Forms.TextBox inputBufferBox;
		private System.Windows.Forms.ToolStripMenuItem showUSBBuffersToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
		private System.Windows.Forms.Timer hornTimer;
		private System.Windows.Forms.Button homeHangButton;
		private System.Windows.Forms.Button homeConcedeButton;
		private System.Windows.Forms.Button visitorConcedeButton;
		private System.Windows.Forms.Button visitorHangButton;
		private System.Windows.Forms.ToolStripMenuItem audioEnabledToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem audioChannel1ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem teamsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem configurationToolStripMenuItem;
		private System.Windows.Forms.CheckBox homeTimeTakenBox;
		private System.Windows.Forms.CheckBox visTimeTakenBox;
		private System.Windows.Forms.ComboBox homeTeamComboBox;
		private System.Windows.Forms.ComboBox visitorTeamComboBox;
		private System.Windows.Forms.Label homeLabel;
		private System.Windows.Forms.Label visitorLabel;
		private System.Windows.Forms.ToolStripMenuItem readDeviceToolStripMenuItem;
		private System.Windows.Forms.TextBox warningBox;
        private System.Windows.Forms.ComboBox homeTeam2ComboBox;
        private System.Windows.Forms.ComboBox visitorTeam2ComboBox;
        private System.Windows.Forms.CheckBox toggleTeamsCheckBox;
        private System.Windows.Forms.TextBox altGameHome;
        private System.Windows.Forms.TextBox altGameVisitor;
        private System.Windows.Forms.TextBox altGameTime;
        private System.Windows.Forms.ToolStripMenuItem dualMatchToolStripMenuItem;
        private System.Windows.Forms.Button clrHomeTOButton;
        private System.Windows.Forms.Button clrVisitorTOButton;
        private System.Windows.Forms.Button addOneMinButton;
        private System.Windows.Forms.Button decOneMinButtun;
        private System.Windows.Forms.ToolStripMenuItem countdownBeepToolStripMenuItem;
        private System.Windows.Forms.Button OTButton;
    }
}

