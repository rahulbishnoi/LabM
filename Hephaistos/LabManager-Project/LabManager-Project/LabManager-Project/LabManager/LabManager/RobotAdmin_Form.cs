using System;
using System.Drawing;
using System.Windows.Forms;
using MySQL_Helper_Class;

namespace LabManager
{
    public partial class RobotAdmin_Form : Form
    {
        private LabManager parent;
        MySQL_HelperClass myHC = new MySQL_HelperClass();
        private int nMachine_ID = -1;
      
        private bool bOnline = false; 
        private int nGripperOpen = -1;
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


        public RobotAdmin_Form(LabManager parent)
        {
            Application.EnableVisualStyles();
            this.StartPosition = FormStartPosition.CenterScreen;

            this.parent = parent;
            nMachine_ID = myHC.GetMachine_IDFromTCPIPConfigurationForRobot((int)Definition.TCPIPAnalyseClass.ROBOTREMOTECONTROL);
                   
            InitializeComponent();


            // Hook up the Elapsed event for the timer.
            //RobotRCTimer.Elapsed += new ElapsedEventHandler(TimerEventProcessor);
            RobotRCTimer.Tick += new EventHandler(TimerEventProcessor);

            // Set the Interval to 1 seconds (1000 milliseconds).
            RobotRCTimer.Interval = 1000;
            RobotRCTimer.Enabled = true;

            ribbonLabel_Connect.SmallImage = imgList.Images["OrangeBlock"];
            ribbonLabel_Connect.Text = "try to connect ...";
            button_Motors_OFF.BackColor = Color.LightPink;
            button_Motors_OFF.Enabled = true;
            button_Motors_ON.BackColor = Color.LightGreen;

            parent.SendCommadToTCPIP(10, -1, "", nMachine_ID);
            nGripperOpen = parent.GetTCPIP_Info(10, 7, nMachine_ID);
            //CheckTCPIPOnlineState();

          
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
            pictureBox_bTeach.Image = imgList.Images["grayDot"];
            pictureBox_bAuto.Image = imgList.Images["grayDot"];
            pictureBox_bWarning.Image = imgList.Images["grayDot"];
            pictureBox_bSError.Image = imgList.Images["grayDot"];
            pictureBox_bSafeguard.Image = imgList.Images["grayDot"];
            pictureBox_bEStop.Image = imgList.Images["grayDot"];
            pictureBox_bError.Image = imgList.Images["grayDot"];
            pictureBox_bPaused.Image = imgList.Images["grayDot"];
            pictureBox_bRunning.Image = imgList.Images["grayDot"];
            pictureBox_bReady.Image = imgList.Images["grayDot"];
        }

        private void RobotAdmin_Form_Load(object sender, EventArgs e)
        {
            if (parent != null)
            {
                CheckTCPIPOnlineState();
                SetStatusIcons();
            }
        }

        private void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            if(!this.Visible){return;}

            if (parent != null && RobotRCTimer.Enabled)
            {
                CheckTCPIPOnlineState();
                SetStatusIcons();
            }
        }

