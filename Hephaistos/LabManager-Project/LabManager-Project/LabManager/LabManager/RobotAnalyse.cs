using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Logging;
using Definition;
using cs_IniHandlerDevelop;
using MySQL_Helper_Class;
using System.Windows.Forms;
using LabManager;
using System.Timers;

namespace Robot
{
    class InterfaceAnalyse
    {
        TCPIP_Channel parent_TCPIP_Channel = null;
        Save mySave = new Save("Robot-InterfaceAnalyse");
        Definitions myThorDef = new Definitions();
        MySQL_HelperClass myHC = new MySQL_HelperClass();
        private System.Timers.Timer RobotTimer = new System.Timers.Timer();
        private int NewState = 0;
        private int OldState = -1;
        private int nMachine_ID = -1;
        private bool bAlarmIsOn = false;
        private bool bConnectionEstablished = false;
       
        public InterfaceAnalyse(int nMachine_ID, TCPIP_Channel parent_TCPIP_Channel = null)
        {
            if (parent_TCPIP_Channel != null)
            {
                this.parent_TCPIP_Channel = parent_TCPIP_Channel;
            }
            this.nMachine_ID = nMachine_ID;

                       
            // Hook up the Elapsed event for the timer.
            RobotTimer.Elapsed += new ElapsedEventHandler(TimerEventProcessor);

            // Set the Interval to 1 seconds (1000 milliseconds).
            RobotTimer.Interval = 1000;
            RobotTimer.Enabled = true;
            
        }


        private void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
             
              
            if (parent_TCPIP_Channel.GetInfo(1, -1) == 0)
            {
                NewState = (int)Definition.BitNumber.OFFLINE;
                if (NewState != OldState)
                {
                    OldState = NewState;
                    SetStateValue(NewState); 
                    bConnectionEstablished = false;
                }
               
                return;
            }
            else
            {
                if (!bAlarmIsOn)
                {
                    NewState = (int)Definition.BitNumber.READYAUTOMATIC;
                    bConnectionEstablished = true;
                    if (NewState != OldState)
                    {
                        OldState = NewState;
                        SetStateValue(NewState);
                    }
                }
            }

            
         /*   if (nTimeout <= 0)
            {
                bConnectionEstablished = false;
                bAskedAreYouThere = false;
                if (nTimeout == 0)
                {
                   // SetStateValue((int)Definition.BitNumber.OFFLINE);
                }
            }
            else { }*/
       //     string t = bConnectionEstablished ? "true" : "false"; 
         //   Console.WriteLine("nTimeout:" + nTimeout + "- " +t );
        }

        //----------------------------------------------------------------------------------
        // sending commands from routing
        //----------------------------------------------------------------------------------
        public int SendCommandFromRouting(int nOrderNumber, string strCommand, int nSample_ID, int nMachine_ID)
        {
            int ret = -1;

           
            switch (nOrderNumber)
            {
                case 0: // set state offline (shutdown)
                    NewState = (int)Definition.BitNumber.OFFLINE;
                    SetStateValue(NewState); 
                    break;

                case 10:
                    if (strCommand != null)
                    {
                        parent_TCPIP_Channel.WriteStream(strCommand);
                        parent_TCPIP_Channel.WriteLoggEntry((int)Definition.ThorLogWindows.COMMUNICATION, "SEND: OrderNumber" + nOrderNumber + " - command: " + strCommand, (int)Definition.Message.D_SEND); 
                    }
                    break;

            }

            return ret;
        }

