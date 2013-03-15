using System.Windows.Forms;
using MySQL_Helper_Class;
using Logging;
using cs_IniHandlerDevelop;
using Definition;
using MySql.Data.MySqlClient;
using System.Data;
using System;
using System.Drawing;
using C1.Win.C1Ribbon;
using C1.Win.C1FlexGrid;
using System.Threading;

namespace LabManager
{
    public partial class Administration_Form : Form
    {
        MySQL_HelperClass myHC = new MySQL_HelperClass();
        Save mySave = new Save("Magazine-Configuration Form");
        IniStructure myIniHandler = new IniStructure();
        Definitions myDefinitions = new Definitions();

        MySqlDataAdapter da_Administration = new MySqlDataAdapter();
        DataSet ds_Administration = new DataSet();
        DataTable dt_Administration = null;
        TreeNode tn_LastSelected = null;
        private bool expandDBTree = true;
        private int nSQLStatementComboboxCount = 10;
        private static System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();
        private bool bLoadDataActive = false;
        private int nRotateIndex = 1;
    

        public Administration_Form()
        {
            Application.EnableVisualStyles();
            this.StartPosition = FormStartPosition.CenterScreen;

            InitializeComponent();

            string IniFilePath = myDefinitions.LanguageFile;
            myIniHandler = IniStructure.ReadIni(IniFilePath);

            this.c1FlexGrid_Administartion.DrawMode = DrawModeEnum.OwnerDraw;
            this.c1FlexGrid_Administartion.Renderer = new MyRenderer();
            this.c1FlexGrid_Administartion.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None;

            

            
            myTimer.Tick += new EventHandler(TimerEventProcessor);

            // Sets the timer interval to x milliseconds.
            myTimer.Interval = 125;
            myTimer.Start();
        }

       
        private void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            myTimer.Stop(); 
            if (bLoadDataActive)
            {
                if (nRotateIndex >= 8)
                {
                    nRotateIndex = 0;
                }
                
                try
                {
                   ribbonLabel_Left.SmallImage = imageList_Rotate.Images[nRotateIndex++];
                }
                catch {
                   
                }
               
            }
            else { ribbonLabel_Left.SmallImage = null; }
            myTimer.Start();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            myTimer.Stop();
            bLoadDataActive = false;
            myTimer.Dispose();

           

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void Administration_Form_Load(object sender, System.EventArgs e)
        {
          
            PopulateTree(treeView_DB.Nodes);

            ribbonTextBoxLimit.Text = myHC.GetUserInputDataByName(myDefinitions.AdminFormLimit, "1000");
            LoadSQLComboBox();
        }

        private void LoadMagazineConfigurationToGrid(string SQL_Statement=null)
        {
            ds_Administration.Clear();
            c1FlexGrid_Administartion.Clear();
            c1FlexGrid_Administartion.DataSource = null;
            if (SQL_Statement != null)
            {
                WriteStatusbarLeft(" try to get data from DB - please wait ...", false);
                 
                if (SQL_Statement.Length > 0)
                {
                    ds_Administration = new DataSet();
                     ribbonLabel_Left.SmallImage = imageList_Rotate.Images[1];


                    bLoadDataActive = true;
                  //  myTimer.Start();
                    da_Administration = myHC.GetAdapterFromSQLCommand(SQL_Statement);
                   
                    try
                    {
                        da_Administration.Fill(ds_Administration);


                        if (ds_Administration.Tables[0] != null)
                        {
                            if (ds_Administration.Tables[0].Rows.Count > 0)
                            {
                                dt_Administration = ds_Administration.Tables[0];
                                c1FlexGrid_Administartion.DataSource = dt_Administration;
                               
                            }
                        }
                    }
                    catch { }
                       WriteStatusbarLeft(" fetched " + ds_Administration.Tables[0].Rows.Count + " entries from DB", false);
                       bLoadDataActive = false;
                    
                }
            }

        }

        private void PopulateTree(TreeNodeCollection ParentNodes)
        {
            TreeNode tn = ParentNodes.Add("Database");
            tn.ImageIndex = tn.SelectedImageIndex = 0;

            TreeNode tn_Tables = null;

            DataSet ds_Tables = new DataSet();
            DataTable dt_Tables = null;

           // int k = 0;

            ds_Tables = myHC.GetDataSetFromSQLCommand("SHOW TABLES");

            if (ds_Tables != null)
            {
                if (ds_Tables.Tables.Count > 0)
                {
                    dt_Tables = ds_Tables.Tables[0];
                    foreach (DataRow dataRow_Table in dt_Tables.Rows) // every Table
                    {
                        tn_Tables = tn.Nodes.Add(dataRow_Table.ItemArray[0].ToString());


                        tn_Tables.ImageIndex = tn_Tables.SelectedImageIndex = 1;


                    }
                }
            }
            if (expandDBTree) { tn.ExpandAll(); } else { tn.Collapse(); }
                
        }
 
