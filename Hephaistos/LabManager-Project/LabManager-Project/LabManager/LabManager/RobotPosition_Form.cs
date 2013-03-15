using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Logging;
using Definition;
using MySQL_Helper_Class;


namespace LabManager
{
    public partial class RobotPosition_Form : Form
    {
        LabManager _parent;
        Definitions myThorDef = new Definitions();
        MySQL_HelperClass myHC = new MySQL_HelperClass();
        Save mySave = new Save("Robot-AdminForm");
        private int nMagPosFrom = -1;
        private int nMagPosTo = -1;

        public RobotPosition_Form(LabManager parent)
        {
            _parent = parent;
            Application.EnableVisualStyles();
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            textBoxCommand.Text = "";
            button_Send.Enabled = false;
        }

        private void button_Send_Click(object sender, EventArgs e)
        {
            string strCommand = null;
            strCommand = textBoxCommand.Text;
            if (strCommand != null)
            {
                if (strCommand.Length > 0)
                {
                    int nMachine_ID = myHC.GetMachine_IDFromTCPIPConfigurationForRobot((int)Definition.TCPIPAnalyseClass.ROBOT);
                    // SendCommadToTCPIP(int nNumber, int nSample_ID, string strCommand, int nMachine_ID)
                    _parent.SendCommadToTCPIP(10, -1, strCommand, nMachine_ID);
                }
            }

        }

        private void RobotAdmin_Form_Load(object sender, EventArgs e)
        {
            textBoxCommand.Enabled = false;
            LoadFromAndToGrids();
            LoadComboBoxFrom(0);
            LoadComboBoxTo(0);
        }

        private void LoadFromAndToGrids()
        {
            DataSet ds_RobotposFrom = null;
            //DataSet ds_RobotposTo = null;
            string SQL_Statement = null;

           // SQL_Statement=@"SELECT Name,RobotMagazinePosition,Dimension_X,Dimension_Y FROM magazine_configuration WHERE IsRobot=1";
            SQL_Statement = @"SELECT Name,RobotMagazinePosition,Dimension_X,Dimension_Y FROM magazine_configuration WHERE IsRobot=1 UNION Select Name,PosNumber As RobotMagazinePosition,0,0 from machine_Positions WHERE IsRobot=1";
            try
            {
                ds_RobotposFrom = myHC.GetDataSetFromSQLCommand(SQL_Statement);
                if (ds_RobotposFrom.Tables[0] != null)
                {
                    if (ds_RobotposFrom.Tables[0].Rows.Count > 0)
                    {

                        c1FlexGridFrom.DataSource = ds_RobotposFrom.Tables[0];

                        c1FlexGridTo.DataSource = ds_RobotposFrom.Tables[0].Copy();

                        c1FlexGridFrom.Cols[4].Visible = false;
                        c1FlexGridFrom.Cols[3].Visible = false;
                        c1FlexGridFrom.Cols[1].Caption = "Name";
                        c1FlexGridFrom.Cols[2].Caption = "Position";
                        c1FlexGridFrom.Cols[0].Width = 15;
                        c1FlexGridFrom.ExtendLastCol = true;
                        c1FlexGridFrom.AutoResize = true;

                        c1FlexGridTo.Cols[4].Visible = false;
                        c1FlexGridTo.Cols[3].Visible = false;
                        c1FlexGridTo.Cols[1].Caption = "Name";
                        c1FlexGridTo.Cols[2].Caption = "Position";
                        c1FlexGridTo.Cols[0].Width = 15;
                        c1FlexGridTo.ExtendLastCol = true;
                        c1FlexGridTo.AutoResize = true;

                    }
                }
            }
            catch { }
        }

        private void LoadComboBoxFrom(int nMaxPos)
        {
            comboMagPosFrom.Items.Clear();
            for (int i = 1; i <= nMaxPos; i++)
            {
                comboMagPosFrom.Items.Add(i);
            }
        }

        private void LoadComboBoxTo(int nMaxPos)
        {
            comboMagPosTo.Items.Clear();
            for (int i = 1; i <= nMaxPos; i++)
            {
                comboMagPosTo.Items.Add(i);
            }
        }

        private void c1FlexGridFrom_Click(object sender, EventArgs e)
        {
            comboMagPosFrom.SelectedIndex = -1;
            
            UpdateSampleFromPosition();
            CreateRobotCommand();
        }

        private void UpdateSampleFromPosition()
        {
            nMagPosFrom = -1;
                int nDimensionX = 0;
                int nDimensionY = 0;
                try
                {
                    Int32.TryParse(c1FlexGridTo[c1FlexGridFrom.Row, "Dimension_X"].ToString(), out nDimensionX);
                    Int32.TryParse(c1FlexGridTo[c1FlexGridFrom.Row, "Dimension_Y"].ToString(), out nDimensionY);
                    nMagPosFrom = nDimensionX * nDimensionY; 
                }
                catch (Exception ex) { mySave.InsertRow((int)Definition.Message.D_MESSAGE, "RobotAdmin_Form::UpdateSampleFromPosition: can not find mag position\r\n" + ex.ToString()); }

                if (nMagPosFrom >= 0)
                {
                    LoadComboBoxFrom(nMagPosFrom);
                }
            
        }

        private void c1FlexGridTo_Click(object sender, EventArgs e)
        {
            comboMagPosTo.SelectedIndex = -1;
           
            UpdateSampleToPosition();
            CreateRobotCommand();
        }
        
