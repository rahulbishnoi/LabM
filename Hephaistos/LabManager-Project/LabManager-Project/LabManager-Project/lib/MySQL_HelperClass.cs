using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data;
using Logging;
using Definition;
using cs_IniHandlerDevelop;
using System.Diagnostics;



namespace MySQL_Helper_Class
{
    class MySQL_HelperClass
    {
        private string _ConnectionString = null;
        private Save mySave = new Save("MySQL Helper Class");
        private Definitions definitions = new Definitions();
        private IniStructure inis = new IniStructure();
        private Object MySQLLock = new Object();
        private static int nConnections = 0;
        private double nSQL_Statement_Calls = 0;
        private static double nSQL_Statement_Calls_total = 0;
        private  bool bMessageBoxShow = true;
        private  MySqlConnection connectionReturnObect = null;
        private  MySqlCommand commandReturnObect = null;

        private bool bWriteMySQL_Log = false;
        private  System.IO.StreamWriter file;
        DateTime dtStart;
      
        public MySQL_HelperClass()
        {
          if (_ConnectionString == null)
            {
                _ConnectionString = SetConnectionString();
            }

              connectionReturnObect = new MySqlConnection(_ConnectionString);
              commandReturnObect = new MySqlCommand();
              commandReturnObect.Connection = connectionReturnObect;
              commandReturnObect.CommandType = System.Data.CommandType.Text;

              
                if (connectionReturnObect != null)
                {
                    try
                    {
                       
                        connectionReturnObect.Open();
                    }
                    catch (MySql.Data.MySqlClient.MySqlException ex)
                    {
                        PrintMySQLDBException(ex);
                        mySave.InsertRow((int)Definition.Message.D_ALARM, ex.ToString());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Unknown Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                   
                }
          
         
            nConnections++;
           // Console.WriteLine("connections+:" + nConnections );
            
            //for testing only
            if (bWriteMySQL_Log )
            {
               
                file = new System.IO.StreamWriter(definitions.LogPath + @"MySQL_Log_" + nConnections + ".txt");
            }

            // for testing 
            dtStart = DateTime.Now; 
            
        }


       private void FileWriteLine(string SQL_Statement)
       {
           if (bWriteMySQL_Log)
           {
               
               file.WriteLine(Environment.StackTrace + "\r\n" + DateTime.Now.ToString("HH:mm:ss tt") + " Timestamp " + Stopwatch.GetTimestamp().ToString() + ": -->" + SQL_Statement + "<--\r\n---------------------------------------------------------------------------------------\r\n");
       
           }
       }

        ~MySQL_HelperClass()
        {
            if (bWriteMySQL_Log)
            {
                DateTime dtStop = DateTime.Now;
                TimeSpan runTime = dtStop - dtStart;

                Console.WriteLine("connection {0}: {1}/seconds ({2} runtime)", nConnections, (nSQL_Statement_Calls / runTime.TotalSeconds), runTime.TotalSeconds.ToString());
                nConnections--;
                if (nConnections == 0)
                {
                    Console.WriteLine("total calls: {0} in {1} seconds ({2}/seconds) ", nSQL_Statement_Calls_total, runTime.TotalSeconds.ToString(), (nSQL_Statement_Calls_total / runTime.TotalSeconds));
                }
            }
            if (connectionReturnObect != null)
            {
                if ((connectionReturnObect.State & global::System.Data.ConnectionState.Open) == global::System.Data.ConnectionState.Open)
                {
                    connectionReturnObect.Close();
                    commandReturnObect.Dispose();
                }
            }
        
        }

        public string GetConnectionString()
        {
            return _ConnectionString;
        }

        private string SetConnectionString()
        {
          string conString = "";
              string IniFilePath = definitions.PathIniFile;
            inis = IniStructure.ReadIni(IniFilePath);

            if (inis == null)
                MessageBox.Show("Something went wrong with the ini-Reader!\r\nerror reading from file: " + IniFilePath, "error");

            //conString = "server= " + inis.GetValue("DB", "server") + ";User Id=" + inis.GetValue("DB", "UserId") + ";password=" + inis.GetValue("DB", "password") + ";database=" + inis.GetValue("DB", "database") + ";Persist Security Info=no";
            conString = definitions.ConnectionString;
            return conString;
        }

      
       
        public bool ConnectionTest()
        {
            MySql.Data.MySqlClient.MySqlConnection connection = new global::MySql.Data.MySqlClient.MySqlConnection();
           
            connection.ConnectionString = _ConnectionString;
            try
            {
                MySql.Data.MySqlClient.MySqlCommand mycommand = new global::MySql.Data.MySqlClient.MySqlCommand();
                mycommand.Connection = connection;
                //  mycommand.CommandText = SQL_Statement;
                mycommand.CommandType = global::System.Data.CommandType.Text;
                mycommand.Connection.Open();

                if (mycommand.Connection.State != global::System.Data.ConnectionState.Open)
                {
                    return false;
                }
                else
                {
                    mycommand.Connection.Close();
                    if (connection != null)
                    {
                        connection.Close();
                    }
                    return true;
                }
            }
            catch (MySqlException ex)
            {
                PrintMySQLDBException(ex);
                return false;
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); return false; }
        }

        private void PrintMySQLDBException(MySqlException ex)
        {
            //When handling errors, you can your application's response based 
            //on the error number.
            //The two most common error numbers when connecting are as follows:
            //0: Cannot connect to server.
            //1045: Invalid user name and/or password.
               
            switch (ex.Number)
            {
                case 0:
                    if (bMessageBoxShow)
                    {
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                    }
                    bMessageBoxShow = false;
                    break;

                case 1040:
                    if (bMessageBoxShow)
                    {
                        MessageBox.Show("Too many connections to DB");
                    }
                    bMessageBoxShow = false;
                    break;

                case 1045:
                    if (bMessageBoxShow)
                    {
                     MessageBox.Show("Invalid username/password, please try again");
                     }
                    bMessageBoxShow = false;
                    break;

                default:
                    mySave.InsertRow((int)Definition.Message.D_ALARM, " MySQL-Exception number:" + ex.Number + "\r\n" + ex.ToString());
                    Console.WriteLine(" MySQL-Exception number:" + ex.Number + "\r\n" + ex.ToString());
                    break;
            }
        }

        public object returnObectBySQLStatement(string SQL_Statement)
        {
             FileWriteLine(SQL_Statement); 
           
            object returnValue = null;

            
            if (SQL_Statement != null)
            {

                try
                {
                    lock (MySQLLock)
                    {

                        nSQL_Statement_Calls++;
                        nSQL_Statement_Calls_total++;
                    
                        commandReturnObect.CommandText = SQL_Statement;

                        try
                        {
                            returnValue = commandReturnObect.ExecuteScalar();
                        }
                        catch (MySqlException myEx) { PrintMySQLDBException(myEx); }
                        catch (InvalidOperationException ioEx) {
                            mySave.InsertRow((int)Definition.Message.D_ALARM, "MySQLHelper::returnObectBySQLStatement: InvalidOperation Exception:\r\n" + SQL_Statement + "\r\n" + ioEx.ToString());
                        }
                        catch (Exception ex)
                        {
                            mySave.InsertRow((int)Definition.Message.D_ALARM, "MySQLHelper::returnObectBySQLStatement: \r\n" + SQL_Statement + "\r\n" + ex.ToString());
                        }
                    }
                }
                 catch (Exception ex)
                {
                    mySave.InsertRow((int)Definition.Message.D_ALARM, " " + SQL_Statement + "\r\n" + ex.ToString());
                }
            }
            return returnValue;
        }

