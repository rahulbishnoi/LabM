using System;
using System.Drawing;
using System.Windows.Forms;
using Logging;
using MySQL_Helper_Class;
using C1.Win.C1FlexGrid;

namespace LabManager
{
    public partial class Select_Form : Form
    {
        Point _pt;
        Routing_Form _parent;
        MySQL_HelperClass myHC = new MySQL_HelperClass();
        Routing.RoutingData routingData = new Routing.RoutingData();
        int _nType = -1;
        bool _bIsConditionTable = false;
        C1FlexGrid _parentGrid;
        Save mySave = new Save("Routing_Select_Form");

        public Select_Form(Routing_Form parent , Point pt, int nType_ID, C1FlexGrid parentGrid, bool bIsConditionTable=false)
        {
            Application.EnableVisualStyles();
            _pt = pt;
            _parent = parent;
            _nType = nType_ID;
            _parentGrid = parentGrid;
            _bIsConditionTable = bIsConditionTable;
            InitializeComponent();
         //   MessageBox.Show(_nCommandType.ToString() + " # " + _nRoutingCommand_ID.ToString());
            
        }

        private void Select_Form_Load(object sender, EventArgs e)
        {
            c1FlexGrid1.Clear();
            c1FlexGrid1.DataSource = null;
            c1FlexGrid1.ExtendLastCol = true;

            c1FlexGrid1.AutoResize = true;

            // add filter row
            FilterRow.FilterRow fr = new FilterRow.FilterRow(c1FlexGrid1);

            this.Location = new Point(_pt.X, _pt.Y);

            if (!_bIsConditionTable)
            {
                switch (_nType)
                {
                    case  (int)Definition.RoutingCommands.SHIFTSAMPLE: // shift sample
                    case  (int)Definition.RoutingCommands.CREATERESERVATION: // create reservation
                    case  (int)Definition.RoutingCommands.DELETERESERVATION: // delete reservation
                        if (_parentGrid.ColSel == 2) { LoadPositionsToTable(true); }
                        break;

                    case  (int)Definition.RoutingCommands.CREATESAMPLE: // create sample
                        if (_parentGrid.ColSel == 2) { LoadPositionsToTable(true); }
                        if (_parentGrid.ColSel == 3) { LoadSampleProgramsToTable(); }
                        break;

                    case  (int)Definition.RoutingCommands.WRITEGLOBALTAG: // write WinCC global tags
                        if (_parentGrid.ColSel == 4) { LoadWinCCGlobalTagsToTable(); }
                        break;

                    case  (int)Definition.RoutingCommands.WRITEMACHINETAG: // write WinCC machine tags
                        if (_parentGrid.ColSel == 4)
                        {
                            int nMachine_ID = -1;
                            try
                            {
                                Int32.TryParse(_parentGrid[_parentGrid.RowSel, 3].ToString(), out nMachine_ID);
                            }
                            catch { }
                            // MessageBox.Show(nMachine_ID.ToString());
                            LoadWinCCMachineTagsToTable(nMachine_ID);
                        }
                        break;

                    case (int)Definition.RoutingCommands.INSERTWSENTRY:    // INSERTWSENTRY  load position to table
                        // if (_parentGrid.ColSel == 3)
                        { LoadPositionsToTable(true); }
                        break;

                    case  (int)Definition.RoutingCommands.SENDINGCOMMANDTOMACHINE: // sending command to machine
                        if (_parentGrid.ColSel == 2) { LoadMachinesToTable(); }
                        if (_parentGrid.ColSel == 3) { LoadMachineCommandsToTable(); }
                        break;

                    default:
                        break;
                }
            }
            else
            {
                switch (_nType)
                {
                    case (int)Definition.RoutingConditions.SAMPLEONPOS: //  sample on pos
                    case (int)Definition.RoutingConditions.SAMPLEPRIORITY: // sample priority
                    case (int)Definition.RoutingConditions.SAMPLETYPE: // sample type
                        if (_parentGrid.ColSel == 3) { LoadPositionsToTable(true); }
                        break;
                }
            }
        }

        private void c1FlexGrid_Commands_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            C1FlexGrid grid = sender as C1FlexGrid;
            SetValue( grid);
              
        }

