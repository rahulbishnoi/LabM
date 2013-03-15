using System;
using System.Data;
using Definition;
using System.Windows.Forms;
using MySQL_Helper_Class;
using Logging;
using C1.Win.C1FlexGrid;


namespace Routing
{

    public class RoutingData
    {
        Definitions myThorDef = new Definitions();
        MySQL_HelperClass myHC = new MySQL_HelperClass();
        Save mySave = new Save("LabManager-RoutingData");

        public DataTable GetDataTableForUnits()
        {
            DataTable dataTableUnits = null;
            string SQL_StatementUnits;
            try
            {
                SQL_StatementUnits = Get_SQL_StatementUnits();
                DataSet dsUnits = new DataSet();
                dsUnits.Clear();
                dsUnits = myHC.GetDataSetFromSQLCommand(SQL_StatementUnits);
                dataTableUnits = dsUnits.Tables[0];
            }
            catch (Exception ex) { mySave.InsertRow((int)Definition.Message.D_ALARM, ex.ToString()); }
            return dataTableUnits;
        }

        private string Get_SQL_StatementUnits()
        {
            string SQL_Statement;
            SQL_Statement = "SELECT DISTINCT Machine_ID FROM routing_positions";
            return SQL_Statement;

        }
        public DataTable GetDataTableForPositions(int nMachine_ID)
        {
            DataTable dataTablePositions = null;
            string SQL_StatementPositions;
            try
            {
                SQL_StatementPositions = Get_SQL_StatementPositions(nMachine_ID);
                DataSet dsPositions = new DataSet();
                dsPositions.Clear();
                dsPositions = myHC.GetDataSetFromSQLCommand(SQL_StatementPositions);
                dataTablePositions = dsPositions.Tables[0];
            }
            catch (Exception ex) { mySave.InsertRow((int)Definition.Message.D_ALARM, ex.ToString()); }
            return dataTablePositions;
        }

        private string Get_SQL_StatementPositions(int nMachine_ID)
        {
            string SQL_Statement;
            SQL_Statement = "SELECT idrouting_positions,Machine_Position_ID,Modified FROM routing_positions WHERE Machine_ID=" + nMachine_ID;
            return SQL_Statement;

        }
        public DataTable GetDataTableForSampleTypes(int nRouting_Position_ID)
        {
            DataTable dataTableSampleTypes = null;
            string SQL_StatementSampleType;
            try
            {
                SQL_StatementSampleType = Get_SQL_StatementSampleTypes(nRouting_Position_ID);
                DataSet dsSampleTypes = new DataSet();
                dsSampleTypes.Clear();
                dsSampleTypes = myHC.GetDataSetFromSQLCommand(SQL_StatementSampleType);
                dataTableSampleTypes = dsSampleTypes.Tables[0];
            }
            catch (Exception ex) { mySave.InsertRow((int)Definition.Message.D_ALARM, ex.ToString()); }
            return dataTableSampleTypes;
        }

        private string Get_SQL_StatementSampleTypes(int nRouting_Position_ID)
        {
            string SQL_Statement;
            SQL_Statement = "SELECT DISTINCT SampleType_ID FROM routing_position_entries WHERE Position_ID=" + nRouting_Position_ID;
            return SQL_Statement;

        }

        public DataTable GetDataTableForRoutingEntries(int nRouting_Postion_ID, int nSampleType_ID)
        {
            DataTable dataTableRoutingEntries = null;
            string SQL_StatementRoutingEntries;
            try
            {
                SQL_StatementRoutingEntries = Get_SQL_StatementRoutingEntries(nRouting_Postion_ID, nSampleType_ID);
                DataSet dsRoutingEntries = new DataSet();
                dsRoutingEntries.Clear();
                dsRoutingEntries = myHC.GetDataSetFromSQLCommand(SQL_StatementRoutingEntries);
                dataTableRoutingEntries = dsRoutingEntries.Tables[0];
            }
            catch (Exception ex) { mySave.InsertRow((int)Definition.Message.D_ALARM, ex.ToString()); }
            return dataTableRoutingEntries;
        }
        private string Get_SQL_StatementRoutingEntries(int nRouting_Postion_ID, int nSampleType_ID)
        {
            string SQL_Statement;
            SQL_Statement = @"SELECT idrouting_position_entries,SampleType_ID,Modified,Description,TimeForWarning,TimeForAlarm 
                        FROM routing_position_entries WHERE Position_ID=" + nRouting_Postion_ID + " AND SampleType_ID=" + nSampleType_ID;
            return SQL_Statement;

        }

        public DataTable GetUnitsForTreeViewMenu()
        {
            DataTable dataTableUnitsForTreeViewMenu = null;
            string SQL_StatementUnitsForTreeViewMenu;
            try
            {
                SQL_StatementUnitsForTreeViewMenu = Get_SQL_StatementUnitsForTreeViewMenu();
                DataSet dsUnitsForTreeViewMenu = new DataSet();
                dsUnitsForTreeViewMenu.Clear();
                dsUnitsForTreeViewMenu = myHC.GetDataSetFromSQLCommand(SQL_StatementUnitsForTreeViewMenu);
                dataTableUnitsForTreeViewMenu = dsUnitsForTreeViewMenu.Tables[0];
            }
            catch (Exception ex) { mySave.InsertRow((int)Definition.Message.D_ALARM, ex.ToString()); }
            return dataTableUnitsForTreeViewMenu;
        }

        private string Get_SQL_StatementUnitsForTreeViewMenu()
        {
            string SQL_Statement;
            SQL_Statement = "SELECT idmachines,Name FROM machines";
            return SQL_Statement;
        }

        public DataTable GetPositionsForTreeViewMenu(int nMachine_ID)
        {
            DataTable dataTablePositionsForTreeViewMenu = null;
            string SQL_StatementPositionsForTreeViewMenu;
            try
            {
                SQL_StatementPositionsForTreeViewMenu = Get_SQL_StatementForTreeViewMenu(nMachine_ID);
                DataSet dsPositionsForTreeViewMenu = new DataSet();
                dsPositionsForTreeViewMenu.Clear();
                dsPositionsForTreeViewMenu = myHC.GetDataSetFromSQLCommand(SQL_StatementPositionsForTreeViewMenu);
                dataTablePositionsForTreeViewMenu = dsPositionsForTreeViewMenu.Tables[0];
            }
            catch (Exception ex) { mySave.InsertRow((int)Definition.Message.D_ALARM, ex.ToString()); }
            return dataTablePositionsForTreeViewMenu;
        }
        private string Get_SQL_StatementForTreeViewMenu(int nMachine_ID)
        {
            string SQL_Statement;
            SQL_Statement = "SELECT idmachine_positions,Name,PosNumber,Machine_ID FROM machine_positions WHERE Machine_ID=" + nMachine_ID.ToString() + " AND InternalPos=0";
            return SQL_Statement;
        }