        public int return_SQL_Statement(string SQL_Statement, string ConnectionString = null)
        {

            FileWriteLine(SQL_Statement); 
            int retValue = -1;
            object returnValue = null;

            if (SQL_Statement.Length > 0)
            {
                returnValue = returnObectBySQLStatement(SQL_Statement);
            }
            else
            {
                mySave.InsertRow((int)Definition.Message.D_ALARM, "MySQLHelper::return_SQL_Statement: SQL statement too short");
            }
                if ((returnValue == null) || (returnValue.GetType() == typeof(global::System.DBNull)))
                {
                    //return new global::System.Nullable<int>();
                    retValue = -1;
                    return retValue;
                }
                else
                {
                    try
                    {
                        retValue = (int)(returnValue);
                    }
                    catch (Exception ex)
                    { 
                        mySave.InsertRow((int)Definition.Message.D_ALARM, "Exception! \n\n" + SQL_Statement + "\n\n" + ex.ToString());
                    }

                    return (retValue);
                }
        }

  
        public double return_SQL_StatementAsDouble(string SQL_Statement)
        {
            FileWriteLine(SQL_Statement); 
            object returnValue = null;

            if (SQL_Statement.Length > 0)
            {
                returnValue = returnObectBySQLStatement(SQL_Statement);
            }
            else
            {
                mySave.InsertRow((int)Definition.Message.D_ALARM, "MySQLHelper::return_SQL_StatementAsDouble: SQL statement too short");
            }
            if (((returnValue == null) || (returnValue.GetType() == typeof(global::System.DBNull))))
            {
              
                return -1;
            }
            else
            {
                 return ((double)(System.Double.Parse((string)returnValue)));
            }
        }

        public bool return_SQL_StatementAsBool(string SQL_Statement)
        {
            FileWriteLine(SQL_Statement); 

            object returnValue = null;
            if (SQL_Statement.Length > 0)
            {
                returnValue = returnObectBySQLStatement(SQL_Statement);
            }
            else
            {
                mySave.InsertRow((int)Definition.Message.D_ALARM, "MySQLHelper::return_SQL_StatementAsBool: SQL statement too short");
            }
            if (((returnValue == null) || (returnValue.GetType() == typeof(global::System.DBNull))))
            {

                return false;
            }
            else
            {
                return ((bool)(returnValue));
            }
        }

        public string return_SQL_StatementAsString(string SQL_Statement, string ConnectionString = null)
        {
            FileWriteLine(SQL_Statement); 

            object returnValue = null;
            if (SQL_Statement.Length > 0)
            {
                returnValue = returnObectBySQLStatement(SQL_Statement);

            }
            else
            {
                mySave.InsertRow((int)Definition.Message.D_ALARM, "MySQLHelper::return_SQL_StatementAsString: SQL statement too short");
            }
            if (((returnValue == null) || (returnValue.GetType() == typeof(global::System.DBNull))))
            {
               // return (string) new global::System.Nullable<string>();
                return null;
            }
            else
            {
                return ((string)(returnValue.ToString()));
            }
        }

        public DateTime return_SQL_StatementAsTimestamp(string SQL_Statement)
        {
            FileWriteLine(SQL_Statement); 

            object returnValue = null;
            if (SQL_Statement.Length > 0)
            {
                returnValue = returnObectBySQLStatement(SQL_Statement);

                if (((returnValue == null) || (returnValue.GetType() == typeof(global::System.DBNull))))
                {
                    mySave.InsertRow((int)Definition.Message.D_DEBUG, "<null> was returned by the following SQL statement: " + SQL_Statement);
                    //  return (DateTime)new global::System.Nullable<DateTime>();
                    return new DateTime(0);

                }
                else
                {
                    return ((DateTime)(returnValue));
                }
            }
            else
            {
                mySave.InsertRow((int)Definition.Message.D_ALARM, "MySQLHelper::return_SQL_StatementAsTimestamp: SQL statement too short");
            }
            return new DateTime(0); 
        }

        public void executeNonQuery_SQL_Statement(string SQL_Statement)
        {
            FileWriteLine(SQL_Statement);
            if (SQL_Statement.Length > 0)
            {
                lock (MySQLLock)
                {
                    object returnValue;
                    try
                    {
                        commandReturnObect.CommandText = SQL_Statement;
                        returnValue = commandReturnObect.ExecuteNonQuery();
                    }
                    catch (Exception ex) { mySave.InsertRow((int)Definition.Message.D_ALARM, "error - see exception text: " + ex.ToString()); }
                }
            }
            else
            {
                mySave.InsertRow((int)Definition.Message.D_ALARM, "MySQLHelper::executeNonQuery_SQL_Statement: SQL statement too short");
            }
        }

        public int executeInsert_SQL_StatementGetIDAsInt(string SQL_Statement)
        {
            FileWriteLine(SQL_Statement); 

            int id = -1;
            if (SQL_Statement.Length > 0)
            {
                lock (MySQLLock)
                {

                    object returnValue;
                    try
                    {
                        commandReturnObect.CommandText = SQL_Statement;
                        returnValue = commandReturnObect.ExecuteNonQuery();
                        //returnValue = command.ExecuteNonQuery();
                        commandReturnObect.CommandText = "select last_insert_id();";
                        id = Convert.ToInt32(commandReturnObect.ExecuteScalar());
                    }
                    catch (Exception ex) { id = -1; mySave.InsertRow((int)Definition.Message.D_ALARM, "error - see exception text: " + ex.ToString()); }

                }
            }
            else
            {
                mySave.InsertRow((int)Definition.Message.D_ALARM, "MySQLHelper::executeInsert_SQL_StatementGetIDAsInt: SQL statement too short");
            }
            return (id);
        }

        public DataSet GetDataSetFromSQLCommand(string SQL_Statement)
        {
            FileWriteLine(SQL_Statement);

            MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection();
            connection.ConnectionString = _ConnectionString;
            MySql.Data.MySqlClient.MySqlDataAdapter myAdapter;
            DataSet myData = new DataSet();

            if (SQL_Statement != null)
            {
                if (SQL_Statement.Length > 0)
                {
                    myAdapter = new MySql.Data.MySqlClient.MySqlDataAdapter();

                    myData.Clear();

                    MySqlCommand myCommand = new MySqlCommand(SQL_Statement, connection);

                    try
                    {
                        commandReturnObect.CommandText = SQL_Statement;
                        myAdapter.SelectCommand = myCommand;
                        myAdapter.Fill(myData);

                    }
                    catch (InvalidOperationException ioEx) { mySave.InsertRow((int)Definition.Message.D_ALARM, ioEx.ToString()); }
                    catch (Exception sqlEx)
                    {
                        mySave.InsertRow((int)Definition.Message.D_ALARM, sqlEx.ToString());
                    }
                    finally
                    {
                        if (myCommand != null)
                        {
                            myCommand.Dispose();
                        }
                    }
                    if (myAdapter != null)
                    {
                        myAdapter.Dispose();
                    }

                }
                else
                {
                    mySave.InsertRow((int)Definition.Message.D_ALARM, "MySQLHelper::GetDataSetFromSQLCommand: SQL statement too short");
                }
            }
            else
            {
                mySave.InsertRow((int)Definition.Message.D_ALARM, "MySQLHelper::GetDataSetFromSQLCommand: SQL statement not set or null");
             }

           
            if (connection != null)
            {
                connection.Close();
            }
               
            return myData;
        }

        public MySqlDataAdapter GetAdapterFromSQLCommand(string SQL_Statement)
        {
            FileWriteLine(SQL_Statement); 

            MySql.Data.MySqlClient.MySqlDataAdapter myAdapter = null;

            if (SQL_Statement.Length > 0)
            {
                myAdapter = new MySql.Data.MySqlClient.MySqlDataAdapter();
                MySql.Data.MySqlClient.MySqlConnection connection = new global::MySql.Data.MySqlClient.MySqlConnection();
                connection.ConnectionString = _ConnectionString;

                MySqlCommand myCommand = new MySqlCommand(SQL_Statement, connection);

                try
                {
                    commandReturnObect.CommandText = SQL_Statement;
                    myAdapter.SelectCommand = myCommand;

                }
                catch (Exception sqlEx)
                {
                    mySave.InsertRow((int)Definition.Message.D_ALARM, sqlEx.ToString());
                }
                finally
                {
                    if (myCommand != null)
                    {
                        myCommand.Dispose();
                    }

                    if (connection != null)
                    {
                        connection.Close();
                    }
                }
            }
            else
            {
                mySave.InsertRow((int)Definition.Message.D_ALARM, "MySQLHelper::GetAdapterFromSQLCommand: SQL statement too short");
            }
            return myAdapter;
        }

