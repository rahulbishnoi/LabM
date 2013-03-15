using System;
using System.Drawing;
using MySQL_Helper_Class;
using Definition;
using Logging;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using cs_IniHandlerDevelop;
using C1.Win.C1FlexGrid;
using Color_Helper;

namespace Magazine_Helper
{
    class MagazineHelper
    {
       
       MySQL_HelperClass myHC = new MySQL_HelperClass();
       Definitions myDef = new Definitions();
       Save mySave = new Save("MagazineHelper");
       IniStructure myIniHandler = new IniStructure();
       Definitions myDefinitions = new Definitions();
       ColorHelper myColorHelper = new ColorHelper();
       MagazineDimension magazineDim = new MagazineDimension();
       string SQL_Statement = null;
       DataSet ds_Magazine = null;
       DataTable dt_Magazine = null;
       int _nMagazine_ID = -1;
       DataSet ds_MagazineSamples = new DataSet();
       DataTable dt_MagazineSamples = new DataTable();
       DataSet ds_SortedSamples = new DataSet();

        public MagazineHelper(int nMagazine_ID)
        {
            _nMagazine_ID = nMagazine_ID;
            UpdateConfiguration();
           
        }

        public void UpdateConfiguration()
        {
            SQL_Statement = @"SELECT * FROM magazine_configuration WHERE idmagazine_configuration=" + _nMagazine_ID;
            ds_Magazine = myHC.GetDataSetFromSQLCommand(SQL_Statement);
            try
            {
                if (ds_Magazine != null)
                {
                    dt_Magazine = ds_Magazine.Tables[0];

                }
            }
            catch { }
        }

        public bool ConfigurationRecordFound()
        {
            bool bret = false;
            string strName = null;

            try
            {
                DataRow dr_Magazine = dt_Magazine.Rows[0];
                int nDimXtemp = (int)dr_Magazine["Dimension_X"];
                int nDimYtemp = (int)dr_Magazine["Dimension_Y"];
                int nSorttemp = (int)dr_Magazine["SortType_ID"];
                strName = (string)dr_Magazine["Name"].ToString();
               // if (nDimXtemp > 0 && nDimYtemp > 0 && nSorttemp > 0 && strName.Length > 0)
                if (nDimXtemp > 0 && nDimYtemp > 0 && nSorttemp >= 0 && strName.Length > 0)
                {
                    bret = true;
                }
                else
                {
                    bret = false;
                }
               
            }
            catch (Exception ex) { mySave.InsertRow((int)Definition.Message.D_ALARM, ex.ToString()); }

            return bret;
        }

        public MagazineDimension GetDimension()
        {
            magazineDim.DimX = 0;
            magazineDim.DimY = 0;

            try{

                DataRow dr_Magazine = dt_Magazine.Rows[0];
                magazineDim.DimX = (int)dr_Magazine["Dimension_X"];
                magazineDim.DimY = (int)dr_Magazine["Dimension_Y"];

            }catch{}

            return magazineDim;

        }

        public string GetMagazineName()
        {
            string strName = null;

            try{
                DataRow dr_Magazine = dt_Magazine.Rows[0];
                strName = (string)dr_Magazine["Name"].ToString();
            }
            catch (Exception ex) { mySave.InsertRow((int)Definition.Message.D_ALARM, ex.ToString()); }

            return strName;
        }


        public int GetSortType()
        {
            int nSort = -1;
            try
            {
                string strSQL_Statement = "SELECT SortType_ID FROM magazine_configuration  WHERE idmagazine_configuration=" + _nMagazine_ID;
                 nSort =  myHC.return_SQL_Statement(strSQL_Statement);
            }
            catch { }

            return nSort;
        }

        public int GetMachine_ID()
        {
            int nMachine_ID = -1;

            try
            {
                DataRow dr_Magazine = dt_Magazine.Rows[0];
                nMachine_ID = (int)dr_Magazine["Machine_ID"];
            }
            catch { }

            return nMachine_ID;
        }

        public bool GetForceFIFO()
        {
            bool bForceFIFO = true;

            try
            {
                DataRow dr_Magazine = dt_Magazine.Rows[0];
                bForceFIFO = (bool)dr_Magazine["ForceFIFO"];
            }
            catch { }

            return bForceFIFO;
        }

        public bool GetStopMode()
        {
            bool bStopMode = true;

            try
            {
                string strSQL_Statement = "SELECT StopMode FROM magazine_configuration  WHERE idmagazine_configuration=" + _nMagazine_ID;
               bStopMode = myHC.return_SQL_StatementAsBool(strSQL_Statement);
            }
            catch { }

            return bStopMode;
        }

