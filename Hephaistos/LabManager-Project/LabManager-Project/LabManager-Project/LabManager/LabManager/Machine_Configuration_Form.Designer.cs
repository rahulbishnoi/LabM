namespace LabManager
{
    partial class Machine_Configuration_Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Machine_Configuration_Form));
            this.tabControl_Machines = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.c1FlexGrid_MachineList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.button_Condition_Save = new System.Windows.Forms.Button();
            this.button_Condition_Delete = new System.Windows.Forms.Button();
            this.button_Condition_Add = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button_machines_Save = new System.Windows.Forms.Button();
            this.c1FlexGrid_Machines = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.button_machines_Delete = new System.Windows.Forms.Button();
            this.button_machines_Add = new System.Windows.Forms.Button();
            this.tabPage9 = new System.Windows.Forms.TabPage();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.button_Group_Save = new System.Windows.Forms.Button();
            this.button_Group_Add = new System.Windows.Forms.Button();
            this.button_Group_Delete = new System.Windows.Forms.Button();
            this.c1FlexGrid_EDIT = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.treeView_Machines = new System.Windows.Forms.TreeView();
            this._imgList = new System.Windows.Forms.ImageList(this.components);
            this.tabControl_Machines.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid_MachineList)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid_Machines)).BeginInit();
            this.tabPage9.SuspendLayout();
            this.groupBox15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid_EDIT)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl_Machines
            // 
            this.tabControl_Machines.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl_Machines.Controls.Add(this.tabPage1);
            this.tabControl_Machines.Controls.Add(this.tabPage2);
            this.tabControl_Machines.Controls.Add(this.tabPage9);
            this.tabControl_Machines.Location = new System.Drawing.Point(12, 12);
            this.tabControl_Machines.Multiline = true;
            this.tabControl_Machines.Name = "tabControl_Machines";
            this.tabControl_Machines.SelectedIndex = 0;
            this.tabControl_Machines.Size = new System.Drawing.Size(1055, 572);
            this.tabControl_Machines.TabIndex = 3;
            this.tabControl_Machines.SelectedIndexChanged += new System.EventHandler(this.tabControl_Machines_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1047, 524);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "MachineList";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.c1FlexGrid_MachineList);
            this.groupBox1.Controls.Add(this.button_Condition_Save);
            this.groupBox1.Controls.Add(this.button_Condition_Delete);
            this.groupBox1.Controls.Add(this.button_Condition_Add);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1038, 512);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            // 
            // c1FlexGrid_MachineList
            // 
            this.c1FlexGrid_MachineList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c1FlexGrid_MachineList.ColumnInfo = "10,1,0,0,0,95,Columns:1{Style:\"TextAlign:GeneralCenter;\";}\t2{Style:\"TextAlign:Gen" +
    "eralCenter;\";}\t";
            this.c1FlexGrid_MachineList.ExtendLastCol = true;
            this.c1FlexGrid_MachineList.Location = new System.Drawing.Point(6, 19);
            this.c1FlexGrid_MachineList.Name = "c1FlexGrid_MachineList";
            this.c1FlexGrid_MachineList.Rows.DefaultSize = 19;
            this.c1FlexGrid_MachineList.Size = new System.Drawing.Size(1026, 458);
            this.c1FlexGrid_MachineList.StyleInfo = resources.GetString("c1FlexGrid_MachineList.StyleInfo");
            this.c1FlexGrid_MachineList.TabIndex = 3;
            this.c1FlexGrid_MachineList.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2007Black;
            this.c1FlexGrid_MachineList.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1FlexGrid_MachineList_BeforeEdit);
            this.c1FlexGrid_MachineList.GetCellErrorInfo += new C1.Win.C1FlexGrid.GetErrorInfoEventHandler(this.c1FlexGrid_MachineList_GetCellErrorInfo);
            // 
            // button_Condition_Save
            // 
            this.button_Condition_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_Condition_Save.Location = new System.Drawing.Point(121, 483);
            this.button_Condition_Save.Name = "button_Condition_Save";
            this.button_Condition_Save.Size = new System.Drawing.Size(75, 23);
            this.button_Condition_Save.TabIndex = 14;
            this.button_Condition_Save.Text = "Save";
            this.button_Condition_Save.UseVisualStyleBackColor = true;
            this.button_Condition_Save.Click += new System.EventHandler(this.button_Condition_Save_Click);
            // 
            // button_Condition_Delete
            // 
            this.button_Condition_Delete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_Condition_Delete.Location = new System.Drawing.Point(202, 483);
            this.button_Condition_Delete.Name = "button_Condition_Delete";
            this.button_Condition_Delete.Size = new System.Drawing.Size(75, 23);
            this.button_Condition_Delete.TabIndex = 13;
            this.button_Condition_Delete.Text = "Delete";
            this.button_Condition_Delete.UseVisualStyleBackColor = true;
            this.button_Condition_Delete.Click += new System.EventHandler(this.button_Condition_Delete_Click);
            // 
            // button_Condition_Add
            // 
            this.button_Condition_Add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_Condition_Add.Location = new System.Drawing.Point(40, 483);
            this.button_Condition_Add.Name = "button_Condition_Add";
            this.button_Condition_Add.Size = new System.Drawing.Size(75, 23);
            this.button_Condition_Add.TabIndex = 12;
            this.button_Condition_Add.Text = "Add";
            this.button_Condition_Add.UseVisualStyleBackColor = true;
            this.button_Condition_Add.Click += new System.EventHandler(this.button_Condition_Add_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1047, 524);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Machines";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox2.Controls.Add(this.button_machines_Save);
            this.groupBox2.Controls.Add(this.c1FlexGrid_Machines);
            this.groupBox2.Controls.Add(this.button_machines_Delete);
            this.groupBox2.Controls.Add(this.button_machines_Add);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1036, 512);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            // 
            // button_machines_Save
            // 
            this.button_machines_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_machines_Save.Location = new System.Drawing.Point(121, 483);
            this.button_machines_Save.Name = "button_machines_Save";
            this.button_machines_Save.Size = new System.Drawing.Size(75, 23);
            this.button_machines_Save.TabIndex = 17;
            this.button_machines_Save.Text = "Save";
            this.button_machines_Save.UseVisualStyleBackColor = true;
            this.button_machines_Save.Click += new System.EventHandler(this.button_machines_Save_Click);
            // 
            // c1FlexGrid_Machines
            // 
            this.c1FlexGrid_Machines.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c1FlexGrid_Machines.ColumnInfo = "10,1,0,0,0,95,Columns:1{Style:\"TextAlign:GeneralCenter;\";}\t2{Style:\"TextAlign:Gen" +
    "eralCenter;\";}\t";
            this.c1FlexGrid_Machines.ExtendLastCol = true;
            this.c1FlexGrid_Machines.Location = new System.Drawing.Point(6, 19);
            this.c1FlexGrid_Machines.Name = "c1FlexGrid_Machines";
            this.c1FlexGrid_Machines.Rows.DefaultSize = 19;
            this.c1FlexGrid_Machines.Size = new System.Drawing.Size(1024, 458);
            this.c1FlexGrid_Machines.StyleInfo = resources.GetString("c1FlexGrid_Machines.StyleInfo");
            this.c1FlexGrid_Machines.TabIndex = 3;
            this.c1FlexGrid_Machines.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2007Black;
            // 
            // button_machines_Delete
            // 
            this.button_machines_Delete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_machines_Delete.Location = new System.Drawing.Point(202, 483);
            this.button_machines_Delete.Name = "button_machines_Delete";
            this.button_machines_Delete.Size = new System.Drawing.Size(75, 23);
            this.button_machines_Delete.TabIndex = 16;
            this.button_machines_Delete.Text = "Delete";
            this.button_machines_Delete.UseVisualStyleBackColor = true;
            this.button_machines_Delete.Click += new System.EventHandler(this.button_machines_Delete_Click);
            // 
            // button_machines_Add
            // 
            this.button_machines_Add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_machines_Add.Location = new System.Drawing.Point(40, 483);
            this.button_machines_Add.Name = "button_machines_Add";
            this.button_machines_Add.Size = new System.Drawing.Size(75, 23);
            this.button_machines_Add.TabIndex = 15;
            this.button_machines_Add.Text = "Add";
            this.button_machines_Add.UseVisualStyleBackColor = true;
            this.button_machines_Add.Click += new System.EventHandler(this.button_machines_Add_Click);
            // 
            // tabPage9
            // 
            this.tabPage9.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage9.Controls.Add(this.groupBox15);
            this.tabPage9.Location = new System.Drawing.Point(4, 22);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Size = new System.Drawing.Size(1047, 546);
            this.tabPage9.TabIndex = 8;
            this.tabPage9.Text = "MachineOptions";
            // 
            // groupBox15
            // 
            this.groupBox15.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox15.Controls.Add(this.button_Group_Save);
            this.groupBox15.Controls.Add(this.button_Group_Add);
            this.groupBox15.Controls.Add(this.button_Group_Delete);
            this.groupBox15.Controls.Add(this.c1FlexGrid_EDIT);
            this.groupBox15.Controls.Add(this.treeView_Machines);
            this.groupBox15.Location = new System.Drawing.Point(0, 3);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new System.Drawing.Size(1046, 542);
            this.groupBox15.TabIndex = 8;
            this.groupBox15.TabStop = false;
            // 
            // button_Group_Save
            // 
            this.button_Group_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_Group_Save.Location = new System.Drawing.Point(378, 513);
            this.button_Group_Save.Name = "button_Group_Save";
            this.button_Group_Save.Size = new System.Drawing.Size(75, 23);
            this.button_Group_Save.TabIndex = 29;
            this.button_Group_Save.Text = "Save";
            this.button_Group_Save.UseVisualStyleBackColor = true;
            this.button_Group_Save.Click += new System.EventHandler(this.button_Group_Save_Click);
            // 
            // button_Group_Add
            // 
            this.button_Group_Add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_Group_Add.Location = new System.Drawing.Point(297, 513);
            this.button_Group_Add.Name = "button_Group_Add";
            this.button_Group_Add.Size = new System.Drawing.Size(75, 23);
            this.button_Group_Add.TabIndex = 27;
            this.button_Group_Add.Text = "Add";
            this.button_Group_Add.UseVisualStyleBackColor = true;
            this.button_Group_Add.Click += new System.EventHandler(this.button_Group_Add_Click);
            // 
            // button_Group_Delete
            // 
            this.button_Group_Delete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_Group_Delete.Location = new System.Drawing.Point(459, 513);
            this.button_Group_Delete.Name = "button_Group_Delete";
            this.button_Group_Delete.Size = new System.Drawing.Size(75, 23);
            this.button_Group_Delete.TabIndex = 28;
            this.button_Group_Delete.Text = "Delete";
            this.button_Group_Delete.UseVisualStyleBackColor = true;
            this.button_Group_Delete.Click += new System.EventHandler(this.button_Group_Delete_Click);
            // 
            // c1FlexGrid_EDIT
            // 
            this.c1FlexGrid_EDIT.AllowFiltering = true;
            this.c1FlexGrid_EDIT.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c1FlexGrid_EDIT.ColumnInfo = resources.GetString("c1FlexGrid_EDIT.ColumnInfo");
            this.c1FlexGrid_EDIT.ExtendLastCol = true;
            this.c1FlexGrid_EDIT.Location = new System.Drawing.Point(262, 13);
            this.c1FlexGrid_EDIT.Name = "c1FlexGrid_EDIT";
            this.c1FlexGrid_EDIT.Rows.DefaultSize = 19;
            this.c1FlexGrid_EDIT.Size = new System.Drawing.Size(778, 494);
            this.c1FlexGrid_EDIT.StyleInfo = resources.GetString("c1FlexGrid_EDIT.StyleInfo");
            this.c1FlexGrid_EDIT.TabIndex = 7;
            this.c1FlexGrid_EDIT.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2007Black;
            this.c1FlexGrid_EDIT.GetCellErrorInfo += new C1.Win.C1FlexGrid.GetErrorInfoEventHandler(this.c1FlexGrid_EDIT_GetCellErrorInfo);
            // 
            // treeView_Machines
            // 
            this.treeView_Machines.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeView_Machines.BackColor = System.Drawing.SystemColors.Control;
            this.treeView_Machines.ImageIndex = 0;
            this.treeView_Machines.ImageList = this._imgList;
            this.treeView_Machines.Location = new System.Drawing.Point(6, 13);
            this.treeView_Machines.Name = "treeView_Machines";
            this.treeView_Machines.SelectedImageIndex = 0;
            this.treeView_Machines.Size = new System.Drawing.Size(250, 523);
            this.treeView_Machines.TabIndex = 0;
            this.treeView_Machines.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView_Machines.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView_Machines_MouseDown);
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
            // 
            // Machine_Configuration_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1079, 596);
            this.Controls.Add(this.tabControl_Machines);
            this.Name = "Machine_Configuration_Form";
            this.Text = "Machine_Configuration_Form";
            this.Load += new System.EventHandler(this.Machine_Configuration_Form_Load);
            this.tabControl_Machines.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid_MachineList)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid_Machines)).EndInit();
            this.tabPage9.ResumeLayout(false);
            this.groupBox15.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid_EDIT)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl_Machines;
        private System.Windows.Forms.TabPage tabPage1;
        private C1.Win.C1FlexGrid.C1FlexGrid c1FlexGrid_MachineList;
        private System.Windows.Forms.TabPage tabPage2;
        private C1.Win.C1FlexGrid.C1FlexGrid c1FlexGrid_Machines;
        private System.Windows.Forms.Button button_Condition_Save;
        private System.Windows.Forms.Button button_Condition_Delete;
        private System.Windows.Forms.Button button_Condition_Add;
        private System.Windows.Forms.Button button_machines_Save;
        private System.Windows.Forms.Button button_machines_Delete;
        private System.Windows.Forms.Button button_machines_Add;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ImageList _imgList;
        private System.Windows.Forms.TabPage tabPage9;
        private System.Windows.Forms.GroupBox groupBox15;
        private C1.Win.C1FlexGrid.C1FlexGrid c1FlexGrid_EDIT;
        private System.Windows.Forms.TreeView treeView_Machines;
        private System.Windows.Forms.Button button_Group_Save;
        private System.Windows.Forms.Button button_Group_Add;
        private System.Windows.Forms.Button button_Group_Delete;
    }
}