        public MySql.Data.MySqlClient.MySqlCommand execute_SQL_Statement(string SQL_Statement, string ConnectionString = null)
        {
            FileWriteLine(SQL_Statement);

            MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection();
            connection.ConnectionString = _ConnectionString;
            MySql.Data.MySqlClient.MySqlCommand myCom = new MySql.Data.MySqlClient.MySqlCommand();
            if (SQL_Statement.Length > 0)
            {
                try
                {
                    myCom.Connection = connection;
                    myCom.CommandText = SQL_Statement;
                    myCom.CommandType = System.Data.CommandType.Text;
                }
                catch (Exception ex)
                {
                    // MessageBox.Show("Error in SQL clause! \n\n" + SQL_Statement + "\n\n" + ex.ToString());
                    mySave.InsertRow((int)Definition.Message.D_ALARM, "Error in SQL clause! \n\n" + SQL_Statement + "\n\n" + ex.ToString());
                    return myCom;
                }
                finally
                {
                    if (connection != null)
                    {
                        connection.Close();
                    }
                }
            }
            else
            {
                mySave.InsertRow((int)Definition.Message.D_ALARM, "MySQLHelper::execute_SQL_Statement: SQL statement too short");
            }
            return myCom;
        }

        public int GetIDFromName(int TABLE, string Name){
             string SQL_Statement = "";

             switch (TABLE)
             {
                 case (int)Definition.SQLTables.SAMPLE_ACTIVE:
                     SQL_Statement = "SELECT idactive_samples FROM sample_active WHERE SampleID='" + Name +"'";
                     break;
                 case (int)Definition.SQLTables.SAMPLE_PROGRAM:
                     SQL_Statement = "SELECT idsample_programs FROM sample_programs WHERE Name='" + Name + "'";
                     break;
                 case (int)Definition.SQLTables.SAMPLE_STATISTIC_VALUES:
                     SQL_Statement = "SELECT idsample_statistic_values FROM sample_statistic_values WHERE Name='" + Name + "'";
                     break;
                 case (int)Definition.SQLTables.MACHINES:
                     SQL_Statement = "SELECT idmachines FROM machines WHERE Name='" + Name + "'";
                     break;
                 case (int)Definition.SQLTables.GLOBAL_TAGS:
                     SQL_Statement = "SELECT idglobal_tags FROM global_tags WHERE Name='" + Name + "'";
                     break;
                 case (int)Definition.SQLTables.MACHINE_TAGS:
                     SQL_Statement = "SELECT idmachine_tags FROM machine_tags WHERE Name='" + Name + "'";
                     break;
                 default: return -1;
             }
             return return_SQL_Statement( SQL_Statement);
        }

        public string GetNameFromID(int TABLE, int ID)
        {
            string SQL_Statement = "";

            switch (TABLE)
            {
                case (int)Definition.SQLTables.SAMPLE_ACTIVE:
                    SQL_Statement = "SELECT SampleID FROM sample_active WHERE (idactive_samples=" + ID + ")";
                    break;
                case (int)Definition.SQLTables.SAMPLE_PROGRAM:
                    SQL_Statement = "SELECT Name FROM sample_programs WHERE (idsample_programs=" + ID + ")";
                    break;
                case (int)Definition.SQLTables.MACHINE_LIST:
                    SQL_Statement = "SELECT Name FROM machine_list WHERE (idmachine_list=" + ID + ")";
                    break;
                case (int)Definition.SQLTables.MACHINES:
                    SQL_Statement = "SELECT Name FROM machines WHERE (idmachines=" + ID + ")";
                    break;
                case (int)Definition.SQLTables.MACHINE_POSITIONS:
                    SQL_Statement = "SELECT Name FROM machine_positions WHERE (idmachine_positions='" + ID + "')";
                    break;
                case (int)Definition.SQLTables.SAMPLE_TYPE_LIST:
                    SQL_Statement = "SELECT Name FROM sample_type_list WHERE (idsample_type_list=" + ID + ")";
                    break;
                case (int)Definition.SQLTables.MACHINE_COMMANDS:
                    SQL_Statement = "SELECT Name FROM machine_commands WHERE (idmachine_commands=" + ID + ")";
                    break;
                case (int)Definition.SQLTables.MACHINE_PROGRAMS:
                    SQL_Statement = "SELECT Name FROM machine_programs WHERE (idmachine_programs=" + ID + ")";
                    break;
                case (int)Definition.SQLTables.ROUTING_COMMAND_VALUES:
                    SQL_Statement = "SELECT Name FROM routing_command_values WHERE (idrouting_command_values=" + ID + ")";
                    break;
                case (int)Definition.SQLTables.GLOBAL_TAGS:
                    SQL_Statement = "SELECT Name FROM global_tags WHERE (idglobal_tags=" + ID + ")";
                    break;
                case (int)Definition.SQLTables.MACHINE_TAGS:
                    SQL_Statement = "SELECT Name FROM machine_tags WHERE (idmachine_tags=" + ID + ")";
                    break;
                case (int)Definition.SQLTables.ROUTING_POSITION_ENTRIES:
                    SQL_Statement = "SELECT Description FROM routing_position_entries WHERE (idrouting_position_entries=" + ID + ")";
                    break;
                default: return null;
            }
            return return_SQL_StatementAsString(SQL_Statement);
        }

      
        public int GetWinCCTypeFromTagID(int TABLE, int ID)
        {
           string SQL_Statement = "";

            switch (TABLE)
            {
                case (int)Definition.SQLTables.GLOBAL_TAGS:
                    SQL_Statement = "SELECT Type FROM global_tags WHERE (idglobal_tags=" + ID + ")";
                    break;
                case (int)Definition.SQLTables.MACHINE_TAGS:
                    SQL_Statement = "SELECT Type FROM machine_tags WHERE (idmachine_tags=" + ID + ")";
                    break;

                default: return -1;
            }
           
            return return_SQL_Statement(SQL_Statement);
        }

        public int GetWinCCTypeFromTagName(int TABLE, string TagName)
        {
            string SQL_Statement = "";

            switch (TABLE)
            {
                case (int)Definition.SQLTables.GLOBAL_TAGS:
                    SQL_Statement = "SELECT Type FROM global_tags WHERE Name LIKE '" + TagName + "'";
                    break;
                case (int)Definition.SQLTables.MACHINE_TAGS:
                    SQL_Statement = "SELECT Type FROM machine_tags WHERE Name LIKE '" + TagName + "'";
                    break;

                default: return -1;
            }

            return return_SQL_Statement(SQL_Statement);
        }

        public int GetStateValeFromStateWord(string Name)
        {
            string SQL_Statement = "SELECT Value FROM machine_state_list WHERE Name LIKE '" + Name + "'";

            return return_SQL_Statement(SQL_Statement);
        }

        public int GetProgramIDFromSampleID( string Name)
        {
            string SQL_Statement = "SELECT SampleProgramType_ID FROM sample_active WHERE SampleID LIKE '" + Name + "'";
               
            return return_SQL_Statement(SQL_Statement);
        }

        public int GetIDFromSampleID(string Name)
        {
            string SQL_Statement = "SELECT idactive_samples FROM sample_active WHERE SampleID='" + Name + "'";

            return return_SQL_Statement(SQL_Statement);
        }

        public int GetMachineIDFromMachinePositions_ID(int ID)
        {
            string SQL_Statement = "SELECT Machine_ID FROM machine_positions WHERE (idmachine_positions=" + ID + ")";

            return return_SQL_Statement(SQL_Statement);
        }

        public int GetSampleTypeIDFromFromSampleProgramID(int ID)
        {
            string SQL_Statement = "SELECT SampleTypeList_ID FROM sample_programs WHERE (idsample_programs=" + ID + ")";

            return return_SQL_Statement(SQL_Statement);
        }
        
      

        public int GetMachineList_IDFromMachinesByMachine_ID(int ID)
        {
            string SQL_Statement;
            SQL_Statement = "SELECT Machine_List_ID FROM machines WHERE (idmachines=" + ID + ")";
            return return_SQL_Statement(SQL_Statement);
        }
       
