using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySQL_Helper_Class;
using Definition;
using System.Windows.Forms;

namespace LabManager
{
    class TranslateString
    {
        MySQL_HelperClass myHC = new MySQL_HelperClass();
        Definitions myDefinitions = new Definitions();

         public string GetFirstEntry(string strAnalyse,string strCharacter) {

             int nPos=strAnalyse.IndexOf(strCharacter);
             return strAnalyse.Substring(0, nPos); ;
        }

        public string TranslateValueString(string strValue, int nSample_ID)
        {
            string replaceString = null;
            string replaceStringWith = null;
            replaceString = strValue;

            string stringToTranslate = null;

            // Loop through all instances of string.
            int nStart = 0;
            int nEnd = 0;
           // while ((nStart = replaceString.IndexOf('<', nStart)) != -1)
            try
            {
                if (replaceString.Length > 0)
                {
                    while ((nStart = replaceString.IndexOf('<', nStart)) != -1)
                    {
                        // find the end
                        nEnd = replaceString.IndexOf('>', nStart);

                        // for example '<GT Variable1 1 3>' 
                        // means: from table 'global_tags' get the value of Variable1 and from character 1 get the next 3 characters ('est' from 'Test')
                        stringToTranslate = replaceString.Substring(nStart, (nEnd - nStart + 1));
                        replaceStringWith = TranslateCheck(stringToTranslate, nSample_ID);
                        replaceString = replaceString.Replace(stringToTranslate, replaceStringWith);

                        // Increment the index
                        nStart++; nEnd++;
                    }
                }
            }
            catch { }
            return replaceString;
        }

        private string TranslateCheck(string stringToTranslate, int nSample_ID)
        {
            string replaceStringWith = null;
            string strKey = null;
            string strValue = null;
            string strFormat1 = null;
            string strFormat2 = null;
            int nFormat1 = -1;
            int nFormat2 = -1;
            string[] Splits = null;

            Splits = stringToTranslate.Split(new char[] { ' ' });
            strKey = Splits[0];

            if (Splits.Length > 2) { strFormat1 = Splits[2]; if (strFormat1.EndsWith(">")) { strFormat1 = strFormat1.Substring(0, strFormat1.Length - 1); } }
            if (Splits.Length > 3) { strFormat2 = Splits[3]; if (strFormat2.EndsWith(">")) { strFormat2 = strFormat2.Substring(0, strFormat2.Length - 1); } }
            if (Splits.Length > 1)
            {
                strValue = Splits[1];
                if (strKey.StartsWith("<")) { strKey = strKey.Substring(1, strKey.Length - 1); }
                if (strValue.EndsWith(">")) { strValue = strValue.Substring(0, strValue.Length - 1); }
            }
            else
            {
                if (strKey.StartsWith("<")) { strKey = strKey.Substring(1, strKey.Length - 1); }
                if (strKey.EndsWith(">")) { strKey = strKey.Substring(0, strKey.Length - 1); }
            }
            
            switch (strKey)
            {
                case "GT": //table global_tags
                    string SQL_Statement = "SELECT Value FROM global_tags WHERE Name='" + strValue + "'";
                    replaceStringWith = myHC.return_SQL_StatementAsString(SQL_Statement);
                    break;
                case "MT": //table machine_tags
                    SQL_Statement = "SELECT Value FROM machine_tags WHERE Name='" + strValue + "'";
                    replaceStringWith = myHC.return_SQL_StatementAsString(SQL_Statement);
                    break;
                case "WS": //worksheet entry
                    if (String.Compare(strValue, "SampleID", true)==0)
                    { SQL_Statement = "SELECT SampleID FROM sample_active WHERE idactive_samples=" + nSample_ID;
                        replaceStringWith = myHC.GetNameFromID((int)Definition.SQLTables.SAMPLE_ACTIVE, nSample_ID);
                    }
                    else if (String.Compare(strValue, "Priority", true)==0)
                    { SQL_Statement = "SELECT Priority FROM sample_active WHERE idactive_samples=" + nSample_ID; }
                    else
                    {
                        SQL_Statement = "SELECT Value FROM sample_values WHERE Name='" + strValue + "' AND ActiveSample_ID=" + nSample_ID;
                        replaceStringWith = myHC.return_SQL_StatementAsString(SQL_Statement);
                    }
                    break;
                case "TIMESTAMP": //TIMESTAMP -> replace with the actual time and date
                    DateTime now = DateTime.Now;
                    string strNow = now.ToString(myDefinitions.ThorCustomFormat);
                    replaceStringWith = strNow;
                    break;
                default: // if wrong key is send put in the original string
                    replaceStringWith = stringToTranslate;
                    break;
            }

            // if format numbers ar given, copy only ther substrings
            try
            {
                bool bFormat1 = Int32.TryParse(strFormat1, out nFormat1);
                bool bFormat2 = Int32.TryParse(strFormat2, out nFormat2);

                 if (bFormat1 && bFormat2 && Splits.Length > 3)
                {
                    if (nFormat2 > replaceStringWith.Length) { nFormat2 = replaceStringWith.Length - nFormat1; }
                    replaceStringWith = replaceStringWith.Substring(nFormat1, nFormat2);
                }
                else if (bFormat1 & !bFormat2)
                {
                    if (nFormat1 > replaceStringWith.Length) { nFormat1 = replaceStringWith.Length; }
                    replaceStringWith = replaceStringWith.Substring(0, nFormat1);
                }

            }
            catch { }

            return replaceStringWith;
        }

        public string TranslateSampleRegistrationString(string strSampleID)
        {
            string replacedString="";
            DateTime now = DateTime.Now;

            strSampleID.Trim();
            replacedString = strSampleID.Replace("%s", String.Format("{0,2:00}", now.Second));
            replacedString = replacedString.Replace("%m", String.Format("{0,2:00}", now.Minute));
            replacedString = replacedString.Replace("%h", String.Format("{0,2:00}", now.Hour));
            replacedString = replacedString.Replace("%D", String.Format("{0,2:00}", now.Day));
            replacedString = replacedString.Replace("%M", String.Format("{0,2:00}", now.Month));
            replacedString = replacedString.Replace("%Y", now.Year.ToString());
            return replacedString;
        
        }
    }
}
