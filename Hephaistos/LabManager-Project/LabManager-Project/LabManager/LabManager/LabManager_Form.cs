using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using Logging;
using Definition;
using MySQL_Helper_Class;
using Routing;
using Magazine;
using System.Collections;
using AssemblyLoad;
using PipesServerTest;
using PipesClientTest;

namespace LabManager
{

    public partial class LabManager : Form
    
    {
        private System.Windows.Forms.Timer LabManagerTimer = new System.Windows.Forms.Timer();        
        MySQL_HelperClass myHC = new MySQL_HelperClass();
        Definitions myThorDef = new Definitions();
        Save mySave = new Save("LabManager");
        public volatile bool LockRoutingCheck = true;
        private bool RunModeChangedToOn = false;
        private bool RunModeChangedToOff = false;
        private bool LabManagerConnectReady = false;
        private bool LabManagerConnectMessageOffline = false;
        private bool LabManagerWinCCReady = false;
        private bool LabManagerWinCCMessageOffline = false;
        private int nTimeoutConnectionToLabManagerConnect = 0;
        private int nTimeoutConnectionToLabManagerConnectStartup = 3;
        private int nTimeoutConnectionToWinCCStartup = 3;
        private int nTimeoutConnectionToWinCC= 8;
        private int nMaxLinesInLogTextboxes = 256;
        private Thread MagazineThread = null;
        private Thread TCPIPThread = null;
        private Thread EpsonRobotAdminDialogThread = null;
        private RoutingCommand routingCommand = null;
        private Magazine_Driver[] MagazineArray = null;
        private TCPIP_Channel[] TcpipChannelArray = null;
        private Hashtable MagazineHashTable = new Hashtable();
        private Hashtable TCPIPHashTable = new Hashtable();
        private Object LabManagerLock = new Object();    
        public delegate void NewMessageDelegate(string NewMessage);
        private PipeServer _pipeServer;

   
        public LabManager()
        {
            Application.EnableVisualStyles();
            
            this.StartPosition = FormStartPosition.CenterScreen;
         
            try
            {
                this.Icon = new Icon(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("LabManager.LM-App.ico"));
             }
            catch { }

            InitializeComponent();

           // inis = IniStructure.ReadIni(myThorDef.PathIniFile);

            InsertLoggingEntry((int)Definition.ThorLogWindows.ROUTING, "starting LabManager ...");   
           
            string[] args = Environment.GetCommandLineArgs();

            foreach (string arg in args)
            {
                if (arg == "/minimize" || arg == "/minimized")
                {
                    this.WindowState = FormWindowState.Minimized;
                }
                if (arg == "/nonvisible")
                {
                    this.Visible = false;
                }
            }


           
            myHC.SetLabManagerConnectReady("WinCCReady", nTimeoutConnectionToWinCCStartup);
            ribbonLabel_WinCC.SmallImage = _imgList.Images["OrangeBlock"];
            ribbonLabel_Connect.SmallImage = _imgList.Images["OrangeBlock"];
            ribbonLabelRoutingCheck.SmallImage = _imgList.Images["RedBlock"];

            _pipeServer = new PipeServer();
            _pipeServer.PipeMessage += new DelegateMessage(PipesMessageHandler);

          
        }

