using System;
using System.Data;
using System.Drawing;
using Logging;
using Definition;
using System.Windows.Forms;
using MySQL_Helper_Class;
using MySql.Data.MySqlClient;
using System.Collections.Specialized;
using C1.Win.C1FlexGrid;
using Color_Helper;
using FlexGridHelper;
using CopyPaste;
using System.Runtime.InteropServices;
using PipesClientTest;
using System.Collections.Generic;

namespace LabManager
{
    public partial class Routing_Form : Form
    {
        MySQL_HelperClass myHC = new MySQL_HelperClass();
        Save mySave = new Save("Routing_Form");
        Routing.RoutingData routingData = new Routing.RoutingData();
        ColorHelper myColorHelper = new ColorHelper();
        private Definitions myDef = new Definitions();
        private ContextMenu contextMenuForTree = new ContextMenu();
        private bool expandLevelUnits, expandLevelPositions, expandLevelSampleTypes = false;
        int[] Unit_IDs = null;
        bool bCheckRoutingLines = false;
        bool bConditionTableChanged = false;
        bool bCommandTableChanged = false;
        MySqlDataAdapter da_Condition = new MySqlDataAdapter();
        MySqlDataAdapter da_Commands = new MySqlDataAdapter();
        DataSet ds_Condition = new DataSet();
        DataSet ds_Commands = new DataSet();
        DataSet ds_CommandCombobox = new DataSet();
        DataTable ConditionTable = null;
        DataTable CommandTable = null;
        int nSelectedRoutingPositionEntry_ID = -1;
        int nSelectedRoutingPosition_ID = -1;
        int nSelectedMachine_ID = -1;
        int nSelectedSampleType_ID = -1;
        int nLastSelectedRowConditions= -1;
        int nLastSelectedRowCommands = -1;
        int nCopiedPlaceID = -1;
       // int _nRoutingPositionEntries_ID = -1;
        int nRoutingPositionForTree = -1;
        TreeNode tn_LastSelected = null;
        C1FlexGrid flexEditorGlobalTags = null;
        C1FlexGrid flexEditorMachineTags = null;
        C1FlexGrid flexEditorSamplePositions = null;
        C1FlexGrid flexEditorMachines = null;
        C1FlexGrid flexEditorStatusBits = null;
        ListDictionary ld_Machines = null;
        ListDictionary ld_Machines_Condition = null;
        ListDictionary ld_Commands = null;
        ListDictionary ld_Positions = null;
        ListDictionary ld_Positions2 = null;
        ListDictionary ld_SamplePrograms = null;
        ListDictionary ld_Statusbits = null;
        ListDictionary ld_GlobalTags = null;
        ListDictionary ld_MachineTags = null;
        ListDictionary ld_SampleTypes = null;
        ListDictionary ld_SamplePosition = null;
        private bool bInputConditionOK = true;
        private bool bInputCommandOK = true;
        private string[][] nConditionInputArray;
        private string[][] nCommandInputArray;
        private int nSelectedRowCommands = -1;
        private int nSelectedColCommands = -1;
        private int nCoursorPositionCommands = -1;
        private MyCopyPaste myCopyPaste = new MyCopyPaste();
        private DataTable dt_Copy = new DataTable("Copy");
        private int[] nCopyInfoArray;
        private int nCopyPasteType = -1;
        
        public Routing_Form()
        {
            Application.EnableVisualStyles();
            this.StartPosition = FormStartPosition.CenterScreen;

            InitializeComponent();
    
       /*     int nFormWidth = 800;
            Int32.TryParse(myHC.GetUserInputDataByName("RoutingFormWidth", "800"), out nFormWidth);
            this.Width = nFormWidth;

            int nFormHeight = 600;
            Int32.TryParse(myHC.GetUserInputDataByName("RoutingFormHeight", "300"), out nFormHeight);
            this.Height = nFormHeight;

            int nSplitter1Distance = 200;
            Int32.TryParse(myHC.GetUserInputDataByName("RoutingFormSplitter1Distance", "250"), out nSplitter1Distance);
            splitContainer1.SplitterDistance = nSplitter1Distance;

            int nSplitter2Distance = 300;
            Int32.TryParse(myHC.GetUserInputDataByName("RoutingFormSplitter2Distance", "300"), out nSplitter2Distance);
            splitContainer2.SplitterDistance = nSplitter2Distance;

          */

          //  PopulateTree(treeView_routing.Nodes);
            //gets the Units for the selectbox to add a not existing unit to the tree
            //GetUnitsForTreeViewMenu();
        }

        public Routing_Form(int nRoutingPositionForTree)
        {
            this.nRoutingPositionForTree = nRoutingPositionForTree;

            Application.EnableVisualStyles();
            this.StartPosition = FormStartPosition.CenterScreen;

            InitializeComponent();

           
            // build the routing tree
           // PopulateTree(treeView_routing.Nodes);
            //gets the Units for the selectbox to add a not existing unit to the tree
         //   GetUnitsForTreeViewMenu();
        }

        private void Routing_Form_Load(object sender, EventArgs e)
        {

            int nFormWidth = 800;
            Int32.TryParse(myHC.GetUserInputDataByName("RoutingFormWidth", "800"), out nFormWidth);
            this.Width = nFormWidth;

            int nFormHeight = 600;
            Int32.TryParse(myHC.GetUserInputDataByName("RoutingFormHeight", "300"), out nFormHeight);
            this.Height = nFormHeight;

            int nSplitter1Distance = 200;
            Int32.TryParse(myHC.GetUserInputDataByName("RoutingFormSplitter1Distance", "250"), out nSplitter1Distance);
            splitContainer1.SplitterDistance = nSplitter1Distance;

            int nSplitter2Distance = 300;
            Int32.TryParse(myHC.GetUserInputDataByName("RoutingFormSplitter2Distance", "300"), out nSplitter2Distance);
            splitContainer2.SplitterDistance = nSplitter2Distance;

            routingData.createViewForRightTree();
            //build tree (right side)
            treeView2.Visible = false;
            PopulateTree(treeView_routing.Nodes);
            if (ribbonCheckBox1.Checked)
            {
                Populate(treeView2.Nodes);
                ribbonTextBox_Search.Enabled = true;
            }
            ribbonTextBox_Search.Enabled = false;
            CellStyle cs = c1FlexGrid_Conditions.Styles.Add("Added", c1FlexGrid_Conditions.Styles.Normal);
            cs.BackColor = SystemColors.Info;
            cs.Font = new Font(c1FlexGrid_Conditions.Font, FontStyle.Bold);

            cs = c1FlexGrid_Conditions.Styles.Add("Detached", c1FlexGrid_Conditions.Styles.Normal);
            cs.BackColor = SystemColors.Info;
            cs.ForeColor = Color.DarkGray;

            cs = c1FlexGrid_Conditions.Styles.Add("Modified", c1FlexGrid_Conditions.Styles.Normal); 
            cs.BackColor = Color.Gold;
           // cs.ForeColor = Color.Gold;
            cs.Font = new Font(c1FlexGrid_Conditions.Font, FontStyle.Bold);

            cs = c1FlexGrid_Conditions.Styles.Add("InputError", c1FlexGrid_Conditions.Styles.Normal);
            cs.BackColor = Color.Red;
            cs.Font = new Font(c1FlexGrid_Conditions.Font, FontStyle.Regular);


            CellStyle csCommands = c1FlexGrid_Commands.Styles.Add("Added", c1FlexGrid_Conditions.Styles.Normal);
            csCommands.BackColor = SystemColors.Info;
            csCommands.Font = new Font(c1FlexGrid_Conditions.Font, FontStyle.Bold);

            csCommands = c1FlexGrid_Commands.Styles.Add("Detached", c1FlexGrid_Conditions.Styles.Normal);
            csCommands.BackColor = SystemColors.Info;
            csCommands.ForeColor = Color.DarkGray;

            csCommands = c1FlexGrid_Commands.Styles.Add("Modified", c1FlexGrid_Conditions.Styles.Normal);
            csCommands.BackColor = Color.Gold;
            csCommands.Font = new Font(c1FlexGrid_Conditions.Font, FontStyle.Bold);

            csCommands = c1FlexGrid_Commands.Styles.Add("InputError", c1FlexGrid_Conditions.Styles.Normal);
            csCommands.BackColor = Color.Red;
            csCommands.Font = new Font(c1FlexGrid_Conditions.Font, FontStyle.Regular);


            ribbonCheckBox_CheckCondition.Checked = false;
           
            /* Adds the event and the event handler for the method that will 
             process the timer event to the timer. */
            myTimer.Tick += new EventHandler(TimerEventProcessor);

            // Sets the timer interval to 1 seconds.
            myTimer.Interval = 1000;
            myTimer.Start();

            nConditionInputArray = myHC.GetInputCheckArrayFromRoutingInputCheckByType("Condition");
            nCommandInputArray = myHC.GetInputCheckArrayFromRoutingInputCheckByType("Command");

     
            c1FlexGrid_Conditions.ExtendLastCol = true;
            c1FlexGrid_Commands.ExtendLastCol = true;

            c1FlexGrid_Conditions.DrawMode = DrawModeEnum.OwnerDraw;
            c1FlexGrid_Conditions.Renderer = new MyRenderer();
            c1FlexGrid_Conditions.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None;

            c1FlexGrid_Commands.DrawMode = DrawModeEnum.OwnerDraw;
            c1FlexGrid_Commands.Renderer = new MyRenderer();
            c1FlexGrid_Commands.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None;

       
            c1FlexGrid_Conditions.DataSource = null;
            c1FlexGrid_Conditions.Clear();
            c1FlexGrid_Commands.DataSource = null;
            c1FlexGrid_Commands.Clear();


            // make sure grid shows the errors
            c1FlexGrid_Conditions.ShowErrors = true;
            c1FlexGrid_Commands.ShowErrors = true;
            // make the grid look nice
            c1FlexGrid_Conditions.ShowCursor = true;
            c1FlexGrid_Commands.ShowCursor = true;

           /* styleCellDeactivated = c1FlexGrid_Conditions.Styles["Deactivated"];
            if (styleCellDeactivated == null)
            {
                styleCellDeactivated = c1FlexGrid_Conditions.Styles.Add("Deactivated");
                styleCellDeactivated.BackColor = Color.LightGray;
            }
           
            styleCellactive = c1FlexGrid_Conditions.Styles["active"];
            if (styleCellactive == null)
            {
                styleCellactive = c1FlexGrid_Conditions.Styles.Add("active");
                styleCellactive.BackColor = Color.White;
            }
            */
           
            //comboBox status
            comboBox_MachineList.Items.Clear();
           // comboBox_MachineList.Items.AddRange(routingData.GetMachineList());
            comboBox_MachineList.DataSource = routingData.GetMachinesDataTable();
            comboBox_MachineList.DisplayMember = "Name";
            comboBox_MachineList.ValueMember = "idmachines";

           
            //comboBox SampleType
            comboBox_SampleType.Items.Clear();
            comboBox_SampleType.Items.AddRange(routingData.GetSampleTypeList());
            comboBox_SampleType.DisplayMember = "Name";
            comboBox_SampleType.ValueMember = "idsample_type_list";

            //comboBox SamplePosition
            comboBox_SamplePos.Items.Clear();
            comboBox_SamplePos.Items.Add(myDef.SampleOnPosOccupiedWord);
            comboBox_SamplePos.Items.Add(myDef.SampleOnPosNOTOccupiedWord);

            //comboBox Command Machine list
            comboBox_Command_MachineList.Items.Clear();
            comboBox_Command_MachineList.DataSource = routingData.GetMachinesDataTable();
            comboBox_Command_MachineList.DisplayMember = "Name";
            comboBox_Command_MachineList.ValueMember = "idmachines";

            comboBox_Command_SampleProgram.Items.Clear();
            comboBox_Command_SampleProgram.Items.Add("own sample");
            comboBox_Command_SampleProgram.Items.Add("sample on position");

             //comboBox statusbits list
            comboBox_StatusBit.Items.Clear();
            comboBox_StatusBit.DisplayMember = "Name";
            comboBox_StatusBit.ValueMember = "idmachine_status_bits";
            comboBox_StatusBit.DataSource = routingData.GetStatusBitsDataTable();

            
            //comboBox StatusBit Value
            comboBox_StatusBit_Value.Items.Clear();
            comboBox_StatusBit_Value.Items.Add(myDef.FalseWord);
            comboBox_StatusBit_Value.Items.Add(myDef.TrueWord);

            // Global Tags
            flexEditorGlobalTags = new C1FlexGrid();
            flexEditorGlobalTags.Visible = false;
            flexEditorGlobalTags.Font = Font;
            flexEditorGlobalTags.Parent = this;
            flexEditorGlobalTags.DataSource = routingData.GetGlobalTagsDataTable();

            // MachinePos Tags
            flexEditorMachineTags = new C1FlexGrid();
            flexEditorMachineTags.Visible = false;
            flexEditorMachineTags.Font = Font;
            flexEditorMachineTags.Parent = this;
            flexEditorMachineTags.DataSource = routingData.GetMachineTagsDataTable();

            // SamplePosition Tags
            flexEditorSamplePositions = new C1FlexGrid();
            flexEditorSamplePositions.Visible = false;
            flexEditorSamplePositions.Font = Font;
            flexEditorSamplePositions.Parent = this;
            flexEditorSamplePositions.DataSource = routingData.GetSamplePositionDataTable();

             // Machines Tags
            flexEditorMachines = new C1FlexGrid();
            flexEditorMachines.Visible = false;
            flexEditorMachines.Font = Font;
            flexEditorMachines.Parent = this;
            flexEditorMachines.DataSource = routingData.GetMachinesDataTable();

             // Statusbits
            flexEditorStatusBits = new C1FlexGrid();
            flexEditorStatusBits.Visible = false;
            flexEditorStatusBits.Font = Font;
            flexEditorStatusBits.Parent = this;
            flexEditorStatusBits.DataSource = routingData.GetStatusBitsDataTable();
            

            LoadConditionsToGrid(-1);

            // load Machines Dictionary
            string SQL_Statement_Machines = GetSQL_StatementForMachines();
            DataSet MachineNames = new DataSet();
            MachineNames.Clear();
            MachineNames = myHC.GetDataSetFromSQLCommand(SQL_Statement_Machines);

            if (MachineNames.Tables[0] != null)
            {
                DataTable dt_Machines = MachineNames.Tables[0];
                ld_Machines = new ListDictionary();
               // ld_Machines.Add(0, "<Please select>");
                foreach (DataRow dr_Machines in dt_Machines.Rows)
                {
                    // ItemArray[0] = id
                    // ItemArray[1] = name
                    ld_Machines.Add((int)dr_Machines.ItemArray[0], dr_Machines.ItemArray[1]);
                }
            }

            // load Machine Commands Dictionary
            string SQL_Statement_Commands = GetSQL_StatementForMaschineCommands();
            DataSet CommandNames = new DataSet();
            CommandNames.Clear();
            CommandNames = myHC.GetDataSetFromSQLCommand(SQL_Statement_Commands);

            if (CommandNames.Tables[0] != null)
            {
                DataTable dt_Commands = CommandNames.Tables[0];
                ld_Commands = new ListDictionary();
                //ld_Commands.Add(0, "<Please select>");
                foreach (DataRow dr_Commands in dt_Commands.Rows)
                {
                    // ItemArray[0] = id
                    // ItemArray[1] = name
                    ld_Commands.Add((int)dr_Commands.ItemArray[0], dr_Commands.ItemArray[1]);

                }
            }

            // load Position Dictionary
            string SQL_Statement_Positions = GetSQL_StatementForMachinePositions();
            DataSet PositionNames = new DataSet();
            PositionNames.Clear();
            PositionNames = myHC.GetDataSetFromSQLCommand(SQL_Statement_Positions);

            if (CommandNames.Tables[0] != null)
            {
                DataTable dt_Positions = PositionNames.Tables[0];
                ld_Positions = new ListDictionary();
                ld_Positions2 = new ListDictionary();
                
                foreach (DataRow dr_Positions in dt_Positions.Rows)
                {
                    // ItemArray[0] = id
                    // ItemArray[1] = name
                    ld_Positions.Add((int)dr_Positions.ItemArray[0], dr_Positions.ItemArray[1] + "/" + dr_Positions.ItemArray[2]);
                    ld_Positions2.Add( dr_Positions.ItemArray[0].ToString(), dr_Positions.ItemArray[1] + "/" + dr_Positions.ItemArray[2]);
                }
            }

            // load SamplePrograms Dictionary
            string SQL_Statement_SamplePrograms = GetSQL_StatementForSamplePrograms();
            DataSet SamplePrograms = new DataSet();
            SamplePrograms.Clear();
            SamplePrograms = myHC.GetDataSetFromSQLCommand(SQL_Statement_SamplePrograms);

            if (CommandNames.Tables[0] != null)
            {
                DataTable dt_SamplePrograms = SamplePrograms.Tables[0];
                ld_SamplePrograms = new ListDictionary();
               // ld_SamplePrograms.Add(-1, "<Please select>");
                foreach (DataRow dr_SamplePrograms in dt_SamplePrograms.Rows)
                {
                    // ItemArray[0] = id
                    // ItemArray[1] = name
                    ld_SamplePrograms.Add((int)dr_SamplePrograms.ItemArray[0], dr_SamplePrograms.ItemArray[1]);
                }
            }

         
             
            // load GlobalTags Dictionary
            string SQL_Statement_GlobalTags = GetSQL_StatementForGlobalTagsDictionary();
            DataSet ds_Globaltags = new DataSet();
            ds_Globaltags.Clear();
            ds_Globaltags = myHC.GetDataSetFromSQLCommand(SQL_Statement_GlobalTags);

            if (ds_Globaltags.Tables[0] != null)
            {
                DataTable dt_GlobalTags = ds_Globaltags.Tables[0];
                ld_GlobalTags = new ListDictionary();
             //   ld_GlobalTags.Add(0, "<Please select>");
                foreach (DataRow dr_GlobalTags in dt_GlobalTags.Rows)
                {
                    // ItemArray[0] = id
                    // ItemArray[1] = name
                    ld_GlobalTags.Add((int)dr_GlobalTags.ItemArray[0], dr_GlobalTags.ItemArray[1]);
                }
            }

             // load MachineTags Dictionary
            string SQL_Statement_MachineTags = GetSQL_StatementForMachineTagsDictionary();
            DataSet ds_Machinetags = new DataSet();
            ds_Machinetags.Clear();
            ds_Machinetags = myHC.GetDataSetFromSQLCommand(SQL_Statement_MachineTags);

            if (ds_Machinetags.Tables[0] != null)
            {
                DataTable dt_MachineTags = ds_Machinetags.Tables[0];
                ld_MachineTags = new ListDictionary();
             //   ld_MachineTags.Add(0, "<Please select>");
                foreach (DataRow dr_MachineTags in dt_MachineTags.Rows)
                {
                    // ItemArray[0] = id
                    // ItemArray[1] = name
                    ld_MachineTags.Add((int)dr_MachineTags.ItemArray[0], dr_MachineTags.ItemArray[1]);
                }
            }

            // load Statusbit Dictionary
            string SQL_Statement_StatusBits = GetSQL_StatementForStatusbitsDictionary();
            DataSet ds_StatusBits = new DataSet();
            ds_StatusBits.Clear();
            ds_StatusBits = myHC.GetDataSetFromSQLCommand(SQL_Statement_StatusBits);

            if (ds_StatusBits.Tables[0] != null)
            {
                DataTable dt_StatusBits = ds_StatusBits.Tables[0];
                ld_Statusbits = new ListDictionary();
             //   ld_Statusbits.Add(0, "<Please select>");
                foreach (DataRow dr_StatusBits in dt_StatusBits.Rows)
                {
                    // ItemArray[0] = id
                    // ItemArray[1] = machine
                    // ItemArray[2] = statusbit name
                    int nStatusbit = (int)dr_StatusBits.ItemArray[0];
                    ld_Statusbits.Add(nStatusbit.ToString(), dr_StatusBits.ItemArray[1] + " / " + dr_StatusBits.ItemArray[2]);
                }
            }

            // load SampleType Dictionary
            string SQL_Statement_SampleTypes = GetSQL_StatementForSampleTypeDictionary();
            DataSet ds_SampleTypes = new DataSet();
            ds_SampleTypes.Clear();
            ds_SampleTypes = myHC.GetDataSetFromSQLCommand(SQL_Statement_SampleTypes);

            if (ds_SampleTypes.Tables[0] != null)
            {
                DataTable dt_SampleTypes = ds_SampleTypes.Tables[0];
                ld_SampleTypes = new ListDictionary();
              //  ld_SampleTypes.Add(0, "<Please select>");
                foreach (DataRow dr_SampleTypes in dt_SampleTypes.Rows)
                {
                    // ItemArray[0] = id
                    // ItemArray[1] = machine
                    // ItemArray[2] = statusbit name
                    int nStatusbit = (int)dr_SampleTypes.ItemArray[0];
                    ld_SampleTypes.Add(nStatusbit.ToString(), dr_SampleTypes.ItemArray[1] );
                }
            }

             ld_SamplePosition = new ListDictionary();
             //   ld_SamplePosition.Add(0, "<Please select>");
                ld_SamplePosition.Add((int)Definition.WSInsertLocation.OWNPOS, "own sample");
                ld_SamplePosition.Add((int)Definition.WSInsertLocation.SAMPLEONPOS, "sample on position");
                ld_SamplePosition.Add((int)Definition.WSInsertLocation.WRITETOPARENT, "write to Parent");
                ld_SamplePosition.Add((int)Definition.WSInsertLocation.OWNPOSHIDDEN, "own sample (hidden)");
                ld_SamplePosition.Add((int)Definition.WSInsertLocation.SAMPLEONPOSHIDDEN, "sample on position (hidden)");
                ld_SamplePosition.Add((int)Definition.WSInsertLocation.WRITETOPARENTHIDDEN, "write to Parent (hidden)");
            

            ld_Machines_Condition = GetMachinesAsListDictionaryMap();

           // SetConditionSaveButtonEnabled(false);
            bCheckRoutingLines = true;

            c1FlexGrid_Conditions.Cols[2].Width = 0;
            c1FlexGrid_Conditions.Cols[3].Width = 160;

            SetButtonEnabled(false);
            SetConditionSaveButtonEnabled(false);
            SetCommandSaveButtonEnabled(false);

           
                if(nSelectedRoutingPositionEntry_ID>0) SetButtonEnabled(true); 
               // if(bInputConditionOK) SetConditionSaveButtonEnabled(true);
              //  if (bInputCommandOK) SetCommandSaveButtonEnabled(true);
            
            if (treeView_routing.SelectedNode != null)
            {
                treeView_routing.SelectedNode.ExpandAll();
                LoadDataToGridsForSelectedNode(treeView_routing.SelectedNode);
                CheckRoutingLines();
            }

            comboBox_Choice.Enabled = false;
            button_AddChoise.Enabled = false;
            button_ColorPicker.Enabled = false;

            // disable ToolTips
            treeView_routing.ShowNodeToolTips = false;
            //  

            nCopyInfoArray = new int[5]{0,0,0,0,0};
        }


        private void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            if (!this.Visible) { return; }

            if (bCheckRoutingLines)
            {

                if (ribbonCheckBox_CheckCondition.Checked)
                {
                    CheckForSample();
                    CheckRoutingLines();
                }
            }
            
        }

