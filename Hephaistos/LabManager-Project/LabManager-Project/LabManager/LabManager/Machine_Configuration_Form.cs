using System;
using System.Collections.Specialized;
using System.Data;
using System.Windows.Forms;
using Logging;
using Definition;
using MySQL_Helper_Class;
using MySql.Data.MySqlClient;
using C1.Win.C1FlexGrid;
using cs_IniHandlerDevelop;

namespace LabManager
{
    public partial class Machine_Configuration_Form : Form
    {
        MySQL_HelperClass myHC = new MySQL_HelperClass();
        Save mySave = new Save("Machine-Configuration Form");
        IniStructure myIniHandler = new IniStructure();
        Definitions myDefinitions = new Definitions();

        TreeNode tn_LastSelected = null;
        int _nSelected_Machine_ID = -1;
        int _nSelectedGroup = -1;
        int _nProgram_Number_ID = -1;

        MySqlDataAdapter da_MachineGroup = new MySqlDataAdapter();
        DataSet ds_MachineGroup = new DataSet();
        DataTable dt_MachineGroup = null;
       
        MySqlDataAdapter da_MachineList = new MySqlDataAdapter();
        DataSet ds_MachineList = new DataSet();
        DataTable dt_MachineList = null;

        MySqlDataAdapter da_Machines = new MySqlDataAdapter();
        DataSet ds_Machines = new DataSet();
        DataTable dt_Machines = null;

        ListDictionary ld_Machines = null;
        ListDictionary ld_Connections = null;
      
        public Machine_Configuration_Form()
        {
            Application.EnableVisualStyles();
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
           
            string IniFilePath = myDefinitions.LanguageFile;
            myIniHandler = IniStructure.ReadIni(IniFilePath);
        }

        private void Machine_Configuration_Form_Load(object sender, EventArgs e)
        {
            // make sure grid shows the errors
            c1FlexGrid_MachineList.ShowErrors = true;
            c1FlexGrid_Machines.ShowErrors = true;
            c1FlexGrid_EDIT.ShowErrors = true;
            ds_MachineGroup.Clear();
            c1FlexGrid_EDIT.DataSource = null;
            c1FlexGrid_EDIT.Clear();
            ButtonEnable(false); 

            CellStyle cs = c1FlexGrid_EDIT.Styles.Add("Boolean");
            cs.DataType = typeof(Boolean);
            cs.ImageAlign = ImageAlignEnum.CenterCenter;

            tabControl_Machines.TabPages[2].Visible = false;

            // load first tab
            LoadMachineListTable();
            PopulateTree(treeView_Machines.Nodes);
        }

        private void LoadMachineListTable()
        {
            string SQL_Statement_MachineList = @"SELECT idmachine_list AS 'ID',name AS Name, Connection_type_list_ID AS ConnectionType,Description FROM machine_list ORDER BY Name";

            da_MachineList = myHC.GetAdapterFromSQLCommand(SQL_Statement_MachineList);
            ds_MachineList.Clear();
            da_MachineList.Fill(ds_MachineList);

            if (ds_MachineList.Tables[0] != null)
            {
                dt_MachineList = ds_MachineList.Tables[0];
                c1FlexGrid_MachineList.DataSource = dt_MachineList;
            }

            //format the grid
            c1FlexGrid_MachineList.Cols[0].Width = 25;
            c1FlexGrid_MachineList.Cols[1].Width = 35;
            c1FlexGrid_MachineList.Cols[2].Width = 180;
            c1FlexGrid_MachineList.ExtendLastCol = true;
            // c1FlexGrid_Machines.Cols[3] = ConnectionType
            c1FlexGrid_MachineList.Cols[3].DataMap = GetListDictionaryForConnectionType();
        }

        private void button_Condition_Save_Click(object sender, EventArgs e)
        {
            string strName = null;
            int nID = -1;

            DataRow dr_Con = dt_MachineList.Rows[c1FlexGrid_MachineList.Row - 1];
            
                try
                {
                    strName = c1FlexGrid_MachineList[c1FlexGrid_MachineList.RowSel,"Name"].ToString();
                }
                catch { }

                if (strName.Length <= 0)
                {
                    MessageBox.Show(myIniHandler.GetValue("Admin-Machines", "Enter_Name"), myIniHandler.GetValue("Miscellaneous", "Error"));
                }

                try
                {
                    nID = Int32.Parse( c1FlexGrid_MachineList[c1FlexGrid_MachineList.RowSel,"ID"].ToString()); 
                }
                catch { }

                if (nID <= 0)
                {
                    MessageBox.Show(myIniHandler.GetValue("Admin-Machines", "Enter_ID"), myIniHandler.GetValue("Miscellaneous", "Error"));
                }

                if (nID > 0 && strName.Length > 0)
                {
                    try
                    {
                        MySqlCommandBuilder myCommand = new MySqlCommandBuilder(da_MachineList);
                        da_MachineList.Update(dt_MachineList);
                    }
                    catch (DBConcurrencyException DBCe)
                    {
                        mySave.InsertRow((int)Definition.Message.D_DEBUG, "button_Configuration_Save_Click: \r\n" + DBCe.ToString());
                    }
                    catch (Exception ex) { mySave.InsertRow((int)Definition.Message.D_ALARM, ex.ToString()); }

                    LoadMachineListTable();
                    c1FlexGrid_MachineList.Update();
                }
                
        }