        public DataTable GetSampleTypesForTreeViewMenu()
        {
            DataTable dataTableSampleTypesForTreeViewMenu = null;
            string SQL_StatementSampleTypesForTreeViewMenu;
            try
            {
                SQL_StatementSampleTypesForTreeViewMenu = Get_SQL_StatementSampleTypesForTreeViewMenu();
                DataSet dsSampleTypesForTreeViewMenu = new DataSet();
                dsSampleTypesForTreeViewMenu.Clear();
                dsSampleTypesForTreeViewMenu = myHC.GetDataSetFromSQLCommand(SQL_StatementSampleTypesForTreeViewMenu);
                dataTableSampleTypesForTreeViewMenu = dsSampleTypesForTreeViewMenu.Tables[0];
            }
            catch (Exception ex) { mySave.InsertRow((int)Definition.Message.D_ALARM, ex.ToString()); }
            return dataTableSampleTypesForTreeViewMenu;
        }

        private string Get_SQL_StatementSampleTypesForTreeViewMenu()
        {
            string SQL_Statement;
            SQL_Statement = "SELECT idsample_type_list,Name FROM sample_type_list WHERE idsample_type_list > 0";
            return SQL_Statement;
        }

        public bool InsertNewUnitIntoRouting(int nMachine_ID)
        {
            bool ret = false;
            if (myHC.InsertNewEntryIntoRouting(nMachine_ID)) { ret = true; }
            return ret;
        }

        public bool DeleteUnitFromRouting(int nMachine_ID)
        {
            bool ret = false;
            if (myHC.DeleteUnitFromRouting(nMachine_ID)) { ret = true; }
            return ret;
        }

        public bool DeleteMachinePositionFromRouting(int nRoutingPosition_ID)
        {
            bool ret = false;
            if (myHC.DeleteMachinePositionFromRouting(nRoutingPosition_ID)) { ret = true; }
            return ret;
        }
        public bool DeleteSampleTypeFromRouting(int nSampleType_ID, int nRoutingPosition_ID)
        {
            bool ret = false;

            if (myHC.DeleteSampleTypeFromRouting(nSampleType_ID, nRoutingPosition_ID)) { ret = true; }
            return ret;
        }
        public bool DeleteConditionEntryFromRouting(int nRoutingPositionEntries_ID)
        {
            bool ret = false;

            if (myHC.DeleteConditionEntryFromRouting(nRoutingPositionEntries_ID)) { ret = true; }
            return ret;
        }
        public bool DeleteEntryFromRoutingCommands(int nRoutingCommand_ID)
        {
            bool ret = false;

            if (myHC.DeleteEntryFromRoutingCommands(nRoutingCommand_ID)) { ret = true; }
            return ret;
        }
        public bool InsertPositionIntoRouting(int nMachine_ID, int nMachinePosition_ID)
        {
            bool ret = false;
            if (myHC.InsertPositionIntoRouting(nMachine_ID, nMachinePosition_ID)) { ret = true; }
            return ret;
        }
        public bool InsertNewEntryIntoRoutingEntries(int nRouting_Position_ID, int SampleType_ID)
        {
            bool ret = false;
            if (myHC.InsertNewEntryIntoRoutingEntries(nRouting_Position_ID, SampleType_ID)) { ret = true; }
            return ret;
        }

        public bool DeleteEntryFromRoutingCondition(int nRouting_Condition_ID)
        {
            bool ret = false;
            if (myHC.DeleteEntryFromRoutingCondition(nRouting_Condition_ID)) { ret = true; }
            return ret;
        }

        public bool DeleteEntriesFromRoutingConditionsByRoutingPositionEintry_ID(int nRoutingPositionEntry_ID)
        {
            bool ret = false;
            if (myHC.DeleteEntriesFromRoutingConditionsByRoutingPositionEntry_ID(nRoutingPositionEntry_ID)) { ret = true; }
            return ret;
        }

        public bool DeleteEntriesFromRoutingCommandsByRoutingPositionEintry_ID(int nRoutingPositionEntry_ID)
        {
            bool ret = false;
            if (myHC.DeleteEntriesFromRoutingCommandsByRoutingPositionEintry_ID(nRoutingPositionEntry_ID)) { ret = true; }
            return ret;
        }

        
        public bool AddConditionsRow(string SQL_Statement)
        {
            bool ret = false;
            if (myHC.InsertSQLCommand(SQL_Statement)) { ret = true; }
            return ret;
        }
        public void SaveTimeWarningAlarmInDB(int nTimeWarning, int nTimeAlarm, int nRoutingPosition_ID)
        {
            string SQL_Statement = "Update routing_position_entries SET TimeForWarning='" + nTimeWarning + "', TimeForAlarm='" + nTimeAlarm + "' WHERE idrouting_position_entries=" + nRoutingPosition_ID;
            myHC.return_SQL_Statement(SQL_Statement);
        }