        private void UpdateSampleToPosition()
        {
            nMagPosTo = -1;
            int nDimensionX = 0;
            int nDimensionY = 0;
            try
            {
                Int32.TryParse(c1FlexGridTo[c1FlexGridTo.Row, "Dimension_X"].ToString(), out nDimensionX);
                Int32.TryParse(c1FlexGridTo[c1FlexGridTo.Row, "Dimension_Y"].ToString(), out nDimensionY);
                nMagPosTo = nDimensionX * nDimensionY; 
            }
            catch (Exception ex) { mySave.InsertRow((int)Definition.Message.D_MESSAGE, "RobotAdmin_Form::UpdateSampleFromPosition: can not find mag position\r\n" + ex.ToString()); }

            if (nMagPosTo >= 0)
            {
                LoadComboBoxTo(nMagPosTo);
            }

        }

        private void checkBox_EditCommand_Click(object sender, EventArgs e)
        {
            if (checkBox_EditCommand.Checked)
            {
                button_Send.Enabled = true;
                textBoxCommand.Enabled = true; 
            } else {
                textBoxCommand.Enabled = false; 
            }
        }

        private void comboMagPosFrom_Click(object sender, EventArgs e)
        {
            CreateRobotCommand();
          
        }

        private void comboMagPosTo_Click(object sender, EventArgs e)
        {
            CreateRobotCommand();
        }

        private void CreateRobotCommand()
        {
            if (checkBox_EditCommand.Checked) { return; }
            int nPosFrom = -1;
            int nPosTo = -1;
            string strMagazinePositionFrom = null;
            string strMagazinePositionTo = null;
            int nSelectedMagPosFrom = -1;
            int nSelectedMagPosTo = -1;
           
            textBoxCommand.Text = "";
            WriteStatusbarLeft("", true);
            try
            {
                Int32.TryParse(c1FlexGridFrom[c1FlexGridFrom.Row, "RobotMagazinePosition"].ToString(), out nPosFrom);
                Int32.TryParse(c1FlexGridTo[c1FlexGridTo.Row, "RobotMagazinePosition"].ToString(), out nPosTo);
            }
            catch (Exception ex) { mySave.InsertRow((int)Definition.Message.D_MESSAGE, "RobotAdmin_Form::CreateRobotCommand: can not find position\r\n" + ex.ToString()); }

           

            if (nMagPosFrom > 0)
            {
                try
                {
                    Int32.TryParse(comboMagPosFrom.Text, out nSelectedMagPosFrom);
                }
                catch (Exception ex) { mySave.InsertRow((int)Definition.Message.D_MESSAGE, "RobotAdmin_Form::CreateRobotCommand: no mag position selected\r\n" + ex.ToString()); }

                if (nSelectedMagPosFrom <= 0)
                {
                    WriteStatusbarLeft("no magazinepos 'from' selected", true);
                    return;
                }
                else { WriteStatusbarLeft(""); }
                strMagazinePositionFrom = "MAGAZINE";

            }
            else { strMagazinePositionFrom = "POSITION"; comboMagPosFrom.Text = "0"; nSelectedMagPosFrom = 0;  WriteStatusbarLeft(""); }

            try
            {
                Int32.TryParse(comboMagPosTo.Text, out nSelectedMagPosTo);
            }
            catch (Exception ex) { mySave.InsertRow((int)Definition.Message.D_MESSAGE, "RobotAdmin_Form::CreateRobotCommand: no mag position selected\r\n" + ex.ToString()); }

            if (nMagPosTo > 0)
            {

                strMagazinePositionTo = "MAGAZINE";
                if (nSelectedMagPosTo <= 0)
                {
                    WriteStatusbarLeft("no magazinepos 'to' selected", true);
                    return;
                }
                else { WriteStatusbarLeft(""); }  

            }
            else { strMagazinePositionTo = "POSITION"; comboMagPosTo.Text = "0"; nSelectedMagPosTo = 0; WriteStatusbarLeft(""); }

            textBoxCommand.Text = @"test@MOVE@" + strMagazinePositionFrom + "@GRIP@" + nPosFrom + "@" + nSelectedMagPosFrom.ToString() + "@" + strMagazinePositionTo + "@RELEASE@" + nPosTo + "@" + nSelectedMagPosTo.ToString();
            WriteStatusbarLeft("");
        }

        private void miExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void WriteStatusbarLeft(string strText, bool bError = false)
        {
            c1StatusBar.LeftPaneItems.Clear();
            c1StatusBar.LeftPaneItems.Add(strText);
            if (bError)
            {
                c1StatusBar.ForeColorOuter = Color.Red;
                button_Send.Enabled = false;
            }
            else
            {
                c1StatusBar.ForeColorOuter = Color.Black;
                button_Send.Enabled = true;
            }

        }

        private void comboMagPosFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            CreateRobotCommand();
            
        }
        private void comboMagPosTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CreateRobotCommand();
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            // read Positions
            string strCommand = null;
            int nMachine_ID = myHC.GetMachine_IDFromTCPIPConfigurationForRobot((int)Definition.TCPIPAnalyseClass.ROBOT);

            strCommand = "COMMAND@GETPOSITIONS@";
            // SendCommadToTCPIP(int nNumber, int nSample_ID, string strCommand, int nMachine_ID)
            _parent.SendCommadToTCPIP(10, -1, strCommand, nMachine_ID);

            strCommand = "COMMAND@GETPALLETS@";           
            _parent.SendCommadToTCPIP(10, -1, strCommand, nMachine_ID);
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            string strCommand = null;
            int nMachine_ID = myHC.GetMachine_IDFromTCPIPConfigurationForRobot((int)Definition.TCPIPAnalyseClass.ROBOT);

            strCommand = "COMMAND@TESTPOS@";
            // SendCommadToTCPIP(int nNumber, int nSample_ID, string strCommand, int nMachine_ID)
            _parent.SendCommadToTCPIP(10, -1, strCommand, nMachine_ID);
        }

       
    }
}