        private void CheckTCPIPOnlineState()
        {
            if (!this.Visible) { return; }

            int returnValue = -1;

            MethodInvoker GetInfo = delegate
            {
              
                nGripperOpen = parent.GetTCPIP_Info(10,7, nMachine_ID); // gripper status
                if (nGripperOpen == 0)
                {
                    pictureBox_Gripper.Image = imageList_Gripper.Images["Gripper_close"];
                }
                else if (nGripperOpen == 1)
                {
                    pictureBox_Gripper.Image = imageList_Gripper.Images["Gripper_open"];
                }
                else if (nGripperOpen == -1)
                {
                    pictureBox_Gripper.Image = imageList_Gripper.Images["Gripper_gripped"];
                }

                //  buttons
                if (parent.GetTCPIP_Info(10, 29, nMachine_ID) == 1) //29 = ready mode (program not started)
                {
                    button_START.BackColor = Color.LightGray;
                    button_START.Enabled = true;
                    button_STOP.Enabled = false;
                    button_RESET.Enabled = true;
                    button_PAUSE.Enabled = false;
                    button_CONTINUE.Enabled = false;
                    button_HOME.Enabled = false;
                    button_GRIPPEROPEN.Enabled = true;
                    button_GRIPPERCLOSE.Enabled = true;
                    button_Motors_ON.Enabled = true;
                    button_Motors_OFF.Enabled = true;
                    pictureBox_Gripper.Enabled = true;
                }
                else
                {
                    button_START.BackColor = Color.LightGreen;
                    button_START.Enabled = false;
                    button_STOP.Enabled = true;
                    button_RESET.Enabled = false;
                    button_PAUSE.Enabled = true;
                    button_CONTINUE.Enabled = true;
                    button_HOME.Enabled = true;
                    button_GRIPPEROPEN.Enabled = false;
                    button_GRIPPERCLOSE.Enabled = false;
                    button_Motors_ON.Enabled = false;
                    button_Motors_OFF.Enabled = false;
                    pictureBox_Gripper.Enabled = false;
                }

               

                // online state
                returnValue = parent.GetTCPIP_Info(1, -1, nMachine_ID);
                if (returnValue == 1)
                { bOnline = true; ribbonLabel_Connect.SmallImage = imgList.Images["GreenBlock"]; ribbonLabel_Connect.Text = "Online"; }
                else
                { bOnline = false; ribbonLabel_Connect.SmallImage = imgList.Images["RedBlock"]; ribbonLabel_Connect.Text = "Offline"; }

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

                returnValue = parent.GetTCPIP_Info(10,20, nMachine_ID);
                if (returnValue == 1) { bTeach = true; } else { bTeach = false; }
                returnValue = parent.GetTCPIP_Info(10, 21, nMachine_ID);
                if (returnValue == 1) { bAuto = true; } else { bAuto = false; }
                returnValue = parent.GetTCPIP_Info(10, 22, nMachine_ID);
                if (returnValue == 1) { bWarning = true; } else { bWarning = false; }
                returnValue = parent.GetTCPIP_Info(10, 23, nMachine_ID);
                if (returnValue == 1) { bSError = true; } else { bSError = false; }
                returnValue = parent.GetTCPIP_Info(10, 24, nMachine_ID);
                if (returnValue == 1) { bSafeguard = true; } else { bSafeguard = false; }
                returnValue = parent.GetTCPIP_Info(10, 25, nMachine_ID);
                if (returnValue == 1) { bEStop = true; } else { bEStop = false; }
                returnValue = parent.GetTCPIP_Info(10, 26, nMachine_ID);
                if (returnValue == 1) { bError = true; } else { bError = false; }
                returnValue = parent.GetTCPIP_Info(10, 27, nMachine_ID);
                if (returnValue == 1) { bPaused = true; } else { bPaused = false; }
                returnValue = parent.GetTCPIP_Info(10, 28, nMachine_ID);
                if (returnValue == 1) { bRunning = true; } else { bRunning = false; }
                returnValue = parent.GetTCPIP_Info(10, 29, nMachine_ID);
                if (returnValue == 1) { bReady = true; } else { bReady = false; }
            
            };
            try
            {
                Invoke(GetInfo);
            }
            catch(Exception ex) { Console.WriteLine("MethodInvoker!!!!\r\n" + ex.ToString()); }

            /*   if (bOnline )
               {
                 //  ribbonLabel_Connect.SmallImage = imgList.Images["GreenBlock"];
                   //bOnline = true;
                 //  Console.WriteLine("online");
               }
               else {
                  // Console.WriteLine("offline");
                   //ribbonLabel_Connect.SmallImage = imgList.Images["RedBlock"]; 
                  // bOnline = false; 
               }*/
        }

        private void miExit_Click(object sender, EventArgs e)
        {
            Close();
        }

       

        private void CheckStatus()
        {
           // nGripperOpen = parent.SendCommadToTCPIP(8, -1, "", nMachine_ID);
            //nGripperOpen = parent.GetTCPIP_Info(10, 8, nMachine_ID);
        }

        private int ExecuteCommand(string strCommand)
        {
            int ret = -1;
            
            return ret;
        }

        private void button_START_Click(object sender, EventArgs e)
        {
            if (parent != null && bOnline)
            {
                parent.SendCommadToTCPIP(100, -1, "$Start,0", nMachine_ID);
            }
        }

        private void button_PAUSE_Click(object sender, EventArgs e)
        {
            if (parent != null && bOnline)
            {
                parent.SendCommadToTCPIP(100, -1, "$Pause", nMachine_ID);
            }
        }

