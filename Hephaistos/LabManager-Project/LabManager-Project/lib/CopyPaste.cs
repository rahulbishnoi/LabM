using System;
using System.Data;
using MySQL_Helper_Class;


namespace CopyPaste
{

	public class MyCopyPaste
	{
        MySQL_HelperClass myHC = new MySQL_HelperClass();

        public MyCopyPaste()
        {

        }

        /// <summary>
        /// copies rows of the flex grid into a datatable 
        /// </summary>
        /// <param name="flex">the flexgrid where to copy the values from</param>
        /// <param name="dt">the data table where the data is stored</param>
        /// <param name="bCompleteGrid">true = copies the data OF THE complete grid; false = only selected rows will be copied</param>
        public int CopyFromFlexGrid(C1.Win.C1FlexGrid.C1FlexGrid flex, DataTable dt, bool bCompleteGrid)
        {

            C1.Win.C1FlexGrid.RowCollection rc = flex.Rows.Selected;

            if (bCompleteGrid)
            {
                // get all rows
                rc = flex.Rows;
            }
            else
            {
                //Get selected rows
                rc = flex.Rows.Selected;
            }

            dt.Clear();
            dt.Columns.Clear();

            for (int m = 0; m < flex.Cols.Count; m++)
            {
                dt.Columns.Add(m.ToString());
            }

            object[] ObArray = new object[flex.Cols.Count];

            foreach (C1.Win.C1FlexGrid.Row row in rc)  // all rows
            {
               
                for (int j = 1; j < flex.Cols.Count; j++) // all cols
                {
                    
                    if (flex.Cols[j].Visible)
                    {
                        if (row.Index > 0)
                        {
                             ObArray[j] = flex.GetData(row.Index, j);
                        }
                    }
                }
                if (row.Index > 0)
                {
                    dt.Rows.Add(ObArray);
                }
            }
            return dt.Rows.Count;
           
        }

        public void PasteToFlexGrid(C1.Win.C1FlexGrid.C1FlexGrid flex, DataTable dt, DataTable dt_ConditionTable, string[][] strDefaultValueArray)
        {
            int nRowDT = 0;
            int nCol = 0;
            int nOffset = flex.Rows.Count; 
            for (int i = 0 ; i < dt.Rows.Count; i++)
            {

                DataRow dr_new = dt_ConditionTable.Rows.Add(0);

                for (int d = 0; d < strDefaultValueArray.Length; d++)
                {
                    try
                    {
                        if (strDefaultValueArray[d][0].Length > 0)
                        {
                            //  dr_new.SetField("routingPositionEntry_ID", nSelectedRoutingPositionEntry_ID);
                            flex.SetData(i + nOffset, strDefaultValueArray[d][0], strDefaultValueArray[d][1]);
                        }
                    }
                    catch { }
                }

                //flex.Rows.Add(); flex.Update();
                nCol = 0;
                for (int j = 0 ; j < flex.Cols.Count; j++)
                {
                    if (flex.Cols[j].Visible)
                    {
                        
                       // flex[i + 1, j] = dt.Rows[i].ItemArray[j];
                        if (dt.Rows[nRowDT].ItemArray[j] != null)
                        {
                            try
                            {
                                if (dt.Rows[nRowDT].ItemArray[nCol++].ToString().Length > 0)
                                {
                                    int tt = -1;
                                    bool ret = Int32.TryParse(dt.Rows[nRowDT].ItemArray[j].ToString(), out tt);
                                    if (ret)
                                    {
                                      //  dr_new.SetField(i, (int)tt);
                                    }
                                    else
                                    {
                                       // dr_new.SetField(i, (string)dt.Rows[nRowDT].ItemArray[j].ToString());
                                    }
                                     flex.SetData(i + nOffset, j, (object)dt.Rows[nRowDT].ItemArray[j]);
                                }
                            }
                            catch { }
                        }
                      //  objectArray[j] = dt.Rows[i].ItemArray[j];
                    }
                    
                }
                nRowDT++;
            }
        }

/*
        public DataTable CopyToDataSet(int nRoutingPositionEntry_ID, int nSampleType_ID, int nRoutingPosition_ID, int nMachine_ID, int nCopyPasteType)
        {
                           
            DataTable dt_Copy = new DataTable();
            dt_Copy.Columns.Add("RoutingPositionEntry_ID");
            dt_Copy.Columns.Add("SampleType_ID");
            dt_Copy.Columns.Add("RoutingPosition_ID");
            dt_Copy.Columns.Add("Machine_ID");
            dt_Copy.Columns.Add("CopyPasteType");

            object[] obArray  = new Object[5];
            obArray[0] = (object)nRoutingPositionEntry_ID;
            obArray[1] = (object)nSampleType_ID;
            obArray[2] = (object)nRoutingPosition_ID;
            obArray[3] = (object)nMachine_ID;
            obArray[4] = (object)nCopyPasteType;

            dt_Copy.Rows.Add(obArray);

            return dt_Copy;
        }
*/
        public int[] CopyInfoToIntArray(int nRoutingPositionEntry_ID, int nSampleType_ID, int nRoutingPosition_ID, int nMachine_ID, int nCopyPasteType)
        {

            int[] intArray = new int[5];
            intArray[0] = nRoutingPositionEntry_ID;
            intArray[1] = nSampleType_ID;
            intArray[2] = nRoutingPosition_ID;
            intArray[3] = nMachine_ID;
            intArray[4] = nCopyPasteType;


            return intArray;
        }