        public string MessageAnalyse(string strMessage)
        {
           
            string strRet = null;
            string searchForThis = null;
            int nFoundAt = -1;

            parent_TCPIP_Channel.WriteLoggEntry((int)Definition.ThorLogWindows.COMMUNICATION, "RECEIVED: " + strMessage, (int)Definition.Message.D_RECEIVE);
            
            switch (strMessage)
            {

                case "DISCONNECT@":
                case "DISCONNECT@\0":
                    bConnectionEstablished = false;
                    // SetStateValue((int)Definition.BitNumber.OFFLINE);
                    return "DISCONNECT@";
                 
   
                case "ARE_YOU_THERE":
                case "ARE_YOU_THERE@":
                case "ARE_YOU_THERE@\0":
                    bConnectionEstablished = true;
                    return "I_AM_HERE@";


                case "I_AM_HERE":
                case "I_AM_HERE@":
                case "I_AM_HERE@\0":
                     bConnectionEstablished = true;
                     return "";
            }

            searchForThis = "@ERRORMESSAGE=";
            nFoundAt = strMessage.IndexOf(searchForThis);
            if (nFoundAt >= 0)
            {
                AnalyseAlarmMessage(strMessage.Substring(nFoundAt + searchForThis.Length));
                bConnectionEstablished = true;
                nFoundAt = -1;
            }

            searchForThis = "@WARNINGMESSAGE=";
            nFoundAt = strMessage.IndexOf(searchForThis);
            if (nFoundAt >= 0)
            {
                AnalyseAlarmMessage(strMessage.Substring(nFoundAt + searchForThis.Length), true);
                bConnectionEstablished = true;
                nFoundAt = -1;
            }

            searchForThis = "@SAMPLEID=";
            nFoundAt = strMessage.IndexOf(searchForThis);
            if (nFoundAt >= 0)
            {
                ShiftSample(strMessage);
                bConnectionEstablished = true;
                nFoundAt = -1;
            }

            searchForThis = "@POSITIONS";  
            nFoundAt = strMessage.IndexOf(searchForThis);
            if (nFoundAt >= 0)
            {
                CreatePositions(strMessage.Substring(nFoundAt + searchForThis.Length));
                bConnectionEstablished = true;
                nFoundAt = -1;
            }

            searchForThis = "@PALLET@";
            nFoundAt = strMessage.IndexOf(searchForThis);
            if (nFoundAt >= 0)
            {
                CreateMagazinePositions(strMessage.Substring(nFoundAt + searchForThis.Length));
                bConnectionEstablished = true;
                nFoundAt = -1;
            }


            
            return strRet;
        }

        private bool AnalyseAlarmMessage(string szText, bool bMessage=false)
        {
           // Console.WriteLine("::AnalyseAlarmMessage: szText:" + szText);
            string[] strCommandAndValue = szText.Split(new Char[] { '@' });
            string strValue = null;
            if (strCommandAndValue.Length > 0)
            {
                 strValue = strCommandAndValue[0];
            }
            
            if ( strValue==null)
            {
                WriteErrorsToWinCC("", 0, bMessage);
                bAlarmIsOn = false;
            }
            else
            {
                if (strValue.Length<=0)
                {
                    WriteErrorsToWinCC("", 0, bMessage);
                    bAlarmIsOn = false;
                }
                else
                {
                    WriteErrorsToWinCC(szText, 1, bMessage);

                     NewState = (int)Definition.BitNumber.BREAKDOWN;
                     if (NewState != OldState)
                     {
                         OldState = NewState;
                         bAlarmIsOn = true;
                         SetStateValue(NewState);
                     }
                }
            }
             

            return true;
        }

        // write the errobits to WinCC variables: <MACHINE_ID>_Errorbits_0 and the text to <MACHINE_ID>_ErrorText
        private void WriteErrorsToWinCC(string strValue, int nErrorBit, bool bMessage)
        {
            // set the errortext
            if (!bMessage)
            {
                myHC.WriteWinCCTagFromSampleValuesByValueName(nMachine_ID + "_Errortext", strValue, 10);
            }
            else
            {
                myHC.WriteWinCCTagFromSampleValuesByValueName(nMachine_ID + "_Messagetext", strValue, 10);
            }
            // set the errorbit

            if (!bMessage)
            {
                myHC.WriteWinCCTagFromSampleValuesByValueName(nMachine_ID + "_Errorbits_0", nErrorBit.ToString(), 5);
            }
            else
            {
                myHC.WriteWinCCTagFromSampleValuesByValueName(nMachine_ID + "_Errorbits_1", nErrorBit.ToString(), 5);
            }
        }

        private void CreatePositions(string strMessage)
        {

            string[] strPositions = strMessage.Split(new Char[] { '@' });
            string[] strPosAndValue;
            string PositionName = null;
            string PositionNumber = null;

            if (strPositions.Length >= 1)
            {
                for (int i = 0; i < strPositions.Length; i++)
                {
                    // command and Values
                    strPosAndValue = strPositions[i].Split(new Char[] { '=' });
                    try
                    {
                        PositionName = strPosAndValue[0];
                        PositionNumber = strPosAndValue[1];

                        if (PositionNumber != null && PositionName != null && nMachine_ID > 0)
                        {
                            // check the "machine_positions" table
                            myHC.CreateRobotPosition(nMachine_ID, PositionNumber, PositionName);
                        }
                        else
                        {
                            mySave.InsertRow((int)Definition.Message.D_ALARM, "CreatePositions: values for position not correct: nMachine_ID:" + nMachine_ID + " PositionNumber:" + PositionNumber + " PositionName:" + PositionName);
                        }
                    }
                    catch { }
                }

            }
            else
            {
                mySave.InsertRow((int)Definition.Message.D_ALARM, "CreatePositions: telegram for machine with id=" + nMachine_ID + " not correct !  (telegram:" + strMessage + ")");
            }

        }