        private void LoadSQLComboBox()
        {
         
            DataSet ds_SQLs = new DataSet();
            DataTable dt_SQLs = null;

           // int k = 0;

            ds_SQLs = myHC.GetDataSetFromSQLCommand("SELECT Value from user_input_data WHERE Name LIKE '" + myDefinitions.AdminFormSQLStatement + "%'");

            if (ds_SQLs != null)
            {
                if (ds_SQLs.Tables.Count > 0)
                {
                    dt_SQLs = ds_SQLs.Tables[0];
                    foreach (DataRow dataRow_SQLs in dt_SQLs.Rows) // every SQL statement
                    {
                        ribbonComboBox_SQL.Items.Add(new RibbonButton(dataRow_SQLs.ItemArray[0].ToString()));
                    }
                }
            }
           
                
        }
 
        
        private void AddButton_Click(object sender, EventArgs e)
        {
            button_Add_Click();
            WriteStatusbarLeft(" added line", false);

        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            button_Save_Click();
            WriteStatusbarLeft(" data saved", false);

        }
      

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            button_Delete_Click();
            WriteStatusbarLeft(" deleted row(s)", false);

        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_Add_Click()
        {
            if (dt_Administration != null)
            {
                try
                {
                    DataRow dr_new = dt_Administration.Rows.Add();

                }
                catch (DBConcurrencyException DBCe)
                {
                    mySave.InsertRow((int)Definition.Message.D_DEBUG, "button_Add_Click: \r\n" + DBCe.ToString());
                }
                catch { }
                c1FlexGrid_Administartion.RowSel = c1FlexGrid_Administartion.Rows.Count - 1;

            }
        }

        private void button_Save_Click()
        {

            c1Ribbon.Focus();

            try
            {
                MySqlCommandBuilder myCommand = new MySqlCommandBuilder(da_Administration);
                da_Administration.Update(dt_Administration);
                LoadMagazineConfigurationToGrid();
            }
            catch (DBConcurrencyException DBCe)
            {
                mySave.InsertRow((int)Definition.Message.D_DEBUG, "button_Save_Click: \r\n" + DBCe.ToString());
            }
            catch (Exception ex) { mySave.InsertRow((int)Definition.Message.D_ALARM, ex.ToString()); }
        }

        private void button_Delete_Click()
        {

            if (c1FlexGrid_Administartion.RowSel >= 0)
            {
                DataRowView dr_delete = (DataRowView)c1FlexGrid_Administartion.Rows[c1FlexGrid_Administartion.RowSel].DataSource;

                string question = "Do you want to delete this row?";

                if (MessageBox.Show(question, String.Format(myIniHandler.GetValue("Admin-Machines", "Delete_Confirm")), System.Windows.Forms.MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    try
                    {
                        dr_delete.Delete();
                        MySqlCommandBuilder myCommand = new MySqlCommandBuilder(da_Administration);
                        da_Administration.Update(dt_Administration);
                        c1FlexGrid_Administartion.Update();
                    }
                    catch (DBConcurrencyException DBCe)
                    {
                        mySave.InsertRow((int)Definition.Message.D_DEBUG, "button_Delete_Click: \r\n" + DBCe.ToString());
                    }
                    catch (Exception ex) { mySave.InsertRow((int)Definition.Message.D_ALARM, ex.ToString()); }
                }
            }
        }

        private void WriteStatusbarLeft(string strText, bool bError = false)
        {
         //   c1StatusBar_Configuration.LeftPaneItems.Clear();
            ribbonLabel_Left.Text = strText;
           // ribbonLabel_Left.SmallImage = null;
            
            if (bError)
            {
                c1StatusBar_Configuration.ForeColorOuter = Color.Red;
            }
            else
            {
                c1StatusBar_Configuration.ForeColorOuter = Color.Black;
            }

        }

        private void c1Button_Save_Click(object sender, EventArgs e)
        {
            button_Save_Click();
        }

        private void treeView_DB_MouseDown(object sender, MouseEventArgs e)
        {
            if (tn_LastSelected != null) { tn_LastSelected.BackColor = treeView_DB.BackColor; tn_LastSelected.ForeColor = this.ForeColor; }
            TreeView tv = sender as TreeView;
            TreeNode tn = treeView_DB.GetNodeAt(e.Location);
            if (tn != null)
            {
                if (tn.Level == 1)
                {
                    tn.BackColor = System.Drawing.Color.Orange;
                    tn.ForeColor = System.Drawing.Color.Black;
                    tn_LastSelected = tn;
                    tv.SelectedNode = tn;
                    tv.SelectedNode.BackColor = System.Drawing.Color.Orange;

                }
                else
                {
                    tv.SelectedNode = null;
                }

                
            }
           
        }

        private void treeView_DB_MouseUp(object sender, MouseEventArgs e)
        {
            Point pt = new Point(e.X, e.Y);
            treeView_DB.PointToClient(pt);


            TreeNode Node = treeView_DB.GetNodeAt(pt);



            if (e.Button == MouseButtons.Right)
            {
                if (Node != null)
                {
                    if (Node.Level == 0)
                    {
                        if (Node.Bounds.Contains(pt))
                        {
                            treeView_DB.SelectedNode = Node;
                        }
                    }
                }
            }

            if (e.Button == MouseButtons.Left)
            {
               
                if (Node != null)
                {
                    if (Node.Level == 1) // Tables
                    {
                        string SQL_Statement = "SELECT * FROM " + Node.Text;
                        if (ribbonTextBoxLimit.Text.Length > 0)
                        {
                            SQL_Statement += " LIMIT " + ribbonTextBoxLimit.Text;
                        }
                        this.Text = "Administration - " + Node.Text;

                        LoadMagazineConfigurationToGrid(SQL_Statement);
                        
                    }
                }
            }

        }


        private void SetSQLElementsEnable(bool enable)
        {
            ribbonComboBox_SQL.Enabled = enable;
            ribbonButton_SQLExecute.Enabled = enable;
        }

        private void Administration_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            myHC.UpdateUserInputDataByName(myDefinitions.AdminFormLimit, ribbonTextBoxLimit.Text);

            for(int i=0;i<ribbonComboBox_SQL.Items.Count;i++){
                ribbonComboBox_SQL.SelectedIndex = i;
                string text = ribbonComboBox_SQL.Text;
                myHC.UpdateUserInputDataByName(myDefinitions.AdminFormSQLStatement + i.ToString(), ribbonComboBox_SQL.Text);

            }
        }