        private void PipesMessageHandler(string message)
        {
            try
            {
                if (this.InvokeRequired)
                {
                   // this.Invoke(new NewMessageDelegate(PipesMessageHandler), message);
                    Console.WriteLine(message);
                }
                else
                {
                    Console.WriteLine(message);
                }
                if (message.StartsWith("RobotRemoteForm"))
                {
                    ThreadShowDialog();
                }
                else if (message.StartsWith("ReloadRoutingTableData"))
                {
                    ReloadRoutingTableData();
                }
                else if (message.StartsWith("Logging_Form"))
                {
                    Assembly_Load myAssembly = new Assembly_Load();
                    myAssembly.RemoteObject(4, 1, 0, "");
                   
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

        }

       
     
        private void LabManager_Load(object sender, EventArgs e)
        {
            // add MenuItems to Configuration
            AddSubMenuToConfigurationMenuButton();
                     
           // start routing Command object, that sends the commands (and it trickers the routing check aswell)
           routingCommand = new RoutingCommand(this);
         

            // starting the TCPIP servers
            TCPIPThread = new Thread(new ThreadStart(TCPIPLoad));
            TCPIPThread.Name = "TCPIPThread";
            //TCPIPThread.Start();
            SetupTCPIPChannels();

           
            // starting the magazines
            MagazineThread = new Thread(new ThreadStart(MagazineLoad));
            MagazineThread.Name = "MagazineThread";
            //MagazineThread.Start();
            SetupMagazines();
          
            //process the timer event to the timer. 
            LabManagerTimer.Tick += new EventHandler(TimerEventProcessor);

            // Set the timer interval to 1 second.
            LabManagerTimer.Interval = 1000;
            LabManagerTimer.Start();


             
            try
            {
                _pipeServer.Listen("LMPipe");            
            }
            catch (Exception)
            {    
               InsertLoggingEntry((int)Definition.ThorLogWindows.ROUTING, "Error Listening on pipe");               
            }

           
        }


        private void ReloadRoutingTableData()
        {
            if (MessageBox.Show("Do you want to reload the routing data?", "Question", System.Windows.Forms.MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                routingCommand.ReloadRoutingTable();
            }
        }
       
        
        void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            if (!this.Visible) { return; }
         
            // get status of LabManagerConnect
            try
            {
                if (nTimeoutConnectionToLabManagerConnectStartup >= 0)
                {
                    nTimeoutConnectionToLabManagerConnectStartup--;
                }
                else
                {
                    nTimeoutConnectionToLabManagerConnect = myHC.CheckForLabManagerConnectReady("LabManagerConnectTimeout");
                    if (nTimeoutConnectionToLabManagerConnect <= 0)
                    {
                        nTimeoutConnectionToLabManagerConnect = 0;
                        LabManagerConnectReady = false;
                        WriteStatusbarLeft("no communication with LabManagerConnect", true);
                    }
                    else
                    {
                        if (!LabManagerConnectReady)
                        {
                            InsertLoggingEntry((int)Definition.ThorLogWindows.ROUTING, "Routing started, communication with LabManagerConnect established");
                        }
                        LabManagerConnectReady = true;
                        LabManagerConnectMessageOffline = false;
                        WriteStatusbarLeft("communication with LabManagerConnect established", false);
                    }
                   
                    if (!LabManagerConnectReady && !LabManagerConnectMessageOffline)
                    {
                        LabManagerConnectMessageOffline = true;
                        InsertLoggingEntry((int)Definition.ThorLogWindows.ERROR, "no communication with LabManagerConnect", (int)Definition.Message.D_ALARM);
                    }

                    if (!LabManagerConnectReady)
                    {

                        ribbonLabel_Connect.SmallImage = _imgList.Images["RedBlock"];
                    }
                    else
                    {
                        ribbonLabel_Connect.SmallImage = _imgList.Images["GreenBlock"];
                    }
                }
             //   nTimeoutConnectionToLabManagerConnect--;
             //   myHC.SetLabManagerConnectReady("LabManagerConnectTimeout", nTimeoutConnectionToLabManagerConnect);

            }
            catch (Exception ex) { InsertLoggingEntry((int)Definition.ThorLogWindows.ERROR, ex.ToString(), (int)Definition.Message.D_ALARM); }



            // set status of LabManagerTimeout
            try
            {
                int nTimeout = 0;
                nTimeout = myHC.CheckForLabManagerConnectReady("LabManagerTimeout");
                if (nTimeout <= 3)
                    {
                        myHC.SetLabManagerConnectReady("LabManagerTimeout", 6);
                    }
               
            }
            catch { }

            // get status of WinCC
            try
            {
                if (nTimeoutConnectionToWinCCStartup >= 0)
                {
                    nTimeoutConnectionToWinCCStartup--;
                   
                }
                else
                {
                    nTimeoutConnectionToWinCC = myHC.CheckForWinCCReady();
                    if (nTimeoutConnectionToWinCC <= 0)
                    {
                        nTimeoutConnectionToWinCC = 0;
                        LabManagerWinCCReady = false;
                        WriteStatusbarLeft("no communication with WinCC", true);
                    }
                    else
                    {
                        LabManagerWinCCReady = true;
                        LabManagerWinCCMessageOffline = false;
                        WriteStatusbarLeft("communication with WinCC established", false);
                    }
                   
                  
                    if (!LabManagerWinCCReady && !LabManagerWinCCMessageOffline)
                    {
                        LabManagerWinCCMessageOffline = true;
                        InsertLoggingEntry((int)Definition.ThorLogWindows.ERROR, "no communication with WinCC", (int)Definition.Message.D_ALARM);
                    }

                    if (!LabManagerWinCCReady)
                    {

                        ribbonLabel_WinCC.SmallImage = _imgList.Images["RedBlock"];
                    }
                    else
                    {
                        ribbonLabel_WinCC.SmallImage = _imgList.Images["GreenBlock"];
                    }
                    nTimeoutConnectionToWinCC--;
                }
                
                myHC.SetLabManagerConnectReady("WinCCReady", nTimeoutConnectionToWinCC);

                
            }
            catch (Exception ex) { InsertLoggingEntry((int)Definition.ThorLogWindows.ERROR, ex.ToString(), (int)Definition.Message.D_ALARM); }
            
            // get status of RUN/STOP Mode
            try
            {
                if (myHC.CheckForRunModeOnWithWinCC() != 1 || !LabManagerConnectReady ) 
                {
                    
                    LockRoutingCheck = true;
                    
                    if (!RunModeChangedToOff)
                    {
                        InsertLoggingEntry((int)Definition.ThorLogWindows.ROUTING, "switched to STOP-Mode!!!");
                        RunModeChangedToOff = true;
                        RunModeChangedToOn = false;
                    }
                }
                else
                {
                    if (LabManagerConnectReady) // only if LabManagerConnect is ready unlock Routing
                    {
                        if (LockRoutingCheck)
                        {
                    //        InsertLoggingEntry((int)Definition.ThorLogWindows.ROUTING, "Routing started, communication with LabManagerConnect established");
                        }
                        if (LabManagerWinCCReady) // only if WinCC is ready unlock Routing
                        {
                            LockRoutingCheck = false;
                        }
                        else 
                        { 
                            LockRoutingCheck = true; 
                        }
                    } 
                    else { LockRoutingCheck = true; }

                    if (!RunModeChangedToOn)
                    {
                        InsertLoggingEntry((int)Definition.ThorLogWindows.ROUTING, "WinCC in RUN-Mode");
                        RunModeChangedToOn = true;
                        RunModeChangedToOff = false;
                    }
                }
               
            }
            catch (Exception ex) { InsertLoggingEntry((int)Definition.ThorLogWindows.ERROR, ex.ToString(), (int)Definition.Message.D_ALARM); }

            // setIcon for RoutingCheck
            if (LockRoutingCheck)
            {
                ribbonLabelRoutingCheck.SmallImage = _imgList.Images["RedBlock"];
            }
            else
            {
                ribbonLabelRoutingCheck.SmallImage = _imgList.Images["GreenBlock"];
            }

            try
            {
                bool bAllow = myHC.GetRightsFromUserAdministration((int) Definition.Rights.PROCESS);
                if (bAllow)
                {
                    menuItem_Routing.Enabled = true;
                    menuItem_Machines.Enabled = true;
                   
                }
                else
                {
                    menuItem_Routing.Enabled = false;
                    menuItem_Machines.Enabled = false;
                    
                }
            }
            catch { }

            // call SQL Procedure "CalledEverySecond"
            try
            {
                myHC.CallMySQLProcedureOnTime();
            }
            catch (Exception ex){ InsertLoggingEntry((int)Definition.ThorLogWindows.ERROR, ex.ToString(), (int)Definition.Message.D_ALARM );}

            // clean the textboxes if there are too many lines
            CleanTextBoxes();
        }

        private void CleanTextBoxes()
        {
            int nLinesToDelete = 8;

            if (c1TextBox_Routing_Log.Lines.Count() > nMaxLinesInLogTextboxes)
            {
               var lines = (from item in c1TextBox_Routing_Log.Text.Split('\n') select item.Trim());
               lines = lines.Skip(nLinesToDelete);
                c1TextBox_Routing_Log.Text = string.Join(Environment.NewLine, lines.ToArray());
            }
           
            if (c1TextBox_ERROR_Log.Lines.Count() > nMaxLinesInLogTextboxes)
            {
                var lines = (from item in c1TextBox_ERROR_Log.Text.Split('\n') select item.Trim());
                lines = lines.Skip(nLinesToDelete);
                c1TextBox_ERROR_Log.Text = string.Join(Environment.NewLine, lines.ToArray());
            }

            if (c1TextBox_WARNING_Log.Lines.Count() > nMaxLinesInLogTextboxes)
            {
                var lines = (from item in c1TextBox_WARNING_Log.Text.Split('\n') select item.Trim());
                lines = lines.Skip(nLinesToDelete);
                c1TextBox_WARNING_Log.Text = string.Join(Environment.NewLine, lines.ToArray());
            }

            if (c1TextBox_Communication_Log.Lines.Count() > nMaxLinesInLogTextboxes)
            {
                var lines = (from item in c1TextBox_Communication_Log.Text.Split('\n') select item.Trim());
                lines = lines.Skip(nLinesToDelete);
                c1TextBox_Communication_Log.Text = string.Join(Environment.NewLine, lines.ToArray());
            }
        }

       private void AddSubMenuToConfigurationMenuButton()
       {
            DataSet ds_ConfigurationMenu = new DataSet();
            DataTable dt_ConfigurationMenu = new DataTable();
          
            string strSQL_Statement = "SELECT idconfiguration_tables,Name,TableName,SQLLoadCommand FROM configuration_tables ORDER BY Name";
            //  ds_ConfigurationMenu = myHC_ConfigurationMenu.GetDataSetFromSQLCommand(strSQL_Statement);
            ds_ConfigurationMenu = myHC.GetDataSetFromSQLCommand(strSQL_Statement);
            if (ds_ConfigurationMenu != null)
            {
                if (ds_ConfigurationMenu.Tables.Count > 0)
                {
                    dt_ConfigurationMenu = ds_ConfigurationMenu.Tables[0];
                    foreach (DataRow dr_ConfigurationMenu in dt_ConfigurationMenu.Rows)
                    {
                        menuItem_Conf.MenuItems.Add(dr_ConfigurationMenu.ItemArray[1].ToString(), new System.EventHandler(this.menuItem_Conf_Click));
                    }
                }
            }
       }

        private void SetupMagazines()
        {
            lock (LabManagerLock)
            {
                DataSet ds_Magazines = new DataSet();
                DataTable dt_Magazines = new DataTable();
               
                string strSQL_Statement = "Select idmagazine_configuration,Machine_ID FROM magazine_configuration ORDER BY idmagazine_configuration";
                ds_Magazines = myHC.GetDataSetFromSQLCommand(strSQL_Statement);
                if (ds_Magazines != null)
                {
                    if (ds_Magazines.Tables.Count > 0)
                    {
                        dt_Magazines = ds_Magazines.Tables[0];
                        MagazineArray = new Magazine_Driver[dt_Magazines.Rows.Count];
                        int nMag = 0;
                        foreach (DataRow dr_magazines in dt_Magazines.Rows)
                        {
                            //  System.Threading.Thread.Sleep(100); 
                            MagazineArray[nMag] = new Magazine_Driver(this, (int)dr_magazines["idmagazine_configuration"]);
                            MagazineHashTable.Add(dr_magazines["Machine_ID"], nMag);
                            nMag++;
                        }
                    }
                }
            }
        }

        private void SetupTCPIPChannels()
        {

            lock (LabManagerLock)
            {
                DataSet ds_TCPIPChannel = new DataSet();
                DataTable dt_TCPIPChannel = new DataTable();
                int nTCPIPChannnel = 0;

                //Type=0 => Server, Type=1 => Client
                string strSQL_Statement = "Select Port,IP_Address,Machine_ID,AnalyseType_ID,Type FROM TCPIP_configuration WHERE Activate=1";
                ds_TCPIPChannel = myHC.GetDataSetFromSQLCommand(strSQL_Statement);
                if (ds_TCPIPChannel != null)
                {
                    if (ds_TCPIPChannel.Tables.Count > 0)
                    {
                        dt_TCPIPChannel = ds_TCPIPChannel.Tables[0];
                        TcpipChannelArray = new TCPIP_Channel[dt_TCPIPChannel.Rows.Count];

                        foreach (DataRow dr_TCPIPChannels in dt_TCPIPChannel.Rows)
                        {
                            try
                            {
                                int nPort = (int)dr_TCPIPChannels["Port"];
                                int nMachine_ID = (int)dr_TCPIPChannels["Machine_ID"];
                                int nAnalyseTypeID = (int)dr_TCPIPChannels["AnalyseType_ID"];
                                string strIPAddress = dr_TCPIPChannels["IP_Address"].ToString();
                                int nType = (int)dr_TCPIPChannels["Type"];
                                TCPIP_Helper.TCPIPHelper tcp_Helper = new TCPIP_Helper.TCPIPHelper(this);
                                // load the TCPIP server objects
                                // (LabManager parent, int nPort,string strIP_Address, int nMachine_ID = -1, int nAnalyseTypeID = -1, int nType=-1)
                                TcpipChannelArray[nTCPIPChannnel] = new TCPIP_Channel(nPort, strIPAddress, nMachine_ID, nAnalyseTypeID, nType, tcp_Helper);
                                TCPIPHashTable.Add(nMachine_ID, nTCPIPChannnel);
                                nTCPIPChannnel++;
                                string strTCPIPType = "client";
                                if (nType == 0) { strTCPIPType = "server"; }
                                WriteTCPIPLoggEntry((int)Definition.ThorLogWindows.ROUTING,
                                    " loading TCPIP channel on port: " + nPort + " for station: `" + myHC.GetNameFromID((int)Definition.SQLTables.MACHINES, nMachine_ID) + "` as " + strTCPIPType
                                    , (int)Definition.Message.D_MESSAGE);
                            }
                            catch (Exception ex)
                            {
                                WriteTCPIPLoggEntry((int)Definition.ThorLogWindows.ERROR, ex.ToString(), (int)Definition.Message.D_ALARM);
                            }
                        }
                    }
                    else { InsertLoggingEntry((int)Definition.ThorLogWindows.ERROR, "no configuration entries found in table 'TCPIP_configuration'", (int)Definition.Message.D_ALARM); }
                }
                else { InsertLoggingEntry((int)Definition.ThorLogWindows.ERROR, "no configuration entries found in table 'TCPIP_configuration' dataset is null", (int)Definition.Message.D_ALARM); }
            }
        }

        public bool SendCommadToMagazine_Driver(int nDriverArray, int nNumber, int nSample_ID, int nPos, int nMachine_ID)
        {
            bool ret = false;
            
            MagazineArray[(int)MagazineHashTable[nMachine_ID]].SendCommand(nNumber, nSample_ID, nPos, nMachine_ID);
            return ret;
        }

        public int SendCommadToTCPIP(int nNumber, int nSample_ID, string strCommand, int nMachine_ID)
        {
           
            int returnValue = -1;
           // int returnValue = tcpip_server.SendCommand(nNumber, strCommand, nSample_ID, nMachine_ID);
            try
            {
                if ((int)TCPIPHashTable[nMachine_ID] >= 0)
                {
                    if (TcpipChannelArray[(int)TCPIPHashTable[nMachine_ID]] != null)
                    {
                        returnValue = TcpipChannelArray[(int)TCPIPHashTable[nMachine_ID]].SendCommand(nNumber, strCommand, nSample_ID, nMachine_ID);

                    }
                }
            }
            catch (Exception ex) { WriteTCPIPLoggEntry((int)Definition.ThorLogWindows.ERROR, "Exception at LabManager::SendCommadToTCPIP from machine with ID: " + nMachine_ID + "(number: " + nNumber + ", command: " + strCommand + ", sample_ID: " + nSample_ID + ")\r\n" + ex.ToString(), (int)Definition.Message.D_ALARM); }
      
            return returnValue;
        }

    
        public int GetTCPIP_Info(int nInfoType, int nInfo, int nMachine_ID)
        {
            int returnValue = -1;

            MethodInvoker GetInfo = delegate
            {
               if ((int)TCPIPHashTable[nMachine_ID] >= 0)
               {
                if (TcpipChannelArray[(int)TCPIPHashTable[nMachine_ID]] != null)
                {
                    returnValue = TcpipChannelArray[(int)TCPIPHashTable[nMachine_ID]].GetInfo(nInfoType, nInfo);
                }
               }
            };
            try
            {
                Invoke(GetInfo);
            }
            catch (Exception ex){ WriteTCPIPLoggEntry((int)Definition.ThorLogWindows.ERROR, "Exception at LabManager::GetTCPIP_Info \r\n" + ex.ToString(), (int)Definition.Message.D_ALARM); }
               
            return returnValue;
        }

       

        private void MagazineLoad()
        {
            SetupMagazines();
             
        }

        private void TCPIPLoad()
        {
            SetupTCPIPChannels();
        }

        public void WriteTCPIPLoggEntry(int LogType, string strLogString, int AlarmType = (int)Definition.Message.D_MESSAGE)
        {

                MethodInvoker Logging = delegate
                {
                     InsertLoggingEntry(LogType, strLogString, AlarmType);     
                };
                try
                {
                    Invoke(Logging);
                }
                catch (System.InvalidOperationException ioe) 
                {
                    Console.WriteLine("LabManager::WriteTCPIPLoggEntry: System.InvalidOperationException: " + ioe.ToString());
                }
                catch (Exception ex)
                {
                    WriteTCPIPLoggEntry((int)Definition.ThorLogWindows.ERROR, "LabManager::WriteTCPIPLoggEntry: exception!\r\n" + ex.ToString(), (int)Definition.Message.D_ALARM);
                }

        }

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            
                LabManagerTimer.Stop();

                // to stop the routingChecker
                LockRoutingCheck = true;

                
                InsertLoggingEntry((int)Definition.ThorLogWindows.ROUTING, "Routing stopped!");

             
                // stopping magazine thread
               if (MagazineThread != null && MagazineThread.IsAlive)
                {
                  MagazineThread.Abort(); 
                }

            

               // stopping TCPIP thread
               if (TCPIPThread != null && TCPIPThread.IsAlive)
               {
                   try
                   {
                       TCPIPThread.Abort();
                   }
                   catch (System.Threading.ThreadAbortException ex) {
                       WriteTCPIPLoggEntry((int)Definition.ThorLogWindows.ERROR, "LabManager::Dispose: exception!\r\n" + ex.ToString(), (int)Definition.Message.D_ALARM);
                   }
               }

               if (TcpipChannelArray != null)
                {
                    for (int k = 0; k < TcpipChannelArray.Length; k++)
                    {
                        if (TcpipChannelArray[k] != null)
                        {
                            try
                            {
                                TcpipChannelArray[k].StopThread();
                            }
                            catch (System.Net.Sockets.SocketException se)
                            {
                                WriteTCPIPLoggEntry((int)Definition.ThorLogWindows.ERROR, "LabManager::Dispose: SocketException!\r\n" + se.ToString(), (int)Definition.Message.D_ALARM);
                            }
                            catch (System.Threading.ThreadAbortException tae)
                            {
                                WriteTCPIPLoggEntry((int)Definition.ThorLogWindows.ERROR, "LabManager::Dispose: ThreadAbortException!\r\n" + tae.ToString(), (int)Definition.Message.D_ALARM);
                            }
                        }
                    }
                }
            
              

                InsertLoggingEntry((int)Definition.ThorLogWindows.ERROR, "LabManager stopped!");



                if (disposing && (components != null))
                {
                    components.Dispose();
                    //
                }
                base.Dispose(disposing);
           
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            InsertLoggingEntry((int)Definition.ThorLogWindows.ROUTING, "routing ..." + e.ProgressPercentage.ToString() + " sec");
        }

        private  void InsertLoggingEntry(int defLogWindow, string logEntry, int AlarmType = (int)Definition.Message.D_MESSAGE)
        {
            DateTime now = DateTime.Now;
            string LogString = now.ToShortDateString() + " " + now.ToLongTimeString() + "." + String.Format("{0,3:000}", now.Millisecond.ToString()) + ": " + logEntry;
            switch(defLogWindow){
                case (int)Definition.ThorLogWindows.ROUTING:
                       mySave.InsertRow(AlarmType, logEntry); 
                       c1TextBox_Routing_Log.AppendText(LogString + "\r\n");                  
                 break;
                case (int)Definition.ThorLogWindows.WARNING:
                       mySave.InsertRow(AlarmType, logEntry); 
                       c1TextBox_WARNING_Log.AppendText(LogString + "\r\n");     
                 break;
                case (int)Definition.ThorLogWindows.ERROR:
                      mySave.InsertRow(AlarmType, logEntry); 
                      c1TextBox_ERROR_Log.AppendText(LogString + "\r\n");
                 break;
                case (int)Definition.ThorLogWindows.COMMUNICATION:
                      mySave.InsertRow(AlarmType, logEntry); 
                      c1TextBox_Communication_Log.AppendText(LogString + "\r\n");
                      //
                 break;
            }
       
        }
      

       
      

        private void menuItem_Routing_Click(object sender, EventArgs e)
        {
            Routing_Form routingForm = new Routing_Form();
            routingForm.ShowDialog();
            routingForm.Close();
        }

        private void _toolbar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            if (e.Button == _tbExit)
            {
                Close();
            }
           
            else if (e.Button == ReloadRoutingTable)
            {
                ReloadRoutingTableData();
               
            }
            else if (e.Button == toolBarReloadLabmanagerConnect)
            {
                ReloadLabmanagerConnect();
               
            }
            
        }