        private void button_Condition_Add_Click(object sender, EventArgs e)
        {
            if (dt_MachineList != null)
            {
                DataRow dr_new = dt_MachineList.Rows.Add();

                c1FlexGrid_MachineList.RowSel = c1FlexGrid_MachineList.Rows.Count - 1;
            }
        }

        private void button_Condition_Delete_Click(object sender, EventArgs e)
        {
            if (c1FlexGrid_MachineList.RowSel >= 0)
            {
                DataRowView dr_delete = (DataRowView)c1FlexGrid_MachineList.Rows[c1FlexGrid_MachineList.RowSel].DataSource;
                string question = String.Format(myIniHandler.GetValue("Admin-Machines", "Delete_Question"), dr_delete["Name"].ToString(), dr_delete["ID"].ToString());

                if (MessageBox.Show(question, myIniHandler.GetValue("Admin-Machines", "Delete_Confirm"), System.Windows.Forms.MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    try
                    {
                        myHC.DeleteMachineListEntryFromMachinelistByMachineList_ID(Int32.Parse(dr_delete["ID"].ToString()));
                        c1FlexGrid_MachineList.RemoveItem(c1FlexGrid_MachineList.Row);
                        c1FlexGrid_MachineList.Update();
                    }
                    catch (DBConcurrencyException DBCe)
                    {
                        mySave.InsertRow((int)Definition.Message.D_DEBUG, "button_Configuration_Delete_Click: \r\n" + DBCe.ToString());
                    }
                    catch (Exception ex) { mySave.InsertRow((int)Definition.Message.D_ALARM, ex.ToString()); }
                    LoadMachineListTable();
                }
            }
        }

        private void c1FlexGrid_MachineList_GetCellErrorInfo(object sender, C1.Win.C1FlexGrid.GetErrorInfoEventArgs e)
        {
            C1FlexGrid grid = sender as C1FlexGrid;

            int nID = -1;
            string strName = null;

            if (grid.Cols[e.Col].Name == "ID" && e.Row > 0)
            {
                try
                {
                     nID = Int32.Parse(grid.GetData(e.Row, "ID").ToString());
                }
                catch { }

                if (nID <= 0)
                {
                    e.ErrorText = myIniHandler.GetValue("Admin-Machines", "Enter_ID");
                }
                else { e.ErrorText = ""; }
            }

            if (grid.Cols[e.Col].Name == "Name" && e.Row > 0)
            {
                try
                {
                  strName = grid.GetData(e.Row, "Name").ToString();
                }
                catch { }

                if (strName.Length <= 0)
                {
                    e.ErrorText = myIniHandler.GetValue("Admin-Machines", "Enter_Name");
                }
                else { e.ErrorText = ""; }
            }
        }

        private void LoadMachinesTable()
        {
            string SQL_Statement_Machines = @"SELECT idmachines AS 'ID',name AS Name,Machine_list_ID AS MachineType,Description FROM machines ORDER BY Name";

            da_Machines = myHC.GetAdapterFromSQLCommand(SQL_Statement_Machines);
            ds_Machines.Clear();
            da_Machines.Fill(ds_Machines);

            if (ds_Machines.Tables[0] != null)
            {
                dt_Machines = ds_Machines.Tables[0];
                c1FlexGrid_Machines.DataSource = dt_Machines;
            }
        
            //format the grid
            c1FlexGrid_Machines.Cols[0].Width = 25;
            c1FlexGrid_Machines.Cols[1].Width = 35;
            c1FlexGrid_Machines.Cols[2].Width = 180;
            c1FlexGrid_Machines.ExtendLastCol = true;

        }

        private void SetDictionariesForMachines()
        {       
            // c1FlexGrid_Machines.Cols[3] = MachineType
            c1FlexGrid_Machines.Cols[3].DataMap = GetListDictionaryForMachines();
        }

        private ListDictionary GetListDictionaryForConnectionType()
        {
            DataSet ConnectionTypeNames = new DataSet();
            ConnectionTypeNames.Clear();
            ConnectionTypeNames = myHC.GetDataSetFromSQLCommand("SELECT idtype_list AS 'id',Name FROM connection_type_list");

            if (ConnectionTypeNames.Tables[0] != null)
            {
                DataTable dt_ConnectionTypes = ConnectionTypeNames.Tables[0];
                ld_Connections = new ListDictionary();
                ld_Connections.Add(0, myIniHandler.GetValue("Miscellaneous", "Please_select"));
                foreach (DataRow dr_ConnectionTypes in dt_ConnectionTypes.Rows)
                {
                    // ItemArray[0] = id
                    // ItemArray[1] = name
                    ld_Connections.Add((int)dr_ConnectionTypes.ItemArray[0], dr_ConnectionTypes.ItemArray[1]);
                }
            }
            return ld_Connections;
        }

        private ListDictionary GetListDictionaryForMachines()
        {
            // load Machines Dictionary
            DataSet MachineNames = new DataSet();
            MachineNames.Clear();
            MachineNames = myHC.GetDataSetFromSQLCommand("SELECT idmachine_list AS 'id',Name FROM machine_list ORDER BY Name");

            if (MachineNames.Tables[0] != null)
            {
                DataTable dt_Machines = MachineNames.Tables[0];
                ld_Machines = new ListDictionary();
                ld_Machines.Add(0, myIniHandler.GetValue("Miscellaneous", "Please_select"));
                foreach (DataRow dr_Machines in dt_Machines.Rows)
                {
                    // ItemArray[0] = id
                    // ItemArray[1] = name
                    ld_Machines.Add((int)dr_Machines.ItemArray[0], dr_Machines.ItemArray[1]);
                }
            }
            return ld_Machines;
        }

        private void c1FlexGrid_MachineList_BeforeEdit(object sender, RowColEventArgs e)
        {
          //  if (e.Row >= 1 && e.Col == 1) { e.Cancel = true; }
        }

      
        private void button_machines_Add_Click(object sender, EventArgs e)
        {
            if (dt_Machines != null)
            {
                DataRow dr_new = dt_Machines.Rows.Add();

                c1FlexGrid_Machines.RowSel = c1FlexGrid_Machines.Rows.Count - 1;
            }
        }

        private void button_machines_Save_Click(object sender, EventArgs e)
        {
            string strName = null;
            int nID = -1;

            DataRow dr_Con = dt_Machines.Rows[c1FlexGrid_Machines.Row - 1];

            try
            {
                strName = c1FlexGrid_Machines[c1FlexGrid_Machines.RowSel, "Name"].ToString();
            }
            catch { }

            if (strName.Length <= 0)
            {
                MessageBox.Show(myIniHandler.GetValue("Admin-Machines", "Enter_Name"), myIniHandler.GetValue("Miscellaneous", "Error"));
            }

            try
            {
                nID = Int32.Parse(c1FlexGrid_Machines[c1FlexGrid_Machines.RowSel, "ID"].ToString());
            }
            catch { }

            if (nID <= 0)
            {
                MessageBox.Show(myIniHandler.GetValue("Admin-Machines", "Enter_ID"), myIniHandler.GetValue("Miscellaneous", "Error"));
            }

            if (nID > 0 && strName.Length > 0)
            {
                try
                {
                    MySqlCommandBuilder myCommand = new MySqlCommandBuilder(da_Machines);
                    da_Machines.Update(dt_Machines);
                }
                catch (DBConcurrencyException DBCe)
                {
                    mySave.InsertRow((int)Definition.Message.D_DEBUG, "button_machines_Save_Click: \r\n" + DBCe.ToString());
                }
                catch (Exception ex) { MessageBox.Show(ex.ToString()); }

                LoadMachinesTable();
                c1FlexGrid_Machines.Update();
            }
        }

        private void button_machines_Delete_Click(object sender, EventArgs e)
        {
            if (c1FlexGrid_Machines.RowSel >= 0)
            {
                DataRowView dr_delete = (DataRowView)c1FlexGrid_Machines.Rows[c1FlexGrid_Machines.RowSel].DataSource;
                string question = String.Format(myIniHandler.GetValue("Admin-Machines", "Delete_Question"), dr_delete["Name"].ToString(), dr_delete["ID"].ToString());
                if (MessageBox.Show(question, String.Format(myIniHandler.GetValue("Admin-Machines", "Delete_Confirm")), System.Windows.Forms.MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    try
                    {
                        myHC.DeleteMachinesEntryFromMachinelistByMachines_ID(Int32.Parse(dr_delete["ID"].ToString()));
                        c1FlexGrid_Machines.RemoveItem(c1FlexGrid_Machines.Row);
                        c1FlexGrid_Machines.Update();
                    }
                    catch (DBConcurrencyException DBCe)
                    {
                        mySave.InsertRow((int)Definition.Message.D_DEBUG, "button_machines_Delete_Click: \r\n" + DBCe.ToString());
                    }
                    catch (Exception ex) { mySave.InsertRow((int)Definition.Message.D_ALARM, ex.ToString()); }
                    LoadMachinesTable();
                }
            }
        }

       

        private string Get_SQL_StatementForMachines()
        {

            return @"SELECT idmachines AS 'ID',name AS Name,Number,Machine_list_ID,Description FROM machines ORDER BY name";

        }

       

        private void tabControl_Machines_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl_Machines.SelectedIndex)
            {
                case 0:
                    LoadMachineListTable();
                    break;
                case 1:
                    LoadMachinesTable();
                    SetDictionariesForMachines();
                    break;
                case 2:
                    
                    break;
              
            }
        }


