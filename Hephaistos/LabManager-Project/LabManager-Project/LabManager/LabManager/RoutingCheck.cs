// This class checks the condition table and check every condition and if the condition is true
// the "condition_comply" tag will be set.

 

using System;
using System.Collections;
using System.Data;
using Logging;
using Definition;
using MySQL_Helper_Class;
using System.Windows.Forms;

namespace Routing
{
    class RoutingCheck
    {
        LabManager.LabManager _parent;
        private System.Windows.Forms.Timer RoutingCeckTimer = new System.Windows.Forms.Timer();   
        private string loggingString_COMPARE = null;
        private Definitions myThorDef = new Definitions();
        private MySQL_HelperClass myHC = new MySQL_HelperClass();
        private MySQL_HelperClass myHC_Timer = new MySQL_HelperClass();
        private Save mySave = new Save("LabManager-RoutingCheck");
        private RoutingData myRoutingData = new RoutingData();
        private Hashtable ConditionComplyHashTable = new Hashtable();
        private DataSet ds_Conditions = null;
        private DataTable dt_Conditions = null;
        private int[][] nCountTimeConditionArray;
        private bool firstCheckDone = false;
        private bool checkData = false;
      //  private string strActualValue = null;

        public RoutingCheck(LabManager.LabManager parent)
        {
            _parent = parent;

            // reset all conditions on start and check the routing lines again if connection to WinCC is established
            string SQL_StatementResetConditions = "UPDATE routing_conditions Set Condition_comply=0";
            myHC.return_SQL_Statement(SQL_StatementResetConditions);

           

            /* Adds the event and the event handler for the method that will 
           process the timer event to the timer. */
            RoutingCeckTimer.Tick += new EventHandler(TimerEventProcessor);

            // Sets the timer interval to 1 second.
            RoutingCeckTimer.Interval = 1000;
            RoutingCeckTimer.Start();


            LoadRoutingTable();
          
        }

        public int LoadRoutingTable()
        {
            checkData = false;
            int ret = -1;
            string SQL_StatementTimeConditions = @"SELECT idrouting_conditions,Value,RoutingPositionEntry_ID,ConditionList_ID 
                            FROM routing_conditions 
                            WHERE  ConditionList_ID=" + (int)Definition.RoutingConditions.PRETIME + " OR ConditionList_ID=" + (int)Definition.RoutingConditions.TIME;
            DataSet dsTimeCon = new DataSet();
            dsTimeCon.Clear();
            dsTimeCon = myHC.GetDataSetFromSQLCommand(SQL_StatementTimeConditions);
            DataTable dtTimeCon = new DataTable();

            if (dsTimeCon != null)
            {
                if (dsTimeCon.Tables.Count > 0)
                {
                    dtTimeCon = dsTimeCon.Tables[0];
                    // get the count of "Time" values in table "routing_conditions"
                    nCountTimeConditionArray = new int[dtTimeCon.Rows.Count][];

                    int n = 0;
                    foreach (DataRow drTimeCon in dtTimeCon.Rows)
                    {
                        try
                        {
                            nCountTimeConditionArray[n] = new int[6] { 0, 0, 0, 0, 0, 0 };
                            nCountTimeConditionArray[n][0] = (int)drTimeCon["RoutingPositionEntry_ID"];
                            nCountTimeConditionArray[n][1] = (int)drTimeCon["ConditionList_ID"];
                            nCountTimeConditionArray[n][2] = Int32.Parse(drTimeCon["Value"].ToString());
                            nCountTimeConditionArray[n][3] = 0; // actual Pretime
                            nCountTimeConditionArray[n][4] = 0; // actual Time
                            nCountTimeConditionArray[n][5] = (int)drTimeCon["idrouting_conditions"];
                            n++;
                        }
                        catch { }
                    }

                    // get routingEntries from DB once during system start 
                    String SQL_Statement = "Select * from routing_conditions";
                    ds_Conditions = myHC.GetDataSetFromSQLCommand(SQL_Statement);
                    dt_Conditions = ds_Conditions.Tables[0];
                    _parent.WriteTCPIPLoggEntry((int)Definition.ThorLogWindows.ROUTING, "loading " + dt_Conditions.Rows.Count.ToString() + " entries into condition table");

                    if (dt_Conditions.Rows.Count > 0)
                    {
                        ConditionComplyHashTable.Clear();

                        foreach (DataRow drCOnditionComply in dt_Conditions.Rows)
                        {
                            ConditionComplyHashTable.Add(drCOnditionComply["idrouting_conditions"], drCOnditionComply["condition_comply"]);
                            //   _parent.InsertLoggingEntry((int)Definition.ThorLogWindows.ROUTING, " drCOnditionComply '" + drCOnditionComply["idrouting_conditions"].ToString() + "' drCOnditionComply" + drCOnditionComply["condition_comply"] + "\r\n");

                        }
                        ret = dt_Conditions.Rows.Count;
                    }
                    checkData = true;
                }
            }
            
            return ret;
        }

        private void WriteLoggEntry(int LogType, string strLogString, int AlarmType = (int)Definition.Message.D_MESSAGE)
        {
            
                if (!String.Equals(strLogString, loggingString_COMPARE, StringComparison.Ordinal))
                {
                   // MethodInvoker Logging = delegate
                  //  {
                        // doing Logging entries
                  //      if (_parent != null)
                    //    {
                            _parent.WriteTCPIPLoggEntry(LogType, strLogString, AlarmType);
                    //    }
                  //  };
                  //  try
                 //   {
                   //     _parent.Invoke(Logging);
                  //      loggingString_COMPARE = strLogString;
                 //   }
                 //   catch { }
                }
            
        }

        private void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            // abgeschaltet, da es Probleme gab 5.10.2012
            //return;


