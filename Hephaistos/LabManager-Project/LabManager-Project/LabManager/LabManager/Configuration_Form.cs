using System.Windows.Forms;
using System.Data;
using C1.Win.C1Ribbon;
using MySQL_Helper_Class;
using MySql.Data.MySqlClient;
using System.Collections.Specialized;
using System;
using C1.Win.C1FlexGrid;
using System.Drawing;
using Logging;
using FlexGridHelper;

namespace LabManager
{
    public partial class Configuration_Form : Form
    {
       private MySQL_HelperClass myHC = new MySQL_HelperClass();
       private string strMenueTabName = null;
       private bool bDataLoaded = false;
       private MySqlDataAdapter da_Conf = new MySqlDataAdapter();
       private DataSet ds_Conf = new DataSet();
       private DataTable dt_Conf = null;
       private Save mySave = new Save("Configuration Form");
       private string strSectedTableName = null;
       private string strLoadSQLCommand = null;
       private bool bTableChanged = false;

        public Configuration_Form(string strMenueTabName)
        {
            Application.EnableVisualStyles();
            this.StartPosition = FormStartPosition.CenterScreen;

            InitializeComponent();

            this.strMenueTabName = strMenueTabName;

            CellStyle cs_Boolean = c1FlexGrid_Conf.Styles.Add("Boolean");
            cs_Boolean.DataType = typeof(Boolean);
            cs_Boolean.ImageAlign = ImageAlignEnum.CenterCenter;

            CellStyle cs_Numeric = c1FlexGrid_Conf.Styles.Add("Numeric");
            C1.Win.C1Input.C1NumericEdit numEdit = new C1.Win.C1Input.C1NumericEdit();
            numEdit.FormatType = C1.Win.C1Input.FormatTypeEnum.Integer;
            numEdit.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.UpDown;
            cs_Numeric.Editor = numEdit;

        //    c1FlexGrid_Conf.DataSource = null;
         //   c1FlexGrid_Conf.Clear();
       //     c1FlexGrid_Conf.Cols[0].Width = 20;


            CellStyle cs = c1FlexGrid_Conf.Styles.Add("Added");
            cs.BackColor = SystemColors.Info;
            cs.Font = new Font(c1FlexGrid_Conf.Font, FontStyle.Bold);

            cs = c1FlexGrid_Conf.Styles.Add("Detached");
            cs.BackColor = SystemColors.Info;
            cs.ForeColor = Color.DarkGray;

            cs = c1FlexGrid_Conf.Styles.Add("Modified");
            cs.BackColor = Color.Gold;
            cs.Font = new Font(c1FlexGrid_Conf.Font, FontStyle.Bold);

            cs = c1FlexGrid_Conf.Styles.Add("NotEditable");
            cs.ForeColor = Color.Gray;
              
            this.c1FlexGrid_Conf.DrawMode = DrawModeEnum.OwnerDraw;
            this.c1FlexGrid_Conf.Renderer = new MyRenderer();
            this.c1FlexGrid_Conf.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None;

          
          
        }

        private void Configuration_Form_Load(object sender, EventArgs e)
        {

            LoadTabs();

            LoadConfigurationToGrid(strLoadSQLCommand);
            bDataLoaded = true;
        }

      
        private void LoadTabs()
        {
            string SQL_Statement = "SELECT idconfiguration_tables,Name,TableName,SQLLoadCommand FROM configuration_tables ORDER BY Name ASC";

            DataSet ds_Configuations = myHC.GetDataSetFromSQLCommand(SQL_Statement);
            DataTable dt_Configuratons = new DataTable();
            string strTabName = null;


            if (ds_Configuations.Tables.Count > 0)
            {
                if (ds_Configuations.Tables[0] != null)
                {
                    dt_Configuratons = ds_Configuations.Tables[0];

                    foreach (DataRow dr_configuration in dt_Configuratons.Rows)
                    {
                        if (dr_configuration.ItemArray[1].ToString().Length > 0)
                        { 
                            strTabName = dr_configuration.ItemArray[1].ToString();
                        }
                        else
                        {
                            strTabName = dr_configuration.ItemArray[2].ToString();
                        }

                       
                        // 
                        try
                        {
                            RibbonTab tab = c1Ribbon.Tabs.Add(strTabName);
                            tab.Tag = dr_configuration;
                            RibbonItem i = ribbonComboBox.Items.Add(strTabName);
                            i.Tag = dr_configuration;

                            if (dr_configuration.ItemArray[1].ToString() == strMenueTabName)
                            {
                                c1Ribbon.SelectedTab = tab;
                            }
                        }
                        catch { }

                        try //load default SQLCOmmand for Startup
                        {
                            if (dr_configuration.ItemArray[1].ToString() == strMenueTabName)
                            {
                                strSectedTableName = dr_configuration.ItemArray[2].ToString();
                                if (dr_configuration.ItemArray.Length >= 4)
                                {
                                    strLoadSQLCommand = dr_configuration.ItemArray[3].ToString();
                                }
                            }
                        }
                        catch { }
                    }
                }
            }
         
           
        }

