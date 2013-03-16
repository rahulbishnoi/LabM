using System;
using Logging;
using Definition;
using MySQL_Helper_Class;
using LabManager;
using System.Timers;

namespace RobotRemoteControl
{
    class InterfaceAnalyse
    {
       TCPIP_Channel parent_TCPIP_Channel = null;
       Save mySave = new Save("RobotRemoteControl-InterfaceAnalyse");
       Definitions myThorDef = new Definitions();
       MySQL_HelperClass myHC = new MySQL_HelperClass();
       private System.Timers.Timer RobotRCTimer = new System.Timers.Timer();
       private int nMachine_ID = -1;
       private int nGripperState = -1;
       private bool bConnectionEstablished = false;
       private bool bLogin = false;
       //Teach/Auto/Warning/SError/Safeguard/EStop/Error/Paused/Running/Ready
       private bool bTeach = false;
       private bool bAuto = false;
       private bool bWarning = false;
       private bool bSError = false;
       private bool bSafeguard = false;
       private bool bEStop = false;
       private bool bError = false;
       private bool bPaused = false;
       private bool bRunning = false;
       private bool bReady = false;
       private bool bGrippedWrong = false;
       private bool bStartedProgram = false;
       private bool bLoginSuccess = false;
       private string strGetStatusChanged  = null;
       private string strGetIOWordChanged = null;
       private string strGetErrorFeedbackChanged = null;
       private int NewState = -1;
       private int OldState = -2;

       public static int nTimeout = 1;

        public InterfaceAnalyse(int nMachine_ID, TCPIP_Channel parent_TCPIP_Channel = null)
        {
            if (parent_TCPIP_Channel != null)
            {
                this.parent_TCPIP_Channel = parent_TCPIP_Channel;
            }
            this.nMachine_ID = nMachine_ID;

            SetAlarmMessages();
            SetStateValue((int)Definition.BitNumber.OFFLINE);
       
            // Hook up the Elapsed event for the timer.
            RobotRCTimer.Elapsed += new ElapsedEventHandler(TimerEventProcessor);

            // Set the Interval to 1 seconds (1000 milliseconds).
            RobotRCTimer.Interval = 1000;
            RobotRCTimer.Enabled = true;

            
        }


        private void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            
            if (parent_TCPIP_Channel.GetInfo(1, -1) <= 0) {
                NewState = (int)Definition.BitNumber.OFFLINE;
                SetAlarmMessages();
                if (NewState != OldState)
                {
                    SetStateValue((int)Definition.BitNumber.OFFLINE); bConnectionEstablished = false;
                }
                
                return; 
            }

            if (nTimeout >= 0) { nTimeout--; }
            if ((nTimeout == 3 || !bConnectionEstablished) )
            {
               
                if (parent_TCPIP_Channel != null && nTimeout != -1)
                {
                   
                    try
                    {
                    //    parent_TCPIP_Channel.WriteStream("$GetStatus");
                    }
                    catch (System.ObjectDisposedException) { bConnectionEstablished = false; }
                    catch  {  }
                    
                    bConnectionEstablished = true;
                }
            }
            
            if (!bLogin && nTimeout == 0)
            {
               
                //Login
                parent_TCPIP_Channel.WriteStream("$Login");
              
                parent_TCPIP_Channel.WriteLoggEntry((int)Definition.ThorLogWindows.COMMUNICATION, "SEND: " + "$Login and $Reset", (int)Definition.Message.D_SEND);
            
                bLogin = true;

                // reset
                 SendCommand(102, "", -1, nMachine_ID);

            }

            if (nTimeout <= 0)
            {
                bConnectionEstablished = false;
                bLogin = false;
                if (nTimeout == 0)
                {
                    NewState = (int)Definition.BitNumber.OFFLINE;
                    SetStateValue(NewState);
                    nTimeout = 10;
                    strGetStatusChanged = null;
                    strGetIOWordChanged = null;
                    SetAlarmMessages();
                }
                
            }
            else
            { // get status 
                if (bLoginSuccess)
                {
                    // get status
                    SendCommand(1, "", -1, nMachine_ID);
                   
                }
            }

