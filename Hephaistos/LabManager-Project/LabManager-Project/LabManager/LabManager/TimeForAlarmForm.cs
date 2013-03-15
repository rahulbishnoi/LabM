using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Logging;
using MySQL_Helper_Class;

namespace LabManager
{
    public partial class TimeForAlarmForm : Form
    {
        MySQL_HelperClass myHC = new MySQL_HelperClass();
        Save mySave = new Save("TimeForAlarmForm");
        private Timer ReloadTimer = new System.Windows.Forms.Timer();
        private DataSet ds_TimeForAlarm = null;

        public TimeForAlarmForm()
        {
            Application.EnableVisualStyles();
            InitializeComponent();

            //process the timer event to the timer. 
            ReloadTimer.Tick += new EventHandler(TimerEventProcessorForMagazine);

            // Set the timer interval to 1 second.
            ReloadTimer.Interval = 1000;
            ReloadTimer.Start();

            this.Text = "Alarm List for sample point exceeding";
            this.StartPosition = FormStartPosition.CenterParent;
        }

        private void TimerEventProcessorForMagazine(Object myObject, EventArgs myEventArgs)
        {
            // nur aktiv neu laden, wenn das fenster im Fokus steht
            if (!this.Visible) { return; }
            SetTimeForAlarmGrid();
        }

        private void TimeForAlarmForm_Load(object sender, EventArgs e)
        {
            SetTimeForAlarmGrid();
        }


        private void SetTimeForAlarmGrid()
        {
            try
            {
                if (ds_TimeForAlarm != null)
                {
                    ds_TimeForAlarm.Clear();
                }
                ds_TimeForAlarm = myHC.GetDataSetFromSQLCommand(GetSQL_Statement());

                if (ds_TimeForAlarm != null)
                {
                    if (ds_TimeForAlarm.Tables[0].Rows.Count > 0)
                    {
                        try
                        {
                            c1TrueDBGridTimeForAlarm.DataSource = ds_TimeForAlarm.Tables[0];
                            c1TrueDBGridTimeForAlarm.Splits[0].DisplayColumns["TimeAlarmOn"].Width = 20;
                            c1TrueDBGridTimeForAlarm.Splits[0].DisplayColumns["TimeWarningOn"].Width = 20;
                            c1TrueDBGridTimeForAlarm.Splits[0].DisplayColumns["TimeAlarmOn"].FetchStyle = true;
                            c1TrueDBGridTimeForAlarm.Splits[0].DisplayColumns["TimeWarningOn"].FetchStyle = true;
                            c1TrueDBGridTimeForAlarm.Splits[0].DisplayColumns["TimeWarningOn"].Visible = false;
                            c1TrueDBGridTimeForAlarm.Columns["TimeALarmOn"].Caption = "";

                            c1TrueDBGridTimeForAlarm.Columns["TimeWarningOn"].ValueItems.Translate = true;
                            c1TrueDBGridTimeForAlarm.Columns["TimeWarningOn"].ValueItems.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.Normal;
                            c1TrueDBGridTimeForAlarm.Columns["TimeAlarmOn"].ValueItems.Translate = true;
                            c1TrueDBGridTimeForAlarm.Columns["TimeAlarmOn"].ValueItems.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.Normal;
                        }
                        catch { }
                    }
                }
            }
            catch { }
        }

        private string GetSQL_Statement()
        {
            string strSQL_String = null;
            strSQL_String = @"SELECT         routing_position_entries.TimeWarningOn,  routing_position_entries.TimeAlarmOn,machines.Name AS MachineName, machine_positions.Name AS PositionName,routing_position_entries.TimeForWarning, routing_position_entries.TimeForAlarm, routing_position_entries.ActualTime
 
                         FROM            machines INNER JOIN
                         routing_positions ON machines.idmachines = routing_positions.Machine_ID INNER JOIN
                         machine_positions ON routing_positions.Machine_Position_ID = machine_positions.idmachine_positions INNER JOIN
                         routing_position_entries ON routing_positions.idrouting_positions = routing_position_entries.Position_ID
                         WHERE        (routing_position_entries.TimeAlarmOn = 1) OR
                        (routing_position_entries.TimeWarningOn = 1)";
            return strSQL_String;
        }

        private void c1TrueDBGridTimeForAlarm_FetchRowStyle_1(object sender, C1.Win.C1TrueDBGrid.FetchRowStyleEventArgs e)
        {
            
            C1.Win.C1TrueDBGrid.C1TrueDBGrid tdbgrid = sender as C1.Win.C1TrueDBGrid.C1TrueDBGrid;

          
            C1.Win.C1TrueDBGrid.Style S = new C1.Win.C1TrueDBGrid.Style();
            Font myfont;
            myfont = new Font(S.Font, FontStyle.Bold);
            S.Font = myfont;
            try
            {
                // Warning
                if ((bool)tdbgrid[e.Row, "TimeWarningOn"] )
                {
                //    tdbgrid.Columns["TimeWarningOn"].ValueItems.Translate = true;
               //     tdbgrid.Columns["TimeAlarmOn"].ValueItems.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.Normal;
                    e.CellStyle.BackColor = System.Drawing.Color.Orange;
                    e.CellStyle.Alpha = 100;
                }
                // Alarm
                if ((bool)tdbgrid[e.Row, "TimeAlarmOn"] )
                {
                    e.CellStyle.BackColor = System.Drawing.Color.Red;
                    e.CellStyle.Alpha = 100;
                  //  e.CellStyle.Font = myfont;
                    if (tdbgrid.Columns["TimeAlarmOn"].Caption == "TimeAlarmOn")
                    {
                      //  tdbgrid.Columns["TimeAlarmOn"].ValueItems.Translate = true;
                     //   tdbgrid.Columns["TimeAlarmOn"].ValueItems.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.Normal;
                      //  e.CellStyle.ForegroundImage = imageList1.Images[0];
                    }
                   
                }
            }
            catch { }
        }

        private void c1TrueDBGridTimeForAlarm_FetchCellStyle(object sender, C1.Win.C1TrueDBGrid.FetchCellStyleEventArgs e)
        {
            C1.Win.C1TrueDBGrid.C1TrueDBGrid tdbgrid = sender as C1.Win.C1TrueDBGrid.C1TrueDBGrid;

            if ((bool)tdbgrid[e.Row, "TimeWarningOn"])
            {
                e.CellStyle.BackColor = System.Drawing.Color.Orange;
                e.CellStyle.Alpha = 100;
                //  e.CellStyle.Font = myfont;
                if (e.Col == 1 )
                {
                    tdbgrid.Columns["TimeAlarmOn"].ValueItems.Translate = true;
                    tdbgrid.Columns["TimeAlarmOn"].ValueItems.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.Normal;
                    e.CellStyle.ForegroundImage = imageList1.Images[1];
                }

            }
            if ((bool)tdbgrid[e.Row, "TimeAlarmOn"])
            {
                e.CellStyle.BackColor = System.Drawing.Color.Red;
                e.CellStyle.Alpha = 100;
                //  e.CellStyle.Font = myfont;
                if ( e.Col==1)
                {
                    tdbgrid.Columns["TimeAlarmOn"].ValueItems.Translate = true;
                    tdbgrid.Columns["TimeAlarmOn"].ValueItems.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.Normal;
                    e.CellStyle.ForegroundImage = imageList1.Images[0];
                }

            }
        }

      

        

    }
}