        public bool CheckDataRowForCondition(C1FlexGrid grid, DataRow dr_Con2)
        {

            bool ret = false;
            int nConditionList_ID = -1;
            DataRowView dv_Con = (DataRowView)grid.Rows[grid.Row].DataSource;
            //DataRow dr_Con = dv_Con.Row;
            DataRow dr_Con = dr_Con2;
            try
            {
                //   nConditionList_ID = (int)grid[grid.Row,1];
                nConditionList_ID = Int32.Parse(dr_Con["Condition"].ToString());
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }

            int nOperation_ID = -1;
            int nValueNamePosition = -1;
            //string strValue = null;
            int nValue = -1;


            // MessageBox.Show("nConditionList_ID " + nConditionList_ID.ToString()); 
            switch (nConditionList_ID)
            {
                // preTime and Time
                case 1:
                case 2:

                    if (dr_Con["Value"].ToString().Length > 0)
                    {
                        try
                        {
                            nValue = Int32.Parse(dr_Con["Value"].ToString());
                        }
                        catch { }
                    }
                    else { nValue = 0; }
                    if (nValue > 0)
                    {
                        ret = true;
                        // apply style based on cell value

                        dr_Con.SetColumnError("Value", "");
                        // dr_Con.SetField("Operation", -1);
                        dr_Con.SetField("ValueName", "");
                    }
                    else { dr_Con.SetColumnError("Value", "Please enter the time as an Integer"); }
                    break;

                case 3:     // status

                    try
                    {

                        if (dr_Con["Operation"].ToString().Length > 0)
                        {
                            nOperation_ID = Int32.Parse(dr_Con["Operation"].ToString());
                        }
                        else { nOperation_ID = 0; }
                        if (nOperation_ID > 0)
                        {
                            dr_Con.SetColumnError("Operation", "");
                        }
                        else { dr_Con.SetColumnError("Operation", "Please select an operation"); }


                        if (dr_Con["ValueName"].ToString().Length > 0)
                        {
                            nValueNamePosition = Int32.Parse(dr_Con["ValueName"].ToString());
                        }
                        else { nValueNamePosition = 0; }

                        if (nValueNamePosition > 0)
                        {
                            dr_Con.SetColumnError("ValueName", "");
                        }
                        else { dr_Con.SetColumnError("ValueName", "Please select an unit"); }


                        if (dr_Con["Value"].ToString().Length > 0)
                        {
                            nValue = Int32.Parse(dr_Con["Value"].ToString());
                        }
                        else { nValue = 0; }

                        if (nValue > 0)
                        {
                            dr_Con.SetColumnError("Value", "");
                        }
                        else { dr_Con.SetColumnError("Value", "Please select a status"); }
                    }
                    catch { }
                    if (nOperation_ID > 0 && nValueNamePosition > 0 && nValue > 0) { ret = true; }
                    break;

                default:

                    dr_Con.SetColumnError("Value", "");
                    dr_Con.SetColumnError("Operation", "");
                    dr_Con.SetColumnError("ValueName", "");
                    break;
            }
            return ret;
        }

        public string[] GetMachineList()
        {
            DataSet MachineList = new DataSet();
            string[] columnNames = null;
            MachineList.Clear();
            MachineList = myHC.GetDataSetFromSQLCommand("Select idmachines,Name from machines");

            if (MachineList.Tables[0] != null)
            {
                DataTable dt_MachineList = MachineList.Tables[0];
                columnNames = new string[dt_MachineList.Rows.Count];
                int n = 0;
                foreach (DataRow dr_Machine in dt_MachineList.Rows)
                {
                    columnNames[n++] = (string)dr_Machine.ItemArray[0];
                }

            }
            return columnNames;
        }

        public string[] GetStateList()
        {
            DataSet ds_StateNames = new DataSet();
            string[] columnNames = null;
            ds_StateNames.Clear();
            ds_StateNames = myHC.GetDataSetFromSQLCommand("Select Name, idmachine_state_list from machine_state_list ");

            if (ds_StateNames.Tables[0] != null)
            {
                DataTable dt_StateList = ds_StateNames.Tables[0];
                columnNames = new string[dt_StateList.Rows.Count];
                int n = 0;
                foreach (DataRow dr_StateList in dt_StateList.Rows)
                {
                    columnNames[n++] = (string)dr_StateList.ItemArray[0];
                }
            }
            return columnNames;
        }
        public string[] GetSampleTypeList()
        {
            DataSet ds_SampleTypeNames = new DataSet();
            string[] columnNames = null;
            ds_SampleTypeNames.Clear();
            ds_SampleTypeNames = myHC.GetDataSetFromSQLCommand("Select Name, idsample_type_list from sample_type_list ");

            if (ds_SampleTypeNames.Tables[0] != null)
            {
                DataTable dt_SampleTypeList = ds_SampleTypeNames.Tables[0];
                columnNames = new string[dt_SampleTypeList.Rows.Count];
                int n = 0;
                foreach (DataRow dr_SampleTypeList in dt_SampleTypeList.Rows)
                {
                    columnNames[n++] = (string)dr_SampleTypeList.ItemArray[0];
                }
            }
            return columnNames;
        }

        public string[] GetGlobalTagsList()
        {
            DataSet ds_TagNames = new DataSet();
            string[] columnNames = null;
            ds_TagNames.Clear();
            ds_TagNames = myHC.GetDataSetFromSQLCommand("Select Name from global_tags");

            if (ds_TagNames.Tables[0] != null)
            {
                DataTable dt_TagNames = ds_TagNames.Tables[0];
                columnNames = new string[dt_TagNames.Rows.Count];
                int n = 0;
                foreach (DataRow dr_TagNames in dt_TagNames.Rows)
                {
                    columnNames[n++] = (string)dr_TagNames.ItemArray[0];
                }
            }
            return columnNames;
        }


        public string[] GetOperationList(int nCondition)
        {
            DataSet ds_TagNames = new DataSet();
            string[] columnNames = null;
            ds_TagNames.Clear();
            ds_TagNames = myHC.GetDataSetFromSQLCommand("SELECT Name FROM routing_operation_list");

            if (ds_TagNames.Tables[0] != null)
            {
                DataTable dt_TagNames = ds_TagNames.Tables[0];
                columnNames = new string[dt_TagNames.Rows.Count];
                int n = 0;
                foreach (DataRow dr_TagNames in dt_TagNames.Rows)
                {
                    columnNames[n++] = (string)dr_TagNames.ItemArray[0];
                }
            }
            return columnNames;
        }


        public DataTable GetGlobalTagsDataTable()
        {
            DataSet ds_TagNames = new DataSet();
            ds_TagNames.Clear();
            ds_TagNames = myHC.GetDataSetFromSQLCommand("Select idglobal_tags,Name,Value,Type from global_tags");
            DataTable dt_TagNames = null;
            if (ds_TagNames.Tables[0] != null)
            {
                dt_TagNames = ds_TagNames.Tables[0];
            }
            return dt_TagNames;
        }

        public DataTable GetMachineTagsDataTable()
        {
            DataSet ds_TagNames = new DataSet();
            ds_TagNames.Clear();
            ds_TagNames = myHC.GetDataSetFromSQLCommand("Select idmachine_tags,Name,Value,Machine_ID from machine_tags");
            DataTable dt_TagNames = null;
            if (ds_TagNames.Tables[0] != null)
            {
                dt_TagNames = ds_TagNames.Tables[0];
            }
            return dt_TagNames;
        }