           // string t = bConnectionEstablished ? "true" : "false"; 
          //  Console.WriteLine("nTimeout:" + nTimeout + "- " +t );
            // start program
            if (bLogin & !bStartedProgram && bLoginSuccess)
            {
               
                // start
                SendCommand(101, "", -1, nMachine_ID);
                bStartedProgram = true;
            }

          
            //get status bits word 0 (gripper etc.)
            if (bLogin && bLoginSuccess)
            {
                SendCommand(10, "", -1, nMachine_ID);
            }

           
         
        }

        //----------------------------------------------------------------------------------
        // sending commands from routing
        //----------------------------------------------------------------------------------
        public int SendCommand(int nOrderNumber, string strCommand, int nSample_ID, int nMachine_ID)
        {
            int ret = -1;

            if (!bConnectionEstablished) {
                mySave.InsertRow((int)Definition.Message.D_MESSAGE, "RoboterRemoteControl::SendCommand: error tried to start command no:" + nOrderNumber + "(command:" +strCommand+ ") while connection not established");
                return -2; 
            }

            if (!bLogin) { return -3; }

            switch (nOrderNumber)
            {
                case 0: // set state offline (shutdown)
                    NewState = (int)Definition.BitNumber.OFFLINE;
                    SetStateValue(NewState);
                    break;

                    // get status
                case 1:
                    parent_TCPIP_Channel.WriteStream("$GetStatus");
                    break;

                 // Get IO Word 0
                case 10:
                    parent_TCPIP_Channel.WriteStream("$GetIOWord,0");
                    
                    break;

                    //  send string
                case 100:
                    if (strCommand != null)
                    {
                        parent_TCPIP_Channel.WriteLoggEntry((int)Definition.ThorLogWindows.COMMUNICATION, "SEND: " + strCommand, (int)Definition.Message.D_SEND);
                        parent_TCPIP_Channel.WriteStream(strCommand);
                       // nGetNewIOState = -1;
                    }
                    break;

                // send start command
                case 101:
                    parent_TCPIP_Channel.WriteLoggEntry((int)Definition.ThorLogWindows.COMMUNICATION, "SEND: OrderNumber" + nOrderNumber + " ($Start,0)", (int)Definition.Message.D_SEND); 
                    parent_TCPIP_Channel.WriteStream("$Start,0");

                    break;

                // send reset command
                case 102:
                    parent_TCPIP_Channel.WriteLoggEntry((int)Definition.ThorLogWindows.COMMUNICATION, "SEND: OrderNumber" + nOrderNumber + " ($Reset)", (int)Definition.Message.D_SEND);
                    parent_TCPIP_Channel.WriteStream("$Reset");

                    break;

               
            }

            return ret;
        }
       
