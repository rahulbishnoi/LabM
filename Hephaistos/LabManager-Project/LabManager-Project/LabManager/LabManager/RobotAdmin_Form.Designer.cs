namespace LabManager
{
    partial class RobotAdmin_Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
      //  private System.Timers.Timer RobotRCTimer = new System.Timers.Timer();
        private System.Windows.Forms.Timer RobotRCTimer = new System.Windows.Forms.Timer();
     
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (RobotRCTimer!=null)
            {
                RobotRCTimer.Enabled = false;
                RobotRCTimer.Stop();
                
            }
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RobotAdmin_Form));
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.miFile = new System.Windows.Forms.MenuItem();
            this.miExit = new System.Windows.Forms.MenuItem();
            this.c1StatusBar_LabManager = new C1.Win.C1Ribbon.C1StatusBar();
            this.ribbonLabel_Connect = new C1.Win.C1Ribbon.RibbonLabel();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.c1DockingTab1 = new C1.Win.C1Command.C1DockingTab();
            this.c1DockingTabPage1 = new C1.Win.C1Command.C1DockingTabPage();
            this.groupBox_MOTOR = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label_bReady = new System.Windows.Forms.Label();
            this.pictureBox_bReady = new System.Windows.Forms.PictureBox();
            this.label_bRunning = new System.Windows.Forms.Label();
            this.pictureBox_bRunning = new System.Windows.Forms.PictureBox();
            this.label_bPaused = new System.Windows.Forms.Label();
            this.pictureBox_bPaused = new System.Windows.Forms.PictureBox();
            this.label_bError = new System.Windows.Forms.Label();
            this.pictureBox_bError = new System.Windows.Forms.PictureBox();
            this.label_bEStop = new System.Windows.Forms.Label();
            this.pictureBox_bEStop = new System.Windows.Forms.PictureBox();
            this.label_bSafeguard = new System.Windows.Forms.Label();
            this.pictureBox_bSafeguard = new System.Windows.Forms.PictureBox();
            this.label_bSError = new System.Windows.Forms.Label();
            this.pictureBox_bSError = new System.Windows.Forms.PictureBox();
            this.label_bWarning = new System.Windows.Forms.Label();
            this.pictureBox_bWarning = new System.Windows.Forms.PictureBox();
            this.label_bAuto = new System.Windows.Forms.Label();
            this.pictureBox_bAuto = new System.Windows.Forms.PictureBox();
            this.label_bTeach = new System.Windows.Forms.Label();
            this.pictureBox_bTeach = new System.Windows.Forms.PictureBox();
            this.pictureBox_Gripper = new System.Windows.Forms.PictureBox();
            this.label_Gripper = new System.Windows.Forms.Label();
            this.button_GRIPPERCLOSE = new System.Windows.Forms.Button();
            this.button_GRIPPEROPEN = new System.Windows.Forms.Button();
            this.button_Motors_OFF = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button_Motors_ON = new System.Windows.Forms.Button();
            this.c1DockingTabPage2 = new C1.Win.C1Command.C1DockingTabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button_Ignore = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_TryAgain = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label_Info = new System.Windows.Forms.Label();
            this.button_START = new System.Windows.Forms.Button();
            this.button_STOP = new System.Windows.Forms.Button();
            this.button_PAUSE = new System.Windows.Forms.Button();
            this.button_CONTINUE = new System.Windows.Forms.Button();
            this.button_RESET = new System.Windows.Forms.Button();
            this.button_HOME = new System.Windows.Forms.Button();
            this.button_LOGIN = new System.Windows.Forms.Button();
            this.button_LOGOUT = new System.Windows.Forms.Button();
            this.imageList_Gripper = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar_LabManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1DockingTab1)).BeginInit();
            this.c1DockingTab1.SuspendLayout();
            this.c1DockingTabPage1.SuspendLayout();
            this.groupBox_MOTOR.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_bReady)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_bRunning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_bPaused)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_bError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_bEStop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_bSafeguard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_bSError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_bWarning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_bAuto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_bTeach)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Gripper)).BeginInit();
            this.c1DockingTabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miFile});
            // 
            // miFile
            // 
            this.miFile.Index = 0;
            this.miFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miExit});
            this.miFile.Text = "&File";
            // 
            // miExit
            // 
            this.miExit.Index = 0;
            this.miExit.Text = "Close";
            this.miExit.Click += new System.EventHandler(this.miExit_Click);
            // 
            // c1StatusBar_LabManager
            // 
            this.c1StatusBar_LabManager.Location = new System.Drawing.Point(0, 483);
            this.c1StatusBar_LabManager.Name = "c1StatusBar_LabManager";
            this.c1StatusBar_LabManager.RightPaneItems.Add(this.ribbonLabel_Connect);
            this.c1StatusBar_LabManager.Size = new System.Drawing.Size(587, 22);
            // 
            // ribbonLabel_Connect
            // 
            this.ribbonLabel_Connect.Name = "ribbonLabel_Connect";
            this.ribbonLabel_Connect.Text = "Robot Online";
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Red;
            this.imgList.Images.SetKeyName(0, "GreenBlock");
            this.imgList.Images.SetKeyName(1, "OrangeBlock");
            this.imgList.Images.SetKeyName(2, "RedBlock");
            this.imgList.Images.SetKeyName(3, "greenDot");
            this.imgList.Images.SetKeyName(4, "grayDot");
            // 
            // c1DockingTab1
            // 
            this.c1DockingTab1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c1DockingTab1.Controls.Add(this.c1DockingTabPage1);
            this.c1DockingTab1.Controls.Add(this.c1DockingTabPage2);
            this.c1DockingTab1.Location = new System.Drawing.Point(12, 12);
            this.c1DockingTab1.Name = "c1DockingTab1";
            this.c1DockingTab1.Size = new System.Drawing.Size(563, 372);
            this.c1DockingTab1.TabIndex = 2;
            this.c1DockingTab1.TabStyle = C1.Win.C1Command.TabStyleEnum.Office2007;
            this.c1DockingTab1.VisualStyle = C1.Win.C1Command.VisualStyle.Office2007Black;
            this.c1DockingTab1.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2007Black;
            // 
            // c1DockingTabPage1
            // 
            this.c1DockingTabPage1.Controls.Add(this.groupBox_MOTOR);
            this.c1DockingTabPage1.Location = new System.Drawing.Point(1, 24);
            this.c1DockingTabPage1.Name = "c1DockingTabPage1";
            this.c1DockingTabPage1.Size = new System.Drawing.Size(561, 347);
            this.c1DockingTabPage1.TabIndex = 0;
            this.c1DockingTabPage1.Text = "Robot Manager";
            // 
            // groupBox_MOTOR
            // 
            this.groupBox_MOTOR.BackColor = System.Drawing.Color.Transparent;
            this.groupBox_MOTOR.Controls.Add(this.groupBox1);
            this.groupBox_MOTOR.Controls.Add(this.pictureBox_Gripper);
            this.groupBox_MOTOR.Controls.Add(this.label_Gripper);
            this.groupBox_MOTOR.Controls.Add(this.button_GRIPPERCLOSE);
            this.groupBox_MOTOR.Controls.Add(this.button_GRIPPEROPEN);
            this.groupBox_MOTOR.Controls.Add(this.button_Motors_OFF);
            this.groupBox_MOTOR.Controls.Add(this.label1);
            this.groupBox_MOTOR.Controls.Add(this.button_Motors_ON);
            this.groupBox_MOTOR.Location = new System.Drawing.Point(15, 13);
            this.groupBox_MOTOR.Name = "groupBox_MOTOR";
            this.groupBox_MOTOR.Size = new System.Drawing.Size(531, 321);
            this.groupBox_MOTOR.TabIndex = 15;
            this.groupBox_MOTOR.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label_bReady);
            this.groupBox1.Controls.Add(this.pictureBox_bReady);
            this.groupBox1.Controls.Add(this.label_bRunning);
            this.groupBox1.Controls.Add(this.pictureBox_bRunning);
            this.groupBox1.Controls.Add(this.label_bPaused);
            this.groupBox1.Controls.Add(this.pictureBox_bPaused);
            this.groupBox1.Controls.Add(this.label_bError);
            this.groupBox1.Controls.Add(this.pictureBox_bError);
            this.groupBox1.Controls.Add(this.label_bEStop);
            this.groupBox1.Controls.Add(this.pictureBox_bEStop);
            this.groupBox1.Controls.Add(this.label_bSafeguard);
            this.groupBox1.Controls.Add(this.pictureBox_bSafeguard);
            this.groupBox1.Controls.Add(this.label_bSError);
            this.groupBox1.Controls.Add(this.pictureBox_bSError);
            this.groupBox1.Controls.Add(this.label_bWarning);
            this.groupBox1.Controls.Add(this.pictureBox_bWarning);
            this.groupBox1.Controls.Add(this.label_bAuto);
            this.groupBox1.Controls.Add(this.pictureBox_bAuto);
            this.groupBox1.Controls.Add(this.label_bTeach);
            this.groupBox1.Controls.Add(this.pictureBox_bTeach);
            this.groupBox1.Location = new System.Drawing.Point(317, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(188, 287);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Status";
            // 
            // label_bReady
            // 
            this.label_bReady.AutoSize = true;
            this.label_bReady.Location = new System.Drawing.Point(48, 252);
            this.label_bReady.Name = "label_bReady";
            this.label_bReady.Size = new System.Drawing.Size(38, 13);
            this.label_bReady.TabIndex = 35;
            this.label_bReady.Text = "Ready";
            // 
            // pictureBox_bReady
            // 
            this.pictureBox_bReady.Location = new System.Drawing.Point(26, 250);
            this.pictureBox_bReady.Name = "pictureBox_bReady";
            this.pictureBox_bReady.Size = new System.Drawing.Size(16, 16);
            this.pictureBox_bReady.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox_bReady.TabIndex = 34;
            this.pictureBox_bReady.TabStop = false;
            // 
            // label_bRunning
            // 
            this.label_bRunning.AutoSize = true;
            this.label_bRunning.Location = new System.Drawing.Point(48, 227);
            this.label_bRunning.Name = "label_bRunning";
            this.label_bRunning.Size = new System.Drawing.Size(42, 13);
            this.label_bRunning.TabIndex = 33;
            this.label_bRunning.Text = "running";
            // 
            // pictureBox_bRunning
            // 
            this.pictureBox_bRunning.Location = new System.Drawing.Point(26, 225);
            this.pictureBox_bRunning.Name = "pictureBox_bRunning";
            this.pictureBox_bRunning.Size = new System.Drawing.Size(16, 16);
            this.pictureBox_bRunning.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox_bRunning.TabIndex = 32;
            this.pictureBox_bRunning.TabStop = false;
            // 
            // label_bPaused
            // 
            this.label_bPaused.AutoSize = true;
            this.label_bPaused.Location = new System.Drawing.Point(48, 202);
            this.label_bPaused.Name = "label_bPaused";
            this.label_bPaused.Size = new System.Drawing.Size(72, 13);
            this.label_bPaused.TabIndex = 31;
            this.label_bPaused.Text = "Paused mode";
            // 
            // pictureBox_bPaused
            // 
            this.pictureBox_bPaused.Location = new System.Drawing.Point(26, 200);
            this.pictureBox_bPaused.Name = "pictureBox_bPaused";
            this.pictureBox_bPaused.Size = new System.Drawing.Size(16, 16);
            this.pictureBox_bPaused.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox_bPaused.TabIndex = 30;
            this.pictureBox_bPaused.TabStop = false;
            // 
            // label_bError
            // 
            this.label_bError.AutoSize = true;
            this.label_bError.Location = new System.Drawing.Point(48, 177);
            this.label_bError.Name = "label_bError";
            this.label_bError.Size = new System.Drawing.Size(29, 13);
            this.label_bError.TabIndex = 29;
            this.label_bError.Text = "Error";
            // 
            // pictureBox_bError
            // 
            this.pictureBox_bError.Location = new System.Drawing.Point(26, 175);
            this.pictureBox_bError.Name = "pictureBox_bError";
            this.pictureBox_bError.Size = new System.Drawing.Size(16, 16);
            this.pictureBox_bError.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox_bError.TabIndex = 28;
            this.pictureBox_bError.TabStop = false;
            // 
            // label_bEStop
            // 
            this.label_bEStop.AutoSize = true;
            this.label_bEStop.Location = new System.Drawing.Point(48, 152);
            this.label_bEStop.Name = "label_bEStop";
            this.label_bEStop.Size = new System.Drawing.Size(85, 13);
            this.label_bEStop.TabIndex = 27;
            this.label_bEStop.Text = "Emergency Stop";
            // 
            // pictureBox_bEStop
            // 
            this.pictureBox_bEStop.Location = new System.Drawing.Point(26, 150);
            this.pictureBox_bEStop.Name = "pictureBox_bEStop";
            this.pictureBox_bEStop.Size = new System.Drawing.Size(16, 16);
            this.pictureBox_bEStop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox_bEStop.TabIndex = 26;
            this.pictureBox_bEStop.TabStop = false;
            // 
            // label_bSafeguard
            // 
            this.label_bSafeguard.AutoSize = true;
            this.label_bSafeguard.Location = new System.Drawing.Point(48, 127);
            this.label_bSafeguard.Name = "label_bSafeguard";
            this.label_bSafeguard.Size = new System.Drawing.Size(59, 13);
            this.label_bSafeguard.TabIndex = 25;
            this.label_bSafeguard.Text = "Safeguard ";
            // 
            // pictureBox_bSafeguard
            // 
            this.pictureBox_bSafeguard.Location = new System.Drawing.Point(26, 125);
            this.pictureBox_bSafeguard.Name = "pictureBox_bSafeguard";
            this.pictureBox_bSafeguard.Size = new System.Drawing.Size(16, 16);
            this.pictureBox_bSafeguard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox_bSafeguard.TabIndex = 24;
            this.pictureBox_bSafeguard.TabStop = false;
            // 
            // label_bSError
            // 
            this.label_bSError.AutoSize = true;
            this.label_bSError.Location = new System.Drawing.Point(48, 102);
            this.label_bSError.Name = "label_bSError";
            this.label_bSError.Size = new System.Drawing.Size(78, 13);
            this.label_bSError.TabIndex = 23;
            this.label_bSError.Text = "signifigant error";
            // 
            // pictureBox_bSError
            // 
            this.pictureBox_bSError.Location = new System.Drawing.Point(26, 100);
            this.pictureBox_bSError.Name = "pictureBox_bSError";
            this.pictureBox_bSError.Size = new System.Drawing.Size(16, 16);
            this.pictureBox_bSError.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox_bSError.TabIndex = 22;
            this.pictureBox_bSError.TabStop = false;
            // 
            // label_bWarning
            // 
            this.label_bWarning.AutoSize = true;
            this.label_bWarning.Location = new System.Drawing.Point(48, 77);
            this.label_bWarning.Name = "label_bWarning";
            this.label_bWarning.Size = new System.Drawing.Size(47, 13);
            this.label_bWarning.TabIndex = 21;
            this.label_bWarning.Text = "Warning";
            // 
            // pictureBox_bWarning
            // 
            this.pictureBox_bWarning.Location = new System.Drawing.Point(26, 75);
            this.pictureBox_bWarning.Name = "pictureBox_bWarning";
            this.pictureBox_bWarning.Size = new System.Drawing.Size(16, 16);
            this.pictureBox_bWarning.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox_bWarning.TabIndex = 20;
            this.pictureBox_bWarning.TabStop = false;
            // 
            // label_bAuto
            // 
            this.label_bAuto.AutoSize = true;
            this.label_bAuto.Location = new System.Drawing.Point(48, 52);
            this.label_bAuto.Name = "label_bAuto";
            this.label_bAuto.Size = new System.Drawing.Size(58, 13);
            this.label_bAuto.TabIndex = 19;
            this.label_bAuto.Text = "Auto mode";
            // 
            // pictureBox_bAuto
            // 
            this.pictureBox_bAuto.Location = new System.Drawing.Point(26, 50);
            this.pictureBox_bAuto.Name = "pictureBox_bAuto";
            this.pictureBox_bAuto.Size = new System.Drawing.Size(16, 16);
            this.pictureBox_bAuto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox_bAuto.TabIndex = 18;
            this.pictureBox_bAuto.TabStop = false;
            // 
            // label_bTeach
            // 
            this.label_bTeach.AutoSize = true;
            this.label_bTeach.Location = new System.Drawing.Point(48, 27);
            this.label_bTeach.Name = "label_bTeach";
            this.label_bTeach.Size = new System.Drawing.Size(67, 13);
            this.label_bTeach.TabIndex = 17;
            this.label_bTeach.Text = "Teach mode";
            // 
            // pictureBox_bTeach
            // 
            this.pictureBox_bTeach.Location = new System.Drawing.Point(26, 25);
            this.pictureBox_bTeach.Name = "pictureBox_bTeach";
            this.pictureBox_bTeach.Size = new System.Drawing.Size(16, 16);
            this.pictureBox_bTeach.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox_bTeach.TabIndex = 16;
            this.pictureBox_bTeach.TabStop = false;
            // 
            // pictureBox_Gripper
            // 
            this.pictureBox_Gripper.Location = new System.Drawing.Point(68, 218);
            this.pictureBox_Gripper.Name = "pictureBox_Gripper";
            this.pictureBox_Gripper.Size = new System.Drawing.Size(93, 88);
            this.pictureBox_Gripper.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_Gripper.TabIndex = 15;
            this.pictureBox_Gripper.TabStop = false;
            // 
            // label_Gripper
            // 
            this.label_Gripper.AutoSize = true;
            this.label_Gripper.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Gripper.Location = new System.Drawing.Point(80, 129);
            this.label_Gripper.Name = "label_Gripper";
            this.label_Gripper.Size = new System.Drawing.Size(66, 20);
            this.label_Gripper.TabIndex = 14;
            this.label_Gripper.Text = "Gripper:";
            // 
            // button_GRIPPERCLOSE
            // 
            this.button_GRIPPERCLOSE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_GRIPPERCLOSE.Location = new System.Drawing.Point(115, 152);
            this.button_GRIPPERCLOSE.Name = "button_GRIPPERCLOSE";
            this.button_GRIPPERCLOSE.Size = new System.Drawing.Size(77, 60);
            this.button_GRIPPERCLOSE.TabIndex = 13;
            this.button_GRIPPERCLOSE.Text = "Close";
            this.button_GRIPPERCLOSE.UseVisualStyleBackColor = true;
            this.button_GRIPPERCLOSE.Click += new System.EventHandler(this.button_GRIPPERCLOSE_Click);
            // 
            // button_GRIPPEROPEN
            // 
            this.button_GRIPPEROPEN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_GRIPPEROPEN.Location = new System.Drawing.Point(32, 152);
            this.button_GRIPPEROPEN.Name = "button_GRIPPEROPEN";
            this.button_GRIPPEROPEN.Size = new System.Drawing.Size(77, 60);
            this.button_GRIPPEROPEN.TabIndex = 12;
            this.button_GRIPPEROPEN.Text = "Open";
            this.button_GRIPPEROPEN.UseVisualStyleBackColor = true;
            this.button_GRIPPEROPEN.Click += new System.EventHandler(this.button_GRIPPEROPEN_Click);
            // 
            // button_Motors_OFF
            // 
            this.button_Motors_OFF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_Motors_OFF.Location = new System.Drawing.Point(115, 40);
            this.button_Motors_OFF.Name = "button_Motors_OFF";
            this.button_Motors_OFF.Size = new System.Drawing.Size(77, 60);
            this.button_Motors_OFF.TabIndex = 11;
            this.button_Motors_OFF.Text = "OFF";
            this.button_Motors_OFF.UseVisualStyleBackColor = true;
            this.button_Motors_OFF.Click += new System.EventHandler(this.button_Motors_OFF_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(84, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "Motors:";
            // 
            // button_Motors_ON
            // 
            this.button_Motors_ON.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_Motors_ON.Location = new System.Drawing.Point(32, 40);
            this.button_Motors_ON.Name = "button_Motors_ON";
            this.button_Motors_ON.Size = new System.Drawing.Size(77, 60);
            this.button_Motors_ON.TabIndex = 9;
            this.button_Motors_ON.Text = "ON";
            this.button_Motors_ON.UseVisualStyleBackColor = true;
            this.button_Motors_ON.Click += new System.EventHandler(this.button_Motors_ON_Click);
            // 
            // c1DockingTabPage2
            // 
            this.c1DockingTabPage2.Controls.Add(this.groupBox3);
            this.c1DockingTabPage2.Controls.Add(this.groupBox2);
            this.c1DockingTabPage2.Location = new System.Drawing.Point(1, 24);
            this.c1DockingTabPage2.Name = "c1DockingTabPage2";
            this.c1DockingTabPage2.Size = new System.Drawing.Size(561, 347);
            this.c1DockingTabPage2.TabIndex = 1;
            this.c1DockingTabPage2.Text = "Request";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.button_Ignore);
            this.groupBox3.Controls.Add(this.button_Cancel);
            this.groupBox3.Controls.Add(this.button_TryAgain);
            this.groupBox3.Location = new System.Drawing.Point(14, 82);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(530, 252);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            // 
            // button_Ignore
            // 
            this.button_Ignore.Location = new System.Drawing.Point(122, 19);
            this.button_Ignore.Name = "button_Ignore";
            this.button_Ignore.Size = new System.Drawing.Size(253, 63);
            this.button_Ignore.TabIndex = 2;
            this.button_Ignore.Text = "Ignore";
            this.button_Ignore.UseVisualStyleBackColor = true;
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(122, 164);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(253, 63);
            this.button_Cancel.TabIndex = 1;
            this.button_Cancel.Text = "Cancel operation and delete sample";
            this.button_Cancel.UseVisualStyleBackColor = true;
            // 
            // button_TryAgain
            // 
            this.button_TryAgain.Location = new System.Drawing.Point(122, 95);
            this.button_TryAgain.Name = "button_TryAgain";
            this.button_TryAgain.Size = new System.Drawing.Size(253, 63);
            this.button_TryAgain.TabIndex = 0;
            this.button_TryAgain.Text = "Try again";
            this.button_TryAgain.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.label_Info);
            this.groupBox2.Location = new System.Drawing.Point(14, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(530, 64);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // label_Info
            // 
            this.label_Info.AutoSize = true;
            this.label_Info.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Info.Location = new System.Drawing.Point(10, 26);
            this.label_Info.Name = "label_Info";
            this.label_Info.Size = new System.Drawing.Size(444, 17);
            this.label_Info.TabIndex = 0;
            this.label_Info.Text = "The crucible/vial wasn\'t gripped by the robot! Please select an option.\r\n";
            // 
            // button_START
            // 
            this.button_START.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_START.Location = new System.Drawing.Point(18, 404);
            this.button_START.Name = "button_START";
            this.button_START.Size = new System.Drawing.Size(75, 23);
            this.button_START.TabIndex = 3;
            this.button_START.Text = "Start";
            this.button_START.UseVisualStyleBackColor = true;
            this.button_START.Click += new System.EventHandler(this.button_START_Click);
            // 
            // button_STOP
            // 
            this.button_STOP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_STOP.Location = new System.Drawing.Point(18, 433);
            this.button_STOP.Name = "button_STOP";
            this.button_STOP.Size = new System.Drawing.Size(75, 23);
            this.button_STOP.TabIndex = 4;
            this.button_STOP.Text = "Stop";
            this.button_STOP.UseVisualStyleBackColor = true;
            this.button_STOP.Click += new System.EventHandler(this.button_STOP_Click);
            // 
            // button_PAUSE
            // 
            this.button_PAUSE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_PAUSE.Location = new System.Drawing.Point(114, 404);
            this.button_PAUSE.Name = "button_PAUSE";
            this.button_PAUSE.Size = new System.Drawing.Size(75, 23);
            this.button_PAUSE.TabIndex = 5;
            this.button_PAUSE.Text = "Pause";
            this.button_PAUSE.UseVisualStyleBackColor = true;
            this.button_PAUSE.Click += new System.EventHandler(this.button_PAUSE_Click);
            // 
            // button_CONTINUE
            // 
            this.button_CONTINUE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_CONTINUE.Location = new System.Drawing.Point(114, 433);
            this.button_CONTINUE.Name = "button_CONTINUE";
            this.button_CONTINUE.Size = new System.Drawing.Size(75, 23);
            this.button_CONTINUE.TabIndex = 6;
            this.button_CONTINUE.Text = "Continue";
            this.button_CONTINUE.UseVisualStyleBackColor = true;
            this.button_CONTINUE.Click += new System.EventHandler(this.button_CONTINUE_Click);
            // 
            // button_RESET
            // 
            this.button_RESET.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_RESET.Location = new System.Drawing.Point(216, 433);
            this.button_RESET.Name = "button_RESET";
            this.button_RESET.Size = new System.Drawing.Size(75, 23);
            this.button_RESET.TabIndex = 7;
            this.button_RESET.Text = "Reset";
            this.button_RESET.UseVisualStyleBackColor = true;
            this.button_RESET.Click += new System.EventHandler(this.button_RESET_Click);
            // 
            // button_HOME
            // 
            this.button_HOME.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_HOME.Location = new System.Drawing.Point(216, 404);
            this.button_HOME.Name = "button_HOME";
            this.button_HOME.Size = new System.Drawing.Size(75, 23);
            this.button_HOME.TabIndex = 9;
            this.button_HOME.Text = "Home";
            this.button_HOME.UseVisualStyleBackColor = true;
            this.button_HOME.Click += new System.EventHandler(this.button_HOME_Click);
            // 
            // button_LOGIN
            // 
            this.button_LOGIN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_LOGIN.Location = new System.Drawing.Point(499, 404);
            this.button_LOGIN.Name = "button_LOGIN";
            this.button_LOGIN.Size = new System.Drawing.Size(75, 23);
            this.button_LOGIN.TabIndex = 11;
            this.button_LOGIN.Text = "Login";
            this.button_LOGIN.UseVisualStyleBackColor = true;
            this.button_LOGIN.Click += new System.EventHandler(this.button_LOGIN_Click);
            // 
            // button_LOGOUT
            // 
            this.button_LOGOUT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_LOGOUT.Location = new System.Drawing.Point(499, 433);
            this.button_LOGOUT.Name = "button_LOGOUT";
            this.button_LOGOUT.Size = new System.Drawing.Size(75, 23);
            this.button_LOGOUT.TabIndex = 12;
            this.button_LOGOUT.Text = "Logout";
            this.button_LOGOUT.UseVisualStyleBackColor = true;
            this.button_LOGOUT.Click += new System.EventHandler(this.button_LOGOUT_Click);
            // 
            // imageList_Gripper
            // 
            this.imageList_Gripper.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList_Gripper.ImageStream")));
            this.imageList_Gripper.TransparentColor = System.Drawing.Color.Red;
            this.imageList_Gripper.Images.SetKeyName(0, "Gripper_close");
            this.imageList_Gripper.Images.SetKeyName(1, "Gripper_open");
            this.imageList_Gripper.Images.SetKeyName(2, "Gripper_gripped");
            // 
            // RobotAdmin_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 505);
            this.Controls.Add(this.button_LOGOUT);
            this.Controls.Add(this.button_LOGIN);
            this.Controls.Add(this.button_HOME);
            this.Controls.Add(this.button_RESET);
            this.Controls.Add(this.button_CONTINUE);
            this.Controls.Add(this.button_PAUSE);
            this.Controls.Add(this.button_STOP);
            this.Controls.Add(this.button_START);
            this.Controls.Add(this.c1DockingTab1);
            this.Controls.Add(this.c1StatusBar_LabManager);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(500, 540);
            this.Name = "RobotAdmin_Form";
            this.Text = "RobotAdmin_Form";
            this.Load += new System.EventHandler(this.RobotAdmin_Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar_LabManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1DockingTab1)).EndInit();
            this.c1DockingTab1.ResumeLayout(false);
            this.c1DockingTabPage1.ResumeLayout(false);
            this.groupBox_MOTOR.ResumeLayout(false);
            this.groupBox_MOTOR.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_bReady)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_bRunning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_bPaused)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_bError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_bEStop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_bSafeguard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_bSError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_bWarning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_bAuto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_bTeach)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Gripper)).EndInit();
            this.c1DockingTabPage2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem miFile;
        private System.Windows.Forms.MenuItem miExit;
        private C1.Win.C1Ribbon.C1StatusBar c1StatusBar_LabManager;
        private C1.Win.C1Ribbon.RibbonLabel ribbonLabel_Connect;
        private System.Windows.Forms.ImageList imgList;
        private C1.Win.C1Command.C1DockingTab c1DockingTab1;
        private C1.Win.C1Command.C1DockingTabPage c1DockingTabPage1;
        private System.Windows.Forms.Button button_START;
        private System.Windows.Forms.Button button_STOP;
        private System.Windows.Forms.Button button_PAUSE;
        private System.Windows.Forms.Button button_CONTINUE;
        private System.Windows.Forms.Button button_RESET;
        private System.Windows.Forms.Button button_Motors_OFF;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_Motors_ON;
        private System.Windows.Forms.Button button_HOME;
        private System.Windows.Forms.Button button_LOGIN;
        private System.Windows.Forms.Button button_LOGOUT;
        private System.Windows.Forms.Button button_GRIPPERCLOSE;
        private System.Windows.Forms.Button button_GRIPPEROPEN;
        private System.Windows.Forms.Label label_Gripper;
        private System.Windows.Forms.GroupBox groupBox_MOTOR;
        private System.Windows.Forms.PictureBox pictureBox_Gripper;
        private System.Windows.Forms.ImageList imageList_Gripper;
        private System.Windows.Forms.PictureBox pictureBox_bTeach;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label_bTeach;
        private System.Windows.Forms.Label label_bReady;
        private System.Windows.Forms.PictureBox pictureBox_bReady;
        private System.Windows.Forms.Label label_bRunning;
        private System.Windows.Forms.PictureBox pictureBox_bRunning;
        private System.Windows.Forms.Label label_bPaused;
        private System.Windows.Forms.PictureBox pictureBox_bPaused;
        private System.Windows.Forms.Label label_bError;
        private System.Windows.Forms.PictureBox pictureBox_bError;
        private System.Windows.Forms.Label label_bEStop;
        private System.Windows.Forms.PictureBox pictureBox_bEStop;
        private System.Windows.Forms.Label label_bSafeguard;
        private System.Windows.Forms.PictureBox pictureBox_bSafeguard;
        private System.Windows.Forms.Label label_bSError;
        private System.Windows.Forms.PictureBox pictureBox_bSError;
        private System.Windows.Forms.Label label_bWarning;
        private System.Windows.Forms.PictureBox pictureBox_bWarning;
        private System.Windows.Forms.Label label_bAuto;
        private System.Windows.Forms.PictureBox pictureBox_bAuto;
        private C1.Win.C1Command.C1DockingTabPage c1DockingTabPage2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label_Info;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button_Ignore;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_TryAgain;
    }
}