        public bool InsertNewEntryIntoRouting(int nMachine_ID)
        {
            bool ret = false;
            DateTime now = DateTime.Now;
            String utcstr = now.ToString(definitions.ThorCustomFormat);

            string SQL_Statement = "Insert into routing_positions (Modified,Machine_ID,Machine_Position_ID) VALUES ('" + utcstr + "','" + nMachine_ID.ToString() + "','-1')";
          
            if (return_SQL_Statement(SQL_Statement) != 0) { ret = true; }
            return ret;
        }
        public bool DeleteUnitFromRouting(int nMachine_ID)
        {
            bool ret = false;
         
            string SQL_Statement = "DELETE FROM routing_positions WHERE Machine_ID=" + nMachine_ID.ToString() + "";
          
            if (return_SQL_Statement(SQL_Statement) != 0) { ret = true; }
            return ret;
        }
        public bool DeleteMachinePositionFromRouting(int nRoutingPosition_ID)
        {
            bool ret = false;

            string SQL_Statement = "DELETE FROM routing_positions WHERE idrouting_positions=" + nRoutingPosition_ID.ToString() + "";
          
            if (return_SQL_Statement(SQL_Statement) != 0) { ret = true; }
            return ret;
        }
        public bool  DeleteSampleTypeFromRouting(int nSampleType_ID, int nRoutingPosition_ID)
        {
            bool ret = false;

            string SQL_Statement = "DELETE FROM routing_position_entries WHERE SampleType_ID=" + nSampleType_ID.ToString() + " AND Position_ID=" + nRoutingPosition_ID.ToString();
          
            if (return_SQL_Statement(SQL_Statement) != 0) { ret = true; }
            return ret;
        }
        public bool  DeleteConditionEntryFromRouting(int nRoutingPositionEntries_ID)
        {
            bool ret = false;

            string SQL_Statement = "DELETE FROM routing_position_entries WHERE idrouting_position_entries=" + nRoutingPositionEntries_ID.ToString();
          
            if (return_SQL_Statement(SQL_Statement) != 0) { ret = true; }
            return ret;
        }
        
        public bool InsertPositionIntoRouting(int nMachine_ID, int nMachine_Position_ID)
        {
            bool ret = false;
            string SQL_Statement = "DELETE FROM routing_positions WHERE  Machine_ID=" + nMachine_ID + " AND Machine_Position_ID='-1'";
            return_SQL_Statement(SQL_Statement);
            
            SQL_Statement = "INSERT INTO routing_positions (Machine_Position_ID,Machine_ID) VALUES ('" + nMachine_Position_ID  + "','" + nMachine_ID.ToString() + "')";
           
            if (return_SQL_Statement(SQL_Statement) != 0) { ret = true; }
            return ret;
        }

        public bool InsertNewEntryIntoRoutingEntries(int nRouting_Position_ID, int SampleType_ID)
        {
            bool ret = false;
            DateTime now = DateTime.Now;
            String utcstr = now.ToString(definitions.ThorCustomFormat);

            string SQL_Statement = "Insert into routing_position_entries (Modified,Position_ID,SampleType_ID,TimeForWarning,TimeForAlarm) VALUES ('" + utcstr + "','" + nRouting_Position_ID.ToString() + "','"+ SampleType_ID.ToString()+"',0,0)";

            if (return_SQL_Statement(SQL_Statement) != 0) { ret = true; }
            return ret;
        }

        public bool DeleteEntryFromRoutingCondition(int nRoutingCondition_ID)
        {
            bool ret = false;

            string SQL_Statement = "DELETE FROM routing_conditions WHERE idrouting_conditions=" + nRoutingCondition_ID.ToString();

            if (return_SQL_Statement(SQL_Statement) != 0) { ret = true; }
            return ret;
        }

        public bool DeleteEntryFromRoutingCommands(int nRoutingCommand_ID)
        {
            bool ret = false;

            string SQL_Statement = "DELETE FROM routing_commands WHERE idrouting_commands=" + nRoutingCommand_ID.ToString();

            if (return_SQL_Statement(SQL_Statement) != 0) { ret = true; }
            return ret;
        }

        public bool DeleteEntriesFromRoutingConditionsByRoutingPositionEntry_ID(int nRoutingPositionEntry_ID)
        {
            bool ret = false;

            string SQL_Statement = "DELETE FROM routing_conditions WHERE RoutingPositionEntry_ID=" + nRoutingPositionEntry_ID.ToString();

            if (return_SQL_Statement(SQL_Statement) != 0) { ret = true; }
            return ret;
        }

        public bool DeleteEntriesFromRoutingCommandsByRoutingPositionEintry_ID(int nRoutingPositionEntry_ID)
        {
            bool ret = false;

            string SQL_Statement = "DELETE FROM routing_commands WHERE RoutingPositionEntry_ID=" + nRoutingPositionEntry_ID.ToString();

            if (return_SQL_Statement(SQL_Statement) != 0) { ret = true; }
            return ret;
        }
        
      
        public void SetConditionComply(bool bConditionComply, int nRoutingCondition_ID, string actualValue="")
        {
             int nConditionComply = 0;
             if (bConditionComply) { nConditionComply = 1; }
             string SQL_Statement = "Update routing_conditions SET condition_comply = " + nConditionComply + " WHERE idrouting_conditions=" + nRoutingCondition_ID;

            return_SQL_Statement(SQL_Statement);
        }


        public void SetConditionActualValue(int nRoutingCondition_ID, string actualValue = "")
        {
            string SQL_Statement = "Update routing_conditions SET actualValue='" + actualValue + "' WHERE idrouting_conditions=" + nRoutingCondition_ID;

            return_SQL_Statement(SQL_Statement);
        }

       

        public bool InsertSQLCommand(string SQL_Statement)
        {
            bool ret = false;
           
            if (return_SQL_Statement(SQL_Statement) != 0) { ret = true; }
            return ret;
        }

        public int GetMachinePosition_IDFromRoutingCondition(int routingPositionEntry_ID)
        {
            int nMachinePosition_ID = -1;

            string SQL_Statement = "SELECT Machine_Position_ID FROM routing_positions WHERE idrouting_positions=" +  GetRoutingPosition_IDFromRoutingPositionEntries(routingPositionEntry_ID);
            nMachinePosition_ID = return_SQL_Statement(SQL_Statement);
            return nMachinePosition_ID;
        }

        public int GetRoutingPosition_IDFromRoutingPositionEntries(int routingPositionEntry_ID)
        {
            int nRoutingPosition_ID = -1;

            string SQL_Statement = "SELECT Position_ID FROM routing_position_entries WHERE idrouting_position_entries=" + routingPositionEntry_ID;
            nRoutingPosition_ID = return_SQL_Statement(SQL_Statement);

            return nRoutingPosition_ID;
        }

        public int GetSampleActive_IDIfOnMachinePosition(int nMachinePosition_ID)
        {
            int nSampleActive_ID = -1;
            string SQL_Statement = "SELECT idactive_samples FROM sample_active WHERE  ActualSamplePosition_ID=" + nMachinePosition_ID;
            nSampleActive_ID = return_SQL_Statement(SQL_Statement);
          
            return nSampleActive_ID;
        }

        public int GetSampleActive_IDFromSampleActiveBySampleName(string  strSampleName)
        {
            int nSampleActive_ID = -1;
            string SQL_Statement = "SELECT idactive_samples FROM sample_active WHERE SampleID LIKE '" + strSampleName +"'";
            nSampleActive_ID = return_SQL_Statement(SQL_Statement);

            return nSampleActive_ID;
        }

        public int GetActualSamplePosition_IDFromSampleActiveBySampleActive_ID(int nSampleActive_ID)
        {
            int nActualSamplePosition_ID = -1;
            string SQL_Statement = "SELECT ActualSamplePosition_ID FROM sample_active WHERE idactive_samples=" + nSampleActive_ID.ToString();
            nActualSamplePosition_ID = return_SQL_Statement(SQL_Statement);

            return nActualSamplePosition_ID;
        }

        public int GetRouting_Position_IDFromRouting_positionsByMachinePosition_ID(int nMachinePosition_ID)
        {
            int nSampleActive_ID = -1;
            string SQL_Statement = "SELECT idrouting_positions FROM routing_positions WHERE Machine_Position_ID=" + nMachinePosition_ID;
            nSampleActive_ID = return_SQL_Statement(SQL_Statement);

            return nSampleActive_ID;
        }

        public int GetRouting_Position_Entry_IDFromRouting_position_entriesByRouting_Position_ID(int nRouting_Position_ID)
        {
            int nSampleActive_ID = -1;
            string SQL_Statement = "SELECT idrouting_position_entries FROM routing_position_entries WHERE Position_ID=" + nRouting_Position_ID;
            nSampleActive_ID = return_SQL_Statement(SQL_Statement);

            return nSampleActive_ID;
        }

        public bool DeleteMachineListEntryFromMachinelistByMachineList_ID(int nMachinelist_ID)
        {
            bool ret = false;
            string SQL_Statement = "DELETE FROM machine_list WHERE idmachine_list=" + nMachinelist_ID;   
            if (return_SQL_Statement(SQL_Statement) != 0) { ret = true; }
            return ret;
        }