            //RoutingCeckTimer.Stop();
            if (checkData) // don't check on reload routing table
            {
                //&& !_parent.LockRoutingCheck
                if (firstCheckDone )
                {
                    for (int n = 0; n < nCountTimeConditionArray.Length; n++)
                    {
                        //nCountTimeConditionArray[n][0] = "RoutingPositionEntry_ID"
                        //nCountTimeConditionArray[n][1] = "ConditionList_ID"
                        //nCountTimeConditionArray[n][2] = Time value from Condition Table
                        //nCountTimeConditionArray[n][3] = actual PreTime counter
                        //nCountTimeConditionArray[n][4] = actual Time counter
                        //nCountTimeConditionArray[n][5] = "idrouting_conditions"
                        int nConditionList_ID = nCountTimeConditionArray[n][1];
                        int nTimeInConditionTable = nCountTimeConditionArray[n][2];
                        int nActualTime = nCountTimeConditionArray[n][3];

                        int nSampleID_ID = GetSampleID_IDFromsampleActiveByRoutingPosition_ID(nCountTimeConditionArray[n][0]);

                        // PRETIME
                        if (nConditionList_ID == (int)Definition.RoutingConditions.PRETIME && nSampleID_ID > 0)
                        {
                            if (nTimeInConditionTable > nCountTimeConditionArray[n][3])
                            {
                                nCountTimeConditionArray[n][3] = nCountTimeConditionArray[n][3] + 1;
                            }

                            if (nCountTimeConditionArray[n][3] > 0 && nCountTimeConditionArray[n][3] < nCountTimeConditionArray[n][2])
                            {
                                // WriteLoggEntry((int)Definition.ThorLogWindows.ROUTING, "PRETIME: " + nCountTimeConditionArray[n][3].ToString() + " RoutingPositionEntry_ID: " + nCountTimeConditionArray[n][0] + " nSampleID_ID:" + nSampleID_ID, (int)Definition.Message.D_MESSAGE);
                            }
                        }
                        else
                        {
                            nCountTimeConditionArray[n][3] = 0;
                        }


                        // TIME
                        if (nConditionList_ID == (int)Definition.RoutingConditions.TIME)
                        {
                            //lock (myHC)
                            {
                                // try to get the SampleID_ID to check if sample is on this Pos
                                //
                                if (CheckIfAllOtherConditionsAreTrue(nCountTimeConditionArray[n][0], nCountTimeConditionArray[n][5]) && nSampleID_ID > 0)
                                {
                                    if (nTimeInConditionTable > nCountTimeConditionArray[n][4])
                                    {
                                        nCountTimeConditionArray[n][4] = nCountTimeConditionArray[n][4] + 1;
                                    }
                                }
                                else
                                {
                                    nCountTimeConditionArray[n][4] = 0;
                                }

                                if (nCountTimeConditionArray[n][4] > 0 && nCountTimeConditionArray[n][4] <= nCountTimeConditionArray[n][2])
                                {
                                    // for debugging only
                                    //   WriteLoggEntry((int)Definition.ThorLogWindows.ROUTING, "TIME: " + nCountTimeConditionArray[n][4].ToString() + " RoutingPositionEntry_ID: " + nCountTimeConditionArray[n][0] + " nSampleID_ID:" + nSampleID_ID, (int)Definition.Message.D_MESSAGE);

                                }
                            }
                        }

                    }
                }
            }
        }

