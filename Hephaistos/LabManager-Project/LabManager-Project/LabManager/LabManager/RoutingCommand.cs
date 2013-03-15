using System;
using System.Data;
using Logging;
using Definition;
using MySQL_Helper_Class;
using System.Windows.Forms;
using System.Diagnostics;


namespace Routing
{
    class RoutingCommand
    {
        LabManager.LabManager _parent;
        private Save mySave = new Save("LabManager-RoutingCommand");
        private string loggingString_COMPARE = null;
        private Definitions myThorDef = new Definitions();
        private MySQL_HelperClass myHC = new MySQL_HelperClass();
        private MySQL_HelperClass myHCTimer = new MySQL_HelperClass();
        private RoutingData myRoutingData = new RoutingData();
        private RoutingData myRoutingDataTimer = new RoutingData();
        private RoutingCheck routingCheck = null;
        private System.Windows.Forms.Timer CommandTimer = new System.Windows.Forms.Timer();
      
        public RoutingCommand(LabManager.LabManager parent)
        {
            _parent = parent;
            //_parent.InsertLoggingEntry((int)Definition.ThorLogWindows.ROUTING, "starting checking commands for routing... ");
            WriteLoggEntry((int)Definition.ThorLogWindows.ROUTING, "starting checking commands for routing ... ", (int)Definition.Message.D_MESSAGE);

            //process the timer event to the timer. 
            CommandTimer.Tick += new EventHandler(TimerEventProcessorForCommands);

            // Set the timer interval to 1 second! do not change the time otherwise the Count of the TimeForWaning/TimeForAlarm are not correct
            CommandTimer.Interval = 1000;
            CommandTimer.Start();

        
            // first check the routing conditions
            routingCheck = new RoutingCheck(_parent);
            routingCheck.CheckRoutingTableForActiveConditions();

           
            myHC.ResetActualTimeAndAlarmsOnRoutingPositionEntries();
            myHC.WriteWinCCTagFromSampleValuesByValueName("WarningTimeOn", "false", 1);
            myHC.WriteWinCCTagFromSampleValuesByValueName("AlarmTimeOn", "false", 1);

        }

        

        private void TimerEventProcessorForCommands(Object myObject, EventArgs myEventArgs)
        {
          
           CommandTimer.Stop();
            DateTime value = DateTime.Now;
            Stopwatch stopwatch = new Stopwatch();

            // Begin timing
            stopwatch.Start();

            // in der Labamager_Form.cs wurde der Thread deaktiviert und somit muss der Aufruf hier erfolgen
            CheckForRoutingCommands();
           
            WriteActualTimeForRoutingPositionEntries();

            // Stop timing
            stopwatch.Stop();

            // Write result
          //   Console.WriteLine("Time elapsed: 'CheckForRoutingCommands': {0} ({1:o})", stopwatch.Elapsed, value);

            CommandTimer.Start();
        }

        public int ReloadRoutingTable()
        {
            return routingCheck.LoadRoutingTable();
        }

        private void WriteActualTimeForRoutingPositionEntries()
        {
            int nRoutingPosition = -1;
            string SQL_StatementActiveSamples = myRoutingDataTimer.GetActiveSamples();
            DataSet dsActiveSamples = new DataSet();
            dsActiveSamples.Clear();
            dsActiveSamples = myHCTimer.GetDataSetFromSQLCommand(SQL_StatementActiveSamples);
            DataTable dtActiveSamples = new DataTable();

            if (dsActiveSamples != null)
            {
                if (dsActiveSamples.Tables.Count > 0)
                {
                    dtActiveSamples = dsActiveSamples.Tables[0];

                    // WriteLoggEntry((int)Definition.ThorLogWindows.ROUTING, "RoutingCommand:: --- sample count: " + dtActiveSamples.Rows.Count, (int)Definition.Message.D_DEBUG); 

                    // check all samples if there is a routing line for the position
                    foreach (DataRow drActiveSample in dtActiveSamples.Rows)
                    {
                        int nMachinePosition_ID = -1;
                        int nSampleProgram_ID = -1;
                        int Machine_ID = -1;
                        try
                        {
                            nMachinePosition_ID = Int32.Parse(drActiveSample["ActualSamplePosition_ID"].ToString());
                            nSampleProgram_ID = Int32.Parse(drActiveSample["SampleProgramType_ID"].ToString());
                            Machine_ID = myHCTimer.GetMachineIDFromMachinePositions_ID(nMachinePosition_ID);
                        }
                        catch (Exception ex) { WriteLoggEntry((int)Definition.ThorLogWindows.ROUTING, @"CheckForRoutingConditions: Exception: \r\n" + ex.ToString(), (int)Definition.Message.D_ALARM); }


                        nRoutingPosition = myRoutingDataTimer.GetRoutingPosition_IDFromRoutingPositionsByMachinePosition_ID(nMachinePosition_ID);
                        int nSampleType = myRoutingDataTimer.GetSampleTypeFromSampleProgramsBySamplePrograms_ID(nSampleProgram_ID);

                        if (nRoutingPosition > 0)
                        {
                            try
                            {
                                string SQL_StatementRoutingPositionEntries = myRoutingDataTimer.Get_SQL_StatementForRoutingPositionEntriesByRoutingPositionAndSampleType(nRoutingPosition, nSampleType);
                                DataSet dsRoutingPositionEntries = new DataSet();
                                dsRoutingPositionEntries.Clear();
                                dsRoutingPositionEntries = myHCTimer.GetDataSetFromSQLCommand(SQL_StatementRoutingPositionEntries);
                                DataTable dtRoutingPositionEntries = new DataTable();
                                dtRoutingPositionEntries = dsRoutingPositionEntries.Tables[0];

                                foreach (DataRow drRoutingPositionEntry in dtRoutingPositionEntries.Rows)
                                {

                                    int nRoutingPositionEntry_ID = Int32.Parse(drRoutingPositionEntry["idrouting_position_entries"].ToString());
                                    string SQL_StatementConditionComply = myRoutingDataTimer.Get_SQL_StatementCondition_ComplyFromRoutingConditionsBYRoutingPositionEntry_ID(nRoutingPositionEntry_ID);


                                    // increase Counter
                                    if (!_parent.LockRoutingCheck)
                                    {
                                        myHCTimer.UpdateActualTimeOnRoutingPositionEntriesByRouting_Position_Entry_ID(nRoutingPositionEntry_ID);
                                    }
                                }

                            }
                            catch (Exception ex) { WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, "RoutingCommand::WriteActualTimeForRoutingPositionEntries: " + ex.ToString(), (int)Definition.Message.D_ALARM); }
                        }
                    }
                }
            }
         }