        // invalidate grid after editing new rows
        // (so the Added style is visible right away)
        private void _drChanged(object sender, DataRowChangeEventArgs e)
        {
            //if (e.Action == DataRowAction.Add)
                c1FlexGrid_Conf.Invalidate();
        }

        public void LoadConfigurationToGrid(string SQL_Statement=null)
        {

          
            c1FlexGrid_Conf.Clear();

            
            if (SQL_Statement != null)
            {
                ds_Conf = new DataSet();
                try
                {
                    da_Conf = myHC.GetAdapterFromSQLCommand(SQL_Statement);
                    da_Conf.ContinueUpdateOnError = true;
                    da_Conf.FillLoadOption = LoadOption.OverwriteChanges;
                    ds_Conf.Clear();
                    c1FlexGrid_Conf.Clear();
                    da_Conf.Fill(ds_Conf);
                }
                catch (Exception ex) { mySave.InsertRow((int)Definition.Message.D_ALARM, ex.ToString()); }

                if (ds_Conf.Tables.Count > 0)
                {
                    if (ds_Conf.Tables[0] != null)
                    {
                         if (ds_Conf.Tables[0].Rows.Count > 0)
                        {
                            dt_Conf = ds_Conf.Tables[0];

                           
                            dt_Conf.RowChanged += new DataRowChangeEventHandler(_drChanged);

                          
                            c1FlexGrid_Conf.DataSource = dt_Conf;
                            c1FlexGrid_Conf.ExtendLastCol = true;
                          
                            // load the Boolean columns
                            SetBooleanValuesInConfTable();
                            // set the Numeric fields
                            SetNumericValuesInConfTable();
                            // set hidden fields
                            SetHiddenFieldsInConfTable();
                            // set up list dictionarys
                            SetListDictionarysInConfTable();
                            // set the NotEditable fields
                            SetNotEditableFieldsInConfTable();
                             
                           c1FlexGrid_Conf.AutoResize = true;
                               // get n pixel space to the right of each column
                           c1FlexGrid_Conf.AutoSizeCols(15);
                              c1FlexGrid_Conf.Update();

                               
                               
                        }
                    }
                }
            }
        }


        private void SetBooleanValuesInConfTable()
        {
            string SQL_Statement = "SELECT TableColumn,Description FROM editor_table WHERE EditorTableListType='StyleBool' AND TableNameReference='" + strSectedTableName  + "'";

            DataSet ds_Boolean = myHC.GetDataSetFromSQLCommand(SQL_Statement);
            DataTable dt_Boolean = new DataTable();

            if (ds_Boolean != null)
            {
                if (ds_Boolean.Tables.Count > 0)
                {
                    if (ds_Boolean.Tables[0] != null)
                    {
                        dt_Boolean = ds_Boolean.Tables[0];

                        foreach (DataRow dr_Boolean in dt_Boolean.Rows)
                        {
                            if (dr_Boolean.ItemArray[0].ToString().Length > 0)
                            {
                                c1FlexGrid_Conf.Cols[dr_Boolean.ItemArray[0].ToString()].Style = c1FlexGrid_Conf.Styles["Boolean"];
                            }
                        }
                    }
                }
            }
        }

        private void SetNumericValuesInConfTable()
        {
            string SQL_Statement = "SELECT TableColumn,Description FROM editor_table WHERE EditorTableListType='StyleNumeric' AND TableNameReference='" + strSectedTableName + "'";

            DataSet ds_Numeric = myHC.GetDataSetFromSQLCommand(SQL_Statement);
            DataTable dt_Numeric = new DataTable();

            if (ds_Numeric != null)
            {
                if (ds_Numeric.Tables.Count > 0)
                {
                    if (ds_Numeric.Tables[0] != null)
                    {
                        dt_Numeric = ds_Numeric.Tables[0];

                        foreach (DataRow dr_Numeric in dt_Numeric.Rows)
                        {
                            if (dr_Numeric.ItemArray[0].ToString().Length > 0)
                            {
                                c1FlexGrid_Conf.Cols[dr_Numeric.ItemArray[0].ToString()].Style = c1FlexGrid_Conf.Styles["Numeric"];
                            }
                        }
                    }
                }
            }
        }

