using System;
using MySql.Data.MySqlClient;
using Definition;
using System.Windows.Forms;
using cs_IniHandlerDevelop;
using MySQL_Helper_Class;
using System.Data;
using System.Globalization;
using Logging;

namespace State_Helper
{

 public class StateHelper
 {
   
    Definitions myDefinitions = new Definitions();
    IniStructure inis = new IniStructure();
    Definitions myDef = new Definitions();
    MySQL_HelperClass myHC = new MySQL_HelperClass();
    Save mySave = new Save("State-Helper");

    private int nMachine_ID = -1;
    private string From_DateTime;
    private string To_DateTime;
 
    public StateHelper(int nMachine_ID, DateTime from_DT , DateTime to_DT)
    {
        this.nMachine_ID = nMachine_ID;
       
        SetTimeValues( from_DT,  to_DT);
    }

    public void SetTimeValues(DateTime from_DT, DateTime to_DT)
    {
        From_DateTime = from_DT.ToString(myDef.ThorCustomFormat);
        To_DateTime = to_DT.ToString(myDef.ThorCustomFormat);
     
    }

     public double GetStateValueByType(int nType)
     {
       
         //example: entered time range is 2012-07-07 12:00:03 -> 2012-07-07 12:00:09
         // entries found for that range : 2012-07-07 12:00:00
         // next entry is 2012-07-07 12:00:10

         int nConnectionType = myHC.GetConnectionTypeFromConnectionListByMachine_ID(nMachine_ID);
         switch (nConnectionType)
         {
                 //PLC
                case (int)Definition.ConnectionTypes.PLC:
                 double nValueWinCCChangedTable = GetStateValueFromWinCCStatisticChangedTable(nType);
                 return nValueWinCCChangedTable;

                 // all other types
                case (int)Definition.ConnectionTypes.MAGAZINE:
                case (int)Definition.ConnectionTypes.ROBOT:
                case (int)Definition.ConnectionTypes.SERIAL:
                case (int)Definition.ConnectionTypes.TCPIP:
                 {
                     double nValueMachineStateSignalsStatistic = GetStateValueFromMachineStateSignalsStatisticTable(nType);
                     return nValueMachineStateSignalsStatistic;
                 }

             default:
                 mySave.InsertRow((int)Definition.Message.D_ALARM, "no valid connection Type found - connection type:" + nConnectionType);
                 return 0;
         }
        

     }