        public void SetStopMode(bool bEnable)
        {
            string strSQL_Statement = "Update magazine_configuration SET StopMode=" + bEnable.ToString() + " WHERE idmagazine_configuration=" + _nMagazine_ID;
            try
            {
                myHC.return_SQL_Statement(strSQL_Statement);
            }
            catch { }

             
        }
        public int GetOutputPosition()
        {
            int nOutputPos = -1;

            try
            {
                DataRow dr_Magazine = dt_Magazine.Rows[0];
                Int32.TryParse(dr_Magazine["OutputPosition"].ToString(),out nOutputPos);
            }
            catch { }

            return nOutputPos;
        }
        public int GetInputPosition()
        {
            int nInputPos = -1;

            try
            {
                DataRow dr_Magazine = dt_Magazine.Rows[0];
                Int32.TryParse(dr_Magazine["InputPosition"].ToString(), out nInputPos);
            }
            catch { }

            return nInputPos;
        }


        public string GetMySqlCommandForSortedSamples(int nSortOrder)
        {
            string strSortOrder = null;

            switch (nSortOrder)
            {
                case 1:     //FIFO
                    strSortOrder = "TimestampInserted,MagazinePos";
                    break;
                case 2:
                    strSortOrder = "Priority DESC,MagazinePos";
                    break;
                case 3:
                    strSortOrder = "MagazinePos";
                    break;
                case 4:
                    strSortOrder = "SampleProgramType_ID,MagazinePos";
                    break;

                default:
                    strSortOrder = "MagazinePos";
                    break;
            }

           // MySql.Data.MySqlClient.MySqlCommand mycom_Magazine;
           return @"SELECT MagazinePos,SampleID,Priority,idactive_samples
                                                    FROM    sample_active WHERE MagazineDoneFlag=0 AND Magazine=" + _nMagazine_ID + " ORDER BY MagazineOrderForce DESC," + strSortOrder;
            //return mycom_Magazine;
        }

        public DataSet GetDatasetForSortedSamples()
        {
            
            ds_SortedSamples = null;
           // MySqlCommand myCommand = GetMySqlCommandForSortedSamples(GetSortType());

            try
            {
                ds_SortedSamples = myHC.GetDataSetFromSQLCommand(GetMySqlCommandForSortedSamples(GetSortType()));
            }
            catch { }

            return ds_SortedSamples;
        }

      /*  public string GetMagazineNameByID(int nMagazine_ID)
        {
            string retString=null;
            string SQL_Statement = @"SELECT Name FROM magazine_Configuration WHERE idmagazine_configuration=" + nMagazine_ID;
            retString = myHC.return_SQL_StatementAsString(SQL_Statement);
            return retString;
        }
        */

        public Image GetCircleBitmap(Size size, Color col, int nSampleDone=-1)
        {
            Bitmap bmp2 = new Bitmap(size.Width+5, size.Height+5);
            
            using (Bitmap bmp = new Bitmap(size.Width+15, size.Height+5))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.Clear(col);
                  
                }
               
                using (TextureBrush t = new TextureBrush(bmp))
                {
                    using (Graphics g = Graphics.FromImage(bmp2))
                    {
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                       
                        g.FillEllipse(t, 0, 0, bmp2.Width-1, bmp2.Height-1);
                        if (nSampleDone == 1)
                        {
                            Rectangle rect = new Rectangle(bmp2.Width - 18, bmp2.Height - 18, 16, 16);
                            Icon ico = new Icon(myDefinitions.LibPath + @"Pics\Miscellaneous\tick.ico");
                            g.DrawIcon(ico, rect);
                        }
                      //  g.FillEllipse(tb, 0, 0, bmp2.Width, bmp2.Height);
                    }
                }
            }