        public bool DeleteMachinesEntryFromMachinelistByMachines_ID(int nMachines_ID)
        {
            bool ret = false;
          //  string SQL_Statement = "DELETE FROM machines WHERE idmachines=" + nMachines_ID;
            string SQL_Statement = "CALL DeleteMachine(" + nMachines_ID.ToString() + ")";
            if (return_SQL_Statement(SQL_Statement) != 0) { ret = true; }
            return ret;
        }

        public bool DeleteSampleFromSampleActiveBySample_ID(int nSample_ID)
        {
            bool ret = false;
            try
            {
                string SQL_Statement = "CALL DeleteSample(" + nSample_ID.ToString() + ")";
                if (return_SQL_Statement(SQL_Statement) != 0) { ret = true; }
            }
            catch (Exception ex) { mySave.InsertRow((int)Definition.Message.D_ALARM, @"Routing::DeleteSampleFromSampleActiveBySample_ID ->\r\n" + ex.ToString()); }
            return ret;
        }

        public int GetConnectionTypeFromConnectionListByMachineList_ID(int nMachineList_ID)
        {
            int nConnectionType_ID = -1;
            string SQL_Statement = "SELECT Connection_type_list_ID FROM machine_list WHERE idmachine_list=" + nMachineList_ID;
            nConnectionType_ID = return_SQL_Statement(SQL_Statement);

            return nConnectionType_ID;
        }

        public int GetConnectionTypeFromConnectionListByMachine_ID(int nMachine_ID)
        {
            int nConnectionType_ID = -1;
            string SQL_Statement = @"SELECT        machine_list.Connection_type_list_ID
                                       FROM          machines INNER JOIN
                                       machine_list ON machines.Machine_list_ID = machine_list.idmachine_list
                                       WHERE        (machines.idmachines =" + nMachine_ID + ")";
            nConnectionType_ID = return_SQL_Statement(SQL_Statement);

            return nConnectionType_ID;
        }

        public int GetColorFromMachineListByMachineList_ID(int nMachineList_ID)
        {
            int nColor = -1;
            string SQL_Statement = "SELECT Color FROM machine_list WHERE (idmachine_list=" + nMachineList_ID + ")";
            nColor = return_SQL_Statement(SQL_Statement);

            return nColor;
        }

        public int GetCommandActiveFROMSampleActiveBynSampleID_ID(int nActiveSample_ID)
        {
            int nCommandActive = -1;
            string SQL_Statement = "SELECT Command_Active FROM sample_active WHERE (idactive_samples=" + nActiveSample_ID.ToString() + ")";
            nCommandActive = return_SQL_Statement(SQL_Statement);

            return nCommandActive;
        }

        public bool UpdateCommandActiveOnsampleActiveBySampleId_ID(int nActiveSample_ID, int nActive)
        {
            bool ret = false;
            string SQL_Statement = "UPDATE sample_active SET Command_Active=" + nActive  + " WHERE idactive_samples=" + nActiveSample_ID.ToString() + "";

            if (return_SQL_Statement(SQL_Statement) != 0) { ret = true; }
            return ret;
        }

        public int GetColorFromsampleprogramsBySampleProgramType_ID(int nSampleProgramType_ID)
        {
            int nColor = -1;
            string SQL_Statement = "SELECT Color FROM sample_programs WHERE (idsample_programs=" + nSampleProgramType_ID + ")";
            nColor = return_SQL_Statement(SQL_Statement);

            return nColor;
        }
        public int SetSorttypeOnmagazineConfiguration(int nSortType_ID, int magazine_configuration_ID)
        {
            int ret = -1;
            string SQL_Statement = "UPDATE magazine_configuration SET SortType_ID =" + nSortType_ID + " WHERE (idmagazine_configuration=" + magazine_configuration_ID + ")";
            ret = return_SQL_Statement(SQL_Statement);

            return ret;
        }

        public int GetMagazine_IDByMachine_ID(int nMachine_ID)
        {
            int ret = -1;
            string SQL_Statement = "SELECT idmagazine_configuration FROM magazine_configuration Where Machine_ID=" + nMachine_ID + "";
            ret = return_SQL_Statement(SQL_Statement);

            return ret;
        }
        public int CheckForCommunicationWithWinCC()
        {
            int  ret = -1;
            string strLabManagerOnline = null;
            string SQL_Statement = "SELECT Value FROM global_tags WHERE Name='LabManagerOnline'";
            strLabManagerOnline = return_SQL_StatementAsString(SQL_Statement);
            Int32.TryParse(strLabManagerOnline, out ret);
            return ret;
        }

        

        public int CheckForRunModeOnWithWinCC()
        {
            int ret = -1;

            string SQL_Statement = "SELECT Value FROM global_tags WHERE Name='RunMode'";
            string strRunMode = return_SQL_StatementAsString(SQL_Statement);
            if (strRunMode == "False" || strRunMode == "FALSE" || strRunMode=="false")
            {
                ret = 0;
            }
            else if (strRunMode == "True" || strRunMode == "TRUE" || strRunMode == "true")
            {
                ret = 1;
            }
            return ret;
        }

        public int CheckForLabManagerConnectReady(string strName)
        {
            int ret = -1;
            string strRet = null;
            string SQL_Statement = "SELECT "+  strName +" FROM communication";
          //  string SQL_Statement = "SELECT Value FROM global_tags WHERE Name='" + strName + "'";
            strRet  = return_SQL_StatementAsString(SQL_Statement);
            
            Int32.TryParse(strRet, out ret);
            return ret;
        }
        public int CheckForWinCCReady()
        {
            int ret = -1;
           
             string SQL_Statement = "SELECT WinCCReady FROM communication";
             ret = return_SQL_Statement(SQL_Statement);
           
            return ret;
        }

        public int SetLabManagerConnectReady(string strName, int nValue)
        {
            int ret = -1;

            string SQL_Statement = "UPDATE communication SET " + strName + "=" + nValue;
            ret = return_SQL_Statement(SQL_Statement);

            return ret;
        }
        public string GetMachineTagValueByName(string strName)
        {
            string retString = null;
            try
            {
                retString = return_SQL_StatementAsString("SELECT Value FROM Machine_tags WHERE Name LIKE '" + strName + "'");
            }
            catch { }
            return retString;
        }

        public bool WriteWinCCTagFromSampleValuesByValueName(string strName, string strValue, int nType)
        {
            bool ret = false;
            string SQL_Statement = null;

            if (CheckForLabManagerConnectRunning())
            {
                if (strName.Length > 0 && strValue.Length > 0 && nType > 0)
                {
                    SQL_Statement = "INSERT INTO write_wincc_data (Name,Value,Type) VALUES ('" + strName + "','" + strValue + "'," + nType.ToString() + ")";
                }

                if (return_SQL_Statement(SQL_Statement) == 0) { ret = true; }
            }
            return ret;
        }

        public int GetPostionIDFROMMachinepositionsByMachine_IDAndPosNumber(int nMachine_ID, int nPosNmber)
        {
            int nCommandActive = -1;
            string SQL_Statement = "SELECT idmachine_positions FROM machine_positions WHERE (Machine_ID=" + nMachine_ID.ToString() + " AND PosNumber=" + nPosNmber.ToString() + ")";
            nCommandActive = return_SQL_Statement(SQL_Statement);

            return nCommandActive;
        }

        public int GetPostionIDFROMMachinepositionsByMachine_IDAndName(int nMachine_ID, string strName)
        {
            int nCommandActive = -1;
            string SQL_Statement = "SELECT idmachine_positions FROM machine_positions WHERE (Machine_ID=" + nMachine_ID.ToString() + " AND Name='" + strName + "')";
            nCommandActive = return_SQL_Statement(SQL_Statement);

            return nCommandActive;
        }

        public string GetWinCCUserName()
        {
            string ret = null;

            string SQL_Statement = "SELECT UserName FROM communication";
            ret = return_SQL_StatementAsString(SQL_Statement);
            return ret;
        }

        public bool GetRightsFromUserAdministration(int nRight)
        {
            bool ret = false;
            string SQL_Statement = null;

            SQL_Statement = "SELECT access FROM user_administration WHERE number=" + nRight + "";

            ret = return_SQL_StatementAsBool(SQL_Statement);

            return ret;
        }