        private void CreateMagazinePositions(string strMessage)
        {

            string[] strPallets = strMessage.Split(new string[] { "@PALLET@" },StringSplitOptions.RemoveEmptyEntries);
            string[] strPalletValues;
            string[] strNameAndValue;
            string PositionName = null;
            string PositionNumber = null;
            string DimensionX = null;
            string DimensionY = null;

            if (strPallets.Length >= 1)
            {
                for (int i = 0; i < strPallets.Length; i++)
                {
                    strPalletValues = strPallets[i].Split(new Char[] { ';' });

                    for (int nValues = 0; nValues < 3; nValues++)
                    {
                        // Name 
                        strNameAndValue = strPalletValues[nValues].Split(new Char[] { '=' });
                        if (nValues == 0)
                        {
                            PositionName = strNameAndValue[0];
                            PositionNumber = strNameAndValue[1];
                        }
                        else
                        {
                            if (strNameAndValue[0] == "X" || strNameAndValue[0] == " X")
                            {
                                DimensionX = strNameAndValue[1];
                            }
                            if (strNameAndValue[0] == "Y" || strNameAndValue[0] == " Y")
                            {
                                DimensionY = strNameAndValue[1];
                            }
                        }
                    }

                    try
                    {

                        if (PositionName != null && PositionNumber != null && DimensionX != null && DimensionY != null)
                        {
                            // check the "machine_positions" table
                            myHC.CreateRobotMagazinePosition(PositionName, PositionNumber, DimensionX, DimensionY);
                        }
                        else
                        {
                            mySave.InsertRow((int)Definition.Message.D_ALARM, "CreateMagazinePositions: values for pallet not correct! - PositionName:" + PositionName + " PositionNumber:" + PositionNumber + " DimensionX:" + DimensionX + " DimensionY:" + DimensionY);
                        }
                    }
                    catch { }
                }

            }
            else
            {
                mySave.InsertRow((int)Definition.Message.D_ALARM, "CreatePositions: telegram for machine with id=" + nMachine_ID + " not correct !  (telegram:" + strMessage + ")");
            }

        }