        private void ReloadLabmanagerConnect()
        {
            myHC.ReloadLabmanagerConnect();
        }
       
        private void menuItem4_Click(object sender, EventArgs e)
        {
            InterfaceTest_Form interfaceForm = new InterfaceTest_Form();
            interfaceForm.ShowDialog();
            interfaceForm.Close();
        }

        private void menuItem_Machines_Click(object sender, EventArgs e)
        {
            Machine_Configuration_Form machineForm = new Machine_Configuration_Form();
            machineForm.ShowDialog();
            machineForm.Close();
        }

       

        private void miExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void menuItem_Scanner_Click(object sender, EventArgs e)
        {
            ScannerForm scannerForm = new ScannerForm();
            scannerForm.ShowDialog();
            scannerForm.Close();
        }

        private void menuItem1_Click_1(object sender, EventArgs e)
        {
            RobotPosition_Form EpsonRobot = new RobotPosition_Form(this);

            EpsonRobot.ShowDialog();
            EpsonRobot.Close();
        }

       

        private void menuItem2_Click(object sender, EventArgs e)
        {
            TimeForAlarmForm alarmForm = new TimeForAlarmForm();
            alarmForm.ShowDialog();
            alarmForm.Close();
        }


        private void WriteStatusbarLeft(string strText, bool bError=false)
        {
            c1StatusBar_LabManager.LeftPaneItems.Clear();
            c1StatusBar_LabManager.LeftPaneItems.Add(strText);
            if (bError)
            {
                c1StatusBar_LabManager.ForeColorOuter = Color.Red;
            }
            else
            {
                c1StatusBar_LabManager.ForeColorOuter = Color.Black;
            }

        }

