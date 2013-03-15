using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data;
using Logging;
using Definition;
using cs_IniHandlerDevelop;
using System.Diagnostics;


namespace MySQL_Connector
{
    class MySQL_Connection
    {
        private string _ConnectionString = null;
        private Save mySave = new Save("MySQL Connector");
        private Definitions definitions = new Definitions();
        private IniStructure inis = new IniStructure();
        private Object MySQLLock = new Object();
        private static int nConnections = 0;
        private double nSQL_Statement_Calls = 0;
        private static double nSQL_Statement_Calls_total = 0;

        private MySqlConnection connectionReturnObect = null;
        private MySqlCommand commandReturnObect = null;

        private bool bWriteMySQL_Log = false;
        System.IO.StreamWriter file;
        DateTime dtStart;
      
        public MySQL_Connection()
        {
            if (_ConnectionString == null)
            {
                _ConnectionString = SetConnectionString();
            }
         
            connectionReturnObect = new MySqlConnection(_ConnectionString);
            commandReturnObect = new MySqlCommand( );
            commandReturnObect.Connection = connectionReturnObect;
            commandReturnObect.CommandType = global::System.Data.CommandType.Text;
            connectionReturnObect.Open();

            nConnections++;
            Console.WriteLine("connections+:" + nConnections );
            
            //for testing only
            if (bWriteMySQL_Log)
            {
                file = new System.IO.StreamWriter(definitions.LogPath + @"MySQL_Log_" + nConnections + ".txt");
            }


            dtStart = DateTime.Now;
            
            
        }


     
        ~MySQL_Connection()
        {
            DateTime dtStop= DateTime.Now;
            TimeSpan runTime =  dtStop - dtStart ;
           
            Console.WriteLine("connection {0}: {1}/seconds ({2} runtime)", nConnections, (nSQL_Statement_Calls/runTime.TotalSeconds), runTime.TotalSeconds.ToString());
            nConnections--;
            if (nConnections == 0)
            {
                Console.WriteLine("total calls: {0} in {1} seconds ({2}/seconds) ", nSQL_Statement_Calls_total, runTime.TotalSeconds.ToString(), (nSQL_Statement_Calls_total / runTime.TotalSeconds));
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

	   private void FileWriteLine(string SQL_Statement)
       {

           if (bWriteMySQL_Log) { file.WriteLine(Environment.StackTrace + "###"+DateTime.Now.ToString("HH:mm:ss tt") + "#" + Stopwatch.GetTimestamp() + ": -->" + SQL_Statement+"<--\r\n"); }
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

            conString = "server= " + inis.GetValue("DB", "server") + ";User Id=" + inis.GetValue("DB", "UserId") + ";password=" + inis.GetValue("DB", "password") + ";database=" + inis.GetValue("DB", "database") + ";Persist Security Info=no";
         
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
                    if(connection!=null)
                    {
                        connection.Close();
                    }
                    return true;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); return false; }
        }

        public MySqlConnection GetMySqlConnection()
        {

            return connectionReturnObect;
        }

        public MySqlCommand GetMySqlCommand()
        {

            return commandReturnObect;
        }

       
    }
}