        private ListDictionary GetListDictionaryForParameterNames()
        {
            //c1FlexGrid_Machines.Cols[4].DataMap = ld_Machines;
            DataSet ConnectionTypeNames = new DataSet();
            ConnectionTypeNames.Clear();
            ConnectionTypeNames = myHC.GetDataSetFromSQLCommand("SELECT idmachine_program_parameter_names AS 'id',Name FROM machine_program_parameter_names");

            if (ConnectionTypeNames.Tables[0] != null)
            {
                DataTable dt_ConnectionTypes = ConnectionTypeNames.Tables[0];
                ld_Connections = new ListDictionary();
                ld_Connections.Add(0, myIniHandler.GetValue("Miscellaneous", "Please_select"));
                foreach (DataRow dr_ConnectionTypes in dt_ConnectionTypes.Rows)
                {
                    // ItemArray[0] = id
                    // ItemArray[1] = name
                    ld_Connections.Add((int)dr_ConnectionTypes.ItemArray[0], dr_ConnectionTypes.ItemArray[1]);
                }
            }
            return ld_Connections;
        }

        private ListDictionary GetListDictionaryForUnitsOfMeasurement()
        {
            //c1FlexGrid_Machines.Cols[4].DataMap = ld_Machines;
            DataSet ConnectionTypeNames = new DataSet();
            ConnectionTypeNames.Clear();
            ConnectionTypeNames = myHC.GetDataSetFromSQLCommand("SELECT idunits_of_measurement AS 'id',unit FROM units_of_measurement");

            if (ConnectionTypeNames.Tables[0] != null)
            {
                DataTable dt_ConnectionTypes = ConnectionTypeNames.Tables[0];
                ld_Connections = new ListDictionary();
                ld_Connections.Add(0, myIniHandler.GetValue("Miscellaneous", "Please_select"));
                foreach (DataRow dr_ConnectionTypes in dt_ConnectionTypes.Rows)
                {
                    // ItemArray[0] = id
                    // ItemArray[1] = name
                    ld_Connections.Add((int)dr_ConnectionTypes.ItemArray[0], dr_ConnectionTypes.ItemArray[1]);
                }
            }
            return ld_Connections;
        }