        private void WriteLoggEntry(int LogType, string strLogString, int AlarmType = (int)Definition.Message.D_MESSAGE)
        {
            if (!String.Equals(strLogString, loggingString_COMPARE, StringComparison.Ordinal))
            {
           //     MethodInvoker Logging = delegate
            //    {
                    // doing Logging entries
                    _parent.WriteTCPIPLoggEntry(LogType, strLogString, AlarmType);
            //    };
             //   try
             //   {
             //       _parent.Invoke(Logging);
              //  }
             //   catch  {}
               // mySave.InsertRow((int)Definition.Message.D_ALARM, strLogString); 
                loggingString_COMPARE = strLogString;
            }
        }


        public void CheckForRoutingCommands()
        {
            // first check the routing conditions
            // now done only by RoutingPositionEntry_ID
            //routingCheck.CheckRoutingTableForActiveConditions();

            if (!_parent.LockRoutingCheck)
            {
                int nRoutingPosition = -1;
                string SQL_StatementActiveSamples = myRoutingData.GetActiveSamples();
                DataSet dsActiveSamples = new DataSet();
                dsActiveSamples.Clear();
                dsActiveSamples = myHC.GetDataSetFromSQLCommand(SQL_StatementActiveSamples);
                DataTable dtActiveSamples = new DataTable();
                dtActiveSamples = dsActiveSamples.Tables[0];

                // WriteLoggEntry((int)Definition.ThorLogWindows.ROUTING, "RoutingCommand:: --- sample count: " + dtActiveSamples.Rows.Count, (int)Definition.Message.D_DEBUG); 

                // check all samples if there is a routing line for the position
                foreach (DataRow drActiveSample in dtActiveSamples.Rows)
                {
                    int nMachinePosition_ID = -1;
                    int nSampleProgram_ID = -1;
                    int nSample_ID = -1;
                   

                    try
                    {
                        nMachinePosition_ID = Int32.Parse(drActiveSample["ActualSamplePosition_ID"].ToString());
                        nSampleProgram_ID = Int32.Parse(drActiveSample["SampleProgramType_ID"].ToString());
                        nSample_ID = Int32.Parse(drActiveSample["idactive_samples"].ToString());

                    }
                    catch (Exception ex) { WriteLoggEntry((int)Definition.ThorLogWindows.ROUTING, @"CheckForRoutingConditions: Exception: \r\n" + ex.ToString(), (int)Definition.Message.D_ALARM); }


                    nRoutingPosition = myRoutingData.GetRoutingPosition_IDFromRoutingPositionsByMachinePosition_ID(nMachinePosition_ID);
                    int nSampleType = myRoutingData.GetSampleTypeFromSampleProgramsBySamplePrograms_ID(nSampleProgram_ID);

                    if (nRoutingPosition > 0)
                    {
                        string SQL_StatementRoutingPositionEntries = myRoutingData.Get_SQL_StatementForRoutingPositionEntriesByRoutingPositionAndSampleType(nRoutingPosition, nSampleType);
                        DataSet dsRoutingPositionEntries = new DataSet();
                        dsRoutingPositionEntries.Clear();
                        dsRoutingPositionEntries = myHC.GetDataSetFromSQLCommand(SQL_StatementRoutingPositionEntries);
                        DataTable dtRoutingPositionEntries = new DataTable();
                        dtRoutingPositionEntries = dsRoutingPositionEntries.Tables[0];

                        foreach (DataRow drRoutingPositionEntry in dtRoutingPositionEntries.Rows)
                        {
                            int nRoutingPositionEntry_ID = Int32.Parse(drRoutingPositionEntry["idrouting_position_entries"].ToString());

                            // check the conditions by routingPositionEntry_ID first, then lookup the commads
                            routingCheck.RunRoutingCheckByRoutingEntryposition_ID(nRoutingPositionEntry_ID);

                            string SQL_StatementConditionComply = myRoutingData.Get_SQL_StatementCondition_ComplyFromRoutingConditionsBYRoutingPositionEntry_ID(nRoutingPositionEntry_ID);

                            ////////////////////////////////////
                            try
                            {
                                int nTimeForWarning = (int)drRoutingPositionEntry["TimeForWarning"];
                                int nTimeForAlarm = (int)drRoutingPositionEntry["TimeForAlarm"];
                                int nActualTime = (int)drRoutingPositionEntry["ActualTime"];

                                if (nActualTime == nTimeForWarning && nTimeForWarning > 0)
                                {
                                    myHC.SetAlarmTimeForWarningOnRoutingPositionEntriesByRouting_Position_Entry_ID(nRoutingPositionEntry_ID);
                                }

                                if (nActualTime == nTimeForAlarm && nTimeForAlarm > 0)
                                {
                                    myHC.SetAlarmTimeForAlarmOnRoutingPositionEntriesByRouting_Position_Entry_ID(nRoutingPositionEntry_ID);
                                }
                            }
                            catch (Exception ex) { WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, "RoutingCommand::WriteActualTimeForRoutingPositionEntries: " + ex.ToString(), (int)Definition.Message.D_ALARM); }

                            //////////////////////////////////////
                            int nCountConditionTrue = 0;

                            
                            DataSet dsRoutingConditionComply = new DataSet();
                            dsRoutingConditionComply.Clear();
                            dsRoutingConditionComply = myHC.GetDataSetFromSQLCommand(SQL_StatementConditionComply);
                            DataTable dtRoutingConditionComply = new DataTable();
                            dtRoutingConditionComply = dsRoutingConditionComply.Tables[0];

                            // all "condition_comply" tags must be true, then execute command(s) 
                            foreach (DataRow drConditonComply in dtRoutingConditionComply.Rows)
                            {
                                try
                                {
                                    if ((bool)drConditonComply["Condition_comply"]) { nCountConditionTrue++; }
                                }
                                catch { }
                            }

                            //if all conditions are true, execute the command(s)
                            if (dtRoutingConditionComply.Rows.Count == nCountConditionTrue && nCountConditionTrue > 0)
                            {
                                ExecuteCommandsByRoutingPositionEntry_ID(nRoutingPositionEntry_ID, nSample_ID, nSampleType, nSampleProgram_ID, drActiveSample, nMachinePosition_ID);

                                myHC.ResetActualTimeAndAlarmsOnRoutingPositionEntries(nRoutingPosition);

                                // if a command(s) is executed, first check the routing condition again before execute the next command
                              ////  routingCheck.CheckRoutingTableForActiveConditions();
                            }
                           
                        }
                    }
                    myRoutingData.CheckIfCommandActiveOnSampleActive();
                }
            }
        }

        private void ExecuteCommandsByRoutingPositionEntry_ID(int nRoutingPositionEntry_ID, int nSample_ID, int nSampleType, int nSampleProgram_ID, DataRow drActiveSample, int nMachinePosition_ID)
        {
            bool bCommandAllreadyExistInCommandTable = false;
            bool bCommandStayActiveIsInCommandList = false;
            bool bCommandChangeSampleTypeInCommandList = false;

            string SQL_StatementCommands = myRoutingData.Get_SQL_StatementCommandsBYRoutingPositionEntry_ID(nRoutingPositionEntry_ID);
            string strRoutingPositionEntryLogString = null;
            int nMachine_Position = myHC.GetMachinePosition_IDFromRoutingCondition(nRoutingPositionEntry_ID);
            int nMachine_ID = myHC.GetMachine_IDFromMachinePositionsByMachinePositionID(nMachine_Position);

            strRoutingPositionEntryLogString = @"" + myHC.GetNameFromID((int)Definition.SQLTables.MACHINES, nMachine_ID) + " (id:" + nMachine_ID + ") ->" +
                " " + myHC.GetNameFromID((int)Definition.SQLTables.MACHINE_POSITIONS, nMachine_Position) + " (id: " + nMachine_Position +") -> " +
                "'" + myHC.GetNameFromID((int)Definition.SQLTables.ROUTING_POSITION_ENTRIES, nRoutingPositionEntry_ID) + "' (id:" + nRoutingPositionEntry_ID +") executing ";

            DataSet dsCommandsToExecute = new DataSet();
            dsCommandsToExecute.Clear();
            dsCommandsToExecute = myHC.GetDataSetFromSQLCommand(SQL_StatementCommands);
            DataTable dtCommandsToExecute = new DataTable();
            dtCommandsToExecute = dsCommandsToExecute.Tables[0];

            // check if a command allready in the command_active table if command for a machine is in the commandlist to execute
            nMachine_ID = -1;
            foreach (DataRow drCommandsToExecute in dtCommandsToExecute.Rows)
            {
                int  nCommand_ID = Int32.Parse(drCommandsToExecute["Command_ID"].ToString());
                if (nCommand_ID == (int)Definition.RoutingCommands.SENDINGCOMMANDTOMACHINE)
                {
                   nMachine_ID = Int32.Parse(drCommandsToExecute["CommandValue0"].ToString());
                    if (myRoutingData.CheckIfCommandForMachineAllreadyExist(nMachine_ID))
                    {
                        bCommandAllreadyExistInCommandTable = true;
                        WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, "RoutingCommand::ExecuteCommandForMachine: Command for machine (machine_ID:" + nMachine_ID + ") allready exist in Command_active table - command(s) is/are not executeted yet", (int)Definition.Message.D_ALARM);
                  
                    }
                }
            }
            // command is command_active table, so don't execute the commands 
            if (bCommandAllreadyExistInCommandTable) { return; }

            foreach (DataRow drCommandsToExecute in dtCommandsToExecute.Rows)
            {

                int nCommand_ID = Int32.Parse(drCommandsToExecute["Command_ID"].ToString());
              
                switch (nCommand_ID)
                {
                    case (int)Definition.RoutingCommands.SHIFTSAMPLE: // shift sample 
                        ExecuteShiftSample(nSample_ID, drActiveSample, drCommandsToExecute, strRoutingPositionEntryLogString);
                        break;

                    case (int)Definition.RoutingCommands.CREATESAMPLE: // create sample 
                        ExecuteCreateSample(nSample_ID, drActiveSample, drCommandsToExecute, strRoutingPositionEntryLogString);
                        break;

                    case (int)Definition.RoutingCommands.DELETESAMPLE: // delete sample 
                        ExecuteDeleteSample(nSample_ID, drActiveSample, drCommandsToExecute, strRoutingPositionEntryLogString);
                        break;

                    case (int)Definition.RoutingCommands.CHANGESAMPLETYPE: // change sample Type
                        ExecuteChangeSampleType(nSample_ID, drActiveSample, drCommandsToExecute, strRoutingPositionEntryLogString);
                        bCommandChangeSampleTypeInCommandList = true;
                        break;

                    case (int)Definition.RoutingCommands.CHANGEPRIORITY: // change priority
                        ExecuteChangePriority(nSample_ID, drActiveSample, drCommandsToExecute, strRoutingPositionEntryLogString);
                        break;

                    case (int)Definition.RoutingCommands.WRITEGLOBALTAG: // write Global tag (WinCC)
                        ExecuteWriteWinCCGlobalTag(nSample_ID, drActiveSample, drCommandsToExecute, strRoutingPositionEntryLogString);
                        break;

                    case (int)Definition.RoutingCommands.WRITEMACHINETAG: // write machine Tag (WinCC)
                        ExecuteWriteWinCCMachineTag(nSample_ID, drActiveSample, drCommandsToExecute, strRoutingPositionEntryLogString);
                        break;

                    case (int)Definition.RoutingCommands.INSERTWSENTRY: // insert WS entry
                        ExecuteInsertWSEntry(nSample_ID, drActiveSample, drCommandsToExecute, strRoutingPositionEntryLogString);
                        break;

                    case (int)Definition.RoutingCommands.DELETEWSENTRY: // delete WS entry
                        ExecuteDeleteWSEntry(nSample_ID, drActiveSample, drCommandsToExecute, strRoutingPositionEntryLogString);
                        break;

                    case (int)Definition.RoutingCommands.SENDINGCOMMANDTOMACHINE: // sending command to machine x
                        ExecuteCommandForMachine(nSample_ID, nSampleType, nSampleProgram_ID, drActiveSample, drCommandsToExecute, strRoutingPositionEntryLogString);
                        break;

                    case (int)Definition.RoutingCommands.CREATERESERVATION: // creating reservation point
                        ExecuteCreateReservation(nSample_ID, drActiveSample, drCommandsToExecute, strRoutingPositionEntryLogString);
                        break;

                    case (int)Definition.RoutingCommands.DELETERESERVATION: // delete reservation point
                        ExecuteDeleteReservation(nSample_ID, drActiveSample, drCommandsToExecute, strRoutingPositionEntryLogString);
                        break;

                    case (int)Definition.RoutingCommands.STAYACTIVE: // stay active
                        bCommandStayActiveIsInCommandList = true;
                        break;

                    default:
                        WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, "RoutingCommand:: execute Command for RoutingPositionEntry: '" + strRoutingPositionEntryLogString + "' failed - unknown Command_ID: " + nCommand_ID, (int)Definition.Message.D_ALARM);
                        break;
                }
            }
            // reset the Time values, so the time will be start from 0
            routingCheck.ResetTimeValuesByExecutingACommand(nRoutingPositionEntry_ID);

            // if command "stay active" or "change SampleType" is in the command list, don't reset the flag "Command_Active" in the "sample_active" table
            if (bCommandStayActiveIsInCommandList || bCommandChangeSampleTypeInCommandList)
            {
               // Console.WriteLine(" sample point stays active");
                myRoutingData.SetCommandActiveOnSampleActiveBySample_ID(nSample_ID, 0, nMachinePosition_ID);
            }else{
                myRoutingData.SetCommandActiveOnSampleActiveBySample_ID(nSample_ID, 1, nMachinePosition_ID);
            }

       }

        private void ExecuteShiftSample(int nSample_ID, DataRow drActiveSample, DataRow drCommandsToExecute, string strRoutingPositionEntryLogString)
        {
            int nMachinePosition_ID = -1;

            if (Int32.TryParse(drActiveSample["ActualSamplePosition_ID"].ToString(), out nMachinePosition_ID))
            {
                string Value = null;

                if (nSample_ID > 0)
                {
                    Value = drCommandsToExecute["CommandValue0"].ToString();
                    if (Value.IndexOf('<') != -1)
                    { // if something in the string to translate open a new Translater and replace the values
                        LabManager.TranslateString translater = new LabManager.TranslateString();
                        Value = translater.TranslateValueString(Value, nSample_ID);
                    }

                    WriteLoggEntry((int)Definition.ThorLogWindows.ROUTING, strRoutingPositionEntryLogString + @"command:: shifting sample" +
                        " :`" + myHC.GetNameFromID((int)Definition.SQLTables.SAMPLE_ACTIVE, nSample_ID) + "`(id:" + nSample_ID + ")" +
                        " to position:  `" + myHC.GetNameFromID((int)Definition.SQLTables.MACHINE_POSITIONS, Int32.Parse(Value)) + "`(id:" + Value + ")" +
                        " from position: `" + myHC.GetNameFromID((int)Definition.SQLTables.MACHINE_POSITIONS, nMachinePosition_ID) + "`(id:" + nMachinePosition_ID + ")");

                    myRoutingData.ShiftSampleToPositionXBySample_ID(nSample_ID, Value);
                    // myRoutingData.SetCommandActiveOnRoutingCommandsByRoutingCommands_ID(nRouting_Commands_ID, 1);

                }
                else
                {
                    int nRouting_Commands_ID = -1;
                    Int32.TryParse(drCommandsToExecute["idrouting_commands"].ToString(), out nRouting_Commands_ID);
                    WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, strRoutingPositionEntryLogString + "command:: Execute ShiftSample: no Sample_ID found for nRouting_Command_ID:" + nRouting_Commands_ID + "  ", (int)Definition.Message.D_ALARM);
                }

            }
            else
            {
                WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, strRoutingPositionEntryLogString + "command:: Execute ShiftSample: no ActualSamplePosition_ID:" + nMachinePosition_ID + " found ", (int)Definition.Message.D_ALARM);
              
            }
        }

        private void ExecuteCreateSample(int nSample_ID, DataRow drActiveSample, DataRow drCommandsToExecute, string strRoutingPositionEntryLogString)
        {
            int nMachinePosition_ID = Int32.Parse(drActiveSample["ActualSamplePosition_ID"].ToString());
            string strSampleName = null;
            int nPriority = -1;
            int nProgramType_ID = -1;
            int nPosition_ID = -1;
            LabManager.TranslateString translater = new LabManager.TranslateString();
           
            nProgramType_ID = Int32.Parse(drCommandsToExecute["CommandValue1"].ToString());
           // strSampleName = translater.TranslateSampleRegistrationString(drCommandsToExecute["CommandValue2"].ToString());
            strSampleName = translater.TranslateValueString(drCommandsToExecute["CommandValue2"].ToString(), nSample_ID);
            nPriority = Int32.Parse(drCommandsToExecute["CommandValue3"].ToString());
            nPosition_ID = Int32.Parse(drCommandsToExecute["CommandValue0"].ToString());
          
            int nRouting_Commands_ID = Int32.Parse(drCommandsToExecute["idrouting_commands"].ToString());
           //  if 
                {
                  
                    WriteLoggEntry((int)Definition.ThorLogWindows.ROUTING, strRoutingPositionEntryLogString + @"command:: create sample" +
                           ": `" + strSampleName +
                           "` with program: `" + myHC.GetNameFromID((int)Definition.SQLTables.SAMPLE_PROGRAM, nProgramType_ID) + "`(id:" + nProgramType_ID + ")" +
                           " with priority: `" + nPriority + "`" +
                           " on position: `" + myHC.GetNameFromID((int)Definition.SQLTables.MACHINE_POSITIONS, nPosition_ID) + "`(id:" + nPosition_ID + ")" +
                           " from position: `" + myHC.GetNameFromID((int)Definition.SQLTables.MACHINE_POSITIONS, nMachinePosition_ID) + "`(id:" + nMachinePosition_ID + ")");
                  
                    myRoutingData.InsertSampleInSample_ActiveBySampleProgrammType_IDAndSampleIDAndPriority(nProgramType_ID, strSampleName, nPriority, nPosition_ID);
                   // myRoutingData.SetCommandActiveOnRoutingCommandsByRoutingCommands_ID(nRouting_Commands_ID, 1);
                    
                }
            
        }

        private void ExecuteDeleteSample(int nSample_ID, DataRow drActiveSample, DataRow drCommandsToExecute, string strRoutingPositionEntryLogString)
        {
            int nMachinePosition_ID = Int32.Parse(drActiveSample["ActualSamplePosition_ID"].ToString());
         
            int nRouting_Commands_ID = Int32.Parse(drCommandsToExecute["idrouting_commands"].ToString());
           
                WriteLoggEntry((int)Definition.ThorLogWindows.ROUTING,strRoutingPositionEntryLogString + @"command:: delete sample" +
                      ": `" + myHC.GetNameFromID((int)Definition.SQLTables.SAMPLE_ACTIVE, nSample_ID) + "` (id:" + nSample_ID + ")" +
                      " from position: `" + myHC.GetNameFromID((int)Definition.SQLTables.MACHINE_POSITIONS, nMachinePosition_ID) + "`(id:" + nMachinePosition_ID + ")");

                myRoutingData.DeleteSampleInSample_ActiveBySampleID(nSample_ID);

        }

        private void ExecuteChangeSampleType(int nSample_ID, DataRow drActiveSample, DataRow drCommandsToExecute, string strRoutingPositionEntryLogString)
        {
            int nMachinePosition_ID = Int32.Parse(drActiveSample["ActualSamplePosition_ID"].ToString());
            string Value = null;

            int nRouting_Commands_ID = Int32.Parse(drCommandsToExecute["idrouting_commands"].ToString());
           
                if (nSample_ID > 0)
                {
                    Value = drCommandsToExecute["CommandValue0"].ToString();
                    if (Value.IndexOf('<') != -1)
                    { // if something in the string to translate open a new Translater and replace the values
                        LabManager.TranslateString translater = new LabManager.TranslateString();
                        Value = translater.TranslateValueString(Value, nSample_ID);
                    }

                    WriteLoggEntry((int)Definition.ThorLogWindows.ROUTING, strRoutingPositionEntryLogString + @"command:: changing the SampleProgramType" +
                        " for sample: `" + myHC.GetNameFromID((int)Definition.SQLTables.SAMPLE_ACTIVE, nSample_ID) + "`(id:" + nSample_ID + ")" +
                        " to value: `" + myHC.GetNameFromID((int)Definition.SQLTables.SAMPLE_PROGRAM, Int32.Parse(Value)) + "`n(id:" + Value + ")" +
                        " from position: `" + myHC.GetNameFromID((int)Definition.SQLTables.MACHINE_POSITIONS, nMachinePosition_ID) + "`(id:" + nMachinePosition_ID + ")");

                    myRoutingData.ChangeSampleTypeInSampleBySample_ID(nSample_ID, Value);
                  //  myRoutingData.SetCommandActiveOnRoutingCommandsByRoutingCommands_ID(nRouting_Commands_ID, 1);
                
                }
           
        }

        private void ExecuteChangePriority(int nSample_ID, DataRow drActiveSample, DataRow drCommandsToExecute, string strRoutingPositionEntryLogString)
        {
            int nMachinePosition_ID = Int32.Parse(drActiveSample["ActualSamplePosition_ID"].ToString());
            string Value = null;
            int nRouting_Commands_ID = Int32.Parse(drCommandsToExecute["idrouting_commands"].ToString());
          
                if (nSample_ID > 0)
                {
                    Value = drCommandsToExecute["CommandValue0"].ToString();
                    if (Value.IndexOf('<') != -1)
                    { // if something in the string to translate open a new Translater and replace the values
                        LabManager.TranslateString translater = new LabManager.TranslateString();
                        Value = translater.TranslateValueString(Value, nSample_ID);
                    }

                    WriteLoggEntry((int)Definition.ThorLogWindows.ROUTING, strRoutingPositionEntryLogString + @"command:: changing the priority" +
                        " for sample: `" + myHC.GetNameFromID((int)Definition.SQLTables.SAMPLE_ACTIVE, nSample_ID) + "`(id:" + nSample_ID + ")" +
                        " to value:  `" + Value + "`" +
                        " from position: `" + myHC.GetNameFromID((int)Definition.SQLTables.MACHINE_POSITIONS, nMachinePosition_ID) + "`(id:" + nMachinePosition_ID + ")");

                    myRoutingData.ChangePriorityValueInSampleBySample_ID(nSample_ID, Value);
                    //myRoutingData.SetCommandActiveOnRoutingCommandsByRoutingCommands_ID(nRouting_Commands_ID, 1);
                   
                }
            
        }

        private void ExecuteWriteWinCCGlobalTag(int nSample_ID, DataRow drActiveSample, DataRow drCommandsToExecute, string strRoutingPositionEntryLogString)
        {
            int nMachinePosition_ID = Int32.Parse(drActiveSample["ActualSamplePosition_ID"].ToString());
            string strValue = null;
            int nValueName_ID = -1;
            int nRouting_Commands_ID = Int32.Parse(drCommandsToExecute["idrouting_commands"].ToString());
           
                if (nSample_ID > 0)
                {
                    string strTagName = drCommandsToExecute["CommandValue2"].ToString();
                    strValue = drCommandsToExecute["CommandValue3"].ToString();
                    if (strValue.IndexOf('<') != -1)
                    { // if something in the string to translate open a new Translater and replace the values
                        LabManager.TranslateString translater = new LabManager.TranslateString();
                        strValue = translater.TranslateValueString(strValue, nSample_ID);
                    }
                    nValueName_ID = myHC.GetIDFromName((int)Definition.SQLTables.GLOBAL_TAGS, strTagName);
                    if (nValueName_ID == -1)
                    {
                        WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, strRoutingPositionEntryLogString + @"ExecuteWriteWinCCGlobalTag:: no id found for globaltag '" + strTagName + "' (may be tag does not exist?)");
                    }
                    else
                    {

                        int nType = myHC.GetWinCCTypeFromTagID((int)Definition.SQLTables.GLOBAL_TAGS, nValueName_ID);
                        if (nType == 1) // bit
                        {
                            strValue = strValue.ToLower();
                        }
                        if (nType == 8 || nType == 9)  //DM_VARTYPE_FLOAT       /* Gleitkommazahl 32-Bit IEEE 
                        {
                            strValue = strValue.Replace(',', '.');
                        }
                        WriteLoggEntry((int)Definition.ThorLogWindows.ROUTING, strRoutingPositionEntryLogString + @"command:: writing WinCC global tag `" + strTagName + "`(id:" + nValueName_ID + ")" +
                            " with value: `" + strValue + "`" +
                            " from sample:  `" + myHC.GetNameFromID((int)Definition.SQLTables.SAMPLE_ACTIVE, nSample_ID) + "`(id:" + nSample_ID + ")" +
                            " from position: `" + myHC.GetNameFromID((int)Definition.SQLTables.MACHINE_POSITIONS, nMachinePosition_ID) + "`(id:" + nMachinePosition_ID + ")");

                        myRoutingData.WriteWinCCTagFromSampleValuesByValueName(strTagName, strValue, nType);
                    }
                }
            
        }
        private void ExecuteWriteWinCCMachineTag(int nSample_ID, DataRow drActiveSample, DataRow drCommandsToExecute, string strRoutingPositionEntryLogString)
        {
            int nMachinePosition_ID = Int32.Parse(drActiveSample["ActualSamplePosition_ID"].ToString());
            string strValue = null;
            string strTagName = null;
            int nValueName_ID = -1;
            int nRouting_Commands_ID = Int32.Parse(drCommandsToExecute["idrouting_commands"].ToString());

            if (nSample_ID > 0)
            {
                strTagName = drCommandsToExecute["CommandValue2"].ToString();
                strValue = drCommandsToExecute["CommandValue3"].ToString();
                if (strValue.IndexOf('<') != -1)
                { // if something in the string to translate open a new Translater and replace the values
                    LabManager.TranslateString translater = new LabManager.TranslateString();
                    strValue = translater.TranslateValueString(strValue, nSample_ID);
                }
                nValueName_ID = myHC.GetIDFromName((int)Definition.SQLTables.MACHINE_TAGS, strTagName);
                int nType = myHC.GetWinCCTypeFromTagID((int)Definition.SQLTables.MACHINE_TAGS, nValueName_ID);
                if (nType == 1) // bit
                {
                    strValue = strValue.ToLower();
                }
                if (nType == 8 || nType == 9)  //DM_VARTYPE_FLOAT       /* Gleitkommazahl 32-Bit IEEE 
                {
                    strValue = strValue.Replace(',', '.');
                }
                WriteLoggEntry((int)Definition.ThorLogWindows.ROUTING, strRoutingPositionEntryLogString + @"command:: writing WinCC machine tag `" + strTagName + "`(id:" + nValueName_ID + ")" +
                    " with value: `" + strValue + "`" +
                    " from sample:  `" + myHC.GetNameFromID((int)Definition.SQLTables.SAMPLE_ACTIVE, nSample_ID) + "`(id:" + nSample_ID + ")" +
                    " from position: `" + myHC.GetNameFromID((int)Definition.SQLTables.MACHINE_POSITIONS, nMachinePosition_ID) + "`(id:" + nMachinePosition_ID + ")");

                myRoutingData.WriteWinCCTagFromSampleValuesByValueName(strTagName, strValue, nType);   
            }

        }

        private void ExecuteInsertWSEntry(int nSample_ID, DataRow drActiveSample, DataRow drCommandsToExecute, string strRoutingPositionEntryLogString)
        {
            int nMachinePosition_ID = Int32.Parse(drActiveSample["ActualSamplePosition_ID"].ToString());
            string ValueName = null;
            string Value = null;
            string strSampleID = null;
            int nLocationType = -1;
            bool bHiddenFlag = false;

            int nRouting_Commands_ID = Int32.Parse(drCommandsToExecute["idrouting_commands"].ToString());
          
            strSampleID = drActiveSample["SampleID"].ToString();

            if (nSample_ID > 0 && strSampleID.Length > 1)
            {
                ValueName = drCommandsToExecute["CommandValue2"].ToString();
                Value = drCommandsToExecute["CommandValue3"].ToString();
                Int32.TryParse(drCommandsToExecute["CommandValue0"].ToString(), out nLocationType);

               
                if (Value.Length > 0 && ValueName.Length > 0)
                {
                    if (Value.IndexOf('<') != -1)
                    { // if something in the string to translate, open a new Translater and replace the values
                        LabManager.TranslateString translater = new LabManager.TranslateString();
                        Value = translater.TranslateValueString(Value, nSample_ID);
                    }
                    if (nLocationType == (int)Definition.WSInsertLocation.OWNPOSHIDDEN ||
                        nLocationType == (int)Definition.WSInsertLocation.SAMPLEONPOSHIDDEN ||
                        nLocationType == (int)Definition.WSInsertLocation.WRITETOPARENTHIDDEN)
                    { bHiddenFlag = true; }

                    if (nLocationType == (int)Definition.WSInsertLocation.SAMPLEONPOS)
                    {
                        int nPosition = -1;
                        Int32.TryParse(drCommandsToExecute["CommandValue1"].ToString(), out nPosition);
                        string strSampleIDonPos = null;
                        nSample_ID = myHC.GetSampleActive_IDIfOnMachinePosition(nPosition);
                        if (nSample_ID > 0)
                        {
                            strSampleIDonPos = myHC.GetNameFromID((int)Definition.SQLTables.SAMPLE_ACTIVE, nSample_ID);
                            if (strSampleIDonPos != null)
                            {
                                strSampleID = strSampleIDonPos;
                            }
                            else
                            {
                                WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, strRoutingPositionEntryLogString + @"ExecuteInsertWSEntry:: no name found for sample with id '" + nSample_ID + "");
                            }
                        }
                        else
                        {
                            WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, strRoutingPositionEntryLogString + @"ExecuteInsertWSEntry:: no sample id found on position: '" + nPosition + "");

                        }
                    }


                    if (nLocationType == (int)Definition.WSInsertLocation.WRITETOPARENT)
                    {
                        string strSampleIDParent = null;
                        strSampleIDParent = myHC.GetSampleValueFromSampleValuesByValueName( "Parent_ID",  strSampleID);
                        if (strSampleID.Length > 0)
                        {
                            nSample_ID = myHC.GetIDFromName((int)Definition.SQLTables.SAMPLE_ACTIVE, strSampleIDParent);
                            if (nSample_ID > 0)
                            {
                                strSampleID = strSampleIDParent;
                            }
                            else
                            {
                                WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, strRoutingPositionEntryLogString + @"ExecuteInsertWSEntry:: no id found for sample with name '" + strSampleID + "");
                            }
                        }
                        else
                        {
                            WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, strRoutingPositionEntryLogString + @"ExecuteInsertWSEntry:: no Parent_ID found");

                        }
                    }

                    if (Value.Length > 0) // look if value after translate is still > 0
                    {
                        //myHC.GetNameFromID((int)Definition.SQLTables.SAMPLE_ACTIVE, nSample_ID)
                        WriteLoggEntry((int)Definition.ThorLogWindows.ROUTING, strRoutingPositionEntryLogString + @"command:: inserting worksheet entry `" + ValueName + "`" +
                            " with value: `" + Value + "`" +
                            " into sample:  `" + strSampleID + "`(id:" + nSample_ID + ")" +
                            " from Routing position:  `" + myHC.GetNameFromID((int)Definition.SQLTables.MACHINE_POSITIONS, nMachinePosition_ID) + "`(id:" + nMachinePosition_ID + ")");
                        
                        switch (ValueName)
                        {
                            case "SampleID":
                            case "sampleid":
                            case "Sampleid":
                            case "SAMPLEID":
                                {
                                    if (Value.Length > 2)
                                    {
                                        myRoutingData.ChangeSampleIDBySample_ID(nSample_ID, Value);
                                        myRoutingData.InsertWSEntryFromSampleValuesBySample_IDAndValueName(nSample_ID, "OriginalSampleID", strSampleID, Value, bHiddenFlag);
                                    }
                                    else {
                                        WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, strRoutingPositionEntryLogString + "command:: ExecuteInsertWSEntry for Sample `" + strSampleID + "` with ID: " + nSample_ID + " failed -  value for SampleID empty or to short (Value: " + Value.ToString() + ")", (int)Definition.Message.D_ALARM);
                                    }
                                }
                                break;

                            default:    // insert into the table "sample_values"
                                myRoutingData.InsertWSEntryFromSampleValuesBySample_IDAndValueName(nSample_ID, ValueName, Value, myHC.GetNameFromID((int)Definition.SQLTables.SAMPLE_ACTIVE, nSample_ID), bHiddenFlag);
                                break;
                        }
                       
                    }
                    else
                    {
                        WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, strRoutingPositionEntryLogString + "command:: ExecuteInsertWSEntry for Sample `" + strSampleID + "` with ID: " + nSample_ID + " failed - value after translate function is null (Value: " + Value.ToString() + ")", (int)Definition.Message.D_ALARM);
                    }
                }
                else
                {
                    WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, strRoutingPositionEntryLogString + "command:: ExecuteInsertWSEntry for Sample  `" + strSampleID + "` with ID: " + nSample_ID + " failed - no Value or ValueName given (Value: " + Value + "; Valuename: " + ValueName + ")", (int)Definition.Message.D_ALARM);                   
                }
            }
            else
            {
                WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, strRoutingPositionEntryLogString + "command:: ExecuteInsertWSEntry for Sample `" + strSampleID + "` with ID: " + nSample_ID + " failed - no SampleName: " + strSampleID, (int)Definition.Message.D_ALARM);                   
            }
            
        }

        private void ExecuteDeleteWSEntry(int nSample_ID, DataRow drActiveSample, DataRow drCommandsToExecute, string strRoutingPositionEntryLogString)
        {
            int nMachinePosition_ID = Int32.Parse(drActiveSample["ActualSamplePosition_ID"].ToString());
            string ValueName = null;
            int nRouting_Commands_ID = Int32.Parse(drCommandsToExecute["idrouting_commands"].ToString());
            
                if (nSample_ID > 0)
                {
                    ValueName = drCommandsToExecute["CommandValue3"].ToString();
                    WriteLoggEntry((int)Definition.ThorLogWindows.ROUTING, strRoutingPositionEntryLogString + @"command:: deleting worksheet entry `" + ValueName + "`" +
                        " from sample: `" + myHC.GetNameFromID((int)Definition.SQLTables.SAMPLE_ACTIVE, nSample_ID) + "`(id:" + nSample_ID + ")" +
                        " from position: `" + myHC.GetNameFromID((int)Definition.SQLTables.MACHINE_POSITIONS, nMachinePosition_ID) + "`(id:" + nMachinePosition_ID + ")");

                    myRoutingData.DeleteWSEntryFromSampleValuesBySample_IDAndValueName(nSample_ID, ValueName);
                   // myRoutingData.SetCommandActiveOnRoutingCommandsByRoutingCommands_ID(nRouting_Commands_ID, 1);           
                }           
        }

        private void ExecuteCommandForMachine(int nSample_ID, int nSampleType, int nSampleProgram_ID, DataRow drActiveSample, DataRow drCommandsToExecute, string strRoutingPositionEntryLogString)
        {
            int nCommand = -1;
            int nMachine_ID = -1;
            int nMachinePosition_ID = Int32.Parse(drActiveSample["ActualSamplePosition_ID"].ToString());
            nCommand = Int32.Parse(drCommandsToExecute["CommandValue1"].ToString());
            nMachine_ID = Int32.Parse(drCommandsToExecute["CommandValue0"].ToString());

            int nRouting_Commands_ID = Int32.Parse(drCommandsToExecute["idrouting_commands"].ToString());
           
                if (nMachine_ID > 0)
                {

                    int nConnectionType = myRoutingData.GetConnectionType_IDFromConnectionListByMachine_ID(nMachine_ID);
                  //  WriteLoggEntry((int)Definition.ThorLogWindows.ROUTING, " #### nConnectionType:" + nConnectionType.ToString());
                    switch(nConnectionType)
                    {
                        //PLC
                        case (int)Definition.ConnectionTypes.PLC: 
                        int nProgram_ID = -1;
                    
                        int nMachine_List_ID = myHC.GetMachineList_IDFromMachinesByMachine_ID(nMachine_ID);

                        nProgram_ID = myRoutingData.GetProgram_IDFromSampleProgramsMachineProgramsByMachineList_IDSampleTypeSampleProgram_ID(nMachine_List_ID, nSampleType, nSampleProgram_ID);

                        WriteLoggEntry((int)Definition.ThorLogWindows.ROUTING, strRoutingPositionEntryLogString + @"command:: execute Command for Machine: `" +
                        myHC.GetNameFromID((int)Definition.SQLTables.MACHINES, nMachine_ID) + "` (id:" + nMachine_ID + ")" +
                                    " Command: `" + myHC.GetNameFromID((int)Definition.SQLTables.MACHINE_COMMANDS, nCommand) + "`(id:" + nCommand + ")" +
                                    " for Sample: `" + myHC.GetNameFromID((int)Definition.SQLTables.SAMPLE_ACTIVE, nSample_ID) + "`(id:" + nSample_ID + ")" +
                                    " with program: `" + myHC.GetNameFromID((int)Definition.SQLTables.MACHINE_PROGRAMS, nProgram_ID) + "`(id:" + nProgram_ID + ")" +
                                    " from position: `" + myHC.GetNameFromID((int)Definition.SQLTables.MACHINE_POSITIONS, nMachinePosition_ID) + "`(id:" + nMachinePosition_ID + ")");

                        myRoutingData.InsertCommandIntoCommandTable(nMachine_ID, nCommand, nSample_ID, nProgram_ID);
                        // myRoutingData.SetCommandActiveOnRoutingCommandsByRoutingCommands_ID(nRouting_Commands_ID, 1);
                        break;

                        // Magazine
                        case (int)Definition.ConnectionTypes.MAGAZINE: 
                            
                            int nNumber = myRoutingData.GetNumberFromMachineCommandsByMachineCommands_ID(nCommand);
                            int nPos = 1;
                           //Magazine_Driver myMagazine = LabManager.LabManager.
                            WriteLoggEntry((int)Definition.ThorLogWindows.ROUTING, strRoutingPositionEntryLogString + @"command:: execute Command for Magazine: `" +
                            myHC.GetNameFromID((int)Definition.SQLTables.MACHINES, nMachine_ID) + "` (id:" + nMachine_ID + ")" +
                                   " Command: `" + myHC.GetNameFromID((int)Definition.SQLTables.MACHINE_COMMANDS, nCommand) + "`(id:" + nCommand + ")" +
                                   " for Sample: `" + myHC.GetNameFromID((int)Definition.SQLTables.SAMPLE_ACTIVE, nSample_ID) + "`(id:" + nSample_ID + ")" +
                                   " from position: `" + myHC.GetNameFromID((int)Definition.SQLTables.MACHINE_POSITIONS, nMachinePosition_ID) + "`(id:" + nMachinePosition_ID + ")");
                            int nNewSampleID = -1;
                            if (drCommandsToExecute["CommandValue2"].ToString().Length > 0) // if sampleName is given, get the new id from Sample ID
                            {
                                string strTranslateString = drCommandsToExecute["CommandValue2"].ToString();
                                LabManager.TranslateString translater = new LabManager.TranslateString();
                                strTranslateString = translater.TranslateValueString(strTranslateString, nSample_ID);
                                nNewSampleID =  myHC.GetIDFromSampleID(strTranslateString);
                            }
                            else
                            {
                                nNewSampleID = nSample_ID;
                            }
                            nPos = myHC.GetMagazinePosBySampleIDFrimSampleValues(nNewSampleID);
                            Boolean bDone = _parent.SendCommadToMagazine_Driver((nMachine_ID - 1), nNumber, nNewSampleID, nPos, nMachine_ID);
                          
                        break;

                        // TCP-IP
                        case (int)Definition.ConnectionTypes.TCPIP: 

                          int nOrderNumber = myRoutingData.GetNumberFromMachineCommandsByMachineCommands_ID(nCommand);
                          string strTranslateStringTCPIP = null;

                          WriteLoggEntry((int)Definition.ThorLogWindows.ROUTING, strRoutingPositionEntryLogString + @"command:: for TCP/IP: `" +
                            myHC.GetNameFromID((int)Definition.SQLTables.MACHINES, nMachine_ID) + "` (id:" + nMachine_ID + ")" +
                                   " Command: `" + myHC.GetNameFromID((int)Definition.SQLTables.MACHINE_COMMANDS, nCommand) + "`(id:" + nCommand + ")" +
                                   " for Sample: `" + myHC.GetNameFromID((int)Definition.SQLTables.SAMPLE_ACTIVE, nSample_ID) + "`(id:" + nSample_ID + ")" +
                                   " from position: `" + myHC.GetNameFromID((int)Definition.SQLTables.MACHINE_POSITIONS, nMachinePosition_ID) + "`(id:" + nMachinePosition_ID + ")");

                            if (drCommandsToExecute["CommandValue2"].ToString().Length > 0) // if sampleName is given, get the new id from Sample ID
                            {
                                strTranslateStringTCPIP = drCommandsToExecute["CommandValue2"].ToString();
                                LabManager.TranslateString translater = new LabManager.TranslateString();
                                strTranslateStringTCPIP = translater.TranslateValueString(strTranslateStringTCPIP, nSample_ID);
                            }
                            else
                            {
                                WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, strRoutingPositionEntryLogString + "RoutingCommand:: ExecuteCommandForMachine for Sample `" + myHC.GetNameFromID((int)Definition.SQLTables.SAMPLE_ACTIVE, nSample_ID) + "` with ID: " + nSample_ID + " failed - no Command found: " + drCommandsToExecute["CommandValue2"].ToString(), (int)Definition.Message.D_ALARM);                   
                            }
                            
                            // SendCommadToTCPIP(int nNumber, int nSample_ID, string strCommand, int nMachine_ID)
                            _parent.SendCommadToTCPIP(nOrderNumber, nSample_ID, strTranslateStringTCPIP, nMachine_ID);

                        break;
                        
                    }
                }
                else
                {
                    WriteLoggEntry((int)Definition.ThorLogWindows.ROUTING, strRoutingPositionEntryLogString + "RoutingCommand:: execute Command: " + nCommand + " for machine_ID:" + nMachine_ID + "  failed no machine_ID found!", (int)Definition.Message.D_ALARM);
                }
            
        }

        private void ExecuteCreateReservation(int nSample_ID, DataRow drActiveSample, DataRow drCommandsToExecute, string strRoutingPositionEntryLogString)
        {
            int nMachinePosition_ID = Int32.Parse(drActiveSample["ActualSamplePosition_ID"].ToString());
            string strSampleName = null;
            int nProgramType_ID = 0;    // reservation type
            int nPosition_ID = -1;
          
            strSampleName = myThorDef.ReservationPrefix + myHC.GetNameFromID((int)Definition.SQLTables.SAMPLE_ACTIVE, nSample_ID); 
            Int32.TryParse(drCommandsToExecute["CommandValue0"].ToString(), out nPosition_ID);

            WriteLoggEntry((int)Definition.ThorLogWindows.ROUTING, strRoutingPositionEntryLogString + @"command:: create reservation" +
                       ": `" + strSampleName +
                       " on position: `" + myHC.GetNameFromID((int)Definition.SQLTables.MACHINE_POSITIONS, nPosition_ID) + "`(id:" + nPosition_ID + ")" +
                       " from position: `" + myHC.GetNameFromID((int)Definition.SQLTables.MACHINE_POSITIONS, nMachinePosition_ID) + "`(id:" + nMachinePosition_ID + ")"+
                       " for sample `" + myHC.GetNameFromID((int)Definition.SQLTables.SAMPLE_ACTIVE, nSample_ID) + "` with id(" + nSample_ID + ")");

                 myRoutingData.InsertSampleInSample_Reservation(nProgramType_ID, strSampleName, nPosition_ID, nSample_ID);
           
        }

        private void ExecuteDeleteReservation(int nSample_ID, DataRow drActiveSample, DataRow drCommandsToExecute, string strRoutingPositionEntryLogString)
        {
            int nMachinePosition_ID = Int32.Parse(drActiveSample["ActualSamplePosition_ID"].ToString());
            string strSampleName = null;
            int nPosition_ID = -1;

            strSampleName = myHC.GetNameFromID((int)Definition.SQLTables.SAMPLE_ACTIVE, nSample_ID);
             nPosition_ID = Int32.Parse(drCommandsToExecute["CommandValue0"].ToString());


             WriteLoggEntry((int)Definition.ThorLogWindows.ROUTING, strRoutingPositionEntryLogString + @"command:: delete reservation" +
                    ": `" + strSampleName +
                    " on position: `" + myHC.GetNameFromID((int)Definition.SQLTables.MACHINE_POSITIONS, nPosition_ID) + "`(id:" + nPosition_ID + ")" +
                    " from position: `" + myHC.GetNameFromID((int)Definition.SQLTables.MACHINE_POSITIONS, nMachinePosition_ID) + "`(id:" + nMachinePosition_ID + ")");

             myRoutingData.DeleteReservationOnSample_ReservationByPosition_ID(nPosition_ID);

        }


    }

    
}
