using System.Windows.Forms;
using System.Data;
using C1.Win.C1Ribbon;
using MySQL_Helper_Class;
using MySql.Data.MySqlClient;
using System.Collections.Specialized;
using System;
using Definition;
using C1.Win.C1FlexGrid;
using System.Reflection;
using System.Collections.Generic;
using cs_IniHandlerDevelop;
using Logging;
using System.Drawing;
using System.Net;
using System.Net.Sockets;

namespace Configuration
{
    public partial class TCPIPConfiguration : Configuration
    {
       private MySQL_HelperClass myHC = new MySQL_HelperClass();
       private MySqlDataAdapter da_TCPIP_Conf = new MySqlDataAdapter();
	   private C1FlexGrid c1FlexGrid_TCP_IP_Conf = null;
       private DataSet ds_TCPIP_Conf = new DataSet();
       private DataTable dt_TCPIP_Conf = null;
       private ListDictionary ld_TCPStations = null;
       private System.Windows.Forms.ErrorProvider errorProvider;
       private bool bInputError = false;
      
       Save mySave = new Save("TCPIP-Configuration Form");
       IniStructure myIniHandler = new IniStructure();
       Definitions myDefinitions = new Definitions();

        public TCPIPConfiguration(C1FlexGrid grid)
        {
            c1FlexGrid_TCP_IP_Conf = grid;
 
            // errorProvider1
            errorProvider = new System.Windows.Forms.ErrorProvider();
            errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
			

            string IniFilePath = myDefinitions.LanguageFile;
            myIniHandler = IniStructure.ReadIni(IniFilePath);

            grid.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1FlexGrid_TCPIPConfiguration_BeforeEdit);
            grid.GetCellErrorInfo += new C1.Win.C1FlexGrid.GetErrorInfoEventHandler(this.c1FlexGrid_Conditions_GetCellErrorInfo);
           // grid.GetRowErrorInfo += new C1.Win.C1FlexGrid.GetErrorInfoEventHandler(this.c1FlexGrid_Conditions_GetRowErrorInfo);
         
        }

        // start override methods from "Configuration" class

        public override void AddRow()
        {
            button_Add_Click();
        }

        public override void DeleteRows()
        {
            button_Delete_Click();
        }

        public override void SaveDataSet()
        {
            if (!bInputError)
            {
                button_Save_Click();
            }
            else { MessageBox.Show("There are errors on the table! Please check first all input fields.","Info"); }
        }

        public override void LoadConfigurationToGrid()
        {
            LoadTCPConfigurationToGrid();
            SetEditorAfterUpdateGrid();
        }

        // end override methods