        private ListDictionary GetListDictionaryForInOutPut()
        {
            ListDictionary ld_InOutputs = null;
            ld_InOutputs = new ListDictionary();
            ld_InOutputs.Add("Please select", myIniHandler.GetValue("Miscellaneous", "Please_select"));
            ld_InOutputs.Add("Input", myIniHandler.GetValue("Admin-Machines", "Input"));
            ld_InOutputs.Add("Output", myIniHandler.GetValue("Admin-Machines", "Output"));
            ld_InOutputs.Add("Status", myIniHandler.GetValue("Admin-Machines", "Statusbit"));
           
            return ld_InOutputs;
        }

        private void c1FlexGrid_MachinePositions_Positions_BeforeEdit(object sender, RowColEventArgs e)
        {
            if (e.Row >= 1 && e.Col == 1) { e.Cancel = true; }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode selNode = e.Node as TreeNode;
            c1FlexGrid_EDIT.Clear();
            string SQL_Statement = null;
            _nSelected_Machine_ID = -1;
            _nSelectedGroup = -1;
            ButtonEnable(false); 

            try
            {
                //
                if (selNode.Level >= 1)
                {
                   
                    if (selNode.Level == 1)
                    {
                        object oSelectedGroup = selNode.Tag as object;
                        int nSelectedGroup = -1;
                        bool bGotGroupID = Int32.TryParse(oSelectedGroup.ToString(), out nSelectedGroup);
                        int nMachine_ID = -1;
                        DataRow DataRowMachine = selNode.Parent.Tag as DataRow;
                        nMachine_ID = (int)DataRowMachine.ItemArray[0];
                        _nSelected_Machine_ID = nMachine_ID;

                        _nSelectedGroup = nSelectedGroup;

                        switch (nSelectedGroup)
                        {
                            case (int)Definition.MachineGroups.POSITIONS:
                                SQL_Statement = "SELECT idmachine_positions As ID,Machine_ID,Name,PosNumber,InternalPos,Registration_Point,Moving_Point,Description FROM machine_positions WHERE Machine_ID=" + nMachine_ID + " ORDER BY PosNumber";
                                break;

                            case (int)Definition.MachineGroups.COMMANDS:
                                SQL_Statement = "SELECT idmachine_commands As ID,Machine_ID,Name,Number,Description FROM machine_Commands WHERE Machine_ID=" + nMachine_ID;
                                break;

                            case (int)Definition.MachineGroups.STATESIGNALS:
                                SQL_Statement = "SELECT idmachine_state_signals As ID,Machine_ID,Name,Signal_type,Signal_number,bit_Number AS Bit, time_since_last_reset AS `Time Reset`, count_since_last_reset AS `Count Reset`,limit_1 AS `Limit 1`,limit_2 AS `Limit 2`,Description FROM machine_state_signals WHERE Machine_ID=" + nMachine_ID;
                                break;

                            case (int)Definition.MachineGroups.PROGRAMS:
                                SQL_Statement = "SELECT idmachine_programs As ID,MachineList_ID,Name,Program_Number AS 'Program Number',Description FROM machine_programs WHERE MachineList_ID=" + myHC.GetMachineList_IDFromMachinesByMachine_ID(nMachine_ID);
                                break;

                            case (int)Definition.MachineGroups.PROGRAMPARAMETERNAMES:
                                SQL_Statement = "SELECT idmachine_program_parameter_names As ID,Machine_ID,units_of_measurement_ID AS 'Unit of measurement',parameter_number AS 'Parameter Number',Description FROM machine_program_parameter_names WHERE Machine_ID=" + nMachine_ID;
                                break;
                        // obsolet
                       /*     case (int)Definition.MachineGroups.SERVICE:
                                SQL_Statement = "SELECT idmachine_state_signals As ID,Machine_ID,Name,Value,limit_1,limit_2,signal_type,Description FROM machine_state_signals WHERE signal_type=2 AND Machine_ID=" + nMachine_ID;
                                break;

                            case (int)Definition.MachineGroups.STATUSBITS:
                                SQL_Statement = "SELECT idmachine_state_signals As ID,Machine_ID,Name,Bit_number AS bit,signal_type,Description FROM machine_state_signals WHERE signal_type=2 AND Machine_ID=" + nMachine_ID;
                                break;
                        * */
                        }
                    }
                    // programparameters
                  /*  if (selNode.Level == 2)
                    {
                      //  DataRow DataRowMachineLevel2 = selNode.Parent.Parent.Tag as DataRow;
                      //  int nMachine_ID = (int)DataRowMachineLevel2.ItemArray[0];
                        _nSelectedGroup = (int)Definition.MachineGroups.PROGRAMPARAMETERS;
                        int nMachine_ID = -1;
                        DataRow DataRowMachine = selNode.Parent.Parent.Tag as DataRow;
                        nMachine_ID = (int)DataRowMachine.ItemArray[0];
                        _nSelected_Machine_ID = nMachine_ID;
                        DataRow DataRowProgram = selNode.Tag as DataRow;
                        _nProgram_Number_ID = Int32.Parse(DataRowProgram["ID"].ToString());
                        SQL_Statement = @"SELECT idmachine_program_parameter As ID,Machine_ID,program_Number,parameter_number,Value 
                                          FROM machine_program_parameter WHERE Machine_ID=" + _nSelected_Machine_ID ;
                       
                    }
                    */
                    if (SQL_Statement != null)
                    {
                        ds_MachineGroup = new DataSet();
                      
                        da_MachineGroup = myHC.GetAdapterFromSQLCommand(SQL_Statement);

                        ReloadGrid();
                    }
                    else { ds_MachineGroup.Clear(); c1FlexGrid_EDIT.Clear(); ButtonEnable(false); }
                }
                else { ds_MachineGroup.Clear(); c1FlexGrid_EDIT.Clear(); ButtonEnable(false); }
            }
            catch (Exception ex) { mySave.InsertRow((int)Definition.Message.D_ALARM, ex.ToString()); }

        }

