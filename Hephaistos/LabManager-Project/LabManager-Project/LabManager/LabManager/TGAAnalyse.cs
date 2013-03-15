using System;
using Logging;
using Definition;
using MySQL_Helper_Class;
using LabManager;
using System.Timers;

namespace TGA
{
    class InterfaceAnalyse
    {
        TCPIP_Channel parent_TCPIP_Channel = null;
        Save mySave = new Save("TGA-InterfaceAnalyse");
        Definitions myThorDef = new Definitions();
        MySQL_HelperClass myHC = new MySQL_HelperClass();
       private System.Timers.Timer TGATimer = new System.Timers.Timer();

        private int nMachine_ID = -1;
        private int NewState = -1;
        private int OldState = -2;
        private bool bConnectionEstablished = false;
        bool bAskedAreYouThere = false;
        public static int nTimeout = 10;
        private int[] errorvalue = null;
        private string strGetStatusChanged = null;
        private string strGetWinCCTagChanged = null;
      
        public InterfaceAnalyse(int nMachine_ID, TCPIP_Channel parent_TCPIP_Channel = null)
        {
            if (parent_TCPIP_Channel != null)
            {
                this.parent_TCPIP_Channel = parent_TCPIP_Channel;
            }
            this.nMachine_ID = nMachine_ID;

            errorvalue = new int[5] {-1,-1,-1,-1,-1};  //general + up to 4 furnaces

            SetStateValue((int)Definition.BitNumber.OFFLINE);
            try
            {
                parent_TCPIP_Channel.WriteStream("ARE_YOU_THERE@");
            }
            catch (System.ObjectDisposedException) { bConnectionEstablished = false; }
            catch { }

            // Hook up the Elapsed event for the timer.
            TGATimer.Elapsed += new ElapsedEventHandler(TimerEventProcessor);

            // Set the Interval to 1 seconds (1000 milliseconds).
            TGATimer.Interval = 1000;
            TGATimer.Enabled = true;
           
        }


        private void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            if (parent_TCPIP_Channel.GetInfo(1, -1) == 0)
            {
                NewState = (int)Definition.BitNumber.OFFLINE;
                SetStateValue((int)Definition.BitNumber.OFFLINE);
                bConnectionEstablished = false;
                return;
            }

            if (nTimeout >= 0) { nTimeout--; }
            if ((nTimeout == 2 || !bConnectionEstablished) )
            {
                bAskedAreYouThere = false;
                if (parent_TCPIP_Channel != null && !bAskedAreYouThere && nTimeout != -1)
                {
                    try
                    {
                        parent_TCPIP_Channel.WriteStream("ARE_YOU_THERE@");
                    }
                    catch (System.ObjectDisposedException) { bConnectionEstablished = false; }
                    catch  {  }
                    bAskedAreYouThere = true;
                    bConnectionEstablished = true;
                }
            }

            if (nTimeout <= 0)
            {
                bConnectionEstablished = false;
                bAskedAreYouThere = false;
                if (nTimeout == 0)
                {
                    SetStateValue((int)Definition.BitNumber.OFFLINE);
                }
            }
            else { }
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

                    // TGA_DATA@SI=<SID>@SAMPLE_WEIGHT=<TGASampleWeight>@CupWeight=<TGACrucibleWeight>@DosingStation=<DosingStation>@ 
                case 10:
                    parent_TCPIP_Channel.WriteStream(strCommand);
                    parent_TCPIP_Channel.WriteLoggEntry((int)Definition.ThorLogWindows.COMMUNICATION, "SEND: " + strCommand, (int)Definition.Message.D_SEND);
                     
                    break;

                case 11:    // insert into sample list
                    int newID = -1;
                    newID = myHC.InsertSampleToSample_TGABySample_ID(nSample_ID,nMachine_ID);
                    mySave.InsertRow((int)Definition.Message.D_MESSAGE, "insert entry into `sample_tga´ (id:" + newID + ") from sample " + myHC.GetNameFromID((int)Definition.SQLTables.SAMPLE_ACTIVE, nSample_ID) + " with id:" + nSample_ID + "");
                    break;

                case 12:    // delete from Sample list
                    myHC.DeleteSampleFromSample_TGABySample_ID(nSample_ID);
                    break;

                case 20:    // send BRING_SAMPLE_OK=0
                    parent_TCPIP_Channel.WriteStream("BRING_SAMPLE_OK=0");
                    parent_TCPIP_Channel.WriteLoggEntry((int)Definition.ThorLogWindows.COMMUNICATION, "SEND: BRING_SAMPLE_OK=0", (int)Definition.Message.D_SEND);
                     
                    break;