        private void SetValue(C1FlexGrid grid)
        {
            int nID = -1;
            bool bGot_ID = false;

            try
            {
                if (!_bIsConditionTable)
                {
                    if (grid.RowSel > 1)
                    {
                        switch (_nType)
                        {
                            case (int)Definition.RoutingCommands.SHIFTSAMPLE: // shift sample
                            case (int)Definition.RoutingCommands.CREATERESERVATION: // create reservation point
                            case (int)Definition.RoutingCommands.DELETERESERVATION: // delete reservation point  
                                bGot_ID = Int32.TryParse(grid[grid.RowSel, 1].ToString(), out nID);
                                _parentGrid.SetData(_parentGrid.RowSel, _parentGrid.ColSel, nID);
                                break;

                            case (int)Definition.RoutingCommands.CREATESAMPLE: // create sample
                                bGot_ID = Int32.TryParse(grid[grid.RowSel, 1].ToString(), out nID);
                                _parentGrid.SetData(_parentGrid.RowSel, _parentGrid.ColSel, nID);
                                break;

                            case (int)Definition.RoutingCommands.WRITEGLOBALTAG: // write WinCC global tag
                                // bGot_ID = Int32.TryParse(grid[grid.RowSel, 1].ToString(), out nID);
                                _parentGrid.SetData(_parentGrid.RowSel, _parentGrid.ColSel, grid[grid.RowSel, 2].ToString());
                                _parentGrid.SetData(_parentGrid.RowSel, 5, "");
                                _parent.SetInputFilterForWinCCTags(_parentGrid, _parentGrid.RowSel, _nType);
                                break;

                            case (int)Definition.RoutingCommands.WRITEMACHINETAG: // write WinCC machine tag
                                //bGot_ID = Int32.TryParse(grid[grid.RowSel, 1].ToString(), out nID);
                                _parentGrid.SetData(_parentGrid.RowSel, _parentGrid.ColSel, grid[grid.RowSel, 2].ToString());
                                _parentGrid.SetData(_parentGrid.RowSel, 5, "");
                                _parent.SetInputFilterForWinCCTags(_parentGrid, _parentGrid.RowSel, _nType);
                                break;

                            case (int)Definition.RoutingCommands.INSERTWSENTRY:    // INSERTWSENTRY  load position to table
                                bGot_ID = Int32.TryParse(grid[grid.RowSel, 1].ToString(), out nID);
                                _parentGrid.SetData(_parentGrid.RowSel, _parentGrid.ColSel, nID);
                                break;

                            case (int)Definition.RoutingCommands.SENDINGCOMMANDTOMACHINE: // sending command to machine
                                bGot_ID = Int32.TryParse(grid[grid.RowSel, 1].ToString(), out nID);
                                _parentGrid.SetData(_parentGrid.RowSel, _parentGrid.ColSel, nID);
                                break;

                            default:
                                break;
                        }
                    }
                }
                else //conditions
                {
                    switch (_nType)
                    {
                        case (int)Definition.RoutingConditions.SAMPLEONPOS: //  sample on pos
                        case (int)Definition.RoutingConditions.SAMPLEPRIORITY: // sample priority
                        case (int)Definition.RoutingConditions.SAMPLETYPE: // sample type
                            //bGot_ID = Int32.TryParse(grid[grid.RowSel, 1].ToString(), out nID);
                            if (grid.RowSel > 1)
                            {
                                string strID = grid[grid.RowSel, 1].ToString();
                                _parentGrid.SetData(_parentGrid.RowSel, _parentGrid.ColSel, strID);
                            }
                            break;
                    }
                }
            }
            catch { }

            Close();
        }

        private void LoadMachinesToTable()
        {
            c1FlexGrid1.DataSource = routingData.GetMachinesDataTable();
            c1FlexGrid1.Cols[1].Visible = false; //hide "id..."
        }
         private void LoadSampleProgramsToTable()
        {
            c1FlexGrid1.DataSource = routingData.GetSampleProgramsDataTable();
            c1FlexGrid1.Cols[1].Visible = false; //hide "id..."
        }
        

        private void LoadMachineCommandsToTable()
        {
            int nMachine_ID = -1;
            bool bGotMachineID = false;

            bGotMachineID = Int32.TryParse(_parentGrid[_parentGrid.RowSel, 2].ToString(), out nMachine_ID);
            if (bGotMachineID)
            {
                c1FlexGrid1.DataSource = routingData.GetMachineCommandsDataTable(nMachine_ID);
                c1FlexGrid1.Cols[1].Visible = false; //hide "id..."
            }
        }

        private void LoadPositionsToTable(bool bNoInternalPositions = false)
        {
            c1FlexGrid1.DataSource = routingData.GetMachinePositionDataTable(bNoInternalPositions);
            c1FlexGrid1.Cols[1].Visible = false; //hide "id..."
        }

        private void LoadWinCCGlobalTagsToTable()
        {
            c1FlexGrid1.DataSource = routingData.GetWinCCGlobalTagsDataTable();
            c1FlexGrid1.Cols[1].Visible = false; //hide "id..."
        }

        private void LoadWinCCMachineTagsToTable(int nMachine_ID)
        {
            c1FlexGrid1.DataSource = routingData.GetWinCCMachineTagsDataTable(nMachine_ID);
            c1FlexGrid1.Cols[1].Visible = false; //hide "id..."
        }


        private void c1FlexGrid1_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
           
            switch (e.KeyChar)
            {
                case (char)27: // Escape
                    e.Handled = true;
                    Close();
                    
                    break;
                case (char)13:  // Enter
                    e.Handled = true;
                    if (c1FlexGrid1.RowSel > 0)
                    {
                        SetValue(c1FlexGrid1);
                    }
                    break;
            }
           
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button_Select_Click(object sender, EventArgs e)
        {
            if (c1FlexGrid1.RowSel > 0)
            {
                SetValue(c1FlexGrid1);
            }
        }

        private void c1FlexGrid1_ChangeEdit(object sender, EventArgs e)
        {

        }

    }
}