        private void ReloadGrid()
        {
            ds_MachineGroup.Clear();
            c1FlexGrid_EDIT.Clear();
            da_MachineGroup.Fill(ds_MachineGroup);


            if (ds_MachineGroup.Tables[0] != null)
            {
                dt_MachineGroup = ds_MachineGroup.Tables[0];
                c1FlexGrid_EDIT.DataSource = dt_MachineGroup;
                c1FlexGrid_EDIT.ExtendLastCol = true;
                c1FlexGrid_EDIT.Cols[2].Visible = false;

                c1FlexGrid_EDIT.Cols[0].Width = 15;
                ButtonEnable(true);
            }

            int i = 0;
            foreach (DataRow dr_Machines in dt_MachineGroup.Rows)
            {
                c1FlexGrid_EDIT.Rows[++i].UserData = "id:" + dr_Machines["ID"].ToString();
            }
            // hide the ID coloumn
            c1FlexGrid_EDIT.Cols[1].Visible = false;

           

            switch (_nSelectedGroup)
            {
                case (int)Definition.MachineGroups.POSITIONS:
                    c1FlexGrid_EDIT.Cols[5].Style = c1FlexGrid_EDIT.Styles["Boolean"];
                    c1FlexGrid_EDIT.Cols[6].Style = c1FlexGrid_EDIT.Styles["Boolean"];
                    c1FlexGrid_EDIT.Cols[7].Style = c1FlexGrid_EDIT.Styles["Boolean"];
                    break;
                case (int)Definition.MachineGroups.PROGRAMPARAMETERNAMES:
                    c1FlexGrid_EDIT.Cols[3].DataMap = GetListDictionaryForUnitsOfMeasurement();
                    break;
                case (int)Definition.MachineGroups.STATESIGNALS:
                    c1FlexGrid_EDIT.Cols[4].DataMap = GetListDictionaryForInOutPut();
                    break;

            }
            c1FlexGrid_EDIT.AutoSizeCols();
        }

        private void ButtonEnable(bool bEnable)
        {
            button_Group_Add.Enabled = bEnable;
            button_Group_Save.Enabled = bEnable;
            button_Group_Delete.Enabled = bEnable;
        }