        private void menuItem_TGASampleList_Click(object sender, EventArgs e)
        {
            SampleListTGA TGAListForm = new SampleListTGA();
            TGAListForm.ShowDialog();
            TGAListForm.Close();
        }

        private void menuItem_AdminRobot_Click(object sender, EventArgs e)
        {
            OpenRobotRemoteForm();
        }

        private void ThreadShowDialog()
        {

            EpsonRobotAdminDialogThread = new Thread(new ThreadStart(EpsonRobotAdminDialogLoad));
            EpsonRobotAdminDialogThread.Name = "EpsonRobotAdminDialogThread";
            EpsonRobotAdminDialogThread.Start();

        }
        private void EpsonRobotAdminDialogLoad()
        {
            OpenRobotRemoteForm();

            EpsonRobotAdminDialogThread.Abort();
        }

        private void OpenRobotRemoteForm()
        {
            
            RobotAdmin_Form EpsonRobotAdmin = new RobotAdmin_Form(this);
            Process[] processes = Process.GetProcessesByName("PDLRT");
            if (processes.Length > 0)
            {
                IntPtr hwnd = processes[0].MainWindowHandle;
                
                EpsonRobotAdmin.ShowDialog(new WindowWrapper(hwnd));
            }
            else
            {
                 EpsonRobotAdmin.ShowDialog();
            }
            EpsonRobotAdmin.Close();
          
        }

