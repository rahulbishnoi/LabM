using System;
using System.Data;
using System.Windows.Forms;
using MySQL_Helper_Class;
using Logging;
using Definition;
namespace LabManager
{
    public partial class SampleListTGA : Form
    {
        MySQL_HelperClass myHC = new MySQL_HelperClass();
        Definitions myThorDef = new Definitions();
        Save mySave = new Save("TGA Sample List");

        public SampleListTGA()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
        }
        private void SampleListTGA_Load(object sender, EventArgs e)
        {
            string SQL_Statement = null;
            SQL_Statement = "SELECT SampleID,TimeStampInserted,TimestampFinished,LOI from sample_tga";
            DataSet ds_TGASamples = myHC.GetDataSetFromSQLCommand(SQL_Statement);
            DataTable dt_TGASamples = ds_TGASamples.Tables[0];
            c1TrueDBGrid1.DataSource = dt_TGASamples;


            this.c1TrueDBGrid1.SetDataBinding(dt_TGASamples, "");
            // size the description column
            this.c1TrueDBGrid1.Splits[0].DisplayColumns["SampleID"].AutoSize();
            this.c1TrueDBGrid1.Splits[0].DisplayColumns["TimeStampInserted"].AutoSize();
            this.c1TrueDBGrid1.Splits[0].DisplayColumns["TimestampFinished"].AutoSize();
            this.c1TrueDBGrid1.Splits[0].DisplayColumns["LOI"].AutoSize();
            // no dead area in the grid
            this.c1TrueDBGrid1.EmptyRows = true;
            this.c1TrueDBGrid1.ExtendRightColumn = true;

        }

        private void c1TrueDBGrid1_Click(object sender, EventArgs e)
        {
           
           
        
        }

       
    }
}