        private void PopulateTree(TreeNodeCollection ParentNodes)
        {
            string SQL_Statement = Get_SQL_StatementForMachines();
            DataSet ds_machines = new DataSet();
            ds_machines.Clear();
            ds_machines = myHC.GetDataSetFromSQLCommand(SQL_Statement);
            foreach (DataTable dt in ds_machines.Tables)
            {

                foreach (DataRow dataRow_Machine in dt.Rows) // every Machine
                {
                    int nMachine_list_ID = (int)dataRow_Machine["Machine_list_ID"];
                    TreeNode nColumn = ParentNodes.Add(dataRow_Machine.ItemArray[1].ToString());
                    nColumn.Tag = dataRow_Machine;
                    nColumn.ImageIndex = nColumn.SelectedImageIndex = 12;
                    TreeNode nColumnPositions = nColumn.Nodes.Add("Positions");
                    nColumnPositions.Tag = (int)Definition.MachineGroups.POSITIONS;
                    nColumnPositions.ImageIndex = nColumnPositions.SelectedImageIndex = 18;
                   
                
                     TreeNode nColumnCommands = nColumn.Nodes.Add("Commands");
                     nColumnCommands.Tag = (int)Definition.MachineGroups.COMMANDS;
                     nColumnCommands.ImageIndex = nColumnCommands.SelectedImageIndex = 18;
                        
                  //  if (myHC.GetConnectionTypeFromConnectionListByMachineList_ID(nMachine_list_ID) == 1)// Only PLC
                    {
                        TreeNode nColumnOutputs = nColumn.Nodes.Add("In-,Out-, State-Signals");
                        nColumnOutputs.Tag = (int)Definition.MachineGroups.STATESIGNALS;
                        nColumnOutputs.ImageIndex = nColumnOutputs.SelectedImageIndex = 18;
                    }

                    if (myHC.GetConnectionTypeFromConnectionListByMachineList_ID(nMachine_list_ID) == 1)// Only PLC
                    {
                        TreeNode nColumnPrograms = nColumn.Nodes.Add("Programs");
                        nColumnPrograms.Tag = (int)Definition.MachineGroups.PROGRAMS;
                        nColumnPrograms.ImageIndex = nColumnPrograms.SelectedImageIndex = 18;
                        TreeNode nColumnProgramParameternames = nColumn.Nodes.Add("Programparameternames");
                        nColumnProgramParameternames.Tag = (int)Definition.MachineGroups.PROGRAMPARAMETERNAMES;
                        nColumnProgramParameternames.ImageIndex = nColumnProgramParameternames.SelectedImageIndex = 18;

                    }
                }

            }
        }

        private void button_Group_Add_Click(object sender, EventArgs e)
        {
            if (dt_MachineGroup != null)
            {
                try
                {
                    DataRow dr_new = dt_MachineGroup.Rows.Add();
                    switch (_nSelectedGroup)
                    {
                        case (int)Definition.MachineGroups.PROGRAMS:
                            dr_new["MachineList_ID"] = myHC.GetMachineList_IDFromMachinesByMachine_ID(_nSelected_Machine_ID);
                            break;

                        case (int)Definition.MachineGroups.PROGRAMPARAMETERS:
                            dr_new["Machine_ID"] = _nSelected_Machine_ID;
                            dr_new["Program_Number_ID"] = _nProgram_Number_ID;
                            break;

                        case (int)Definition.MachineGroups.POSITIONS:
                            dr_new["Machine_ID"] = _nSelected_Machine_ID;
                            c1FlexGrid_EDIT[c1FlexGrid_EDIT.RowSel, "InternalPos"] = 0;
                            c1FlexGrid_EDIT[c1FlexGrid_EDIT.RowSel, "Registration_Point"] = 0;
                            break;

                        default:
                            dr_new["Machine_ID"] = _nSelected_Machine_ID;
                            break;
                    }

                }
                catch (DBConcurrencyException DBCe)
                {
                    mySave.InsertRow((int)Definition.Message.D_DEBUG, "button_Group_Add_Click: \r\n" + DBCe.ToString());
                }
                catch { }
                c1FlexGrid_EDIT.RowSel = c1FlexGrid_EDIT.Rows.Count - 1;

            }
        }

        private void button_Group_Save_Click(object sender, EventArgs e)
        {
                try
                {
                    MySqlCommandBuilder myCommand = new MySqlCommandBuilder(da_MachineGroup);
                    da_MachineGroup.Update(dt_MachineGroup);
                }
                catch (DBConcurrencyException DBCe)
                {
                    mySave.InsertRow((int)Definition.Message.D_DEBUG, "button_Group_Save_Click: \r\n" + DBCe.ToString());
                }
                catch (Exception ex) { mySave.InsertRow((int)Definition.Message.D_ALARM, ex.ToString()); }
                ReloadGrid();
        }