        private void SetHiddenFieldsInConfTable()
        {
            string SQL_Statement = "SELECT TableColumn,Description FROM editor_table WHERE EditorTableListType='Hidden' AND TableNameReference='" + strSectedTableName + "'";

            DataSet ds_Hidden = myHC.GetDataSetFromSQLCommand(SQL_Statement);
            DataTable dt_Hidden = new DataTable();

            if (ds_Hidden != null)
            {
                if (ds_Hidden.Tables.Count > 0)
                {
                    if (ds_Hidden.Tables[0] != null)
                    {
                        dt_Hidden = ds_Hidden.Tables[0];

                        foreach (DataRow dr_Hidden in dt_Hidden.Rows)
                        {
                            if (dr_Hidden.ItemArray[0].ToString().Length > 0)
                            {
                                c1FlexGrid_Conf.Cols[dr_Hidden.ItemArray[0].ToString()].Visible = false;
                            }
                        }
                    }
                }
            }
        }

        private void SetListDictionarysInConfTable()
        {
            string SQL_Statement = "SELECT TableColumn,CommaSeparatedListNames,CommaSeparatedListValues,SQL_Command,Description FROM editor_table WHERE EditorTableListType='ListDictionary' AND TableNameReference='" + strSectedTableName + "'";

            DataSet ds_ListDictionary = myHC.GetDataSetFromSQLCommand(SQL_Statement);
            DataTable dt_ListDictionary = new DataTable();

            if (ds_ListDictionary != null)
            {
                if (ds_ListDictionary.Tables.Count > 0)
                {
                    if (ds_ListDictionary.Tables[0] != null)
                    {
                        dt_ListDictionary = ds_ListDictionary.Tables[0];

                        foreach (DataRow dr_ListDictionary in dt_ListDictionary.Rows)
                        {
                            if (dr_ListDictionary.ItemArray[0].ToString().Length > 0)
                            {
                                c1FlexGrid_Conf.Cols[dr_ListDictionary.ItemArray[0].ToString()].DataMap = GetListDictionary(dr_ListDictionary.ItemArray[3].ToString(), dr_ListDictionary.ItemArray[1].ToString(), dr_ListDictionary.ItemArray[2].ToString());
                            }
                        }
                    }
                }
            }
        }

