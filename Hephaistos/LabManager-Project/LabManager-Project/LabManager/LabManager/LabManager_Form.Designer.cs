namespace LabManager
{
    partial class LabManager
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

       
        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LabManager));
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.miFile = new System.Windows.Forms.MenuItem();
            this.miExit = new System.Windows.Forms.MenuItem();
            this.miExtras = new System.Windows.Forms.MenuItem();
            this.menuItem_Routing = new System.Windows.Forms.MenuItem();
            this.menuItem_Machines = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem_AdminRobot = new System.Windows.Forms.MenuItem();
            this.menuItem_Conf = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem_Scanner = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem_TGASampleList = new System.Windows.Forms.MenuItem();
            this.c1StatusBar_LabManager = new C1.Win.C1Ribbon.C1StatusBar();
            this.ribbonLabel_Connect = new C1.Win.C1Ribbon.RibbonLabel();
            this.ribbonSeparator2 = new C1.Win.C1Ribbon.RibbonSeparator();
            this.ribbonLabel_WinCC = new C1.Win.C1Ribbon.RibbonLabel();
            this.ribbonSeparator1 = new C1.Win.C1Ribbon.RibbonSeparator();
            this.ribbonLabelRoutingCheck = new C1.Win.C1Ribbon.RibbonLabel();
            this._imgList = new System.Windows.Forms.ImageList(this.components);
            this._toolbar = new System.Windows.Forms.ToolBar();
            this._tbExit = new System.Windows.Forms.ToolBarButton();
            this._tbSep2 = new System.Windows.Forms.ToolBarButton();
            this.ReloadRoutingTable = new System.Windows.Forms.ToolBarButton();
            this._tbSep1 = new System.Windows.Forms.ToolBarButton();
            this.toolBarReloadLabmanagerConnect = new System.Windows.Forms.ToolBarButton();
            this.c1TextBox_Routing_Log = new C1.Win.C1Input.C1TextBox();
            this.c1DockingTab1 = new C1.Win.C1Command.C1DockingTab();
            this.c1DockingTabPage1 = new C1.Win.C1Command.C1DockingTabPage();
            this.c1DockingTabPage2 = new C1.Win.C1Command.C1DockingTabPage();
            this.c1TextBox_ERROR_Log = new C1.Win.C1Input.C1TextBox();
            this.c1DockingTabPage3 = new C1.Win.C1Command.C1DockingTabPage();
            this.c1TextBox_WARNING_Log = new C1.Win.C1Input.C1TextBox();
            this.c1DockingTabPage4 = new C1.Win.C1Command.C1DockingTabPage();
            this.c1TextBox_Communication_Log = new C1.Win.C1Input.C1TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar_LabManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1TextBox_Routing_Log)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1DockingTab1)).BeginInit();
            this.c1DockingTab1.SuspendLayout();
            this.c1DockingTabPage1.SuspendLayout();
            this.c1DockingTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1TextBox_ERROR_Log)).BeginInit();
            this.c1DockingTabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1TextBox_WARNING_Log)).BeginInit();
            this.c1DockingTabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1TextBox_Communication_Log)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miFile,
            this.miExtras,
            this.menuItem3});
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
            // miExtras
            // 
            this.miExtras.Index = 1;
            this.miExtras.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem_Routing,
            this.menuItem_Machines,
            this.menuItem1,
            this.menuItem_AdminRobot,
            this.menuItem_Conf,
            this.menuItem5});
            this.miExtras.Text = "&Administration";
            // 
            // menuItem_Routing
            // 
            this.menuItem_Routing.Index = 0;
            this.menuItem_Routing.Text = "Routing";
            this.menuItem_Routing.Click += new System.EventHandler(this.menuItem_Routing_Click);
            // 
            // menuItem_Machines
            // 
            this.menuItem_Machines.Index = 1;
            this.menuItem_Machines.Text = "Machines";
            this.menuItem_Machines.Click += new System.EventHandler(this.menuItem_Machines_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 2;
            this.menuItem1.Text = "Test Robot Positions";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click_1);
            // 
            // menuItem_AdminRobot
            // 
            this.menuItem_AdminRobot.Index = 3;
            this.menuItem_AdminRobot.Text = "Admin Robot";
            this.menuItem_AdminRobot.Click += new System.EventHandler(this.menuItem_AdminRobot_Click);
            // 
            // menuItem_Conf
            // 
            this.menuItem_Conf.Index = 4;
            this.menuItem_Conf.Text = "Configuration";
            this.menuItem_Conf.Click += new System.EventHandler(this.menuItem_Conf_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 5;
            this.menuItem5.Text = "DB-Adminstration";
            this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 2;
            this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem4,
            this.menuItem_Scanner,
            this.menuItem2,
            this.menuItem_TGASampleList});
            this.menuItem3.Text = "Extras";
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 0;
            this.menuItem4.Text = "Communication-Test";
            this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
            // 
            // menuItem_Scanner
            // 
            this.menuItem_Scanner.Enabled = false;
            this.menuItem_Scanner.Index = 1;
            this.menuItem_Scanner.Text = "ScannerTest";
            this.menuItem_Scanner.Click += new System.EventHandler(this.menuItem_Scanner_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 2;
            this.menuItem2.Text = "TimeForAlarmList";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // menuItem_TGASampleList
            // 
            this.menuItem_TGASampleList.Index = 3;
            this.menuItem_TGASampleList.Text = "TGA Sample List";
            this.menuItem_TGASampleList.Click += new System.EventHandler(this.menuItem_TGASampleList_Click);
            // 
            // c1StatusBar_LabManager
            // 
            this.c1StatusBar_LabManager.Location = new System.Drawing.Point(0, 379);
            this.c1StatusBar_LabManager.Name = "c1StatusBar_LabManager";
            this.c1StatusBar_LabManager.RightPaneItems.Add(this.ribbonLabel_Connect);
            this.c1StatusBar_LabManager.RightPaneItems.Add(this.ribbonSeparator2);
            this.c1StatusBar_LabManager.RightPaneItems.Add(this.ribbonLabel_WinCC);
            this.c1StatusBar_LabManager.RightPaneItems.Add(this.ribbonSeparator1);
            this.c1StatusBar_LabManager.RightPaneItems.Add(this.ribbonLabelRoutingCheck);
            this.c1StatusBar_LabManager.Size = new System.Drawing.Size(976, 22);
            // 
            // ribbonLabel_Connect
            // 
            this.ribbonLabel_Connect.Name = "ribbonLabel_Connect";
            this.ribbonLabel_Connect.Text = "LabManagerConnect";
            // 
            // ribbonSeparator2
            // 
            this.ribbonSeparator2.Name = "ribbonSeparator2";
            // 
            // ribbonLabel_WinCC
            // 
            this.ribbonLabel_WinCC.Name = "ribbonLabel_WinCC";
            this.ribbonLabel_WinCC.Text = "WinCC";
            // 
            // ribbonSeparator1
            // 
            this.ribbonSeparator1.Name = "ribbonSeparator1";
            // 
            // ribbonLabelRoutingCheck
            // 
            this.ribbonLabelRoutingCheck.Name = "ribbonLabelRoutingCheck";
            this.ribbonLabelRoutingCheck.Text = "Run/Hold mode";
            // 
            // _imgList
            // 
            this._imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_imgList.ImageStream")));
            this._imgList.TransparentColor = System.Drawing.Color.Red;
            this._imgList.Images.SetKeyName(0, "Log-Out.ico");
            this._imgList.Images.SetKeyName(1, "refresh-icon_LMC.png");
            this._imgList.Images.SetKeyName(2, "Actions-view-refresh-icon_16_transparent.png");
            this._imgList.Images.SetKeyName(3, "arrow-redo-icon.png");
            // 
            // _toolbar
            // 
            this._toolbar.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this._toolbar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this._tbExit,
            this._tbSep2,
            this.ReloadRoutingTable,
            this._tbSep1,
            this.toolBarReloadLabmanagerConnect});
            this._toolbar.DropDownArrows = true;
            this._toolbar.ImageList = this._imgList;
            this._toolbar.Location = new System.Drawing.Point(0, 0);
            this._toolbar.Name = "_toolbar";
            this._toolbar.ShowToolTips = true;
            this._toolbar.Size = new System.Drawing.Size(976, 28);
            this._toolbar.TabIndex = 5;
            this._toolbar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this._toolbar_ButtonClick);
            // 
            // _tbExit
            // 
            this._tbExit.ImageKey = "Log-Out.ico";
            this._tbExit.Name = "_tbExit";
            this._tbExit.ToolTipText = "Exit";
            // 
            // _tbSep2
            // 
            this._tbSep2.Name = "_tbSep2";
            this._tbSep2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ReloadRoutingTable
            // 
            this.ReloadRoutingTable.ImageKey = "arrow-redo-icon.png";
            this.ReloadRoutingTable.Name = "ReloadRoutingTable";
            this.ReloadRoutingTable.ToolTipText = "Reload routing table data";
            // 
            // _tbSep1
            // 
            this._tbSep1.Name = "_tbSep1";
            this._tbSep1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // toolBarReloadLabmanagerConnect
            // 
            this.toolBarReloadLabmanagerConnect.ImageKey = "refresh-icon_LMC.png";
            this.toolBarReloadLabmanagerConnect.Name = "toolBarReloadLabmanagerConnect";
            this.toolBarReloadLabmanagerConnect.ToolTipText = "reloads the  variables on LabManagerConnect";
            // 
            // c1TextBox_Routing_Log
            // 
            this.c1TextBox_Routing_Log.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.c1TextBox_Routing_Log.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(198)))));
            this.c1TextBox_Routing_Log.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.c1TextBox_Routing_Log.Location = new System.Drawing.Point(3, 3);
            this.c1TextBox_Routing_Log.MaxLength = 256;
            this.c1TextBox_Routing_Log.Multiline = true;
            this.c1TextBox_Routing_Log.Name = "c1TextBox_Routing_Log";
            this.c1TextBox_Routing_Log.ReadOnly = true;
            this.c1TextBox_Routing_Log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.c1TextBox_Routing_Log.Size = new System.Drawing.Size(940, 277);
            this.c1TextBox_Routing_Log.TabIndex = 7;
            this.c1TextBox_Routing_Log.Tag = null;
            this.c1TextBox_Routing_Log.TextDetached = true;
            this.c1TextBox_Routing_Log.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.c1TextBox_Routing_Log.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            // 
            // c1DockingTab1
            // 
            this.c1DockingTab1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.c1DockingTab1.Controls.Add(this.c1DockingTabPage1);
            this.c1DockingTab1.Controls.Add(this.c1DockingTabPage2);
            this.c1DockingTab1.Controls.Add(this.c1DockingTabPage3);
            this.c1DockingTab1.Controls.Add(this.c1DockingTabPage4);
            this.c1DockingTab1.Location = new System.Drawing.Point(8, 19);
            this.c1DockingTab1.Name = "c1DockingTab1";
            this.c1DockingTab1.Size = new System.Drawing.Size(952, 312);
            this.c1DockingTab1.TabIndex = 8;
            this.c1DockingTab1.TabStyle = C1.Win.C1Command.TabStyleEnum.WindowsXP;
            this.c1DockingTab1.VisualStyle = C1.Win.C1Command.VisualStyle.Custom;
            this.c1DockingTab1.VisualStyleBase = C1.Win.C1Command.VisualStyle.System;
            // 
            // c1DockingTabPage1
            // 
            this.c1DockingTabPage1.Controls.Add(this.c1TextBox_Routing_Log);
            this.c1DockingTabPage1.Location = new System.Drawing.Point(2, 25);
            this.c1DockingTabPage1.Name = "c1DockingTabPage1";
            this.c1DockingTabPage1.Size = new System.Drawing.Size(946, 283);
            this.c1DockingTabPage1.TabIndex = 0;
            this.c1DockingTabPage1.Text = "Routing_Log";
            // 
            // c1DockingTabPage2
            // 
            this.c1DockingTabPage2.Controls.Add(this.c1TextBox_ERROR_Log);
            this.c1DockingTabPage2.Location = new System.Drawing.Point(2, 25);
            this.c1DockingTabPage2.Name = "c1DockingTabPage2";
            this.c1DockingTabPage2.Size = new System.Drawing.Size(946, 283);
            this.c1DockingTabPage2.TabIndex = 1;
            this.c1DockingTabPage2.Text = "Error log";
            // 
            // c1TextBox_ERROR_Log
            // 
            this.c1TextBox_ERROR_Log.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.c1TextBox_ERROR_Log.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(198)))));
            this.c1TextBox_ERROR_Log.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.c1TextBox_ERROR_Log.Location = new System.Drawing.Point(3, 3);
            this.c1TextBox_ERROR_Log.Multiline = true;
            this.c1TextBox_ERROR_Log.Name = "c1TextBox_ERROR_Log";
            this.c1TextBox_ERROR_Log.ReadOnly = true;
            this.c1TextBox_ERROR_Log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.c1TextBox_ERROR_Log.Size = new System.Drawing.Size(940, 277);
            this.c1TextBox_ERROR_Log.TabIndex = 8;
            this.c1TextBox_ERROR_Log.Tag = null;
            this.c1TextBox_ERROR_Log.TextDetached = true;
            this.c1TextBox_ERROR_Log.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.c1TextBox_ERROR_Log.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            // 
            // c1DockingTabPage3
            // 
            this.c1DockingTabPage3.Controls.Add(this.c1TextBox_WARNING_Log);
            this.c1DockingTabPage3.Location = new System.Drawing.Point(2, 25);
            this.c1DockingTabPage3.Name = "c1DockingTabPage3";
            this.c1DockingTabPage3.Size = new System.Drawing.Size(946, 283);
            this.c1DockingTabPage3.TabIndex = 2;
            this.c1DockingTabPage3.Text = "Warning Log";
            // 
            // c1TextBox_WARNING_Log
            // 
            this.c1TextBox_WARNING_Log.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.c1TextBox_WARNING_Log.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(198)))));
            this.c1TextBox_WARNING_Log.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.c1TextBox_WARNING_Log.Location = new System.Drawing.Point(3, 3);
            this.c1TextBox_WARNING_Log.Multiline = true;
            this.c1TextBox_WARNING_Log.Name = "c1TextBox_WARNING_Log";
            this.c1TextBox_WARNING_Log.ReadOnly = true;
            this.c1TextBox_WARNING_Log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.c1TextBox_WARNING_Log.Size = new System.Drawing.Size(940, 277);
            this.c1TextBox_WARNING_Log.TabIndex = 9;
            this.c1TextBox_WARNING_Log.Tag = null;
            this.c1TextBox_WARNING_Log.TextDetached = true;
            this.c1TextBox_WARNING_Log.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.c1TextBox_WARNING_Log.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            // 
            // c1DockingTabPage4
            // 
            this.c1DockingTabPage4.Controls.Add(this.c1TextBox_Communication_Log);
            this.c1DockingTabPage4.Location = new System.Drawing.Point(2, 25);
            this.c1DockingTabPage4.Name = "c1DockingTabPage4";
            this.c1DockingTabPage4.Size = new System.Drawing.Size(946, 283);
            this.c1DockingTabPage4.TabIndex = 3;
            this.c1DockingTabPage4.Text = "Communication Log";
            // 
            // c1TextBox_Communication_Log
            // 
            this.c1TextBox_Communication_Log.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.c1TextBox_Communication_Log.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(198)))));
            this.c1TextBox_Communication_Log.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.c1TextBox_Communication_Log.Location = new System.Drawing.Point(3, 3);
            this.c1TextBox_Communication_Log.Multiline = true;
            this.c1TextBox_Communication_Log.Name = "c1TextBox_Communication_Log";
            this.c1TextBox_Communication_Log.ReadOnly = true;
            this.c1TextBox_Communication_Log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.c1TextBox_Communication_Log.Size = new System.Drawing.Size(940, 277);
            this.c1TextBox_Communication_Log.TabIndex = 10;
            this.c1TextBox_Communication_Log.Tag = null;
            this.c1TextBox_Communication_Log.TextDetached = true;
            this.c1TextBox_Communication_Log.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.c1TextBox_Communication_Log.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.c1DockingTab1);
            this.groupBox1.Location = new System.Drawing.Point(4, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(971, 337);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            // 
            // LabManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(976, 401);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this._toolbar);
            this.Controls.Add(this.c1StatusBar_LabManager);
            this.Menu = this.mainMenu1;
            this.Name = "LabManager";
            this.Text = "LabManager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LabManager_FormClosing);
            this.Load += new System.EventHandler(this.LabManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar_LabManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1TextBox_Routing_Log)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1DockingTab1)).EndInit();
            this.c1DockingTab1.ResumeLayout(false);
            this.c1DockingTabPage1.ResumeLayout(false);
            this.c1DockingTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1TextBox_ERROR_Log)).EndInit();
            this.c1DockingTabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1TextBox_WARNING_Log)).EndInit();
            this.c1DockingTabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1TextBox_Communication_Log)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem miFile;
        private System.Windows.Forms.MenuItem miExit;
        private System.Windows.Forms.MenuItem miExtras;
        private System.Windows.Forms.MenuItem menuItem_Routing;
        private C1.Win.C1Ribbon.C1StatusBar c1StatusBar_LabManager;
        private System.Windows.Forms.ImageList _imgList;
        private System.Windows.Forms.ToolBar _toolbar;
        private System.Windows.Forms.ToolBarButton _tbExit;
        private System.Windows.Forms.ToolBarButton _tbSep2;
        private C1.Win.C1Input.C1TextBox c1TextBox_Routing_Log;
        private C1.Win.C1Command.C1DockingTab c1DockingTab1;
        private C1.Win.C1Command.C1DockingTabPage c1DockingTabPage1;
        private C1.Win.C1Command.C1DockingTabPage c1DockingTabPage2;
        private C1.Win.C1Command.C1DockingTabPage c1DockingTabPage3;
        private C1.Win.C1Command.C1DockingTabPage c1DockingTabPage4;
        private C1.Win.C1Input.C1TextBox c1TextBox_ERROR_Log;
        private C1.Win.C1Input.C1TextBox c1TextBox_WARNING_Log;
        private C1.Win.C1Input.C1TextBox c1TextBox_Communication_Log;
        private System.Windows.Forms.MenuItem menuItem_Machines;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem4;
     //   private System.Windows.Forms.ToolBarButton _tbTopMost;
      //  private System.Windows.Forms.ToolBarButton ReloadRoutingTable;
        private System.Windows.Forms.MenuItem menuItem_Scanner;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.ToolBarButton ReloadRoutingTable;
        private C1.Win.C1Ribbon.RibbonLabel ribbonLabel_Connect;
        private C1.Win.C1Ribbon.RibbonLabel ribbonLabel_WinCC;
        private System.Windows.Forms.MenuItem menuItem_TGASampleList;
        private C1.Win.C1Ribbon.RibbonSeparator ribbonSeparator2;
        private C1.Win.C1Ribbon.RibbonSeparator ribbonSeparator1;
        private C1.Win.C1Ribbon.RibbonLabel ribbonLabelRoutingCheck;
        private System.Windows.Forms.MenuItem menuItem_AdminRobot;
        private System.Windows.Forms.MenuItem menuItem_Conf;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolBarButton _tbSep1;
        private System.Windows.Forms.ToolBarButton toolBarReloadLabmanagerConnect;
      

    }
}