        private void button_STOP_Click(object sender, EventArgs e)
        {
            if (parent != null && bOnline)
            {
                parent.SendCommadToTCPIP(100, -1, "$Stop", nMachine_ID);
            }
        }

        private void button_CONTINUE_Click(object sender, EventArgs e)
        {
            if (parent != null && bOnline)
            {
                parent.SendCommadToTCPIP(100, -1, "$Continue", nMachine_ID);
            }
        }

        private void button_RESET_Click(object sender, EventArgs e)
        {
            if (parent != null && bOnline)
            {
                parent.SendCommadToTCPIP(100, -1, "$Reset", nMachine_ID);
            }
        }

        private void button_Motors_ON_Click(object sender, EventArgs e)
        {
            if (parent != null && bOnline)
            {
                parent.SendCommadToTCPIP(100, -1, "$SetMotorsOn", nMachine_ID);
            }
        }

        private void button_Motors_OFF_Click(object sender, EventArgs e)
        {
            if (parent != null && bOnline)
            {
                parent.SendCommadToTCPIP(100, -1, "$SetMotorsOff", nMachine_ID);
            }
        }

        private void button_HOME_Click(object sender, EventArgs e)
        {
            if (parent != null && bOnline)
            {
                parent.SendCommadToTCPIP(100, -1, "$Home", nMachine_ID);
            }
        }

        private void button_LOGIN_Click(object sender, EventArgs e)
        {
            if (parent != null && bOnline)
            {
                parent.SendCommadToTCPIP(100, -1, "$Login", nMachine_ID);
            }
        }

        private void button_LOGOUT_Click(object sender, EventArgs e)
        {
            if (parent != null && bOnline)
            {
                parent.SendCommadToTCPIP(100, -1, "$Logout", nMachine_ID);
            }
        }

        private void button_GRIPPEROPEN_Click(object sender, EventArgs e)
        {
            if (parent != null && bOnline)
            {
                parent.SendCommadToTCPIP(100, -1, "$SetIO,8,0", nMachine_ID);
                parent.SendCommadToTCPIP(100, -1, "$SetIO,9,1", nMachine_ID);
                
                CheckStatus();
            }
        }

        private void button_GRIPPERCLOSE_Click(object sender, EventArgs e)
        {
            if (parent != null && bOnline)
            {
                parent.SendCommadToTCPIP(100, -1, "$SetIO,9,0", nMachine_ID);
                parent.SendCommadToTCPIP(100, -1, "$SetIO,8,1", nMachine_ID);
               
                CheckStatus();
            }
        }

        private void SetStatusIcons()
        {
            if (bTeach)
            { pictureBox_bTeach.Image = imgList.Images["greenDot"];   } else {  pictureBox_bTeach.Image = imgList.Images["grayDot"];  }

            if (bAuto)
            { pictureBox_bAuto.Image = imgList.Images["greenDot"]; }
            else { pictureBox_bAuto.Image = imgList.Images["grayDot"]; }

            if (bWarning)
            { pictureBox_bWarning.Image = imgList.Images["greenDot"]; }
            else { pictureBox_bWarning.Image = imgList.Images["grayDot"]; }

            if (bSError)
            { pictureBox_bSError.Image = imgList.Images["greenDot"]; }
            else { pictureBox_bSError.Image = imgList.Images["grayDot"]; }

            if (bSafeguard)
            { pictureBox_bSafeguard.Image = imgList.Images["greenDot"]; }
            else { pictureBox_bSafeguard.Image = imgList.Images["grayDot"]; }

            if (bEStop)
            { pictureBox_bEStop.Image = imgList.Images["greenDot"]; }
            else { pictureBox_bEStop.Image = imgList.Images["grayDot"]; }

            if (bError)
            { pictureBox_bError.Image = imgList.Images["greenDot"]; }
            else { pictureBox_bError.Image = imgList.Images["grayDot"]; }

            if (bPaused)
            { pictureBox_bPaused.Image = imgList.Images["greenDot"]; }
            else { pictureBox_bPaused.Image = imgList.Images["grayDot"]; }

            if (bRunning)
            { pictureBox_bRunning.Image = imgList.Images["greenDot"]; }
            else { pictureBox_bRunning.Image = imgList.Images["grayDot"]; }

            if (bReady)
            { pictureBox_bReady.Image = imgList.Images["greenDot"]; }
            else { pictureBox_bReady.Image = imgList.Images["grayDot"]; }

           
        }
        
        

    }
}