        public bool GetConditionComplyFromRoutingConditionsByRoutingConditonsID(int nRoutingConditions_ID)
        {
            bool ret = false;
            string SQL_Statement = null;

            SQL_Statement = "SELECT condition_comply FROM routing_conditions WHERE idrouting_conditions=" + nRoutingConditions_ID + "";

            ret = return_SQL_StatementAsBool(SQL_Statement);

            return ret;
        }

        public bool GetMachineFreeOfSamples(int nMachine_ID, int nSample_ID=-1)
        {
            bool ret = false;
            string SQL_Statement = null;

            SQL_Statement = "Select CheckIfMachineIsSampleFree(" + nMachine_ID + "," + nSample_ID + ")";

            ret = return_SQL_StatementAsBool(SQL_Statement);

            return ret;
        }
        

        public string GetActualValueFromRoutingConditionsByRoutingConditonsID(int nRoutingConditions_ID)
        {
            string ret = null;

            string SQL_Statement = "SELECT actualValue FROM routing_conditions WHERE idrouting_conditions=" + nRoutingConditions_ID + "";
            ret = return_SQL_StatementAsString(SQL_Statement);
            return ret;
        }

        public string GetDescriptionFromRoutingPositionEntriesByRoutingPositionEntriesID(int nRoutingPositionEntries_ID)
        {
            string ret = null;

            string SQL_Statement = "SELECT Description FROM routing_position_entries WHERE idrouting_position_entries=" + nRoutingPositionEntries_ID + "";
            ret = return_SQL_StatementAsString(SQL_Statement);
            return ret;
        }
        public string UpdateDescriptionFromRoutingPositionEntriesByRoutingPositionEntriesID(int nRoutingPositionEntries_ID, string strNewName)
        {
            string ret = null;

            string SQL_Statement = "Update routing_position_entries Set Description='" + strNewName  + "' WHERE idrouting_position_entries=" + nRoutingPositionEntries_ID + "";
            ret = return_SQL_StatementAsString(SQL_Statement);
            return ret;
        }

        public bool UpdateActualSamplePosOnSampleActiveBySampleId_ID(int nActiveSample_ID, int nMachinePosition)
        {
            bool ret = false;
            string SQL_Statement = "UPDATE sample_active SET ActualSamplePosition_ID=" + nMachinePosition + " WHERE idactive_samples=" + nActiveSample_ID.ToString() + "";

            if (return_SQL_Statement(SQL_Statement) != 0) { ret = true; }
            return ret;
        }

        public bool UpdateActualSamplePosOnSampleActiveBySampleID(string strSampleID, int nMachinePosition)
        {
            bool ret = false;
            string SQL_Statement = "UPDATE sample_active SET ActualSamplePosition_ID=" + nMachinePosition + " WHERE SampleID='" + strSampleID + "'";

            if (return_SQL_Statement(SQL_Statement) != 0) { ret = true; }
            return ret;
        }

        public bool UpdateActualPalletPosOnSampleActiveBySampleID(string strSampleID, int nMagazine, int nMagazinePosition, bool bOnInput = true)
        {
            bool ret = false;
            string SQL_Statement = null;
            int nMachine_ID = return_SQL_Statement("Select Machine_ID FROM magazine_configuration Where RobotMagazinePosition=" + nMagazine);
            int nMagazine_Configuration_ID = return_SQL_Statement("Select idmagazine_configuration FROM magazine_configuration Where RobotMagazinePosition=" + nMagazine);
            if (!bOnInput)
            {
                int nActualPosition = return_SQL_Statement("Select idmachine_positions FROM machine_positions Where Machine_ID=" + nMachine_ID + " AND PosNumber=" + nMagazinePosition);
                SQL_Statement = "UPDATE sample_active SET Magazine=" + nMagazine_Configuration_ID + ",MagazinePos=" + nMagazinePosition + ",ActualSamplePosition_ID=" + nActualPosition + " WHERE SampleID='" + strSampleID + "'";
            }
            else
            {
                int nActualPosition = -1;
                 nActualPosition = return_SQL_Statement("Select InputPosition FROM magazine_configuration Where RobotMagazinePosition=" + nMagazine);
                if (nActualPosition <= 0)
                {
                    mySave.InsertRow((int)Definition.Message.D_ALARM, "no Inputposition found for magazine with idmagazine_configuaration: " + nMagazine_Configuration_ID + " - Please check InputPosition in magazine configuration");   
                    return false;
                }
                SQL_Statement = "UPDATE sample_active SET Magazine=0,MagazinePos=0,ActualSamplePosition_ID=" + nActualPosition + " WHERE SampleID='" + strSampleID + "'";     
            }
            if (return_SQL_Statement(SQL_Statement) != 0) { ret = true; }
            return ret;
        }

        public bool UpdateActualTimeOnRoutingPositionEntriesByRouting_Position_Entry_ID(int nRouting_Position_Entry_ID)
        {
            bool ret = false;
            string SQL_Statement = null;
   
                SQL_Statement = "UPDATE routing_position_entries SET ActualTime=ActualTime+1 WHERE idrouting_position_entries=" + nRouting_Position_Entry_ID.ToString() + "";
          
            if (return_SQL_Statement(SQL_Statement) != 0) { ret = true; }
            return ret;
        }

        public bool ResetActualTimeAndAlarmsOnRoutingPositionEntries( int nRouting_Position_ID=-1 )
        {
            bool ret = false;
            string SQL_Statement = null;

            SQL_Statement = "UPDATE routing_position_entries SET ActualTime=0,TimeWarningOn=0,TimeAlarmOn=0";
            if (nRouting_Position_ID > 0)
            {
                SQL_Statement = SQL_Statement + " WHERE Position_ID=" + nRouting_Position_ID.ToString() + "";
                WriteWinCCTagFromSampleValuesByValueName("WarningTimeOn", "false", 1);
                WriteWinCCTagFromSampleValuesByValueName("AlarmTimeOn", "false", 1);
            }
            if (return_SQL_Statement(SQL_Statement) != 0) { ret = true; }
            return ret;
        }


        public bool SetAlarmTimeForWarningOnRoutingPositionEntriesByRouting_Position_Entry_ID(int nRouting_Position_Entry_ID)
        {
            bool ret = false;
            string SQL_Statement = null;

            SQL_Statement = "UPDATE routing_position_entries SET TimeWarningOn=1 WHERE idrouting_position_entries=" + nRouting_Position_Entry_ID.ToString() + "";

            if (return_SQL_Statement(SQL_Statement) != 0) { ret = true; }
            
            WriteWinCCTagFromSampleValuesByValueName("WarningTimeOn", "true", 1);
            return ret;
        }

        public bool SetAlarmTimeForAlarmOnRoutingPositionEntriesByRouting_Position_Entry_ID(int nRouting_Position_Entry_ID)
        {
            bool ret = false;
            string SQL_Statement = null;

            SQL_Statement = "UPDATE routing_position_entries SET TimeAlarmOn=1 WHERE idrouting_position_entries=" + nRouting_Position_Entry_ID.ToString() + "";

            if (return_SQL_Statement(SQL_Statement) != 0) { ret = true; }
            
            WriteWinCCTagFromSampleValuesByValueName("AlarmTimeOn", "true", 1);
            return ret;
        }
      
        public int CheckIfSamplePosIsFreeOnSampleActive(int nPos, int nMagazine_ID, int nMagPos)
        {
            int ret = -1;
            string SQL_Statement = null;

            if (nPos > 0)
            {
                SQL_Statement = "SELECT idactive_samples from sample_active WHERE ActualSamplePosition_ID=" + nPos.ToString();
                ret = return_SQL_Statement(SQL_Statement);
                if (ret == -1)
                {
                    SQL_Statement = "SELECT idactive_samples from sample_active WHERE Magazine=" + nMagazine_ID.ToString() + " AND MagazinePos=" + nMagPos.ToString();
                    ret = return_SQL_Statement(SQL_Statement);
                }
                
            }
            else { ret = -1; }

            return ret;
        }

        public int CheckIfSampleIDAllreadyExistOnSampleActive(string strSampleID)
        {
            int ret = -1;
            string SQL_Statement = null;

            if (strSampleID.Length > 0)
            {
                SQL_Statement = "SELECT idactive_samples from sample_active WHERE SampleID='" + strSampleID +"'";
                ret = return_SQL_Statement(SQL_Statement);
            }
            else { ret = -1; }

            return ret;
        }