        public string MessageAnalyse(string strMessage)
        {
               
            string strRet = null;
            string searchForThis = null;
            int nFoundAt = -1;
            bConnectionEstablished = true;

            nTimeout = 10;

            switch (strMessage)
            {

                case "DISCONNECT@":
                case "DISCONNECT@\r":
                    bConnectionEstablished = false;
                    // SetStateValue((int)Definition.BitNumber.OFFLINE);
                    return "DISCONNECT@";

                case "ARE_YOU_THERE":
                case "ARE_YOU_THERE@":
                case "ARE_YOU_THERE@\r":
                    bConnectionEstablished = true;
                    return "I_AM_HERE@";
   
            }


            if (strMessage.StartsWith("!"))
            {
                bLogin = false;
                bLoginSuccess = false;
                if (strMessage.CompareTo(strGetErrorFeedbackChanged) != 0)
                {
                    strGetErrorFeedbackChanged = strMessage;
                    parent_TCPIP_Channel.WriteLoggEntry((int)Definition.ThorLogWindows.COMMUNICATION, "RECEIVED: " + strMessage, (int)Definition.Message.D_RECEIVE);
                }
            }


             // Teach/Auto/Warning/SError/Safeguard/EStop/Error/Paused/Running/Ready
            // #GetStatus,0100000001,0000
            searchForThis = "#Login";
            nFoundAt = strMessage.IndexOf(searchForThis);
            if (nFoundAt >= 0)
            {
                bLoginSuccess = true;
                nFoundAt = -1;
            }

            // Teach/Auto/Warning/SError/Safeguard/EStop/Error/Paused/Running/Ready
            // #GetStatus,0100000001,0000
            searchForThis = "#GetStatus,";
            nFoundAt = strMessage.IndexOf(searchForThis);
            if (nFoundAt >= 0)
            {
                
                if (strMessage.CompareTo(strGetStatusChanged) != 0)
                {
                    strGetStatusChanged = strMessage;
                    parent_TCPIP_Channel.WriteLoggEntry((int)Definition.ThorLogWindows.COMMUNICATION, "RECEIVED: " + strMessage, (int)Definition.Message.D_RECEIVE);

                    AnalyseStatus(strMessage.Substring(nFoundAt + searchForThis.Length));
                }
                bConnectionEstablished = true;
                nFoundAt = -1;
                return null;
            }

            //  #GetIOWord,0200
            searchForThis = "#GetIOWord,";
            nFoundAt = strMessage.IndexOf(searchForThis);
            if (nFoundAt >= 0)
            {
                if (strMessage.CompareTo(strGetIOWordChanged) != 0)
                {
                    strGetIOWordChanged = strMessage;
                    parent_TCPIP_Channel.WriteLoggEntry((int)Definition.ThorLogWindows.COMMUNICATION, "RECEIVED: " + strMessage, (int)Definition.Message.D_RECEIVE);

                    AnalyseIO(strMessage.Substring(nFoundAt + searchForThis.Length));
                }
                bConnectionEstablished = true;
                nFoundAt = -1;
                return null;
            }

            // if not known information comes throw which wasn't handled above, log it
            parent_TCPIP_Channel.WriteLoggEntry((int)Definition.ThorLogWindows.COMMUNICATION, "RECEIVED: " + strMessage, (int)Definition.Message.D_RECEIVE);

            return strRet;
        }

        private void AnalyseStatus(string strMessage)
        {
            /* bTeach,bAuto,bWarning,bSError,bSafeguard,bEStop,bError,bPaused,bRunning,bReady*/

            string[] strStatusAndError = strMessage.Split(new Char[] { ',' });
            string strStatus = null;
            string strError = null;

            if (strStatusAndError.Length > 0)
            {
                strStatus = strStatusAndError[0];
                strError = strStatusAndError[1];
            }

            if (strStatus[0] == '0') { bTeach = false; } else { bTeach = true; }
            if (strStatus[1] == '0') { bAuto = false; } else { bAuto = true; }
            if (strStatus[2] == '0') { bWarning = false; } else { bWarning = true; }
            if (strStatus[3] == '0') { bSError = false; } else { bSError = true; }
            if (strStatus[4] == '0') { bSafeguard = false; } else { bSafeguard = true; }
            if (strStatus[5] == '0') { bEStop = false; } else { bEStop = true; }
            if (strStatus[6] == '0') { bError = false; } else { bError = true; }
            if (strStatus[7] == '0') { bPaused = false; } else { bPaused = true; }
            if (strStatus[8] == '0') { bRunning = false; } else { bRunning = true; }
            if (strStatus[9] == '0') { bReady = false; } else { bReady = true; }

            // offline
             NewState = (int)Definition.BitNumber.OFFLINE;

             if (bReady)
                { // ready
                    NewState = (int)Definition.BitNumber.AUTOMATIC;
                }

               if(bRunning && bAuto){ // ready
                    NewState = (int)Definition.BitNumber.READYAUTOMATIC;
               }


               if(bTeach){// manual
                    NewState = (int)Definition.BitNumber.READYMANUAL;
               }

              if(bWarning){// warning
                    NewState = (int)Definition.BitNumber.WARNING;
              }

              if (bTeach || bWarning || bPaused)
              { // warning/messages
                  NewState = (int)Definition.BitNumber.WARNING;
                  
              }

              if (bSError || bSafeguard || bEStop || bError)
              { // breakdown
                    NewState = (int)Definition.BitNumber.BREAKDOWN;
                   
              }

              SetMessages();
              SetAlarmMessages(strError);
              SetStateValue(NewState);
              
        }

