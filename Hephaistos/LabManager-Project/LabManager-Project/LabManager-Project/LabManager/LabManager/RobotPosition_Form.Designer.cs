namespace LabManager
{
    partial class RobotPosition_Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RobotPosition_Form));
            this.textBoxCommand = new System.Windows.Forms.TextBox();
            this.button_Send = new System.Windows.Forms.Button();
            this.c1FlexGridFrom = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.c1FlexGridTo = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label_from = new System.Windows.Forms.Label();
            this.label_to = new System.Windows.Forms.Label();
            this.comboMagPosFrom = new System.Windows.Forms.ComboBox();
            this.comboMagPosTo = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBox_EditCommand = new System.Windows.Forms.CheckBox();
            this.c1StatusBar = new C1.Win.C1Ribbon.C1StatusBar();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.miFile = new System.Windows.Forms.MenuItem();
            this.miExit = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGridFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGridTo)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxCommand
            // 
            this.textBoxCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCommand.Location = new System.Drawing.Point(12, 41);
            this.textBoxCommand.Multiline = true;
            this.textBoxCommand.Name = "textBoxCommand";
            this.textBoxCommand.Size = new System.Drawing.Size(717, 53);
            this.textBoxCommand.TabIndex = 0;
            // 
            // button_Send
            // 
            this.button_Send.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button_Send.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Send.Location = new System.Drawing.Point(735, 41);
            this.button_Send.Name = "button_Send";
            this.button_Send.Size = new System.Drawing.Size(75, 53);
            this.button_Send.TabIndex = 1;
            this.button_Send.Text = "Send";
            this.button_Send.UseVisualStyleBackColor = true;
            this.button_Send.Click += new System.EventHandler(this.button_Send_Click);
            // 
            // c1FlexGridFrom
            // 
            this.c1FlexGridFrom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c1FlexGridFrom.AutoResize = true;
            this.c1FlexGridFrom.ColumnInfo = "10,1,0,0,0,95,Columns:";
            this.c1FlexGridFrom.Location = new System.Drawing.Point(18, 25);
            this.c1FlexGridFrom.Name = "c1FlexGridFrom";
            this.c1FlexGridFrom.Rows.DefaultSize = 19;
            this.c1FlexGridFrom.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1FlexGridFrom.Size = new System.Drawing.Size(312, 301);
            this.c1FlexGridFrom.StyleInfo = resources.GetString("c1FlexGridFrom.StyleInfo");
            this.c1FlexGridFrom.TabIndex = 3;
            this.c1FlexGridFrom.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2007Black;
            this.c1FlexGridFrom.Click += new System.EventHandler(this.c1FlexGridFrom_Click);
            // 
            // c1FlexGridTo
            // 
            this.c1FlexGridTo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c1FlexGridTo.AutoResize = true;
            this.c1FlexGridTo.ColumnInfo = "10,1,0,0,0,95,Columns:";
            this.c1FlexGridTo.Location = new System.Drawing.Point(3, 25);
            this.c1FlexGridTo.Name = "c1FlexGridTo";
            this.c1FlexGridTo.Rows.DefaultSize = 19;
            this.c1FlexGridTo.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1FlexGridTo.Size = new System.Drawing.Size(300, 301);
            this.c1FlexGridTo.StyleInfo = resources.GetString("c1FlexGridTo.StyleInfo");
            this.c1FlexGridTo.TabIndex = 4;
            this.c1FlexGridTo.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2007Black;
            this.c1FlexGridTo.Click += new System.EventHandler(this.c1FlexGridTo_Click);
            // 
            // label_from
            // 
            this.label_from.AutoSize = true;
            this.label_from.Location = new System.Drawing.Point(15, 9);
            this.label_from.Name = "label_from";
            this.label_from.Size = new System.Drawing.Size(27, 13);
            this.label_from.TabIndex = 5;
            this.label_from.Text = "from";
            // 
            // label_to
            // 
            this.label_to.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_to.AutoSize = true;
            this.label_to.Location = new System.Drawing.Point(7, 10);
            this.label_to.Name = "label_to";
            this.label_to.Size = new System.Drawing.Size(16, 13);
            this.label_to.TabIndex = 6;
            this.label_to.Text = "to";
            // 
            // comboMagPosFrom
            // 
            this.comboMagPosFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboMagPosFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.comboMagPosFrom.FormattingEnabled = true;
            this.comboMagPosFrom.Location = new System.Drawing.Point(336, 20);
            this.comboMagPosFrom.Name = "comboMagPosFrom";
            this.comboMagPosFrom.Size = new System.Drawing.Size(69, 306);
            this.comboMagPosFrom.TabIndex = 7;
            this.comboMagPosFrom.SelectedIndexChanged += new System.EventHandler(this.comboMagPosFrom_SelectedIndexChanged);
            this.comboMagPosFrom.Click += new System.EventHandler(this.comboMagPosFrom_Click);
            // 
            // comboMagPosTo
            // 
            this.comboMagPosTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboMagPosTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.comboMagPosTo.FormattingEnabled = true;
            this.comboMagPosTo.Location = new System.Drawing.Point(309, 20);
            this.comboMagPosTo.Name = "comboMagPosTo";
            this.comboMagPosTo.Size = new System.Drawing.Size(64, 306);
            this.comboMagPosTo.TabIndex = 8;
            this.comboMagPosTo.SelectedIndexChanged += new System.EventHandler(this.comboMagPosTo_SelectedIndexChanged);
            this.comboMagPosTo.Click += new System.EventHandler(this.comboMagPosTo_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.splitContainer2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(810, 358);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Positions";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.Location = new System.Drawing.Point(6, 16);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer2.Panel1.Controls.Add(this.label_from);
            this.splitContainer2.Panel1.Controls.Add(this.c1FlexGridFrom);
            this.splitContainer2.Panel1.Controls.Add(this.comboMagPosFrom);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer2.Panel2.Controls.Add(this.label_to);
            this.splitContainer2.Panel2.Controls.Add(this.c1FlexGridTo);
            this.splitContainer2.Panel2.Controls.Add(this.comboMagPosTo);
            this.splitContainer2.Size = new System.Drawing.Size(798, 336);
            this.splitContainer2.SplitterDistance = 412;
            this.splitContainer2.TabIndex = 9;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.checkBox_EditCommand);
            this.groupBox2.Controls.Add(this.button_Send);
            this.groupBox2.Controls.Add(this.textBoxCommand);
            this.groupBox2.Location = new System.Drawing.Point(12, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(819, 114);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Command";
            // 
            // checkBox_EditCommand
            // 
            this.checkBox_EditCommand.AutoSize = true;
            this.checkBox_EditCommand.Location = new System.Drawing.Point(12, 19);
            this.checkBox_EditCommand.Name = "checkBox_EditCommand";
            this.checkBox_EditCommand.Size = new System.Drawing.Size(92, 17);
            this.checkBox_EditCommand.TabIndex = 2;
            this.checkBox_EditCommand.Text = "edit command";
            this.checkBox_EditCommand.UseVisualStyleBackColor = true;
            this.checkBox_EditCommand.Click += new System.EventHandler(this.checkBox_EditCommand_Click);
            // 
            // c1StatusBar
            // 
            this.c1StatusBar.Location = new System.Drawing.Point(0, 508);
            this.c1StatusBar.Name = "c1StatusBar";
            this.c1StatusBar.Size = new System.Drawing.Size(838, 22);
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miFile,
            this.menuItem1});
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
            // menuItem1
            // 
            this.menuItem1.Index = 1;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem2,
            this.menuItem3});
            this.menuItem1.Text = "Extras";
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 0;
            this.menuItem2.Text = "Read Positions";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 1;
            this.menuItem3.Text = "Drive all Points";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel2MinSize = 150;
            this.splitContainer1.Size = new System.Drawing.Size(838, 530);
            this.splitContainer1.SplitterDistance = 377;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 11;
            // 
            // RobotPosition_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 530);
            this.Controls.Add(this.c1StatusBar);
            this.Controls.Add(this.splitContainer1);
            this.Menu = this.mainMenu1;
            this.Name = "RobotPosition_Form";
            this.Text = "RobotAdmin_Form";
            this.Load += new System.EventHandler(this.RobotAdmin_Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGridFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGridTo)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxCommand;
        private System.Windows.Forms.Button button_Send;
        private C1.Win.C1FlexGrid.C1FlexGrid c1FlexGridFrom;
        private C1.Win.C1FlexGrid.C1FlexGrid c1FlexGridTo;
        private System.Windows.Forms.Label label_from;
        private System.Windows.Forms.Label label_to;
        private System.Windows.Forms.ComboBox comboMagPosFrom;
        private System.Windows.Forms.ComboBox comboMagPosTo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBox_EditCommand;
        private C1.Win.C1Ribbon.C1StatusBar c1StatusBar;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem miFile;
        private System.Windows.Forms.MenuItem miExit;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
    }
}