        private ListDictionary GetListDictionary(string SQL_Statement, string CommaSeparatedListNames = null, string CommaSeparatedListValues=null)
        {
            // load  Dictionary
            ListDictionary ld_temp = null;
            ld_temp = new ListDictionary();
           
            if (CommaSeparatedListNames != null && CommaSeparatedListValues != null)
            {
                string[] nSplittedListNames = CommaSeparatedListNames.Split(',');
                string[] nSplittedListValues = CommaSeparatedListValues.Split(',');

                if (nSplittedListNames.Length > 0 && nSplittedListValues.Length > 0)
                {
                    for (int i = 0; i < nSplittedListNames.Length; i++)
                    {
                        int nIntValue = -1;
                        bool bParsed = Int32.TryParse(nSplittedListValues[i], out nIntValue);
                        if (bParsed)
                        {
                            ld_temp.Add((int)nIntValue, nSplittedListNames[i]);
                        }
                        else
                        {
                            ld_temp.Add((object)nSplittedListValues[i], nSplittedListNames[i]);
                        }
                    }
                }
            }

            DataSet ds_List = new DataSet();
            ds_List.Clear();
            if (SQL_Statement.Length > 0)
            {
                ds_List = myHC.GetDataSetFromSQLCommand(SQL_Statement);
               

                if (ds_List != null && ds_List.Tables.Count > 0)
                {
                    if (ds_List != null)
                    {
                        if (ds_List.Tables[0] != null)
                        {
                            if (ds_List.Tables[0].Rows.Count > 0)
                            {
                                DataTable dt_List = ds_List.Tables[0];

                                foreach (DataRow dr_List in dt_List.Rows)
                                {
                                    if (dr_List.ItemArray.Length > 1)
                                    { //   Value, Name
                                        ld_temp.Add((object)dr_List.ItemArray[0], dr_List.ItemArray[1]);
                                    }
                                    else // if only one column is given like on the "show Tables" command
                                    {
                                        ld_temp.Add((object)dr_List.ItemArray[0], dr_List.ItemArray[0]);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return ld_temp;
        }

        private void SetNotEditableFieldsInConfTable()
        {
            string SQL_Statement = "SELECT TableColumn,Description FROM editor_table WHERE EditorTableListType='NotEditable' AND TableNameReference='" + strSectedTableName + "'";

            DataSet ds_NotEditable = myHC.GetDataSetFromSQLCommand(SQL_Statement);
            DataTable dt_NotEditable = new DataTable();

            if (ds_NotEditable != null)
            {
                if (ds_NotEditable.Tables.Count > 0)
                {
                    if (ds_NotEditable.Tables[0] != null)
                    {
                        dt_NotEditable = ds_NotEditable.Tables[0];

                        foreach (DataRow dr_NotEditable in dt_NotEditable.Rows)
                        {
                            if (dr_NotEditable.ItemArray[0].ToString().Length > 0)
                            {
                                c1FlexGrid_Conf.Cols[dr_NotEditable.ItemArray[0].ToString()].AllowEditing = false;//.Style = c1FlexGrid_Conf.Styles["NotEditable"]; ;
                                if (c1FlexGrid_Conf.Cols[dr_NotEditable.ItemArray[0].ToString()].Style.DataType != typeof(Boolean))
                                {
                                    c1FlexGrid_Conf.Cols[dr_NotEditable.ItemArray[0].ToString()].Style = c1FlexGrid_Conf.Styles["NotEditable"];
                                }
                            }
                        }
                    }
                }
            }
        }
        private void AddButton_Click(object sender, EventArgs e)
        {
            button_Add_Click();
            
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            button_Delete_Click();
                WriteStatusbarLeft(" deleted row(s)", false);
            
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
           button_Save_Click();
                WriteStatusbarLeft(" data saved",  false);
           
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
                     
             this.Close();
            
        }

        private void WriteStatusbarLeft(string strText, bool bError = false)
        {
            c1StatusBar_Configuration.LeftPaneItems.Clear();
            c1StatusBar_Configuration.LeftPaneItems.Add(strText);
            if (bError)
            {
                c1StatusBar_Configuration.ForeColorOuter = Color.Red;
            }
            else
            {
                c1StatusBar_Configuration.ForeColorOuter = Color.Black;
            }

        }

       
        private void c1Ribbon1_SelectedTabChanged(object sender, EventArgs e)
        {
           C1Ribbon ribbon = sender as C1Ribbon;

           if (bDataLoaded)
                {
                    if (AskForDataChanged() != 3)
                    {
                        int nTabIndex = ribbon.SelectedTabIndex;
                        DataRow dr_Tab = ribbon.Tabs[nTabIndex].Tag as DataRow;

                        try
                        {
                            strSectedTableName = dr_Tab.ItemArray[2].ToString();
                            LoadConfigurationToGrid(dr_Tab.ItemArray[3].ToString());
                        }
                        catch { }

                        this.Text = "Configuration Form";
                        if (dr_Tab.ItemArray[1].ToString().Length > 0)
                        { this.Text += " - " + dr_Tab.ItemArray[1].ToString(); }
                    }
                  
                }
                
        }

        private void ribbonComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            RibbonComboBox box = sender as RibbonComboBox;

            if (bDataLoaded)
            {

                DataRow dr_Box = box.SelectedItem.Tag as DataRow;

                try
                {
                    strSectedTableName = dr_Box.ItemArray[2].ToString();
                    LoadConfigurationToGrid(dr_Box.ItemArray[3].ToString());
                    c1Ribbon.SelectedTabIndex = box.SelectedIndex;
                }
                catch { }


                this.Text = "Configuration Form";
                if (dr_Box.ItemArray[1].ToString().Length > 0)
                { this.Text += " - " + dr_Box.ItemArray[1].ToString(); }

            }
        }

        private void button_Add_Click()
        {
            if (dt_Conf != null)
            {
                try
                {
                    DataRow dr_new = dt_Conf.Rows.Add();

                }
                catch (DBConcurrencyException DBCe)
                {
                    mySave.InsertRow((int)Definition.Message.D_DEBUG, "button_Add_Click: \r\n" + DBCe.ToString());
                }
                catch { }
                c1FlexGrid_Conf.RowSel = c1FlexGrid_Conf.Rows.Count - 1;
                c1FlexGrid_Conf.Update();
            }
        }

        private void button_Delete_Click()
        {

            if (c1FlexGrid_Conf.RowSel >= 0)
            {
                DataRowView dr_delete = (DataRowView)c1FlexGrid_Conf.Rows[c1FlexGrid_Conf.RowSel].DataSource;
                if (dr_delete != null)
                {
                    
                    string question = "Do you want to delete the selected row?";

                    if (MessageBox.Show(question, "Delete_Confirm", System.Windows.Forms.MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        try
                        {
                            dr_delete.Delete();
                            MySqlCommandBuilder myCommand = new MySqlCommandBuilder(da_Conf);
                            da_Conf.Update(dt_Conf);
                            bTableChanged = false;
                            c1FlexGrid_Conf.Update();
                        }
                        catch (DBConcurrencyException DBCe)
                        {
                            mySave.InsertRow((int)Definition.Message.D_DEBUG, "button_Delete_Click: \r\n" + DBCe.ToString());
                        }
                        catch (Exception ex) { mySave.InsertRow((int)Definition.Message.D_ALARM, ex.ToString()); }
                    }
                }
                else
                {
                    MessageBox.Show("no row selected!", "error");
                }
            }
        }

        private void button_Save_Click()
        {
            c1Ribbon.Focus();
            try
            {
                MySqlCommandBuilder myCommand = new MySqlCommandBuilder(da_Conf);
                myCommand.SetAllValues = true;
                
                da_Conf.Update(dt_Conf);
             
                bTableChanged = false;
            }
            catch (DBConcurrencyException DBCe)
            {
                mySave.InsertRow((int)Definition.Message.D_DEBUG, "button_Save_Click: \r\n" + DBCe.ToString());
            }
            catch (Exception ex) {
                mySave.InsertRow((int)Definition.Message.D_ALARM, ex.ToString()); 
            }
        }


        private int AskForDataChanged()
        {
            int nRet = -1;
            if (bTableChanged)
            {
                DialogResult r = MessageBox.Show("Data changed - Do you want to save the data?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                if (r.ToString() == "No")
                {
                    // do nothing 
                    bTableChanged = false;
                    nRet = 0;
                }
                else if (r.ToString() == "Yes")
                {
                    // save the data
                    button_Save_Click();
                    nRet = 1;
                }
                else if (r.ToString() == "Cancel")
                {
                    // stop - don't reset set bTableChanged
                    nRet = 3;
                }
            }
            return nRet;
        }
      

        private void Configuration_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bTableChanged)
            {
                if (AskForDataChanged() == 3)
                {
                    // stop closing the form
                    e.Cancel = true;
                }
            }
        }

      



        private void c1FlexGrid_Conf_OwnerDrawCell_1(object sender, OwnerDrawCellEventArgs e)
        {
            C1.Win.C1FlexGrid.C1FlexGrid flex = sender as C1.Win.C1FlexGrid.C1FlexGrid;
            // only apply styles to scrollable cells
            if (e.Row < flex.Rows.Fixed || e.Col < flex.Cols.Fixed)
                return;

            // get underlying DataRow
            int indexRow = flex.Rows[e.Row].DataIndex;
              //if (indexRow < 0) return;

            CurrencyManager cm = (CurrencyManager)BindingContext[flex.DataSource, flex.DataMember];
            DataRowView drv = cm.List[indexRow] as DataRowView;
           // e.Style.BackColor = Color.White;
            // select style based on row state
            switch (drv.Row.RowState)
            {
                    
                case DataRowState.Added:
                 //   e.Style = flex.Styles["Added"];
                  //  bTableChanged = true;
                    break;
                case DataRowState.Modified:
                  //  e.Style = flex.Styles["Modified"];
                  //  bTableChanged = true;
                    if ( indexRow +1== e.Row)
                    {
                       // CellRange rg = flex.GetCellRange(e.Row, 0);
                       // rg.Image = Image.FromFile(@"d:\Arbeit\Hephaistos\lib\Pics\Miscellaneous\Error-icon.png");
                       // e.Style.BackColor = Color.Gold;
                    }
                    break;
                case DataRowState.Detached:
                //    e.Style = flex.Styles["Detached"];
               //     bTableChanged = true;
                    break;
                case DataRowState.Unchanged:
                    if (e.Row == flex.RowSel)
                    {
                 //       e.Style.BackColor = Color.LightBlue;
                    }
                    else
                    {
                   //     e.Style.BackColor = Color.White;
                    }
                    break;
                default:
                    break;
            }
        }

        private void c1FlexGrid_Conf_ChangeEdit(object sender, EventArgs e)
        {
            bTableChanged = true;
            Console.WriteLine("c1FlexGrid_Conf_ChangeEdit entered");
        }

   

            
        

    }

  /*  class MyRenderer : C1.Win.C1FlexGrid.GridRendererOffice2007Blue
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
                        cellType = CellType.TopLeft;
                    }
                }

                base.OnDrawCell(flex, e, cellType);
            }catch{}
        }
    }
    */
}