        private void SetMessages()
        {
            int nMessageWord = 0;
            if (bWarning) { nMessageWord += (int)System.Math.Pow(2,8); }
            if (bPaused) { nMessageWord += (int)System.Math.Pow(2, 9); }
            if (bTeach) { nMessageWord += (int)System.Math.Pow(2, 10); }

            myHC.WriteWinCCTagFromSampleValuesByValueName(nMachine_ID + "_Errorbits_3", nMessageWord.ToString(), 5);
        }

        private void SetAlarmMessages(string strError = null)
        {
            int nAlarmWord = 0;
            if (OldState != NewState)
            {
                if (strError != null)
                {
                    myHC.WriteWinCCTagFromSampleValuesByValueName(nMachine_ID + "_Errortext_2", strError, 10);
                }
                if (bSError) { nAlarmWord += (int)System.Math.Pow(2, 8); }
                if (bSafeguard) { nAlarmWord += (int)System.Math.Pow(2, 9); }
                if (bEStop) { nAlarmWord += (int)System.Math.Pow(2, 10); }
                if (bError) { nAlarmWord += (int)System.Math.Pow(2, 11); }
                if (bGrippedWrong) { nAlarmWord += (int)System.Math.Pow(2, 12); }
                if (NewState == (int)Definition.BitNumber.OFFLINE) { nAlarmWord += (int)System.Math.Pow(2, 13); }

                myHC.WriteWinCCTagFromSampleValuesByValueName(nMachine_ID + "_Errorbits_2", nAlarmWord.ToString(), 5);
            }
        }

        // set the StateValue in the DB
        private void SetStateValue(int NewState)
        {
            //  string t = bConnectionEstablished ? "true" : "false";
            //  Console.WriteLine("SetStateValue: nTimeout:" + nTimeout + " nBitNumber:" + nBitNumber + " " + t);
            if (OldState != NewState)
            {
                OldState = NewState;
                if (bConnectionEstablished)
                {
                    myHC.SetStateValueOnMachineStateSignals(nMachine_ID, NewState);
                }
                else
                {
                    myHC.SetStateValueOnMachineStateSignals(nMachine_ID, (int)Definition.BitNumber.OFFLINE);
                    OldState = NewState = (int)Definition.BitNumber.OFFLINE;
                }
            }
        }

        private void AnalyseIO(string strHexValue)
        {

            string strIOs = null;
            if (strHexValue.Length == 4)
            {
                strIOs = hex2binary(strHexValue);
            }

            if (strIOs != null)
            {
                if (strIOs.Length == 16)
                {

                    if (strIOs[0] == '1') { }
                    if (strIOs[1] == '1') { }
                    if (strIOs[2] == '1') { }
                    if (strIOs[3] == '1') { }
                    if (strIOs[4] == '1') { }
                    if (strIOs[5] == '1') { }

                    // set first to gripped
                    nGripperState = -1;
                    // if gripper open set to 0
                    if (strIOs[6] == '1') { nGripperState = 0; }
                    // if gripper closed set to 1
                    if (strIOs[7] == '1') { nGripperState = 1; }

                    if (strIOs[8] == '1') { bGrippedWrong = true; } else { bGrippedWrong = false; }
                    if (strIOs[9] == '1') { }
                    if (strIOs[10] == '1') { }
                    if (strIOs[11] == '1') { }
                    if (strIOs[12] == '1') { }
                    if (strIOs[13] == '1') { }
                    if (strIOs[14] == '1') { }
                    if (strIOs[15] == '1') { }

                    SetStatusbits();

                }
            }
        }