        public void LoadTCPConfigurationToGrid()
        {
            if (c1FlexGrid_TCP_IP_Conf == null) { return; }

            string SQL_Statement = @"SELECT * FROM tcpip_configuration";


            if (SQL_Statement != null)
            {
                ds_TCPIP_Conf = new DataSet();

                da_TCPIP_Conf = myHC.GetAdapterFromSQLCommand(SQL_Statement, "");
                ds_TCPIP_Conf.Clear();
                c1FlexGrid_TCP_IP_Conf.Clear();
                da_TCPIP_Conf.Fill(ds_TCPIP_Conf);


                if (ds_TCPIP_Conf.Tables[0] != null)
                {
                    if (ds_TCPIP_Conf.Tables[0].Rows.Count > 0)
                    {
                        dt_TCPIP_Conf = ds_TCPIP_Conf.Tables[0];
                        c1FlexGrid_TCP_IP_Conf.DataSource = dt_TCPIP_Conf;
                        c1FlexGrid_TCP_IP_Conf.ExtendLastCol = true;

                    
                        c1FlexGrid_TCP_IP_Conf.Cols[0].Width = 15;
                        c1FlexGrid_TCP_IP_Conf.Cols[1].Width = 25;

                       
                       c1FlexGrid_TCP_IP_Conf.Cols[4].DataMap = GetListDictionaryForServerClient();

                        GetListDictionaryForTCPStations();
                        if (ld_TCPStations != null)
                        {
                            c1FlexGrid_TCP_IP_Conf.Cols[6].DataMap = ld_TCPStations;
                        }
                       c1FlexGrid_TCP_IP_Conf.Cols[8].DataMap = GetListDictionaryForAnalyserClass(); 
                      
                       c1FlexGrid_TCP_IP_Conf.Cols[9].DataMap = GetListDictionaryForTerminationString(); 
                      
                        
                        c1FlexGrid_TCP_IP_Conf.Cols[1].Caption = "ID";
                        c1FlexGrid_TCP_IP_Conf.Cols[2].Caption = "Name";
                        c1FlexGrid_TCP_IP_Conf.Cols[3].Caption = "Port";
                        c1FlexGrid_TCP_IP_Conf.Cols[4].Caption = "Type";
                        c1FlexGrid_TCP_IP_Conf.Cols[5].Caption = "IP address";
                        c1FlexGrid_TCP_IP_Conf.Cols[6].Caption = "Station";
                        c1FlexGrid_TCP_IP_Conf.Cols[7].Caption = "on/off";
                        c1FlexGrid_TCP_IP_Conf.Cols[7].Style = c1FlexGrid_TCP_IP_Conf.Styles["Boolean"];
                        c1FlexGrid_TCP_IP_Conf.Cols[8].Caption = "Analyser class";
                        c1FlexGrid_TCP_IP_Conf.Cols[9].Caption = "Termination string";
                        c1FlexGrid_TCP_IP_Conf.Cols[10].Caption = "Description";
                 
                        c1FlexGrid_TCP_IP_Conf.Cols[8].Width = 220;
                    
                        c1FlexGrid_TCP_IP_Conf.AutoResize = true;
                        c1FlexGrid_TCP_IP_Conf.AutoSizeCols(1, 9, 10);
                        c1FlexGrid_TCP_IP_Conf.Update();
                    }
                }
            }

        }

        private ListDictionary GetListDictionaryForServerClient()
        {
            ListDictionary ld_temp = null;

                    ld_temp = new ListDictionary();
                    ld_temp.Add(0, "Server");
                    ld_temp.Add(1, "Client");

                    return ld_temp;
        }

        private ListDictionary GetListDictionaryForAnalyserClass()
        {
            ListDictionary ld_AnalayserClass = null;
   
            ld_AnalayserClass = new ListDictionary();
            string[] names = Enum.GetNames(typeof(TCPIPAnalyseClass));
            int[] values = (int[])Enum.GetValues(typeof(TCPIPAnalyseClass));

            for( int i = 0; i < names.Length; i++ )
            {
                  ld_AnalayserClass.Add(values[i], names[i]);
            }
            
             return ld_AnalayserClass;
        }

        private ListDictionary GetListDictionaryForTerminationString()
        {
            ListDictionary ld_TerminationString = null;
            ld_TerminationString = new ListDictionary();

            ld_TerminationString.Add("", " ");
            ld_TerminationString.Add("\\r", "CR");
            ld_TerminationString.Add("\\r\\n", "CR+LF");
            ld_TerminationString.Add("\\0", "NULL");

            return ld_TerminationString;
        }

