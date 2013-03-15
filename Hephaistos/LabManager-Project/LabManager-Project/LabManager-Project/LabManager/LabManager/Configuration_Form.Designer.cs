namespace LabManager
{
    partial class Configuration_Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Configuration_Form));
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
            this.ribbonComboBox = new C1.Win.C1Ribbon.RibbonComboBox();
            this.ribbonQat1 = new C1.Win.C1Ribbon.RibbonQat();
            this.c1StatusBar_Configuration = new C1.Win.C1Ribbon.C1StatusBar();
            this.c1FlexGrid_Conf = new C1.Win.C1FlexGrid.C1FlexGrid();
            ((System.ComponentModel.ISupportInitialize)(this.c1Ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar_Configuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid_Conf)).BeginInit();
            this.SuspendLayout();
            // 
            // c1Ribbon
            // 
            this.c1Ribbon.ApplicationMenuHolder = this.ribbonApplicationMenu1;
            this.c1Ribbon.ConfigToolBarHolder = this.ribbonConfigToolBar1;
            this.c1Ribbon.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.c1Ribbon.Location = new System.Drawing.Point(0, 0);
            this.c1Ribbon.Name = "c1Ribbon";
            this.c1Ribbon.QatHolder = this.ribbonQat1;
            this.c1Ribbon.Size = new System.Drawing.Size(1111, 53);
            this.c1Ribbon.VisualStyle = C1.Win.C1Ribbon.VisualStyle.Office2007Blue;
            this.c1Ribbon.SelectedTabChanged += new System.EventHandler(this.c1Ribbon1_SelectedTabChanged);
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
            this.ribbonConfigToolBar1.Items.Add(this.ribbonComboBox);
            this.ribbonConfigToolBar1.Name = "ribbonConfigToolBar1";
            // 
            // ribbonComboBox
            // 
            this.ribbonComboBox.Label = "Table";
            this.ribbonComboBox.Name = "ribbonComboBox";
            this.ribbonComboBox.TextAreaWidth = 120;
            this.ribbonComboBox.SelectedIndexChanged += new System.EventHandler(this.ribbonComboBox_SelectedIndexChanged);
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
            this.c1StatusBar_Configuration.Location = new System.Drawing.Point(0, 523);
            this.c1StatusBar_Configuration.Name = "c1StatusBar_Configuration";
            this.c1StatusBar_Configuration.Size = new System.Drawing.Size(1111, 22);
            this.c1StatusBar_Configuration.VisualStyle = C1.Win.C1Ribbon.VisualStyle.Office2007Blue;
            // 
            // c1FlexGrid_Conf
            // 
            this.c1FlexGrid_Conf.AllowDelete = true;
            this.c1FlexGrid_Conf.AllowFiltering = true;
            this.c1FlexGrid_Conf.ColumnInfo = "10,1,0,0,0,95,Columns:";
            this.c1FlexGrid_Conf.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1FlexGrid_Conf.ExtendLastCol = true;
            this.c1FlexGrid_Conf.Location = new System.Drawing.Point(0, 53);
            this.c1FlexGrid_Conf.Name = "c1FlexGrid_Conf";
            this.c1FlexGrid_Conf.Rows.DefaultSize = 19;
            this.c1FlexGrid_Conf.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1FlexGrid_Conf.Size = new System.Drawing.Size(1111, 470);
            this.c1FlexGrid_Conf.StyleInfo = resources.GetString("c1FlexGrid_Conf.StyleInfo");
            this.c1FlexGrid_Conf.TabIndex = 2;
            this.c1FlexGrid_Conf.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2007Blue;
            this.c1FlexGrid_Conf.ChangeEdit += new System.EventHandler(this.c1FlexGrid_Conf_ChangeEdit);
            this.c1FlexGrid_Conf.OwnerDrawCell += new C1.Win.C1FlexGrid.OwnerDrawCellEventHandler(this.c1FlexGrid_Conf_OwnerDrawCell_1);
            // 
            // Configuration_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1111, 545);
            this.Controls.Add(this.c1FlexGrid_Conf);
            this.Controls.Add(this.c1StatusBar_Configuration);
            this.Controls.Add(this.c1Ribbon);
            this.MinimumSize = new System.Drawing.Size(984, 448);
            this.Name = "Configuration_Form";
            this.Text = "Configuration_Form";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Configuration_Form_FormClosing);
            this.Load += new System.EventHandler(this.Configuration_Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1Ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar_Configuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid_Conf)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1Ribbon.C1Ribbon c1Ribbon;
        private C1.Win.C1Ribbon.RibbonApplicationMenu ribbonApplicationMenu1;
        private C1.Win.C1Ribbon.RibbonConfigToolBar ribbonConfigToolBar1;
        private C1.Win.C1Ribbon.RibbonQat ribbonQat1;
        private C1.Win.C1Ribbon.RibbonButton ribbonButton_Save;
        private C1.Win.C1Ribbon.RibbonButton ribbonButton_Add;
        private C1.Win.C1Ribbon.RibbonSeparator ribbonSeparator3;
        private C1.Win.C1Ribbon.RibbonButton ribbonButton_Delete;
        private C1.Win.C1Ribbon.RibbonSeparator ribbonSeparator2;
        private C1.Win.C1Ribbon.RibbonSeparator ribbonSeparator1;
        private C1.Win.C1Ribbon.RibbonButton ribbonButton_Exit;
        private C1.Win.C1Ribbon.C1StatusBar c1StatusBar_Configuration;
        private C1.Win.C1Ribbon.RibbonComboBox ribbonComboBox;
        private C1.Win.C1FlexGrid.C1FlexGrid c1FlexGrid_Conf;
    }
}