        // ---------------------------------------------------------------------------------------------------------
        // Set status bits: gripped wrong, gripper open gipper close
        // ---------------------------------------------------------------------------------------------------------
        private void SetStatusbits()
        {
           //set status bits on word 1
          //  Console.WriteLine("SetStatusbits");
               // first statusbit gripper open
                if (nGripperState == 0)
                {
                    myHC.SetStateBitOnMachineStateSignals(nMachine_ID, 0, 1, true);
                }
                else 
                {
                    myHC.SetStateBitOnMachineStateSignals(nMachine_ID, 0, 1, false);
                }

                // second statusbit gripper close
                if (nGripperState == 1)
                {
                    myHC.SetStateBitOnMachineStateSignals(nMachine_ID, 1, 1, true);
                }
                else
                {
                    myHC.SetStateBitOnMachineStateSignals(nMachine_ID, 1, 1, false);
                }

                // third bit is gripped wrong
                if (bGrippedWrong)
                {
                    myHC.SetStateBitOnMachineStateSignals(nMachine_ID, 2, 1, true);
                    SetAlarmMessages();
                }
                else
                {
                    myHC.SetStateBitOnMachineStateSignals(nMachine_ID, 2, 1, false);
                    SetAlarmMessages();
                }

                 // fourth bit is "Teach mode"
                if (bTeach)
                {
                    myHC.SetStateBitOnMachineStateSignals(nMachine_ID, 3, 1, true);
                    SetAlarmMessages();
                }
                else
                {
                    myHC.SetStateBitOnMachineStateSignals(nMachine_ID, 3, 1, false);
                    SetAlarmMessages();
                }
                // fourth bit is "Pause mode"
                if (bPaused)
                {
                    myHC.SetStateBitOnMachineStateSignals(nMachine_ID, 4, 1, true);
                    SetAlarmMessages();
                }
                else
                {
                    myHC.SetStateBitOnMachineStateSignals(nMachine_ID, 4, 1, false);
                    SetAlarmMessages();
                }

        }

        private string hex2binary(string hexvalue) 
        { 
            string binaryval = "";
            binaryval = Convert.ToString(Convert.ToInt32(hexvalue, 16), 2).PadLeft(16, '0'); 
            return binaryval; 
        }

        public int GetInfo(int nInfo)
        {
            int ret = -1;
          
            switch (nInfo)
            {
                case 7: // bit 7
                    ret = nGripperState;
                    break;

                case 9: 
                    ret = bGrippedWrong ? 1 : 0;
                    break;

                /* bTeach
                   bAuto 
                   bWarning 
                   bSError
                   bSafeguard 
                   bEStop
                   bError 
                   bPaused 
                   bRunning
                   bReady*/


                 case 20: // teach mode
                    ret = bTeach ? 1 : 0;
                    break;
                 case 21: // start mode
                    ret = bAuto? 1 : 0;
                    break;
                 case 22: // Warning
                    ret = bWarning ? 1 : 0;
                    break;
                 case 23: // segnificant Error
                    ret = bSError ? 1 : 0;
                    break;
                 case 24: // SafeGuard 
                    ret = bSafeguard ? 1 : 0;
                    break;
                 case 25: // EStop 
                    ret = bEStop ? 1 : 0;
                    break;
                 case 26: // Error 
                    ret = bError ? 1 : 0;
                    break;
                 case 27: // Paused 
                    ret = bPaused ? 1 : 0;
                    break;
                 case 28: // running mode (program is started)
                    ret = bRunning ? 1 : 0;
                    break;
                 case 29: // ready mode (program not started)
                    ret = bReady ? 1 : 0;
                    break;

              
            }
            return ret;
        }
 
    }

}