            return bmp2;
        }

        public string GetMySQLStringForMagazineSamples()
        {
            string execute_SQL_Statement_Magazine = null;
           
            execute_SQL_Statement_Magazine = @"SELECT  idactive_samples, Magazine, MagazinePos, TimestampInserted, SampleProgramType_ID, SampleID, Priority 
                                                          FROM  sample_active WHERE Magazine=" + _nMagazine_ID;


            return execute_SQL_Statement_Magazine;
        }

        public void CreateMagazineFlexgrid(C1FlexGrid flex)
        {
            MagazineDimension magDim = null;
            magDim = GetDimension();
            flex.Clear();
            // set the cols,rows of the grid to the values of the configuration
            flex.Cols.Count = magDim.DimX;
            flex.Rows.Count = magDim.DimY;

            for (int i = 0; i < magDim.DimX; i++)
            {
                flex.Cols[i].Width = (flex.Width / magDim.DimX);
            }

            for (int i = 0; i < magDim.DimY; i++)
            {
                flex.Rows[i].Height = (flex.Width / magDim.DimX) - 2;
            }

            try
            {
                ds_MagazineSamples.Clear();
                dt_MagazineSamples.Clear();
                ds_MagazineSamples = myHC.GetDataSetFromSQLCommand(GetMySQLStringForMagazineSamples());
                if (ds_MagazineSamples != null)
                {
                    dt_MagazineSamples = ds_MagazineSamples.Tables[0];
                }

            }
            catch (Exception ex) { mySave.InsertRow((int)Definition.Message.D_ALARM, ex.ToString()); }

            // zoom the picture in the cell
            flex.BackgroundImageLayout = ImageLayout.Zoom;

            if (flex.Cols[0].Width > 30)
            {
                Size size = new Size();
                size.Height = (flex.Cols[0].Width) - 30;
                size.Width = (flex.Cols[0].Width) - 30;
                int nColor = 0;
                try
                {
                    int nRow = 0;
                    int nCol = 0;
                    int nPos = 0;
                    for (nRow = 0; nRow < (magDim.DimY); nRow++)
                    {
                        // MessageBox.Show(nRow.ToString() + "#" + _magDim.DimX.ToString());
                        for (nCol = 0; nCol < (flex.Cols.Count); nCol++)
                        {
                            nPos++;
                            flex.SetData(nRow, nCol, "Pos " + nPos.ToString());
                            foreach (DataRow dr_Sample in dt_MagazineSamples.Rows)
                            {
                                if ((int)dr_Sample["MagazinePos"] == nPos)
                                {

                                    nColor = myHC.GetColorFromsampleprogramsBySampleProgramType_ID((int)dr_Sample["SampleProgramType_ID"]);
                                    Color col = Color.FromArgb(255, myColorHelper.GetCOLORFromRGBValue('R', nColor), myColorHelper.GetCOLORFromRGBValue('G', nColor), myColorHelper.GetCOLORFromRGBValue('B', nColor));

                                    //   MessageBox.Show(nRow.ToString() + "#" + nCol.ToString());

                                    flex.SetCellImage(nRow, nCol, GetCircleBitmap(size, col));
                                }
                            }
                        }
                    }

                }
                catch (Exception ex) { mySave.InsertRow((int)Definition.Message.D_ALARM, ex.ToString()); }
            }
            else
            {
                string strMagazinegrid = String.Format(myIniHandler.GetValue("Magazine", "IllegalGridWidth"), flex.Cols[0].Width.ToString());
                mySave.InsertRow((int)Definition.Message.D_ALARM, strMagazinegrid);
            }


            flex.Styles.Normal.ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.CenterTop;
            flex.Styles.Normal.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterBottom;

        }

        public int GetPreparationStep(int nSample_ID)
        {
            
           // int nRelationID = -1;
            int retPrepartionStepColor = -1;
            string SQL_Statement = null;
         //   string strRelationID = null;
            string strPrepartionStepColor = null;
           
             try{
                 SQL_Statement = "Select GetColorOFPreparationStep(" + nSample_ID + ",'Parent_ID','PreparationStepColor')";

                 strPrepartionStepColor = myHC.return_SQL_StatementAsString(SQL_Statement);
                 if (strPrepartionStepColor != null)
                 {
                    retPrepartionStepColor = Int32.Parse(strPrepartionStepColor);
                 }
             }
            catch (Exception ex) {mySave.InsertRow((int)Definition.Message.D_ALARM, ex.ToString());}
/*
            SQL_Statement = "SELECT Value FROM sample_values WHERE ActiveSample_ID=" + nidmagazine + " AND Name LIKE 'RelationshipID'";
            try
            {
                strRelationID = myHC.return_SQL_StatementAsString(SQL_Statement, "");
             
            }
            catch (Exception ex) { mySave.InsertRow((int)Definition.Message.D_ALARM, ex.ToString()); }

            SQL_Statement = "SELECT Value FROM sample_values WHERE ActiveSample_ID=" + strRelationID + " AND Name LIKE 'PreparationStep'";
            try
            {
                
                strPrepartionStep = myHC.return_SQL_StatementAsString(SQL_Statement, "");
                retPrepartionStep = Int32.Parse(strPrepartionStep);
               
            }
            catch (Exception ex) {mySave.InsertRow((int)Definition.Message.D_ALARM, ex.ToString()); }
 * */
             return retPrepartionStepColor;
        }

       

    }

    class MagazineDimension
    {
        private int X = 0;
        private int Y = 0;

        public int DimX
        {
            get
            {
                return X;
            }
            set
            {
                X = value;
            }
        }

        
        public int DimY
        {
            get
            {
                return Y;
            }
            set
            {
               Y = value;
            }
        }
    }
}