        public DataTable GetSamplePositionDataTable()
        {
            DataSet ds_TagNames = new DataSet();
            ds_TagNames.Clear();
            ds_TagNames = myHC.GetDataSetFromSQLCommand(@"SELECT        machine_positions.idmachine_positions as ID, machine_positions.Name AS Position, machines.Name AS Machine
                                                    FROM            machine_positions INNER JOIN
                                                    machines ON machine_positions.Machine_ID = machines.idmachines");
            DataTable dt_TagNames = null;
            if (ds_TagNames.Tables[0] != null)
            {
                dt_TagNames = ds_TagNames.Tables[0];
            }
            return dt_TagNames;
        }

        public DataTable GetOperationDataTable()
        {
            DataSet ds_TagNames = new DataSet();
            ds_TagNames.Clear();
            ds_TagNames = myHC.GetDataSetFromSQLCommand(@"SELECT Name,idrouting_operation_list FROM routing_operation_list");
            DataTable dt_TagNames = null;
            if (ds_TagNames.Tables[0] != null)
            {
                dt_TagNames = ds_TagNames.Tables[0];
            }
            return dt_TagNames;
        }

        public DataTable GetMachinesDataTable()
        {
            DataSet ds_TagNames = new DataSet();
            ds_TagNames.Clear();
            ds_TagNames = myHC.GetDataSetFromSQLCommand(@"SELECT idmachines,Name,Description FROM machines");
            DataTable dt_TagNames = null;
            if (ds_TagNames.Tables[0] != null)
            {
                dt_TagNames = ds_TagNames.Tables[0];
            }
            return dt_TagNames;
        }

      

        public DataTable GetMachinePositionDataTable(bool bNoInternalPositions = false)
        {
            DataSet ds_TagNames = new DataSet();
            ds_TagNames.Clear();
            string SQL_Statement = null;

            SQL_Statement = @"SELECT  machine_positions.idmachine_positions, machine_positions.Name AS Position, machines.Name AS MachineName
                         FROM  machine_positions LEFT OUTER JOIN
                         machines ON machine_positions.Machine_ID = machines.idmachines";
            if (bNoInternalPositions) { SQL_Statement += " WHERE InternalPos=0"; }

            SQL_Statement += " ORDER BY  machines.Name,machine_positions.Name";

            ds_TagNames = myHC.GetDataSetFromSQLCommand(SQL_Statement);
            DataTable dt_TagNames = null;
            if (ds_TagNames.Tables[0] != null)
            {
                dt_TagNames = ds_TagNames.Tables[0];
            }
            return dt_TagNames;
        }

        public DataTable GetWinCCGlobalTagsDataTable()
        {
            DataSet ds_TagNames = new DataSet();
            ds_TagNames.Clear();
            ds_TagNames = myHC.GetDataSetFromSQLCommand(@"SELECT idglobal_tags,Name,Value FROM global_tags");
            DataTable dt_TagNames = null;
            if (ds_TagNames.Tables[0] != null)
            {
                dt_TagNames = ds_TagNames.Tables[0];
            }
            return dt_TagNames;
        }

        public DataTable GetWinCCMachineTagsDataTable(int nMachine_ID)
        {
            string SQL_Statement = null;
            if(nMachine_ID>0){
                SQL_Statement = @"SELECT idmachine_tags,Name,Value FROM machine_tags WHERE Machine_ID=" + nMachine_ID.ToString();
            }else{
                SQL_Statement = @"SELECT idmachine_tags,Name,Value FROM machine_tags";
            }
           
            DataSet ds_TagNames = new DataSet();
            ds_TagNames.Clear();
            ds_TagNames = myHC.GetDataSetFromSQLCommand(SQL_Statement);
            DataTable dt_TagNames = null;
            if (ds_TagNames.Tables[0] != null)
            {
                dt_TagNames = ds_TagNames.Tables[0];
            }
            return dt_TagNames;
        }

        public DataTable GetMachineCommandsDataTable( int nMachine_ID)
        {
            DataSet ds_TagNames = new DataSet();
            ds_TagNames.Clear();
            ds_TagNames = myHC.GetDataSetFromSQLCommand(@"SELECT idmachine_commands,Name,Number FROM machine_commands WHERE Machine_ID=" + nMachine_ID.ToString());
            DataTable dt_TagNames = null;
            if (ds_TagNames.Tables[0] != null)
            {
                dt_TagNames = ds_TagNames.Tables[0];
            }
            return dt_TagNames;
        }
        public DataTable GetSampleValuesFromSampleActice_ID(int nSampleActive_ID)
        {
            string SQL_Statement = "Select Name,Value FROM sample_values WHERE ActiveSample_ID=" + nSampleActive_ID;
            DataSet ds_TagNames = new DataSet();
            ds_TagNames.Clear();
            ds_TagNames = myHC.GetDataSetFromSQLCommand(SQL_Statement);
            DataTable dt_TagNames = null;
            if (ds_TagNames.Tables[0] != null)
            {
                dt_TagNames = ds_TagNames.Tables[0];
            }
            return dt_TagNames;
        }

        public DataTable GetSampleProgramsDataTable()
        {
            string SQL_Statement = "Select idsample_programs,Name,Description FROM sample_programs";
            DataSet ds_TagNames = new DataSet();
            ds_TagNames.Clear();
            ds_TagNames = myHC.GetDataSetFromSQLCommand(SQL_Statement);
            DataTable dt_TagNames = null;
            if (ds_TagNames.Tables[0] != null)
            {
                dt_TagNames = ds_TagNames.Tables[0];
            }
            return dt_TagNames;
        }

        public DataTable GetStatusBitsDataTable()
        {
           string SQL_Statement = @"SELECT  machine_state_signals.idmachine_state_signals, machines.Name AS Machine, machine_state_signals.Name 
                                    FROM  machine_state_signals INNER JOIN
                                    machines ON machine_state_signals.Machine_ID = machines.idmachines WHERE machine_state_signals.signal_type='Status'";
            DataSet ds_TagNames = new DataSet();
            ds_TagNames.Clear();
            ds_TagNames = myHC.GetDataSetFromSQLCommand(SQL_Statement);
            DataTable dt_TagNames = null;
            if (ds_TagNames.Tables[0] != null)
            {
                dt_TagNames = ds_TagNames.Tables[0];
            }
            return dt_TagNames;
        }

        public String GetMachineTagValue(string strName)
        {
            string retString = null;
            try
            {
                retString = myHC.return_SQL_StatementAsString("SELECT Value FROM machine_tags WHERE Name='" + strName + "'");
            }
            catch (System.NullReferenceException) { }
            catch (Exception ex) { mySave.InsertRow((int)Definition.Message.D_ALARM, ex.ToString()); }
            return retString;
        }

        public int GetMachineTagType(string strName)
        {
            int retType = -1;
            try
            {
                retType = myHC.return_SQL_Statement("SELECT Type FROM machine_tags WHERE Name='" + strName + "'");
            }
            catch (System.NullReferenceException) { }
            catch (Exception ex) { mySave.InsertRow((int)Definition.Message.D_ALARM, ex.ToString()); }
            return retType;
        }

        public string GetGlobalTagValue(string strName)
        {
            string retString = null;
            try
            {
                retString = myHC.return_SQL_StatementAsString("SELECT Value FROM global_tags WHERE Name='" + strName + "'");
            }
            catch (System.NullReferenceException ) {}
            catch (Exception ex) { MessageBox.Show(ex.ToString()); mySave.InsertRow((int)Definition.Message.D_ALARM, ex.ToString()); }
            return retString;
        }

        public int GetGlobalTagType(string strName)
        {
            int retType = -1;
            try
            {
                retType = myHC.return_SQL_Statement("SELECT Type FROM global_tags WHERE Name='" + strName + "'");
            }
            catch (System.NullReferenceException) { }
            catch (Exception ex) { mySave.InsertRow((int)Definition.Message.D_ALARM, ex.ToString()); }
            return retType;
        }

        public int GetMachinePosition_IDFromRoutingCondition(int routingPositionEntry_ID)
        {
            int nMachinePaosition_ID = -1;
                nMachinePaosition_ID = myHC.GetMachinePosition_IDFromRoutingCondition(routingPositionEntry_ID);
            return nMachinePaosition_ID;
        }

        public int GetSampleActive_IDIfOnMachinePosition(int nMachinePosition_ID)
        {
            int nSample_active_ID = -1;
            nSample_active_ID = myHC.GetSampleActive_IDIfOnMachinePosition(nMachinePosition_ID);
            return nSample_active_ID;
        }

        
        public String GetSampleValuesFromSampleActice_ID(int nSampleActive_ID, string strName)
        {
            string SQL_Statement = "Select Value FROM sample_values WHERE ActiveSample_ID=" + nSampleActive_ID + " AND Name LIKE '" + strName + "'";
            string strValue = null;
            try
            {
                strValue = myHC.return_SQL_StatementAsString(SQL_Statement);
            }
            catch { }
            return strValue;
        }


        public int  GetMachinePositionIDFromMachinePositionsByName(string strName)
        {
            string SQL_Statement;
            int nValue = -1;
            SQL_Statement = "SELECT idmachine_positions FROM machine_positions WHERE Name LIKE '" +strName+"'";
            nValue = myHC.return_SQL_Statement(SQL_Statement);
            return nValue;
        }

        public int GetSampleActive_IDFromSampleActiveByMachinePosition_ID(int nMachinePosition_ID)
        {
            string SQL_Statement;
            int nValue = -1;
            SQL_Statement = "SELECT idactive_samples FROM sample_active WHERE ActualSamplePosition_ID=" + nMachinePosition_ID;
            nValue = myHC.return_SQL_Statement(SQL_Statement);
            return nValue;
        }

        public int GetSampleReservation_IDFromSampleReservationByMachineID(int nMachinePosition_ID, int nSample_ID=-1)
        {
            string SQL_Statement;
            int nValue = -1;
            SQL_Statement = "SELECT idsample_reservation FROM sample_reservation WHERE ActualSamplePosition_ID=" + nMachinePosition_ID;
            if (nSample_ID >= 0)
            {
                SQL_Statement = SQL_Statement + " AND NOT ActiveSample_ID=" + nSample_ID;
            }
            nValue = myHC.return_SQL_Statement(SQL_Statement);
            return nValue;
        }
        public int GetSamplePriorityFromSampleActiveBySample_ID(int nSampleID)
        {
            string SQL_Statement;
            int nValue = -1;
            SQL_Statement = "SELECT Priority FROM sample_active WHERE idactive_samples=" + nSampleID;
            nValue = myHC.return_SQL_Statement(SQL_Statement);
            return nValue;
        }

        public int GetSampleTypeFromSampleActiveBySample_ID(int nSampleID)
        {
            string SQL_Statement;
            int nValue = -1;
            int nSampleProgramType_ID = -1;

            SQL_Statement = "SELECT SampleProgramType_ID FROM sample_active WHERE idactive_samples=" + nSampleID;
            nSampleProgramType_ID = myHC.return_SQL_Statement(SQL_Statement);
            SQL_Statement = "SELECT SampleTypeList_ID FROM sample_programs WHERE idsample_programs=" + nSampleProgramType_ID;
            nValue = myHC.return_SQL_Statement(SQL_Statement);
            return nValue;
        }


        public bool CheckStatusBitFromMachineStausBitsByStatusBits_ID(int nStausbit_ID )
        {
            bool ret = false;
            string SQL_Statement = "";
          //  int nValue =-1;
            bool bValue = false;
            if (nStausbit_ID > 0)
            {
                SQL_Statement = "SELECT Value FROM machine_state_signals WHERE idmachine_state_signals=" + nStausbit_ID + "";

                bValue = myHC.return_SQL_StatementAsBool(SQL_Statement);
                if (bValue) { ret = true; } else { ret = false; }
                return ret;
            }
            else
            {
                mySave.InsertRow((int)Definition.Message.D_ALARM, ":CheckStatusBitFromMachineStausBitsByStatusBits_ID: no Stausbit_ID given ");
                return ret;
            }
        }

        public string Get_SQL_StatementRoutingPositionEntriesFromRoutingConditions()
        {
            string SQL_Statement;
            SQL_Statement = "SELECT DISTINCT RoutingPositionEntry_ID FROM routing_conditions";
            return SQL_Statement;

        }

        public string Get_SQL_StatementCondition_ComplyFromRoutingConditionsBYRoutingPositionEntry_ID(int RoutingPositionEntry_ID)
        {
            string SQL_Statement;
            SQL_Statement = "SELECT Condition_comply FROM routing_conditions WHERE RoutingPositionEntry_ID=" + RoutingPositionEntry_ID;
            return SQL_Statement;

        }

        public string Get_SQL_StatementCommandsBYRoutingPositionEntry_ID(int RoutingPositionEntry_ID)
        {
            string SQL_Statement;
            SQL_Statement = "SELECT idrouting_commands,CommandValue0,Command_ID,CommandValue1,CommandValue2,CommandValue3 FROM routing_commands WHERE RoutingPositionEntry_ID=" + RoutingPositionEntry_ID;
            return SQL_Statement;

        }

        public string GetActiveSamples()
        {
            string SQL_Statement;
            SQL_Statement = "SELECT * FROM sample_active WHERE Command_active=0 AND Magazine=0 Order BY Priority DESC";
            return SQL_Statement;

        }
        public int GetRoutingPosition_IDFromRoutingPositionsByMachinePosition_ID(int nMachinePosition_ID)
        {
            string SQL_Statement;
            int nValue = -1;
            SQL_Statement = "SELECT idrouting_positions FROM routing_positions WHERE Machine_Position_ID=" + nMachinePosition_ID;
            nValue = myHC.return_SQL_Statement(SQL_Statement);
            return nValue;
        }
        public int GetSampleTypeFromSampleProgramsBySamplePrograms_ID(int nSampleProgram_ID)
        {
            string SQL_Statement;
            int nValue = -1;
            SQL_Statement = "SELECT SampleTypeList_ID FROM sample_programs WHERE idsample_programs=" + nSampleProgram_ID;
            nValue = myHC.return_SQL_Statement(SQL_Statement);
            return nValue;
        }
         
        public String Get_SQL_StatementForRoutingPositionEntriesByRoutingPositionAndSampleType(int nRoutingPosition, int nSampleType)
        {
            string SQL_Statement;

            SQL_Statement = "SELECT idrouting_position_entries,TimeForWarning,TimeForAlarm,ActualTime FROM routing_position_entries WHERE Position_ID=" + nRoutingPosition + " AND SampleType_ID=" + nSampleType;
           
            return SQL_Statement;
        }

        public int GetProgram_IDFromSampleProgramsMachineProgramsByMachineList_IDSampleTypeSampleProgram_ID(int nMachine_List_ID, int nSampleType, int nSampleProgram_ID)
        {
           return (int)myHC.return_SQL_Statement(@"SELECT  machineProgram_ID
                         FROM  sample_programs_machineprograms
                         WHERE (MachineList_ID=" + nMachine_List_ID + ") and (SampleTypeList_ID=" + nSampleType + ") and (SampleProgram_ID=" + nSampleProgram_ID + ")");
        }
       
        public void InsertCommandIntoCommandTable(int nMachine_ID, int nCommand, int nSample_ID, int nProgram_ID)
        {
            string SQL_Command = null;
            int nProgramNumber = -1;
            try
            {
                nProgramNumber = GetProgramNumberFromMachineProgramssByMachinePrograms_ID(nProgram_ID);
            }
            catch (Exception ex) { mySave.InsertRow((int)Definition.Message.D_ALARM, "Routing::InsertCommandIntoCommandTable: can't get the program number!\r\n" + ex.ToString()); }

            SQL_Command = "Insert INTO command_active (MachineCommand_ID, Program_ID, Sample_ID) Values(" + nCommand + ", " + nProgramNumber + "," + nSample_ID + ")";
            myHC.return_SQL_Statement(SQL_Command);
        }
       
      
        public bool DeleteWSEntryFromSampleValuesBySample_IDAndValueName(int nSample_ID, string ValueName)
        {
            bool ret = false;
            string SQL_Statement = "DELETE FROM sample_values WHERE ActiveSample_ID=" + nSample_ID + " AND Name LIKE '" + ValueName + "'";

            if (myHC.return_SQL_Statement(SQL_Statement) == 0) { ret = true; }
            return ret;
        }

        public bool InsertWSEntryFromSampleValuesBySample_IDAndValueName(int nSample_ID, string strValueName, string strValue,string strSampleID, bool bHiddenFlag)
        {
            bool ret = false;
            int nSampleValue_ID = -1;
            string SQL_Statement = "SELECT idsample_values FROM sample_values WHERE ActiveSample_ID=" + nSample_ID + " AND Name='" + strValueName +"'";

            nSampleValue_ID = myHC.return_SQL_Statement(SQL_Statement);

            if (nSampleValue_ID > 0)   // if entry exist update value else insert new entry
            {
                SQL_Statement = "UPDATE sample_values SET Value='" + strValue + "' WHERE idsample_values=" + nSampleValue_ID;
            }
            else    // else: insert new entry with the values
            {
                int nHidden = bHiddenFlag ? 1 : 0;
                SQL_Statement = "INSERT INTO sample_values (ActiveSample_ID,Name,Value,SampleID,Hidden) VALUES ('" + nSample_ID + "','" + strValueName + "','" + strValue + "','" + strSampleID + "'," + nHidden + ")";
            }
            if (myHC.return_SQL_Statement(SQL_Statement) == 0) { ret = true; }
            return ret;
        }

        public bool WriteWinCCTagFromSampleValuesByValueName(string strName, string strValue, int nType)
        {
            bool ret = false;
            try
            {
                if (myHC.WriteWinCCTagFromSampleValuesByValueName(strName, strValue, nType)) { ret = true; }
            }
            catch (Exception ex) { mySave.InsertRow((int)Definition.Message.D_ALARM, "strName:" + strName + " strValue:" + strValue + " nType:" + nType + "\r\n" + ex.ToString()); }
            return ret;
        }

        public bool ChangePriorityValueInSampleBySample_ID(int nSample_ID, string strValue)
        {
             bool ret = false;
             string SQL_Statement = "UPDATE sample_active SET Priority=" + strValue  + " WHERE idactive_samples=" + nSample_ID ;

             if (myHC.return_SQL_Statement(SQL_Statement) == 0) { ret = true; }
             return ret;
        }

        public bool ChangeSampleTypeInSampleBySample_ID(int nSample_ID, string strValue)
        {
             bool ret = false;
             string SQL_Statement = "UPDATE sample_active SET SampleProgramType_ID=" + strValue  + " WHERE idactive_samples=" + nSample_ID ;

             if (myHC.return_SQL_Statement(SQL_Statement) == 0) { ret = true; }
             return ret;
        }
       
        public bool ShiftSampleToPositionXBySample_ID(int nSample_ID, string strValue)
        {
             bool ret = false;
             string SQL_Statement = "UPDATE sample_active SET ActualSamplePosition_ID=" + strValue  + " WHERE idactive_samples=" + nSample_ID ;

             if (myHC.return_SQL_Statement(SQL_Statement) == 0) { ret = true; }

          return ret;
        }
      
        public bool SetCommandActiveOnSampleActiveBySample_ID(int nSample_ID, int nValue, int nMachinePosition_ID)
        {
            bool ret = false;
            string SQL_Statement = "UPDATE sample_active SET Command_Active=" + nValue.ToString() + ",LastCommandActionPosition_ID=" + nMachinePosition_ID + " WHERE idactive_samples=" + nSample_ID;

            if (myHC.return_SQL_Statement(SQL_Statement) == 0) { ret = true; }
            return ret;
        }

        public void CheckIfCommandActiveOnSampleActive()
        {
            int nSample_ID = -1;
            string SQL_StatementNONActiveSamples = "SELECT ActualSamplePosition_ID,LastCommandActionPosition_ID,idactive_samples from sample_active WHERE Command_Active=1";
            DataSet dsNONActiveSamples = new DataSet();
            dsNONActiveSamples.Clear();
            dsNONActiveSamples = myHC.GetDataSetFromSQLCommand(SQL_StatementNONActiveSamples);
            DataTable dtNONActiveSamples = new DataTable();

            if (dsNONActiveSamples.Tables.Count > 0)
            {
                if (dsNONActiveSamples.Tables[0] != null)
                {
                    dtNONActiveSamples = dsNONActiveSamples.Tables[0];

                    // check all samples 
                    foreach (DataRow drNONActiveSample in dtNONActiveSamples.Rows)
                    {
                        int nActualPosition = -1;
                        int nLastActionPosititon = -1;

                        try
                        {
                            nActualPosition = Int32.Parse(drNONActiveSample["ActualSamplePosition_ID"].ToString());
                            nLastActionPosititon = Int32.Parse(drNONActiveSample["LastCommandActionPosition_ID"].ToString());
                        }
                        catch (Exception ex) { mySave.InsertRow((int)Definition.Message.D_ALARM, ex.ToString()); }
                        if (nActualPosition != nLastActionPosititon)
                        {
                            try
                            {
                                nSample_ID = Int32.Parse(drNONActiveSample["idactive_samples"].ToString());
                            }
                            catch (Exception ex) { mySave.InsertRow((int)Definition.Message.D_ALARM, ex.ToString()); }
                            string SQL_Statement = "UPDATE sample_active SET Command_Active=0 WHERE idactive_samples=" + nSample_ID;
                            myHC.return_SQL_Statement(SQL_Statement);
                        }
                    }
                }
            }
            else { mySave.InsertRow((int)Definition.Message.D_ALARM, "no table found for SQL-Command: " + SQL_StatementNONActiveSamples); }
        }

        public bool SetCommandValue0OnRoutingCommandsByRoutingCommand_ID(int nMachine_ID, int nRoutingCommand_ID)
        {
            bool ret = false;
            string SQL_Statement = "UPDATE routing_Commands SET CommandValue0=" + nMachine_ID.ToString() + " WHERE idrouting_commands=" + nRoutingCommand_ID.ToString();

            if (myHC.return_SQL_Statement(SQL_Statement) == 0) { ret = true; }
            return ret;
        }

        public bool InsertSampleInSample_ActiveBySampleProgrammType_IDAndSampleIDAndPriority(int nSampleProgramType_ID, string strSampleID , int nPriority, int  nPosition_ID)
        {
            bool ret = false;
            int nSample_ID = -1;

            string SQL_Statement = "SELECT idactive_samples FROM sample_active WHERE SampleID LIKE '" + strSampleID + "'";
            nSample_ID = myHC.return_SQL_Statement(SQL_Statement);

            if (nSample_ID > 0) //if sample allready registered write error message
            {
                mySave.InsertRow((int)Definition.Message.D_ALARM, @"Routing::InsertSampleInSample_ActiveBySampleProgrammType_IDAndSampleIDAndPriority: allready found a sample with name:'" + strSampleID + "'");
                    return false;
            }else{
                if (nSampleProgramType_ID >= 0 && nPriority >= 0 && strSampleID.Length > 2 && nPosition_ID > 0)
                {
                    SQL_Statement = "INSERT INTO sample_active (SampleProgramType_ID,SampleID,Priority,ActualSamplePosition_ID,StartPosition_ID) VALUES ('" + nSampleProgramType_ID + "','" + strSampleID + "','" + nPriority + "','" + nPosition_ID + "','" + nPosition_ID + "')";
                    int r = myHC.return_SQL_Statement(SQL_Statement);
                   
                    ret = true; 
                }
                else
                {
                    mySave.InsertRow((int)Definition.Message.D_ALARM, @":InsertSampleInSample_ActiveBySampleProgrammType_IDAndSampleIDAndPriority: at least one value not given or wrong format: SampleProgramType_ID="
                         + nSampleProgramType_ID + " SampleID='" + strSampleID + "' Priority=" + nPriority + " Position=" + nPosition_ID);
                    ret = false;
                }
            }
            return ret;
        }

        public bool DeleteSampleInSample_ActiveBySampleID(int nSample_ID)
        {
            bool ret = false;
            ret = myHC.DeleteSampleFromSampleActiveBySample_ID(nSample_ID);
            return ret;
        }

        public bool CheckIfCommandForMachineAllreadyExist(int nMachine_ID)
        {
            bool ret = false;
            try
            {
                string SQL_Statement = "SELECT CheckIfCommandForMachineAllreadyExist(" + nMachine_ID.ToString() + ")";

                ret = myHC.return_SQL_StatementAsBool(SQL_Statement);
            }
            catch (Exception ex) { mySave.InsertRow((int)Definition.Message.D_ALARM, @"Routing::CheckIfCommandForMachineAllreadyExist:Exception ->\r\n" + ex.ToString()); }
            return ret;
        }
       
        public int GetPosition_IDFromRoutingPositionsByRoutingPositionEntry_ID(int nRoutingPositionEntry_ID)
        {
           return (int)myHC.return_SQL_Statement(@"SELECT        routing_positions.Machine_Position_ID
                        FROM            routing_position_entries INNER JOIN
                        routing_positions ON routing_position_entries.Position_ID = routing_positions.idrouting_positions
                        WHERE        (routing_position_entries.idrouting_position_entries = " + nRoutingPositionEntry_ID.ToString() + ")");
        }

        public int GetSampleType_IDFromRoutingPositionsByPosition_ID(int nPos_ID)
        {
            return (int)myHC.return_SQL_Statement(@"SELECT        sample_programs.SampleTypeList_ID
                        FROM            sample_active INNER JOIN
                         sample_programs ON sample_active.SampleProgramType_ID = sample_programs.idsample_programs
                      WHERE        (sample_active.ActualSamplePosition_ID = " + nPos_ID.ToString() + ")");
        }

        public int GetConnectionType_IDFromConnectionListByMachine_ID(int nMachine_ID)
        {
            return (int)myHC.return_SQL_Statement(@"SELECT        connection_type_list.idtype_list
            FROM            machine_list INNER JOIN
                         machines ON machine_list.idmachine_list = machines.Machine_list_ID INNER JOIN
                         connection_type_list ON machine_list.Connection_type_list_ID = connection_type_list.idtype_list
            WHERE        (machines.idmachines = " + nMachine_ID.ToString() + ")");
        }

        public int GetNumberFromMachineCommandsByMachineCommands_ID(int nMachineCommands_ID)
        {
            return (int)myHC.return_SQL_Statement(@"SELECT        Number   FROM machine_commands
            WHERE       idmachine_commands = " + nMachineCommands_ID.ToString() + "");
        }

        public string GetSampleIDFromsampleActiveByMachinePosition_ID(int nActualPosition_ID)
        {
            string retString = null;
            try
            {
                retString = myHC.return_SQL_StatementAsString(@"SELECT        SampleID
                    FROM            sample_active
                    WHERE        (ActualSamplePosition_ID = " + nActualPosition_ID.ToString() + ")");
            }
            catch { }
            return retString;
        }

        public int GetProgramNumberFromMachineProgramssByMachinePrograms_ID(int nMachinePrograms_ID)
        {
            return (int)myHC.return_SQL_Statement(@"SELECT        Program_Number   FROM machine_programs
            WHERE       idmachine_programs = " + nMachinePrograms_ID.ToString() + "");
        }

        public bool InsertSampleInSample_Reservation(int nSampleProgramType_ID, string strSampleID, int nPosition_ID, int nSample_ID)
        {
            bool ret = false;
            string SQL_Statement = null;
          
                if (nSampleProgramType_ID >= 0 && strSampleID.Length > 2 && nPosition_ID > 0)
                {
                    SQL_Statement = "INSERT INTO sample_reservation (SampleProgramType_ID,SampleID,ActualSamplePosition_ID,ActiveSample_ID) VALUES ('" + nSampleProgramType_ID + "','" + strSampleID + "','" + nPosition_ID + "','" + nSample_ID + "')";
                    int r = myHC.return_SQL_Statement(SQL_Statement);

                    ret = true;
                }
                else
                {
                    mySave.InsertRow((int)Definition.Message.D_ALARM, @":InsertSampleInSample_ReservationBySampleProgrammType_IDAndSampleIDAndPosition: at least one value not given or wrong format: SampleProgramType_ID="
                         + nSampleProgramType_ID + " SampleID='" + strSampleID + "'  Position=" + nPosition_ID);
                    ret = false;
                }
            
            return ret;
        }

        public bool DeleteReservationOnSample_ReservationByPosition_ID(int nPosition_ID)
        {
            bool ret = false;
            try
            {
               // string SQL_Statement = "CALL DeleteReservationOnSample_Active(" + nPosition_ID.ToString() + ")";
                string SQL_Statement = "DELETE FROM sample_reservation WHERE (ActualSamplePosition_ID=" + nPosition_ID.ToString() + ")";
                ret = myHC.return_SQL_StatementAsBool(SQL_Statement);
            }
            catch (Exception ex) { mySave.InsertRow((int)Definition.Message.D_ALARM, @"Routing::DeleteReservationOnSample_ActiveByPosition_ID:Exception ->\r\n" + ex.ToString()); }
            return ret;
        }

        public bool DeleteReservationOnSample_ReservationBySampleID(string strSampleID)
        {
            bool ret = false;
            try
            {
                string SQL_Statement = "DELETE FROM sample_reservation WHERE (SampleID=" + strSampleID + ")";
                ret = myHC.return_SQL_StatementAsBool(SQL_Statement);
            }
            catch (Exception ex) { mySave.InsertRow((int)Definition.Message.D_ALARM, @"Routing::DeleteReservationOnSample_ActiveByPosition_ID:Exception ->\r\n" + ex.ToString()); }
            return ret;
        }

        public bool ChangeSampleIDBySample_ID(int nSample_ID, string strNewSampleID)
        {
            bool ret = false;
            try
            {
                string SQL_Statement = "CALL ChangeSampleIDBySample_ID(" + nSample_ID + ",'" + strNewSampleID + "')";
                ret = myHC.return_SQL_StatementAsBool(SQL_Statement);
            }
            catch (Exception ex) { mySave.InsertRow((int)Definition.Message.D_ALARM, @"Routing::ChangeSampleIDBySample_ID:Exception ->\r\n" + ex.ToString()); }
            return ret;
        }

        public bool CheckIfOwnMagazinePosIsOccupied(int nSample_ID)
        {
            bool ret = true;
            string  strMagazine = null;
            string  strMagazinePos = null;
            int nSample_IDFound = -1;
            try
            {
                string SQL_Statement = "SELECT Value FROM sample_values WHERE Name='MAGAZINE' AND ActiveSample_ID=" + nSample_ID;
                strMagazine = myHC.return_SQL_StatementAsString(SQL_Statement);
                SQL_Statement = "SELECT Value FROM sample_values WHERE Name='MAGAZINE_POS' AND ActiveSample_ID=" + nSample_ID;
                strMagazinePos = myHC.return_SQL_StatementAsString(SQL_Statement);

              
                if (strMagazine != null && strMagazinePos != null)
                {
                    SQL_Statement = "SELECT idactive_samples FROM sample_active WHERE Magazine=" + strMagazine + " AND MagazinePos=" + strMagazinePos;
                    nSample_IDFound = myHC.return_SQL_Statement(SQL_Statement);
                    if (nSample_IDFound <= 0) { ret = false; }
                }
            }
            catch (Exception ex) { mySave.InsertRow((int)Definition.Message.D_ALARM, @"Routing::CheckIfOwnMagazinePosIsFree:Exception ->\r\n" + ex.ToString()); }
            return ret;
        }

        
    }
}