       // public function to check from other files if LabManagerConnect is online
        public bool LabManagerConnectIsOnline()
        {
            return LabManagerConnectReady;
        }


        private void menuItem_Conf_Click(object sender, EventArgs e)
        {
            MenuItem mi = sender as MenuItem;

            Configuration_Form ConfForm = new Configuration_Form(mi.Text);

            ConfForm.ShowDialog();
            ConfForm.Close();
        }

        private void menuItem5_Click(object sender, EventArgs e)
        {
            Administration_Form AdminForm = new Administration_Form();

            AdminForm.ShowDialog();
            AdminForm.Close();
        }

        private void LabManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Do you really want to exit Labmanager?", "Question", System.Windows.Forms.MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                e.Cancel = true;
            }
            else
            {
                _pipeServer.PipeMessage -= new DelegateMessage(PipesMessageHandler);
                _pipeServer = null;
            }
        }

     
      
    }

 

    public class RemoteObject : System.MarshalByRefObject
    {

        private static PipeClient _pipeClient;
   
        public RemoteObject(int nForm, int nInternalForm, int nValue1, string strSampleID)
        {
           
            Process[] processes = Process.GetProcessesByName("PDLRT");
            foreach (Process p in processes)
            {
                IntPtr hwnd = p.MainWindowHandle;
                switch (nInternalForm)
                {
                    case 1:
                         InterfaceTest_Form interfaceTestForm = new InterfaceTest_Form();
                         interfaceTestForm.ShowDialog(new WindowWrapper(hwnd));
                         interfaceTestForm.Close();
                     break;

                    case 2:

                         Routing_Form routingForm = new Routing_Form(nValue1);
                         routingForm.ShowDialog(new WindowWrapper(hwnd));
                         routingForm.Close();
                    break;

                    case 3:      
                         TimeForAlarmForm alarmForm = new TimeForAlarmForm();
                         alarmForm.ShowDialog(new WindowWrapper(hwnd));
                         alarmForm.Close();

                    break;

                    case 4:   
                        SampleListTGA TGAListForm = new SampleListTGA();
                        TGAListForm.ShowDialog(new WindowWrapper(hwnd));
                        TGAListForm.Close();
                    break;

                    case 5:
                          _pipeClient = new PipeClient();
                          _pipeClient.Send("RobotRemoteForm", "LMPipe", 1000);
                          _pipeClient = null;
                    break;


                }
            }
        }
    /*    public void Send(string SendStr, string PipeName, int TimeOut = 1000)
        {
            try
            {
                NamedPipeClientStream pipeStream =
                   new NamedPipeClientStream(".", PipeName, PipeDirection.Out, PipeOptions.Asynchronous);

                // The connect function will indefinitely wait for the pipe to become available
                // If that is not acceptable specify a maximum waiting time (in ms)
                pipeStream.Connect(TimeOut);
                Console.WriteLine("[Client] Pipe connection established");

                byte[] _buffer = Encoding.UTF8.GetBytes(SendStr);
                pipeStream.BeginWrite(_buffer, 0, _buffer.Length, new AsyncCallback(AsyncSend), pipeStream);
            }
            catch (TimeoutException oEX)
            {
                Console.WriteLine(oEX.Message);
            }
        }

        private void AsyncSend(IAsyncResult iar)
        {
            try
            {
                // Get the pipe
                NamedPipeClientStream pipeStream = (NamedPipeClientStream)iar.AsyncState;

                // End the write
                pipeStream.EndWrite(iar);
                pipeStream.Flush();
                pipeStream.Close();
                pipeStream.Dispose();
            }
            catch (Exception oEX)
            {
                Console.WriteLine(oEX.Message);
            }
        }*/
    }

    public class WindowWrapper : System.Windows.Forms.IWin32Window
    {
        public WindowWrapper(IntPtr handle)
        {
            _hwnd = handle;
        }

        public IntPtr Handle
        {
            get { return _hwnd; }
        }

        private IntPtr _hwnd;
    }

   
   

}