        public int GetPositionCountOfMagazineByMachineID(int nMachineID)
        {
            int ret = -1;
            string SQL_Statement = null;

            if (nMachineID > 0)
            {
                SQL_Statement = "SELECT (Dimension_x * Dimension_Y) AS PosCount FROM magazine_configuration WHERE idmagazine_configuration=" + nMachineID.ToString() + "";
                ret = Int32.Parse(return_SQL_StatementAsString(SQL_Statement));
            }
            else { ret = -1; }

            return ret;
        }

        public string GetUserInputDataByName(string strName, string strDefaultValue = null)
        {
            string ret = null;

            string SQL_Statement = "SELECT Value  FROM user_input_data WHERE Name='" + strName + "'";
            ret = return_SQL_StatementAsString(SQL_Statement);
            if (ret == null) { ret = strDefaultValue; }
            return ret;
        }

        public int UpdateUserInputDataByName(string strName, string strValue)
        {
            int ret = -1;
            string SQL_Statement = null;

            if (strName.Length > 0)
            {
                SQL_Statement = "SELECT iduser_input_data FROM user_input_data WHERE Name='" + strName + "'";
                ret = return_SQL_Statement(SQL_Statement);
            }
            else { ret = -1; }
            if (ret > 0)
            {
                SQL_Statement = "UPDATE user_input_data SET Value='" + strValue+ "' WHERE Name='" + strName + "'";
                ret = return_SQL_Statement(SQL_Statement);
            }
            else
            {
                SQL_Statement = "INSERT INTO user_input_data (Value,Name) VALUES ('" + strValue + "','" + strName + "')";
                ret = return_SQL_Statement(SQL_Statement);
            }

            return ret;
        }

        public bool CallMySQLProcedureOnTime()
        {
            bool ret = false;
            string SQL_Statement = "CALL CalledEverySecond()";
            if (return_SQL_Statement(SQL_Statement) != 0) { ret = true; }
            return ret;
        }

        public int GetOrderForceFromSampleActiveBySample_ID(int nSampleID)
        {
            int ret = -1;
            string SQL_Statement = null;

            if (nSampleID > 0)
            {
                SQL_Statement = "SELECT MagazineOrderForce FROM sample_active WHERE idactive_samples=" + nSampleID.ToString() + "";
                ret = return_SQL_Statement(SQL_Statement);
            }
            else { ret = -1; }

            return ret;
        }

        public int UpdateOrderForceFromSampleActiveBySample_ID(int nSampleID, int nValue)
        {
            int ret = -1;
            string SQL_Statement = null;

            if (nSampleID > 0)
            {
                SQL_Statement = "UPDATE  sample_active SET MagazineOrderForce=" + nValue + " WHERE idactive_samples=" + nSampleID.ToString() + "";
                ret = return_SQL_Statement(SQL_Statement);
            }
            else { ret = -1; }

            return ret;
        }

        public string[][] GetInputCheckArrayFromRoutingInputCheckByType(string Type)
        {
            string[][] ret = null;
            string SQL_Statement = null;

            if (Type.Length > 0)
            {        
                SQL_Statement = "SELECT Type_ID,Field1,Field2,Field3,Field4 FROM routing_error_text WHERE Type='" + Type + "'";
                DataSet ds_InputCheck = GetDataSetFromSQLCommand(SQL_Statement);

                if (ds_InputCheck.Tables[0] != null)
                {
                    ret = new string[ds_InputCheck.Tables[0].Rows.Count][];
                    int nIndex = 0;
                    DataTable dt_InputCheck = ds_InputCheck.Tables[0];
                    foreach(DataRow dr_InputCheck in dt_InputCheck.Rows)
                    {
                        ret[nIndex] = new string[ds_InputCheck.Tables[0].Columns.Count];
                         
                        for(int k=0;k<ds_InputCheck.Tables[0].Columns.Count;k++)
                        {
                            Type t = dr_InputCheck[k].GetType();
                         //   if (dr_InputCheck.ItemArray[k].GetType().Name == "Int32")
                            {
                                 ret[nIndex][k] = dr_InputCheck[k].ToString();
                            }
                        }
                        nIndex++;
                    }
                }
            }
           
            return ret;
        }

        public int GetLineStepReachedFromSampleActiveBySample_ID(int nSampleID)
        {
            int ret = -1;
            string SQL_Statement = null;

            if (nSampleID > 0)
            {
                SQL_Statement = "SELECT LineStepReached FROM sample_active WHERE idactive_samples=" + nSampleID.ToString() + "";
                ret = return_SQL_Statement(SQL_Statement);
            }
            else { ret = -1; }

            return ret;
        }

        public int UpdateLineStepReachedFromSampleActiveBySample_ID(int nSampleID, int nValue)
        {
            int ret = -1;
            string SQL_Statement = null;

            if (nSampleID > 0)
            {
                SQL_Statement = "UPDATE  sample_active SET LineStepReached=" + nValue.ToString() + " WHERE idactive_samples=" + nSampleID.ToString() + "";
                ret = return_SQL_Statement(SQL_Statement);
            }
            else { ret = -1; }

            return ret;
        }

        public bool GetViewInSampleTrackingFromMachinesByMachine_ID(int nMachine_ID)
        {
            bool ret = false;
            string SQL_Statement = null;

            SQL_Statement = "SELECT ViewInSampleTracking FROM machines WHERE idmachines=" + nMachine_ID.ToString() + "";

            int nValue = return_SQL_Statement(SQL_Statement);
            if (nValue == 1) { ret = true; } else { ret = false; }
            return ret;
        }

        public bool IsMachineOffline(int nMachine_ID)
        {
            bool ret = false;
            string SQL_Statement = null;
            if (nMachine_ID > 0)
            {
                SQL_Statement = "SELECT value FROM machine_state_signals";

                SQL_Statement = SQL_Statement + " WHERE signal_type='Status' AND bit_number=0 AND Machine_ID=" + nMachine_ID.ToString() + " AND signal_number=0";
           
                if (return_SQL_StatementAsBool(SQL_Statement) ) { ret = true; }
            }
            return ret;
        }

        public bool SetStateValueOnMachineStateSignals(int nMachine_ID, int nBitNumber)
        {
            bool ret = false;
            string SQL_Statement = null;
            if (nMachine_ID > 0)
            {
                SQL_Statement = "UPDATE machine_state_signals SET value=0 WHERE signal_type='Status' AND signal_number=0 AND Machine_ID=" + nMachine_ID.ToString();
                return_SQL_Statement(SQL_Statement);
                SQL_Statement = "UPDATE machine_state_signals SET value=1 WHERE signal_type='Status' AND signal_number=0 AND bit_number=" + nBitNumber.ToString() + " AND Machine_ID=" + nMachine_ID.ToString();
                if (return_SQL_Statement(SQL_Statement) >= 0) { ret = true; }
                SQL_Statement = "CALL SetMachineStateStatistic(" + nMachine_ID + ")";
                return_SQL_Statement(SQL_Statement);
            }
            return ret;
        }

        public bool SetStateBitOnMachineStateSignals(int nMachine_ID, int nBitNumber, int nSignalNumber, bool bValue)
        {
            bool ret = false;
            string SQL_Statement = null;
            if (nMachine_ID > 0)
            {
                string strValue = bValue ? "1" : "0";
                SQL_Statement = "UPDATE machine_state_signals SET value=" + strValue + " WHERE signal_type='Status' AND signal_number=" + nSignalNumber + " AND bit_number=" + nBitNumber.ToString() + " AND Machine_ID=" + nMachine_ID.ToString();
                if (return_SQL_Statement(SQL_Statement) >= 0) { ret = true; }
            }
            return ret;
        }