        private void button_Group_Delete_Click(object sender, EventArgs e)
        {
            if (c1FlexGrid_EDIT.RowSel >= 0)
            {
                DataRowView dr_delete = (DataRowView)c1FlexGrid_EDIT.Rows[c1FlexGrid_EDIT.RowSel].DataSource;
                string question = "Delete?";
                try
                {
                    question = String.Format(myIniHandler.GetValue("Admin-Machines", "Delete_Question"), dr_delete["Name"].ToString(), dr_delete["ID"].ToString());
                }
                catch { }

                if (MessageBox.Show(question, String.Format(myIniHandler.GetValue("Admin-Machines", "Delete_Confirm")), System.Windows.Forms.MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    try
                    {
                        dr_delete.Delete();
                        MySqlCommandBuilder myCommand = new MySqlCommandBuilder(da_MachineGroup);
                        da_MachineGroup.Update(dt_MachineGroup);
                        c1FlexGrid_EDIT.Update();
                    }
                    catch (DBConcurrencyException DBCe)
                    {
                        mySave.InsertRow((int)Definition.Message.D_DEBUG, "button_Group_Delete_Click: \r\n" + DBCe.ToString());
                    }
                    catch (Exception ex) {  mySave.InsertRow((int)Definition.Message.D_ALARM, ex.ToString()); }                  
                }
            }
        }

        private void treeView_Machines_MouseDown(object sender, MouseEventArgs e)
        {
            if (tn_LastSelected != null) { tn_LastSelected.BackColor = this.BackColor; tn_LastSelected.ForeColor = this.ForeColor; tn_LastSelected.ImageIndex = tn_LastSelected.SelectedImageIndex = 18; }
            TreeView tv = sender as TreeView;
            TreeNode tn = treeView_Machines.GetNodeAt(e.Location);
            if (tn != null)
            {
                if (tn.Level >= 1)
                {
                    tn.BackColor = System.Drawing.Color.Orange;
                    tn.ForeColor = System.Drawing.Color.Black;
                    tn.ImageIndex = tn.SelectedImageIndex = 19;
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

        private void c1FlexGrid_EDIT_GetCellErrorInfo(object sender, GetErrorInfoEventArgs e)
        {
            C1FlexGrid grid = sender as C1FlexGrid;
      
            switch (_nSelectedGroup)
            {
                case (int)Definition.MachineGroups.POSITIONS:   // Name,PosNumber,InternalPos,Description
                    if (e.Col == 3)
                    {
                        try
                        {
                            if ( grid[e.Row, "Name"].ToString().Length <=0)
                            {
                                e.ErrorText = myIniHandler.GetValue("Admin-Machines", "Enter_Name");
                            }             
                        }
                        catch { e.ErrorText = myIniHandler.GetValue("Admin-Machines", "Enter_Name"); /**/}
                    }
                    if (e.Col == 4)
                    {
                        try
                        {
                            if (grid[e.Row, "PosNumber"].ToString().Length <= 0)
                            {
                                e.ErrorText = myIniHandler.GetValue("Admin-Machines", "Enter_Pos_Number");
                            }
                        }
                        catch { e.ErrorText = myIniHandler.GetValue("Admin-Machines", "Enter_Pos_Number"); /**/}
                    }
                    if (e.Col == 5)
                    {
                        try
                        {
                            if (grid[e.Row, "InternalPos"].ToString().Length <= 0)
                            {
                                e.ErrorText = myIniHandler.GetValue("Admin-Machines", "Enter_Internal_Pos");
                            }
                        }
                        catch { e.ErrorText = myIniHandler.GetValue("Admin-Machines", "Enter_Internal_Pos"); /**/}
                    }
                    if (e.Col == 6)
                    {
                        try
                        {
                            if (grid[e.Row, "Registration_Point"].ToString().Length <= 0)
                            {
                                e.ErrorText = myIniHandler.GetValue("Admin-Machines", "Select_Registration_Point");
                            }
                        }
                        catch { e.ErrorText = myIniHandler.GetValue("Admin-Machines", "Select_Registration_Point"); /**/}
                    }
                    break;

                case (int)Definition.MachineGroups.COMMANDS:    //Name,Number,Description
                    if (e.Col == 3)
                    {
                        try
                        {
                            if (grid[e.Row, "Name"].ToString().Length <= 0)
                            {
                                e.ErrorText = myIniHandler.GetValue("Admin-Machines", "Enter_Name");
                            }
                        }
                        catch { e.ErrorText = myIniHandler.GetValue("Admin-Machines", "Enter_Name"); /**/}
                    }
                    if (e.Col == 4)
                    {
                        try
                        {
                            if (grid[e.Row, "Number"].ToString().Length <= 0)
                            {
                                e.ErrorText = myIniHandler.GetValue("Admin-Machines", "Enter_Pos_Number");
                            }
                        }
                        catch { e.ErrorText = myIniHandler.GetValue("Admin-Machines", "Enter_Pos_Number"); /**/}
                    }
                    break;

                case (int)Definition.MachineGroups.PROGRAMS:    //Name,Description
                    if (e.Col == 3)
                    {
                        try
                        {
                            if (grid[e.Row, "Name"].ToString().Length <= 0)
                            {
                                e.ErrorText = myIniHandler.GetValue("Admin-Machines", "Enter_Name");
                            }
                        }
                        catch { e.ErrorText = myIniHandler.GetValue("Admin-Machines", "Enter_Name"); /**/}
                    }
                    break;

                case (int)Definition.MachineGroups.PROGRAMPARAMETERNAMES: //Name,units_of_measurement_ID AS Unit,parameter_number AS 'Parameter Number',Description


                    if (e.Col == 3)//units_of_measurement_ID
                    {
                        try
                        {
                            if (grid[e.Row, 3].ToString().Length <= 0)
                            {
                                e.ErrorText = myIniHandler.GetValue("Admin-Machines", "Enter_Unit"); ;
                            }
                        }
                        catch { e.ErrorText = myIniHandler.GetValue("Admin-Machines", "Enter_Unit");  /**/}
                    }

                    if (e.Col == 4) // parameter_number
                    {
                        try
                        {
                            if (grid[e.Row, 4].ToString().Length <= 0)
                             //   if (grid[e.Row, "Pararmeter Number"].ToString().Length <= 0)
                            {
                                e.ErrorText = myIniHandler.GetValue("Admin-Machines", "Pararmeter_Number"); ;
                            }
                        }
                        catch { e.ErrorText = myIniHandler.GetValue("Admin-Machines", "Pararmeter_Number");  /**/}
                    }
                    break;

                case (int)Definition.MachineGroups.STATESIGNALS:   // Name,Signal_type,Signal_number,Bit,actual_count,limit_1,limit_2,Description
                    //  SELECT idmachine_state_signals As ID,Machine_ID,Name,Signal_type,Signal_number,bit_Number AS Bit, actual_count AS `actual count`,limit_1 AS `Limit 1`,limit_2 AS `Limit 2`,Description FROM machine_state_signals 
               
                    if (e.Col == 3)
                    {
                        try
                        {
                            if (grid[e.Row, "Name"].ToString().Length <= 0)
                            {
                                e.ErrorText = myIniHandler.GetValue("Admin-Machines", "Enter_Name");
                            }
                        }
                        catch { e.ErrorText = myIniHandler.GetValue("Admin-Machines", "Enter_Name"); }
                    }
                    if (e.Col == 4)
                    {
                        try
                        {
                            if (grid[e.Row, "Signal_type"].ToString().Length <= 0)
                            {
                                e.ErrorText = myIniHandler.GetValue("Admin-Machines", "Enter_In_Output"); 
                            }
                        }
                        catch { e.ErrorText = myIniHandler.GetValue("Admin-Machines", "Enter_In_Output"); }
                    }
                    if (e.Col == 5)
                    {
                        try
                        {
                            if (grid[e.Row, "Signal_number"].ToString().Length <= 0)
                            {
                                e.ErrorText = myIniHandler.GetValue("Admin-Machines", "Enter_Number"); 
                            }
                        }
                       catch { e.ErrorText = myIniHandler.GetValue("Admin-Machines", "Enter_Number"); }
                    }
                    if (e.Col == 6)
                    {
                        try
                        {
                            if (grid[e.Row, "Bit"].ToString().Length <= 0)
                            {
                                e.ErrorText = myIniHandler.GetValue("Admin-Machines", "Enter_Bit");
                            }
                        }
                        catch { e.ErrorText = myIniHandler.GetValue("Admin-Machines", "Enter_Bit");}
                    }
                    try
                    {
                       if (grid[e.Row, "Signal_type"] != null)
                        { 
                            string strSignal_Type = grid[e.Row, "Signal_type"].ToString();
                             //if (strSignal_Type.Length <= 0) //myIniHandler.GetValue("Admin-Machines", "Input")
                            if (String.Equals(strSignal_Type, myIniHandler.GetValue("Admin-Machines", "Input")) 
                                || String.Equals(strSignal_Type, myIniHandler.GetValue("Admin-Machines", "Output")))
                             {
                                /* if (e.Col == 7)
                                 {
                                     try
                                     {
                                         if (grid[e.Row, "actual count"].ToString().Length <= 0)
                                         {
                                             e.ErrorText = myIniHandler.GetValue("Admin-Machines", "Enter_Start_Value");
                                         }
                                     }
                                     catch { e.ErrorText = myIniHandler.GetValue("Admin-Machines", "Enter_Start_Value"); }
                                 }*/
                                 if (e.Col == 8)
                                 {
                                     try
                                     {
                                         if (grid[e.Row, "Limit 1"].ToString().Length <= 0)
                                         {
                                             e.ErrorText = myIniHandler.GetValue("Admin-Machines", "Enter_Limit");
                                         }
                                     }
                                     catch { e.ErrorText = myIniHandler.GetValue("Admin-Machines", "Enter_Limit"); }
                                 }
                                 if (e.Col == 9)
                                 {
                                     try
                                     {
                                         if (grid[e.Row, "Limit 2"].ToString().Length <= 0)
                                         {
                                             e.ErrorText = myIniHandler.GetValue("Admin-Machines", "Enter_Limit");
                                         }
                                     }
                                     catch { e.ErrorText = myIniHandler.GetValue("Admin-Machines", "Enter_Limit"); }
                                 }
                             }
                        }
                    }
                    catch { }
                    break;

               

                case (int)Definition.MachineGroups.PROGRAMPARAMETERS:   // program_Number_ID,Value,machine_program_parameter_name_ID AS ParameterName,Description
                   
                    if (e.Col == 4)
                    {
                        try
                        {
                            if (grid[e.Row, "Value"].ToString().Length <= 0)
                            {
                                e.ErrorText = myIniHandler.GetValue("Admin-Machines", "Enter_Value");
                            }
                        }
                        catch { e.ErrorText = myIniHandler.GetValue("Admin-Machines", "Enter_Value");/**/}
                    }
                    if (e.Col == 5)
                    {
                        try
                        {
                            if (grid[e.Row, "ParameterName"].ToString().Length <= 0)
                            {
                                e.ErrorText = myIniHandler.GetValue("Admin-Machines", "Enter_Name");
                            }
                        }
                        catch { e.ErrorText = myIniHandler.GetValue("Admin-Machines", "Enter_Name"); /**/}
                    }
                    break;

                case (int)Definition.MachineGroups.STATUSBITS:   // Name,Bit_number AS bit,Description
                    if (e.Col == 3)
                    {
                        try
                        {
                            if (grid[e.Row, "Name"].ToString().Length <= 0)
                            {
                                e.ErrorText = myIniHandler.GetValue("Admin-Machines", "Enter_Name");
                            }
                        }
                        catch { e.ErrorText = myIniHandler.GetValue("Admin-Machines", "Enter_Name"); /**/}
                    }
                    if (e.Col == 4)
                    {
                        try
                        {
                            if (grid[e.Row, "bit"].ToString().Length <= 0)
                            {
                                e.ErrorText = myIniHandler.GetValue("Admin-Machines", "Enter_Bit");
                            }
                        }
                        catch { e.ErrorText = myIniHandler.GetValue("Admin-Machines", "Enter_Bit"); /**/}
                    }
                  
                    break;
            }
        }

      
     

    }
}