        public int GetSampleID_IDFromsampleActiveByRoutingPosition_ID(int nRoutingPositionEntry_ID)
        {
            int nRet = -1;
            int nActualPosition_ID = -1;
            try{
                 nActualPosition_ID = (int)myHC_Timer.return_SQL_Statement(@"SELECT        routing_positions.Machine_Position_ID
                        FROM            routing_position_entries INNER JOIN
                        routing_positions ON routing_position_entries.Position_ID = routing_positions.idrouting_positions
                        WHERE        (routing_position_entries.idrouting_position_entries = " + nRoutingPositionEntry_ID.ToString() + ")");
            }
            catch {  }
            try
            {
                nRet = myHC_Timer.return_SQL_Statement(@"SELECT        idactive_samples
                    FROM            sample_active
                    WHERE        (ActualSamplePosition_ID = " + nActualPosition_ID.ToString() + ")");
            }
            catch  {  }
            return nRet;
        }

        private bool CheckIfAllOtherConditionsAreTrue(int nRoutingPositionEntry_ID, int nRoutingCondition_ID)
        {
            bool ret = false;
            int nConditionsTrue = 0;
            int nCountConditions = -1;
            string strCountConditions = null;
            string strConditionsTrue = null;

            // get the amount of routing conditions for the routing position
            string SQL_Statement = "SELECT Count(idrouting_conditions) FROM routing_conditions WHERE RoutingPositionEntry_ID=" + nRoutingPositionEntry_ID + " AND NOT idrouting_conditions=" + nRoutingCondition_ID;
            strCountConditions = myHC_Timer.return_SQL_StatementAsString(SQL_Statement);

            // get the amount of conditions which are true
            SQL_Statement = "SELECT COUNT(Condition_comply) FROM routing_conditions WHERE Condition_comply=1 AND RoutingPositionEntry_ID=" + nRoutingPositionEntry_ID + " AND NOT idrouting_conditions=" + nRoutingCondition_ID;
            strConditionsTrue = myHC_Timer.return_SQL_StatementAsString(SQL_Statement);
            //     MessageBox.Show(strConditionsTrue + "#" + strCountConditions.ToString());

            Int32.TryParse(strConditionsTrue, out nConditionsTrue);
            Int32.TryParse(strCountConditions, out nCountConditions);
            // if the amount of conditions is equal to the amount of true conditions except the "time" conditions -> return true
            if (nConditionsTrue == nCountConditions && nConditionsTrue > 0) { ret = true; }

            return ret;
        }

        public void CheckRoutingTableForActiveConditions()
        {
            try
            {
                RunRoutingCheck();
                // set firstCheckDone
                if (!firstCheckDone) firstCheckDone = true;

            }
            catch (Exception ex) {
                WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, ex.ToString(), (int)Definition.Message.D_ALARM); 
               
            }
        }

        private void RunRoutingCheck()
        {
            if (checkData) // don't check on reload routing table
            {
                foreach (DataRow dr_Condition in dt_Conditions.Rows)
                {
                    // check every Line if it is true
                    int nRoutingCondition_ID = Int32.Parse(dr_Condition.ItemArray[0].ToString());
                    bool bConditionComply = CheckLine(dr_Condition);

                    // compare 'bConditionComply' with the hashtabel and if its different write it to DB 
                    if ((bool)ConditionComplyHashTable[dr_Condition["idrouting_conditions"]] != bConditionComply)
                    {
                        ConditionComplyHashTable[dr_Condition["idrouting_conditions"]] = bConditionComply;
                        myHC.SetConditionComply(bConditionComply, nRoutingCondition_ID);
                    }
                }
            }
        }

        public void RunRoutingCheckByRoutingEntryposition_ID(int nRoutingPositionEntry_ID)
        {
            if (checkData) // don't check on reload routing table
            {
                foreach (DataRow dr_Condition in dt_Conditions.Rows)
                {
                    if ((int)dr_Condition.ItemArray[1] == nRoutingPositionEntry_ID)
                    {
                        // check every Line if it is true
                        int nRoutingCondition_ID = Int32.Parse(dr_Condition.ItemArray[0].ToString());
                        bool bConditionComply = CheckLine(dr_Condition);

                        // compare 'bConditionComply' with the hashtabel and if its different write it to DB 
                        if ((bool)ConditionComplyHashTable[dr_Condition["idrouting_conditions"]] != bConditionComply)
                        {
                            ConditionComplyHashTable[dr_Condition["idrouting_conditions"]] = bConditionComply;
                            myHC.SetConditionComply(bConditionComply, nRoutingCondition_ID);
                        }
                    }
                }
            }
        }

        private bool CheckLine(DataRow dr_Condition)
        {
            bool ret = false;
            int nConditionList_ID = Int32.Parse(dr_Condition.ItemArray[2].ToString());


            switch (nConditionList_ID)
                {
                 case (int)Definition.RoutingConditions.PRETIME:

                        if (CheckPreTime(dr_Condition)) { ret = true; }
     
                  break;

                 case (int)Definition.RoutingConditions.TIME:

                        if (CheckTime(dr_Condition)) { ret = true; }

                  break;

                 case (int)Definition.RoutingConditions.MACHINESAMPLEFREE:

                        if (CheckMachineSampleFree(dr_Condition)) { ret = true; }
                      
                  break;

                 case (int)Definition.RoutingConditions.GLOBALTAG:

                        if (CheckGlobalTag(dr_Condition)) { ret = true; }

                  break;

                 case (int)Definition.RoutingConditions.MACHINETAG:

                        if (CheckMachineTag(dr_Condition)) { ret = true; }

                  break;

                 case (int)Definition.RoutingConditions.WORKSHEETENTRY:

                        if (CheckWorkSheetEntry(dr_Condition)) { ret = true; }

                  break;

                 case (int)Definition.RoutingConditions.SAMPLEONPOS:

                        if(CheckSampleOnPosition(dr_Condition)) { ret = true; }

                  break;
                
                 case (int)Definition.RoutingConditions.SAMPLETYPE:

                       if (CheckSampleType(dr_Condition)) { ret = true; }

                  break;

                 case (int)Definition.RoutingConditions.SAMPLEPRIORITY:

                      if (CheckSamplePriority(dr_Condition)) { ret = true; }

                  break;

                 case (int)Definition.RoutingConditions.STATUSBITS:

                      if (CheckStatusBits(dr_Condition)) { ret = true; }

                  break;

                 case (int)Definition.RoutingConditions.CHECKOWNMAGPOS:

                  if (CheckOwnMagPos(dr_Condition)) { ret = true; }

                  break;


                default:
                  ret = false;
                  WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, "RoutingCheck::CheckLine: wrong routing condition found: " + nConditionList_ID.ToString(), (int)Definition.Message.D_ALARM);
                  break;

                }
            return ret;
        }

        public bool ResetTimeValuesByExecutingACommand(int nRoutingPositionEntry_ID)
        {
            //nCountTimeConditionArray[n][0] = "RoutingPositionEntry_ID"
            //nCountTimeConditionArray[n][1] = "ConditionList_ID"
            //nCountTimeConditionArray[n][2] = Time value from Condition Table
            //nCountTimeConditionArray[n][3] = actual PreTime counter
            //nCountTimeConditionArray[n][4] = actual Time counter
            //nCountTimeConditionArray[n][5] = "idrouting_conditions"
            bool ret = false;
          
            for (int n = 0; n < nCountTimeConditionArray.Length; n++)
            {
                if (nRoutingPositionEntry_ID == nCountTimeConditionArray[n][0])
                {
                    nCountTimeConditionArray[n][3] = 0;
                    nCountTimeConditionArray[n][4] = 0;
                    ret = true;
                }
            }

            return ret;
        }

        private bool CheckPreTime(DataRow dr_Condition)
        {
            //nCountTimeConditionArray[n][0] = "RoutingPositionEntry_ID"
            //nCountTimeConditionArray[n][1] = "ConditionList_ID"
            //nCountTimeConditionArray[n][2] = Time value from Condition Table
            //nCountTimeConditionArray[n][3] = actual PreTime counter
            //nCountTimeConditionArray[n][4] = actual Time counter
            //nCountTimeConditionArray[n][5] = "idrouting_conditions"
           bool ret = false;
           int nRoutingCondition_ID = 0;
           Int32.TryParse(dr_Condition["idrouting_conditions"].ToString(), out nRoutingCondition_ID);

            for (int n = 0; n < nCountTimeConditionArray.Length; n++)
            {
                if (nRoutingCondition_ID == nCountTimeConditionArray[n][5])
                {
                    myHC.SetConditionActualValue(nRoutingCondition_ID, nCountTimeConditionArray[n][3].ToString());
                    // if "Time value from Condition Table" = "actual Time counter" 
                    if (nCountTimeConditionArray[n][2] == nCountTimeConditionArray[n][3]) { ret = true; }
                }
            }

            return ret;
        }

