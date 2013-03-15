using System;
using MySQL_Helper_Class;
using Definition;
using System.Data;
using cs_IniHandlerDevelop;
using Magazine_Helper;
using System.Windows.Forms;
using System.Timers;



namespace Magazine
{
    class Magazine_Driver
    {
        LabManager.LabManager _parent;
        private MySQL_HelperClass myHC = new MySQL_HelperClass();
      //  private MySQL_HelperClass myHC_Command = new MySQL_HelperClass();
     //   private MySQL_HelperClass myHC_Move = new MySQL_HelperClass();

        DataSet ds_SortedSamples = null;
        DataTable dt_SortedSamples = null;

        private Definitions myDef = new Definitions();
       // Save mySave = new Save("Magazine_Driver");
        private IniStructure myIniHandler = new IniStructure();
    //    private MagazineDimension magazineDim = new MagazineDimension();
        private MagazineHelper myMagazineHelper = null;
        private MagazineHelper myMagazineHelperCommand = null;

        private System.Timers.Timer MagazineDriverTimer = new System.Timers.Timer();

        private string loggingString_COMPARE = null;
        int _nMagazine_ID = -1;
        bool _bMagazineIsFull = true;
        bool _bReady = false;
        bool _bOnStopMode = false;
        bool _bReserve = false;
        int nStateValue = 1;

        public Magazine_Driver(LabManager.LabManager parent, int nMagazine_ID)
        {
             _nMagazine_ID = nMagazine_ID;
             _parent = parent;

           

             myMagazineHelper = new MagazineHelper(_nMagazine_ID);

             string IniFilePath = myDef.LanguageFile;
             myIniHandler = IniStructure.ReadIni(IniFilePath);

          //  _parent.InsertLoggingEntry((int)Definition.ThorLogWindows.ROUTING, "starting magazine '" + myMagazineHelper.GetMagazineName() + "' ");
             string strStarting = String.Format(myIniHandler.GetValue("Magazine", "StartMagazineInit"), myMagazineHelper.GetMagazineName());

             WriteLoggEntry((int)Definition.ThorLogWindows.ROUTING, strStarting, (int)Definition.Message.D_MESSAGE);

             UpdateMagazine(); 

             // Hook up the Elapsed event for the timer.
             MagazineDriverTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);

             // Set the Interval to 1 seconds (1000 milliseconds).
             MagazineDriverTimer.Interval = 1000;
             MagazineDriverTimer.Enabled = true;


             if (myMagazineHelper.GetOutputPosition() == -1 || myMagazineHelper.GetMachine_ID() == -1)
             {
                 string strCorruptConf = String.Format(myIniHandler.GetValue("Magazine", "CorruptConfiguration"), myMagazineHelper.GetOutputPosition(), myMagazineHelper.GetMachine_ID());
                 WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, strCorruptConf, (int)Definition.Message.D_ALARM);
             }

