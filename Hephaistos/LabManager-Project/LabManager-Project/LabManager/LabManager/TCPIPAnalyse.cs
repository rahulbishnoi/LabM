using Logging;
using Definition;
using MySQL_Helper_Class;
using LabManager;

namespace TCPIP
{
    class InterfaceAnalyse
    {
        TCPIP_Channel parent_TCPIP_Channel = null;
        Save mySave = new Save("TCPIP-InterfaceAnalyse");
        Definitions myThorDef = new Definitions();
        MySQL_HelperClass myHC = new MySQL_HelperClass();
        private int nMachine_ID = -1;
        private int nAnalyseTypeID = -1;
        private LabManager.InterfaceAnalyse analyserLabManager = null;
        private TGA.InterfaceAnalyse analyserTGA = null;
        private Robot.InterfaceAnalyse analyserRobot = null;
        private RobotRemoteControl.InterfaceAnalyse analyserRobotRemoteControl = null;
     
        public InterfaceAnalyse(int nMachine_ID, TCPIP_Channel parent_TCPIP_Channel = null, int nAnalyseTypeID = -1)
        {
            if (parent_TCPIP_Channel != null)
            {
                this.parent_TCPIP_Channel = parent_TCPIP_Channel;
            }
            this.nMachine_ID = nMachine_ID;
            this.nAnalyseTypeID = nAnalyseTypeID;

         
            switch (nAnalyseTypeID)
            {
                case (int)Definition.TCPIPAnalyseClass.LABMANAGER: // LabMAanager
                    analyserLabManager = new LabManager.InterfaceAnalyse();
                    break;

                case (int)Definition.TCPIPAnalyseClass.TGA:
                    analyserTGA = new TGA.InterfaceAnalyse(nMachine_ID, this.parent_TCPIP_Channel);
                    break;

                case (int)Definition.TCPIPAnalyseClass.LIMS:
                    // Lims class not implemented yet
                    break;

                case (int)Definition.TCPIPAnalyseClass.ROBOT:
                    // Robot 
                    analyserRobot = new Robot.InterfaceAnalyse(nMachine_ID, this.parent_TCPIP_Channel);
                    break;

                case (int)Definition.TCPIPAnalyseClass.ROBOTREMOTECONTROL:
                    // Robot remote control
                    analyserRobotRemoteControl = new RobotRemoteControl.InterfaceAnalyse(nMachine_ID, this.parent_TCPIP_Channel);
                    break;

                case -1:
                    parent_TCPIP_Channel.WriteLoggEntry((int)Definition.ThorLogWindows.ERROR, "no nAnalyseTypeID given ", (int)Definition.Message.D_ALARM);
             
                    break;
            }
            
        }


       
        //----------------------------------------------------------------------------------
        // sending commands from routing
        //----------------------------------------------------------------------------------
        public int SendCommandFromRouting(int nOrderNumber, string strCommand, int nSample_ID, int nMachine_ID)
        {
           

            int ret = -1;
            switch (nAnalyseTypeID)
            {
                case (int)Definition.TCPIPAnalyseClass.LABMANAGER: // LabManager
                    // LabMAanager Interface hat keine Methode für Kommandos vom Routing
                  ret = -1;
                  break;

                case (int)Definition.TCPIPAnalyseClass.TGA:
                  if (analyserTGA != null)
                  {
                      ret = analyserTGA.SendCommandFromRouting(nOrderNumber, strCommand, nSample_ID, nMachine_ID);
                  }
                  break;

                case (int)Definition.TCPIPAnalyseClass.LIMS:
                  // Lims class not implemented yet
                  ret = -1;
                  break;

                case (int)Definition.TCPIPAnalyseClass.ROBOT:
                  // robot class 
                  if (analyserRobot != null)
                  {
                      ret = analyserRobot.SendCommandFromRouting(nOrderNumber, strCommand, nSample_ID, nMachine_ID);
                  }
                  break;

                case (int)Definition.TCPIPAnalyseClass.ROBOTREMOTECONTROL:
                  // robot remote control class  
                  if (analyserRobotRemoteControl != null)
                  {
                      ret = analyserRobotRemoteControl.SendCommand(nOrderNumber, strCommand, nSample_ID, nMachine_ID);
                  }
                  break;

               
            }
            return ret;
        }

        public string MessageAnalyse(string strMessage)
        {
            // trim off the CR if exist
            strMessage = strMessage.TrimEnd('\r', '\n');
            string strCommand = null;

            switch (nAnalyseTypeID)
            {
                case (int)Definition.TCPIPAnalyseClass.LABMANAGER: // LabMAanager
                    {
                        if (analyserLabManager != null)
                        {
                            strCommand = analyserLabManager.MessageAnalyse(strMessage) ;
                        }
                        break; 
                    }

                case (int)Definition.TCPIPAnalyseClass.TGA:
                    {
                        if (analyserTGA != null)
                        {
                            strCommand = analyserTGA.MessageAnalyse(strMessage) ;
                        }
                        break;
                    }

                case (int)Definition.TCPIPAnalyseClass.LIMS:
                    // Lims class not implemented yet
                    break;

                case (int)Definition.TCPIPAnalyseClass.ROBOT:
                    if (analyserRobot != null)
                    {
                        strCommand = analyserRobot.MessageAnalyse(strMessage) ;
                    }
                    break;

                case (int)Definition.TCPIPAnalyseClass.ROBOTREMOTECONTROL:
                    if (analyserRobotRemoteControl != null)
                    {
                        strCommand = analyserRobotRemoteControl.MessageAnalyse(strMessage) ;
                    }
                    break;
            }

           
            return strCommand;
        }

        public int GetInfo(int nInfo)
        {
            int nRet = -1;
            

            switch (nAnalyseTypeID)
            {
                
                case (int)Definition.TCPIPAnalyseClass.ROBOTREMOTECONTROL:
                    if (analyserRobotRemoteControl != null)
                    {
                        nRet = analyserRobotRemoteControl.GetInfo(nInfo);
                    }
                    break;
            }


            return nRet;
        }

      
       
 
    }

}