    /*    public int PasteFromDataTable(DataTable dt_Copy,  int nNew_ID)
        {
            int nReturn = -1;
            int nCopyPasteType = -1;
            //int nRoutingPositionEntry_ID = -1;
            int nSampleType_ID = -1;
            int nRoutingPosition_ID = -1;
            bool bGotSampleType_ID = false;
            bool bRoutingPosition_ID = false;
          
            if (dt_Copy.Rows.Count > 0)
            {
                Int32.TryParse(dt_Copy.Rows[0].ItemArray[4].ToString(), out nCopyPasteType);

                switch (nCopyPasteType)
                {
                    case (int)Definition.CopyPasteObjectType.ROUTINGFORM_SAMPLETYPE:
                          bGotSampleType_ID = Int32.TryParse(dt_Copy.Rows[0].ItemArray[1].ToString(), out nSampleType_ID);
                          bRoutingPosition_ID = Int32.TryParse(dt_Copy.Rows[0].ItemArray[2].ToString(), out nRoutingPosition_ID);
                          if (bGotSampleType_ID && bRoutingPosition_ID)
                          {
                              CopySampleType(nRoutingPosition_ID, nSampleType_ID, nNew_ID);
                          }
                        break;

                    default:
                        Console.WriteLine(" wrong nCopyPasteType ");
                        break;
                }

            } 
           
            return nReturn;
        }
        */