        private bool CheckTime(DataRow dr_Condition)
        {
            //nCountTimeConditionArray[n][0] = "RoutingPositionEntry_ID"
            //nCountTimeConditionArray[n][1] = "ConditionList_ID"
            //nCountTimeConditionArray[n][2] = Time value from Condition Table
            //nCountTimeConditionArray[n][3] = actual PreTime counter
            //nCountTimeConditionArray[n][4] = actual Time counter
            //nCountTimeConditionArray[n][5] = "idrouting_conditions"
            bool ret = false;

            int nRoutingCondition_ID = 0;
            Int32.TryParse(dr_Condition["idrouting_conditions"].ToString(), out nRoutingCondition_ID);

            for (int n = 0; n < nCountTimeConditionArray.Length; n++)
            {
                if (nRoutingCondition_ID == nCountTimeConditionArray[n][5])
                {
                    myHC.SetConditionActualValue(nRoutingCondition_ID, nCountTimeConditionArray[n][4].ToString());
                    // if "Time value from Condition Table" = "actual Time counter"
                    if (nCountTimeConditionArray[n][2] == nCountTimeConditionArray[n][4]) { ret = true; }
                }
            }

            return ret;
        }

        //check status if ConditionList_ID =='status'
        private bool CheckMachineSampleFree(DataRow dr_Condition)
        {
            bool ret = false;
               int nOperation_ID = -1;
               int nMachine_ID = -1;
               bool bMachineSampleFree = false;
               try
               {
                   Int32.TryParse(dr_Condition["Operation_ID"].ToString(), out nOperation_ID);
                   Int32.TryParse(dr_Condition["ValueName"].ToString(),out nMachine_ID);             
               }
               catch (Exception ex) { WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, "RoutingCheck::CheckLine::MACHINESAMPLEFREE:  \r\n" + ex.ToString(), (int)Definition.Message.D_ALARM); }