        public bool InsertWSEntryIntoSampleValuesBySampleIDAndValueName(string strSampleID, string strValueName, string strValue)
        {
            bool ret = false;
            int nSampleValue_ID = -1;
            int nSample_ID = -1;

            nSample_ID = GetIDFromSampleID(strSampleID);
            if (nSample_ID==-1)
            {
                mySave.InsertRow((int)Definition.Message.D_ALARM, "no id found for sample with sampleID:" + strSampleID + " in function <InsertWSEntryIntoSampleValuesBySampleIDAndValueName>");   
            }

            string SQL_Statement = "SELECT idsample_values FROM sample_values WHERE SampleID='" + strSampleID + "' AND Name='" + strValueName + "'";

            nSampleValue_ID = return_SQL_Statement(SQL_Statement);

            if (nSampleValue_ID > 0)   // if entry exist update value else insert new entry
            {
                SQL_Statement = "UPDATE sample_values SET Value='" + strValue + "' WHERE idsample_values=" + nSampleValue_ID;
            }
            else    // else: insert new entry with the values
            {
                SQL_Statement = "INSERT INTO sample_values (ActiveSample_ID,Name,Value,SampleID) VALUES ('" + nSample_ID + "','" + strValueName + "','" + strValue + "','" + strSampleID + "')";
            }
            if (return_SQL_Statement(SQL_Statement) == 0) { ret = true; }
            return ret;
        }

        public int InsertSampleToSample_TGABySample_ID(int nSample_ID, int nMachine_ID)
        {
            string strSampleID = GetNameFromID((int)Definition.SQLTables.SAMPLE_ACTIVE,nSample_ID);
            string SQL_Statement  = null;
            SQL_Statement = "INSERT INTO sample_TGA (SampleID,TimeStampInserted,Machine_ID) VALUES('" + strSampleID + "', now()," + nMachine_ID  + ")";
            return executeInsert_SQL_StatementGetIDAsInt(SQL_Statement);
        }

        public void DeleteSampleFromSample_TGABySample_ID(int nSample_ID)
        {
            string strSampleID = GetNameFromID((int)Definition.SQLTables.SAMPLE_ACTIVE, nSample_ID);
            string SQL_Statement = null;
            SQL_Statement = "DELETE FROM sample_TGA WHERE SampleID='" + strSampleID +"'" ;
            return_SQL_Statement(SQL_Statement);
        }

        public int GetMagazinePosBySampleIDFrimSampleValues(int nSample_ID)
        {
            string SQL_Statement = null;
            int nPos = -1;
            string strPos = null;

            SQL_Statement = "SELECT Value FROM sample_values WHERE ActiveSample_ID=" + nSample_ID.ToString() + " AND Name LIKE 'MAGAZINE_POS'";
            try
            {
                strPos = return_SQL_StatementAsString(SQL_Statement);
                Int32.TryParse(strPos, out nPos);
            }
            catch (Exception ex) { mySave.InsertRow((int)Definition.Message.D_ALARM, ex.ToString()); }
            return nPos;
        }

        public int ReactivateSampleInMagazine(int nSample_ID)
        {
            int ret = -1;
            string SQL_Statement = null;

            if (nSample_ID > 0)
            {
                SQL_Statement = "UPDATE sample_active SET SampleID=CONCAT('D#' ,SampleID),MagazineDoneFlag=0 WHERE idactive_samples=" + nSample_ID.ToString() + "";
                ret = return_SQL_Statement(SQL_Statement);
            }
            else { ret = -1; }

            return ret;
        }

        public string GetSampleValueFromSampleValuesByValueName(string strName, string strSampleID)
        {
            string ret = null;

            string SQL_Statement = "SELECT Value FROM sample_values WHERE Name='" + strName + "' AND SampleID='" + strSampleID+ "'";
            ret = return_SQL_StatementAsString(SQL_Statement);
            return ret;
        }


       
        public int GetSampletypeFromSampleProgramBySampleProgram_ID(int nSampleProgram_ID)
        {
            int ret = -1;
            string SQL_Statement = null;

            if (nSampleProgram_ID > 0)
            {
                SQL_Statement = "SELECT SampleObjectList_ID FROM sample_programs WHERE idsample_programs=" + nSampleProgram_ID.ToString() + "";
                ret = return_SQL_Statement(SQL_Statement);
            }
            else { ret = -1; }

            return ret;
        }

        public int GetMachine_IDFromMagazineConfigurationByMagazine_ID(int nMagazine_ID)
        {
            int ret = -1;
            string SQL_Statement = "SELECT Machine_ID FROM magazine_configuration Where idmagazine_configuration=" + nMagazine_ID + "";
            ret = return_SQL_Statement(SQL_Statement);

            return ret;
        }

        public int GetMachine_IDFromTCPIPConfigurationForRobot(int nAnalyseType_ID)
        {
            int ret = -1;
            string SQL_Statement = "SELECT Machine_ID FROM tcpip_configuration Where AnalyseType_ID=" + nAnalyseType_ID.ToString() + "";
            ret = return_SQL_Statement(SQL_Statement);

            return ret;
        }
        
        public int CreateRobotPosition(int nMachine_ID, string PositionNumber, string PositionName)
        {
            int ret = -1;
            int nMachinePosition_ID = -1;

            string SQL_Statement = "SELECT idmachine_positions FROM machine_positions WHERE Machine_ID=" + nMachine_ID + " AND PosNumber=" + PositionNumber;

            nMachinePosition_ID = return_SQL_Statement(SQL_Statement);

            if (nMachinePosition_ID > 0)   // if entry exist update value else insert new entry
            {
                SQL_Statement = "UPDATE machine_positions SET Name='" + PositionName + "',IsRobot=true WHERE idmachine_positions=" + nMachinePosition_ID;
            }
            else    // else: insert new entry with the values
            {
               SQL_Statement = "INSERT INTO machine_positions (Machine_ID,PosNumber,Name,IsRobot) VALUES ('" + nMachine_ID + "','" + PositionNumber + "','" + PositionName + "',true )";
           
            }
           ret = return_SQL_Statement(SQL_Statement);

            return ret;
        }

        public int CreateRobotMagazinePosition( string PositionName, string PositionNumber, string DimensionX, string DimensionY)
        {
            int ret = -1;
            int nMagazinePosition_ID = -1;

            string SQL_Statement = "SELECT idmagazine_configuration FROM magazine_configuration WHERE RobotMagazinePosition=" + PositionNumber;

            nMagazinePosition_ID = return_SQL_Statement(SQL_Statement);

            if (nMagazinePosition_ID > 0)   // if entry exist update value else insert new entry
            {
                SQL_Statement = "UPDATE magazine_configuration SET Name='" + PositionName + "', Dimension_X='" + DimensionX + "', Dimension_Y='" + DimensionY +"' WHERE idmagazine_configuration=" + nMagazinePosition_ID;
            }
            else    // else: insert new entry with the values
            {
                SQL_Statement = "INSERT INTO magazine_configuration (RobotMagazinePosition,Name,IsRobot,Dimension_X,Dimension_Y) VALUES (" + PositionNumber + ",'" + PositionName + "',true," + DimensionX + "," + DimensionY + ")";

            }
            ret = return_SQL_Statement(SQL_Statement);

            return ret;
        }

        public string GetTerminationStringFromTCPIPConfigurationByMachine_ID(int nMachine_ID)
        {
            string ret = null;
            string SQL_Statement = "SELECT TerminationString FROM tcpip_configuration Where Machine_ID=" + nMachine_ID.ToString() + "";
            ret = return_SQL_StatementAsString(SQL_Statement);
            if (ret != null)
            {
                switch (ret)
                {
                    /*
                     * '\\r\\n' geht beim Roboter nicht! Geändert auf '\r' bis aufs weitere
                     * Es muss noch gesucht werden, wo der Fehler liegt.
                     * */
                    case "\\r\\n":
                        ret = "\r\n";
                        break;
                    case "\\r":
                        ret = "\r";
                        break;
                    case "\\n":
                        ret = "\n";
                        break;
                    case "\\0":
                        ret = "\0";
                        break;
                }
            }
            return ret;
        }

        public void ReloadLabmanagerConnect()
        {     
            string SQL_Statement = "UPDATE communication SET ReloadLabmanagerConnect=1";
            return_SQL_Statement(SQL_Statement);
        }

        public bool CheckForLabManagerConnectRunning()
        {
            bool ret = false;
            try
            {
                Process[] proc = Process.GetProcessesByName("LabManagerConnect");
                if (proc.Length >= 1) { ret = true; }
            }
            catch (Exception)
            { }
            return ret;
        }

        public int GetMachine_IDFromMachinePositionsByMachinePositionID(int nMachinePosition_ID)
        {
            int nMachin_ID = -1;

            string SQL_Statement = "SELECT Machine_ID FROM machine_positions WHERE idmachine_positions=" + nMachinePosition_ID;
            nMachin_ID = return_SQL_Statement(SQL_Statement);
            return nMachin_ID;
        }
    }
}
