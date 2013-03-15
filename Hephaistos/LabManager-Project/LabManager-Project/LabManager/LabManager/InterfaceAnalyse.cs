using System;
using System.Data;
using Logging;
using Definition;
using MySQL_Helper_Class;

namespace LabManager
{
    class InterfaceAnalyse
    {
        Save mySave = new Save("LabManager-InterfaceAnalyse");
        Definitions myThorDef = new Definitions();
        MySQL_HelperClass myHC = new MySQL_HelperClass();
        int _nSelectedSampleProgram_ID = -1;
        public InterfaceAnalyse()
        {

        }

        public string MessageAnalyse(string strMessage)
        {
            
            string strRet = null;
            switch (strMessage)
            {

                case "DISCONNECT@":
                    strRet = "DISCONNECT@";
                    break;
                case "ARE_YOU_THERE@":
                    strRet = "I_AM_HERE@";
                    break;

            }
           
            string searchForThis = "INSERT_SAMPLE";
            int nFoundAt = strMessage.IndexOf(searchForThis);
            if (nFoundAt >= 0)
            {
                strRet = CheckCommandForSampleInsert(strMessage.Substring(nFoundAt + searchForThis.Length + 1));
            }

            searchForThis = "GET_SAMPLE_PROGRAM_LIST";
            if (strMessage.StartsWith(searchForThis))
            {
                strRet = GetSampleProgramList();
            }

            return strRet;
        }

        public string CheckCommandForSampleInsert(string Command)
        {
            string strRet = null;

            string strCommand = null;
            string strValue = null;
            string strSample_ID = null;
            int nStartLocation = -1;
            int nSampleProgram_ID = -1;
            int nPriority = (int)myThorDef.DefaultPriority;
            bool bGotProgram = false;

            string[] strNMessages = Command.Split(new Char[] { '@' });


            foreach (string strMessage in strNMessages)
            {

                string[] strCommandAndValue = strMessage.Split(new Char[] { '=' });
                    if (strCommandAndValue.Length == 2)
                    {
                        strCommand = strCommandAndValue[0];
                        strValue = strCommandAndValue[1];
                        if (mySave.DEBUG_MODE) { mySave.InsertRow((int)Definition.Message.D_DEBUG, "Command=" + Command + " strCommand=" + strCommand + "  - value=" + strValue); }

                        switch (strCommand)
                        {
                            case "SI":
                                strSample_ID = strValue;
                                break;

                            case "LO":
                                Int32.TryParse(strValue, out nStartLocation);
                                break;
                            
                            case "PRIO":
                                Int32.TryParse(strValue, out nPriority);
                                break;

                            case "PR":
                                bGotProgram = Int32.TryParse(strValue, out nSampleProgram_ID);
                                break;

                            default:
                                mySave.InsertRow((int)Definition.Message.D_ALARM, "unknown keyword found");
                                strRet = "SAMPLE_NOT_INSERTED@NAME=" + strSample_ID +"@MESSAGE=unknown keyword found@";
                                break;
                        }
                      
                    }
                    else
                    {
                        if (strMessage.Length > 0)
                        {
                            mySave.InsertRow((int)Definition.Message.D_ALARM, "no or to many '=' found for command 'INSERT_SAMPLE' :" + strNMessages);
                        }
                        strRet = "SAMPLE_NOT_INSERTED@NAME=" + strSample_ID +"@MESSAGE=no or to many '=' found for command@";
                    }
                    
            }
             TranslateString strTranslate = new TranslateString();
             strSample_ID = strTranslate.TranslateSampleRegistrationString(strSample_ID);

            if (mySave.DEBUG_MODE) mySave.InsertRow((int)Definition.Message.D_DEBUG, "sample values: strSample_ID=" + strSample_ID + " nStartLocation=" + nStartLocation + " nPriority=" + nPriority + " Programm_ID=" + nSampleProgram_ID);

            int retID = (InsertSampleByInterface(strSample_ID, nStartLocation, nPriority, nSampleProgram_ID));
            if (retID == -1)
            {
                strRet = "SAMPLE_NOT_INSERTED@NAME=" + strSample_ID + "@MESSAGE=duplicate entry? - see log info on LabManager@";
            }
            else
            {
                strRet = "SAMPLE_INSERTED_SUCCESSFULLY@NAME=" + strSample_ID + "@ID=" + retID + "@PR=" + _nSelectedSampleProgram_ID + "@LO=" + nStartLocation + "@PRIO=" + nPriority;
            }
                return strRet;
        }

        private int InsertSampleByInterface(string strSampleString, int nStartLocation, int nPriority, int nSampleProgram_ID)
        {
            int ret = -1;
            
            if (nSampleProgram_ID == -1)
            {
                nSampleProgram_ID = GetSampleProgram_IDByAutoDetection(strSampleString);  
               // if (nSampleProgram_ID == -1){
                    // mySave.InsertRow((int)Definition.Message.D_ALARM, "no SampleProgram_ID found in detection pool");
              //  }
            }
            else if (strSampleString.Length < 3)
            {
                mySave.InsertRow((int)Definition.Message.D_ALARM, "sampleID not valid or to short");
            }
            else if (nStartLocation == -1)
            {
                mySave.InsertRow((int)Definition.Message.D_ALARM, "no start location found");
            }
                
            if(strSampleString.Length>2 && nStartLocation > 0 && nSampleProgram_ID > 0)
            {
                _nSelectedSampleProgram_ID = nSampleProgram_ID;
                string SQL_Statement = @"INSERT INTO  sample_active
                    (SampleProgramType_ID, SampleID, ActualSamplePosition_ID, Priority, StartPosition_ID)
                    Values (" + nSampleProgram_ID + ",'" + strSampleString + "',"+nStartLocation+"," + nPriority + ","+ nStartLocation+")";

                int nSample_ID = myHC.executeInsert_SQL_StatementGetIDAsInt(SQL_Statement);

                if (nSample_ID > 0) { mySave.InsertRow((int)Definition.Message.D_MESSAGE, "sample " + strSampleString + " successfully inserted with id:" + nSample_ID);
                    ret = nSample_ID;
                }
            }
           
            return ret;
        }

        private int GetSampleProgram_IDByAutoDetection(string strSampleID)
        {
            int nProgram_ID = -1;
            try
            {
                AutoDetection autodec = new AutoDetection();
                nProgram_ID = autodec.GetSampleProgram_ID(strSampleID);
            }
            catch (Exception ex) { mySave.InsertRow((int)Definition.Message.D_ALARM, " Error SampleProgram auto detection: \r\n" + ex.ToString()); }
            return nProgram_ID;
        }

        private string GetSampleProgramList()
        {
            string strRet = null;

            string SQL_Statement = "Select Name,idsample_programs FROM sample_programs ";
            DataSet ds_SamplePrograms = new DataSet();
            ds_SamplePrograms.Clear();

            ds_SamplePrograms = myHC.GetDataSetFromSQLCommand(SQL_Statement);
            DataTable dt_SamplePrograms = null;
            if (ds_SamplePrograms.Tables[0] != null)
            {
                strRet = "SAMPLE_PROGRAM_LIST@";
                dt_SamplePrograms = ds_SamplePrograms.Tables[0];
                foreach (DataRow dr_SampleProgram in dt_SamplePrograms.Rows)
                {
                    strRet = strRet + dr_SampleProgram["Name"] + ";" + dr_SampleProgram["idsample_programs"] + "@";
                }
            }
            return strRet;
        }

    }


}
