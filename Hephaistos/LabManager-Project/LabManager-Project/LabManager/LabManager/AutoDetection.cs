using System.Data;
using Logging;
using MySQL_Helper_Class;

namespace LabManager
{
    class AutoDetection
    {
        Save mySave = new Save("LabManager-AutoDetection");
        MySQL_HelperClass myHC = new MySQL_HelperClass();

        /// <summary>
        /// this method checks if the pattern in the detection string matches to the sampleID string 
        /// '?' is a pattern that allows every character
        /// '(a-z)' OR '(1-9)' is a range because of the '-' character
        /// '(1,2,5,6,7)' is a OR option, so character at that place could be a 1 OR 2 OR 5 OR 6 OR 7
        /// a combination of range and 'OR' option are possible like '(a-f,4,6,8)'
        /// </summary>
        /// <param name="strDetection">the string of detection for example 'A(1-5)????(a-f,h,o)'</param>
        /// <param name="strSampleID">the sampleID string for example 'A1_123b' (this will match to the detection pattern above)</param>
        /// <returns>returns 'true' in case of matching sampleID with the detection pattern, otherwise 'false'</returns>
        private bool CheckDetection(string strDetection, string strSampleID)
        {
            char[][] cDetectionArray = null;
            char[] cSampleIDArray = strSampleID.ToCharArray();
            int nStart = 0;
            int nEnd = 0;
            bool bMatch = false;
            int nCountCorrelation = 0;
            string Logstr = null;

            cDetectionArray = new char[strDetection.Length][];

            int nNextChar = 0;
            int nValuesRangeOK = 0;

            for (int n = 0; n < strDetection.Length; n++)
            {
                //|| strDetection[n] != ')'
                if (strDetection[n] == '(')
                {
                    nStart = strDetection.IndexOf('(', nStart);
                    nEnd = strDetection.IndexOf(')', nStart);
                    if (nStart > 0 && nEnd < 0)
                    {
                        mySave.InsertRow((int)Definition.Message.D_ALARM,"found '(' but no ')'");
                        return false;
                    }
                    else
                    {
                        string subString = strDetection.Substring(nStart + 1, (nEnd - nStart - 1));

                        int nSearchIndexKomma = subString.IndexOf(',');
                        int nSearchIndexRange = subString.IndexOf('-');

                        if (nSearchIndexRange > 0 || nSearchIndexKomma > 0)
                        {
                            string[] strRanges = subString.Split(',');
                            int nStartCharacter = -1;
                            int nEndCharacter = -1;
                            int nRangeCount = 0;
                            int nTotalCount = 0;
                            nValuesRangeOK = 0;
                            cDetectionArray[nNextChar] = new char[256];

                            foreach (string sub in strRanges)
                            {
                                string[] strRange = sub.Split('-');

                                if (strRange[0] != null)
                                {
                                    if (strRange[0].Length != 1) { nValuesRangeOK++; }

                                    if (nValuesRangeOK > 0)
                                    {
                                        mySave.InsertRow((int)Definition.Message.D_ALARM, "Values must be only one character each - e.g. 'a-b' NOT 'ab-cd'");
                                        return false;
                                    }
                                    else
                                    {
                                        nStartCharacter = CharToInt(strRange[0][0]);
                                        try
                                        {
                                            if (strRange.Length > 1)
                                            { nEndCharacter = CharToInt(strRange[1][0]); }
                                            else { nEndCharacter = CharToInt(strRange[0][0]); }
                                        }
                                        catch { nEndCharacter = CharToInt(strRange[0][0]); }

                                        if (nEndCharacter < nStartCharacter) { int dummy = nEndCharacter; nEndCharacter = nStartCharacter; nStartCharacter = dummy; }

                                        nRangeCount = nEndCharacter - nStartCharacter;

                                        for (int o = 0; o <= nRangeCount; o++)
                                        {
                                            cDetectionArray[nNextChar][nTotalCount] = (char)(o + nStartCharacter);

                                            // debug info
                                            if (cSampleIDArray.Length > nNextChar)
                                            {
                                                //string logout = string.Format("cDetectionArray[{1}][{2}]: {3} cSampleIDArray[{4}] {5}", subString, nNextChar, nTotalCount, cDetectionArray[nNextChar][nTotalCount], nNextChar, cSampleIDArray[nNextChar]);
                                                //mySave.InsertRow((int)Definition.Message.D_ALARM, logout);
                                            }
                                            nTotalCount++;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            mySave.InsertRow((int)Definition.Message.D_ALARM, "no '-' or ',' found, but found a '(', so there must be at least one ',' or '-'");
                            return false;
                        }
                    }
                    nStart++; nEnd++;
                    nNextChar++;
                    n = nEnd - 1;
                }
                else
                {
                    if (cSampleIDArray.Length < (nNextChar + 1))
                    {
                        mySave.InsertRow((int)Definition.Message.D_ALARM, "Sample_ID to short for Detection (Sample_ID size:" + cSampleIDArray.Length + " DetectionString length:" + (nNextChar + 1) + ")");
                        return false;
                    }
                    else
                    {
                        cDetectionArray[nNextChar] = new char[1];
                        cDetectionArray[nNextChar][0] = strDetection[n];
                      //  string logout = string.Format("cDetectionArray[{0}][0] :{1} cSampleIDArray[{2}] {3}", nNextChar, cDetectionArray[nNextChar][0], nNextChar, cSampleIDArray[nNextChar]);
                      //  mySave.InsertRow((int)Definition.Message.D_ALARM, logout);
                        nNextChar++;
                    }
                }
            }

            if (nValuesRangeOK == 0)
            {
                int nOrOption = 0;
                if (nNextChar <= strSampleID.Length)
                {
                    // search for correlations 
                    // to do so, look at every character of the sampleID string and compare it to the detection string
                    for (int k = 0; k < nNextChar; k++) //if detection string is 'A(1-4)???' it checks 5 characters of the sampleID
                    {
                        nOrOption = 0;
                        // in the second dimension of the character array are the OR options
                        // look if at least one of the OR option matches
                        for (int m = 0; m < cDetectionArray[k].Length; m++) //if detection string is 'A(1-4)???' it checks if second character is 1 oR 2  OR 3 OR 4
                        {
                            if (cSampleIDArray[k] == cDetectionArray[k][m] || cDetectionArray[k][0] == '?') { nOrOption = 1; }
                        }
                        nCountCorrelation = nCountCorrelation + nOrOption;
                    }

                    // if found correlations are equals to the size of the detection string return 'true'
                    if (nCountCorrelation >= nNextChar) { bMatch = true; }

                    if (bMatch)
                    {
                        Logstr = string.Format("{0}  match to  {1} nCountCorrelation {2}", strDetection, strSampleID, nCountCorrelation);
                        mySave.InsertRow((int)Definition.Message.D_MESSAGE, Logstr);
                        return true;
                    }
                    else
                    {
                        Logstr = string.Format("{0}  do not match to  {1}  nCountCorrelation {2}", strDetection, strSampleID, nCountCorrelation);
                        mySave.InsertRow((int)Definition.Message.D_MESSAGE, Logstr);
                    }

                }
                else
                {
                    Logstr = string.Format("{0}  do not match to  {1}  - SampleID to short for Detection! nNextChar {2} strSampleID.Length{3}", strDetection, strSampleID, nNextChar, strSampleID.Length);
                    mySave.InsertRow((int)Definition.Message.D_ALARM, Logstr);
                    return false;
                }

            }
            else
            {
                mySave.InsertRow((int)Definition.Message.D_ALARM, "syntax error in '" + strDetection + "'");
                return false;
            }
            return bMatch;
        }

        private int CharToInt(char c)
        {
            return (int)(c - '0') + 48;
        }

        public int GetSampleProgram_ID(string strSample_ID)
        {
            int nProgram_ID = -1;
            int nCountDetections = 0;
            string SQL_Statement = "Select Detection,Name,idsample_programs FROM sample_programs ";
            DataSet ds_SamplePrograms = new DataSet();
            ds_SamplePrograms.Clear();
           
            ds_SamplePrograms = myHC.GetDataSetFromSQLCommand(SQL_Statement);
            DataTable dt_SamplePrograms = null;
            if (ds_SamplePrograms.Tables[0] != null)
            {
                dt_SamplePrograms = ds_SamplePrograms.Tables[0];
                foreach (DataRow dr_SampleProgram in dt_SamplePrograms.Rows)
                {
                    if (CheckDetection(dr_SampleProgram["Detection"].ToString(), strSample_ID))
                    {
                        nProgram_ID = (int)dr_SampleProgram["idsample_programs"];
                        nCountDetections++;
                        mySave.InsertRow((int)Definition.Message.D_MESSAGE, strSample_ID + " match to " + dr_SampleProgram["Detection"].ToString() + "   --> Program " + dr_SampleProgram["Name"].ToString() + "");
                    }
                }
            }
            if(nProgram_ID == -1)
            {
                nProgram_ID = GetDefaultProgram();
                mySave.InsertRow((int)Definition.Message.D_ALARM,  @"no match for " + strSample_ID + " found in program detection pool, got "
                 + myHC.GetNameFromID((int)Definition.SQLTables.SAMPLE_PROGRAM,nProgram_ID) + " (id:"  + nProgram_ID + ") as default");

            }
          //  MessageBox.Show(nProgram_ID.ToString());
            if (nCountDetections > 1) { mySave.InsertRow((int)Definition.Message.D_ALARM, "found more than one(" + nCountDetections + ") matches to " + strSample_ID + " in programm detection pool"); }
            return nProgram_ID;
        }

        private int GetDefaultProgram()
        {
            int nProgramID = -1;
            string SQL_Statement = "Select idsample_programs FROM sample_programs WHERE DefaultProgram=1";
            nProgramID = myHC.return_SQL_Statement(SQL_Statement);
            return nProgramID;
        }

    }
}