        private ListDictionary GetListDictionaryForTCPStations()
        {
            // load Machines Dictionary
            DataSet TCPStationsNames = new DataSet();
            TCPStationsNames.Clear();
            TCPStationsNames = myHC.GetDataSetFromSQLCommand(@"SELECT         machines.idmachines,machines.Name
            FROM            machine_list INNER JOIN
                         machines ON machine_list.idmachine_list = machines.Machine_list_ID
            WHERE        (machine_list.Connection_type_list_ID = 4)");

            if (TCPStationsNames.Tables[0] != null)
            {
                if (TCPStationsNames.Tables[0].Rows.Count > 0)
                {
                    DataTable dt_TCPStations = TCPStationsNames.Tables[0];
                    ld_TCPStations = new ListDictionary();
                    ld_TCPStations.Add(0,"Please Select");
                    foreach (DataRow dr_TCPStations in dt_TCPStations.Rows)
                    {
                        // ItemArray[0] = id
                        // ItemArray[1] = name
                        ld_TCPStations.Add((int)dr_TCPStations.ItemArray[0], dr_TCPStations.ItemArray[1]);
                    }
                }
            }
            return ld_TCPStations;
        }


        private void button_Add_Click()
        {
            if (dt_TCPIP_Conf != null)
            {
                try
                {
                    DataRow dr_new = dt_TCPIP_Conf.Rows.Add();

                }
                catch (DBConcurrencyException DBCe)
                {
                    mySave.InsertRow((int)Definition.Message.D_DEBUG, "button_Add_Click: \r\n" + DBCe.ToString());
                }
                catch { }
                c1FlexGrid_TCP_IP_Conf.RowSel = c1FlexGrid_TCP_IP_Conf.Rows.Count - 1;
                SetEditorAfterUpdateGrid();
            }
        }

        private void button_Delete_Click()
        {

            if (c1FlexGrid_TCP_IP_Conf.RowSel >= 0)
            {
                DataRowView dr_delete = (DataRowView)c1FlexGrid_TCP_IP_Conf.Rows[c1FlexGrid_TCP_IP_Conf.RowSel].DataSource;
                if (dr_delete != null)
                {
                     //, dr_delete["Name"].ToString(), dr_delete["idmagazine_configuration"].ToString()
                    string question = String.Format(myIniHandler.GetValue("Miscellaneous", "Delete_Question"), dr_delete["Name"].ToString(), dr_delete["idTCPIP_configuration"].ToString());
               
                    if (MessageBox.Show(question, String.Format(myIniHandler.GetValue("Admin-Machines", "Delete_Confirm")), System.Windows.Forms.MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        try
                        {
                            dr_delete.Delete();
                            MySqlCommandBuilder myCommand = new MySqlCommandBuilder(da_TCPIP_Conf);
                            da_TCPIP_Conf.Update(dt_TCPIP_Conf);
                            c1FlexGrid_TCP_IP_Conf.Update();
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
                    MessageBox.Show("no row selected!","error" );
                }
            }
        }

        private void button_Save_Click()
        {
            try
            {
                MySqlCommandBuilder myCommand = new MySqlCommandBuilder(da_TCPIP_Conf);
                da_TCPIP_Conf.Update(dt_TCPIP_Conf);
                LoadTCPConfigurationToGrid();
            }
            catch (DBConcurrencyException DBCe)
            {
                mySave.InsertRow((int)Definition.Message.D_DEBUG, "button_Save_Click: \r\n" + DBCe.ToString());
            }
            catch (Exception ex) { mySave.InsertRow((int)Definition.Message.D_ALARM, ex.ToString()); }
        }


        private void c1FlexGrid_TCPIPConfiguration_BeforeEdit(object sender, RowColEventArgs e)
        {
            C1FlexGrid grid = sender as C1FlexGrid;

            if (e.Row >= 1 && e.Row == grid.RowSel)
            {
                int nType = -1;
                try
                {
                    nType = Int32.Parse(grid.GetData(e.Row, "Type").ToString());

                    if (e.Col == 5)
                    {
                        if (nType == 0)
                        {
                            e.Cancel = true;
                        }
                    }
                }
                catch { }
            }
        }

        private void SetEditorAfterUpdateGrid()
        {
            C1FlexGrid grid = c1FlexGrid_TCP_IP_Conf;

            foreach (Row rowConf in grid.Rows)
            {
                // editor for Port number
                CellRange rg = grid.GetCellRange(rowConf.Index, 3, rowConf.Index, 3);
                CellStyle cs = grid.Styles.Add("Port_Address");

                MaskedTextBox.MaskedTextBox tbPort = new MaskedTextBox.MaskedTextBox();
                tbPort.Masked = MaskedTextBox.Mask.Digit;
               // tbPort.MaxLength = 5;

                cs.Editor = tbPort;
                
                rg.Style = grid.Styles["Port_Address"];
                
                //editor ip address
                rg = grid.GetCellRange(rowConf.Index, 5, rowConf.Index, 5);
                cs = grid.Styles.Add("IP_Address");

                MaskedTextBox.MaskedTextBox tb = new MaskedTextBox.MaskedTextBox();
                tb.Masked = MaskedTextBox.Mask.IpAddress;
                cs.Editor = tb;

                rg.Style = grid.Styles["IP_Address"];

            
            }
        }

        private void c1FlexGrid_Conditions_GetCellErrorInfo(object sender, GetErrorInfoEventArgs e)
        {
            
            C1FlexGrid grid = sender as C1FlexGrid;
            int nPort = -1;

            bInputError = false;

            if (grid.Cols[e.Col].Name == null) { return; }
            if (e.Row < 0) { return; }

            
           if (e.Row > 0)
           {
               try
               {
                   Int32.TryParse(grid.GetData(e.Row, "Port").ToString(), out nPort);
               }
               catch {  }
           }

            if (grid.Cols[e.Col].Name == "Port" && e.Row > 0) // port field
             {
                 if (nPort <= 0 || nPort > 65535)
                 {
                     //Console.WriteLine(e.Row.ToString());
                     e.ErrorText = "Please enter a valid Port number! (1-65535)";
                     bInputError = true;
                    // return;
                 }
                 else { e.ErrorText = ""; }

             }
           
             if (e.Col == 5 && e.Row > 0) // ip address field
             {
                 int nType = -1;
                 if (grid.GetData(e.Row, "Type") != null)
                 {
                     Int32.TryParse(grid.GetData(e.Row, "Type").ToString(), out nType);
                     if (nType == 1)// client
                     {
                         if (!IsIPv4(grid.GetData(e.Row, 5).ToString()))
                         {
                             e.ErrorText = "not a valid ip address";
                             bInputError = true;
                         }
                         else { e.ErrorText = ""; }
                     }
                     else
                     {
                         e.ErrorText = "";
                     }
                 }
                 else { e.ErrorText = "type is null"; bInputError = true; }
             }
              
            if (grid.Cols[e.Col].Name == "Name" && e.Row > 0) // Name field
            {
                if (grid.GetData(e.Row, "Name") != null)
                {
                    if (grid.GetData(e.Row, "Name").ToString().Length < 1)
                    {
                        e.ErrorText = "name to short";
                        bInputError = true;
                        // return;
                    }
                    else
                    {
                        e.ErrorText = "";
                    }
                }
                else { e.ErrorText = "name is null"; bInputError = true; }
            }

            if (grid.Cols[e.Col].Name == "Station" && e.Row > 0) // station field
            {
                int nStation_ID = -1;
                Int32.TryParse(grid.GetData(e.Row, "Station").ToString(), out nStation_ID);
                if (nStation_ID < 1)
                {
                    e.ErrorText = "name to short";
                    bInputError = true;
                    //return;
                }
                else
                {
                    e.ErrorText = "";
                }
            }
            string t = bInputError? "True":"False";
            Console.WriteLine(t);


        }

        public static bool IsIPv4(string value)
        {
            IPAddress address;

            if (IPAddress.TryParse(value, out address))
            {
                if (address.AddressFamily == AddressFamily.InterNetwork)
                {
                    return true;
                }
            }

            return false;
        }

      /*  void c1FlexGrid_Conditions_GetRowErrorInfo(object sender, C1.Win.C1FlexGrid.GetErrorInfoEventArgs e)
        {
            C1FlexGrid grid = sender as C1FlexGrid;
            if (e.Row > 0)
            {
                e.ErrorText = "This row contains errors! Please check that the inputs.";
               
            }
        }
        */
      
    }
}