        private void ShiftSample(string strMessage)
        {

            string[] strNMessages = strMessage.Split(new Char[] { '@' });
            string[] strNameAndValue;
            string strSampleID = null;
            int nPosition = -1;
            int nPalletNumber = -1;
            int nMachinePosition = -1;
          
            if (strNMessages.Length >= 2)
            {
                for (int i = 0; i < strNMessages.Length; i++)
                {
                    // name and value
                    strNameAndValue = strNMessages[i].Split(new Char[] { '=' });
                    try
                    {
                        if (strNameAndValue[0].Equals("SAMPLEID", StringComparison.OrdinalIgnoreCase))
                        {
                            strSampleID=strNameAndValue[1];
                        }

                        if (strNameAndValue[0].Equals("SAMPLEPOSITION", StringComparison.OrdinalIgnoreCase))
                        {
                            if (strNameAndValue[1].Equals("InRobot", StringComparison.OrdinalIgnoreCase))
                            {
                                if (strNameAndValue[1].Length > 0)
                                {
                                    nMachinePosition = myHC.GetPostionIDFROMMachinepositionsByMachine_IDAndName(nMachine_ID, strNameAndValue[1]);
                                    if (nMachinePosition > 0)
                                    {
                                        myHC.UpdateActualSamplePosOnSampleActiveBySampleID(strSampleID, nMachinePosition);
                                        return;
                                    }
                                    else
                                    {
                                        mySave.InsertRow((int)Definition.Message.D_ALARM, "RobotAnalyse::Shiftsample: no nMachinePosition for 'INROBOT' found (" + strMessage + ")");
                                    }
                                }
                                else 
                                {
                                    mySave.InsertRow((int)Definition.Message.D_ALARM, "RobotAnalyse::Shiftsample: no name for samplepos found (" + strMessage + ")");
                                }
                            }
                            else //if SAMPLEPOS is a number
                            {
                                Int32.TryParse(strNameAndValue[1], out nPosition);
                            }
                        }

                        if (strNameAndValue[0].Equals("PALLET", StringComparison.OrdinalIgnoreCase))
                        {
                            Int32.TryParse(strNameAndValue[1], out nPalletNumber);
                        }
                    }
                    catch(Exception ex)
                    {
                        mySave.InsertRow((int)Definition.Message.D_ALARM, "RobotAnalyse::Shiftsample: Exception: " + ex.ToString() + "");
                    }

                }

                if (nPosition >= 0 && strSampleID != null && nPalletNumber==-1) // Position
                {
                    nMachinePosition = myHC.GetPostionIDFROMMachinepositionsByMachine_IDAndPosNumber(nMachine_ID, nPosition);
                    if (nMachinePosition > 0)
                    {
                        myHC.UpdateActualSamplePosOnSampleActiveBySampleID(strSampleID, nMachinePosition);
                    }
                    else
                    {
                        mySave.InsertRow((int)Definition.Message.D_ALARM, "RobotAnalyse::Shiftsample: no idmachine_positions found");
                    }
                }
                else if (nPalletNumber >= 0) // Magazine
                {
                    if (!myHC.UpdateActualPalletPosOnSampleActiveBySampleID(strSampleID, nPalletNumber, nPosition))
                    {
                        parent_TCPIP_Channel.WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, "RobotAnalyse::Shiftsample: an error occurred - see logging for more details! Message was: " + strMessage, (int)Definition.Message.D_ALARM);
                    }
                }
            }
            else
            {
                mySave.InsertRow((int)Definition.Message.D_ALARM, "RobotAnalyse::Shiftsample: telegram for machine with id=" + nMachine_ID + " not correct !  (telegram:" + strMessage + ")");
            }

        }


        // state is done by digital I/O from robot to PLC and than to WinCC
        /*
        // ---------------------------------------------------------------------------------------------------------
        // AnalyseAlarmMessage Command: @STATUS=<STATEVALUE>
        // ---------------------------------------------------------------------------------------------------------
        public void CheckState(string Command)
        {
            //Console.WriteLine("CheckState:" + Command);

            string strCommand = null;
            string strValue = null;
        
            string[] strNMessages = Command.Split(new Char[] { '@' });
        
            if (strNMessages.Length > 1)
            {
                string strMessage = strNMessages[0];

                string[] strCommandAndValue = strMessage.Split(new Char[] { '=' });
            
                int nStateValue = -1;
                bool bReadStateValue = false;

                if (strCommandAndValue.Length >= 1)
                {
                    strCommand = strCommandAndValue[0];
                    strValue = strCommandAndValue[1];
                    if (mySave.DEBUG_MODE) { mySave.InsertRow((int)Definition.Message.D_DEBUG, "Command=" + Command + " strCommand=" + strCommand + "  - value=" + strValue); }
                    try
                    {
                        bReadStateValue = Int32.TryParse(strValue, out nStateValue);
                    }
                    catch (Exception ex) { mySave.InsertRow((int)Definition.Message.D_ALARM, "can not parse state value from string=" + Command + " StateValue=" + nStateValue + "\r\n" + ex.ToString()); }

                    switch (nStateValue)
                    {
                        case 1: // ready
                            NewState = (int)Definition.BitNumber.READYAUTOMATIC;
                            break;
                        case 2: // manual
                            NewState = (int)Definition.BitNumber.READYMANUAL;
                            break;
                        case 4: // warning
                            NewState = (int)Definition.BitNumber.WARNING;
                            break;
                        case 8: // breakdown
                            NewState = (int)Definition.BitNumber.BREAKDOWN;
                           
                            break;
                        case 16: // offline
                            NewState = (int)Definition.BitNumber.OFFLINE;
                            break;
                        case 32:    //Calibration
                        case 64:    //Control
                            NewState = (int)Definition.BitNumber.CALIBRATION;
                            break;
                    }
                    //TODO: test evtl. wieder raus nehemen
                   // AnalyseAlarmMessage(strNMessages[1], 2);

                    SetStateValue(NewState);
                }
            }
            else
            {
                // illegal Status telegram
                mySave.InsertRow((int)Definition.Message.D_ALARM, "illegal telegram from machine with id=" + nMachine_ID + " (telegram:" + strNMessages +")"); 
            }
        }
        */
       


        // set the StateValue in the DB
        private void SetStateValue(int nBitNumber)
        {
          //  string t = bConnectionEstablished ? "true" : "false";
          //  Console.WriteLine("SetStateValue: nTimeout:" + nTimeout + " nBitNumber:" + nBitNumber + " " + t);

            if (bConnectionEstablished)
            {
                myHC.SetStateValueOnMachineStateSignals(nMachine_ID, nBitNumber);
            }
            else
            {
                myHC.SetStateValueOnMachineStateSignals(nMachine_ID, (int)Definition.BitNumber.OFFLINE);
            }
        }

           

       
 
    }

}