        public int PasteFromInfoArray(int[] nInfoArray, int nNew_ID)
        {
            int nReturn = -1;
            int nCopyPasteType = -1;
            int nRoutingPositionEntry_ID = -1;
            int nSampleType_ID = -1;
            int nRoutingPosition_ID = -1;
            int nMachine_ID = -1;
            bool bGotSampleType_ID = false;
            bool bRoutingPosition_ID = false;
            bool bRoutingPositionEntry_ID = false;
           // bool bMachine_ID = false;

            // Info
            /*  
            intArray[0] = nRoutingPositionEntry_ID;
            intArray[1] = nSampleType_ID;
            intArray[2] = nRoutingPosition_ID;
            intArray[3] = nMachine_ID;
            intArray[4] = nCopyPasteType;
            */
          
               nCopyPasteType = nInfoArray[4];

                switch (nCopyPasteType)
                {
                  // copy conditionSet
                    case (int)Definition.CopyPasteObjectType.ROUTINGFORM_CONDITION:

                        nRoutingPositionEntry_ID = nInfoArray[0];
                        if (nRoutingPositionEntry_ID > 0) { bRoutingPositionEntry_ID = true; }

                        if (bRoutingPositionEntry_ID)
                        {
                            CopyConditionsAndCommandsByRoutingPositionEntry_ID(nRoutingPositionEntry_ID, nNew_ID);
                        }
                        break;

             
                    // copy the entries for sample types
                    case (int)Definition.CopyPasteObjectType.ROUTINGFORM_SAMPLETYPE:

                       nSampleType_ID=nInfoArray[1];
                       if (nSampleType_ID > 0) { bGotSampleType_ID = true; }

                       nRoutingPosition_ID=nInfoArray[2];
                       if (nRoutingPosition_ID > 0) { bRoutingPosition_ID = true; }

                        if (bGotSampleType_ID && bRoutingPosition_ID)
                        {
                           // CopySampleType(nRoutingPosition_ID, nSampleType_ID, nNew_ID);
                            if (bRoutingPosition_ID && bGotSampleType_ID)
                            {
                                string strSQL_Statement = @"Select idrouting_position_entries,SampleType_ID,Description from routing_position_entries WHERE Position_ID=" + nRoutingPosition_ID + " AND SampleType_ID=" + nSampleType_ID;


                                DataSet ds_CopyRoutingPositionEntries = myHC.GetDataSetFromSQLCommand(strSQL_Statement);

                                if (ds_CopyRoutingPositionEntries != null)
                                {
                                    if (ds_CopyRoutingPositionEntries.Tables.Count > 0)
                                    {
                                        if (ds_CopyRoutingPositionEntries.Tables[0].Rows.Count > 0)
                                        {
                                            foreach (DataRow dr_CopyRoutingPositionEntries in ds_CopyRoutingPositionEntries.Tables[0].Rows)
                                            {
                                                int nActualSampleType_ID = -1;
                                                Int32.TryParse(dr_CopyRoutingPositionEntries.ItemArray[1].ToString(), out nActualSampleType_ID);

                                                Int32.TryParse(dr_CopyRoutingPositionEntries.ItemArray[0].ToString(), out nRoutingPositionEntry_ID);
                                                int nNewRoutingPositionEntryID = myHC.executeInsert_SQL_StatementGetIDAsInt("INSERT INTO routing_position_entries (Position_ID,SampleType_ID,Description) VALUES(" + nRoutingPosition_ID + " ," + nNew_ID + ",'" + dr_CopyRoutingPositionEntries.ItemArray[2].ToString()+"')");
                                                //  CopySampleType(nRoutingPosition_ID, nActualSampleType_ID,nNew_ID );
                                                CopyConditionsAndCommandsByRoutingPositionEntry_ID(nRoutingPositionEntry_ID, nNewRoutingPositionEntryID);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;

                        // copy all entries for 
                    case (int)Definition.CopyPasteObjectType.ROUTINGFORM_POSITION:

                      
                       nRoutingPosition_ID=nInfoArray[2];
                       if (nRoutingPosition_ID > 0) { bRoutingPosition_ID = true; }

                       if (bRoutingPosition_ID)
                       {
                           string strSQL_Statement = @"Select idrouting_position_entries,SampleType_ID,Description from routing_position_entries WHERE Position_ID="+ nRoutingPosition_ID ;

                           DataSet ds_CopyRoutingPositionEntries = myHC.GetDataSetFromSQLCommand(strSQL_Statement);

                           if (ds_CopyRoutingPositionEntries != null)
                           {
                               if (ds_CopyRoutingPositionEntries.Tables.Count > 0)
                               {
                                   if (ds_CopyRoutingPositionEntries.Tables[0].Rows.Count > 0)
                                   {
                                       foreach (DataRow dr_CopyRoutingPositionEntries in ds_CopyRoutingPositionEntries.Tables[0].Rows)
                                       {
                                           int nActualSampleType_ID = -1;
                                           Int32.TryParse(dr_CopyRoutingPositionEntries.ItemArray[1].ToString(), out nActualSampleType_ID);

                                           Int32.TryParse(dr_CopyRoutingPositionEntries.ItemArray[0].ToString(), out nRoutingPositionEntry_ID);
                                           int nNewRoutingPositionEntryID = myHC.executeInsert_SQL_StatementGetIDAsInt("INSERT INTO routing_position_entries (Position_ID,SampleType_ID,Description) VALUES(" + nNew_ID + " ," + nActualSampleType_ID + ",'" + dr_CopyRoutingPositionEntries.ItemArray[2].ToString()+"')");
                                          //  CopySampleType(nRoutingPosition_ID, nActualSampleType_ID,nNew_ID );
                                           CopyConditionsAndCommandsByRoutingPositionEntry_ID(nRoutingPositionEntry_ID, nNewRoutingPositionEntryID);
                                       }
                                   }
                               }
                           }
                       }
                        break;

                    case (int)Definition.CopyPasteObjectType.ROUTINGFORM_STATION:

                         nMachine_ID = nInfoArray[3];
                       //if(nMachine_ID > 0){ bMachine_ID = true;}

                        break;

                    default:
                        Console.WriteLine(" wrong nCopyPasteType ");
                        break;
                }


            return nReturn;
        }

        public int CopySampleType(int nRoutingPosition_ID, int nSampleType_ID, int nNewSampleType_ID)
        {
            int nRoutingPositionEntry_ID = -1;
            int nNewRoutingPositionEntry_ID = -1;

            string strSQL_Statement = @"SELECT idrouting_position_entries FROM routing_position_entries WHERE Position_ID=" + nRoutingPosition_ID + " AND SampleType_ID=" + nSampleType_ID;
   
            DataSet ds_CopyRoutingPositionEntries = myHC.GetDataSetFromSQLCommand(strSQL_Statement);

            if (ds_CopyRoutingPositionEntries != null)
            {
                if (ds_CopyRoutingPositionEntries.Tables.Count > 0)
                {
                    if (ds_CopyRoutingPositionEntries.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr_CopyRoutingPositionEntries in ds_CopyRoutingPositionEntries.Tables[0].Rows)
                        {
                            Int32.TryParse(dr_CopyRoutingPositionEntries.ItemArray[0].ToString(), out nRoutingPositionEntry_ID);
                            nNewRoutingPositionEntry_ID = myHC.return_SQL_Statement("SELECT idrouting_position_entries FROM routing_position_entries WHERE Position_ID=" + nRoutingPosition_ID + " AND SampleType_ID=" + nNewSampleType_ID);

                            CopyConditionsAndCommandsByRoutingPositionEntry_ID(nRoutingPositionEntry_ID, nNewRoutingPositionEntry_ID);
                        }
                    }
                }
               
          
            }
            return 0;
        }


        private void CopyConditionsAndCommandsByRoutingPositionEntry_ID(int nRoutingPositionEntry_ID, int nNewRoutingPositionEntry_ID)
        {
            if (nRoutingPositionEntry_ID > 0)
            {
                string strSQL_Statement = @"CALL CopyConditionsAndCommandsByRoutingPositionEntry_ID("+nRoutingPositionEntry_ID+ "," +nNewRoutingPositionEntry_ID+")";

                myHC.executeNonQuery_SQL_Statement(strSQL_Statement);
            }

           /* if (nRoutingPositionEntry_ID > 0)
            {
               string strSQL_Statement = @"SELECT ConditionList_ID, Operation_ID,ValueName,Value,Description FROM routing_conditions WHERE RoutingPositionEntry_ID=" + nRoutingPositionEntry_ID + ";" +
                                    "SELECT Command_ID,CommandValue0,CommandValue1,CommandValue2,CommandValue3,Description FROM routing_commands WHERE RoutingPositionEntry_ID=" + nRoutingPositionEntry_ID;

                DataSet ds_CopySampleType = myHC.GetDataSetFromSQLCommand(strSQL_Statement);
                if (ds_CopySampleType != null)
                {
                    // condition table
                    if (ds_CopySampleType.Tables[0] != null)
                    {
                        if (ds_CopySampleType.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr_CopySampleType in ds_CopySampleType.Tables[0].Rows)
                            {
                                InsertRoutingConditionEntry(dr_CopySampleType, nNewRoutingPositionEntry_ID);
                            }
                        }
                    }
                    Console.WriteLine("#################### copy Commands:");


                    // command table
                    if (ds_CopySampleType.Tables[1] != null)
                    {
                        if (ds_CopySampleType.Tables[1].Rows.Count > 0)
                        {
                            foreach (DataRow dr_CopySampleType in ds_CopySampleType.Tables[1].Rows)
                            {

                                InsertRoutingCommands(dr_CopySampleType, nNewRoutingPositionEntry_ID);
                            }
                        }
                    }
                }
            }*/
        }

    /*    private void InsertRoutingConditionEntry(DataRow dr_CopySampleType, int nNewRoutingPositionEntry_ID)
        {
            String SQL_Statement = "INSERT INTO routing_conditions (RoutingPositionEntry_ID,ConditionList_ID, Operation_ID,ValueName,Value,Description) VALUES ('" + nNewRoutingPositionEntry_ID + "','" + dr_CopySampleType.ItemArray[0] + "', '" + dr_CopySampleType.ItemArray[1].ToString() + "', '" + dr_CopySampleType.ItemArray[2] + "', '" + dr_CopySampleType.ItemArray[3] + "', '" + dr_CopySampleType.ItemArray[4] + "')";
            Console.WriteLine(SQL_Statement);
            myHC.return_SQL_Statement(SQL_Statement);
        }


        private void InsertRoutingCommands(DataRow dr_CopySampleType, int nNewRoutingPositionEntry_ID)
        {
            String SQL_Statement = "INSERT INTO routing_commands (RoutingPositionEntry_ID,Command_ID,CommandValue0,CommandValue1,CommandValue2,CommandValue3,Description) VALUES ('" + nNewRoutingPositionEntry_ID + "','" + dr_CopySampleType.ItemArray[0] + "','" + dr_CopySampleType.ItemArray[1].ToString() + "', '" + dr_CopySampleType.ItemArray[2].ToString() + "', '" + dr_CopySampleType.ItemArray[3] + "', '" + dr_CopySampleType.ItemArray[4] + "', '" + dr_CopySampleType.ItemArray[5] + "')";
            Console.WriteLine(SQL_Statement );
            myHC.return_SQL_Statement(SQL_Statement);
        }
        */
	}
   
}