     // for all other machines except PLCs
     private double GetStateValueFromMachineStateSignalsStatisticTable(int nType)
     {
         double dStateTime = 0;
         int nStateBit = -1;
        
         string SQL_Statement = @"SELECT bit_number,Timestamp FROM machine_state_signals_statistic WHERE 
                                Machine_ID=" + nMachine_ID + " AND signal_type='Status' AND Timestamp>='" + From_DateTime + "' AND Timestamp<='" + To_DateTime + "'  ORDER BY Timestamp";
         DataSet ds_StateValues = myHC.GetDataSetFromSQLCommand(SQL_Statement);
         if (ds_StateValues.Tables[0] != null)
         {
             if (ds_StateValues.Tables[0].Rows.Count > 0)
             {
                 // all in between the range
                 DataTable dt_StateValues = ds_StateValues.Tables[0];
                 for (int i = 0; i < (dt_StateValues.Rows.Count - 1); i++)
                 {

                     bool bGotStateValue = Int32.TryParse(dt_StateValues.Rows[i].ItemArray[0].ToString(), out nStateBit);
                     if (CheckBitNumberFromType(nType, nStateBit) && bGotStateValue)
                     {
                         DateTime dt_From;
                         DateTime dt_To;
                         DateTime.TryParse((string)dt_StateValues.Rows[i].ItemArray[1].ToString(), null, DateTimeStyles.None, out dt_From);
                         DateTime.TryParse((string)dt_StateValues.Rows[i + 1].ItemArray[1].ToString(), null, DateTimeStyles.None, out dt_To);
                         TimeSpan span;
                         span = dt_To.Subtract(dt_From);
                         dStateTime += span.TotalSeconds;

                     }
                 }
                 //    Console.Write("nType: " + nType + " - dStateTime:" + dStateTime);

                 // time before the next entry in  DB
                 int nStateBitBefore = -1;
                 string strStateValueBefore = null;
                 strStateValueBefore = myHC.return_SQL_StatementAsString(@"SELECT bit_number FROM machine_state_signals_statistic WHERE 
                 Machine_ID=" + nMachine_ID + " AND signal_type='Status' AND Timestamp<='" + From_DateTime + "'  ORDER BY Timestamp DESC LIMIT 1", "");

                 bool bGotStateValueBefore = Int32.TryParse(strStateValueBefore, out nStateBitBefore);
                 if (CheckBitNumberFromType(nType, nStateBitBefore))
                 {
                     TimeSpan span;
                     DateTime dt_From;
                     DateTime dt_To;
                     DateTime.TryParse(From_DateTime, null, DateTimeStyles.None, out dt_From);

                     DateTime.TryParse((string)dt_StateValues.Rows[0].ItemArray[1].ToString(), null, DateTimeStyles.None, out dt_To);

                     span = dt_To.Subtract(dt_From);
                     //    Console.Write(" Before:" + span.TotalSeconds);
                     dStateTime += span.TotalSeconds;
                 }

                 // time after the last entry in DB
                 bool bGotStateValueAfter = Int32.TryParse(dt_StateValues.Rows[dt_StateValues.Rows.Count - 1].ItemArray[0].ToString(), out nStateBit);
                 if (CheckBitNumberFromType(nType, nStateBit) && bGotStateValueAfter)
                 {
                     TimeSpan span;
                     DateTime dt_From;
                     DateTime dt_To;
                     DateTime.TryParse((string)dt_StateValues.Rows[dt_StateValues.Rows.Count - 1].ItemArray[1].ToString(), null, DateTimeStyles.None, out dt_From);

                     DateTime.TryParse(To_DateTime, null, DateTimeStyles.None, out dt_To);

                     span = dt_To.Subtract(dt_From);
                     //    Console.Write(" after:" + span.TotalSeconds);
                     dStateTime += span.TotalSeconds;
                 }


             }
             else // if no records found
             {
                 // time before 
                 int nStateBitBefore = -1;
                 string strStateValueBefore = null;
                 strStateValueBefore = myHC.return_SQL_StatementAsString(@"SELECT bit_number FROM machine_state_signals_statistic WHERE 
                 Machine_ID=" + nMachine_ID + " AND signal_type='Status' AND Timestamp<='" + From_DateTime + "'  ORDER BY Timestamp DESC LIMIT 1", "");

                 bool bGotStateValueBefore = Int32.TryParse(strStateValueBefore, out nStateBitBefore);
                 if (CheckBitNumberFromType(nType, nStateBitBefore))
                 {
                     TimeSpan span;
                     DateTime dt_To;
                     DateTime GotFirstTime;
                     GotFirstTime = myHC.return_SQL_StatementAsTimestamp(@"SELECT TimeStamp FROM machine_state_signals_statistic WHERE 
                     Machine_ID=" + nMachine_ID + " AND signal_type='Status' AND Timestamp<='" + From_DateTime + "'  ORDER BY Timestamp DESC LIMIT 1");
                    
                     DateTime.TryParse(To_DateTime, null, DateTimeStyles.None, out dt_To);

                     span = dt_To.Subtract(GotFirstTime);
                    
                     dStateTime = span.TotalSeconds;
                 }
             }
        
         }
         return dStateTime;
        
     }

     // for all machines that are to a PLC connected
     private double GetStateValueFromWinCCStatisticChangedTable(int nType)
     {
         double dStateTime=0;
         int nStateValue = -1;
         //Select Value,Timestamp FROM wincc_statistics_changed Where Machine_ID=9 AND Name='9_Status_0' AND Timestamp>'2012-08-01 12:01:05' AND Timestamp<'2012-08-01 16:44:09' ORDER BY Timestamp

         string SQL_Statement = @"Select Value,Timestamp FROM wincc_statistics_changed Where 
                                 Name='" + nMachine_ID + "_Status_0' AND Timestamp>='"+From_DateTime+"' AND Timestamp<='"+To_DateTime+"'  ORDER BY Timestamp";
         DataSet ds_StateValues = myHC.GetDataSetFromSQLCommand(SQL_Statement);
         if (ds_StateValues.Tables[0] != null)
         {
             if (ds_StateValues.Tables[0].Rows.Count > 0)
             {
                 // all in between the range
                 DataTable dt_StateValues = ds_StateValues.Tables[0];
                 for (int i = 0; i < (dt_StateValues.Rows.Count-1); i++)
                 {
                    
                    bool bGotStateValue = Int32.TryParse( dt_StateValues.Rows[i].ItemArray[0].ToString(),out nStateValue);
                    if (CheckStateValueFromType(nType, nStateValue) && bGotStateValue)
                    {
                        DateTime dt_From;
                        DateTime dt_To;
                        DateTime.TryParse((string)dt_StateValues.Rows[i].ItemArray[1].ToString(), null, DateTimeStyles.None, out dt_From);
                        DateTime.TryParse((string)dt_StateValues.Rows[i + 1].ItemArray[1].ToString(), null, DateTimeStyles.None, out dt_To);
                        TimeSpan span;
                        span = dt_To.Subtract(dt_From);
                        dStateTime += span.TotalSeconds;
                       
                    }
                 }
             //    Console.Write("nType: " + nType + " - dStateTime:" + dStateTime);

                 // time before the next entry in  DB
                 int nStateValueBefore = -1;
                 string strStateValueBefore = null;
                 strStateValueBefore = myHC.return_SQL_StatementAsString(@"Select Value FROM wincc_statistics_changed Where 
                             Name='" + nMachine_ID + "_Status_0' AND Timestamp<='" + From_DateTime + "'  ORDER BY Timestamp DESC  LIMIT 1", "");

                 bool bGotStateValueBefore = Int32.TryParse(strStateValueBefore, out nStateValueBefore);
                 if (CheckStateValueFromType(nType, nStateValueBefore) )
                 {
                     TimeSpan span;
                     DateTime dt_From;
                     DateTime dt_To;
                     DateTime.TryParse(From_DateTime, null, DateTimeStyles.None, out dt_From);

                     DateTime.TryParse((string)dt_StateValues.Rows[0].ItemArray[1].ToString(), null, DateTimeStyles.None, out dt_To);     
                     
                     span = dt_To.Subtract(dt_From);
                 //    Console.Write(" Before:" + span.TotalSeconds);
                     dStateTime += span.TotalSeconds;
                 }

                 // time after the last entry in DB
                 bool bGotStateValueAfter = Int32.TryParse(dt_StateValues.Rows[dt_StateValues.Rows.Count-1].ItemArray[0].ToString(), out nStateValue);
                 if (CheckStateValueFromType(nType, nStateValue) && bGotStateValueAfter)
                 {
                     TimeSpan span;
                     DateTime dt_From;
                     DateTime dt_To;
                     DateTime.TryParse((string)dt_StateValues.Rows[dt_StateValues.Rows.Count-1].ItemArray[1].ToString(), null, DateTimeStyles.None, out dt_From);

                     DateTime.TryParse(To_DateTime, null, DateTimeStyles.None, out dt_To);     
                     
                     span = dt_To.Subtract(dt_From);
                //    Console.Write(" after:" + span.TotalSeconds);
                     dStateTime += span.TotalSeconds;
                 }

               
             }
             else // if no records found
             {
                 // time before 
                 int nStateBitBefore = -1;
                 string strStateValueBefore = null;
                 strStateValueBefore = myHC.return_SQL_StatementAsString(@"Select Value FROM wincc_statistics_changed WHERE 
                             Name='" + nMachine_ID + "_Status_0' AND Timestamp<='" + From_DateTime + "'  ORDER BY Timestamp DESC  LIMIT 1", "");

             
                 bool bGotStateValueBefore = Int32.TryParse(strStateValueBefore, out nStateBitBefore);
                 if (CheckStateValueFromType(nType, nStateBitBefore))
                 {
                     TimeSpan span;
                     DateTime dt_To;
                     DateTime GotFirstTime;

                     try
                     {
                         GotFirstTime = myHC.return_SQL_StatementAsTimestamp(@"SELECT TimeStamp FROM wincc_statistics_changed WHERE 
                                              Name='" + nMachine_ID + "_Status_0' AND Timestamp<='" + From_DateTime + "'  ORDER BY Timestamp DESC LIMIT 1");

                         DateTime.TryParse(To_DateTime, null, DateTimeStyles.None, out dt_To);

                         span = dt_To.Subtract(GotFirstTime);

                         dStateTime = span.TotalSeconds;
                     }
                     catch { }
                 }
             }
           //  Console.WriteLine(" dStateTime:" + dStateTime);
         }
         return dStateTime;
     }

     private bool CheckStateValueFromType(int nType, int nStateValue)
     {
         bool ret = false;
         switch (nType)
         {
             case 0:     // Offline
                 if (nStateValue == 256) { ret = true; }
                 break;
             case 1:     // ready
                 if (nStateValue == 512) { ret = true; }
                 break;
             case 2:     // busy
                 if (nStateValue == 2048 || nStateValue == 4096) { ret = true; }
                 break;
             case 3:     // Manual mode
                 if (nStateValue == 1024 || nStateValue == 16384) { ret = true; }
                 break;
             case 4:     // Sync
                 if (nStateValue == 32768 || nStateValue == 1) { ret = true; }
                 break;
             case 5:     // Calibration
                 if (nStateValue == 8192) { ret = true; }
                 break;
             case 6:     // Breakdown
                 if (nStateValue == 8) { ret = true; }
                 break;
             case 7:     // Warning
                 if (nStateValue == 32) { ret = true; }
                 break;
             case 8:     // StopMode
                 if (nStateValue == 2) { ret = true; }
                 break;
             case 9:     // Magazine Full
                 if (nStateValue == 16) { ret = true; }
                 break;
         }
         return ret;
     }

     private bool CheckBitNumberFromType(int nType, int nBitNumber)
     {
         bool ret = false;
         switch (nType)
         {
             case 0:     // Offline
                 if (nBitNumber == 0) { ret = true; }
                 break;
             case 1:     // ready
                 if (nBitNumber == 1) { ret = true; }
                 break;
             case 2:     // busy
                 if (nBitNumber == 3 || nBitNumber == 4) { ret = true; }
                 break;
             case 3:     // Manual mode
                 if (nBitNumber == 2 || nBitNumber == 6) { ret = true; }
                 break;
             case 4:     // Sync
                 if (nBitNumber == 7 || nBitNumber == 8) { ret = true; }
                 break;
             case 5:     // Calibration
                 if (nBitNumber == 5) { ret = true; }
                 break;
             case 6:     // Breakdown
                 if (nBitNumber == 11) { ret = true; }
                 break;
             case 7:     // Warning
                 if (nBitNumber == 13) { ret = true; }
                 break;
             case 8:     // StopMode
                 if (nBitNumber == 9) { ret = true; }
                 break;
             case 9:     // Magazine Full
                 if (nBitNumber == 12) { ret = true; }
                 break;
         }
         return ret;
     }

    public int[] GetStatusValuesByType()
    {
        int nMachineList_ID = -1;
        int[] nStateListArray = null;

        nMachineList_ID = myHC.GetMachineList_IDFromMachinesByMachine_ID(nMachine_ID);
        DataSet ds_MachineStateList = myHC.GetDataSetFromSQLCommand(@"SELECT StatusWord0,StatusWord1,StatusWord2,StatusWord3,
                                                                        StatusWord4,StatusWord5,StatusWord6,StatusWord7,StatusWord8,StatusWord9 
                                                                        FROM machine_state_list WHERE MachineList_ID=" + nMachineList_ID);
         if (ds_MachineStateList != null)
        {
            if (ds_MachineStateList.Tables[0] != null)
            {
                DataTable dt_MachineStateList = ds_MachineStateList.Tables[0];
                nStateListArray = new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
                DataRow dr_MachineStateList = dt_MachineStateList.Rows[0];

                for (int l = 0; l < dt_MachineStateList.Columns.Count; l++)
                {
                    try
                    {
                        if (Int32.Parse(dr_MachineStateList.ItemArray[l].ToString()) == 1)
                        {
                            nStateListArray[l] = l;
                        }
                    }
                    catch { }
                }
            }
        }
        return nStateListArray;
    }
 }
}