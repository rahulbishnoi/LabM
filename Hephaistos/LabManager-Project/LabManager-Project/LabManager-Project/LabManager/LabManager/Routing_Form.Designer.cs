namespace LabManager
{
    partial class Routing_Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        static System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();


        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {

            myTimer.Stop();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Routing_Form));
            this.treeView_routing = new System.Windows.Forms.TreeView();
            this._imgList = new System.Windows.Forms.ImageList(this.components);
            this.c1FlexGrid_Conditions = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.button_Condition_Add = new System.Windows.Forms.Button();
            this.button_Condition_Delete = new System.Windows.Forms.Button();
            this.button_Condition_Save = new System.Windows.Forms.Button();
            this.comboBox_StatusBit = new System.Windows.Forms.ComboBox();
            this.comboBox_StatusBit_Value = new System.Windows.Forms.ComboBox();
            this.comboBox_Operation = new System.Windows.Forms.ComboBox();
            this.comboBox_SamplePos = new System.Windows.Forms.ComboBox();
            this.comboBox_SampleType = new System.Windows.Forms.ComboBox();
            this.numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.comboBox_MachineList = new System.Windows.Forms.ComboBox();
            this.pictureBox_Conditions = new System.Windows.Forms.PictureBox();
            this.c1Button_Condition_down = new C1.Win.C1Input.C1Button();
            this.c1Button_Condition_up = new C1.Win.C1Input.C1Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox_Command = new System.Windows.Forms.PictureBox();
            this.c1FlexGrid_Commands = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.c1Button_command_up = new C1.Win.C1Input.C1Button();
            this.button_Command_Add = new System.Windows.Forms.Button();
            this.button_Command_Save = new System.Windows.Forms.Button();
            this.button_Command_Delete = new System.Windows.Forms.Button();
            this.c1Button_command_down = new C1.Win.C1Input.C1Button();
            this.comboBox_Choice = new System.Windows.Forms.ComboBox();
            this.button_AddChoise = new System.Windows.Forms.Button();
            this.button_ColorPicker = new System.Windows.Forms.Button();
            this.comboBox_Command_SampleProgram = new System.Windows.Forms.ComboBox();
            this.comboBox_CommandValue2 = new System.Windows.Forms.ComboBox();
            this.comboBox_Command_MachineList = new System.Windows.Forms.ComboBox();
            this.numericUpDown_Commands = new System.Windows.Forms.NumericUpDown();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.c1Ribbon = new C1.Win.C1Ribbon.C1Ribbon();
            this.ribbonApplicationMenu1 = new C1.Win.C1Ribbon.RibbonApplicationMenu();
            this.ribbonButton_Expand = new C1.Win.C1Ribbon.RibbonButton();
            this.ribbonSeparator3 = new C1.Win.C1Ribbon.RibbonSeparator();
            this.ribbonButton_Collapse = new C1.Win.C1Ribbon.RibbonButton();
            this.ribbonSeparator2 = new C1.Win.C1Ribbon.RibbonSeparator();
            this.ribbonButton_Save = new C1.Win.C1Ribbon.RibbonButton();
            this.ribbonSeparator5 = new C1.Win.C1Ribbon.RibbonSeparator();
            this.ribbonButton_RelaodRoutingData = new C1.Win.C1Ribbon.RibbonButton();
            this.ribbonSeparator1 = new C1.Win.C1Ribbon.RibbonSeparator();
            this.ribbonButton_Exit = new C1.Win.C1Ribbon.RibbonButton();
            this.ribbonConfigToolBar1 = new C1.Win.C1Ribbon.RibbonConfigToolBar();
            this.ribbonCheckBox1 = new C1.Win.C1Ribbon.RibbonCheckBox();
            this.ribbonTextBox_Search = new C1.Win.C1Ribbon.RibbonTextBox();
            this.ribbonSeparator7 = new C1.Win.C1Ribbon.RibbonSeparator();
            this.ribbonNumericBox_Warning = new C1.Win.C1Ribbon.RibbonNumericBox();
            this.ribbonSeparator6 = new C1.Win.C1Ribbon.RibbonSeparator();
            this.ribbonNumericBox_Alarm = new C1.Win.C1Ribbon.RibbonNumericBox();
            this.ribbonSeparator4 = new C1.Win.C1Ribbon.RibbonSeparator();
            this.ribbonCheckBox_CheckCondition = new C1.Win.C1Ribbon.RibbonCheckBox();
            this.ribbonQat1 = new C1.Win.C1Ribbon.RibbonQat();
            this.c1StatusBar_Routing = new C1.Win.C1Ribbon.C1StatusBar();
            this.treeView2 = new System.Windows.Forms.TreeView();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid_Conditions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Conditions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Command)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid_Commands)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Commands)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar_Routing)).BeginInit();
            this.SuspendLayout();
            // 
            // treeView_routing
            // 
            this.treeView_routing.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.treeView_routing.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView_routing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView_routing.ImageIndex = 0;
            this.treeView_routing.ImageList = this._imgList;
            this.treeView_routing.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.treeView_routing.Location = new System.Drawing.Point(0, 0);
            this.treeView_routing.Name = "treeView_routing";
            this.treeView_routing.SelectedImageIndex = 28;
            this.treeView_routing.Size = new System.Drawing.Size(209, 476);
            this.treeView_routing.TabIndex = 0;
            this.treeView_routing.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeView_routing_AfterLabelEdit);
            this.treeView_routing.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_routing_NodeMouseClick);
            this.treeView_routing.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView_routing_MouseDown);
            this.treeView_routing.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeView_routing_MouseUp);
            // 
            // _imgList
            // 
            this._imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_imgList.ImageStream")));
            this._imgList.TransparentColor = System.Drawing.Color.Red;
            this._imgList.Images.SetKeyName(0, "");
            this._imgList.Images.SetKeyName(1, "");
            this._imgList.Images.SetKeyName(2, "");
            this._imgList.Images.SetKeyName(3, "");
            this._imgList.Images.SetKeyName(4, "cancel-icon.png");
            this._imgList.Images.SetKeyName(5, "offline-icon_32.png");
            this._imgList.Images.SetKeyName(6, "Log-Out-icon.png");
            this._imgList.Images.SetKeyName(7, "Ready-icon_32.png");
            this._imgList.Images.SetKeyName(8, "RUN-icon.png");
            this._imgList.Images.SetKeyName(9, "performance.ico");
            this._imgList.Images.SetKeyName(10, "Save.ico");
            this._imgList.Images.SetKeyName(11, "tool-pro.ico");
            this._imgList.Images.SetKeyName(12, "machine-icon.png");
            this._imgList.Images.SetKeyName(13, "Enter_gruen.ico");
            this._imgList.Images.SetKeyName(14, "Expand_tree.ico");
            this._imgList.Images.SetKeyName(15, "Collapse_tree.ico");
            this._imgList.Images.SetKeyName(16, "doc.ico");
            this._imgList.Images.SetKeyName(17, "edit.ico");
            this._imgList.Images.SetKeyName(18, "list-bullets-icon.png");
            this._imgList.Images.SetKeyName(19, "list-edit-all-icon.png");
            this._imgList.Images.SetKeyName(20, "Things-Book-icon.png");
            this._imgList.Images.SetKeyName(21, "Glas_Yellow.ico");
            this._imgList.Images.SetKeyName(22, "Glas_Green.ico");
            this._imgList.Images.SetKeyName(23, "Glas_Red.ico");
            this._imgList.Images.SetKeyName(24, "GreenTag");
            this._imgList.Images.SetKeyName(25, "RedCross");
            this._imgList.Images.SetKeyName(26, "colors-icon");
            this._imgList.Images.SetKeyName(27, "color-icon");
            this._imgList.Images.SetKeyName(28, "note-add-icon.png");
            this._imgList.Images.SetKeyName(29, "plus.png");
            this._imgList.Images.SetKeyName(30, "Visualpharm-Must-Have-Delete.ico");
            this._imgList.Images.SetKeyName(31, "Custom-Icon-Design-Office-Add-2.ico");
            // 
            // c1FlexGrid_Conditions
            // 
            this.c1FlexGrid_Conditions.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1FlexGrid_Conditions.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.c1FlexGrid_Conditions.ColumnInfo = resources.GetString("c1FlexGrid_Conditions.ColumnInfo");
            this.c1FlexGrid_Conditions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1FlexGrid_Conditions.ExtendLastCol = true;
            this.c1FlexGrid_Conditions.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None;
            this.c1FlexGrid_Conditions.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
            this.c1FlexGrid_Conditions.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this.c1FlexGrid_Conditions.Location = new System.Drawing.Point(6, 20);
            this.c1FlexGrid_Conditions.Name = "c1FlexGrid_Conditions";
            this.c1FlexGrid_Conditions.Rows.Count = 1;
            this.c1FlexGrid_Conditions.Rows.DefaultSize = 19;
            this.c1FlexGrid_Conditions.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
            this.c1FlexGrid_Conditions.Size = new System.Drawing.Size(687, 200);
            this.c1FlexGrid_Conditions.StyleInfo = resources.GetString("c1FlexGrid_Conditions.StyleInfo");
            this.c1FlexGrid_Conditions.TabIndex = 1;
            this.c1FlexGrid_Conditions.TabStop = false;
            this.c1FlexGrid_Conditions.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2007Blue;
            this.c1FlexGrid_Conditions.AfterSort += new C1.Win.C1FlexGrid.SortColEventHandler(this.c1FlexGrid_Conditions_AfterSort);
            this.c1FlexGrid_Conditions.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.c1FlexGrid_Conditions_AfterRowColChange);
            this.c1FlexGrid_Conditions.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1FlexGrid_Conditions_BeforeEdit);
            this.c1FlexGrid_Conditions.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1FlexGrid_Conditions_AfterEdit);
            this.c1FlexGrid_Conditions.ChangeEdit += new System.EventHandler(this.c1FlexGrid_Conditions_ChangeEdit);
            this.c1FlexGrid_Conditions.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1FlexGrid_Conditions_CellChanged);
            this.c1FlexGrid_Conditions.OwnerDrawCell += new C1.Win.C1FlexGrid.OwnerDrawCellEventHandler(this.c1FlexGrid_Conditions_OwnerDrawCell);
            this.c1FlexGrid_Conditions.GetCellErrorInfo += new C1.Win.C1FlexGrid.GetErrorInfoEventHandler(this.c1FlexGrid_Conditions_GetCellErrorInfo);
            this.c1FlexGrid_Conditions.Click += new System.EventHandler(this.c1FlexGrid_Conditions_Click);
            this.c1FlexGrid_Conditions.DoubleClick += new System.EventHandler(this.c1FlexGrid_Conditions_DoubleClick);
            this.c1FlexGrid_Conditions.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.c1FlexGrid_Conditions_MouseDoubleClick);
            this.c1FlexGrid_Conditions.MouseDown += new System.Windows.Forms.MouseEventHandler(this.c1FlexGrid_Conditions_MouseDown);
            this.c1FlexGrid_Conditions.MouseUp += new System.Windows.Forms.MouseEventHandler(this.c1FlexGrid_Conditions_MouseUp);
            // 
            // button_Condition_Add
            // 
            this.button_Condition_Add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_Condition_Add.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Condition_Add.ImageIndex = 31;
            this.button_Condition_Add.ImageList = this._imgList;
            this.button_Condition_Add.Location = new System.Drawing.Point(6, 226);
            this.button_Condition_Add.Margin = new System.Windows.Forms.Padding(2, 2, 0, 0);
            this.button_Condition_Add.Name = "button_Condition_Add";
            this.button_Condition_Add.Size = new System.Drawing.Size(73, 24);
            this.button_Condition_Add.TabIndex = 9;
            this.button_Condition_Add.Text = "Add row";
            this.button_Condition_Add.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_Condition_Add.UseVisualStyleBackColor = true;
            this.button_Condition_Add.Click += new System.EventHandler(this.button_Condition_Add_Click);
            // 
            // button_Condition_Delete
            // 
            this.button_Condition_Delete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_Condition_Delete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Condition_Delete.ImageIndex = 30;
            this.button_Condition_Delete.ImageList = this._imgList;
            this.button_Condition_Delete.Location = new System.Drawing.Point(87, 226);
            this.button_Condition_Delete.Margin = new System.Windows.Forms.Padding(0);
            this.button_Condition_Delete.Name = "button_Condition_Delete";
            this.button_Condition_Delete.Padding = new System.Windows.Forms.Padding(2, 2, 0, 0);
            this.button_Condition_Delete.Size = new System.Drawing.Size(67, 24);
            this.button_Condition_Delete.TabIndex = 10;
            this.button_Condition_Delete.Text = "Delete";
            this.button_Condition_Delete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_Condition_Delete.UseVisualStyleBackColor = true;
            this.button_Condition_Delete.Click += new System.EventHandler(this.button_condition_Delete_Click);
            // 
            // button_Condition_Save
            // 
            this.button_Condition_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_Condition_Save.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Condition_Save.ImageIndex = 10;
            this.button_Condition_Save.ImageList = this._imgList;
            this.button_Condition_Save.Location = new System.Drawing.Point(164, 226);
            this.button_Condition_Save.Margin = new System.Windows.Forms.Padding(0);
            this.button_Condition_Save.Name = "button_Condition_Save";
            this.button_Condition_Save.Size = new System.Drawing.Size(64, 24);
            this.button_Condition_Save.TabIndex = 11;
            this.button_Condition_Save.Text = "Save";
            this.button_Condition_Save.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_Condition_Save.UseVisualStyleBackColor = true;
            this.button_Condition_Save.Click += new System.EventHandler(this.button_Condition_Save_Click);
            // 
            // comboBox_StatusBit
            // 
            this.comboBox_StatusBit.CausesValidation = false;
            this.comboBox_StatusBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_StatusBit.Items.AddRange(new object[] {
            "Var1",
            "Var2",
            "Var3",
            "Var4"});
            this.comboBox_StatusBit.Location = new System.Drawing.Point(173, 28);
            this.comboBox_StatusBit.Name = "comboBox_StatusBit";
            this.comboBox_StatusBit.Size = new System.Drawing.Size(31, 21);
            this.comboBox_StatusBit.TabIndex = 11;
            this.comboBox_StatusBit.Tag = "";
            this.comboBox_StatusBit.Visible = false;
            // 
            // comboBox_StatusBit_Value
            // 
            this.comboBox_StatusBit_Value.DisplayMember = "Name";
            this.comboBox_StatusBit_Value.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_StatusBit_Value.FormattingEnabled = true;
            this.comboBox_StatusBit_Value.Items.AddRange(new object[] {
            "Var1",
            "Var2",
            "Var3",
            "Var4"});
            this.comboBox_StatusBit_Value.Location = new System.Drawing.Point(91, 27);
            this.comboBox_StatusBit_Value.Name = "comboBox_StatusBit_Value";
            this.comboBox_StatusBit_Value.Size = new System.Drawing.Size(31, 21);
            this.comboBox_StatusBit_Value.TabIndex = 10;
            this.comboBox_StatusBit_Value.ValueMember = "idrouting_operation_list";
            this.comboBox_StatusBit_Value.Visible = false;
            // 
            // comboBox_Operation
            // 
            this.comboBox_Operation.DisplayMember = "Name";
            this.comboBox_Operation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Operation.FormattingEnabled = true;
            this.comboBox_Operation.Items.AddRange(new object[] {
            "Var1",
            "Var2",
            "Var3",
            "Var4"});
            this.comboBox_Operation.Location = new System.Drawing.Point(128, 27);
            this.comboBox_Operation.Name = "comboBox_Operation";
            this.comboBox_Operation.Size = new System.Drawing.Size(30, 21);
            this.comboBox_Operation.TabIndex = 9;
            this.comboBox_Operation.ValueMember = "idrouting_operation_list";
            this.comboBox_Operation.Visible = false;
            // 
            // comboBox_SamplePos
            // 
            this.comboBox_SamplePos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_SamplePos.FormattingEnabled = true;
            this.comboBox_SamplePos.Items.AddRange(new object[] {
            "Var1",
            "Var2",
            "Var3",
            "Var4"});
            this.comboBox_SamplePos.Location = new System.Drawing.Point(55, 26);
            this.comboBox_SamplePos.Name = "comboBox_SamplePos";
            this.comboBox_SamplePos.Size = new System.Drawing.Size(30, 21);
            this.comboBox_SamplePos.TabIndex = 8;
            this.comboBox_SamplePos.Visible = false;
            // 
            // comboBox_SampleType
            // 
            this.comboBox_SampleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_SampleType.FormattingEnabled = true;
            this.comboBox_SampleType.Location = new System.Drawing.Point(108, 26);
            this.comboBox_SampleType.Name = "comboBox_SampleType";
            this.comboBox_SampleType.Size = new System.Drawing.Size(30, 21);
            this.comboBox_SampleType.TabIndex = 7;
            this.comboBox_SampleType.Visible = false;
            // 
            // numericUpDown
            // 
            this.numericUpDown.Location = new System.Drawing.Point(210, 30);
            this.numericUpDown.Maximum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            0});
            this.numericUpDown.Name = "numericUpDown";
            this.numericUpDown.Size = new System.Drawing.Size(49, 20);
            this.numericUpDown.TabIndex = 6;
            this.numericUpDown.Visible = false;
            // 
            // comboBox_MachineList
            // 
            this.comboBox_MachineList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_MachineList.Items.AddRange(new object[] {
            ""});
            this.comboBox_MachineList.Location = new System.Drawing.Point(155, 26);
            this.comboBox_MachineList.Name = "comboBox_MachineList";
            this.comboBox_MachineList.Size = new System.Drawing.Size(32, 21);
            this.comboBox_MachineList.TabIndex = 4;
            this.comboBox_MachineList.Visible = false;
            // 
            // pictureBox_Conditions
            // 
            this.pictureBox_Conditions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_Conditions.Location = new System.Drawing.Point(672, 226);
            this.pictureBox_Conditions.Name = "pictureBox_Conditions";
            this.pictureBox_Conditions.Size = new System.Drawing.Size(22, 22);
            this.pictureBox_Conditions.TabIndex = 12;
            this.pictureBox_Conditions.TabStop = false;
            // 
            // c1Button_Condition_down
            // 
            this.c1Button_Condition_down.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.c1Button_Condition_down.Image = ((System.Drawing.Image)(resources.GetObject("c1Button_Condition_down.Image")));
            this.c1Button_Condition_down.Location = new System.Drawing.Point(266, 227);
            this.c1Button_Condition_down.Name = "c1Button_Condition_down";
            this.c1Button_Condition_down.Size = new System.Drawing.Size(24, 24);
            this.c1Button_Condition_down.TabIndex = 14;
            this.c1Button_Condition_down.UseVisualStyleBackColor = true;
            this.c1Button_Condition_down.Click += new System.EventHandler(this.c1Button_Condition_down_Click);
            // 
            // c1Button_Condition_up
            // 
            this.c1Button_Condition_up.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.c1Button_Condition_up.Image = ((System.Drawing.Image)(resources.GetObject("c1Button_Condition_up.Image")));
            this.c1Button_Condition_up.Location = new System.Drawing.Point(296, 227);
            this.c1Button_Condition_up.Name = "c1Button_Condition_up";
            this.c1Button_Condition_up.Size = new System.Drawing.Size(24, 24);
            this.c1Button_Condition_up.TabIndex = 13;
            this.c1Button_Condition_up.UseVisualStyleBackColor = true;
            this.c1Button_Condition_up.Click += new System.EventHandler(this.c1Button_Condition_up_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Location = new System.Drawing.Point(8, 54);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView_routing);
            this.splitContainer1.Panel1MinSize = 150;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel2MinSize = 350;
            this.splitContainer1.Size = new System.Drawing.Size(918, 480);
            this.splitContainer1.SplitterDistance = 213;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 13;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.splitContainer2.Panel1.Controls.Add(this.c1Button_Condition_down);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            this.splitContainer2.Panel1.Controls.Add(this.c1Button_Condition_up);
            this.splitContainer2.Panel1.Controls.Add(this.c1FlexGrid_Conditions);
            this.splitContainer2.Panel1.Controls.Add(this.pictureBox_Conditions);
            this.splitContainer2.Panel1.Controls.Add(this.button_Condition_Save);
            this.splitContainer2.Panel1.Controls.Add(this.button_Condition_Add);
            this.splitContainer2.Panel1.Controls.Add(this.button_Condition_Delete);
            this.splitContainer2.Panel1.Padding = new System.Windows.Forms.Padding(6, 20, 6, 32);
            this.splitContainer2.Panel1MinSize = 150;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.splitContainer2.Panel2.Controls.Add(this.label2);
            this.splitContainer2.Panel2.Controls.Add(this.pictureBox_Command);
            this.splitContainer2.Panel2.Controls.Add(this.c1FlexGrid_Commands);
            this.splitContainer2.Panel2.Controls.Add(this.c1Button_command_up);
            this.splitContainer2.Panel2.Controls.Add(this.button_Command_Add);
            this.splitContainer2.Panel2.Controls.Add(this.button_Command_Save);
            this.splitContainer2.Panel2.Controls.Add(this.button_Command_Delete);
            this.splitContainer2.Panel2.Controls.Add(this.c1Button_command_down);
            this.splitContainer2.Panel2.Controls.Add(this.comboBox_Choice);
            this.splitContainer2.Panel2.Controls.Add(this.button_AddChoise);
            this.splitContainer2.Panel2.Controls.Add(this.button_ColorPicker);
            this.splitContainer2.Panel2.Controls.Add(this.comboBox_Command_SampleProgram);
            this.splitContainer2.Panel2.Controls.Add(this.comboBox_CommandValue2);
            this.splitContainer2.Panel2.Controls.Add(this.comboBox_Command_MachineList);
            this.splitContainer2.Panel2.Controls.Add(this.numericUpDown_Commands);
            this.splitContainer2.Panel2.Padding = new System.Windows.Forms.Padding(6, 20, 6, 32);
            this.splitContainer2.Panel2MinSize = 150;
            this.splitContainer2.Size = new System.Drawing.Size(703, 480);
            this.splitContainer2.SplitterDistance = 256;
            this.splitContainer2.SplitterWidth = 2;
            this.splitContainer2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Conditions";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Commands";
            // 
            // pictureBox_Command
            // 
            this.pictureBox_Command.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_Command.Location = new System.Drawing.Point(672, 210);
            this.pictureBox_Command.Name = "pictureBox_Command";
            this.pictureBox_Command.Size = new System.Drawing.Size(22, 22);
            this.pictureBox_Command.TabIndex = 13;
            this.pictureBox_Command.TabStop = false;
            // 
            // c1FlexGrid_Commands
            // 
            this.c1FlexGrid_Commands.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.c1FlexGrid_Commands.ColumnInfo = "8,1,0,0,0,95,Columns:1{Style:\"TextAlign:GeneralCenter;\";}\t2{Style:\"TextAlign:Gene" +
                "ralCenter;\";}\t";
            this.c1FlexGrid_Commands.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1FlexGrid_Commands.ExtendLastCol = true;
            this.c1FlexGrid_Commands.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None;
            this.c1FlexGrid_Commands.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this.c1FlexGrid_Commands.Location = new System.Drawing.Point(6, 20);
            this.c1FlexGrid_Commands.Name = "c1FlexGrid_Commands";
            this.c1FlexGrid_Commands.Rows.Count = 1;
            this.c1FlexGrid_Commands.Rows.DefaultSize = 19;
            this.c1FlexGrid_Commands.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
            this.c1FlexGrid_Commands.ShowErrors = true;
            this.c1FlexGrid_Commands.Size = new System.Drawing.Size(687, 166);
            this.c1FlexGrid_Commands.StyleInfo = resources.GetString("c1FlexGrid_Commands.StyleInfo");
            this.c1FlexGrid_Commands.TabIndex = 12;
            this.c1FlexGrid_Commands.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2007Blue;
            this.c1FlexGrid_Commands.AfterSort += new C1.Win.C1FlexGrid.SortColEventHandler(this.c1FlexGrid_Commands_AfterSort);
            this.c1FlexGrid_Commands.MouseEnterCell += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1FlexGrid_Commands_MouseEnterCell);
            this.c1FlexGrid_Commands.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1FlexGrid_Commands_BeforeEdit);
            this.c1FlexGrid_Commands.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1FlexGrid_Commands_AfterEdit);
            this.c1FlexGrid_Commands.ChangeEdit += new System.EventHandler(this.c1FlexGrid_Commands_ChangeEdit);
            this.c1FlexGrid_Commands.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1FlexGrid_Commands_CellChanged);
            this.c1FlexGrid_Commands.OwnerDrawCell += new C1.Win.C1FlexGrid.OwnerDrawCellEventHandler(this.c1FlexGrid_Commands_OwnerDrawCell);
            this.c1FlexGrid_Commands.GetCellErrorInfo += new C1.Win.C1FlexGrid.GetErrorInfoEventHandler(this.c1FlexGrid_Commands_GetCellErrorInfo);
            this.c1FlexGrid_Commands.Click += new System.EventHandler(this.c1FlexGrid_Commands_Click);
            this.c1FlexGrid_Commands.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.c1FlexGrid_Commands_MouseDoubleClick);
            this.c1FlexGrid_Commands.MouseDown += new System.Windows.Forms.MouseEventHandler(this.c1FlexGrid_Commands_MouseDown);
            this.c1FlexGrid_Commands.MouseUp += new System.Windows.Forms.MouseEventHandler(this.c1FlexGrid_Commands_MouseUp);
            // 
            // c1Button_command_up
            // 
            this.c1Button_command_up.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.c1Button_command_up.Image = ((System.Drawing.Image)(resources.GetObject("c1Button_command_up.Image")));
            this.c1Button_command_up.Location = new System.Drawing.Point(297, 207);
            this.c1Button_command_up.Name = "c1Button_command_up";
            this.c1Button_command_up.Size = new System.Drawing.Size(24, 24);
            this.c1Button_command_up.TabIndex = 15;
            this.c1Button_command_up.UseVisualStyleBackColor = false;
            this.c1Button_command_up.Click += new System.EventHandler(this.c1Button_command_up_Click);
            // 
            // button_Command_Add
            // 
            this.button_Command_Add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_Command_Add.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Command_Add.ImageIndex = 31;
            this.button_Command_Add.ImageList = this._imgList;
            this.button_Command_Add.Location = new System.Drawing.Point(6, 207);
            this.button_Command_Add.Name = "button_Command_Add";
            this.button_Command_Add.Size = new System.Drawing.Size(73, 24);
            this.button_Command_Add.TabIndex = 10;
            this.button_Command_Add.Text = "Add row";
            this.button_Command_Add.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_Command_Add.UseVisualStyleBackColor = true;
            this.button_Command_Add.Click += new System.EventHandler(this.button_Command_Add_Click);
            // 
            // button_Command_Save
            // 
            this.button_Command_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_Command_Save.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Command_Save.ImageIndex = 10;
            this.button_Command_Save.ImageList = this._imgList;
            this.button_Command_Save.Location = new System.Drawing.Point(164, 207);
            this.button_Command_Save.Name = "button_Command_Save";
            this.button_Command_Save.Size = new System.Drawing.Size(64, 24);
            this.button_Command_Save.TabIndex = 12;
            this.button_Command_Save.Text = "Save";
            this.button_Command_Save.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_Command_Save.UseVisualStyleBackColor = true;
            this.button_Command_Save.Click += new System.EventHandler(this.button_Command_Save_Click);
            // 
            // button_Command_Delete
            // 
            this.button_Command_Delete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_Command_Delete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Command_Delete.ImageIndex = 30;
            this.button_Command_Delete.ImageList = this._imgList;
            this.button_Command_Delete.Location = new System.Drawing.Point(87, 207);
            this.button_Command_Delete.Name = "button_Command_Delete";
            this.button_Command_Delete.Size = new System.Drawing.Size(67, 24);
            this.button_Command_Delete.TabIndex = 12;
            this.button_Command_Delete.Text = "Delete";
            this.button_Command_Delete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_Command_Delete.UseVisualStyleBackColor = true;
            this.button_Command_Delete.Click += new System.EventHandler(this.button_Command_Delete_Click);
            // 
            // c1Button_command_down
            // 
            this.c1Button_command_down.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.c1Button_command_down.Image = ((System.Drawing.Image)(resources.GetObject("c1Button_command_down.Image")));
            this.c1Button_command_down.Location = new System.Drawing.Point(267, 207);
            this.c1Button_command_down.Name = "c1Button_command_down";
            this.c1Button_command_down.Size = new System.Drawing.Size(24, 24);
            this.c1Button_command_down.TabIndex = 15;
            this.c1Button_command_down.UseVisualStyleBackColor = true;
            this.c1Button_command_down.Click += new System.EventHandler(this.c1Button_command_down_Click);
            // 
            // comboBox_Choice
            // 
            this.comboBox_Choice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox_Choice.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.comboBox_Choice.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox_Choice.FormattingEnabled = true;
            this.comboBox_Choice.Items.AddRange(new object[] {
            "<WS SampleID>",
            "<WS SampleID 4>",
            "<WS SampleID 0 3>",
            "<GT Globaltagname>",
            "<GT Globaltagname 4>",
            "<GT Globaltagname 0 5>",
            "<MT Machinetagname>",
            "<MT Machinetagname  3>",
            "<MT Machinetagname  0 3>",
            "<TIMESTAMP>"});
            this.comboBox_Choice.Location = new System.Drawing.Point(365, 210);
            this.comboBox_Choice.Name = "comboBox_Choice";
            this.comboBox_Choice.Size = new System.Drawing.Size(176, 21);
            this.comboBox_Choice.TabIndex = 15;
            // 
            // button_AddChoise
            // 
            this.button_AddChoise.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_AddChoise.Image = global::LabManager.Properties.Resources.plus;
            this.button_AddChoise.Location = new System.Drawing.Point(337, 208);
            this.button_AddChoise.Name = "button_AddChoise";
            this.button_AddChoise.Size = new System.Drawing.Size(22, 23);
            this.button_AddChoise.TabIndex = 16;
            this.button_AddChoise.Text = "+";
            this.button_AddChoise.UseVisualStyleBackColor = true;
            this.button_AddChoise.Click += new System.EventHandler(this.button_AddChoise_Click);
            // 
            // button_ColorPicker
            // 
            this.button_ColorPicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_ColorPicker.Image = global::LabManager.Properties.Resources.coloricon;
            this.button_ColorPicker.Location = new System.Drawing.Point(668, 208);
            this.button_ColorPicker.Name = "button_ColorPicker";
            this.button_ColorPicker.Size = new System.Drawing.Size(22, 23);
            this.button_ColorPicker.TabIndex = 19;
            this.button_ColorPicker.Tag = "";
            this.button_ColorPicker.Text = "+";
            this.button_ColorPicker.UseVisualStyleBackColor = true;
            this.button_ColorPicker.Visible = false;
            this.button_ColorPicker.Click += new System.EventHandler(this.button_ColorPicker_Click);
            // 
            // comboBox_Command_SampleProgram
            // 
            this.comboBox_Command_SampleProgram.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox_Command_SampleProgram.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Command_SampleProgram.FormattingEnabled = true;
            this.comboBox_Command_SampleProgram.Items.AddRange(new object[] {
            "Var1",
            "Var2",
            "Var3",
            "Var4"});
            this.comboBox_Command_SampleProgram.Location = new System.Drawing.Point(743, 210);
            this.comboBox_Command_SampleProgram.Name = "comboBox_Command_SampleProgram";
            this.comboBox_Command_SampleProgram.Size = new System.Drawing.Size(39, 21);
            this.comboBox_Command_SampleProgram.TabIndex = 10;
            this.comboBox_Command_SampleProgram.Visible = false;
            // 
            // comboBox_CommandValue2
            // 
            this.comboBox_CommandValue2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox_CommandValue2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.comboBox_CommandValue2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox_CommandValue2.FormattingEnabled = true;
            this.comboBox_CommandValue2.Items.AddRange(new object[] {
            "",
            "<WS SampleID>",
            "<WS ParentSampleID>"});
            this.comboBox_CommandValue2.Location = new System.Drawing.Point(562, 210);
            this.comboBox_CommandValue2.Name = "comboBox_CommandValue2";
            this.comboBox_CommandValue2.Size = new System.Drawing.Size(45, 21);
            this.comboBox_CommandValue2.TabIndex = 14;
            this.comboBox_CommandValue2.Visible = false;
            // 
            // comboBox_Command_MachineList
            // 
            this.comboBox_Command_MachineList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox_Command_MachineList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Command_MachineList.Items.AddRange(new object[] {
            ""});
            this.comboBox_Command_MachineList.Location = new System.Drawing.Point(696, 210);
            this.comboBox_Command_MachineList.Name = "comboBox_Command_MachineList";
            this.comboBox_Command_MachineList.Size = new System.Drawing.Size(41, 21);
            this.comboBox_Command_MachineList.TabIndex = 13;
            this.comboBox_Command_MachineList.Visible = false;
            // 
            // numericUpDown_Commands
            // 
            this.numericUpDown_Commands.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numericUpDown_Commands.Location = new System.Drawing.Point(613, 211);
            this.numericUpDown_Commands.Maximum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            0});
            this.numericUpDown_Commands.Name = "numericUpDown_Commands";
            this.numericUpDown_Commands.Size = new System.Drawing.Size(49, 20);
            this.numericUpDown_Commands.TabIndex = 13;
            this.numericUpDown_Commands.Visible = false;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.LineColor = System.Drawing.Color.Empty;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(202, 326);
            this.treeView1.TabIndex = 0;
            // 
            // c1Ribbon
            // 
            this.c1Ribbon.ApplicationMenuHolder = this.ribbonApplicationMenu1;
            this.c1Ribbon.ConfigToolBarHolder = this.ribbonConfigToolBar1;
            this.c1Ribbon.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.c1Ribbon.Location = new System.Drawing.Point(0, 0);
            this.c1Ribbon.Name = "c1Ribbon";
            this.c1Ribbon.QatHolder = this.ribbonQat1;
            this.c1Ribbon.Size = new System.Drawing.Size(1131, 53);
            this.c1Ribbon.VisualStyle = C1.Win.C1Ribbon.VisualStyle.Office2007Blue;
            // 
            // ribbonApplicationMenu1
            // 
            this.ribbonApplicationMenu1.DropDownWidth = 177;
            this.ribbonApplicationMenu1.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonApplicationMenu1.LargeImage")));
            this.ribbonApplicationMenu1.LeftPaneItems.Add(this.ribbonButton_Expand);
            this.ribbonApplicationMenu1.LeftPaneItems.Add(this.ribbonSeparator3);
            this.ribbonApplicationMenu1.LeftPaneItems.Add(this.ribbonButton_Collapse);
            this.ribbonApplicationMenu1.LeftPaneItems.Add(this.ribbonSeparator2);
            this.ribbonApplicationMenu1.LeftPaneItems.Add(this.ribbonButton_Save);
            this.ribbonApplicationMenu1.LeftPaneItems.Add(this.ribbonSeparator5);
            this.ribbonApplicationMenu1.LeftPaneItems.Add(this.ribbonButton_RelaodRoutingData);
            this.ribbonApplicationMenu1.LeftPaneItems.Add(this.ribbonSeparator1);
            this.ribbonApplicationMenu1.LeftPaneItems.Add(this.ribbonButton_Exit);
            this.ribbonApplicationMenu1.Name = "ribbonApplicationMenu1";
            // 
            // ribbonButton_Expand
            // 
            this.ribbonButton_Expand.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton_Expand.LargeImage")));
            this.ribbonButton_Expand.Name = "ribbonButton_Expand";
            this.ribbonButton_Expand.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton_Expand.SmallImage")));
            this.ribbonButton_Expand.Text = "Expand tree";
            this.ribbonButton_Expand.Click += new System.EventHandler(this.ExpandTree_Click);
            // 
            // ribbonSeparator3
            // 
            this.ribbonSeparator3.Name = "ribbonSeparator3";
            // 
            // ribbonButton_Collapse
            // 
            this.ribbonButton_Collapse.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton_Collapse.LargeImage")));
            this.ribbonButton_Collapse.Name = "ribbonButton_Collapse";
            this.ribbonButton_Collapse.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton_Collapse.SmallImage")));
            this.ribbonButton_Collapse.Text = "Collapse tree";
            this.ribbonButton_Collapse.Click += new System.EventHandler(this.CollapseTree_Click);
            // 
            // ribbonSeparator2
            // 
            this.ribbonSeparator2.Name = "ribbonSeparator2";
            // 
            // ribbonButton_Save
            // 
            this.ribbonButton_Save.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton_Save.LargeImage")));
            this.ribbonButton_Save.Name = "ribbonButton_Save";
            this.ribbonButton_Save.Text = "Save";
            this.ribbonButton_Save.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // ribbonSeparator5
            // 
            this.ribbonSeparator5.Name = "ribbonSeparator5";
            // 
            // ribbonButton_RelaodRoutingData
            // 
            this.ribbonButton_RelaodRoutingData.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton_RelaodRoutingData.LargeImage")));
            this.ribbonButton_RelaodRoutingData.Name = "ribbonButton_RelaodRoutingData";
            this.ribbonButton_RelaodRoutingData.Text = "Relaod routing table";
            this.ribbonButton_RelaodRoutingData.Click += new System.EventHandler(this.RelaodRoutingTableData_Click);
            // 
            // ribbonSeparator1
            // 
            this.ribbonSeparator1.Name = "ribbonSeparator1";
            // 
            // ribbonButton_Exit
            // 
            this.ribbonButton_Exit.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton_Exit.LargeImage")));
            this.ribbonButton_Exit.Name = "ribbonButton_Exit";
            this.ribbonButton_Exit.Text = "Exit";
            // 
            // ribbonConfigToolBar1
            // 
            this.ribbonConfigToolBar1.Items.Add(this.ribbonCheckBox1);
            this.ribbonConfigToolBar1.Items.Add(this.ribbonTextBox_Search);
            this.ribbonConfigToolBar1.Items.Add(this.ribbonSeparator7);
            this.ribbonConfigToolBar1.Items.Add(this.ribbonNumericBox_Warning);
            this.ribbonConfigToolBar1.Items.Add(this.ribbonSeparator6);
            this.ribbonConfigToolBar1.Items.Add(this.ribbonNumericBox_Alarm);
            this.ribbonConfigToolBar1.Items.Add(this.ribbonSeparator4);
            this.ribbonConfigToolBar1.Items.Add(this.ribbonCheckBox_CheckCondition);
            this.ribbonConfigToolBar1.Name = "ribbonConfigToolBar1";
            // 
            // ribbonCheckBox1
            // 
            this.ribbonCheckBox1.Name = "ribbonCheckBox1";
            this.ribbonCheckBox1.Text = "Search";
            this.ribbonCheckBox1.CheckedChanged += new System.EventHandler(this.ribbonCheckBox1_CheckedChanged);
            // 
            // ribbonTextBox_Search
            // 
            this.ribbonTextBox_Search.Name = "ribbonTextBox_Search";
            // 
            // ribbonSeparator7
            // 
            this.ribbonSeparator7.Name = "ribbonSeparator7";
            // 
            // ribbonNumericBox_Warning
            // 
            this.ribbonNumericBox_Warning.Label = "Time before Warning";
            this.ribbonNumericBox_Warning.Name = "ribbonNumericBox_Warning";
            this.ribbonNumericBox_Warning.TextAreaWidth = 50;
            // 
            // ribbonSeparator6
            // 
            this.ribbonSeparator6.Name = "ribbonSeparator6";
            // 
            // ribbonNumericBox_Alarm
            // 
            this.ribbonNumericBox_Alarm.Label = "Time before Alarm";
            this.ribbonNumericBox_Alarm.Name = "ribbonNumericBox_Alarm";
            this.ribbonNumericBox_Alarm.TextAreaWidth = 50;
            // 
            // ribbonSeparator4
            // 
            this.ribbonSeparator4.Name = "ribbonSeparator4";
            // 
            // ribbonCheckBox_CheckCondition
            // 
            this.ribbonCheckBox_CheckCondition.Name = "ribbonCheckBox_CheckCondition";
            this.ribbonCheckBox_CheckCondition.Text = "Check Conditions";
            this.ribbonCheckBox_CheckCondition.Click += new System.EventHandler(this.ribbonCheckBox_CheckCondition_Click);
            // 
            // ribbonQat1
            // 
            this.ribbonQat1.ItemLinks.Add(this.ribbonButton_Expand);
            this.ribbonQat1.ItemLinks.Add(this.ribbonButton_Collapse);
            this.ribbonQat1.MenuVisible = false;
            this.ribbonQat1.Name = "ribbonQat1";
            // 
            // c1StatusBar_Routing
            // 
            this.c1StatusBar_Routing.Location = new System.Drawing.Point(0, 540);
            this.c1StatusBar_Routing.Name = "c1StatusBar_Routing";
            this.c1StatusBar_Routing.Size = new System.Drawing.Size(1131, 22);
            this.c1StatusBar_Routing.VisualStyle = C1.Win.C1Ribbon.VisualStyle.Office2007Blue;
            // 
            // treeView2
            // 
            this.treeView2.Dock = System.Windows.Forms.DockStyle.Right;
            this.treeView2.Location = new System.Drawing.Point(932, 53);
            this.treeView2.Name = "treeView2";
            this.treeView2.Size = new System.Drawing.Size(199, 487);
            this.treeView2.TabIndex = 14;
            this.treeView2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeView_routing1_MouseUp);
            // 
            // Routing_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(1131, 562);
            this.Controls.Add(this.treeView2);
            this.Controls.Add(this.comboBox_Operation);
            this.Controls.Add(this.comboBox_SampleType);
            this.Controls.Add(this.comboBox_MachineList);
            this.Controls.Add(this.comboBox_StatusBit_Value);
            this.Controls.Add(this.comboBox_SamplePos);
            this.Controls.Add(this.comboBox_StatusBit);
            this.Controls.Add(this.numericUpDown);
            this.Controls.Add(this.c1StatusBar_Routing);
            this.Controls.Add(this.c1Ribbon);
            this.Controls.Add(this.splitContainer1);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "Routing_Form";
            this.Text = "Routing";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Routing_Form_FormClosing);
            this.Load += new System.EventHandler(this.Routing_Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid_Conditions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Conditions)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Command)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid_Commands)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Commands)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar_Routing)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView_routing;
        private System.Windows.Forms.ImageList _imgList;
        private C1.Win.C1FlexGrid.C1FlexGrid c1FlexGrid_Conditions;
        private System.Windows.Forms.Button button_Condition_Add;
        private System.Windows.Forms.Button button_Condition_Delete;
        private System.Windows.Forms.Button button_Condition_Save;
        private System.Windows.Forms.Button button_Command_Delete;
        private System.Windows.Forms.Button button_Command_Save;
        private System.Windows.Forms.Button button_Command_Add;
        private System.Windows.Forms.ComboBox comboBox_MachineList;
        private System.Windows.Forms.NumericUpDown numericUpDown;
        private System.Windows.Forms.ComboBox comboBox_SampleType;
        private System.Windows.Forms.ComboBox comboBox_SamplePos;
        private System.Windows.Forms.ComboBox comboBox_Operation;
        private C1.Win.C1FlexGrid.C1FlexGrid c1FlexGrid_Commands;
        private System.Windows.Forms.ComboBox comboBox_Command_MachineList;
        private System.Windows.Forms.ComboBox comboBox_Command_SampleProgram;
        private System.Windows.Forms.ComboBox comboBox_StatusBit_Value;
        private System.Windows.Forms.ComboBox comboBox_StatusBit;
        private System.Windows.Forms.NumericUpDown numericUpDown_Commands;
        private System.Windows.Forms.PictureBox pictureBox_Conditions;
        private System.Windows.Forms.PictureBox pictureBox_Command;
        private System.Windows.Forms.ComboBox comboBox_CommandValue2;
        private System.Windows.Forms.ComboBox comboBox_Choice;
        private System.Windows.Forms.Button button_AddChoise;
        private System.Windows.Forms.Button button_ColorPicker;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TreeView treeView1;
        private C1.Win.C1Ribbon.C1Ribbon c1Ribbon;
        private C1.Win.C1Ribbon.RibbonApplicationMenu ribbonApplicationMenu1;
        private C1.Win.C1Ribbon.RibbonButton ribbonButton_Expand;
        private C1.Win.C1Ribbon.RibbonSeparator ribbonSeparator3;
        private C1.Win.C1Ribbon.RibbonButton ribbonButton_Collapse;
        private C1.Win.C1Ribbon.RibbonSeparator ribbonSeparator2;
        private C1.Win.C1Ribbon.RibbonButton ribbonButton_Save;
        private C1.Win.C1Ribbon.RibbonSeparator ribbonSeparator1;
        private C1.Win.C1Ribbon.RibbonButton ribbonButton_Exit;
        private C1.Win.C1Ribbon.RibbonConfigToolBar ribbonConfigToolBar1;
        private C1.Win.C1Ribbon.RibbonQat ribbonQat1;
        private C1.Win.C1Ribbon.C1StatusBar c1StatusBar_Routing;
        private C1.Win.C1Input.C1Button c1Button_Condition_up;
        private C1.Win.C1Input.C1Button c1Button_Condition_down;
        private C1.Win.C1Ribbon.RibbonSeparator ribbonSeparator5;
        private C1.Win.C1Ribbon.RibbonButton ribbonButton_RelaodRoutingData;
        private C1.Win.C1Input.C1Button c1Button_command_down;
        private C1.Win.C1Input.C1Button c1Button_command_up;
        private C1.Win.C1Ribbon.RibbonNumericBox ribbonNumericBox_Warning;
        private C1.Win.C1Ribbon.RibbonNumericBox ribbonNumericBox_Alarm;
        private C1.Win.C1Ribbon.RibbonCheckBox ribbonCheckBox_CheckCondition;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private C1.Win.C1Ribbon.RibbonSeparator ribbonSeparator6;
        private C1.Win.C1Ribbon.RibbonSeparator ribbonSeparator4;
        private C1.Win.C1Ribbon.RibbonTextBox ribbonTextBox_Search;
        private C1.Win.C1Ribbon.RibbonSeparator ribbonSeparator7;
        private C1.Win.C1Ribbon.RibbonCheckBox ribbonCheckBox1;
        private System.Windows.Forms.TreeView treeView2;
        
      
    }
}