        private void c1Ribbon_ribbonButton_SQLExecute_Click(object sender, EventArgs e)
        {
            string SQL_Statement = ribbonComboBox_SQL.Text;
            
            LoadMagazineConfigurationToGrid(SQL_Statement);
            if (CheckforDuplicateStringInCombobox(SQL_Statement))
            {
                UpdateSQLStatemenComboBox(SQL_Statement);
            }
        }

        private void UpdateSQLStatemenComboBox(string strValue)
        {

            ribbonComboBox_SQL.Items.Add(strValue);
            ribbonComboBox_SQL.SelectedIndex = ribbonComboBox_SQL.SelectedIndex + 1;
            if (ribbonComboBox_SQL.Items.Count >= nSQLStatementComboboxCount)
            {
                ribbonComboBox_SQL.Items.RemoveAt(ribbonComboBox_SQL.Items.Count-1);
            }
            
        }

        private bool CheckforDuplicateStringInCombobox(string strCompare)
        {
            bool bIsInCombo = true;
            int nSel = ribbonComboBox_SQL.SelectedIndex;
            for (int i = 0; i < ribbonComboBox_SQL.Items.Count; i++)
            {
                ribbonComboBox_SQL.SelectedIndex = i;
                string text = ribbonComboBox_SQL.Text;
                if (strCompare == text) {  return false; }
            }
          //  ribbonComboBox_SQL.SelectedIndex = nSel; 
            return bIsInCombo;
        }



        class MyRenderer : C1.Win.C1FlexGrid.GridRendererOffice2007Blue
        {
            public override void OnDrawCell(C1FlexGridBase flex, OwnerDrawCellEventArgs e, C1FlexGridRenderer.CellType cellType)
            {
                try
                {

                    if (cellType == CellType.Highlight)
                    {
                        if (e.Style.Name == "Modified")
                        {
                            cellType = CellType.RowHeaderSelectedHot;

                            CellRange rg = flex.GetCellRange(e.Row, 1, e.Row, flex.Cols.Count - 1);
                            rg.Style = flex.Styles["Modified"];
                        }
                        else
                            if (e.Style.Name == "Added")
                            {
                                cellType = CellType.RowHeaderSelectedHot;

                                CellRange rg = flex.GetCellRange(e.Row, 1, e.Row, flex.Cols.Count - 1);
                                rg.Style = flex.Styles["Added"];
                            }
                            else
                                if (e.Style.Name == "Detached")
                                {
                                    cellType = CellType.RowHeaderSelectedHot;

                                    CellRange rg = flex.GetCellRange(e.Row, 1, e.Row, flex.Cols.Count - 1);
                                    rg.Style = flex.Styles["Detached"];
                                }
                                else
                                {
                                    cellType = CellType.RowHeaderSelected;
                                }
                    }

                    base.OnDrawCell(flex, e, cellType);
                }
                catch { }
            }
        }

       

      
    }
}