        private void CheckRoutingLines()
        {
            C1FlexGrid grid = c1FlexGrid_Conditions;
            Image ConditionComplyImage = Image.FromFile(myDef.PicsPath + @"\miscellaneous\ConditionComply.ico");
            Image ConditionNOTComplyImage = Image.FromFile(myDef.PicsPath + @"\miscellaneous\ConditionNotComply.ico");
           
            grid.Styles.Normal.ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.RightCenter;

            if (!ribbonCheckBox_CheckCondition.Checked)
            {
                c1FlexGrid_ConditionsUpdate();
            }
            else
            {
                if (nSelectedRoutingPositionEntry_ID > 0)
                {
                    DataTable dt_ConditionFlex = (DataTable)grid.DataSource;

                    string SQL_Statement_Conditions = GetSQL_Statement_Conditions(nSelectedRoutingPositionEntry_ID);
                    DataSet ds_ConditionCheck = myHC.GetDataSetFromSQLCommand(SQL_Statement_Conditions);
                    DataTable dt_ConditionCheck;


                    if (ds_ConditionCheck.Tables[0] != null && dt_ConditionFlex.Rows.Count > 0)
                    {
                        if (ds_ConditionCheck.Tables[0].Rows.Count > 0)
                        {
                            dt_ConditionCheck = ds_ConditionCheck.Tables[0];
                            //  dt_ConditionCheck.RowChanged += new DataRowChangeEventHandler(f);

                            // c1FlexGrid_Conditions.DataSource = dt_ConditionCheck;

                            CellStyle cs_OK = grid.Styles.Add("Condion_OK");
                            cs_OK.BackColor = Color.LightGreen;

                            CellStyle cs_NOTOK = grid.Styles.Add("Condion_NOTOK");
                            cs_NOTOK.BackColor = Color.LightPink;

                            cs_OK = grid.Styles.Add("Condion_OK");
                            cs_NOTOK = grid.Styles.Add("Condion_NOTOK");

                            if (dt_ConditionCheck != null)
                            {
                                // MessageBox.Show(dt_ConditionFlex.Rows.Count.ToString());
                                for (int i = 0; i < dt_ConditionCheck.Rows.Count; i++)
                                {
                                    DataRow dr_Condition = dt_ConditionCheck.Rows[i]; //dt_ConditionCheck.Columns.Count
                                    CellRange rg = grid.GetCellRange((i + 1), 0, (i + 1), 0);
                                    try
                                    {

                                        if ((bool)dr_Condition["Condition_Comply"])
                                        {
                                            //rg.Style = grid.Styles["Condion_OK"];
                                            rg.Image = ConditionComplyImage;

                                        }
                                        else
                                        {
                                            // rg.Style = grid.Styles["Condion_NOTOK"];
                                            rg.Image = ConditionNOTComplyImage;
                                        }
                                    }
                                    catch { }

                                }
                            }
                        }
                    }
                }
            }
        }

        private void drChanged_Condition(object sender, DataRowChangeEventArgs e)
        {
           
            if (e.Action == DataRowAction.Change)
            {
                bConditionTableChanged=true;
               
            }
        }

        private void drChanged_Command(object sender, DataRowChangeEventArgs e)
        {
            if (e.Action == DataRowAction.Change)
            {
                bCommandTableChanged = true;
                c1FlexGrid_Commands.Invalidate();
            }
        }

        // to show if there is a sample on this point or not
        private void CheckForSample()
        {
            try
            {
                if (nSelectedRoutingPositionEntry_ID > 0)
                {
                    int nPos = routingData.GetPosition_IDFromRoutingPositionsByRoutingPositionEntry_ID(nSelectedRoutingPositionEntry_ID);
                    int nSampleType = routingData.GetSampleType_IDFromRoutingPositionsByPosition_ID(nPos);
                    string strSampleID = routingData.GetSampleIDFromsampleActiveByMachinePosition_ID(nPos);
                    if (strSampleID != null && nSampleType == nSelectedSampleType_ID)
                    {
                        WriteStatusbarRight("SampleID: " + strSampleID);
                    }
                    else
                    {
                        WriteStatusbarRight("");
                    }
                }
                else
                {
                    WriteStatusbarRight("");
                }
            }
            catch { }
        }

        private void ribbonCheckBox_CheckCondition_Click(object sender, EventArgs e)
        {
            CheckRoutingLines();

            if (ribbonCheckBox_CheckCondition.Checked)
            {
                SetButtonEnabled(false);
                SetConditionSaveButtonEnabled(false);
                SetCommandSaveButtonEnabled(false);
            }
            else
            {
                SetButtonEnabled(true);
                if (bInputConditionOK) SetConditionSaveButtonEnabled(true);
                if (bInputCommandOK) SetCommandSaveButtonEnabled(true);
            }
        }
      