                case 21:     // send TAKE_SAMPLE_OK=0
                    parent_TCPIP_Channel.WriteStream("TAKE_SAMPLE_OK=0");
                    parent_TCPIP_Channel.WriteLoggEntry((int)Definition.ThorLogWindows.COMMUNICATION, "SEND: TAKE_SAMPLE_OK=0", (int)Definition.Message.D_SEND);
                  
                    break;

            }

            return ret;
        }

        public string MessageAnalyse(string strMessage)
        {
            string searchForThis = null;
            string strRet = null;
            int nFoundAt = -1;

            nTimeout = 10;

            switch (strMessage)
            {

                case "DISCONNECT@":
                case "DISCONNECT@\0":
                    bConnectionEstablished = false;
                    SetStateValue(-1);
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

            bConnectionEstablished = true;

            searchForThis = "STATUS";
            nFoundAt = strMessage.IndexOf(searchForThis);
            if (nFoundAt >= 0)
            {
                if (strMessage.CompareTo(strGetStatusChanged) != 0)
                {
                    strGetStatusChanged = strMessage;
                    parent_TCPIP_Channel.WriteLoggEntry((int)Definition.ThorLogWindows.COMMUNICATION, "RECEIVED: " + strMessage, (int)Definition.Message.D_RECEIVE);

                    //CheckState(strMessage.Substring(nFoundAt + searchForThis.Length + 1));
                    CheckState(strMessage);
                }
                 bConnectionEstablished = true;
                 nFoundAt = -1;
            }

            searchForThis = "TAKE_SAMPLE_OK";
            nFoundAt = strMessage.IndexOf(searchForThis);
            if (nFoundAt >= 0)
            {
                parent_TCPIP_Channel.WriteLoggEntry((int)Definition.ThorLogWindows.COMMUNICATION, "RECEIVED: " + strMessage, (int)Definition.Message.D_RECEIVE);

                SetTakeSampleOKBit(strMessage);
                nFoundAt = -1;
            }

            searchForThis = "BRING_SAMPLE_OK";
            nFoundAt = strMessage.IndexOf(searchForThis);
            if (nFoundAt >= 0)
            {
                parent_TCPIP_Channel.WriteLoggEntry((int)Definition.ThorLogWindows.COMMUNICATION, "RECEIVED: " + strMessage, (int)Definition.Message.D_RECEIVE);

                SetBringSampleOKBit(strMessage);
                nFoundAt = -1;
            }

            searchForThis = "TGAResult";
            nFoundAt = strMessage.IndexOf(searchForThis);
            if (nFoundAt >= 0)
            {
                parent_TCPIP_Channel.WriteLoggEntry((int)Definition.ThorLogWindows.COMMUNICATION, "RECEIVED: " + strMessage, (int)Definition.Message.D_RECEIVE);

                SetTGAResultValue(strMessage);
                nFoundAt = -1;
            }

            searchForThis = "SETWINCCTAG";
            nFoundAt = strMessage.IndexOf(searchForThis);
            if (nFoundAt >= 0)
            {
                if (strMessage.CompareTo(strGetWinCCTagChanged) != 0)
                {
                    strGetWinCCTagChanged = strMessage;
                    parent_TCPIP_Channel.WriteLoggEntry((int)Definition.ThorLogWindows.COMMUNICATION, "RECEIVED: " + strMessage, (int)Definition.Message.D_RECEIVE);

                    SetWinCCTag(strMessage);
                }
                nFoundAt = -1;
            }

            searchForThis = "ARE_YOU_READY";
            nFoundAt = strMessage.IndexOf(searchForThis);
            if (nFoundAt >= 0)
            {
                parent_TCPIP_Channel.WriteLoggEntry((int)Definition.ThorLogWindows.COMMUNICATION, "RECEIVED: " + strMessage, (int)Definition.Message.D_RECEIVE);

                // for now allways say yes; this must be changed may be
                strRet = "MACHINE_READY_FOR_MOVEMENT=YES";
                nFoundAt = -1;
            }

            strMessage = "";
            return strRet;
        }

        // ---------------------------------------------------------------------------------------------------------
        // AnalyseAlarmMessage Command: @STATUS=<TGA_State>@MESSAGE=blabla1;blabla2;@MsgCode=<GGGG1111222233334444>
        // ---------------------------------------------------------------------------------------------------------
        public void CheckState(string Command)
        {
          //  Console.WriteLine("CheckState:" + Command);

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
                   
                    SetStateValue(NewState);

                    if (strNMessages.Length > 2)
                    {
                        //@MsgCode=<GGGG1111222233334444>
                        AnalyseAlarmMessage(strNMessages[2]);

                    }
                }
            }
            else
            {
                // illegal Status telegram
                mySave.InsertRow((int)Definition.Message.D_ALARM, "illegal telegram from machine with id=" + nMachine_ID + " (telegram:" + strNMessages +")"); 
            }
        }

        // ---------------------------------------------------------------------------------------------------------
        // AnalyseAlarmMessage message: MsgCode=<FFFF1111222233334444>
        // ---------------------------------------------------------------------------------------------------------
        private bool  AnalyseAlarmMessage( string szText)
        {
           // Console.WriteLine("szText:" +szText);
            string[] strCommandAndValue = szText.Split(new Char[] { '=' });
	        string strValue = strCommandAndValue[1];
            int k=0;
            int nLength = strValue.Length;

            if (strCommandAndValue[0].StartsWith("MsgCode"))
            {
                if (nLength < 8)
                {
                    mySave.InsertRow((int)Definition.Message.D_ALARM, "status telegram from machine with id=" + nMachine_ID + " to short!  (telegram:" + szText + ")");
                }

                // splitt  FFFF0002000300040005 in FFFF,0002,0003,0004,0005
                // and write it to errorbits array
                for (int i = 0; i < nLength; )
                {
                    WriteErrorsToWinCC(0, k);
                    errorvalue[k] = (int)Convert.ToUInt32(strValue.Substring(i, 4), 16);
                    if (errorvalue[k] >= 0)
                    {
                        WriteErrorsToWinCC(errorvalue[k], k);
                    }
                    //  Console.WriteLine(errorvalue[k] + " " );
                    i = i + 4;
                    k++;

                }
            }
            else
            {
                mySave.InsertRow((int)Definition.Message.D_ALARM, "status telegram from machine with id=" + nMachine_ID + " wrong!  (telegram:" + szText + ")");
            }

		      return true;
        }

        // write the errobits to WinCC variables: <MACHINE_ID>_Eroorbits_0, <MACHINE_ID>_Eroorbits_1, ...
        private void WriteErrorsToWinCC(int nError, int nErrorWordCount)
        {
            myHC.WriteWinCCTagFromSampleValuesByValueName(nMachine_ID + "_Errorbits_" + nErrorWordCount, nError.ToString(), 5);
        }

        // set the StateValue in the DB
        private void SetStateValue(int nBitNumber)
        {
          
            if (NewState != OldState)
            {
                OldState = NewState;
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

        // ---------------------------------------------------------------------------------------------------------
        // TakeSampleOK message: TAKE_SAMPLE_OK=<0/1> 
        // ---------------------------------------------------------------------------------------------------------
        private void SetTakeSampleOKBit(string strMessage)
        {
            char cValue;
        
            string[] strNMessages = strMessage.Split(new Char[] { '=' });
            if (strNMessages.Length > 1)
            {

                cValue = strNMessages[1][0];

                if (cValue == '1')
                {
                    myHC.SetStateBitOnMachineStateSignals(nMachine_ID, 1, 1, true);
                 }
                else if (cValue == '0')
                {
                    myHC.SetStateBitOnMachineStateSignals(nMachine_ID, 1, 1, false);
                }
                else
                {
                    mySave.InsertRow((int)Definition.Message.D_ALARM, "TakeSampleOK: telegram for machine with id=" + nMachine_ID + " not correct !  (telegram:" + strMessage + ")"); 
                }
            }
        
        }

        // ---------------------------------------------------------------------------------------------------------
        // BringSampleOK message: BRING_SAMPLE_OK=<0/1> 
        // ---------------------------------------------------------------------------------------------------------
        private void SetBringSampleOKBit(string strMessage)
        {
            char cValue;

            string[] strNMessages = strMessage.Split(new Char[] { '=' });
            if (strNMessages.Length > 1)
            {

                cValue = strNMessages[1][0];

                if (cValue == '1')
                {
                    myHC.SetStateBitOnMachineStateSignals(nMachine_ID, 0, 1, true);
                }
                else if (cValue == '0')
                {
                    myHC.SetStateBitOnMachineStateSignals(nMachine_ID, 0, 1, false);
                }
                else
                {
                    mySave.InsertRow((int)Definition.Message.D_ALARM, "BringSampleOK: telegram for machine with id=" + nMachine_ID + " not correct !  (telegram:" + strMessage + ")");
                }
            }

        }

        // ---------------------------------------------------------------------------------------------------------
        // TGAResult message: TGAResult@SampleID=<Sample ident>@Value=<TGA Value>@DryWeightFactor=<DryWeightFactor> 
        // ---------------------------------------------------------------------------------------------------------
        private void SetTGAResultValue(string strMessage)
        {
            string[] strNMessages = strMessage.Split(new Char[] { '@' });
            string[] strCommandAndValue;
            string strSampleID = null;
            string strValue = null;
            string DryWeightFactor = null;

            if (strNMessages.Length >= 3)
            {
                for (int i = 0; i < strNMessages.Length; i++)
                {
                     // command and Values
                     strCommandAndValue = strNMessages[i].Split(new Char[] { '=' });

                    if(strCommandAndValue[0].Equals("SampleID", StringComparison.OrdinalIgnoreCase))
                    {
                        strSampleID = strCommandAndValue[1];
                    }
                    if (strCommandAndValue[0].Equals("Value", StringComparison.OrdinalIgnoreCase))
                    {
                        strValue = strCommandAndValue[1];
                    }
                    if (strCommandAndValue[0].Equals("DryWeightFactor", StringComparison.OrdinalIgnoreCase))
                    {
                        DryWeightFactor = strCommandAndValue[1];
                    }
                }
            
            }
            else
            {
                mySave.InsertRow((int)Definition.Message.D_ALARM, "TGAResult: telegram for machine with id=" + nMachine_ID + " not correct !  (telegram:" + strMessage + ")");    
            }
            // write the values to the table "sample_values"
            if (strSampleID.Length > 2 && strValue.Length > 1)
            {
                try
                {
                    myHC.InsertWSEntryIntoSampleValuesBySampleIDAndValueName(strSampleID, "TGA_LOI", strValue);
                    myHC.InsertWSEntryIntoSampleValuesBySampleIDAndValueName(strSampleID, "TGA_DryWeightFactor", DryWeightFactor);
                }
                catch (Exception ex)
                {
                    mySave.InsertRow((int)Definition.Message.D_ALARM, "SetTGAResultValue: " + ex.ToString());
                }
            }
          //  Console.WriteLine("::SetTGAResultValue: strSampleID: " + strSampleID + " strValue: " + strValue + " DryWeightFactor:" + DryWeightFactor);
        }

        // ---------------------------------------------------------------------------------------------------------------------------
        // TGAResult message: SETWINCCTAG@TAG=<Keyname where to store the data>@VALUE=<Value of the data>@FORMAT=<Format of the value> 
        // ---------------------------------------------------------------------------------------------------------------------------
        private void SetWinCCTag(string strMessage)
        {
           
            string[] strNMessages = strMessage.Split(new Char[] { '@' });
            string[] strCommandAndValue;
            string strTagName = null;
            string strValue = null;
            int nType = -1;

            if (strNMessages != null)
            {
                if (strNMessages.Length >= 3)
                {
                    for (int i = 0; i < strNMessages.Length; i++)
                    {
                        // command and Values
                        strCommandAndValue = strNMessages[i].Split(new Char[] { '=' });

                        if (strCommandAndValue[0].Equals("TAG", StringComparison.OrdinalIgnoreCase))
                        {
                            strTagName = strCommandAndValue[1];
                            strTagName = strTagName.Replace("\"", "");
                        }
                        if (strCommandAndValue[0].Equals("VALUE", StringComparison.OrdinalIgnoreCase))
                        {
                            strValue = strCommandAndValue[1];
                            strValue = strValue.Replace("\"", "");
                        }
                        if (strCommandAndValue[0].Equals("FORMAT", StringComparison.OrdinalIgnoreCase))
                        {
                            switch (strCommandAndValue[1])
                            {
                                case "INT":
                                case "int":
                                    nType = 5;
                                    break;

                                case "REAL":
                                case "real":
                                    nType = 8;
                                    break;

                                case "CHAR":
                                case "char":
                                    nType = 10;
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    mySave.InsertRow((int)Definition.Message.D_ALARM, "SetTWinCCTag: telegram for machine with id=" + nMachine_ID + " not correct !  (telegram:" + strMessage + ") count of splitted strings " + strNMessages.Length);
                }
            }
            else
            {
                mySave.InsertRow((int)Definition.Message.D_ALARM, "SetTWinCCTag: telegram for machine with id=" + nMachine_ID + " is null  (telegram:" + strMessage + ")");
            }
         //   Console.WriteLine("::SetTWinCCTag: strTagName: " + strTagName + " strValue: " + strValue + " nType:" + nType);
      
            // write the values to the table "sample_values"
            if (strTagName.Length > 1 && strValue.Length >= 1 && nType != -1)
            {
                try
                {
                    myHC.WriteWinCCTagFromSampleValuesByValueName(strTagName, strValue, nType);
                }
                catch (Exception ex)
                {
                    mySave.InsertRow((int)Definition.Message.D_ALARM, "SetTWinCCTag: " + ex.ToString());
                }
            }
            else
            {
                mySave.InsertRow((int)Definition.Message.D_ALARM, "SetWinCCTag: telegram for machine with id=" + nMachine_ID + " not correct !  (telegram:" + strMessage + ")");    
            }
         }
        

       
 
    }

}
