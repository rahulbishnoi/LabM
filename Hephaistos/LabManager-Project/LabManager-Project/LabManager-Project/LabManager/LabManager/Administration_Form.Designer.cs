namespace LabManager
{
    partial class Administration_Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

      
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Administration_Form));
            this.c1FlexGrid_Administartion = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.c1Ribbon = new C1.Win.C1Ribbon.C1Ribbon();
            this.ribbonApplicationMenu1 = new C1.Win.C1Ribbon.RibbonApplicationMenu();
            this.ribbonButton_Add = new C1.Win.C1Ribbon.RibbonButton();
            this.ribbonSeparator3 = new C1.Win.C1Ribbon.RibbonSeparator();
            this.ribbonButton_Delete = new C1.Win.C1Ribbon.RibbonButton();
            this.ribbonSeparator2 = new C1.Win.C1Ribbon.RibbonSeparator();
            this.ribbonButton_Save = new C1.Win.C1Ribbon.RibbonButton();
            this.ribbonSeparator1 = new C1.Win.C1Ribbon.RibbonSeparator();
            this.ribbonButton_Exit = new C1.Win.C1Ribbon.RibbonButton();
            this.ribbonConfigToolBar1 = new C1.Win.C1Ribbon.RibbonConfigToolBar();
            this.ribbonComboBox_SQL = new C1.Win.C1Ribbon.RibbonComboBox();
            this.ribbonButton_SQLExecute = new C1.Win.C1Ribbon.RibbonButton();
            this.ribbonSeparator4 = new C1.Win.C1Ribbon.RibbonSeparator();
            this.ribbonTextBoxLimit = new C1.Win.C1Ribbon.RibbonTextBox();
            this.ribbonQat1 = new C1.Win.C1Ribbon.RibbonQat();
            this.c1StatusBar_Configuration = new C1.Win.C1Ribbon.C1StatusBar();
            this.ribbonLabel_Left = new C1.Win.C1Ribbon.RibbonLabel();
            this.treeView_DB = new System.Windows.Forms.TreeView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.imageList_Rotate = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid_Administartion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar_Configuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // c1FlexGrid_Administartion
            // 
            this.c1FlexGrid_Administartion.AllowDelete = true;
            this.c1FlexGrid_Administartion.AllowFiltering = true;
            this.c1FlexGrid_Administartion.AutoResize = true;
            this.c1FlexGrid_Administartion.ColumnInfo = "10,1,0,0,0,95,Columns:1{Style:\"TextAlign:GeneralCenter;\";}\t2{Style:\"TextAlign:Gen" +
    "eralCenter;\";}\t";
            this.c1FlexGrid_Administartion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1FlexGrid_Administartion.ExtendLastCol = true;
            this.c1FlexGrid_Administartion.Location = new System.Drawing.Point(0, 0);
            this.c1FlexGrid_Administartion.Name = "c1FlexGrid_Administartion";
            this.c1FlexGrid_Administartion.Rows.DefaultSize = 19;
            this.c1FlexGrid_Administartion.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1FlexGrid_Administartion.Size = new System.Drawing.Size(826, 519);
            this.c1FlexGrid_Administartion.StyleInfo = resources.GetString("c1FlexGrid_Administartion.StyleInfo");
            this.c1FlexGrid_Administartion.TabIndex = 5;
            this.c1FlexGrid_Administartion.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2007Blue;
            // 
            // c1Ribbon
            // 
            this.c1Ribbon.ApplicationMenuHolder = this.ribbonApplicationMenu1;
            this.c1Ribbon.ConfigToolBarHolder = this.ribbonConfigToolBar1;
            this.c1Ribbon.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.c1Ribbon.Location = new System.Drawing.Point(0, 0);
            this.c1Ribbon.Name = "c1Ribbon";
            this.c1Ribbon.QatHolder = this.ribbonQat1;
            this.c1Ribbon.Size = new System.Drawing.Size(1097, 53);
            this.c1Ribbon.VisualStyle = C1.Win.C1Ribbon.VisualStyle.Office2007Blue;
            // 
            // ribbonApplicationMenu1
            // 
            this.ribbonApplicationMenu1.DropDownWidth = 120;
            this.ribbonApplicationMenu1.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonApplicationMenu1.LargeImage")));
            this.ribbonApplicationMenu1.LeftPaneItems.Add(this.ribbonButton_Add);
            this.ribbonApplicationMenu1.LeftPaneItems.Add(this.ribbonSeparator3);
            this.ribbonApplicationMenu1.LeftPaneItems.Add(this.ribbonButton_Delete);
            this.ribbonApplicationMenu1.LeftPaneItems.Add(this.ribbonSeparator2);
            this.ribbonApplicationMenu1.LeftPaneItems.Add(this.ribbonButton_Save);
            this.ribbonApplicationMenu1.LeftPaneItems.Add(this.ribbonSeparator1);
            this.ribbonApplicationMenu1.LeftPaneItems.Add(this.ribbonButton_Exit);
            this.ribbonApplicationMenu1.Name = "ribbonApplicationMenu1";
            // 
            // ribbonButton_Add
            // 
            this.ribbonButton_Add.LargeImage = global::LabManager.Properties.Resources.plus;
            this.ribbonButton_Add.Name = "ribbonButton_Add";
            this.ribbonButton_Add.Text = "Add";
            this.ribbonButton_Add.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // ribbonSeparator3
            // 
            this.ribbonSeparator3.Name = "ribbonSeparator3";
            // 
            // ribbonButton_Delete
            // 
            this.ribbonButton_Delete.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton_Delete.LargeImage")));
            this.ribbonButton_Delete.Name = "ribbonButton_Delete";
            this.ribbonButton_Delete.Text = "Delete";
            this.ribbonButton_Delete.Click += new System.EventHandler(this.DeleteButton_Click);
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
            // ribbonSeparator1
            // 
            this.ribbonSeparator1.Name = "ribbonSeparator1";
            // 
            // ribbonButton_Exit
            // 
            this.ribbonButton_Exit.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton_Exit.LargeImage")));
            this.ribbonButton_Exit.Name = "ribbonButton_Exit";
            this.ribbonButton_Exit.Text = "Exit";
            this.ribbonButton_Exit.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // ribbonConfigToolBar1
            // 
            this.ribbonConfigToolBar1.Items.Add(this.ribbonComboBox_SQL);
            this.ribbonConfigToolBar1.Items.Add(this.ribbonButton_SQLExecute);
            this.ribbonConfigToolBar1.Items.Add(this.ribbonSeparator4);
            this.ribbonConfigToolBar1.Items.Add(this.ribbonTextBoxLimit);
            this.ribbonConfigToolBar1.Name = "ribbonConfigToolBar1";
            // 
            // ribbonComboBox_SQL
            // 
            this.ribbonComboBox_SQL.Label = "SQL Statement";
            this.ribbonComboBox_SQL.MaxLength = 2500;
            this.ribbonComboBox_SQL.Name = "ribbonComboBox_SQL";
            this.ribbonComboBox_SQL.TextAreaWidth = 800;
            // 
            // ribbonButton_SQLExecute
            // 
            this.ribbonButton_SQLExecute.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton_SQLExecute.LargeImage")));
            this.ribbonButton_SQLExecute.Name = "ribbonButton_SQLExecute";
            this.ribbonButton_SQLExecute.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton_SQLExecute.SmallImage")));
            this.ribbonButton_SQLExecute.TextImageRelation = C1.Win.C1Ribbon.TextImageRelation.ImageBeforeText;
            this.ribbonButton_SQLExecute.ToolTip = "Executes the SQL statement on the left";
            this.ribbonButton_SQLExecute.Click += new System.EventHandler(this.c1Ribbon_ribbonButton_SQLExecute_Click);
            // 
            // ribbonSeparator4
            // 
            this.ribbonSeparator4.Name = "ribbonSeparator4";
            // 
            // ribbonTextBoxLimit
            // 
            this.ribbonTextBoxLimit.Label = "Limit";
            this.ribbonTextBoxLimit.Name = "ribbonTextBoxLimit";
            // 
            // ribbonQat1
            // 
            this.ribbonQat1.ItemLinks.Add(this.ribbonButton_Add);
            this.ribbonQat1.ItemLinks.Add(this.ribbonButton_Delete);
            this.ribbonQat1.ItemLinks.Add(this.ribbonButton_Save);
            this.ribbonQat1.MenuVisible = false;
            this.ribbonQat1.Name = "ribbonQat1";
            // 
            // c1StatusBar_Configuration
            // 
            this.c1StatusBar_Configuration.LeftPaneItems.Add(this.ribbonLabel_Left);
            this.c1StatusBar_Configuration.Location = new System.Drawing.Point(0, 576);
            this.c1StatusBar_Configuration.Name = "c1StatusBar_Configuration";
            this.c1StatusBar_Configuration.Size = new System.Drawing.Size(1097, 22);
            this.c1StatusBar_Configuration.VisualStyle = C1.Win.C1Ribbon.VisualStyle.Office2007Blue;
            // 
            // ribbonLabel_Left
            // 
            this.ribbonLabel_Left.Name = "ribbonLabel_Left";
            // 
            // treeView_DB
            // 
            this.treeView_DB.BackColor = System.Drawing.Color.White;
            this.treeView_DB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeView_DB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView_DB.ImageIndex = 0;
            this.treeView_DB.ImageList = this.imageList;
            this.treeView_DB.Location = new System.Drawing.Point(0, 0);
            this.treeView_DB.Name = "treeView_DB";
            this.treeView_DB.SelectedImageIndex = 0;
            this.treeView_DB.Size = new System.Drawing.Size(261, 519);
            this.treeView_DB.TabIndex = 10;
            this.treeView_DB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView_DB_MouseDown);
            this.treeView_DB.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeView_DB_MouseUp);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "Database.ico");
            this.imageList.Images.SetKeyName(1, "Table.png");
            this.imageList.Images.SetKeyName(2, "Database-Active");
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 53);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView_DB);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.c1FlexGrid_Administartion);
            this.splitContainer1.Size = new System.Drawing.Size(1097, 523);
            this.splitContainer1.SplitterDistance = 265;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 13;
            // 
            // imageList_Rotate
            // 
            this.imageList_Rotate.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList_Rotate.ImageStream")));
            this.imageList_Rotate.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList_Rotate.Images.SetKeyName(0, "Rotate1.ico");
            this.imageList_Rotate.Images.SetKeyName(1, "Rotate2.ico");
            this.imageList_Rotate.Images.SetKeyName(2, "Rotate3.ico");
            this.imageList_Rotate.Images.SetKeyName(3, "Rotate4.ico");
            this.imageList_Rotate.Images.SetKeyName(4, "Rotate5.ico");
            this.imageList_Rotate.Images.SetKeyName(5, "Rotate6.ico");
            this.imageList_Rotate.Images.SetKeyName(6, "Rotate7.ico");
            this.imageList_Rotate.Images.SetKeyName(7, "Rotate8.ico");
            // 
            // Administration_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1097, 598);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.c1StatusBar_Configuration);
            this.Controls.Add(this.c1Ribbon);
            this.MinimumSize = new System.Drawing.Size(1113, 636);
            this.Name = "Administration_Form";
            this.Text = "Administration_Form";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Administration_Form_FormClosing);
            this.Load += new System.EventHandler(this.Administration_Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid_Administartion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar_Configuration)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1FlexGrid.C1FlexGrid c1FlexGrid_Administartion;
        private C1.Win.C1Ribbon.C1Ribbon c1Ribbon;
        private C1.Win.C1Ribbon.RibbonApplicationMenu ribbonApplicationMenu1;
        private C1.Win.C1Ribbon.RibbonButton ribbonButton_Add;
        private C1.Win.C1Ribbon.RibbonSeparator ribbonSeparator3;
        private C1.Win.C1Ribbon.RibbonButton ribbonButton_Delete;
        private C1.Win.C1Ribbon.RibbonSeparator ribbonSeparator2;
        private C1.Win.C1Ribbon.RibbonButton ribbonButton_Save;
        private C1.Win.C1Ribbon.RibbonSeparator ribbonSeparator1;
        private C1.Win.C1Ribbon.RibbonButton ribbonButton_Exit;
        private C1.Win.C1Ribbon.RibbonConfigToolBar ribbonConfigToolBar1;
        private C1.Win.C1Ribbon.RibbonQat ribbonQat1;
        private C1.Win.C1Ribbon.C1StatusBar c1StatusBar_Configuration;
        private System.Windows.Forms.TreeView treeView_DB;
        private System.Windows.Forms.ImageList imageList;
        private C1.Win.C1Ribbon.RibbonTextBox ribbonTextBoxLimit;
        private C1.Win.C1Ribbon.RibbonComboBox ribbonComboBox_SQL;
        private C1.Win.C1Ribbon.RibbonSeparator ribbonSeparator4;
        private C1.Win.C1Ribbon.RibbonButton ribbonButton_SQLExecute;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private C1.Win.C1Ribbon.RibbonLabel ribbonLabel_Left;
        private System.Windows.Forms.ImageList imageList_Rotate;
    }
}