        // check the events of the buttons in the toolbar at the top
        private void _toolbar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
          
          
        }

        private void ExpandTree_Click(object sender, EventArgs e)
        {
            
                if (!expandLevelUnits || !expandLevelPositions || !expandLevelSampleTypes)
                {
                    expandLevelUnits = expandLevelPositions = expandLevelSampleTypes = true;
                    UpdateTree();
                }
            
        }

        private void CollapseTree_Click(object sender, EventArgs e)
        {

            if (expandLevelUnits || expandLevelPositions || expandLevelSampleTypes)
            {
                expandLevelUnits = expandLevelPositions = expandLevelSampleTypes = false;
                UpdateTree();
            }

        }


        private void RelaodRoutingTableData_Click(object sender, EventArgs e)
        {
           PipeClient _pipeClient;
            _pipeClient = new PipeClient();
            _pipeClient.Send("ReloadRoutingTableData", "LMPipe", 1000);
            _pipeClient = null;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveTimeWarningAlarm();
            SaveConditions();
            SaveCommands();
            bInputCommandOK = true;
            bInputConditionOK = true;

        }


        private void Populate(TreeNodeCollection ParentNodes)
        {
          
            // build the routing tree
            treeView2.Nodes.Clear();
            int k = 0;
            DataTable dtUnits = new DataTable();
            string[] words = ribbonTextBox_Search.Text.Split(',');
            List<String> units = new List<string>();
            if (words.GetLength(0) > 1)
            {
                units.Add(words[1]);
                formRightTree(units,ParentNodes,words[0]);
            }
            else
            {
                DataTable dt_units = new DataTable();
                dt_units = routingData.GetDatatableForUnitsForSearch(words[0]);
                Unit_IDs = new int[dt_units.Rows.Count];
                foreach (DataRow dataRow_Units in dt_units.Rows) // every Unit
                {
                    int nMachine_ID = Int32.Parse(dataRow_Units.ItemArray[0].ToString());
                    string strName = myHC.GetNameFromID((int)Definition.SQLTables.MACHINES, nMachine_ID);
                    units.Add(strName);
                    Unit_IDs[k++] = nMachine_ID;
                    
                }

                formRightTree(units, ParentNodes, words[0]);
            }
        }

        private void formRightTree(List<String> units,TreeNodeCollection ParentNodes,String parm){

            TreeNode tn_ParentStations = ParentNodes.Add("Stations");
            tn_ParentStations.ImageIndex = tn_ParentStations.SelectedImageIndex = 28;

            foreach (String unit in units)
            {
                TreeNode tn_Units = null;
                tn_Units = tn_ParentStations.Nodes.Add(unit);
               // tn_Units.Tag = dataRow_Units;
                // if (expandLevelUnits) { tn_Units.Expand(); } else { tn_Units.Collapse(); }
                tn_Units.ImageIndex = tn_Units.SelectedImageIndex = 12;
            
    
                DataTable dtPositions = new DataTable();
                TreeNode tn_Positions = null;
                    dtPositions = routingData.getDataTableForRightTreePositions(myHC.GetIDFromName((int)Definition.SQLTables.MACHINES,unit),parm);
                    foreach (DataRow dataRowPositions in dtPositions.Rows) // every routing position
                    {
                        int nRouting_Position_ID = Int32.Parse(dataRowPositions.ItemArray[0].ToString());
                        int nMachine_Position_ID = Int32.Parse(dataRowPositions.ItemArray[1].ToString());
                        if (nMachine_Position_ID > 0)
                        {
                            string strPositionName = myHC.GetNameFromID((int)Definition.SQLTables.MACHINE_POSITIONS, nMachine_Position_ID);
                            tn_Positions = tn_Units.Nodes.Add(strPositionName);
                            tn_Positions.Tag = dataRowPositions;
                            tn_Positions.ImageIndex = tn_Positions.SelectedImageIndex = 2;
                            //  if (expandLevelPositions) { tn_Positions.Expand(); } else { tn_Positions.Collapse(); }

                            TreeNode tn_SampleTypes = null;

                            DataTable dtSampleTypes = new DataTable();
                            dtSampleTypes = routingData.GetDataTableForRightTreeSampleTypes(nRouting_Position_ID, parm, myHC.GetIDFromName((int)Definition.SQLTables.MACHINES, unit));
                            foreach (DataRow dataRowSampleTypes in dtSampleTypes.Rows) // every sample type
                            {
                                int nSampleType_ID = Int32.Parse(dataRowSampleTypes.ItemArray[0].ToString());
                                string strSampleTypeName = myHC.GetNameFromID((int)Definition.SQLTables.SAMPLE_TYPE_LIST, nSampleType_ID);
                                tn_SampleTypes = tn_Positions.Nodes.Add(strSampleTypeName);
                                tn_SampleTypes.Tag = dataRowSampleTypes;

                                if (nSampleType_ID == 1)
                                {
                                    tn_SampleTypes.ImageIndex = tn_SampleTypes.SelectedImageIndex = 21;
                                }
                                else if (nSampleType_ID == 2)
                                {
                                    tn_SampleTypes.ImageIndex = tn_SampleTypes.SelectedImageIndex = 22;
                                }
                                else
                                {
                                    tn_SampleTypes.ImageIndex = tn_SampleTypes.SelectedImageIndex = 23;
                                }

                                if (expandLevelSampleTypes) { tn_SampleTypes.Expand(); } else { tn_SampleTypes.Collapse(); }

                                TreeNode tn_RoutingEntries = null;
                                int nIndex = 0;
                                DataTable dtRoutingEntries = new DataTable();
                                dtRoutingEntries = routingData.GetDataTableForRoutingEntries(nRouting_Position_ID, nSampleType_ID);
                                foreach (DataRow dataRowRoutingEntries in dtRoutingEntries.Rows) // every condition entry
                                {
                                    nIndex++;
                                //    string strDescription = dataRowRoutingEntries.ItemArray[3].ToString();
                                    string nNodeName = "Condition: " + nIndex.ToString();
                              //      if (strDescription.Length > 0) { nNodeName = strDescription; }
                                    tn_RoutingEntries = tn_SampleTypes.Nodes.Add(nNodeName);
                                    tn_RoutingEntries.Tag = dataRowRoutingEntries;
                                    tn_RoutingEntries.ImageIndex = tn_RoutingEntries.SelectedImageIndex = 18;
                                    tn_RoutingEntries.Expand();

                                    try
                                    {
                                        int idRoutingPositionEntry = (int)dataRowRoutingEntries.ItemArray[0];
                                        if (idRoutingPositionEntry == nRoutingPositionForTree) { treeView_routing.SelectedNode = tn_RoutingEntries; }
                                    }
                                    catch { }

                                }
                            }
                        }
                    }
            }



        }
        

        //build the tree on the left hand site 
        private void PopulateTree(TreeNodeCollection ParentNodes)
        {

            TreeNode tn_ParentStations = ParentNodes.Add("Stations");
            tn_ParentStations.ImageIndex = tn_ParentStations.SelectedImageIndex = 28;
            
            // build the routing tree
           
            TreeNode tn_Units = null;
            TreeNode tn_Positions = null;
            TreeNode tn_SampleTypes = null;
            TreeNode tn_RoutingEntries = null;

            int k = 0;
            DataTable dtUnits = new DataTable();
           
            dtUnits = routingData.GetDataTableForUnits();
            Unit_IDs = new int[dtUnits.Rows.Count];
            foreach (DataRow dataRow_Units in dtUnits.Rows) // every Unit
            {
                int nMachine_ID = Int32.Parse(dataRow_Units.ItemArray[0].ToString());
                string strName = myHC.GetNameFromID((int)Definition.SQLTables.MACHINES, nMachine_ID);
                Unit_IDs[k++] = nMachine_ID;
                tn_Units = tn_ParentStations.Nodes.Add(strName);
                tn_Units.Tag = dataRow_Units;
               // if (expandLevelUnits) { tn_Units.Expand(); } else { tn_Units.Collapse(); }
                tn_Units.ImageIndex = tn_Units.SelectedImageIndex = 12;
                
            
                DataTable dtPositions = new DataTable();
                dtPositions = routingData.GetDataTableForPositions(nMachine_ID);
                foreach (DataRow dataRowPositions in dtPositions.Rows) // every routing position
                {
                    int nRouting_Position_ID = Int32.Parse(dataRowPositions.ItemArray[0].ToString());
                    int nMachine_Position_ID = Int32.Parse(dataRowPositions.ItemArray[1].ToString());
                    if (nMachine_Position_ID > 0)
                    {
                        string strPositionName = myHC.GetNameFromID((int)Definition.SQLTables.MACHINE_POSITIONS, nMachine_Position_ID);
                        tn_Positions = tn_Units.Nodes.Add(strPositionName);
                        tn_Positions.Tag = dataRowPositions;
                        tn_Positions.ImageIndex = tn_Positions.SelectedImageIndex = 2;
                      //  if (expandLevelPositions) { tn_Positions.Expand(); } else { tn_Positions.Collapse(); }
                       

                        DataTable dtSampleTypes = new DataTable();
                        dtSampleTypes = routingData.GetDataTableForSampleTypes(nRouting_Position_ID);
                        foreach (DataRow dataRowSampleTypes in dtSampleTypes.Rows) // every sample type
                        {
                            int nSampleType_ID = Int32.Parse(dataRowSampleTypes.ItemArray[0].ToString());
                            string strSampleTypeName = myHC.GetNameFromID((int)Definition.SQLTables.SAMPLE_TYPE_LIST, nSampleType_ID);
                            tn_SampleTypes = tn_Positions.Nodes.Add(strSampleTypeName);
                            tn_SampleTypes.Tag = dataRowSampleTypes;

                           /* string strTypename = "nSampleType_ID" + nSampleType_ID;
                            if (!treeView_routing.ImageList.Images.ContainsKey(strTypename))
                            {
                                Image bm = null;
                                Size size = new Size(12, 12);
                                bm = myColorHelper.GetCircleBitmap(size, Color.Black);
                                treeView_routing.ImageList.Images.Add(strTypename, bm);
                               
                            }
                            tn_SampleTypes.ImageIndex = treeView_routing.ImageList.Images.Count - 1;
                            tn_SampleTypes.ImageKey = strTypename;*/
                            if (nSampleType_ID == 1)
                            {
                                tn_SampleTypes.ImageIndex = tn_SampleTypes.SelectedImageIndex = 21;             
                            }
                            else if (nSampleType_ID == 2)
                            {
                                tn_SampleTypes.ImageIndex = tn_SampleTypes.SelectedImageIndex = 22;
                            }
                            else
                            {
                                tn_SampleTypes.ImageIndex = tn_SampleTypes.SelectedImageIndex = 23;
                            }

                            if (expandLevelSampleTypes) { tn_SampleTypes.Expand(); } else { tn_SampleTypes.Collapse(); }

                            int nIndex = 0;
                            DataTable dtRoutingEntries = new DataTable();
                            dtRoutingEntries = routingData.GetDataTableForRoutingEntries(nRouting_Position_ID, nSampleType_ID);
                            foreach (DataRow dataRowRoutingEntries in dtRoutingEntries.Rows) // every condition entry
                            {
                                nIndex++;
                                string strDescription = dataRowRoutingEntries.ItemArray[3].ToString();
                                string nNodeName = "Condition: " + nIndex.ToString();
                                if (strDescription.Length > 0) { nNodeName =  strDescription; }
                                tn_RoutingEntries = tn_SampleTypes.Nodes.Add(nNodeName);
                                tn_RoutingEntries.Tag = dataRowRoutingEntries;
                                tn_RoutingEntries.ImageIndex =  tn_RoutingEntries.SelectedImageIndex = 18;
                                tn_RoutingEntries.Expand();
                            
                                try
                                {
                                    int idRoutingPositionEntry = (int)dataRowRoutingEntries.ItemArray[0];
                                    if (idRoutingPositionEntry == nRoutingPositionForTree) { treeView_routing.SelectedNode = tn_RoutingEntries; }
                                }
                                catch { }
                               
                            }
                        }
                    }  
                 }
                if (expandLevelUnits) { tn_Units.ExpandAll(); } else { tn_Units.Collapse(); }
                tn_ParentStations.Expand();
                if (nSelectedMachine_ID == nMachine_ID)
                {
                    ExpandNodeInTreeByNode(tn_Units); 
                }
            }
           
        }

        private void SaveTimeWarningAlarm()
        {
            if (nSelectedRoutingPositionEntry_ID > 0)
            {
                try
                {
                    int nTimeWarning = (int)ribbonNumericBox_Warning.Value;
                  //  int nTimeWarning = Int32.Parse(c1TextBox_Time_Warning.Text);
                    int nTimeAlarm = (int)ribbonNumericBox_Alarm.Value;
                //    int nTimeAlarm = Int32.Parse(c1TextBox_Time_Alarm.Text);
                    routingData.SaveTimeWarningAlarmInDB(nTimeWarning, nTimeAlarm, nSelectedRoutingPositionEntry_ID);
                   // UpdateTree();
                }
                catch (Exception ex) { mySave.InsertRow((int)Definition.Message.D_ALARM, ex.ToString()); }
            }
        }

    private void treeView_routing1_MouseUp(object sender, MouseEventArgs e) {

        if (!AskForDataSave()) { return; }

        Point pt = new Point(e.X, e.Y);
        treeView2.PointToClient(pt);

        // reset all tree information 
        nSelectedRoutingPositionEntry_ID = -1;
        nSelectedSampleType_ID = -1;
        nSelectedRoutingPosition_ID = -1;
        nSelectedMachine_ID = -1;



        // get the new tree infos
        TreeNode Node = treeView2.GetNodeAt(pt);

        if (Node != null)
        {
            if (Node.Level == 4) //conditions
            {
                DataRow dr_NodeLevel1 = (DataRow)Node.Parent.Parent.Parent.Tag;
                DataRow dr_NodeLevel2 = (DataRow)Node.Parent.Parent.Tag;
                DataRow dr_NodeLevel3 = (DataRow)Node.Parent.Tag;
                DataRow dr_NodeLevel4 = (DataRow)Node.Tag;

                try
                {
                    Int32.TryParse(dr_NodeLevel4[0].ToString(), out nSelectedRoutingPositionEntry_ID);
                    Int32.TryParse(dr_NodeLevel3[0].ToString(), out nSelectedSampleType_ID);
                    Int32.TryParse(dr_NodeLevel2[0].ToString(), out nSelectedRoutingPosition_ID);
                    Int32.TryParse(dr_NodeLevel1[0].ToString(), out nSelectedMachine_ID);

                }
                catch { }

            }
            else
                if (Node.Level == 3) //sample types
                {
                    DataRow dr_NodeLevel1 = (DataRow)Node.Parent.Parent.Tag;
                    DataRow dr_NodeLevel2 = (DataRow)Node.Parent.Tag;
                    DataRow dr_NodeLevel3 = (DataRow)Node.Tag;
                    try
                    {
                        Int32.TryParse(dr_NodeLevel3[0].ToString(), out nSelectedSampleType_ID);
                        Int32.TryParse(dr_NodeLevel2[0].ToString(), out nSelectedRoutingPosition_ID);
                        Int32.TryParse(dr_NodeLevel1[0].ToString(), out nSelectedMachine_ID);
                    }
                    catch { }

                }
                else
                    if (Node.Level == 2) //positions 
                    {
                        DataRow dr_NodeLevel1 = (DataRow)Node.Parent.Tag;
                        DataRow dr_NodeLevel2 = (DataRow)Node.Tag;

                        try
                        {
                            Int32.TryParse(dr_NodeLevel2[0].ToString(), out nSelectedRoutingPosition_ID);
                            Int32.TryParse(dr_NodeLevel1[0].ToString(), out nSelectedMachine_ID);
                        }
                        catch { }
                    }
                    else
                        if (Node.Level == 1) //stations 
                        {
                            DataRow dr_NodeLevel1 = (DataRow)Node.Tag;

                            try
                            {
                                Int32.TryParse(dr_NodeLevel1[0].ToString(), out nSelectedMachine_ID);
                            }
                            catch { }
                        }
        }

        //   Console.WriteLine("nSelectedRoutingPositionEntry_ID:" + nSelectedRoutingPositionEntry_ID + " - nSelectedSampleType_ID: " + nSelectedSampleType_ID + " - nSelectedRoutingPosition_ID: " + nSelectedRoutingPosition_ID + " - nSelectedMachine_ID: " + nSelectedMachine_ID);


        //  if (e.Button == MouseButtons.Left)
        {
            SetButtonEnabled(false);
            //  SetConditionSaveButtonEnabled(false);
            ds_Condition.Clear();
            ds_Commands.Clear();
            LoadTimesForWarningAlarm(null);


            if (Node != null)
            {
                if (Node.Level == 4) //conditions
                {
                    LoadDataToGridsForSelectedNode(Node);
                    try
                    {
                        c1FlexGrid_Conditions.Select(0, 0, false);
                        c1FlexGrid_Commands.Select(0, 0, false);
                    }
                    catch { }
                }
                CheckForSample();
            }
            if (ribbonCheckBox_CheckCondition.Checked)
            {
                CheckRoutingLines();
            }
        }


        if (e.Button == MouseButtons.Right)
        {
            if (Node != null)
            {
                if (Node.Level == 0) // root stations
                {
                    if (Node.Bounds.Contains(pt))
                    {
                        treeView2.SelectedNode = Node;
                        contextMenuForTree.MenuItems.Clear();

                        MenuItem newLevel0 = contextMenuForTree.MenuItems.Add("New station");
                        DataTable dtUnitsForTreeViewMenu = new DataTable();
                        dtUnitsForTreeViewMenu = routingData.GetUnitsForTreeViewMenu();
                        int[] nMachineID = new int[dtUnitsForTreeViewMenu.Rows.Count];
                        int n = 0;

                        foreach (DataRow dataRowUnitsForTreeViewMenu in dtUnitsForTreeViewMenu.Rows) // every routing position
                        {
                            int nMachine_ID = Int32.Parse(dataRowUnitsForTreeViewMenu.ItemArray[0].ToString());
                            if (!checkIFUnitIsInTreeAllready(nMachine_ID))
                            {
                                nMachineID[n++] = nMachine_ID;
                                string strUnit = dataRowUnitsForTreeViewMenu.ItemArray[1].ToString();

                                MenuItem mi = newLevel0.MenuItems.Add(strUnit, new EventHandler(New_Tree_Click_Level_Units));
                                mi.Tag = dataRowUnitsForTreeViewMenu;
                                if (checkIFUnitIsInTreeAllready(nMachine_ID)) { mi.Enabled = false; }
                            }
                        }
                        contextMenuForTree.Show(treeView2, pt);
                        contextMenuForTree.Tag = newLevel0.Tag = Node;
                    }
                }

                if (Node.Level == 1)
                {
                    if (Node.Bounds.Contains(pt))
                    {
                        treeView2.SelectedNode = Node;
                        contextMenuForTree.MenuItems.Clear();

                        /*
                                                    ToolStripMenuItem newLevel0 = new ToolStripMenuItem("New Position");
                                                    newLevel0.Checked = true;
                                                    newLevel0.Image = _imgList.Images[0];
                                                    newLevel0.Tag = newLevel0.Tag = Node;
                                                    newLevel0.Click += new EventHandler(Delete_Tree_Click);

                                                    ContextMenuStrip checkImageContextMenuStrip = new ContextMenuStrip();
                                                    checkImageContextMenuStrip.Items.Add(newLevel0);
                                                    checkImageContextMenuStrip.Items.Add("-");
                                                    checkImageContextMenuStrip.Items.Add("Delete", _imgList.Images[1], new EventHandler(Delete_Tree_Click));
                                                    checkImageContextMenuStrip.Visible = true;
                                                    checkImageContextMenuStrip.Show(treeView_routing, pt);
                                                    */
                        contextMenuForTree.MenuItems.Add("Expand", new EventHandler(Expand_Stations_Tree_Click));
                        contextMenuForTree.MenuItems.Add("-");

                        MenuItem newLevel1 = contextMenuForTree.MenuItems.Add("New Position");
                        GetPositionsForTreeViewMenu(newLevel1, Node);

                        contextMenuForTree.MenuItems.Add("-");
                        MenuItem miCopy = contextMenuForTree.MenuItems.Add("Copy station", new EventHandler(Copy_Stations_Tree_Click));
                        miCopy.Enabled = false;
                        MenuItem mi = contextMenuForTree.MenuItems.Add("Paste station", new EventHandler(Paste_Stations_Tree_Click));
                        if (nCopyPasteType == (int)Definition.CopyPasteObjectType.ROUTINGFORM_STATION &&
                           nSelectedMachine_ID != nCopiedPlaceID
                            )
                        { mi.Enabled = true; }
                        else { mi.Enabled = false; }
                        contextMenuForTree.MenuItems.Add("-");
                        contextMenuForTree.MenuItems.Add("Delete", new EventHandler(Delete_Tree_Click));


                        contextMenuForTree.Show(treeView2, pt);
                        contextMenuForTree.Tag = newLevel1.Tag = Node;
                    }
                }
                if (Node.Level == 2)
                {
                    if (Node.Bounds.Contains(pt))
                    {
                        treeView2.SelectedNode = Node;
                        contextMenuForTree.MenuItems.Clear();
                        contextMenuForTree.MenuItems.Add("Expand", new EventHandler(Expand_Stations_Tree_Click));
                        contextMenuForTree.MenuItems.Add("-");

                        MenuItem newLevel2 = contextMenuForTree.MenuItems.Add("New Type");
                        GetSampleTypesForTreeViewMenu(newLevel2, Node);
                        contextMenuForTree.MenuItems.Add("-");
                        MenuItem miCopy = contextMenuForTree.MenuItems.Add("Copy position", new EventHandler(Copy_Positions_Tree_Click));
                        //  miCopy.Enabled = false;
                        MenuItem mi = contextMenuForTree.MenuItems.Add("Paste position", new EventHandler(Paste_Positions_Tree_Click));
                        if (nCopyPasteType == (int)Definition.CopyPasteObjectType.ROUTINGFORM_POSITION &&
                            nSelectedRoutingPosition_ID != nCopiedPlaceID
                            )
                        { mi.Enabled = true; }
                        else { mi.Enabled = false; }

                        contextMenuForTree.MenuItems.Add("-");
                        contextMenuForTree.MenuItems.Add("Delete", new EventHandler(Delete_Tree_Click));

                        contextMenuForTree.Show(treeView2, pt);
                        contextMenuForTree.Tag = newLevel2.Tag = Node;
                    }
                }
                if (Node.Level == 3) // sample types
                {
                    if (Node.Bounds.Contains(pt))
                    {
                        treeView2.SelectedNode = Node;
                        contextMenuForTree.MenuItems.Clear();
                        MenuItem newLevel3 = contextMenuForTree.MenuItems.Add("New Condition", new EventHandler(New_Tree_Click_Level_Units));
                        contextMenuForTree.MenuItems.Add("-");
                        contextMenuForTree.MenuItems.Add("Copy sample type", new EventHandler(Copy_SampleTypes_Tree_Click));
                        MenuItem mi = contextMenuForTree.MenuItems.Add("Paste sample type", new EventHandler(Paste_SampleTypes_Tree_Click));
                        int nSampleTypeCopy = nSelectedSampleType_ID;
                        if (dt_Copy.Rows.Count > 0)
                        {
                            Int32.TryParse(dt_Copy.Rows[0].ItemArray[1].ToString(), out nSampleTypeCopy);
                        }
                        if (nCopyPasteType == (int)Definition.CopyPasteObjectType.ROUTINGFORM_SAMPLETYPE)
                        { mi.Enabled = true; }
                        else { mi.Enabled = false; }

                        contextMenuForTree.MenuItems.Add("-");
                        contextMenuForTree.MenuItems.Add("Delete", new EventHandler(Delete_Tree_Click));


                        contextMenuForTree.Show(treeView2, pt);
                        contextMenuForTree.Tag = newLevel3.Tag = Node;
                    }
                }
                if (Node.Level == 4) //conditions
                {
                    if (Node.Bounds.Contains(pt))
                    {
                        treeView2.SelectedNode = Node;
                        contextMenuForTree.MenuItems.Clear();

                        contextMenuForTree.MenuItems.Add("Copy", new EventHandler(Copy_Conditions_Tree_Click));
                        MenuItem mi = contextMenuForTree.MenuItems.Add("Paste", new EventHandler(Paste_Conditions_Tree_Click));
                        if (nCopyPasteType == (int)Definition.CopyPasteObjectType.ROUTINGFORM_CONDITION &&
                           nSelectedRoutingPositionEntry_ID != nCopiedPlaceID
                            )
                        { mi.Enabled = true; }
                        else { mi.Enabled = false; }

                        contextMenuForTree.MenuItems.Add("-");
                        contextMenuForTree.MenuItems.Add("Rename", new EventHandler(Rename_Tree_Click));
                        contextMenuForTree.MenuItems.Add("-");
                        contextMenuForTree.MenuItems.Add("Delete", new EventHandler(Delete_Tree_Click));

                        contextMenuForTree.Show(treeView2, pt);
                        contextMenuForTree.Tag = Node;
                    }
                }
            }
        }


        CheckForErrorText("Condition");
        CheckForErrorText("Command");

    
    }
        // executed if you click on the tree 
     private void treeView_routing_MouseUp(object sender, MouseEventArgs e)
     {
        
         if (!AskForDataSave()) { return; }
     
            Point pt = new Point(e.X, e.Y);
            treeView_routing.PointToClient(pt);

         // reset all tree information 
            nSelectedRoutingPositionEntry_ID = -1;
            nSelectedSampleType_ID = -1;
            nSelectedRoutingPosition_ID = -1;
            nSelectedMachine_ID = -1;
            
        
            
         // get the new tree infos
            TreeNode Node = treeView_routing.GetNodeAt(pt);

            if (Node != null)
            {
                if (Node.Level == 4) //conditions
                {
                    DataRow dr_NodeLevel1 = (DataRow)Node.Parent.Parent.Parent.Tag;
                    DataRow dr_NodeLevel2 = (DataRow)Node.Parent.Parent.Tag;
                    DataRow dr_NodeLevel3 = (DataRow)Node.Parent.Tag;
                    DataRow dr_NodeLevel4 = (DataRow)Node.Tag;
                   
                   try
                    {
                        Int32.TryParse(dr_NodeLevel4[0].ToString(), out nSelectedRoutingPositionEntry_ID);
                        Int32.TryParse(dr_NodeLevel3[0].ToString(), out nSelectedSampleType_ID);
                        Int32.TryParse(dr_NodeLevel2[0].ToString(), out nSelectedRoutingPosition_ID);
                        Int32.TryParse(dr_NodeLevel1[0].ToString(), out nSelectedMachine_ID);
                       
                    }
                    catch { }
                  
                }
                else
                if (Node.Level == 3) //sample types
                {
                    DataRow dr_NodeLevel1 = (DataRow)Node.Parent.Parent.Tag;
                    DataRow dr_NodeLevel2 = (DataRow)Node.Parent.Tag;
                    DataRow dr_NodeLevel3 = (DataRow)Node.Tag;
                     try
                    {
                        Int32.TryParse(dr_NodeLevel3[0].ToString(), out nSelectedSampleType_ID);
                        Int32.TryParse(dr_NodeLevel2[0].ToString(), out nSelectedRoutingPosition_ID);
                        Int32.TryParse(dr_NodeLevel1[0].ToString(), out nSelectedMachine_ID);
                    }
                    catch { }
                    
                }
                else
                if (Node.Level == 2) //positions 
                {
                    DataRow dr_NodeLevel1 = (DataRow)Node.Parent.Tag;
                    DataRow dr_NodeLevel2 = (DataRow)Node.Tag;
                   
                    try
                    {
                        Int32.TryParse(dr_NodeLevel2[0].ToString(), out nSelectedRoutingPosition_ID);
                        Int32.TryParse(dr_NodeLevel1[0].ToString(), out nSelectedMachine_ID);
                    }
                    catch { }
                }
                else
                if (Node.Level == 1) //stations 
                {
                    DataRow dr_NodeLevel1 = (DataRow)Node.Tag;
                  
                    try
                    {
                        Int32.TryParse(dr_NodeLevel1[0].ToString(), out nSelectedMachine_ID);
                    }
                    catch { }
                }
            }
            
         //   Console.WriteLine("nSelectedRoutingPositionEntry_ID:" + nSelectedRoutingPositionEntry_ID + " - nSelectedSampleType_ID: " + nSelectedSampleType_ID + " - nSelectedRoutingPosition_ID: " + nSelectedRoutingPosition_ID + " - nSelectedMachine_ID: " + nSelectedMachine_ID);
      

          //  if (e.Button == MouseButtons.Left)
            {
                SetButtonEnabled(false);
                //  SetConditionSaveButtonEnabled(false);
                ds_Condition.Clear();
                ds_Commands.Clear();
                LoadTimesForWarningAlarm(null);

                
                if (Node != null)
                {
                    if (Node.Level == 4) //conditions
                    {
                        LoadDataToGridsForSelectedNode(Node);
                        try
                        {
                            c1FlexGrid_Conditions.Select(0, 0, false);
                            c1FlexGrid_Commands.Select(0, 0, false);
                        }
                        catch { }
                    }
                    CheckForSample();
                }
                if (ribbonCheckBox_CheckCondition.Checked)
                {
                    CheckRoutingLines();
                }
            }

        
         if (e.Button == MouseButtons.Right)
            {    
                if (Node != null )
                {
                    if (Node.Level == 0) // root stations
                    {
                        if (Node.Bounds.Contains(pt))
                        {
                            treeView_routing.SelectedNode = Node;
                            contextMenuForTree.MenuItems.Clear();

                            MenuItem newLevel0 = contextMenuForTree.MenuItems.Add("New station");
                            DataTable dtUnitsForTreeViewMenu = new DataTable();
                            dtUnitsForTreeViewMenu = routingData.GetUnitsForTreeViewMenu();
                            int[] nMachineID = new int[dtUnitsForTreeViewMenu.Rows.Count];
                            int n = 0;
                            
                            foreach (DataRow dataRowUnitsForTreeViewMenu in dtUnitsForTreeViewMenu.Rows) // every routing position
                            {
                                int nMachine_ID = Int32.Parse(dataRowUnitsForTreeViewMenu.ItemArray[0].ToString());
                                if (!checkIFUnitIsInTreeAllready(nMachine_ID))
                                {
                                    nMachineID[n++] = nMachine_ID;
                                    string strUnit = dataRowUnitsForTreeViewMenu.ItemArray[1].ToString();
                                    
                                    MenuItem mi = newLevel0.MenuItems.Add(strUnit, new EventHandler(New_Tree_Click_Level_Units));
                                    mi.Tag = dataRowUnitsForTreeViewMenu;
                                    if (checkIFUnitIsInTreeAllready(nMachine_ID)) { mi.Enabled = false; }
                                }
                            }
                            contextMenuForTree.Show(treeView_routing, pt);
                            contextMenuForTree.Tag = newLevel0.Tag = Node;
                        }
                    }

                    if (Node.Level == 1)
                    {
                        if (Node.Bounds.Contains(pt))
                        {
                            treeView_routing.SelectedNode = Node;
                            contextMenuForTree.MenuItems.Clear();

/*
                            ToolStripMenuItem newLevel0 = new ToolStripMenuItem("New Position");
                            newLevel0.Checked = true;
                            newLevel0.Image = _imgList.Images[0];
                            newLevel0.Tag = newLevel0.Tag = Node;
                            newLevel0.Click += new EventHandler(Delete_Tree_Click);

                            ContextMenuStrip checkImageContextMenuStrip = new ContextMenuStrip();
                            checkImageContextMenuStrip.Items.Add(newLevel0);
                            checkImageContextMenuStrip.Items.Add("-");
                            checkImageContextMenuStrip.Items.Add("Delete", _imgList.Images[1], new EventHandler(Delete_Tree_Click));
                            checkImageContextMenuStrip.Visible = true;
                            checkImageContextMenuStrip.Show(treeView_routing, pt);
                            */
                            contextMenuForTree.MenuItems.Add("Expand", new EventHandler(Expand_Stations_Tree_Click));
                            contextMenuForTree.MenuItems.Add("-");
                            
                            MenuItem newLevel1 = contextMenuForTree.MenuItems.Add("New Position");
                            GetPositionsForTreeViewMenu(newLevel1, Node);
                    
                            contextMenuForTree.MenuItems.Add("-");
                            MenuItem miCopy = contextMenuForTree.MenuItems.Add("Copy station", new EventHandler(Copy_Stations_Tree_Click));
                            miCopy.Enabled = false;
                            MenuItem mi = contextMenuForTree.MenuItems.Add("Paste station", new EventHandler(Paste_Stations_Tree_Click));
                            if (nCopyPasteType == (int)Definition.CopyPasteObjectType.ROUTINGFORM_STATION &&
                               nSelectedMachine_ID != nCopiedPlaceID
                                )
                            { mi.Enabled = true; }else { mi.Enabled = false; }
                            contextMenuForTree.MenuItems.Add("-");
                            contextMenuForTree.MenuItems.Add("Delete", new EventHandler(Delete_Tree_Click));
                   
                           
                            contextMenuForTree.Show(treeView_routing, pt);
                            contextMenuForTree.Tag = newLevel1.Tag = Node;
                        }
                    }
                    if (Node.Level == 2)
                    {
                        if (Node.Bounds.Contains(pt))
                        {
                            treeView_routing.SelectedNode = Node;
                            contextMenuForTree.MenuItems.Clear();
                            contextMenuForTree.MenuItems.Add("Expand", new EventHandler(Expand_Stations_Tree_Click));
                            contextMenuForTree.MenuItems.Add("-");
                         
                            MenuItem newLevel2 = contextMenuForTree.MenuItems.Add("New Type");
                            GetSampleTypesForTreeViewMenu(newLevel2, Node);
                            contextMenuForTree.MenuItems.Add("-");
                            MenuItem miCopy = contextMenuForTree.MenuItems.Add("Copy position", new EventHandler(Copy_Positions_Tree_Click));
                          //  miCopy.Enabled = false;
                            MenuItem mi = contextMenuForTree.MenuItems.Add("Paste position", new EventHandler(Paste_Positions_Tree_Click));
                            if (nCopyPasteType == (int)Definition.CopyPasteObjectType.ROUTINGFORM_POSITION &&
                                nSelectedRoutingPosition_ID != nCopiedPlaceID
                                ) 
                            { mi.Enabled = true; } else { mi.Enabled = false; }
                          
                            contextMenuForTree.MenuItems.Add("-");
                            contextMenuForTree.MenuItems.Add("Delete", new EventHandler(Delete_Tree_Click));
                           
                            contextMenuForTree.Show(treeView_routing, pt);
                            contextMenuForTree.Tag = newLevel2.Tag = Node;
                        }
                    }
                    if (Node.Level == 3) // sample types
                    {
                        if (Node.Bounds.Contains(pt))
                        {
                            treeView_routing.SelectedNode = Node;
                            contextMenuForTree.MenuItems.Clear();
                            MenuItem newLevel3 = contextMenuForTree.MenuItems.Add("New Condition", new EventHandler(New_Tree_Click_Level_Units));
                            contextMenuForTree.MenuItems.Add("-");
                            contextMenuForTree.MenuItems.Add("Copy sample type", new EventHandler(Copy_SampleTypes_Tree_Click));
                            MenuItem mi = contextMenuForTree.MenuItems.Add("Paste sample type", new EventHandler(Paste_SampleTypes_Tree_Click));
                            int nSampleTypeCopy = nSelectedSampleType_ID;
                            if (dt_Copy.Rows.Count > 0)
                            {
                                Int32.TryParse(dt_Copy.Rows[0].ItemArray[1].ToString(), out nSampleTypeCopy);
                            }
                            if (nCopyPasteType == (int)Definition.CopyPasteObjectType.ROUTINGFORM_SAMPLETYPE)
                            { mi.Enabled = true; } else { mi.Enabled = false; }
                          
                            contextMenuForTree.MenuItems.Add("-");
                            contextMenuForTree.MenuItems.Add("Delete", new EventHandler(Delete_Tree_Click));
                           

                            contextMenuForTree.Show(treeView_routing, pt);
                            contextMenuForTree.Tag = newLevel3.Tag = Node;
                        }
                    }
                    if (Node.Level == 4) //conditions
                    {
                        if (Node.Bounds.Contains(pt))
                        {
                            treeView_routing.SelectedNode = Node;
                            contextMenuForTree.MenuItems.Clear();
                
                            contextMenuForTree.MenuItems.Add("Copy", new EventHandler(Copy_Conditions_Tree_Click));
                            MenuItem mi = contextMenuForTree.MenuItems.Add("Paste", new EventHandler(Paste_Conditions_Tree_Click));
                            if (nCopyPasteType == (int)Definition.CopyPasteObjectType.ROUTINGFORM_CONDITION &&
                               nSelectedRoutingPositionEntry_ID  != nCopiedPlaceID
                                ) 
                            { mi.Enabled = true; } else { mi.Enabled = false; }
                          
                            contextMenuForTree.MenuItems.Add("-");
                            contextMenuForTree.MenuItems.Add("Rename", new EventHandler(Rename_Tree_Click));
                            contextMenuForTree.MenuItems.Add("-");
                            contextMenuForTree.MenuItems.Add("Delete", new EventHandler(Delete_Tree_Click));
                           
                            contextMenuForTree.Show(treeView_routing, pt);
                            contextMenuForTree.Tag = Node;
                        }
                    }
                }
            }

          
            CheckForErrorText("Condition");
            CheckForErrorText("Command");

        }

     private void LoadDataToGridsForSelectedNode(TreeNode selectedNode)
     {
         DataRow dr_Node = (DataRow)selectedNode.Tag;

         if (nSelectedRoutingPositionEntry_ID > 0)
         {
             if (!ribbonCheckBox_CheckCondition.Checked) { SetButtonEnabled(true); }
             LoadConditionsToGrid(nSelectedRoutingPositionEntry_ID);
             LoadTimesForWarningAlarm(dr_Node);
           
         }
     }

     private void LoadDataToGridsForSelectedNode()
     {
        
         if (nSelectedRoutingPositionEntry_ID > 0)
         {
             if (!ribbonCheckBox_CheckCondition.Checked) { SetButtonEnabled(true); }
             LoadConditionsToGrid(nSelectedRoutingPositionEntry_ID);
         }
     }

     private void treeView_routing_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
     {
         if (e.Node != null)
         {
             if (e.Node.IsExpanded)
             {
                 //e.Node.Collapse();

             }
             else
             {
            //     e.Node.ExpandAll();

             }
         }
     }
        private void LoadConditionsToGrid(int nRoutingPositionEntries_ID)
        {
            
            string SQL_Statement_Conditions = GetSQL_Statement_Conditions(nRoutingPositionEntries_ID);
            da_Condition = myHC.GetAdapterFromSQLCommand(SQL_Statement_Conditions);
            da_Condition.ContinueUpdateOnError = true;
            da_Condition.AcceptChangesDuringUpdate = true;
            da_Condition.AcceptChangesDuringFill = true;
            da_Condition.FillLoadOption = LoadOption.OverwriteChanges;
            ds_Condition.Clear();
            ds_Commands.Clear();

            string SQL_Statement_Commands = GetSQL_Statement_Commands(nRoutingPositionEntries_ID);
            da_Commands = myHC.GetAdapterFromSQLCommand(SQL_Statement_Commands);
            da_Commands.AcceptChangesDuringUpdate = true;
            da_Commands.AcceptChangesDuringFill = true;
            da_Commands.ContinueUpdateOnError = true;
            da_Commands.FillLoadOption = LoadOption.OverwriteChanges;
            da_Commands.Fill(ds_Commands);
         
           // ds_Condition = myHC.GetDataSetFromSQLCommand(SQL_Statement_Conditions);
            da_Condition.Fill(ds_Condition);
            if (ds_Condition.Tables[0] != null)
            {
                ConditionTable = ds_Condition.Tables[0];
                CommandTable = ds_Commands.Tables[0];

                c1FlexGrid_Conditions.Cols[2].Caption = "Condition";
                if (ds_Condition.Tables[0].Rows.Count >= 0)
                {
                    
                    ConditionTable.AcceptChanges();
                    ConditionTable.RowChanged += new DataRowChangeEventHandler(drChanged_Condition);

                    c1FlexGrid_Conditions.DataSource = ConditionTable;

                    c1FlexGrid_ConditionsUpdate();

                    c1FlexGrid_Conditions.Cols[6].Visible = false; //hide "idrouting_conditions"
                    c1FlexGrid_Conditions.Cols[7].Visible = false; //hide "routingPositionEntry_ID"
                    c1FlexGrid_Conditions.Cols[8].Visible = false; //hide "Condition_Comply"
                    CreateSelectboxForConditions();
                    CreateSelectboxForOperations();
                    c1FlexGrid_Conditions.Cols[2].Caption = "Condition";
                    c1FlexGrid_Conditions.Cols[3].Caption = "";
                    c1FlexGrid_Conditions.Cols[4].Caption = "";
                    c1FlexGrid_Conditions.Cols[5].Caption = "";

                    // Commands

                  
                   
                    if (ds_Commands.Tables[0] != null)
                    {
                        
                        CommandTable.RowChanged += new DataRowChangeEventHandler(drChanged_Command);

                        c1FlexGrid_Commands.DataSource = CommandTable;
                        c1FlexGrid_Commands.Cols[6].Visible = false; //hide "idrouting_commands"
                        c1FlexGrid_Commands.Cols[7].Visible = false; //hide "RoutingPositionEntry_ID"
                        CreateSelectboxForCommands();

                        if (nRoutingPositionEntries_ID > 0)
                        {
                            c1FlexGrid_CommandsUpdate();
                        }

                        c1FlexGrid_Commands.Cols[2].Caption = "";
                        c1FlexGrid_Commands.Cols[3].Caption = "";
                        c1FlexGrid_Commands.Cols[4].Caption = "";
                        c1FlexGrid_Commands.Cols[5].Caption = "";

                    }
                }
            }
        }
        private void LoadTimesForWarningAlarm(DataRow dr_Node)
        {
            if (dr_Node != null)
            {
                try
                {
                    ribbonNumericBox_Warning.Value = (int)dr_Node[4];
                    ribbonNumericBox_Alarm.Value = (int)dr_Node[5];
                }
                catch 
                {

                }
            }
            else
            {
                ribbonNumericBox_Warning.Value = 0;
                ribbonNumericBox_Alarm.Value = 0;
            }
        }

        private string GetSQL_Statement_Conditions(int nRoutingPositionEntries_ID)
        {
            string strSQL_String = null;
            if (ribbonCheckBox_CheckCondition.Checked)
            {
                strSQL_String = @"SELECT ConditionList_ID AS 'Condition',actualValue,ValueName,Operation_ID AS 'Operation',Value,idrouting_conditions,routingPositionEntry_ID,Condition_Comply,Description
                            FROM routing_conditions WHERE routingPositionEntry_ID=" + nRoutingPositionEntries_ID + " ORDER BY SortOrder";
            }
            else
            {
                strSQL_String = @"SELECT ConditionList_ID AS 'Condition',actualValue,ValueName,Operation_ID AS 'Operation',Value,idrouting_conditions,routingPositionEntry_ID,0 AS Condition_Comply,Description
                            FROM routing_conditions WHERE routingPositionEntry_ID=" + nRoutingPositionEntries_ID + " ORDER BY SortOrder";
            }
            return strSQL_String;
        }

        private string GetSQL_Statement_Commands(int nRoutingPositionEntries_ID)
        {
            string strSQL_String = null;
            strSQL_String = @"SELECT Command_ID AS 'Command',CommandValue0 AS 'Value0',CommandValue1 AS 'Value1',CommandValue2 AS 'Value2',CommandValue3 AS 'Value3',idrouting_commands,RoutingPositionEntry_ID,Description
                             FROM routing_commands WHERE routingPositionEntry_ID=" + nRoutingPositionEntries_ID + " ORDER BY SortOrder"; ;
            return strSQL_String;
        }

        private string GetSQL_StatementForCommandComboBox()
        {
            string strSQL_String = null;
            strSQL_String = "SELECT Name,idrouting_command_list FROM routing_command_list";
            return strSQL_String;
        }

        private string GetSQL_StatementForMachinesComboBox()
        {
            string strSQL_String = null;
            strSQL_String = "SELECT Name,idmachines AS 'id' FROM machines";
            return strSQL_String;
        }
        private string GetSQL_StatementForMachinePosComboBox()
        {
            string strSQL_String = null;
            strSQL_String = "SELECT Name,idmachine_positions AS 'id' FROM machine_positions";
            return strSQL_String;
        }

        private void CreateSelectboxForConditions()
        {
            //
            //Condition SelectBox
            //
            string SQL_Statement_Condition = GetSQL_StatementForCondtions();
            DataSet ConditionNames = new DataSet();
            ConditionNames.Clear();
            ConditionNames = myHC.GetDataSetFromSQLCommand(SQL_Statement_Condition);

            if (ConditionNames.Tables[0] != null)
            {
                DataTable dt_Conditions = ConditionNames.Tables[0];

                //StringBuilder sb = new StringBuilder();
                ListDictionary ld_Conditions = new ListDictionary();
                ld_Conditions.Add(0, "<Please select>");
                foreach (DataRow dr_Condition in dt_Conditions.Rows)
                {
                    // dr_Condition.ItemArray[0] = idrouting_Condition_list
                    // dr_Condition.ItemArray[1] = name
                    ld_Conditions.Add((int)dr_Condition.ItemArray[0], dr_Condition.ItemArray[1]);
                }
                c1FlexGrid_Conditions.Cols[1].DataMap = ld_Conditions;
            }
        }
        private string GetSQL_StatementForCondtions()
        {
            string strSQL_String = null;
            strSQL_String = "SELECT idrouting_conditions_list,Name FROM routing_condition_list ORDER BY Name";
            return strSQL_String;
        }

        private string GetSQL_StatementForMaschineCommands()
        {
            string strSQL_String = null;
            strSQL_String = "SELECT idmachine_commands,Name FROM machine_commands ORDER BY Name";
            return strSQL_String;
        }
        
        private void CreateSelectboxForOperations()
          {
              //
              //Operation SelectBox
              //
              string SQL_Statement_Operations = GetSQL_StatementForOperations();
              DataSet OperationNames = new DataSet();
              OperationNames.Clear();
              OperationNames = myHC.GetDataSetFromSQLCommand(SQL_Statement_Operations);
           
              if (OperationNames.Tables[0] != null)
              {
                  DataTable dt_Operations = OperationNames.Tables[0];
                  ListDictionary ld_Operations = new ListDictionary();
                //  ld_Operations.Add(0, "<Please select>");
                  foreach (DataRow dr_Operations in dt_Operations.Rows)
                  {
                      // dr_Operations.ItemArray[0] = idrouting_Condition_list
                      // dr_Operations.ItemArray[1] = name
                      ld_Operations.Add((int)dr_Operations.ItemArray[0], dr_Operations.ItemArray[1]);
                    
                  }
                  c1FlexGrid_Conditions.Cols[4].DataMap = ld_Operations;
              }

          }

        /* 
          private ListDictionary GetSelectboxForOperationsAsMap(int nCondition)
          {
              //
              //Operation SelectBox
              //
              ListDictionary ld_Operations = new ListDictionary();
              string SQL_Statement_Operations = GetSQL_StatementForOperations(nCondition);
              DataSet OperationNames = new DataSet();
              OperationNames.Clear();
              OperationNames = myHC.GetDataSetFromSQLCommand(SQL_Statement_Operations);

              if (OperationNames.Tables[0] != null)
              {
                  DataTable dt_Operations = OperationNames.Tables[0];
                  ld_Operations = new ListDictionary();
                 // ld_Operations.Add(0, "<Please select>");
                  foreach (DataRow dr_Operations in dt_Operations.Rows)
                  {
                      // dr_Operations.ItemArray[0] = idrouting_Condition_list
                      // dr_Operations.ItemArray[1] = name
                      ld_Operations.Add((int)dr_Operations.ItemArray[0], dr_Operations.ItemArray[1]);
                  }
                  //c1FlexGrid_Conditions.Cols[3].DataMap = ld_Operations;
              }
              return ld_Operations;
          }
          */
        private ListDictionary GetMachinesAsListDictionaryMap()
        {
            //
            // Machines SelectBox
            //
            ListDictionary ld_Machines = new ListDictionary();
                      
                DataTable dt_Machines = routingData.GetMachinesDataTable();
                ld_Machines = new ListDictionary();
           //     ld_Machines.Add(0, "<Please select>");
                foreach (DataRow dr_Machines in dt_Machines.Rows)
                {
                    int nID = (int)dr_Machines.ItemArray[0];
                    // dr_Machines.ItemArray[0] = idmachines
                    // dr_Machines.ItemArray[1] = Name
                    ld_Machines.Add(nID.ToString(), dr_Machines.ItemArray[1]);
                }
              
                return ld_Machines;
        }

        private string GetSQL_StatementForOperations(int nCondition=-1)
        {
            string  strSQL_String = "SELECT idrouting_operation_list,Name FROM routing_operation_list";
            if (nCondition >= 1)
            {
                switch (nCondition)
                {
                   /* case 0:
                        strSQL_String = "SELECT idrouting_operation_list,Name FROM routing_operation_list WHERE Condition_Equals=1";
                        break;
                    */
                    case 3: // machine sample free
                    case 7: // sample on position
                    case 8: // sample type
                        strSQL_String = "SELECT idrouting_operation_list,Name FROM routing_operation_list WHERE Condition_EqualsNotEquals=1";
                        break;
                    /*case 9: 
                        strSQL_String = "SELECT idrouting_operation_list,Name FROM routing_operation_list WHERE Condition_GreaterSmaller=1";
                        break;
                        */
                    default:
                        strSQL_String = "SELECT idrouting_operation_list,Name FROM routing_operation_list";
                        break;
                }
            }
            return strSQL_String;
        }
      
      
      
    /*    private void GetUnitsForTreeViewMenu()
        {
            DataTable dtUnitsForTreeViewMenu = new DataTable();
            dtUnitsForTreeViewMenu = routingData.GetUnitsForTreeViewMenu();
            int[] nMachineID = new int[dtUnitsForTreeViewMenu.Rows.Count];
            int n=0;
            comboBox_Units.Items.Clear();

            foreach (DataRow dataRowUnitsForTreeViewMenu in dtUnitsForTreeViewMenu.Rows) // every routing position
                {
                  int nMachine_ID = Int32.Parse(dataRowUnitsForTreeViewMenu.ItemArray[0].ToString());
                  if (!checkIFUnitIsInTreeAllready(nMachine_ID))
                  {
                      nMachineID[n++] = nMachine_ID;               
                      string strUnit = dataRowUnitsForTreeViewMenu.ItemArray[1].ToString();
                      comboBox_Units.Items.Add(strUnit);
                      
                  }         
                }
            comboBox_Units.Tag = nMachineID;         
        }
        */
       

        private bool checkIFUnitIsInTreeAllready(int nMachine_ID)
        {
            bool ret = false;
            if (Unit_IDs != null)
            {
                for (int i = 0; i < Unit_IDs.Length; i++)
                {
                    if (Unit_IDs[i] == nMachine_ID) { ret = true; return ret; }
                }
            }
            return ret;
        }

        private void GetPositionsForTreeViewMenu(Menu newLevel, TreeNode Node)
       // private void GetPositionsForTreeViewMenu(ToolStripMenuItem newLevel0, TreeNode Node)
        {
            
            DataTable dtPositionsForTreeViewMenu = new DataTable();
            DataRow dt_NodeTag = Node.Tag as DataRow;
            int nMachine_ID = Int32.Parse(dt_NodeTag.ItemArray[0].ToString());
            dtPositionsForTreeViewMenu = routingData.GetPositionsForTreeViewMenu(nMachine_ID);
            foreach (DataRow dataRowPositionsForTreeViewMenu in dtPositionsForTreeViewMenu.Rows) // every routing position
            {
                MenuItem mi = newLevel.MenuItems.Add(dataRowPositionsForTreeViewMenu.ItemArray[1].ToString(), new EventHandler(New_Tree_Click_Level_Units));
                mi.Tag = dataRowPositionsForTreeViewMenu;
                if (checkIFPositionIsInTreeAllready(Node, dataRowPositionsForTreeViewMenu)) { mi.Enabled = false; }
            }
        }

        private void GetStationsForTreeViewMenu(Menu newLevel0, TreeNode Node)
        {
            DataTable dtStationsForTreeViewMenu = new DataTable();
            DataRow dt_NodeTag = Node.Tag as DataRow;
            int nMachine_ID = Int32.Parse(dt_NodeTag.ItemArray[0].ToString());
            dtStationsForTreeViewMenu = routingData.GetUnitsForTreeViewMenu();
            foreach (DataRow dataRowStationsForTreeViewMenu in dtStationsForTreeViewMenu.Rows) // every routing position
            {
                MenuItem mi = newLevel0.MenuItems.Add(dataRowStationsForTreeViewMenu.ItemArray[1].ToString(), new EventHandler(New_Tree_Click_Level_Units));
                mi.Tag = dataRowStationsForTreeViewMenu;
                if (checkIFUnitIsInTreeAllready(nMachine_ID)) { mi.Enabled = false; }
            }
        }

        private bool checkIFPositionIsInTreeAllready(TreeNode Node, DataRow dr_Position)
        {
            bool ret = false;
            int nPosition_ID = Int32.Parse(dr_Position.ItemArray[0].ToString());
            TreeNodeCollection tnCol = Node.Nodes;
            for (int i = 0; i < tnCol.Count; i++)
            {
                DataRow dr_Child = tnCol[i].Tag as DataRow;
                int nChildPosition_ID = Int32.Parse(dr_Child.ItemArray[1].ToString());
                if (nPosition_ID == nChildPosition_ID) { ret = true; }
            }
            return ret;
        }
        private void GetSampleTypesForTreeViewMenu(Menu newLevel1, TreeNode Node)
        {
            DataTable dtSampleTypesForTreeViewMenu = new DataTable();
           
            dtSampleTypesForTreeViewMenu = routingData.GetSampleTypesForTreeViewMenu();
            foreach (DataRow dataRowSampleTypesForTreeViewMenu in dtSampleTypesForTreeViewMenu.Rows) // every routing position
            {
                
                {
                    // int nmachines_ID = Int32.Parse(dataRowPositionsForTreeViewMenu.ItemArray[0].ToString());
                    MenuItem mi = newLevel1.MenuItems.Add(dataRowSampleTypesForTreeViewMenu.ItemArray[1].ToString(), new EventHandler(New_Tree_Click_Level_Units));
                    mi.Tag = dataRowSampleTypesForTreeViewMenu;
                    if (checkIFSampleTypeIsInTreeAllready(Node, dataRowSampleTypesForTreeViewMenu)) { mi.Enabled = false; }
                }
            }
        }
        private bool checkIFSampleTypeIsInTreeAllready(TreeNode Node, DataRow dr_SampleType)
        {
            bool ret = false;
            int nSample_ID = Int32.Parse(dr_SampleType.ItemArray[0].ToString());
            TreeNodeCollection tnCol = Node.Nodes;
            for (int i = 0; i < tnCol.Count; i++)
            {
                DataRow dr_Child = tnCol[i].Tag as DataRow;
                int nChildSample_ID = Int32.Parse(dr_Child.ItemArray[0].ToString());
                if (nSample_ID == nChildSample_ID) { ret = true; }
            }
            return ret;
        }

        private void New_Tree_Click_Level_Units(object sender, EventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            Menu miParent = mi.Parent;
            TreeNode Node = (TreeNode)miParent.Tag;

            if (Node.Level == 0)    // stations 
            {
                DataRow dr_MenuItem = (DataRow)mi.Tag;
                int nMachinePosition_ID = Int32.Parse(dr_MenuItem[0].ToString());
                int nMachine_ID = Int32.Parse(dr_MenuItem[0].ToString());
                if (nMachine_ID > 0)
                {
                    if (routingData.InsertNewUnitIntoRouting(nMachine_ID)) { UpdateTree(); }
                }
                
            }

            if (Node.Level == 1)    // units (insert positions)
            {
               // DataRow dr_Node = (DataRow)Node.Tag;
               // int nMachine_ID = Int32.Parse(dr_Node[0].ToString());
                DataRow dr_MenuItem = (DataRow)mi.Tag;
                int nMachinePosition_ID = Int32.Parse(dr_MenuItem[0].ToString());
                int nMachine_ID = Int32.Parse(dr_MenuItem[3].ToString());
                routingData.InsertPositionIntoRouting(nMachine_ID, nMachinePosition_ID);
            }
            if (Node.Level == 2)    // Positions (insert sample type)
            {
                // DataRow dr_Node = (DataRow)Node.Tag;
                // int nMachine_ID = Int32.Parse(dr_Node[0].ToString());
                DataRow dr_MenuItem = (DataRow)mi.Tag;
                DataRow dr_Machine = Node.Tag as DataRow;
                int nRoutingPosition_ID = Int32.Parse(dr_Machine.ItemArray[0].ToString());
                int nMachinePosition_ID = Int32.Parse(dr_Machine.ItemArray[1].ToString());
                dr_Machine = Node.Parent.Tag as DataRow;
                int nMachine_ID = Int32.Parse(dr_Machine.ItemArray[0].ToString());
                int nSampleType_ID = Int32.Parse(dr_MenuItem[0].ToString());
               // MessageBox.Show(nRoutingPosition_ID.ToString() +"#"+nMachinePosition_ID.ToString() + "#" + nMachine_ID.ToString() + "#" + nSampleType_ID.ToString());
                myHC.InsertNewEntryIntoRoutingEntries(nRoutingPosition_ID, nSampleType_ID);
               
            }
            if (Node.Level == 3)    // sample types (insert condition entries)
            {
                DataRow dr_Sampletype = Node.Tag as DataRow;
                int nSampleType_ID = Int32.Parse(dr_Sampletype.ItemArray[0].ToString());
                DataRow dr_RoutingPosition = Node.Parent.Tag as DataRow;
                int nRoutingPosition_ID = Int32.Parse(dr_RoutingPosition.ItemArray[0].ToString());
                // MessageBox.Show(nSampleType_ID.ToString() + "#" + nMachinePosition_ID.ToString());
                 myHC.InsertNewEntryIntoRoutingEntries(nRoutingPosition_ID, nSampleType_ID);
            }
            
           
            UpdateTree();
           
            //GetUnitsForTreeViewMenu();
        }

        private void Delete_Tree_Click(object sender, EventArgs e)
        {
           
            MenuItem mi = sender as MenuItem;
            ContextMenu cm =  mi.Parent as ContextMenu;     
            TreeNode Node = (TreeNode)cm.Tag;
            DataRow dr_Node = (DataRow)Node.Tag;
            string strQuestion = null;
            
            strQuestion = "Do you want to delete the entry?";
          
            if (MessageBox.Show(strQuestion, "Confirm Delete", System.Windows.Forms.MessageBoxButtons.OKCancel) == DialogResult.OK)
            {

                if (Node.Level == 1)    // units
                {
                    // delete childs
                    foreach (TreeNode childnode in Node.Nodes)
                    {
                        DeleteMachinePositions(childnode, true);
                    }
                    dr_Node = (DataRow)Node.Tag;
                    int nMachine_ID = Int32.Parse(dr_Node[0].ToString());
                    if (nMachine_ID > 0)
                    {
                        routingData.DeleteUnitFromRouting(nMachine_ID);
                    }
                }
                if (Node.Level == 2)    // machine Positions
                {
                    DeleteMachinePositions(Node, true);
                }
                if (Node.Level == 3)    // sample types
                {
                    DeleteSampleTypes(Node, true);
                }
                if (Node.Level == 4)    // condition entries
                {
                    DeleteConditionEntries(Node, true); 
                }
            }
            UpdateTree();
            //GetUnitsForTreeViewMenu();
        }


        private void DeleteMachinePositions(TreeNode Node, bool bDeleteChilds)
        {
            if (bDeleteChilds)
            {
                // delete childs
                foreach (TreeNode childnode in Node.Nodes)
                {
                    DeleteSampleTypes(childnode, bDeleteChilds);
                }
            }
            DataRow dr_Node = (DataRow)Node.Tag;
            int nRoutingPosition_ID = Int32.Parse(dr_Node[0].ToString());
            if (nRoutingPosition_ID > 0)
            {
                routingData.DeleteMachinePositionFromRouting(nRoutingPosition_ID);
            }
        }

        private void DeleteSampleTypes(TreeNode Node, bool bDeleteChilds)
        {
            DataRow dr_Node = (DataRow)Node.Parent.Tag;
            int nRoutingPosition_ID = Int32.Parse(dr_Node[0].ToString());
            dr_Node = (DataRow)Node.Tag;
            int nSampleType_ID = Int32.Parse(dr_Node[0].ToString());
            if (nRoutingPosition_ID > 0)
            {
                if (bDeleteChilds)
                {
                    // delete childs
                    foreach (TreeNode childnode in Node.Nodes)
                    {
                        DeleteConditionEntries(childnode, bDeleteChilds);
                    }
                }
                // delete sample type entry
                routingData.DeleteSampleTypeFromRouting(nSampleType_ID, nRoutingPosition_ID);
                
            }
        }

        private void DeleteConditionEntries(TreeNode Node,bool bDeleteChilds)
        {
            DataRow dr_Node = (DataRow)Node.Tag;
            int nRoutingPositionEntries_ID = Int32.Parse(dr_Node[0].ToString());
            if (nRoutingPositionEntries_ID > 0)
            {
                if (bDeleteChilds)
                {
                    // delete entries from "routing_conditions" table
                    routingData.DeleteEntriesFromRoutingConditionsByRoutingPositionEintry_ID(nRoutingPositionEntries_ID);

                    // delete entries from "routing_commands" table
                    routingData.DeleteEntriesFromRoutingCommandsByRoutingPositionEintry_ID(nRoutingPositionEntries_ID);
                }
                // delete entries from "routing_position_entries"
                routingData.DeleteConditionEntryFromRouting(nRoutingPositionEntries_ID);
            }
        }


        private void Copy_Conditions_Tree_Click(object sender, EventArgs e)
        {
            nCopyPasteType = (int)Definition.CopyPasteObjectType.ROUTINGFORM_CONDITION;
            nCopiedPlaceID = nSelectedRoutingPositionEntry_ID;
            dt_Copy.Clear();
          /*  MenuItem mi = sender as MenuItem;
            ContextMenu cm = mi.Parent as ContextMenu;
            TreeNode Node = (TreeNode)cm.Tag;
            DataRow dr_Node = (DataRow)Node.Tag;
            Console.WriteLine(dr_Node.ItemArray[0]);*/
           // myCopyPaste.CopyFromFlexGrid(c1FlexGrid_Conditions, dt_Copy , true);
            nCopyInfoArray = myCopyPaste.CopyInfoToIntArray(nSelectedRoutingPositionEntry_ID, nSelectedSampleType_ID, nSelectedRoutingPosition_ID, nSelectedMachine_ID, nCopyPasteType);
          
        }

        private void Paste_Conditions_Tree_Click(object sender, EventArgs e)
        {

            MenuItem mi = sender as MenuItem;
            ContextMenu cm = mi.Parent as ContextMenu;
            TreeNode Node = (TreeNode)cm.Tag;
            DataRow dr_Node = (DataRow)Node.Tag;
        //    Console.WriteLine(dr_Node.ItemArray[0]);

            myCopyPaste.PasteFromInfoArray(nCopyInfoArray, nSelectedRoutingPositionEntry_ID);
        }

        private void Copy_SampleTypes_Tree_Click(object sender, EventArgs e)
        {
           nCopyPasteType = (int)Definition.CopyPasteObjectType.ROUTINGFORM_SAMPLETYPE;
           nCopiedPlaceID = nSelectedSampleType_ID;
           nCopyInfoArray =  myCopyPaste.CopyInfoToIntArray(nSelectedRoutingPositionEntry_ID, nSelectedSampleType_ID, nSelectedRoutingPosition_ID, nSelectedMachine_ID, nCopyPasteType);
           /* dt_Copy = myCopyPaste.CopyToDataSet(nSelectedRoutingPosition_ID, nSelectedSampleType_ID, nSelectedRoutingPosition_ID, nSelectedMachine_ID, nCopyPasteType);
             if (dt_Copy != null)
             {
                 Console.WriteLine("Copy_SampleTypes_Tree_Click - dt_Copy.Rows.Count: " + dt_Copy.Rows.Count);
             }*/
        }

        private void Paste_SampleTypes_Tree_Click(object sender, EventArgs e)
        {

            MenuItem mi = sender as MenuItem;
            ContextMenu cm = mi.Parent as ContextMenu;
            TreeNode Node = (TreeNode)cm.Tag;
            /* DataRow dr_Node = (DataRow)Node.Tag;
            DataRow dr_NodeChild = (DataRow)Node.Nodes[0].Tag;
            int nRoutingPositionEntry_ID = -1;

            Int32.TryParse(dr_NodeChild.ItemArray[0].ToString(), out nRoutingPositionEntry_ID);
            */
           
            if (nCopyPasteType == (int)Definition.CopyPasteObjectType.ROUTINGFORM_SAMPLETYPE)
            {
              
                myCopyPaste.PasteFromInfoArray(nCopyInfoArray, nSelectedSampleType_ID);
                UpdateTree();
                LoadDataToGridsForSelectedNode();
                ExpandNodeInTreeByNode(Node);
            }
           
        }

        private void Copy_Positions_Tree_Click(object sender, EventArgs e)
        {
            nCopyPasteType = (int)Definition.CopyPasteObjectType.ROUTINGFORM_POSITION;
            nCopiedPlaceID = nSelectedRoutingPosition_ID;
            MenuItem mi = sender as MenuItem;
            ContextMenu cm = mi.Parent as ContextMenu;
            TreeNode Node = (TreeNode)cm.Tag;
            DataRow dr_Node = (DataRow)Node.Tag;
            
             nCopyInfoArray = myCopyPaste.CopyInfoToIntArray(nSelectedRoutingPositionEntry_ID, nSelectedSampleType_ID, nSelectedRoutingPosition_ID, nSelectedMachine_ID, nCopyPasteType);
            
        }

        private void Paste_Positions_Tree_Click(object sender, EventArgs e)
        {

            MenuItem mi = sender as MenuItem;
            ContextMenu cm = mi.Parent as ContextMenu;
            TreeNode Node = (TreeNode)cm.Tag;
            DataRow dr_Node = (DataRow)Node.Tag;
            if (nCopyPasteType == (int)Definition.CopyPasteObjectType.ROUTINGFORM_POSITION)
            {
                 myCopyPaste.PasteFromInfoArray(nCopyInfoArray, nSelectedRoutingPosition_ID);
                UpdateTree();
                ExpandNodeInTreeByNode(Node);
            }
        }

        private void Expand_Stations_Tree_Click(object sender, EventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            ContextMenu cm = mi.Parent as ContextMenu;
            TreeNode Node = (TreeNode)cm.Tag;

            ExpandNodeInTreeByNode(Node);
            
        }

        private void ExpandNodeInTreeByNode(TreeNode treeNode)
        {
            treeNode.Expand();
            foreach (TreeNode i in treeNode.Nodes)
            {
                ExpandNodeInTreeByNode(i);
            }

         /*   treeNode.Expand();

            TreeNodeCollection childNodesLevel1 = treeNode.Nodes;

            for (int l1 = childNodesLevel1.Count - 1; l1 >= 0; l1--)
            {
                childNodesLevel1[l1].Expand();

                TreeNodeCollection childNodesLevel2 = childNodesLevel1[l1].Nodes;
                if (childNodesLevel2 != null)
                {
                    for (int l2 = childNodesLevel2.Count - 1; l2 >= 0; l2--)
                    {
                        childNodesLevel2[l2].Expand();
                    }
                }
            } */
        }

        private void Copy_Stations_Tree_Click(object sender, EventArgs e)
        {
            nCopyPasteType = (int)Definition.CopyPasteObjectType.ROUTINGFORM_STATION;
            nCopiedPlaceID = nSelectedMachine_ID;
            MenuItem mi = sender as MenuItem;
            ContextMenu cm = mi.Parent as ContextMenu;
            TreeNode Node = (TreeNode)cm.Tag;
            DataRow dr_Node = (DataRow)Node.Tag;
            // myCopyPaste.CopyFromFlexGrid(c1FlexGrid_Conditions, dt_Copy, true);
        }

        private void Paste_Stations_Tree_Click(object sender, EventArgs e)
        {

            MenuItem mi = sender as MenuItem;
            ContextMenu cm = mi.Parent as ContextMenu;
            TreeNode Node = (TreeNode)cm.Tag;
            DataRow dr_Node = (DataRow)Node.Tag;
        

        }

        // edit the tree node
        private void Rename_Tree_Click(object sender, EventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            ContextMenu cm = mi.Parent as ContextMenu;
            TreeNode Node = (TreeNode)cm.Tag;
            DataRow dr_Node = (DataRow)Node.Tag;

            if (Node != null && Node.Parent != null)
            {
                if (Node.Level == 4)    // condition entries
                {
                    dr_Node = (DataRow)Node.Tag;
                    if (nSelectedRoutingPositionEntry_ID > 0)
                    {
                        treeView_routing.SelectedNode = Node;
                        treeView_routing.LabelEdit = true;
                        if (!Node.IsEditing)
                        {
                            Node.BeginEdit();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No tree node selected or selected node is a root node.\n" +
                   "Editing of root nodes is not allowed.", "Invalid selection");
            }
        }
       
        private void UpdateTree()
        {
            treeView_routing.Nodes.Clear();

            PopulateTree(treeView_routing.Nodes);
        }

        private void button_condition_Delete_Click(object sender, EventArgs e)
        {
            if (c1FlexGrid_Conditions.Rows.Fixed <= (c1FlexGrid_Conditions.RowSel-1) )
            {
               // return;
            }
            
            if (c1FlexGrid_Conditions.RowSel > 0)
            {
                DataRowView dr_delete = (DataRowView)c1FlexGrid_Conditions.Rows[c1FlexGrid_Conditions.RowSel].DataSource;
                if (MessageBox.Show("Do you want to delete the record?", "Confirm Delete", System.Windows.Forms.MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    try
                    {
                        c1FlexGrid_Conditions.RemoveItem(c1FlexGrid_Conditions.Row);
                        c1FlexGrid_Conditions.Update();
                    }
                    catch { }
                    
                    try
                    {
                        routingData.DeleteEntryFromRoutingCondition(Int32.Parse(dr_delete["idrouting_conditions"].ToString()));
                       
                    }
                    catch (DBConcurrencyException DBCe)
                    {
                        mySave.InsertRow((int)Definition.Message.D_DEBUG, "button_condition_Delete_Click: \r\n" + DBCe.ToString());
                    }
                    catch (Exception ex) { mySave.InsertRow((int)Definition.Message.D_ALARM, ex.ToString()); }
                }
                CheckForErrorText("Condition");
            }
        }

        private void button_Condition_Add_Click(object sender, EventArgs e)
        {
            DataRow dr_new = ConditionTable.Rows.Add(0);
            try
            {
                dr_new.SetField("routingPositionEntry_ID", nSelectedRoutingPositionEntry_ID);
                dr_new.SetField("Condition_comply", 0);
            }
            catch (Exception ex) { mySave.InsertRow((int)Definition.Message.D_ALARM, "RoutingForm::button_Condition_Add_Click: " + ex.ToString()); }

            if (!ribbonCheckBox_CheckCondition.Checked && bInputConditionOK) { SetConditionSaveButtonEnabled(true); }
            c1FlexGrid_Conditions.RowSel = c1FlexGrid_Conditions.Rows.Count - 1;
            CheckForErrorText("Condition");
        }

        private void SetButtonEnabled(bool bActive)
        {
            button_Condition_Add.Enabled = bActive;
            //button_Condition_Save.Enabled = bActive;
            button_Condition_Delete.Enabled = bActive;
            button_Command_Add.Enabled = bActive;
          //  button_Command_Save.Enabled = bActive;
            button_Command_Delete.Enabled = bActive;
        }

        private void SetConditionSaveButtonEnabled(bool bActive)
        {
            if (bActive == true && !bInputConditionOK) { return; }
            button_Condition_Save.Enabled = bActive;
           
        }

        private void SetCommandSaveButtonEnabled(bool bActive)
        {
            if (bActive == true && !bInputCommandOK) { return; }
            button_Command_Save.Enabled = bActive;   
        }
 

        private void button_Condition_Save_Click(object sender, EventArgs e)
        {
            SaveConditions();
        }

        private void SaveConditions()
        {
            if (!bInputConditionOK) { return; }
            if (ConditionTable.Rows.Count > 0)
            {
              //  DataRow dr_Con = ConditionTable.Rows[c1FlexGrid_Conditions.Row - 1];
                try
                {
                    MySqlCommandBuilder myCommand = new MySqlCommandBuilder(da_Condition);   
                    da_Condition.Update(ConditionTable);
                    myCommand.Dispose();
                }
                catch (DBConcurrencyException DBCe)
                {
                    mySave.InsertRow((int)Definition.Message.D_DEBUG, "button_Condition_Save_Click: \r\n" + DBCe.ToString());
                }
                catch (Exception ex) { mySave.InsertRow((int)Definition.Message.D_ALARM, ex.ToString()); }

                c1FlexGrid_Conditions.Update();
                bConditionTableChanged = false;
                bInputConditionOK = true;
                SaveTimeWarningAlarm();
            }
        }

     // to set the tree node active(orange) so you can see which one is selected after you lost the focus
        private void treeView_routing_MouseDown(object sender, MouseEventArgs e)
        {

            if (tn_LastSelected != null) { tn_LastSelected.BackColor = treeView_routing.BackColor; tn_LastSelected.ForeColor = this.ForeColor; tn_LastSelected.ImageIndex = tn_LastSelected.SelectedImageIndex = 18; }
            TreeView tv = sender as TreeView;
            TreeNode tn = treeView_routing.GetNodeAt(e.Location);
            if (tn != null)
            {
                if (tn.Level == 4)
                {
                    tn.BackColor = System.Drawing.Color.Orange;
                    tn.ForeColor = System.Drawing.Color.Black;
                    tn.ImageIndex = tn.SelectedImageIndex = 19;
                    tn_LastSelected = tn;
                    tv.SelectedNode = tn;
                    tv.SelectedNode.BackColor = System.Drawing.Color.Orange;
                    
                }
                else
                {
                    tv.SelectedNode = null;
                }

                treeView_routing.LabelEdit = false;
            }
           
        }

       private void c1FlexGrid_Conditions_AfterRowColChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
        {
           // C1FlexGrid grid = sender as C1FlexGrid;
           // MessageBox.Show("AfterRowColChange");
           
        }

      

       private void c1FlexGrid_Conditions_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
       {
           C1FlexGrid grid = sender as C1FlexGrid;

       
           if ( e.Row > 0 && e.Col == 1 )
           {
                grid.SetData(e.Row, 2, "");
                grid.SetData(e.Row, 3, "");
                grid.SetData(e.Row, 4, "");
                grid.SetData(e.Row, 5, "");

                c1FlexGrid_ConditionsUpdate();
           }
           if (grid.RowSel > 0 && e.Col==1)
           {
               if (e.Row == grid.RowSel)
               {
                   try
                   {
                       int nCondition = -1;
                       Int32.TryParse(grid[grid.RowSel, 1].ToString(), out nCondition);
                       if (nCondition == (int)Definition.RoutingConditions.CHECKOWNMAGPOS) // check own magazine oposition
                       {
                           grid.SetData(e.Row, 4, 1);
                           grid.SetData(e.Row, 5, "free");
                       }
                   }
                   catch { }
               }
           }
          
        
       }

      

       private void c1FlexGrid_Conditions_GetCellErrorInfo(object sender, GetErrorInfoEventArgs e)
       {
           
           C1FlexGrid grid = sender as C1FlexGrid;
           int nCondition = -1;
           string strValue = null;
            
           if (grid.Cols[e.Col].Name == null) { return; }
           if (e.Row < 0) { return; }
           
             if (e.Row > 0)
             {
                 //if (grid.Cols[e.Col].Name == "Condition")
                 //if (e.Col == 1)
                 {
                     try
                     {
                         Int32.TryParse(grid.GetData(e.Row, "Condition").ToString(), out nCondition);
                     }
                     catch { }
                 }
             }

             if (grid.Cols[e.Col].Name == "Condition" && e.Row > 0)
             {
                 if (nCondition == 0)
                 {
                    e.ErrorText = e.ErrorText = "Please select a condition!";
                 }
                 else { e.ErrorText = null; }

             }

             if (nCondition > 0)
             {
                 if (grid.Cols[e.Col].Name == "ValueName" && e.Row > 0)
                 {
                     if (nCondition > 0 && nCondition <= nConditionInputArray.Length)
                     {
                         string[] nArray = GetStringArrayForErrorText("Condition", nCondition);
                         strValue = grid[e.Row, "ValueName"].ToString();
                         if (strValue.Length <= 0 || strValue.StartsWith("0")) { e.ErrorText = nArray[2]; } else { e.ErrorText = null; }
                     }
                 }

                 if (grid.Cols[e.Col].Name == "Operation" && e.Row > 0)
                 {
                     if (nCondition > 0 && nCondition <= nConditionInputArray.Length)
                     {
                         string[] nArray = GetStringArrayForErrorText("Condition", nCondition);
                         strValue = grid[e.Row, "Operation"].ToString();
                         if (strValue.Length <= 0 || strValue.StartsWith("0")) { e.ErrorText = nArray[3]; }
                         else
                         {
                             e.ErrorText = null;
                         }
                     }
                 }

                 if (grid.Cols[e.Col].Name == "Value" && e.Row > 0)
                 {
                     if (nCondition > 0 && nCondition <= nConditionInputArray.Length)
                     {
                         string[] nArray = GetStringArrayForErrorText("Condition", nCondition);
                         strValue = grid[e.Row, "Value"].ToString();
                         if (strValue.Length <= 0) { e.ErrorText = nArray[4]; } else { e.ErrorText = null; }
                     }
                 }

             }
       }

       public void c1FlexGrid_ConditionsUpdate()
       {
           int nCondition = -1;
           C1FlexGrid grid = c1FlexGrid_Conditions;
           CellStyle cs_CompleteLine = grid.Styles.Add("neutral");
           cs_CompleteLine.BackColor = Color.White;

    
           foreach (Row rowConditions in grid.Rows)
           {
               try
               {
                   Int32.TryParse(rowConditions["Condition"].ToString(), out nCondition);
                   if (rowConditions.Index >= 1)
                   {
                       CellRange rg_completeLine = grid.GetCellRange(rowConditions.Index, 1, rowConditions.Index, 9);
                       rg_completeLine.Style = grid.Styles["neutral"];

                       CellRange rg_Image = grid.GetCellRange(rowConditions.Index, 0, rowConditions.Index, 0);
                       rg_Image.Image = null;
                   }
               }
               catch { }
               if ((nCondition == (int)Definition.RoutingConditions.PRETIME || nCondition == (int)Definition.RoutingConditions.TIME))
                   {
                  
                       CellRange rg = grid.GetCellRange(rowConditions.Index, 3, rowConditions.Index, 3);
                       CellStyle cs = grid.Styles.Add("time2");
                      
                       rg.Style = grid.Styles["time2"];

                       rg = grid.GetCellRange(rowConditions.Index, 3, rowConditions.Index, 5);
                       cs = grid.Styles.Add("disabled");
                      
                       cs.Editor = null;
                       rg.Style = grid.Styles["disabled"];

                       
                   }
               else if ((nCondition == (int)Definition.RoutingConditions.MACHINESAMPLEFREE)) // machine sample free
                   {
                        CellRange rg = grid.GetCellRange(rowConditions.Index, 3, rowConditions.Index, 3);
                        CellStyle cs = grid.Styles.Add("machineSampleFree");
                        cs.DataType = typeof(Int32);
                        // cs.Editor = comboBox_MachineList;
                        cs.DataMap = ld_Machines_Condition;
                        rg.Style = grid.Styles["machineSampleFree"];

                        rg = grid.GetCellRange(rowConditions.Index, 2, rowConditions.Index, 2);
                        cs = grid.Styles.Add("disabled");
                        cs.BackColor = Color.White;
                        cs.Editor = null;
                        rg.Style = grid.Styles["disabled"];

                        rg = grid.GetCellRange(rowConditions.Index, 4, rowConditions.Index, 4);
                        cs = grid.Styles.Add("enabled");
                        cs.BackColor = Color.White;
                        cs.Editor = null;
                        rg.Style = grid.Styles["enabled"];

                        /*rg = grid.GetCellRange(rowConditions.Index, 5, rowConditions.Index, 5);
                        cs = grid.Styles.Add("machineSampleFreeValue");
                        //    cs.DataType = typeof(string);
                        cs.Editor = comboBox_SamplePos;
                        rg.Style = grid.Styles["machineSampleFreeValue"];
                       */
                   }
               else if ((nCondition == (int)Definition.RoutingConditions.GLOBALTAG)) // global tags
                   {

                           CellRange rg = grid.GetCellRange(rowConditions.Index, 3, rowConditions.Index, 3);
                           CellStyle cs = grid.Styles.Add("UITypeEditorsGT");

                           MultiColumnEditor mcEditor = new MultiColumnEditor(flexEditorGlobalTags, "Name", false);
                          // mcEditor.SelectFilterRowColumn(0);
                           flexEditorGlobalTags.BackColor = cs.BackColor;
                           cs.DataType = typeof(Int32);
                           cs.Editor = new UITypeEditorControl(mcEditor, false);
                         

                          // cs.DataMap = ld_GlobalTags;
                           rg.Style = grid.Styles["UITypeEditorsGT"];

                           rg = grid.GetCellRange(rowConditions.Index, 2, rowConditions.Index, 2);
                           cs = grid.Styles.Add("disabled");
                           cs.BackColor = Color.White;
                           cs.Editor = null;
                           rg.Style = grid.Styles["disabled"];

                           rg = grid.GetCellRange(rowConditions.Index, 4, rowConditions.Index, 4);
                           cs = grid.Styles.Add("enabled");
                           cs.BackColor = Color.White;
                           cs.Editor = null;
                           rg.Style = grid.Styles["enabled"];
                      
                   }
               if ((nCondition == (int)Definition.RoutingConditions.MACHINETAG)) // machine tags
                   {
                           CellRange rg = grid.GetCellRange(rowConditions.Index, 3, rowConditions.Index, 3);
                           CellStyle cs = grid.Styles.Add("UITypeEditorsMT");

                           MultiColumnEditor mcEditor = new MultiColumnEditor(flexEditorMachineTags, "Name", false);
                           flexEditorGlobalTags.BackColor = cs.BackColor;
                           //    
                           cs.Editor = new UITypeEditorControl(mcEditor, false);
                          // cs.DataMap = ld_MachineTags;
                           rg.Style = grid.Styles["UITypeEditorsMT"];
                           //  rg.Style.MergeWith(cs);
                           rg = grid.GetCellRange(rowConditions.Index, 2, rowConditions.Index, 2);
                           cs = grid.Styles.Add("disabled");
                           cs.BackColor = Color.White;
                           cs.DataType = typeof(string);
                           cs.Editor = null;
                           rg.Style = grid.Styles["disabled"];

                           rg = grid.GetCellRange(rowConditions.Index, 4, rowConditions.Index, 4);
                           cs = grid.Styles.Add("enabled");
                           cs.BackColor = Color.White;
                           cs.Editor = null;
                           rg.Style = grid.Styles["enabled"];
                   }

               if ((nCondition == (int)Definition.RoutingConditions.WORKSHEETENTRY)) // WS entry
                   {

                       CellRange rg = grid.GetCellRange(rowConditions.Index, 3, rowConditions.Index, 3);
                       CellStyle cs = grid.Styles.Add("Enpty");
                       cs.Editor = null;
                       cs.DataMap = null;
                       rg.Style = grid.Styles["Enpty"];
                       rg = grid.GetCellRange(rowConditions.Index, 5, rowConditions.Index, 5);
                       rg.Style = grid.Styles["Enpty"];

                       rg = grid.GetCellRange(rowConditions.Index, 2, rowConditions.Index, 2);
                       cs = grid.Styles.Add("disabled");
                       cs.BackColor = Color.White;
                       cs.Editor = null;
                       rg.Style = grid.Styles["disabled"];

                       rg = grid.GetCellRange(rowConditions.Index, 4, rowConditions.Index, 4);
                       cs = grid.Styles.Add("enabled");
                       cs.BackColor = Color.White;
                       cs.Editor = null;
                       rg.Style = grid.Styles["enabled"];
                   }

               if ((nCondition == (int)Definition.RoutingConditions.SAMPLEONPOS
                   || nCondition == (int)Definition.RoutingConditions.SAMPLETYPE
                   || nCondition == (int)Definition.RoutingConditions.SAMPLEPRIORITY)) // sample on pos ,sample type , sample priority
                   {
                       CellRange rg = grid.GetCellRange(rowConditions.Index, 2, rowConditions.Index, 2);
                       CellStyle cs = grid.Styles.Add("enabled");
                          // MultiColumnEditor mcEditor = new MultiColumnEditor(flexEditorSamplePositions, "Position", false);
                          //UITypeEditorControl UITSamplePositionEditor = new UITypeEditorControl(mcEditor, false);
                          
                           cs.Editor = null;
                           rg.Style = grid.Styles["enabled"];
                           // rg.Style.MergeWith(cs);  

                           rg = grid.GetCellRange(rowConditions.Index, 3, rowConditions.Index, 3);
                           cs = grid.Styles.Add("UITypeEditorsSamples");
                           cs.DataMap = ld_Positions2;
                          // flexEditorGlobalTags.BackColor = cs.BackColor;
                           //    
                          // cs.Editor = new UITypeEditorControl(mcEditor, false);
                           rg.Style = grid.Styles["UITypeEditorsSamples"];

                           rg = grid.GetCellRange(rowConditions.Index, 4, rowConditions.Index, 4);
                           cs.Editor = null;
                         
                           rg.Style = grid.Styles["enabled"];
                   }
               if (nCondition == (int)Definition.RoutingConditions.SAMPLEONPOS) // Sample on pos
                   {
                       CellRange rg = grid.GetCellRange(rowConditions.Index, 5, rowConditions.Index, 5);
                           CellStyle cs = grid.Styles.Add("samplePos");
                           //     cs.DataType = typeof(string);
                           cs.Editor = comboBox_SamplePos;
                           rg.Style = grid.Styles["samplePos"];

                           
                   }
               if (nCondition == (int)Definition.RoutingConditions.SAMPLETYPE)
                   { // sample Type

                       CellRange rg = grid.GetCellRange(rowConditions.Index, 5, rowConditions.Index, 5);
                           CellStyle cs = grid.Styles.Add("sampleType");
                           //      cs.DataType = typeof(string);
                           //cs.Editor = comboBox_SampleType;
                           cs.DataMap = ld_SampleTypes;
                           rg.Style = grid.Styles["sampleType"];
                   }

               if (nCondition == (int)Definition.RoutingConditions.SAMPLEPRIORITY)
                   { // sample priority
                       CellRange rg = grid.GetCellRange(rowConditions.Index, 5, rowConditions.Index, 5);
                           CellStyle cs = grid.Styles.Add("samplePriority");
                           //       cs.DataType = typeof(string);
                           cs.Editor = numericUpDown;
                           rg.Style = grid.Styles["samplePriority"];
                       
                   }

               if (nCondition == (int)Definition.RoutingConditions.STATUSBITS)
                   { // status bit
                       Color rowColor = Color.White;
                      
                       MultiColumnEditor mcEditor = new MultiColumnEditor(flexEditorStatusBits, "Name", true);
                       UITypeEditorControl UITSampleStatusbitEditor = new UITypeEditorControl(mcEditor, false);

                       CellRange rg = grid.GetCellRange(rowConditions.Index, 2, rowConditions.Index, 2);
                       CellStyle cs = grid.Styles.Add("disabled2");
                       cs.BackColor = rowColor;
                       cs.DataType = typeof(Int32);
                       // cs.Editor = new UITypeEditorControl(mcEditor, false);
                       // cs.DataMap = ld_Statusbits;
                       //   string[] columns = new string[] { "Machine", "Name" };
                       //  DataTable dt = new DataTable();
                       //   dt = routingData.GetStatusBitsDataTable();
                       //   MultiColumnDictionary map = new MultiColumnDictionary(dt, "idmachine_status_bits", columns, 1);
                       //   cs.DataMap = map;
                       rg.Style = grid.Styles["disabled2"];

                       rg = grid.GetCellRange(rowConditions.Index, 3, rowConditions.Index, 3);
                       cs = grid.Styles.Add("statusbit");
                   
                       cs.BackColor = Color.White;
                       cs.DataType = typeof(Int32);
                       cs.DataMap = ld_Statusbits;
                       cs.Editor = new UITypeEditorControl(mcEditor, false);
                      
                       rg.Style = grid.Styles["statusbit"];

                       rg = grid.GetCellRange(rowConditions.Index, 5, rowConditions.Index, 5);
                       cs = grid.Styles.Add("statusbitValue");
                       cs.BackColor = Color.White;
                       //       cs.DataType = typeof(string);
                       cs.Editor = comboBox_StatusBit_Value;
                       rg.Style = grid.Styles["statusbitValue"];

                       

                       rg = grid.GetCellRange(rowConditions.Index, 4, rowConditions.Index, 4);
                       cs = grid.Styles.Add("enabled");
                       cs.BackColor = rowColor;
                       cs.BackColor = Color.White;
                       cs.Editor = null;
                       rg.Style = grid.Styles["enabled"];
                   }

               if ((nCondition == (int)Definition.RoutingConditions.CHECKOWNMAGPOS)) // check own magazine position is free
                   {

                       CellRange rg = grid.GetCellRange(rowConditions.Index, 3, rowConditions.Index, 3);
                       CellStyle cs = grid.Styles.Add("Enpty");
                       cs.Editor = null;
                       cs.DataMap = null;
                       rg.Style = grid.Styles["Enpty"];

                       rg = grid.GetCellRange(rowConditions.Index, 5, rowConditions.Index, 5);
                       rg.Style = grid.Styles["Enpty"];

                       rg = grid.GetCellRange(rowConditions.Index, 2, rowConditions.Index, 2);
                       cs = grid.Styles.Add("disabled");
                       cs.BackColor = Color.White;
                       cs.Editor = null;
                       rg.Style = grid.Styles["disabled"];

                       rg = grid.GetCellRange(rowConditions.Index, 4, rowConditions.Index, 4);
                       cs = grid.Styles.Add("enabled");
                       cs.BackColor = Color.White;
                       cs.Editor = null;
                       rg.Style = grid.Styles["enabled"];
                   }
           }
           
           
           c1FlexGrid_Conditions.AutoSizeCols(25);
           c1FlexGrid_Conditions.Cols[2].Width = 0;
       }

       private void c1FlexGrid_Conditions_BeforeEdit(object sender, RowColEventArgs e)
       {
           
           C1FlexGrid grid = sender as C1FlexGrid;

           if (ribbonCheckBox_CheckCondition.Checked)
           {
               e.Cancel = true;
           }
           else
           {
               if (e.Row >= 1 && e.Row == grid.RowSel)
               {
                   int nCondition = -1;
                   if (ConditionTable != null)
                   {
                       try
                       {
                            nCondition = Int32.Parse(grid.GetData(e.Row, 1).ToString());
                       }
                       catch
                       {
                           e.Cancel = true;
                           return;
                       }
                   }
                   else
                   {
                       return;
                   }

                   if (nCondition > 0)
                   {
                       if ((nCondition == (int)Definition.RoutingConditions.PRETIME
                           || nCondition == (int)Definition.RoutingConditions.TIME)
                           && (e.Col == 2 || e.Col == 5 || e.Col == 4)) //|| e.Col == 3 
                       { 
                           e.Cancel = true;
                           return;
                       }

                   
                       if ((nCondition == (int)Definition.RoutingConditions.MACHINESAMPLEFREE)) // machine sample free
                       {

                           if (e.Col == 2)
                           {
                               e.Cancel = true;
                           }
                           if (e.Col == 4)
                           {
                               grid[e.Row, e.Col] = 1;
                               e.Cancel = true;
                           }
                           if (e.Col == 5)
                           {
                               grid[e.Row, e.Col] = "sample free";
                               e.Cancel = true;
                           }
                       }
                       else if ((nCondition == (int)Definition.RoutingConditions.GLOBALTAG)) // global tags
                       {
                          
                          
                           if (e.Col == 2)
                           {
                               e.Cancel = true;
                           }
                       }
                       if ((nCondition == (int)Definition.RoutingConditions.MACHINETAG)) // machine tags
                       {

                           if (e.Col == 2)
                           {
                               e.Cancel = true;
                           }
                       }
                       if ((nCondition == (int)Definition.RoutingConditions.WORKSHEETENTRY)) // WS entry
                       {
                           if (e.Col == 2)
                           {
                               e.Cancel = true;
                           }
                       }

                       if (nCondition == (int)Definition.RoutingConditions.SAMPLEONPOS) // Sample on pos
                       {
                           if (e.Col == 4)
                           {
                               grid[e.Row, e.Col] = 1;
                               e.Cancel = true;
                           }
                           if (e.Col == 2 || e.Col == 3)
                           {
                               e.Cancel = true;
                           }

                       }

                       if ((nCondition == (int)Definition.RoutingConditions.SAMPLETYPE
                           || nCondition == (int)Definition.RoutingConditions.SAMPLEPRIORITY)) // sample on pos ,sample type , sample priority
                       {

                           if (e.Col == 2 || e.Col == 3)
                           {
                               e.Cancel = true;
                           }
                       }

                       if (nCondition == (int)Definition.RoutingConditions.STATUSBITS)
                       { // status bit


                           if (e.Col == 2)
                           {
                               e.Cancel = true;
                           }
                           if (e.Col == 4)
                           {
                               grid[e.Row, e.Col] = 1;
                               e.Cancel = true;
                           }

                       }

                       if ((nCondition == (int)Definition.RoutingConditions.CHECKOWNMAGPOS)) // check own magazine oposition
                       {
                           /*   if (e.Col == 4)
                              {
                                  grid[e.Row, e.Col] = 1;
                                  e.Cancel = true;
                              }
                              if (e.Col == 5)
                              {
                                  grid[e.Row, e.Col] = "free";
                                  e.Cancel = true;
                              }*/
                           if (e.Col == 2 || e.Col == 3 || e.Col == 4 || e.Col == 5)
                           {
                               e.Cancel = true;
                           }
                       }
                   }
               }

           }  
           
       }
       private void c1FlexGrid_Conditions_MouseDown(object sender, MouseEventArgs e)
       {
         
           C1FlexGrid grid = sender as C1FlexGrid;
           bool bGotCondition = false;

           // idrouting_commands,RoutingPositionEntry_ID

           // writes the captions of the command grid every time you click on a cell
           int nCondition = -1;
           if (grid.RowSel > 0)
           {
               try
               {
                   bGotCondition = Int32.TryParse(grid[grid.RowSel, "Condition"].ToString(), out nCondition);
               }
               catch { }
               grid.Cols[4].Caption = "";   //Operation =,!=,>,...

               if (bGotCondition)
               {
                   if (nCondition == (int)Definition.RoutingConditions.PRETIME) // PreTime
                   {
                       grid.Cols[2].Caption = "Time";
                       grid.Cols[3].Caption = "";
                       grid.Cols[5].Caption = "";
                       grid.Cols[6].Caption = "";
                   }
                   if (nCondition == (int)Definition.RoutingConditions.TIME) // Time
                   {
                       grid.Cols[2].Caption = "Time";
                       grid.Cols[3].Caption = "";
                       grid.Cols[5].Caption = "";
                       grid.Cols[6].Caption = "";
                   }
                   if (nCondition == (int)Definition.RoutingConditions.MACHINESAMPLEFREE) // machine sample free
                   {
                       grid.Cols[2].Caption = "";
                       grid.Cols[3].Caption = "Machine";
                       grid.Cols[5].Caption = "";
                       grid.Cols[6].Caption = "";
                   }
                   if (nCondition == (int)Definition.RoutingConditions.GLOBALTAG) // global tag
                   {
                       grid.Cols[2].Caption = "";
                       grid.Cols[3].Caption = "Global tag";
                       grid.Cols[5].Caption = "Value";
                       grid.Cols[6].Caption = "";
                   }
                   if (nCondition == (int)Definition.RoutingConditions.MACHINETAG) // machine tag
                   {
                       grid.Cols[2].Caption = "";
                       grid.Cols[3].Caption = "Machine tag";
                       grid.Cols[5].Caption = "Value";
                       grid.Cols[6].Caption = "";
                   }
                   if (nCondition == (int)Definition.RoutingConditions.WORKSHEETENTRY) // WS entry
                   {
                       grid.Cols[2].Caption = "";
                       grid.Cols[3].Caption = "WS entry";
                       grid.Cols[5].Caption = "Value";
                       grid.Cols[6].Caption = "";
                   }
                   if (nCondition == (int)Definition.RoutingConditions.SAMPLEONPOS) // sample on pos
                   {
                       grid.Cols[2].Caption = "";
                       grid.Cols[3].Caption = "Position";
                       grid.Cols[5].Caption = "Value";
                       grid.Cols[6].Caption = "";
                   }
                   if (nCondition == (int)Definition.RoutingConditions.SAMPLETYPE) // sample type
                   {
                       grid.Cols[2].Caption = "";
                       grid.Cols[3].Caption = "Position";
                       grid.Cols[5].Caption = "Type";
                       grid.Cols[6].Caption = "";
                   }
                   if (nCondition == (int)Definition.RoutingConditions.SAMPLEPRIORITY) // sample priority
                   {
                       grid.Cols[2].Caption = "";
                       grid.Cols[3].Caption = "Position";
                       grid.Cols[5].Caption = "Priority";
                       grid.Cols[6].Caption = "";
                   }
                   if (nCondition == (int)Definition.RoutingConditions.STATUSBITS) // status bit
                   {
                       grid.Cols[2].Caption = "";
                       grid.Cols[3].Caption = "Status bit";
                       grid.Cols[5].Caption = "Condition";
                       grid.Cols[6].Caption = "";
                   }
                   if ((nCondition == (int)Definition.RoutingConditions.CHECKOWNMAGPOS)) // check own magazine oposition
                   {
                        grid.Cols[2].Caption = "";
                        grid.Cols[3].Caption = "";
                        grid.Cols[5].Caption = "Condition";
                        grid.Cols[6].Caption = "";
                   }
               }
               else
               {
                   grid.Cols[2].Caption = "";
                   grid.Cols[3].Caption = "";
                   grid.Cols[5].Caption = "";
                   grid.Cols[6].Caption = "";
               }
           }
       }


       private void CreateSelectbox(int nObject, C1FlexGrid grid)
       {
           //
          if (nObject == 1)
           {
     //          grid.Cols[2].DataMap = routingData.GetMachineMap();
        
           }
           else if (nObject == 2)
           {
            //   grid.Cols[4].DataMap =  routingData.GetStateListMap();
           }
           else if (nObject == 3)
           {
         //      grid.Cols[2].DataMap = routingData.GetGlobalTagsMap();
             //  grid.Cols[4].DataMap = null;
           }
          
       }

       private void button_Command_Add_Click(object sender, EventArgs e)
       {
           DataRow dr_new = CommandTable.Rows.Add(0);
           try
           {
               dr_new.SetField("RoutingPositionEntry_ID", nSelectedRoutingPositionEntry_ID);
           }
           catch (Exception ex) { mySave.InsertRow((int)Definition.Message.D_ALARM, "RoutingForm::button_Command_Add_Click: " + ex.ToString()); }

           try
           {
               c1FlexGrid_Commands.RowSel = c1FlexGrid_Commands.Rows.Count - 1;
               c1FlexGrid_Commands[c1FlexGrid_Commands.RowSel, "RoutingPositionEntry_ID"] = nSelectedRoutingPositionEntry_ID;
               
           }
           catch { }
           CheckForErrorText("Command");
       }

       private void button_Command_Save_Click(object sender, EventArgs e)
       {
           SaveCommands();
       }
       private void SaveCommands()
       {
           if (!bInputCommandOK) { return; }
           try
           {
               MySqlCommandBuilder myCommand = new MySqlCommandBuilder(da_Commands);
               da_Commands.Update(CommandTable);
               bCommandTableChanged = false;
               bInputCommandOK = true;
               myCommand.Dispose();
           }
           catch (DBConcurrencyException DBCe)
           {
               mySave.InsertRow((int)Definition.Message.D_DEBUG, "button_Command_Save_Click: \r\n" + DBCe.ToString());
           }
           catch (Exception ex) { mySave.InsertRow((int)Definition.Message.D_ALARM, ex.ToString()); /* MessageBox.Show(ex.ToString());*/ }

       }

       private void button_Command_Delete_Click(object sender, EventArgs e)
       {
           if (c1FlexGrid_Commands.RowSel > 0)
           {
               DataRowView dr_delete = (DataRowView)c1FlexGrid_Commands.Rows[c1FlexGrid_Commands.RowSel].DataSource;
               if (MessageBox.Show("Do you want to delete the record?", "Confirm Delete", System.Windows.Forms.MessageBoxButtons.OKCancel) == DialogResult.OK)
               {
                   try
                   {
                       c1FlexGrid_Commands.RemoveItem(c1FlexGrid_Commands.Row);
                       c1FlexGrid_Commands.Update();
                   }
                   catch { }
                   try
                   {
                       routingData.DeleteEntryFromRoutingCommands(Int32.Parse(dr_delete["idrouting_commands"].ToString()));
                   }
                   catch (DBConcurrencyException DBCe)
                   {
                       mySave.InsertRow((int)Definition.Message.D_DEBUG, "button_Command_Delete_Click: \r\n" + DBCe.ToString());
                   }
                   catch (Exception ex) { mySave.InsertRow((int)Definition.Message.D_ALARM, ex.ToString()); }
               }
               CheckForErrorText("Command");
           }
       }

       private void c1FlexGrid_Commands_BeforeEdit(object sender, RowColEventArgs e)
       {
           C1FlexGrid grid = sender as C1FlexGrid;
          // grid.ShowButtons = ShowButtonsEnum.Always;

           // "e.Cancel" blocks the direkt input possibility.
           // we want to edit some of the fields by an extra grid
       if (e.Row > 0 && e.Row == grid.RowSel)
           {
               if (ribbonCheckBox_CheckCondition.Checked) { e.Cancel = true; return; }

               int nCommandType = -1;
               try
               {
                   nCommandType = Int32.Parse(grid.GetData(e.Row, 1).ToString());
               }
               catch { }

               if (nCommandType == (int)Definition.RoutingCommands.SHIFTSAMPLE) // shift sample
               {
                   if (e.Col == 2 || e.Col == 3 || e.Col == 4 || e.Col == 5)
                   {

                       e.Cancel = true;
                   }
               }
               if (nCommandType == (int)Definition.RoutingCommands.CREATESAMPLE) // create sample
               {
                   if (e.Col == 2 || e.Col == 3)
                   {
                     e.Cancel = true;
                   }

                   // e.Coll == 4 => SampleID free typing; so no "Cancel"

                   if (e.Col == 5)
                   {
                       CellRange rg = grid.GetCellRange(grid.Row, 5, grid.Row, 5);
                       CellStyle cs = grid.Styles.Add("SamplePriority");
                       cs.Editor = numericUpDown_Commands;
                       rg.Style = grid.Styles["SamplePriority"];
                   }
               }

               if (nCommandType == (int)Definition.RoutingCommands.DELETESAMPLE) // delete sample
               {
                   if (e.Col == 2 || e.Col == 3 || e.Col == 4 || e.Col == 5)
                   {
                       e.Cancel = true;
                   }
               }

               if (nCommandType == (int)Definition.RoutingCommands.CHANGESAMPLETYPE) // change sample type
               {
                   if ( e.Col == 3 || e.Col == 4 || e.Col == 5)
                   {
                       e.Cancel = true;
                   }
                   if (e.Col == 2)
                   {

                   }
               }

               if (nCommandType == (int)Definition.RoutingCommands.CHANGEPRIORITY) // change sample priority
               {
                   if (e.Col == 3 || e.Col == 4 || e.Col == 5 )
                   {
                       e.Cancel = true;
                   }
                   if (e.Col == 2)
                   {
                       CellRange rg = grid.GetCellRange(grid.Row,2, grid.Row, 2);
                       CellStyle cs = grid.Styles.Add("ChangeSamplePriority");
                       cs.Editor = numericUpDown_Commands;
                       rg.Style = grid.Styles["ChangeSamplePriority"];
                   }
               }

               if (nCommandType == (int)Definition.RoutingCommands.WRITEGLOBALTAG ) // write WinCC global tag
               {
                   if (e.Col == 2) // Name select by grid
                   {
                       e.Cancel = true;
                   }    
                   if (e.Col == 3 || e.Col == 4)
                   {
                       e.Cancel = true;
                   }

               }
               if ( nCommandType == (int)Definition.RoutingCommands.WRITEMACHINETAG) // write WinCC  machine tag
               {
                   if (e.Col == 2) // Name select by grid
                   {
                       e.Cancel = true;
                   }
                   if (e.Col == 4) // e.Col == 4 => Input field Tagname
                   {
                       e.Cancel = true;
                   }

               }

               if (nCommandType == (int)Definition.RoutingCommands.INSERTWSENTRY) // write WS entry
               {
                  // e.Col == 2 ||
                   if ( e.Col == 3)
                   {
                       e.Cancel = true;
                   }

                   int nSampleLocation = -1;
                   Int32.TryParse(grid.GetData(e.Row, 2).ToString(), out nSampleLocation);
                   if (e.Col == 2)
                   {
                       if (nSampleLocation != (int)Definition.WSInsertLocation.SAMPLEONPOS)
                       {
                           if ((string)grid.GetData(e.Row, 3).ToString() != "")
                           {
                               grid.SetData(e.Row, 3, "");
                           }
                       }
                   }
                  
                  // e.Col == 4 & e.Col == 5  Name and Value must be free for enter
               }
               if (nCommandType == (int)Definition.RoutingCommands.DELETEWSENTRY) // delete WS entry
               {
                   if (e.Col == 2 || e.Col == 3 || e.Col == 5)
                   {
                       e.Cancel = true;
                   }
                   // e.Col == 4  Name and Value must be free for enter
               }

               if (nCommandType == (int)Definition.RoutingCommands.SENDINGCOMMANDTOMACHINE)   //sending command to machine
               {
                   if (e.Col == 2 || e.Col == 3 || e.Col == 5)
                   {
                       e.Cancel = true;
                   }
                   if (e.Col == 4 && grid.GetData(e.Row, 2).ToString().Length>0)    // 
                   {
                      int nConnectionType = myHC.GetConnectionTypeFromConnectionListByMachineList_ID(myHC.GetMachineList_IDFromMachinesByMachine_ID(Int32.Parse(grid.GetData(e.Row, 2).ToString())));
                      if (nConnectionType == (int)Definition.ConnectionTypes.MAGAZINE || nConnectionType == (int)Definition.ConnectionTypes.TCPIP)
                      {
                          e.Cancel = false;
                      } else{
                         // grid.SetData(e.Row, 4, "");
                          e.Cancel = true;
                      }
                     
                   }
               }
               if (nCommandType == (int)Definition.RoutingCommands.CREATERESERVATION 
                   || nCommandType == (int)Definition.RoutingCommands.DELETERESERVATION) // create/delete reservation point
               {
                   if (e.Col == 2 || e.Col == 3 || e.Col == 4 || e.Col == 5)
                   {
                       e.Cancel = true;
                   }                 
               }  
           }
            
       }

       public void SetInputFilterForWinCCTags(C1FlexGrid grid, int nRow,int nCommandType)
       {
           try
           {
               string nWinCCTagName = grid[nRow, 4].ToString();
               int nWinCCTagType = -1;

               if (nCommandType == (int)Definition.RoutingCommands.WRITEGLOBALTAG) { nWinCCTagType = myHC.GetWinCCTypeFromTagName((int)Definition.SQLTables.GLOBAL_TAGS, nWinCCTagName); }
               if (nCommandType == (int)Definition.RoutingCommands.WRITEMACHINETAG) { nWinCCTagType = myHC.GetWinCCTypeFromTagName((int)Definition.SQLTables.MACHINE_TAGS, nWinCCTagName); }
               CellRange rg = new CellRange();
               rg = grid.GetCellRange(grid.Row, 5, grid.Row, 5);
               CellStyle cs = null;
               
               switch (nWinCCTagType)
               {
                     
                   case 1:

                      cs = grid.Styles.Add("GlobalTagBool");
                      cs.DataType = typeof(bool);
                      cs.TextAlign = TextAlignEnum.CenterCenter;
                      rg.Style = grid.Styles["GlobalTagBool"];
                      break;
                   
                    case 2:   
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                        switch (nWinCCTagType)
                        {
                            case 2: // DM_VARTYPE_SBYTE    Vorzeichenbehafteter 8-Bit Wert 
                                numericUpDown_Commands.Maximum = 128;
                                numericUpDown_Commands.Minimum = -127;
                                break;
                            case 3: // DM_VARTYPE_BYTE     Vorzeichenloser 8-Bit Wert 
                                numericUpDown_Commands.Maximum = 256;
                                numericUpDown_Commands.Minimum = 0;
                                break;
                            case 4: // DM_VARTYPE_SWORD    Vorzeichenbehafteter 16-Bit Wert 
                                numericUpDown_Commands.Maximum = 32768;
                                numericUpDown_Commands.Minimum = -32767;
                                break;
                            case 5: // DM_VARTYPE_WORD     Vorzichenloser 16-Bit Wert 
                                numericUpDown_Commands.Maximum = 65535;
                                numericUpDown_Commands.Minimum = 0;
                                break;
                            case 6: // DM_VARTYPE_SDWORD   Vorzeichenbehafteter 32-Bit Wert 
                                numericUpDown_Commands.Maximum = 2147483648;
                                numericUpDown_Commands.Minimum = -2147483648;
                                break;
                            case 7: // DM_VARTYPE_DWORD    Vorzeichenloser 32-Bit Wert 
                                numericUpDown_Commands.Maximum = 4294967296;
                                numericUpDown_Commands.Minimum = 0;
                                break;

                        }
                        cs = grid.Styles.Add("GlobalTagDezimal");
                        cs.DataType = typeof(int);
                        cs.Editor = numericUpDown_Commands;
                        rg.Style = grid.Styles["GlobalTagDezimal"];
                        break;
                   
                   case 8: //DM_VARTYPE_FLOAT        Gleitkommazahl 32-Bit IEEE 
                        cs = grid.Styles.Add("GlobalTagFloat");
                        cs.DataType = typeof(float);
                        // cs.Format = "N0";
                        rg.Style = grid.Styles["GlobalTagFloat"];
                        break;

                    case 9: // DM_VARTYPE_DOUBLE      Gleitkommazahl 64-Bit IEEE 754 
                        cs = grid.Styles.Add("GlobalTagDouble");
                        cs.DataType = typeof(double);
                        rg.Style = grid.Styles["GlobalTagDouble"];
                        break;
                    
                    case 10:
                    case 11:
                        cs = grid.Styles.Add("GlobalTagString");
                        cs.DataType = typeof(string);
                        rg.Style = grid.Styles["GlobalTagString"];
                        break;
                                    
               }
           }
           catch {}
       }

       private void c1FlexGrid_Conditions_AfterEdit(object sender, RowColEventArgs e)
       {
           bConditionTableChanged = true;
          // c1FlexGrid_ConditionsUpdate();
           c1FlexGrid_Conditions.AutoSizeRow(e.Row);
           CheckForErrorText("Condition");
          
        
       }
       private void c1FlexGrid_Conditions_AfterSort(object sender, SortColEventArgs e)
       {
           c1FlexGrid_ConditionsUpdate();
       }


       public void c1FlexGrid_CommandsUpdate()
       {
           
         int nCommandType = -1;
         C1FlexGrid grid = c1FlexGrid_Commands;

         foreach (Row rowCommands in grid.Rows)
            {
                // for(int i=1; i <=  c1FlexGrid_Commands.Rows.Count;i++){
                Int32.TryParse(rowCommands["Command"].ToString(), out nCommandType);

                if (nCommandType == (int)Definition.RoutingCommands.SHIFTSAMPLE) // shift sample
                {
                    CellRange rg = grid.GetCellRange(rowCommands.Index, 2, rowCommands.Index, 2);
                    CellStyle cs = grid.Styles.Add("MachinePositions");
                    cs.DataMap = ld_Positions;
                    rg.Style = grid.Styles["MachinePositions"];
                }

                if (nCommandType == (int)Definition.RoutingCommands.CREATESAMPLE) // create sample
                {
                    CellRange rg = grid.GetCellRange(rowCommands.Index, 2, rowCommands.Index, 2);
                    CellStyle cs = grid.Styles.Add("MachinePositions");
                    cs.DataMap = ld_Positions;
                    rg.Style = grid.Styles["MachinePositions"];

                    rg = grid.GetCellRange(rowCommands.Index, 3, rowCommands.Index, 3);
                    cs = grid.Styles.Add("SamplePrograms");
                    cs.DataMap = ld_SamplePrograms;
                    rg.Style = grid.Styles["SamplePrograms"];
                     
                }

                if (nCommandType == (int)Definition.RoutingCommands.INSERTWSENTRY) // change type/position to which sample add or change the value 
                {
                    CellRange rg = grid.GetCellRange(rowCommands.Index, 2, rowCommands.Index, 2);
                    CellStyle cs = grid.Styles.Add("InsertWSEntry_Location");
                    cs.DataMap = ld_SamplePosition;
                    rg.Style = grid.Styles["InsertWSEntry_Location"];

                 //   int nPositionType = -1;
                    //Int32.TryParse(grid.GetData(rowCommands.Index, 2).ToString() ,out nPositionType);
                  //  if (nPositionType == (int)Definition.WSInsertLocation.SAMPLEONPOS)
                    {
                        rg = grid.GetCellRange(rowCommands.Index, 3, rowCommands.Index, 3);
                        cs = grid.Styles.Add("MachinePositions");
                        cs.DataMap = ld_Positions;
                        rg.Style = grid.Styles["MachinePositions"];
                    }
                }

                if (nCommandType == (int)Definition.RoutingCommands.CHANGESAMPLETYPE) // change sample type/programm
                {
                    CellRange rg = grid.GetCellRange(rowCommands.Index, 2, rowCommands.Index, 2);
                    CellStyle cs = grid.Styles.Add("SamplePrograms");
                    cs.DataMap = ld_SamplePrograms;
                    rg.Style = grid.Styles["SamplePrograms"];
                }

                if (nCommandType == (int)Definition.RoutingCommands.WRITEGLOBALTAG) // write WinCC global tag
                {
                    SetInputFilterForWinCCTags(grid, rowCommands.Index, nCommandType);
                }

                if (nCommandType == (int)Definition.RoutingCommands.WRITEMACHINETAG) // write WinCC machine tag
                {
                    CellRange rg = grid.GetCellRange(rowCommands.Index, 3, rowCommands.Index,3);
                    CellStyle cs = grid.Styles.Add("Machinenames");
                    cs.DataMap = ld_Machines;
                    rg.Style = grid.Styles["Machinenames"];

                    SetInputFilterForWinCCTags(grid, rowCommands.Index, nCommandType);
                }

                if (nCommandType == (int)Definition.RoutingCommands.SENDINGCOMMANDTOMACHINE) // sending command to machine
                {
                    CellRange rg = grid.GetCellRange(rowCommands.Index, 2, rowCommands.Index, 2);               
                    CellStyle cs = grid.Styles.Add("Machines");
                    cs.DataMap = ld_Machines;
                    rg.Style = grid.Styles["Machines"];

                    rg = grid.GetCellRange(rowCommands.Index, 3, rowCommands.Index, 3);               
                    cs = grid.Styles.Add("MachineCommands");
                    cs.DataMap = ld_Commands;
                    rg.Style = grid.Styles["MachineCommands"];

                    rg = grid.GetCellRange(rowCommands.Index, 4, rowCommands.Index, 4);               
                    cs = grid.Styles.Add("MachineCommandOptions");
                   // cs.Editor = comboBox_CommandValue2;
                    rg.Style = grid.Styles["MachineCommandOptions"];

                    
                     
                }

                if (nCommandType == (int)Definition.RoutingCommands.CREATERESERVATION || nCommandType == (int)Definition.RoutingCommands.DELETERESERVATION) // create/delete reservation point
                {
                    CellRange rg = grid.GetCellRange(rowCommands.Index, 2, rowCommands.Index, 2);
                    CellStyle cs = grid.Styles.Add("MachinePositions");
                    cs.DataMap = ld_Positions;
                    rg.Style = grid.Styles["MachinePositions"];
                }
             }
         c1FlexGrid_Commands.AutoSizeCols(25);
       }

       private void CreateSelectboxForCommands()
       {
           //
           //Commands SelectBox
           //
           string SQL_Statement_Commands = GetSQL_StatementForCommands();
           DataSet CommandNames = new DataSet();
           CommandNames.Clear();
           CommandNames = myHC.GetDataSetFromSQLCommand(SQL_Statement_Commands);

           if (CommandNames.Tables[0] != null)
           {
               DataTable dt_Commands = CommandNames.Tables[0];
               ListDictionary ld_Commands = new ListDictionary();
               ld_Commands.Add(0, "<Please select>");
               foreach (DataRow dr_Commands in dt_Commands.Rows)
               {
                   // ItemArray[0] = id
                   // ItemArray[1] = name
                   ld_Commands.Add((int)dr_Commands.ItemArray[0], dr_Commands.ItemArray[1]);

               }
               c1FlexGrid_Commands.Cols[1].DataMap = ld_Commands;
           }

       }

       private string GetSQL_StatementForCommands()
       {
           string strSQL_String = null;
           strSQL_String = "SELECT idrouting_command_list,Name FROM routing_command_list";
           return strSQL_String;
       }

       private void CreateSelectboxForMachines()
       {
           //
           //Commands SelectBox
           //
           string SQL_Statement_Machines = GetSQL_StatementForMachines();
           DataSet MachineNames = new DataSet();
           MachineNames.Clear();
           MachineNames = myHC.GetDataSetFromSQLCommand(SQL_Statement_Machines);

           if (MachineNames.Tables[0] != null)
           {
               DataTable dt_Machines = MachineNames.Tables[0];
               ListDictionary ld_Commands = new ListDictionary();
               ld_Commands.Add(0, "<Please select>");
               foreach (DataRow dr_Machiness in dt_Machines.Rows)
               {
                   // ItemArray[0] = id
                   // ItemArray[1] = name
                   ld_Commands.Add((int)dr_Machiness.ItemArray[0], dr_Machiness.ItemArray[1]);

               }
               c1FlexGrid_Commands.Cols[2].DataMap = ld_Commands;
           }

       }

       private string GetSQL_StatementForMachines()
       {
           string strSQL_String = null;
           strSQL_String = "SELECT idmachines,Name FROM Machines";
           return strSQL_String;
       }

       private string GetSQL_StatementForMachinePositions()
       {
           string strSQL_String = null;
           //strSQL_String = "SELECT idmachine_positions,Name, FROM machine_positions";
           strSQL_String = @"SELECT  machine_positions.idmachine_positions, machine_positions.Name, machines.Name AS MachineName
                         FROM  machine_positions LEFT OUTER JOIN
                         machines ON machine_positions.Machine_ID = machines.idmachines";
           return strSQL_String;
       }

       private string GetSQL_StatementForSamplePrograms()
       {
           string strSQL_String = null;
           strSQL_String = "Select idsample_programs,Name FROM sample_programs Where (idsample_programs > 0)";
           return strSQL_String;
       }

       private string GetSQL_StatementForWinCCGlobalTagsDictionary()
       {
           string strSQL_String = null;
           strSQL_String = "Select idglobal_tags,Name FROM global_tags";
           return strSQL_String;
       }
       private string GetSQL_StatementForWinCCMachineTagsDictionary()
       {
           string strSQL_String = null;
           strSQL_String = "Select idmachine_tags,Name FROM machine_tags";
           return strSQL_String;
       }

       private string GetSQL_StatementForStatusbitsDictionary()
       {
           string strSQL_String = null;
           
           strSQL_String = @"SELECT machine_state_signals.idmachine_state_signals,machines.Name AS Machine, machine_state_signals.Name AS Bit
                             FROM machine_state_signals INNER JOIN
                             machines ON machine_state_signals.Machine_ID = machines.idmachines WHERE machine_state_signals.signal_type='Status'";
           return strSQL_String;
       }
       private string GetSQL_StatementForSampleTypeDictionary()
       {
           string strSQL_String = null;

           strSQL_String = @"SELECT idsample_type_list,Name From sample_type_list WHERE idsample_type_list>0";
           return strSQL_String;
       }
       private string GetSQL_StatementForGlobalTagsDictionary()
       {
           string strSQL_String = null;
           strSQL_String = "Select idglobal_tags,Name FROM global_tags";
           return strSQL_String;
       }
       private string GetSQL_StatementForMachineTagsDictionary()
       {
           string strSQL_String = null;
           strSQL_String = "Select idmachine_tags,Name FROM machine_tags";
           return strSQL_String;
       }
       private void c1FlexGrid_Commands_MouseDown(object sender, MouseEventArgs e)
       {
           C1FlexGrid grid = sender as C1FlexGrid;
           bool bGotCommandTypeID = false;

          // idrouting_commands,RoutingPositionEntry_ID

           if ((grid.ColSel == 4 || grid.ColSel == 5) && !ribbonCheckBox_CheckCondition.Checked)
           {
               nSelectedRowCommands = grid.RowSel;
               nSelectedColCommands = grid.ColSel;
               comboBox_Choice.Enabled = true;
               button_AddChoise.Enabled = true;
               button_ColorPicker.Enabled = true;
           }
           else
           {
               nSelectedRowCommands = -1;
               nSelectedColCommands = -1;
               comboBox_Choice.Enabled = false;
               button_AddChoise.Enabled = false;
               button_ColorPicker.Enabled = false;
           }
           // writes the captions of the command grid every time you click on a cell
           int nCommandType_ID = -1;
           if (grid.RowSel > 0)
           {
              

               bGotCommandTypeID = Int32.TryParse(grid[grid.RowSel, "Command"].ToString(), out nCommandType_ID);

               if (bGotCommandTypeID)
               {
                   if (nCommandType_ID == (int)Definition.RoutingCommands.SHIFTSAMPLE) // shift sample
                   {
                       grid.Cols[2].Caption = "Position";
                       grid.Cols[3].Caption = "";
                       grid.Cols[4].Caption = "";
                       grid.Cols[5].Caption = "";
                   }
                   if (nCommandType_ID == (int)Definition.RoutingCommands.CREATESAMPLE) // create sample
                   {
                       grid.Cols[2].Caption = "Position";
                       grid.Cols[3].Caption = "Program";
                       grid.Cols[4].Caption = "SampleName";
                       grid.Cols[5].Caption = "Priority";
                   }
                   if (nCommandType_ID == (int)Definition.RoutingCommands.DELETESAMPLE) // delete sample 
                   {
                       grid.Cols[2].Caption = "";
                       grid.Cols[3].Caption = "";
                       grid.Cols[4].Caption = "";
                       grid.Cols[5].Caption = "";
                   }
                   if (nCommandType_ID == (int)Definition.RoutingCommands.CHANGESAMPLETYPE) // sample type
                   {
                       grid.Cols[2].Caption = "Type";
                       grid.Cols[3].Caption = "";
                       grid.Cols[4].Caption = "";
                       grid.Cols[5].Caption = "";
                   }
                   if (nCommandType_ID == (int)Definition.RoutingCommands.CHANGEPRIORITY) // sample priority
                   {
                       grid.Cols[2].Caption = "Priority";
                       grid.Cols[3].Caption = "";
                       grid.Cols[4].Caption = "";
                       grid.Cols[5].Caption = "";
                   }
                   if (nCommandType_ID == (int)Definition.RoutingCommands.WRITEGLOBALTAG) // write WinCC global tag
                   {
                       grid.Cols[2].Caption = "";
                       grid.Cols[3].Caption = "";
                       grid.Cols[4].Caption = "TagName";
                       grid.Cols[5].Caption = "Value";
                   }
                   if (nCommandType_ID == (int)Definition.RoutingCommands.WRITEMACHINETAG) // write WinCC machine tag
                   {
                       grid.Cols[2].Caption = "";
                       grid.Cols[3].Caption = "Machine";
                       grid.Cols[4].Caption = "TagName";
                       grid.Cols[5].Caption = "Value";
                   }
                   if (nCommandType_ID == (int)Definition.RoutingCommands.INSERTWSENTRY) // write WS entry
                   {
                       grid.Cols[2].Caption = "Location";
                       grid.Cols[3].Caption = "Position";
                       grid.Cols[4].Caption = "Name";
                       grid.Cols[5].Caption = "Value";
                   }
                   if (nCommandType_ID == (int)Definition.RoutingCommands.DELETEWSENTRY) // delete WS entry
                   {
                       grid.Cols[2].Caption = "";
                       grid.Cols[3].Caption = "";
                       grid.Cols[4].Caption = "Name";
                       grid.Cols[5].Caption = "";
                   }
                   if (nCommandType_ID == (int)Definition.RoutingCommands.SENDINGCOMMANDTOMACHINE) // sending command to machine
                   {
                       grid.Cols[2].Caption = "Machine";
                       grid.Cols[3].Caption = "Command";
                       grid.Cols[4].Caption = "Option";
                       grid.Cols[5].Caption = "";
                   }
                   if (nCommandType_ID == (int)Definition.RoutingCommands.CREATERESERVATION) // create reservation point
                   {
                       grid.Cols[2].Caption = "Position";
                       grid.Cols[3].Caption = "";
                       grid.Cols[5].Caption = "";
                       grid.Cols[6].Caption = "";
                   }
                   if (nCommandType_ID == (int)Definition.RoutingCommands.DELETERESERVATION) // delete reservation point
                   {
                       grid.Cols[2].Caption = "Position";
                       grid.Cols[3].Caption = "";
                       grid.Cols[5].Caption = "";
                       grid.Cols[6].Caption = "";
                   }
               }
               else
               {
                   grid.Cols[2].Caption = "";
                   grid.Cols[3].Caption = "";
                   grid.Cols[4].Caption = "";
                   grid.Cols[5].Caption = "";


               }
           }
           else
           {
               nSelectedRowCommands = -1;
               nSelectedColCommands = -1;
               comboBox_Choice.Enabled = false;
               button_AddChoise.Enabled = false;
               button_ColorPicker.Enabled = false;
           }
        //   MessageBox.Show(grid.RowSel.ToString() + "#" + grid.ColSel.ToString());
       }

       private void c1FlexGrid_Commands_MouseDoubleClick(object sender, MouseEventArgs e)
       {
           if (ribbonCheckBox_CheckCondition.Checked) { ShowEditInfo(); return; }

           C1FlexGrid grid = sender as C1FlexGrid;
           Point pt = new Point(e.X, e.Y);
           Point FormLocation = this.Location;

           Point groupLocation = grid.Location;
           Point groupLocationSplitter = splitContainer1.Panel2.Location;
           Point PopupLocation = new Point(FormLocation.X + groupLocation.X + e.X + groupLocationSplitter.X-70, FormLocation.Y + 90 + groupLocation.Y + e.Y);
          
           // Rectangle rc = grid.GetCellRect(row,col);
           // idrouting_commands,RoutingPositionEntry_ID
           if (grid.Row != -1)
           {
               int nCommandType_ID = (int)grid[grid.RowSel, "Command"];

               if (nCommandType_ID == (int)Definition.RoutingCommands.SHIFTSAMPLE) // shift sample
               {
                   if (grid.ColSel == 2)
                   {
                       Select_Form select_Form = new Select_Form(this, PopupLocation, nCommandType_ID, grid);
                       select_Form.ShowDialog();
                   }
               }

               if (nCommandType_ID == (int)Definition.RoutingCommands.CREATESAMPLE) // create sample
               {
                   if (grid.ColSel == 2 || grid.ColSel == 3)
                   {
                       Select_Form select_Form = new Select_Form(this, PopupLocation, nCommandType_ID, grid);
                       select_Form.ShowDialog();
                   }
               }

               if (nCommandType_ID == (int)Definition.RoutingCommands.DELETESAMPLE) // delete sample
               {
                   // no edit needed
               }

               if (nCommandType_ID == (int)Definition.RoutingCommands.CHANGESAMPLETYPE) // change sample type
               {
                   // done with a combobox
               }

               if (nCommandType_ID == (int)Definition.RoutingCommands.CHANGEPRIORITY) // change sample priority
               {
                   // done with a numeric box
               }

               if (nCommandType_ID == (int)Definition.RoutingCommands.WRITEGLOBALTAG) // write WinCC global tag
               {
                   if (grid.ColSel == 4)
                   {
                       Select_Form select_Form = new Select_Form(this, PopupLocation, nCommandType_ID, grid);
                       select_Form.ShowDialog();
                   }
               }
               if (nCommandType_ID == (int)Definition.RoutingCommands.WRITEMACHINETAG) // write WinCC machine tag
               {
                   if (grid.ColSel == 4)
                   {
                       Select_Form select_Form = new Select_Form(this, PopupLocation, nCommandType_ID, grid);
                       select_Form.ShowDialog();
                   }

               }

               if (nCommandType_ID == (int)Definition.RoutingCommands.INSERTWSENTRY) // write WS entry
               {
                   // both Name and Value must be free discribable
                   if (grid.ColSel == 3)
                   {
                       int nPositionType = -1;
                       Int32.TryParse(grid.GetData(grid.RowSel, 2).ToString(), out nPositionType);
                       if (nPositionType == (int)Definition.WSInsertLocation.SAMPLEONPOS)
                       {
                           Select_Form select_Form = new Select_Form(this, PopupLocation, nCommandType_ID, grid);
                           select_Form.ShowDialog();
                       }
                   }
               }

               if (nCommandType_ID == (int)Definition.RoutingCommands.DELETEWSENTRY) // delete WS entry
               {
                   // both Name must be free discribable
               }

               if (nCommandType_ID == (int)Definition.RoutingCommands.SENDINGCOMMANDTOMACHINE) // sending command to machine
               {
                   if (grid.ColSel == 2 || grid.ColSel == 3)
                   {
                       Select_Form select_Form = new Select_Form(this, PopupLocation, nCommandType_ID, grid);
                       select_Form.ShowDialog();
                   }
               }
               if (nCommandType_ID == (int)Definition.RoutingCommands.CREATERESERVATION
                   || nCommandType_ID == (int)Definition.RoutingCommands.DELETERESERVATION) // create/delete reservation
               {
                   if (grid.ColSel == 2)
                   {
                       Select_Form select_Form = new Select_Form(this, PopupLocation, nCommandType_ID, grid);
                       select_Form.ShowDialog();
                   }
               }
           }
       }

      

       private void c1FlexGrid_Commands_AfterEdit(object sender, RowColEventArgs e)
       {
           c1FlexGrid_CommandsUpdate();
           CheckForErrorText("Command");
        
       }

       public void c1FlexGrid_Update()
       {
           if (nSelectedRoutingPositionEntry_ID > 0)
           {
               c1FlexGrid_Commands.Clear();
               ds_Commands.Clear();
               string SQL_Statement_Commands = GetSQL_Statement_Commands(nSelectedRoutingPositionEntry_ID);
               da_Commands = myHC.GetAdapterFromSQLCommand(SQL_Statement_Commands);

               da_Commands.Fill(ds_Commands);
               if (ds_Commands.Tables[0] != null)
               {
                   CommandTable = ds_Commands.Tables[0];
                   c1FlexGrid_Commands.DataSource = CommandTable;
                   c1FlexGrid_Commands.Cols[6].Visible = false; //hide "idrouting_commands"
                   c1FlexGrid_Commands.Cols[7].Visible = false; //hide "RoutingPositionEntry_ID"
                   CreateSelectboxForCommands();

                   if (nSelectedRoutingPositionEntry_ID > 0)
                   {
                       c1FlexGrid_CommandsUpdate();
                   }
                   //  CreateSelectboxForMachines();
               }
           }
          
       }

       private void c1FlexGrid_Commands_AfterSort(object sender, SortColEventArgs e)
       {         
           c1FlexGrid_CommandsUpdate();
       }

       private void c1FlexGrid_Commands_CellChanged(object sender, RowColEventArgs e)
       {
           C1FlexGrid grid = sender as C1FlexGrid;
          
           if (e.Row > 0 && e.Col == 1)
           {
                {
                   grid.SetData(e.Row, 2, "");
                   grid.SetData(e.Row, 3, "");
                   grid.SetData(e.Row, 4, "");
                   grid.SetData(e.Row, 5, "");
                   grid.SetData(e.Row, 6, "");
               }
           }
           grid.Update();
           
       }

       private string[] GetStringArrayForErrorText(string strType, int nTypeID)
       {
           switch(strType)
           {
               case "Condition":
                   for(int k=0;k<nConditionInputArray.Length;k++)
                   {
                       if (nTypeID == Int32.Parse(nConditionInputArray[k][0].ToString()))
                       {
                           return nConditionInputArray[k];
                       }
                   }
                   break;

               case "Command":
                   for (int k = 0; k < nCommandInputArray.Length; k++)
                   {
                       if (nTypeID == Int32.Parse(nCommandInputArray[k][0].ToString()))
                       {
                           return nCommandInputArray[k];
                       }
                   }
                   break;
           }
           
               
           return null;
       }
 
       private void c1FlexGrid_Commands_GetCellErrorInfo(object sender, GetErrorInfoEventArgs e)
       {

            C1FlexGrid grid = sender as C1FlexGrid;
       
           string strValue = null;
           int nCommand = -1;
           if (e.Row <= 0) { return; }
          
           try
           {
               nCommand = Int32.Parse(grid.GetData(e.Row, "Command").ToString());
           }
           catch {  return; }

           if (grid.Cols[e.Col].Name == "Command" && e.Row > 0)
           {
               if (nCommand == 0)
               {
                   e.ErrorText = "Please select a command!";
               }
               else 
               {
                   e.ErrorText = ""; 
                 
               }
               
           }

           if (grid.Cols[e.Col].Name == "Value0" && e.Row > 0) // 
           {
               if (nCommand > 0)
               {
                   strValue = grid[e.Row, "Value0"].ToString();
                   if (strValue.Length <= 0 || strValue.StartsWith("<Please select"))
                   {
                       try
                       {
                           string[] nArray = GetStringArrayForErrorText("Command", nCommand);
                           e.ErrorText = nArray[1];
                       }
                       catch { }
                   }
                   else
                   {
                       e.ErrorText = "";
                      
                   }
               }
           }

           if (grid.Cols[e.Col].Name == "Value1" && e.Row > 0) // 
           {
               if (nCommand > 0)
               {
                   strValue = grid[e.Row, "Value1"].ToString();
                   if (strValue.Length <= 0 || strValue.StartsWith("<Please select"))
                   {
                       try{
                         string[] nArray = GetStringArrayForErrorText("Command", nCommand);
                         e.ErrorText = nArray[2];
                       }
                       catch { }  
                   }
                   else
                   {
                       e.ErrorText = "";   
                   }
               }
           }

           if (grid.Cols[e.Col].Name == "Value2" && e.Row > 0) // 
           {
               if (nCommand > 0)
               {
                   strValue = grid[e.Row, "Value2"].ToString();
                   if (strValue.Length <= 0 || strValue.StartsWith("<Please select"))
                   {
                       try{
                          string[] nArray = GetStringArrayForErrorText("Command", nCommand);
                          e.ErrorText = nArray[3];
                       }
                       catch { }
                   }
                   else
                   {
                       e.ErrorText = "";
                   }
               }
           }

           if (grid.Cols[e.Col].Name == "Value3" && e.Row > 0) // 
           {
               if (nCommand > 0)
               {
                   strValue = grid[e.Row, "Value3"].ToString();
                   if (strValue.Length <= 0 || strValue.StartsWith("<Please select"))
                   {
                       try{
                          string[] nArray = GetStringArrayForErrorText("Command", nCommand);
                         e.ErrorText = nArray[4];
                       }
                       catch { }
                   }
                   else
                   {
                       e.ErrorText = "";
                      
                   }
               }
           }

       }

       // Conditions OwnerDraw
       private void c1FlexGrid_Conditions_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
       {
   
           C1.Win.C1FlexGrid.C1FlexGrid _flex = sender as C1.Win.C1FlexGrid.C1FlexGrid;
           // only apply styles to scrollable cells
           if (e.Row < _flex.Rows.Fixed || e.Col < _flex.Cols.Fixed)
               return;

           // get underlying DataRow
           int indexRow = _flex.Rows[e.Row].DataIndex;
           int indexCol = _flex.Cols[e.Col].DataIndex;
           if (indexRow < 0) return;
          
           if (bConditionTableChanged)
           {
              
              CurrencyManager cm = (CurrencyManager)BindingContext[_flex.DataSource, _flex.DataMember];
              
              DataRowView drv = cm.List[indexRow] as DataRowView;
             
            
              switch (drv.Row.RowState)
                {
                    case DataRowState.Added:
                       // e.Style = _flex.Styles["Added"];
                        SetConditionSaveButtonEnabled(true);
                        //  bConditionTableChanged = true;
                        break;

                    case DataRowState.Modified:
                     //   e.Style = _flex.Styles["Modified"];
                      //  e.Style.BackColor = Color.Red;
                        SetConditionSaveButtonEnabled(true);
                        //  bConditionTableChanged = true;
                       
                        break;

                    default:
                    //    e.Style.BackColor = Color.White;
                        break;
                }
           }
        //   e.Handled = true;
       }

       // command OwnerDraw
       private void c1FlexGrid_Commands_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
       {

           C1.Win.C1FlexGrid.C1FlexGrid _flex = sender as C1.Win.C1FlexGrid.C1FlexGrid;
           // only apply styles to scrollable cells
           if (e.Row < _flex.Rows.Fixed || e.Col < _flex.Cols.Fixed)
               return;

           // get underlying DataRow
           int indexRow = _flex.Rows[e.Row].DataIndex;
           if (indexRow < 0) return;
           DataRowView drv = null;
           try
           {
               CurrencyManager cm = (CurrencyManager)BindingContext[_flex.DataSource, _flex.DataMember];
                drv = cm.List[indexRow] as DataRowView;
           }
           catch { }

           if (bCommandTableChanged && drv!=null)
           {
               // select style based on row state
               switch (drv.Row.RowState)
               {
                   case DataRowState.Added:
                     //  e.Style = _flex.Styles["Added"];
                       //  bCommandTableChanged = true;
                       // SetConditionSaveButtonEnabled(true);
                       break;
                   case DataRowState.Modified:
                      // e.Style = _flex.Styles["Modified"];
                       //  bCommandTableChanged = true;
                       //  SetConditionSaveButtonEnabled(true);
                       break;

                   default:
                       break;
               }
           }
       }
       
       private void treeView_routing_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
       {
           if (e.Label != null)
           {
               if (e.Label.Length > 0)
               {
                   if (e.Label.IndexOfAny(new char[] { '@', '\'', ',', '!' }) == -1)
                   {
                       // Stop editing without canceling the label change.
                       e.Node.EndEdit(false);
                       string strNodeText = e.Label;
                       treeView_routing.LabelEdit = false;
                       myHC.UpdateDescriptionFromRoutingPositionEntriesByRoutingPositionEntriesID(nSelectedRoutingPositionEntry_ID, strNodeText);

                   }
                   else
                   {
                       /* Cancel the label edit action, inform the user, and 
                          place the node in edit mode again. */
                       e.CancelEdit = true;
                       MessageBox.Show("Invalid tree node label.\n" +
                          "The invalid characters are: '@',''', ',', '!'",
                          "Node Label Edit");
                       e.Node.BeginEdit();
                   }
               }
               else
               {
                   /* Cancel the label edit action, inform the user, and 
                      place the node in edit mode again. */
                   e.CancelEdit = true;
                   MessageBox.Show("Invalid tree node label.\nThe label cannot be blank",
                   "Node Label Edit");
                   e.Node.BeginEdit();
               }

               e.Node.EndEdit(true);

           }
       }

   
       private void c1FlexGrid_Conditions_DoubleClick(object sender, EventArgs e)
       {
           ShowEditInfo();
         
       }

       private void ShowEditInfo()
       {
           if (ribbonCheckBox_CheckCondition.Checked) { MessageBox.Show("To edit this table uncheck \"check Conditions\""); }
       }

      

       private bool ConditionChanged()
       {
          return bConditionTableChanged;
       }


       private bool CommandsChanged()
       {
           return bCommandTableChanged;
       }

       private bool AskForDataSave()
       {
           bool bReturn = true;
          
           CheckForErrorText("Condition");
           CheckForErrorText("Command");
           if (!bInputConditionOK || !bInputCommandOK)
           {
               DialogResult r = MessageBox.Show("Input data not complete! - Correct the values or delete the row!", "Input data not correct/incomplete!", MessageBoxButtons.OK, MessageBoxIcon.Error);
              
               return false;
           }
           else
           if (ConditionChanged() || CommandsChanged())
           {
               DialogResult result = MessageBox.Show("Data changed - Do you want to save the data?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
               if (result == DialogResult.No)
               {
                   bReturn = true;
                   bConditionTableChanged = false;
                   bCommandTableChanged = false;
               }
               else
               if (result == DialogResult.Yes)
               {
                   bReturn = true;
                   if(ConditionChanged())
                   {
                        button_Condition_Save_Click(null, null);
                   }
                   if (CommandsChanged())
                   {
                       button_Command_Save_Click(null, null);
                   }
               }
               else
               if (result == DialogResult.Cancel)
               {

               }
           }
          
           return bReturn;
       }

       private void Routing_Form_FormClosing(object sender, FormClosingEventArgs e)
       {
           CheckForErrorText("Condition");
           CheckForErrorText("Command");

           myHC.UpdateUserInputDataByName("RoutingFormSplitter1Distance", splitContainer1.SplitterDistance.ToString());
           myHC.UpdateUserInputDataByName("RoutingFormSplitter2Distance", splitContainer2.SplitterDistance.ToString());

           myHC.UpdateUserInputDataByName("RoutingFormWidth", this.Width.ToString());
           myHC.UpdateUserInputDataByName("RoutingFormHeight", this.Height.ToString());

           if (!bInputConditionOK || !bInputCommandOK)
           {
               DialogResult r = MessageBox.Show("Input data not complete! - Correct the values or press 'Cancel' to ignore the changes and close the window!", "Input data not correct/incomplete!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
               if (r.ToString() == "Cancel")
               {
                   e.Cancel = false;
               }
               else
               {
                   e.Cancel = true;
               }
           }
         /*  else if (!bInputCommandOK)
           {
               DialogResult r = MessageBox.Show("Command input data not complete! - Correct the values or delete the row!", "Input data not correct/incomplete!", MessageBoxButtons.OK, MessageBoxIcon.Error);

               e.Cancel = true;
           }*/
           else
           if (ConditionChanged() || CommandsChanged())
           {
               DialogResult r = MessageBox.Show("Data changed - Do you want to save the data?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
               if (r == DialogResult.No)
               {
                  // do nthing - close without saving
               }
               else if (r == DialogResult.Yes)
               {
                   if (ConditionChanged())
                   {
                       button_Condition_Save_Click(null, null);
                   }
                   if (CommandsChanged())
                   {
                       button_Command_Save_Click(null, null);
                   }
               }else if (r == DialogResult.Cancel)
               {
                   e.Cancel = true;
               }
           }

       }

       private bool CheckForErrorText(string strType)
       {
           bool ret = false;
           switch (strType)
           {
               case "Condition":
                 
                   if (c1FlexGrid_Conditions.Rows.Count > 1)
                   {
                       if (c1FlexGrid_Conditions.DataSource != null)
                       {
                           bInputConditionOK = true;
                           for (int nRow = 1; nRow < c1FlexGrid_Conditions.Rows.Count; nRow++)   // all rows
                           {
                               int nTypeID = -1;
                               string[] ErrorTextArray = new string[5];

                               try
                               {
                                   Int32.TryParse(c1FlexGrid_Conditions[nRow, 1].ToString(), out nTypeID);   // ConditionID
                                   ErrorTextArray = GetStringArrayForErrorText(strType, nTypeID);
                                   if (nTypeID == 0 && nRow > 0)// Condition 0 is "<Please select>"
                                   {
                                       bInputConditionOK = false; //Console.WriteLine("nTypeID == 0");
                                   }
                               }
                               catch { }

                               if (nTypeID > 0)
                               {
                                   for (int nCol = 2; nCol < 5; nCol++)
                                   {
                                       try
                                       {
                                           if (ErrorTextArray[nCol] != null)
                                           {
                                               if (ErrorTextArray[nCol] != "")
                                               {
                                                   if (c1FlexGrid_Conditions[nRow, nCol + 1].ToString().Length <= 0)
                                                   {
                                                       // Console.WriteLine("nRow:" + nRow + " - nCol:" + nCol);
                                                       bInputConditionOK = false;
                                                   }
                                               }
                                           }
                                       }
                                       catch { }
                                   }
                               }
                               else { }
                           }
                       }
                     
                       pictureBox_Conditions.Visible = true;
                       if (!bInputConditionOK) 
                       {
                           SetConditionSaveButtonEnabled(false);
                           pictureBox_Conditions.Image = _imgList.Images["RedCross"]; 
                       } else {
                           if (!ribbonCheckBox_CheckCondition.Checked) { SetConditionSaveButtonEnabled(true); }
                           pictureBox_Conditions.Image = _imgList.Images["GreenTag"];
                       }
                   }
                   else { pictureBox_Conditions.Visible = false; }
                  break;

               case "Command":
                  if (c1FlexGrid_Commands.Rows.Count > 1)
                  {
                      bInputCommandOK = true;
                      for (int nRow = 0; nRow < c1FlexGrid_Commands.Rows.Count; nRow++)   // all rows
                      {
                          int nTypeID = -1;
                           string[] ErrorTextArray = new string[5];
                           try
                           {
                               Int32.TryParse(c1FlexGrid_Commands[nRow, 1].ToString(), out nTypeID);   // ConditionID
                               ErrorTextArray = GetStringArrayForErrorText(strType, nTypeID);
                           }
                           catch { }

                          if (nTypeID == 0 && nRow > 0)// Condition 0 is "<Please select>"
                          {
                              bInputCommandOK = false; //Console.WriteLine("nTypeID == 0");
                          }

                          if (nTypeID > 0)
                          {
                              for (int nCol = 1; nCol < 5; nCol++)
                              {
                                  try
                                  {
                                      if (ErrorTextArray[nCol] != null)
                                      {
                                          if (ErrorTextArray[nCol] != "")
                                          {
                                              if (c1FlexGrid_Commands[nRow, nCol + 1].ToString().Length <= 0)
                                              {
                                                  //Console.WriteLine("nRow:" + nRow + " - nCol:" + nCol);
                                                  bInputCommandOK = false;
                                              }
                                          }
                                      }
                                  }
                                  catch { }
                              }
                          }
                      }
                     // string tcom = bInputCommandOK ? "command true" : "command false";
                    //  Console.WriteLine(tcom);
                      pictureBox_Command.Visible = true;
                      if (!bInputCommandOK) {
                          SetCommandSaveButtonEnabled(false);
                          pictureBox_Command.Image = _imgList.Images["RedCross"]; 
                      } else {
                          if (!ribbonCheckBox_CheckCondition.Checked) { SetCommandSaveButtonEnabled(true); }
                          pictureBox_Command.Image = _imgList.Images["GreenTag"]; 
                      }
                  }
                  else { pictureBox_Command.Visible = false; }
                   break;
           }
          
           return ret;
       }

       private void c1FlexGrid_Conditions_ChangeEdit(object sender, EventArgs e)
       {
           bConditionTableChanged = true;
           
           if (c1FlexGrid_Conditions.Editor != null)
            {
                Console.WriteLine(c1FlexGrid_Conditions.Editor.Text);
            }
          // CheckForErrorText("Condition");
       }

       private void c1FlexGrid_Conditions_Click(object sender, EventArgs e)
       {
           CheckForErrorText("Condition");
           nLastSelectedRowConditions = c1FlexGrid_Conditions.RowSel;
       }

       private void c1FlexGrid_Commands_Click(object sender, EventArgs e)
       {
           CheckForErrorText("Command");
           nLastSelectedRowCommands = c1FlexGrid_Commands.RowSel;
       }

           
       private void c1FlexGrid_Commands_MouseEnterCell(object sender, RowColEventArgs e)
       {
           C1FlexGrid grid = sender as C1FlexGrid;
       
           TextBox tb = grid.Editor as TextBox;
           if (tb != null)
           {
               nCoursorPositionCommands = tb.SelectionStart;
           }
       }

     
       private void button_AddChoise_Click(object sender, EventArgs e)
       {
           if (nSelectedRowCommands > 0 && nSelectedColCommands > 2)
           {
               try
               {

                   string strReplace = null;
                   string strOption = (string)c1FlexGrid_Commands.GetData(nSelectedRowCommands, nSelectedColCommands);
                   string strSelection = comboBox_Choice.SelectedItem.ToString();
                   if (strSelection.Length > 0)
                   {
                       if (nCoursorPositionCommands > 0)
                       {
                           string strOptionPart = strOption.Substring(0, nCoursorPositionCommands);
                           strReplace = strOptionPart + strSelection + strOption.Substring(nCoursorPositionCommands, strOption.Length - nCoursorPositionCommands);
                       }
                       else
                       {
                           strReplace = strOption + strSelection;
                       }
                       c1FlexGrid_Commands.SetData(nSelectedRowCommands, nSelectedColCommands, (string)strReplace);
                   }
               }
               catch { }
           }
       }


       private void pictreBox1_Click(object sender, EventArgs e)
       {
           
       }

       private void button_ColorPicker_Click(object sender, EventArgs e)
       {
           if (nSelectedRowCommands > 0 && nSelectedColCommands > 2)
           {
               try
               {

                   string strReplace = null;
                   string strOption = (string)c1FlexGrid_Commands.GetData(nSelectedRowCommands, nSelectedColCommands);
                   string strSelection = null;

                   ColorDialog MyDialog = new ColorDialog();
                   // Keeps the user from selecting a custom color.
                   MyDialog.AllowFullOpen = true;
                   MyDialog.ShowHelp = true;
                   // Sets the initial color select to the current text color.
                   Color col = new Color();
                   // Update the text box color if the user clicks OK 
                   if (MyDialog.ShowDialog() == DialogResult.OK)
                   {
                       col = MyDialog.Color;
                       int nColDB = col.R + (col.G * 256) + (col.B * 256 * 256);
                       strSelection = nColDB.ToString();

                       if (strSelection.Length > 0 && nColDB > 0)
                       {
                           if (nCoursorPositionCommands > 0)
                           {
                               string strOptionPart = strOption.Substring(0, nCoursorPositionCommands);
                               strReplace = strOptionPart + strSelection + strOption.Substring(nCoursorPositionCommands, strOption.Length - nCoursorPositionCommands);
                           }
                           else
                           {
                               strReplace = strOption + strSelection;
                           }
                           c1FlexGrid_Commands.SetData(nSelectedRowCommands, nSelectedColCommands, (string)strReplace);
                       }
                   }
               }
               catch { }
           }

       }

       private void c1FlexGrid_Conditions_MouseDoubleClick(object sender, MouseEventArgs e)
       {
           if (!ribbonCheckBox_CheckCondition.Checked)
           {
               C1FlexGrid grid = sender as C1FlexGrid;
               if (grid.RowSel > 0)
               {
                  
                   Point pt = new Point(e.X, e.Y);
                   Point FormLocation = this.Location;

                   // Point groupLocation = groupBox2.Location;
                   // Point groupLocation = splitContainer2.Panel1.Location;
                   Point groupLocationSplitter = splitContainer1.Panel2.Location;
                   Point groupLocation = grid.Location;

                   Point PopupLocation = new Point(FormLocation.X + groupLocation.X + e.X + groupLocationSplitter.X - 40, FormLocation.Y + 145 + groupLocation.Y + e.Y);
                   int nCondition_ID = (int)grid[grid.RowSel, "Condition"];


                   if (nCondition_ID == (int)Definition.RoutingConditions.SAMPLEONPOS || // sample on pos
                       nCondition_ID == (int)Definition.RoutingConditions.SAMPLEPRIORITY || // sample priority
                       nCondition_ID == (int)Definition.RoutingConditions.SAMPLETYPE) // sample type
                   {
                       if (grid.ColSel == 3)
                       {
                           Select_Form select_Form = new Select_Form(this, PopupLocation, nCondition_ID, grid, true);
                           select_Form.ShowDialog();
                       }
                   }
               }
           }
       }

      

       private void c1Button_Condition_up_Click(object sender, EventArgs e)
       {
           C1FlexGrid flex = c1FlexGrid_Conditions;
           int index = nLastSelectedRowConditions - 1;
           int NewUpperIndex = nLastSelectedRowConditions;
           int nIdrouting_condition = -1;
           int nUpperIdrouting_condition = -1;
           bool bGotRoutingCondition_ID = false;

           if (nLastSelectedRowConditions > 1)
           {
               bGotRoutingCondition_ID = Int32.TryParse(flex.GetData(nLastSelectedRowConditions, "idrouting_conditions").ToString(), out nIdrouting_condition);
               Int32.TryParse(flex.GetData((nLastSelectedRowConditions - 1), "idrouting_conditions").ToString(), out nUpperIdrouting_condition);
               // move it up
               if (bGotRoutingCondition_ID)
               {
                   string strSQL_Statement = @"UPDATE routing_conditions SET SortOrder=" + index + " WHERE idrouting_conditions = " + nIdrouting_condition + ";"
                       + " UPDATE routing_conditions SET SortOrder=" + NewUpperIndex + " WHERE idrouting_conditions = " + nUpperIdrouting_condition;
                   myHC.return_SQL_Statement(strSQL_Statement);
                   LoadConditionsToGrid(nSelectedRoutingPositionEntry_ID);
                   for (int i = 0; i < flex.Rows.Count; i++)
                   {
                       if (i == index)
                       {
                           flex.Rows[i].Selected = true;
                           nLastSelectedRowConditions = i;
                        }
                       else
                       {
                           flex.Rows[i].Selected = false;
                       }
                   }
                   
               }
           }
       }

       private void c1Button_Condition_down_Click(object sender, EventArgs e)
       {
           C1FlexGrid flex = c1FlexGrid_Conditions;
           int index = nLastSelectedRowConditions + 1;
           int NewLowerIndex = nLastSelectedRowConditions;
           int nIdrouting_condition = -1;
           int nLowerIdrouting_condition = -1;
           bool bGotRoutingCondition_ID = false;

           if(nLastSelectedRowConditions>0 &&nLastSelectedRowConditions < (flex.Rows.Count - 1))
           {
               bGotRoutingCondition_ID = Int32.TryParse(flex.GetData(nLastSelectedRowConditions, "idrouting_conditions").ToString(), out nIdrouting_condition);
               Int32.TryParse(flex.GetData((nLastSelectedRowConditions + 1), "idrouting_conditions").ToString(), out nLowerIdrouting_condition);
               // move it up
               if (bGotRoutingCondition_ID)
               {
                   string strSQL_Statement = @"UPDATE routing_conditions SET SortOrder=" + index + " WHERE idrouting_conditions = " + nIdrouting_condition + ";"
                       + " UPDATE routing_conditions SET SortOrder=" + NewLowerIndex + " WHERE idrouting_conditions = " + nLowerIdrouting_condition;
                   myHC.return_SQL_Statement(strSQL_Statement);
                   LoadConditionsToGrid(nSelectedRoutingPositionEntry_ID);
                   for (int i = 0; i < flex.Rows.Count ; i++)
                   {
                       if (i == index)
                       {
                           nLastSelectedRowConditions = i;
                           flex.Rows[i].Selected = true;
                       }
                       else
                       {
                           flex.Rows[i].Selected = false;
                       }
                   }
               
               }
           }
       }

       

       private void c1Button_command_up_Click(object sender, EventArgs e)
       {
           C1FlexGrid flex = c1FlexGrid_Commands;
           int index = nLastSelectedRowCommands - 1;
           int NewUpperIndex = nLastSelectedRowCommands;
           int nIdrouting_condition = -1;
           int nUpperIdrouting_condition = -1;
           bool bGotRoutingCondition_ID = false;

           if (nLastSelectedRowCommands > 1)
           {
               bGotRoutingCondition_ID = Int32.TryParse(flex.GetData(nLastSelectedRowCommands, "idrouting_commands").ToString(), out nIdrouting_condition);
               Int32.TryParse(flex.GetData((nLastSelectedRowCommands - 1), "idrouting_commands").ToString(), out nUpperIdrouting_condition);
               // move it up
               if (bGotRoutingCondition_ID)
               {
                   string strSQL_Statement = @"UPDATE routing_commands SET SortOrder=" + index + " WHERE idrouting_commands = " + nIdrouting_condition + ";"
                       + " UPDATE routing_commands SET SortOrder=" + NewUpperIndex + " WHERE idrouting_commands = " + nUpperIdrouting_condition;
                   myHC.return_SQL_Statement(strSQL_Statement);
                   LoadConditionsToGrid(nSelectedRoutingPositionEntry_ID);
                   for (int i = 0; i < flex.Rows.Count; i++)
                   {
                       if (i == index)
                       {
                           flex.Rows[i].Selected = true;
                           nLastSelectedRowCommands = i;
                       }
                       else
                       {
                           flex.Rows[i].Selected = false;
                       }
                   }

               }
           }
       }

       private void c1Button_command_down_Click(object sender, EventArgs e)
       {
           C1FlexGrid flex = c1FlexGrid_Commands;
           int index = nLastSelectedRowCommands + 1;
           int NewUpperIndex = nLastSelectedRowCommands;
           int nIdrouting_condition = -1;
           int nUpperIdrouting_condition = -1;
           bool bGotRoutingCondition_ID = false;

           if (nLastSelectedRowCommands > 0 && nLastSelectedRowCommands < (flex.Rows.Count - 1))
            {
               bGotRoutingCondition_ID = Int32.TryParse(flex.GetData(nLastSelectedRowCommands, "idrouting_commands").ToString(), out nIdrouting_condition);
               Int32.TryParse(flex.GetData((nLastSelectedRowCommands + 1), "idrouting_commands").ToString(), out nUpperIdrouting_condition);
               // move it up
               if (bGotRoutingCondition_ID)
               {
                   string strSQL_Statement = @"UPDATE routing_commands SET SortOrder=" + index + " WHERE idrouting_commands = " + nIdrouting_condition + ";"
                       + " UPDATE routing_commands SET SortOrder=" + NewUpperIndex + " WHERE idrouting_commands = " + nUpperIdrouting_condition;
                   myHC.return_SQL_Statement(strSQL_Statement);
                   LoadConditionsToGrid(nSelectedRoutingPositionEntry_ID);
                   for (int i = 0; i < flex.Rows.Count; i++)
                   {
                       if (i == index)
                       {
                           flex.Rows[i].Selected = true;
                           nLastSelectedRowCommands = i;
                       }
                       else
                       {
                           flex.Rows[i].Selected = false;
                       }
                   }

               }
           }

       }

       private void c1FlexGrid_Conditions_MouseUp(object sender, MouseEventArgs e)
       {
            Point p = new Point(e.X, e.Y);
            int k = 0;
            HitTestInfo tHitTestTRouting = c1FlexGrid_Conditions.HitTest(p);

            if (e.Button == MouseButtons.Right)
            {
                ContextMenu contextMenuForCopyPasteConditions = new ContextMenu();
                contextMenuForCopyPasteConditions.MenuItems.Clear();

            
                for(int i = 1; i< c1FlexGrid_Conditions.Rows.Count;i++){
                    if(c1FlexGrid_Conditions.Rows[i].Selected){
                        k++;
                    }
                }

                if (k <= 0)
                {
                    contextMenuForCopyPasteConditions.MenuItems.Add("Copy");
                }else
                if (k == 1)
                {
                    contextMenuForCopyPasteConditions.MenuItems.Add("Copy " + k + " row", new EventHandler(Copy_SelectedConditionRows_Click));
                }
                else
                {
                    contextMenuForCopyPasteConditions.MenuItems.Add("Copy " + k + " rows", new EventHandler(Copy_SelectedConditionRows_Click));
                }

             
                if (k <= 0)
                {
                    contextMenuForCopyPasteConditions.MenuItems[0].Enabled = false;
                }
                contextMenuForCopyPasteConditions.MenuItems.Add("Paste", new EventHandler(Paste_SelectedConditionRows_Click));

                if (nCopyPasteType == (int)Definition.CopyPasteObjectType.ROUTINGFORM_CONDITIONSELECTEDROWS && nSelectedRoutingPositionEntry_ID>0)
                {
                    contextMenuForCopyPasteConditions.MenuItems[1].Enabled = true;
                }
                else
                {
                    contextMenuForCopyPasteConditions.MenuItems[1].Enabled = false;
                }
                contextMenuForCopyPasteConditions.Show(c1FlexGrid_Conditions, p);
            }
       }

       private void Copy_SelectedConditionRows_Click(object sender, EventArgs e)
       {
           nCopyPasteType = (int)Definition.CopyPasteObjectType.ROUTINGFORM_CONDITIONSELECTEDROWS;
           dt_Copy.Clear();
          
           myCopyPaste.CopyFromFlexGrid(c1FlexGrid_Conditions, dt_Copy, false);
       }

       private void Paste_SelectedConditionRows_Click(object sender, EventArgs e)
       {


           string[][] strRoutingPositionEntry_ID = new string[2][];
           strRoutingPositionEntry_ID[0] = new string[2];
           strRoutingPositionEntry_ID[0][0] = "routingPositionEntry_ID";
           strRoutingPositionEntry_ID[0][1] = nSelectedRoutingPositionEntry_ID.ToString();
           strRoutingPositionEntry_ID[1] = new string[2];
           strRoutingPositionEntry_ID[1][0] = "Condition_comply";
           strRoutingPositionEntry_ID[1][1] = "0";

           myCopyPaste.PasteToFlexGrid(c1FlexGrid_Conditions, dt_Copy, ConditionTable, strRoutingPositionEntry_ID);
       }

        // CopyPaste for commands
       private void c1FlexGrid_Commands_MouseUp(object sender, MouseEventArgs e)
       {

           Point p = new Point(e.X, e.Y);
           int k = 0;
           HitTestInfo tHitTestTRouting = c1FlexGrid_Commands.HitTest(p);

           if (e.Button == MouseButtons.Right)
           {
               ContextMenu contextMenuForCopyPasteCommands = new ContextMenu();
               contextMenuForCopyPasteCommands.MenuItems.Clear();


               for (int i = 1; i < c1FlexGrid_Commands.Rows.Count; i++)
               {
                   if (c1FlexGrid_Commands.Rows[i].Selected)
                   {
                       k++;
                   }
               }

               if (k <= 0)
               {
                   contextMenuForCopyPasteCommands.MenuItems.Add("Copy");
               }
               else
                   if (k == 1)
                   {
                       contextMenuForCopyPasteCommands.MenuItems.Add("Copy " + k + " row", new EventHandler(Copy_SelectedCommandRows_Click));
                   }
                   else
                   {
                       contextMenuForCopyPasteCommands.MenuItems.Add("Copy " + k + " rows", new EventHandler(Copy_SelectedCommandRows_Click));
                   }


               if (k <= 0)
               {
                   contextMenuForCopyPasteCommands.MenuItems[0].Enabled = false;
               }
               contextMenuForCopyPasteCommands.MenuItems.Add("Paste", new EventHandler(Paste_SelectedCommandRows_Click));

               if (nCopyPasteType == (int)Definition.CopyPasteObjectType.ROUTINGFORM_COMMANDSELECTEDROWS && nSelectedRoutingPositionEntry_ID > 0)
               {
                   contextMenuForCopyPasteCommands.MenuItems[1].Enabled = true;
               }
               else
               {
                   contextMenuForCopyPasteCommands.MenuItems[1].Enabled = false;
               }
               contextMenuForCopyPasteCommands.Show(c1FlexGrid_Commands, p);
           }

       }

       private void Copy_SelectedCommandRows_Click(object sender, EventArgs e)
       {
           nCopyPasteType = (int)Definition.CopyPasteObjectType.ROUTINGFORM_COMMANDSELECTEDROWS;
           dt_Copy.Clear();

           myCopyPaste.CopyFromFlexGrid(c1FlexGrid_Commands, dt_Copy, false);
       }

       private void Paste_SelectedCommandRows_Click(object sender, EventArgs e)
       {


           string[][] strRoutingPositionEntry_ID = new string[1][];
           strRoutingPositionEntry_ID[0] = new string[2];
           strRoutingPositionEntry_ID[0][0] = "routingPositionEntry_ID";
           strRoutingPositionEntry_ID[0][1] = nSelectedRoutingPositionEntry_ID.ToString();
         
           myCopyPaste.PasteToFlexGrid(c1FlexGrid_Commands, dt_Copy, CommandTable, strRoutingPositionEntry_ID);
       }

       private void c1FlexGrid_Commands_ChangeEdit(object sender, EventArgs e)
       {
           bCommandTableChanged = true;
       }


       private void WriteStatusbarLeft(string strText, bool bError = false)
       {
           c1StatusBar_Routing.LeftPaneItems.Clear();
           c1StatusBar_Routing.LeftPaneItems.Add(strText);
           if (bError)
           {
               c1StatusBar_Routing.ForeColorOuter = Color.Red;
           }
           else
           {
               c1StatusBar_Routing.ForeColorOuter = Color.Black;
           }

       }

       private void WriteStatusbarRight(string strText, bool bError = false)
       {
           c1StatusBar_Routing.RightPaneItems.Clear();
           c1StatusBar_Routing.RightPaneItems.Add(strText);
           if (bError)
           {
               c1StatusBar_Routing.ForeColorOuter = Color.Red;
           }
           else
           {
               c1StatusBar_Routing.ForeColorOuter = Color.Black;
           }

       }

       private void ribbonCheckBox1_CheckedChanged(object sender, EventArgs e)
       {

           treeView2.Nodes.Clear();
           if (ribbonCheckBox1.Checked == true)
           {
             
               ribbonTextBox_Search.Enabled = true;
               treeView2.Visible = true;
           }
           if (ribbonCheckBox1.Checked == false)
           {
               treeView2.Visible = false;
               ribbonTextBox_Search.Enabled = false;
           }
       }

       private void p(object sender, EventArgs e)
       {
           Populate(treeView2.Nodes);
       }

      

     

    }

  
}