             // Keep the timer alive until the end of Main.
              //GC.KeepAlive(MagazineDriverTimer);
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            
            UpdateMagazine(); 
        }
      

        public int SendCommand(int nCommandNumber, int nSample_ID, int nPos, int nMachine_ID)
        {
            int nRet = -1;
            myMagazineHelperCommand = new MagazineHelper(_nMagazine_ID);

            switch (nCommandNumber)
            {
                case 1:
                    {// insert
                         try
                        {
                            InsertSample(nSample_ID, myMagazineHelperCommand.GetForceFIFO(), nPos, nMachine_ID, false);
                            string strNoPos = String.Format(myIniHandler.GetValue("Magazine", "InsertSample"), myHC.GetNameFromID((int)Definition.SQLTables.SAMPLE_ACTIVE, nSample_ID), nSample_ID, myMagazineHelperCommand.GetMagazineName());
                            WriteLoggEntry((int)Definition.ThorLogWindows.ROUTING, strNoPos, (int)Definition.Message.D_DEBUG);                 
                        }
                        catch (Exception ex) { WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, ex.ToString(), (int)Definition.Message.D_ALARM); }                 
                        break;
                    }

                case 2:
                    { // delete
                        try{
                             DeleteSample(nSample_ID);
                        }
                        catch (Exception ex) { WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, ex.ToString(), (int)Definition.Message.D_ALARM); }

                            string strNoPos = String.Format(myIniHandler.GetValue("Magazine", "DeletedSample"), nSample_ID);
                            WriteLoggEntry((int)Definition.ThorLogWindows.ROUTING, strNoPos, (int)Definition.Message.D_DEBUG);
                       
                        break;
                    }

                case 3:
                    {
                        // force sortorder ON
                        try
                        {
                            ForceSortOrderForSample(nSample_ID, 1);
                        }
                        catch (Exception ex) { WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, ex.ToString(), (int)Definition.Message.D_ALARM); }

                        string strForceSortOrder = String.Format(myIniHandler.GetValue("Magazine", "ForceSortOrderSample"), nSample_ID, "ON");
                        WriteLoggEntry((int)Definition.ThorLogWindows.ROUTING, strForceSortOrder, (int)Definition.Message.D_DEBUG);
                        break;
                    }
                case 4:
                    {
                        // force sortorder OFF
                        try
                        {
                            ForceSortOrderForSample(nSample_ID, 0);
                        }
                        catch (Exception ex) { WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, ex.ToString(), (int)Definition.Message.D_ALARM); }

                        string strForceSortOrder = String.Format(myIniHandler.GetValue("Magazine", "ForceSortOrderSample"), nSample_ID, "OFF");
                        WriteLoggEntry((int)Definition.ThorLogWindows.ROUTING, strForceSortOrder, (int)Definition.Message.D_DEBUG);
                        break;
                    }
                case 5:
                    {// putsample back
                        try
                        {
                                // the "put back flag" is set so "MagazineDoneFlag" in table "sample_active" is set
                            InsertSample(nSample_ID, myMagazineHelperCommand.GetForceFIFO(), nPos, nMachine_ID, true);
                            string strNoPos = String.Format(myIniHandler.GetValue("Magazine", "SampleReturn"), myHC.GetNameFromID((int)Definition.SQLTables.SAMPLE_ACTIVE, nSample_ID), nSample_ID, myMagazineHelperCommand.GetMagazineName());
                            WriteLoggEntry((int)Definition.ThorLogWindows.ROUTING, strNoPos, (int)Definition.Message.D_DEBUG);
                        }
                        catch (Exception ex) { WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, ex.ToString(), (int)Definition.Message.D_ALARM); }
                        break;
                    }
                 case 6:
                    {// reactivate sample (run duplicate sample)
                        try
                        {
                            // the "MagazineDoneFlag" will be mset to 0 -  in table "sample_active" 
                            // the sample ID will be set to "D#" + "OLDSAMPLEID" to indicate that this one is a second try to get sample results
                            ReactivateSample(nSample_ID);
                            string strNoPos = String.Format(myIniHandler.GetValue("Magazine", "SampleDuplicate"), myHC.GetNameFromID((int)Definition.SQLTables.SAMPLE_ACTIVE, nSample_ID), nSample_ID, myMagazineHelperCommand.GetMagazineName());
                            WriteLoggEntry((int)Definition.ThorLogWindows.ROUTING, strNoPos, (int)Definition.Message.D_DEBUG);
                        }
                        catch (Exception ex) { WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, ex.ToString(), (int)Definition.Message.D_ALARM); }
                        break;
                    }
                   
                default:
                    string strUnknownCommand = String.Format(myIniHandler.GetValue("Magazine", "UnknownCommand"), nCommandNumber.ToString());
                    WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, strUnknownCommand , (int)Definition.Message.D_ALARM);
                    break;
            }

           
            return nRet;
        }

        private void WriteLoggEntry(int LogType, string strLogString, int AlarmType = (int)Definition.Message.D_MESSAGE)
        {
            
                if (!String.Equals(strLogString, loggingString_COMPARE, StringComparison.Ordinal))
                {
                  //  MethodInvoker Logging = delegate
                  //  {
                        // doing Logging entries
                   //     if (_parent != null)
                     //   {
                            _parent.WriteTCPIPLoggEntry(LogType, strLogString, AlarmType);
                       // }
                  //  };
                  //  try
                  //  {
                      //  _parent.Invoke(Logging);
                    //    Invoke(Logging);
                    //     loggingString_COMPARE = strLogString;
                   // }
                   // catch {  }
                }
            
        }

        
        // This method will update the magazine driver - called from LabManager_Form.cs on Timer process 
        private void UpdateMagazine()
        {
          
      

            if (myMagazineHelper.GetOutputPosition() == -1 || myMagazineHelper.GetMachine_ID() == -1)
            {
                  return;
            }

            
                if (CheckIFMagazineIsFull())
                {
                    _bMagazineIsFull = true;
                }
                else
                {
                    _bMagazineIsFull = false;
                }

                  if (CheckIFMagazineIsReadyToMoveNextSample())
                  {
               
                      if (!myMagazineHelper.GetStopMode())
                      {
                          MoveSampleToOutputPos();
                      }
                  }
               

                if (myMagazineHelper.GetStopMode())
                {
                    _bOnStopMode = true;         
                }
                else
                {
                    _bOnStopMode = false;
                }
            

                if (_bMagazineIsFull || myMagazineHelper.GetStopMode())
                {
                    _bReady = false;
                }
                else
                {
                    _bReady = true;
                }
                      
            SetStatusbits();
        }

        private void MoveSampleToOutputPos()
        {
           
            ds_SortedSamples = myMagazineHelper.GetDatasetForSortedSamples();
            if (ds_SortedSamples != null)
            {
                if (ds_SortedSamples.Tables.Count > 0)
                {
                    dt_SortedSamples = ds_SortedSamples.Tables[0];
                    if (dt_SortedSamples.Rows.Count > 0)
                    {
                        DataRow dr_SortedTopSample = dt_SortedSamples.Rows[0];
                        if (dr_SortedTopSample["idactive_samples"].ToString().Length > 0)
                        {
                            string SQL_ProcedureCall = "CALL MoveSampleFromMagazineToActiveSamples(" + dr_SortedTopSample["idactive_samples"].ToString() + "," + myMagazineHelper.GetOutputPosition() + ")";
                            int nMachine_ID = myHC.GetMachineIDFromMachinePositions_ID(myMagazineHelper.GetOutputPosition());
                            string strPositionName = myHC.GetNameFromID((int)Definition.SQLTables.MACHINE_POSITIONS, myMagazineHelper.GetOutputPosition());
                            string strLog = "MagazineDriver(" + myMagazineHelper.GetMagazineName() + ")::MoveSampleToOutputPos: moving sample \"" + dr_SortedTopSample["SampleID"].ToString() + "\" with id:" + dr_SortedTopSample["idactive_samples"].ToString() + " to position \"" + strPositionName + "\"" + " (" + myHC.GetNameFromID((int)Definition.SQLTables.MACHINES, nMachine_ID) + ") with id:" + myMagazineHelper.GetOutputPosition();
                            WriteLoggEntry((int)Definition.ThorLogWindows.ROUTING, strLog, (int)Definition.Message.D_DEBUG);

                            myHC.return_SQL_Statement(SQL_ProcedureCall);
                        }
                        else { WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, "no sample_ID found in function - MoveSampleToOutputPos-", (int)Definition.Message.D_ALARM); }
                    }
                }
                else { WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, "no table found - MoveSampleToOutputPos-", (int)Definition.Message.D_ALARM); }
                  
            }
        }

        private bool CheckIFMagazineIsFull()
        {
            // call MySQL-Routine
            return myHC.return_SQL_StatementAsBool("SELECT CheckIfAllMagazinePositionsAreOccupied(" + _nMagazine_ID + ")");
           
        }

        private bool CheckIFMagazineIsReadyToMoveNextSample()
        {
            // call MySQL-Routine
            return myHC.return_SQL_StatementAsBool("SELECT CheckIfMagazinePositionIsReadyToOutput(" + myMagazineHelper.GetOutputPosition() + ")");

        }


        private void SetStatusbits()
        {

          //  string SQL_Statement = "CALL SetStatusBitsForMagazine(" + myMagazineHelper.GetMachine_ID() + "," + _bReady + "," + _bOnStopMode + "," + _bMagazineIsFull + "," + _bReserve + ")";  
         //   myHC.return_SQL_Statement(SQL_Statement, "");
          //  WriteLoggEntry((int)Definition.ThorLogWindows.ROUTING, SQL_Statement, (int)Definition.Message.D_DEBUG);
            int nValue = 1;
             if (_bReady && !_bOnStopMode && !_bMagazineIsFull) { nValue = 2; }
             if (_bOnStopMode && !_bMagazineIsFull) { nValue = 512; }
             if (_bMagazineIsFull) { nValue = 4096; }

            // call only, if state has changed
             if (nStateValue != nValue)
             {
                 nStateValue = nValue;
                 string SQL_Statement = "CALL SetStatusBitsForMagazine(" + myMagazineHelper.GetMachine_ID() + "," + _bReady + "," + _bOnStopMode + "," + _bMagazineIsFull + "," + _bReserve + ")";
                 myHC.return_SQL_Statement(SQL_Statement);

               //  SQL_Statement = "CALL SetMachineStateStatistic(" + myMagazineHelper.GetMachine_ID() + ")";
               //  myHC.return_SQL_Statement(SQL_Statement, "");

              //   string strName = myMagazineHelper.GetMachine_ID() + "_StatusInternal";
               // 
             //    string strValue = nValue.ToString();
              //   myHC.WriteWinCCTagFromSampleValuesByValueName(strName, strValue, 7);
             }
        }


        private void InsertSample(int nSample_ID, bool bForcFIFO, int nPos, int nMachine_ID,bool bPutBack=false)
        {
            string SQL_Statement = "CALL InsertSampleInMagazine(" + _nMagazine_ID + "," + nSample_ID + "," + bForcFIFO + "," + nPos + "," + nMachine_ID + "," + bPutBack + ")";
            myHC.return_SQL_Statement(SQL_Statement);
        }

        private void DeleteSample(int nSample_ID)
        {
            string SQL_Statement = "CALL DeleteSampleOutOfMagazine(" + nSample_ID + ")";
            int nID = myHC.return_SQL_Statement(SQL_Statement); 
        }

        private void ForceSortOrderForSample(int nSample_ID, int nValue)
        {
            string SQL_Statement = "CALL ForceSortOrderSampleOutOfMagazine(" + nSample_ID + ", " + nValue  + ")";
            int nID = myHC.return_SQL_Statement(SQL_Statement);
        }

        private void ReactivateSample(int nSample_ID)
        {
            try
            {
                myHC.ReactivateSampleInMagazine(nSample_ID);
            }
            catch { }
        }
    }
}