               if (nOperation_ID > 0)
               {
                   int nSample_ID = -1;
                   int RoutingPositionEntry_ID = -1;
                   try
                   {
                       Int32.TryParse(dr_Condition["RoutingPositionEntry_ID"].ToString(), out RoutingPositionEntry_ID);
                       int nPos = myRoutingData.GetPosition_IDFromRoutingPositionsByRoutingPositionEntry_ID(RoutingPositionEntry_ID);
                       nSample_ID = myRoutingData.GetSampleActive_IDFromSampleActiveByMachinePosition_ID(nPos);
                   }
                   catch { }
                    
                   try
                   {
                       bMachineSampleFree = myHC.GetMachineFreeOfSamples(nMachine_ID, nSample_ID);
                   }
                   catch (Exception ex) { WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, "RoutingCheck::CheckLine::MACHINESAMPLEFREE: \r\nnMachine_ID: #" + nMachine_ID + "#\r\n" + ex.ToString(), (int)Definition.Message.D_ALARM); }

                  
                   switch (nOperation_ID)
                   {
                       case (int)Definition.RoutingOperations.EQUALS:     // equals
                           if (bMachineSampleFree == true) { ret = true; }
                           break;
                       case (int)Definition.RoutingOperations.NOTEQUALS:     // not equals
                           if (bMachineSampleFree != true) { ret = true; }
                           break;

                       default:
                           ret = false;
                           WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, "RoutingCheck::CheckLine::MACHINESAMPLEFREE: wrong Operation_ID found for condition 'MACHINESAMPLEFREE' operation_ID: " + nOperation_ID, (int)Definition.Message.D_ALARM);
                           break;
                   }
               }
               
            return ret;
        }

        //check if ConditionList_ID =='global tag'
        private bool CheckGlobalTag(DataRow dr_Condition)
        {
            bool ret = false;
            int nOperation_ID = -1;
            int nType = -1;
            string strGlobalTagValue = null;
            string strValue = null;
            string strGlobalTagName = null;
            double dValue = Double.MinValue;
            bool bValueIsInt = false;
            double dGlobalTagValue = Double.MinValue;

            try
            {
                Int32.TryParse(dr_Condition["Operation_ID"].ToString(), out nOperation_ID);
               // strName = dr_Condition["ValueName"].ToString();
                strGlobalTagName = dr_Condition["ValueName"].ToString();
                
            }
            catch (Exception ex) { WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, "RoutingCheck::CheckLine::GLOBALTAG:  \r\n" + ex.ToString(), (int)Definition.Message.D_ALARM); }

            try
            {
                bValueIsInt = double.TryParse(dr_Condition["Value"].ToString(), out dValue);
            }
            catch (OverflowException ex)
            {
                WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, "RoutingCheck::CheckLine::GLOBALTAG: The number cannot fit in an Int32 \r\n" + ex.ToString(), (int)Definition.Message.D_ALARM); 
            }

            if (!bValueIsInt)
            {
                try
                {
                    strValue = dr_Condition["Value"].ToString();
                }
                catch { }
            }

            if (nOperation_ID > 0)
            {
                try
                {
                    nType = myRoutingData.GetGlobalTagType(strGlobalTagName);
                }
                catch (Exception ex) { WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, "RoutingCheck::CheckLine::GLOBALTAG:  \r\n" + ex.ToString(), (int)Definition.Message.D_ALARM); }

                try
                {
                    strGlobalTagValue = myRoutingData.GetGlobalTagValue(strGlobalTagName);
                  
                }
                catch (Exception ex) { WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, "RoutingCheck::CheckLine::GLOBALTAG:  \r\n" + ex.ToString(), (int)Definition.Message.D_ALARM); }

                if (!bValueIsInt)    // value is an String
                {
                    switch (nOperation_ID)
                    {
                        case (int)Definition.RoutingOperations.EQUALS: // equals
                            if (String.Equals(strValue, strGlobalTagValue, StringComparison.Ordinal)) { return true; }
                            break;
                        case (int)Definition.RoutingOperations.NOTEQUALS: // NOT equals
                            if (!String.Equals(strValue, strGlobalTagValue, StringComparison.Ordinal)) { return true; }
                            break;
                        default:
                            ret = false;
                            WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, "RoutingCheck::CheckLine::GLOBALTAG: illegall Operation_ID for string operation: " + nOperation_ID + " \r\nstrValue:" + strValue + " \r\n", (int)Definition.Message.D_ALARM);
                            return false;
                    }
                }
                else    // value is a number
                {

                   switch(nType)
                   {
                           // variable types that could be a number
                       case 2:
                       case 3:
                       case 4:
                       case 5:
                       case 6:
                       case 7:
                       case 8:
                       case 9:
                       
                            try
                            {
                                dGlobalTagValue = Double.Parse(strGlobalTagValue);
                            }
                            catch {  }
                            switch (nOperation_ID)
                            {
                                case (int)Definition.RoutingOperations.EQUALS: // equals
                                    if (dGlobalTagValue == dValue) { ret = true; }
                                    break;
                                case (int)Definition.RoutingOperations.NOTEQUALS: // NOT equals
                                    if (dGlobalTagValue != dValue) { ret = true; }
                                    break;
                                case (int)Definition.RoutingOperations.GREATER: // greater
                                    if (dGlobalTagValue > dValue) { ret = true; }
                                    break;
                                case (int)Definition.RoutingOperations.SMALLER: // smaller
                                    if (dGlobalTagValue < dValue) { ret = true; }
                                    break;
                                case (int)Definition.RoutingOperations.GREATEREQUALS: // greater, equals
                                    if (dGlobalTagValue >= dValue) { ret = true; }
                                    break;
                                case (int)Definition.RoutingOperations.SMALLEREQUALS: // smaller, equals
                                    if (dGlobalTagValue <= dValue) { ret = true; }
                                    break;
                                default:
                                    ret = false;
                                    break;
                            }
                            break;

                       default:
                            ret = false;
                            break;
                        }
                   
                }
            
            }
            else
            {
                WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, "RoutingCheck::CheckLine::GLOBALTAG: no Operation found for global_tag entry:" + strGlobalTagName + "\r\nstrValue:" + strValue + " \r\n", (int)Definition.Message.D_ALARM);
            }
         
            return ret;
        }

        //check  if ConditionList_ID =='machine tag'
        private bool CheckMachineTag(DataRow dr_Condition)
        {
            bool ret = false;
            int nOperation_ID = -1;
            int nType = -1;
            string strMachineTagValue = null;
            string strMachinetagName = null;
            string strValue = null;
            double dValue = Double.MinValue;
            bool bValueIsInt = false;
            double dMachineTagValue = Double.MinValue;

            Int32.TryParse(dr_Condition["Operation_ID"].ToString(), out nOperation_ID);
            try
            {
               // strName = dr_Condition["ValueName"].ToString();
                strMachinetagName = dr_Condition["ValueName"].ToString();
            }
            catch (Exception ex) { WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, "RoutingCheck::CheckLine::MACHINETAG:  \r\n" + ex.ToString(), (int)Definition.Message.D_ALARM); }

            try
            {
                bValueIsInt = double.TryParse(dr_Condition["Value"].ToString(), out dValue);
            }
            catch (OverflowException ex)
            {
                WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, "RoutingCheck::CheckLine::MACHINETAG: The number cannot fit in an Int32 \r\n" + ex.ToString(), (int)Definition.Message.D_ALARM);
            }

            if (!bValueIsInt)
            {
                try
                {
                    strValue = dr_Condition["Value"].ToString();
                }
                catch { }
            }

            if (nOperation_ID > 0)
            {
                try
                {
                    nType = myRoutingData.GetMachineTagType(strMachinetagName);
               
                }
                catch (Exception ex) { WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, "RoutingCheck::CheckLine::MACHINETAG: cant get nType \r\n" + ex.ToString(), (int)Definition.Message.D_ALARM); }

                try
                {
                    strMachineTagValue = myRoutingData.GetMachineTagValue(strMachinetagName);
                }
                catch (Exception ex) { WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, "RoutingCheck::CheckLine::MACHINETAG:  \r\n" + ex.ToString(), (int)Definition.Message.D_ALARM); }

                if (!bValueIsInt)    // value is an String
                {
                    switch (nOperation_ID)
                    {
                        case (int)Definition.RoutingOperations.EQUALS: // equals
                            if (String.Equals(strValue, strMachineTagValue, StringComparison.Ordinal)) { return true; }
                            break;
                        case (int)Definition.RoutingOperations.NOTEQUALS: // NOT equals
                            if (!String.Equals(strValue, strMachineTagValue, StringComparison.Ordinal)) { return true; }
                            break;
                        default:
                            WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, "RoutingCheck::CheckLine::MACHINETAG: illegall Operation_ID for string operation: " + nOperation_ID + " \r\nstrValue:" + strValue + " \r\n", (int)Definition.Message.D_ALARM);
                            return false;
                    }
                }
                else    // value is an Int
                {
                 
                    switch (nType)
                    {
                        // variable types that could be a number
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                    
                            try
                            {
                                dMachineTagValue = Double.Parse(strMachineTagValue);
                            }
                            catch
                            {
                                WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, "RoutingCheck::CheckLine::MACHINETAG:  dMachineTagValue: " + dMachineTagValue.ToString()  + " \r\n", (int)Definition.Message.D_ALARM);
                            }
                            switch (nOperation_ID)
                            {

                                case (int)Definition.RoutingOperations.EQUALS: // equals
                                    if (dMachineTagValue == dValue) { ret = true; }
                                    break;
                                case (int)Definition.RoutingOperations.NOTEQUALS: // NOT equals
                                    if (dMachineTagValue != dValue) { ret = true; }
                                    break;
                                case (int)Definition.RoutingOperations.GREATER: // greater
                                    if (dMachineTagValue > dValue) { ret = true; }
                                    break;
                                case (int)Definition.RoutingOperations.SMALLER: // smaller
                                    if (dMachineTagValue < dValue) { ret = true; }
                                    break;
                                case (int)Definition.RoutingOperations.GREATEREQUALS: // greater, equals
                                    if (dMachineTagValue >= dValue) { ret = true; }
                                    break;
                                case (int)Definition.RoutingOperations.SMALLEREQUALS: // smaller, equals
                                    if (dMachineTagValue <= dValue) { ret = true; }
                                    break;
                                default:
                                    ret = false;
                                    break;
                            }

                            break;

                        default:
                            ret = false;
                            break;
                    }
                }
            }
                  
            return ret;
        }

        //check if ConditionList_ID =='worksheet entry'
        private bool CheckWorkSheetEntry(DataRow dr_Condition)
        {
            bool ret = false;
            int nOperation_ID = -1;
            int routingPositionEntry_ID = -1;
            int nMachinePosition_ID = -1;
            int nSampleActive_ID = -1;
            bool bValueIsInt = false;
            double dConditionSampleValue = Double.MinValue;
            String strName = null;
            String strConditionSampleValue = null;
            String strSampleValue = null;
            try
            {
                 nOperation_ID = Int32.Parse(dr_Condition["Operation_ID"].ToString());
                 routingPositionEntry_ID = Int32.Parse(dr_Condition["routingPositionEntry_ID"].ToString());
                 strName = dr_Condition["ValueName"].ToString();
            }
            catch { }
            try
            {
              nMachinePosition_ID = myRoutingData.GetMachinePosition_IDFromRoutingCondition(routingPositionEntry_ID);
            }
            catch { }
            
            try{
                nSampleActive_ID = myRoutingData.GetSampleActive_IDIfOnMachinePosition(nMachinePosition_ID);
             }
             catch { }

            // no SampleID found so canceling here
            if (nSampleActive_ID == -1) { return false; }

            // value from sample worksheet
            strSampleValue = myRoutingData.GetSampleValuesFromSampleActice_ID(nSampleActive_ID, strName);

            try
            {
                bValueIsInt = double.TryParse(dr_Condition["Value"].ToString(), out dConditionSampleValue);
            }
            catch (OverflowException ex)
            {
                WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, "RoutingCheck::CheckLine::WorkSheetEntry: The number cannot fit in an Int32 \r\n" + ex.ToString(), (int)Definition.Message.D_ALARM);
            }

            if (!bValueIsInt)
            {
                try
                {   // value from Routing line
                    strConditionSampleValue = dr_Condition["Value"].ToString();
                }
                catch { }
            }
            if (!bValueIsInt)    // value is a String
            {
                switch (nOperation_ID)
                {
                    case (int)Definition.RoutingOperations.EQUALS: // equals
                        if (String.Equals(strSampleValue, strConditionSampleValue, StringComparison.Ordinal)) { ret = true; }
                        break;
                    case (int)Definition.RoutingOperations.NOTEQUALS: // NOT equals
                        if (!String.Equals(strSampleValue, strConditionSampleValue.ToString(), StringComparison.Ordinal)) { ret = true; }
                        break;
                    default:
                        ret = false;
                        break;
                }
            }
            else    // value is a digit or NULL
            {
                if (strSampleValue != null)
                {
                    double dSampleValue = Double.MinValue;
                    try
                    {
                        dSampleValue = Double.Parse(strSampleValue);
                    }
                    catch
                    {
                        WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, "RoutingCheck::CheckLine::WORKSHEETENTRY: \r\nnSampleActive_ID " + nSampleActive_ID + "\r\ndSampleValue: " + dSampleValue + " \r\nstrValue:" + strSampleValue + " \r\nnConditionSampleValue: " + dConditionSampleValue, (int)Definition.Message.D_ALARM);
                    }
                  
                    switch (nOperation_ID)
                    {
                        case (int)Definition.RoutingOperations.EQUALS: // equals
                            if (dSampleValue == dConditionSampleValue) { ret = true; }
                            break;
                        case (int)Definition.RoutingOperations.NOTEQUALS: // NOT equals
                            if (dSampleValue != dConditionSampleValue) { ret = true; }
                            break;
                        case (int)Definition.RoutingOperations.GREATER: // greater
                            if (dSampleValue > dConditionSampleValue) { ret = true; }
                            break;
                        case (int)Definition.RoutingOperations.SMALLER: // smaller
                            if (dSampleValue < dConditionSampleValue) { ret = true; }
                            break;
                        case (int)Definition.RoutingOperations.GREATEREQUALS: // greater, equals
                            if (dSampleValue >= dConditionSampleValue) { ret = true; }
                            break;
                        case (int)Definition.RoutingOperations.SMALLEREQUALS: // smaller, equals
                            if (dSampleValue <= dConditionSampleValue) { ret = true; }
                            break;
                        default:
                            ret = false;
                            break;
                    }
                }
                else
                {
                    // if no value with the <NAME> was found and OperationID is NOTEQUALS -> return true
                    if (nOperation_ID == (int)Definition.RoutingOperations.NOTEQUALS)
                    {
                        ret = true;
                    }
               //     WriteLoggEntry((int)Definition.ThorLogWindows.WARNING, "RoutingCheck::CheckLine::WORKSHEETENTRY: no value found for entry: " + strName + "  in sample:" + myHC.GetNameFromID((int)Definition.SQLTables.SAMPLE_ACTIVE, nSampleActive_ID) + "(id:" + nSampleActive_ID +")", (int)Definition.Message.D_MESSAGE);
                }
            }
     
            return ret;
        }

        //check sample on position if ConditionList_ID =='sample on pos'
        private bool CheckSampleOnPosition(DataRow dr_Condition)
        {
            bool ret = false;
            bool bGotMachinePos_ID = false;
            String strPosOccupied = null;
            int nMachinePos_ID = -1;
            int nSampleActive_ID = -1;
            int nSampleReservation = -1;
          
            try
            {
                strPosOccupied = dr_Condition["Value"].ToString();
            }
            catch { }

            bGotMachinePos_ID = Int32.TryParse(dr_Condition["ValueName"].ToString(), out nMachinePos_ID);
            if (!bGotMachinePos_ID)
            {
                WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, "RoutingCheck::CheckLine::SAMPLEONPOS: no position id found!", (int)Definition.Message.D_ALARM);
            }

            if (nMachinePos_ID > 0)
            {
                nSampleActive_ID = myRoutingData.GetSampleActive_IDFromSampleActiveByMachinePosition_ID(nMachinePos_ID);
                int nSample_ID = -1;
                int RoutingPositionEntry_ID = -1;
                try
                {
                    Int32.TryParse(dr_Condition["RoutingPositionEntry_ID"].ToString(), out RoutingPositionEntry_ID);
                    int nPos = myRoutingData.GetPosition_IDFromRoutingPositionsByRoutingPositionEntry_ID(RoutingPositionEntry_ID);
                    nSample_ID = myRoutingData.GetSampleActive_IDFromSampleActiveByMachinePosition_ID(nPos);
                }
                catch { }
              //  Console.WriteLine("RoutingPositionEntry_ID:" + RoutingPositionEntry_ID + " nSample_ID:" + nSample_ID + " nSampleActive_ID" + nSampleActive_ID);
                nSampleReservation = myRoutingData.GetSampleReservation_IDFromSampleReservationByMachineID(nMachinePos_ID, nSample_ID);
            }
            else
            {
                WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, "RoutingCheck::CheckLine::SAMPLEONPOS: no Machine_ID found!", (int)Definition.Message.D_ALARM);
            }
            // if occupied is selected
            if ((nSampleActive_ID > 0 || nSampleReservation > 0) && String.Equals(strPosOccupied, myThorDef.SampleOnPosOccupiedWord, StringComparison.Ordinal))
            { ret = true; return ret; }
            // if not occupied is selected
            if ((nSampleActive_ID == -1 && nSampleReservation == -1) && String.Equals(strPosOccupied, myThorDef.SampleOnPosNOTOccupiedWord, StringComparison.Ordinal))
            { ret = true; return ret; }

            return ret;
        }


        //check  if ConditionList_ID =='sample type'
        private bool CheckSampleType(DataRow dr_Condition)
        {
            bool ret = false;
            int nOperation_ID = -1;
            bool bGotMachinePos_ID = false;
            int nTypeCondition = Int16.MinValue;
            int nMachinePos_ID = -1;
            int nSampleActive_ID = -1;
            int nType = Int16.MinValue;

            try
            {
                nOperation_ID = Int32.Parse(dr_Condition["Operation_ID"].ToString());
            }
            catch { WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, "RoutingCheck::CheckLine::SAMPLETYPE: Operation_ID is not valid!", (int)Definition.Message.D_ALARM); }

            try
            {
                nTypeCondition = Int32.Parse(dr_Condition["Value"].ToString());
            }
            catch { WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, "RoutingCheck::CheckLine::SAMPLETYPE: type is not a digit!", (int)Definition.Message.D_ALARM); }

            bGotMachinePos_ID = Int32.TryParse(dr_Condition["ValueName"].ToString(), out nMachinePos_ID);
            if (!bGotMachinePos_ID)         
            {
                WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, "RoutingCheck::CheckLine::SAMPLETYPE: no position name found!", (int)Definition.Message.D_ALARM);
            }

            if (nMachinePos_ID > 0)
            {
                nSampleActive_ID = myRoutingData.GetSampleActive_IDFromSampleActiveByMachinePosition_ID(nMachinePos_ID);
            }
            else
            {
                WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, "RoutingCheck::CheckLine::SAMPLETYPE: no Machine_ID found!", (int)Definition.Message.D_ALARM);
            }

            if (nSampleActive_ID > 0)
            {
                nType = myRoutingData.GetSampleTypeFromSampleActiveBySample_ID(nSampleActive_ID);
            }
            else
            {
                return false;
            }
         
            switch (nOperation_ID)
            {
                case (int)Definition.RoutingOperations.EQUALS: // equals
                    if (nType == nTypeCondition) { ret = true; }
                    break;
                case (int)Definition.RoutingOperations.NOTEQUALS: // NOT equals
                    if (nType != nTypeCondition) { ret = true; }
                    break;
                case (int)Definition.RoutingOperations.GREATER: // greater
                    if (nType > nTypeCondition) { ret = true; }
                    break;
                case (int)Definition.RoutingOperations.SMALLER: // smaller
                    if (nType < nTypeCondition) { ret = true; }
                    break;
                case (int)Definition.RoutingOperations.GREATEREQUALS: // greater, equals
                    if (nType >= nTypeCondition) { ret = true; }
                    break;
                case (int)Definition.RoutingOperations.SMALLEREQUALS: // smaller, equals
                    if (nType <= nTypeCondition) { ret = true; }
                    break;
                default:
                    ret = false;
                    break;
            }

            return ret;
        }

        //check  if ConditionList_ID =='sample priority'
        private bool CheckSamplePriority(DataRow dr_Condition)
        {
            bool ret = false;
            int nOperation_ID = -1;
            bool bGotMachinePos_ID = false;
            int nPriorityCondition = Int16.MinValue;
            int nMachinePos_ID = -1;
            int nSampleActive_ID = -1;
            int nPriority = Int16.MinValue;

            try
            {
                nOperation_ID = Int32.Parse(dr_Condition["Operation_ID"].ToString());
            }
            catch { WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, "RoutingCheck::CheckLine::SAMPLEPRIORITY: Operation_ID is not valid!", (int)Definition.Message.D_ALARM); }

            try
            {
                nPriorityCondition = Int32.Parse(dr_Condition["Value"].ToString());
            }
            catch { WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, "RoutingCheck::CheckLine::SAMPLEPRIORITY: priority is not a digit!", (int)Definition.Message.D_ALARM); }


            bGotMachinePos_ID = Int32.TryParse(dr_Condition["ValueName"].ToString(), out nMachinePos_ID);
            if (!bGotMachinePos_ID)           
            {
                WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, "RoutingCheck::CheckLine::SAMPLEPRIORITY: can't get position id!", (int)Definition.Message.D_ALARM);
            }
           
            if (nMachinePos_ID > 0)
            {
                nSampleActive_ID = myRoutingData.GetSampleActive_IDFromSampleActiveByMachinePosition_ID(nMachinePos_ID);
                // if no sampleID found we can quit here
                if (nSampleActive_ID == -1) { return false; }
            }
            else
            {
                WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, "RoutingCheck::CheckLine::SAMPLEPRIORITY: no machine position ID found!", (int)Definition.Message.D_ALARM);
            }
         
            if (nSampleActive_ID > 0)
            {
                nPriority = myRoutingData.GetSamplePriorityFromSampleActiveBySample_ID(nSampleActive_ID);
            }
            else
            {
                WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, "RoutingCheck::CheckLine::SAMPLEPRIORITY: no sample ID found!", (int)Definition.Message.D_ALARM);
                return false;
            }


             switch (nOperation_ID)
                {
                    case (int)Definition.RoutingOperations.EQUALS: // equals
                        if (nPriority == nPriorityCondition) { ret = true; }
                        break;
                    case (int)Definition.RoutingOperations.NOTEQUALS: // NOT equals
                        if (nPriority != nPriorityCondition) { ret = true; }
                        break;
                    case (int)Definition.RoutingOperations.GREATER: // greater
                        if (nPriority > nPriorityCondition) { ret = true; }
                        break;
                    case (int)Definition.RoutingOperations.SMALLER: // smaller
                        if (nPriority < nPriorityCondition) { ret = true; }
                        break;
                    case (int)Definition.RoutingOperations.GREATEREQUALS: // greater, equals
                        if (nPriority >= nPriorityCondition) { ret = true; }
                        break;
                    case (int)Definition.RoutingOperations.SMALLEREQUALS: // smaller, equals
                        if (nPriority <= nPriorityCondition) { ret = true; }
                        break;
                    default:
                        ret = false;
                        break;
                }
                
            return ret;
        }


        //check  if ConditionList_ID =='statusbits'
        private bool CheckStatusBits(DataRow dr_Condition)
        {
            bool ret = false;
            bool bGotStatusbit_ID = false;
            int nOperation_ID = -1;
            int nStausbit_ID = -1;
            string strValueStatusbit = null;
            bool bActualStatusbitValue = false;
            bool bActualStatusbitValueCondition = false;

            try
            {
                nOperation_ID = Int32.Parse(dr_Condition["Operation_ID"].ToString());
                bGotStatusbit_ID = Int32.TryParse(dr_Condition["ValueName"].ToString(), out nStausbit_ID);
                strValueStatusbit =  dr_Condition["Value"].ToString();
                if (String.Equals(strValueStatusbit, myThorDef.TrueWord, StringComparison.Ordinal))
                {
                    bActualStatusbitValueCondition = true;
                }
                else { bActualStatusbitValueCondition = false; }

                
            }
            catch (Exception ex) { WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, "RoutingCheck::CheckLine::STATUSBITS:  \r\nnStausbit_ID:  #" + nStausbit_ID + "#\r\nstrValueStatusbit#" +strValueStatusbit +"#\r\n"+ ex.ToString(), (int)Definition.Message.D_ALARM); }

            if (nOperation_ID > 0)
            {
                try
                {
                    bActualStatusbitValue = myRoutingData.CheckStatusBitFromMachineStausBitsByStatusBits_ID(nStausbit_ID);
                }
                catch (Exception ex) { WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, "RoutingCheck::CheckLine::STATUSBITS:  \r\n" + ex.ToString(), (int)Definition.Message.D_ALARM); }
     
                switch (nOperation_ID)
                {
                    case (int)Definition.RoutingOperations.EQUALS:     // equals
                        if (bActualStatusbitValue == bActualStatusbitValueCondition) { ret = true; }
                        break;
                    case (int)Definition.RoutingOperations.NOTEQUALS:     // not equals
                        if (bActualStatusbitValue != bActualStatusbitValueCondition) { ret = true; }
                        break;

                    default:
                        ret = false;
                        WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, "RoutingCheck::CheckLine::STATUSBITS: wrong Operation_ID found for condition -statusbits- operation_ID: " + nOperation_ID, (int)Definition.Message.D_ALARM);
                        break;
                }
            }

            return ret;
        }

        //check if own magazine pos is free
        private bool CheckOwnMagPos(DataRow dr_Condition)
        {
            bool ret = false;
            bool bIsMagPosOccupied = true;
            int nOperation_ID = -1;
            int nSampleID = -1;
          
            try
            {
                nOperation_ID = Int32.Parse(dr_Condition["Operation_ID"].ToString());
            }
            catch (Exception ex) { WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, "CheckOwnMagPos:: wrong operation_ID \r\nOperation_ID:" + nOperation_ID + "\r\n" + ex.ToString(), (int)Definition.Message.D_ALARM); }

            if (nOperation_ID == 1) // 1 => "="
            {
                int nSelectedRoutingPositionEntry_ID = -1;
                Int32.TryParse(dr_Condition["RoutingPositionEntry_ID"].ToString(), out nSelectedRoutingPositionEntry_ID);
                int nMachinePos = myRoutingData.GetPosition_IDFromRoutingPositionsByRoutingPositionEntry_ID(nSelectedRoutingPositionEntry_ID);
                nSampleID = myRoutingData.GetSampleActive_IDFromSampleActiveByMachinePosition_ID(nMachinePos);
                
                if (nSampleID > 0)
                {
                    try
                    {
                        bIsMagPosOccupied = myRoutingData.CheckIfOwnMagazinePosIsOccupied(nSampleID);
                    }
                    catch (Exception ex) { WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, "RoutingCheck::CheckOwnMagPos: Exception: \r\n" + ex.ToString(), (int)Definition.Message.D_ALARM); }
                }
                if (!bIsMagPosOccupied) { ret = true; }
            }
           
            return ret;
        }


    }
}
