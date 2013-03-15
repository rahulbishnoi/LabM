using System.Drawing;
using cs_IniHandlerDevelop;
using System;
using System.Reflection;
using System.Collections;

namespace Definition
{
 enum Message
{
    D_RECEIVE=1,
	D_SEND=2,
	D_MESSAGE=4,
	D_ALARM=8,
	D_DEBUG=16
}
 enum SQLTables
 {
     SAMPLE_ACTIVE = 1,
     SAMPLE_PROGRAM = 2,
     MACHINE_LIST = 3,
     MACHINES = 4,
     SAMPLE_STATISTIC_VALUES = 5,
     MACHINE_POSITIONS = 6,
     SAMPLE_TYPE_LIST = 7,
     MACHINE_COMMANDS = 8,
     MACHINE_PROGRAMS = 9,
     ROUTING_COMMAND_VALUES = 10,
     GLOBAL_TAGS = 11,
     MACHINE_TAGS = 12,
     ROUTING_POSITION_ENTRIES = 13
 }
 enum Status
 {
     READY = 1, 
     MANUAL = 2,
     WARNING = 4,
     BREAKDOWN = 8,
     OFFLINE = 16,
     CALIBRATION = 32,
     SETUP = 64,
     BUSY = 128
 }
 enum ThorLogWindows
 {
     ROUTING = 1,
     WARNING = 2,
     ERROR = 3,
     COMMUNICATION = 4
     
 }
 enum RoutingConditions
 {
     PRETIME = 1,
     TIME = 2,
     MACHINESAMPLEFREE = 3,
     GLOBALTAG = 4,
     MACHINETAG = 5,
     WORKSHEETENTRY = 6,
     SAMPLEONPOS = 7,
     SAMPLETYPE = 8,
     SAMPLEPRIORITY = 9,
     STATUSBITS = 10,
     CHECKOWNMAGPOS = 11

 }
 enum RoutingOperations
 {
     EQUALS = 1,
     NOTEQUALS = 2,
     GREATER = 3,
     SMALLER = 4,
     GREATEREQUALS = 5,
     SMALLEREQUALS = 6

 }
 enum RoutingCommands
 { 
     SHIFTSAMPLE = 1,
     CREATESAMPLE = 2,
     DELETESAMPLE = 3,
     CHANGESAMPLETYPE = 4,
     CHANGEPRIORITY = 5,
     WRITEGLOBALTAG = 6,
     WRITEMACHINETAG = 7,
     INSERTWSENTRY = 8,
     DELETEWSENTRY = 9,
     SENDINGCOMMANDTOMACHINE = 10,
     CREATERESERVATION = 11,
     DELETERESERVATION = 12,
     STAYACTIVE = 13
 }
 enum MachineGroups
 {
     POSITIONS = 1,
     COMMANDS = 2,
     STATESIGNALS = 3,
     PROGRAMS = 4,
     PROGRAMPARAMETERS = 5,
     PROGRAMPARAMETERNAMES= 6,
     SERVICE = 7,
     STATUSBITS = 8
    
 }

 enum Rights
 {
     USERADMINISTRATION = 1,
     CHANGEVALUES = 2,
     PROCESS = 3
 }

 enum ConnectionTypes
 {
     PLC = 1,
     ROBOT = 2,
     SERIAL = 3,
     TCPIP = 4,
     MAGAZINE = 5
 }
 
enum AlarmType
{
        AlarmType_ALARM = '1',
        AlarmType_WARNING = '2',
        AlarmType_MESSAGE = '3'
}
enum MessageType
{
    MESSAGE_ALARM = 1,
    MESSAGE_WARNING = 2,
    MESSGAE_INFO = 3
}


enum BitNumber
{
    OFFLINE = 0,
    READYAUTOMATIC = 1,
    READYMANUAL = 2,
    BUSYAUTOMATIC = 3,
    BUSYMANAUAL = 4,
    AUTOMATIC = 5,
    MANUAL = 6,
    SYNC = 7,
    SNCREQUESTED = 8,
    NOSAMPLEACCEPTED = 9,
    CALIBRATION = 10,
    BREAKDOWN = 11,
    MAGAZINEFULL = 12,
    WARNING = 13
}
enum TCPIPAnalyseClass
{
    LABMANAGER = 1,
    TGA = 2,
    LIMS = 3,
    ROBOT = 4,
    ROBOTREMOTECONTROL = 5
}
enum WSInsertLocation
{
    OWNPOS = 1,
    SAMPLEONPOS = 2,
    WRITETOPARENT = 3,
    OWNPOSHIDDEN = 4,
    SAMPLEONPOSHIDDEN = 5,
    WRITETOPARENTHIDDEN = 6
}

enum IPC_Name
{
    ROBOT_REMOTE = '1',
    ROBOT_COMMANDS = '2',
    SAMPLEINFO = '3'
   
}


enum CopyPasteObjectType
{
    ROUTINGFORM_STATION = 1,
    ROUTINGFORM_POSITION = 2,
    ROUTINGFORM_SAMPLETYPE = 3,
    ROUTINGFORM_CONDITION = 4,
    ROUTINGFORM_CONDITIONSELECTEDROWS = 5,
    ROUTINGFORM_COMMANDSELECTEDROWS = 6
}
/*enum ConfigurationTable_ID
{
    [StringValue("TCP-IP")] TCPIP = 1,
    [StringValue("Connection types")] CONNECTION_TYPE_LIST = 2,
    [StringValue("Routing error text")] ROUTING_ERROR_TEXT = 3
}
    */
    public class Definitions
    {
        IniStructure inis = new IniStructure();
        private string localPath = System.IO.Path.GetDirectoryName(new System.Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath);
        private static string localPathIniFile = @"\..\..\..\..\";
        private static string localPathIniFileName = @"INI\LabManager.ini";
	    private const int LOCAL_RECEIVE = 0x0001;
        private const int LOCAL_SEND = 0x0002;
        private const int LOCAL_MESSAGE = 0x0004;
        private const int LOCAL_ALARM = 0x0008;
        private const int LOCAL_DEBUG = 0x0010;
        private string TimeStampEditMask  = "####-##-## ##:##:##";
        private string TimeStampCustomFormat = "yyyy-MM-dd HH:mm:ss";
        private string TimeCustomFormat = "HH:mm:ss";
        private string DateCustomFormat = "yyyy-MM-dd";
        
        private string AlarmType_ALARM = "ALARM";
        private string AlarmType_WARNING = "WARNING";
        private string AlarmType_MESSAGE = "MESSAGE";
        private string AlarmType_DEBUG = "DEBUG";
        private string AlarmType_SEND = "SEND";
        private string AlarmType_RECEIVE = "RECEIVE";
        private Color[] colorArrayMachineStatistic = { Color.DarkGray, Color.Green, Color.Blue, Color.Magenta, Color.DarkOrange, Color.Goldenrod, Color.Red, Color.LightSalmon,Color.Crimson,Color.MediumOrchid };
        private string[] strMachineStatusWord = { "Offline", "Ready", "Busy", "Manual mode", "Sync", "Calibration", "Breakdown", "Warning", "Stop mode", "Magazine full" };
        private string strMachineInfo_ReportXML= @"..\..\Report.xml";
        private string strMachineInfo_ReportDataXML = @"..\..\ReportData.xml";
        private string strSampleOnPosOccupiedWord = "occupied";
        private string strSampleOnPosNOTOccupiedWord = "not occupied";
        private string strTrueWord = "true";
        private string strFalseWord = "false";
        private int nDefaultPriority = 100;
        private string strReservationPrefix = "R#";
        private string strAdminFormLimit = "Administartion_Form_Limit";
        private string strAdminFormSQLStatement = "Administration_Form_SQLStatement";
        
        public string PathIniFile
        {
            get
            {
               // string localPath = System.IO.Path.GetDirectoryName(new System.Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath);
                return localPath + localPathIniFile + localPathIniFileName;
            }
        }
        public string PathIniFileName
        {
            get
            {
                return localPathIniFileName;
            }
        }
        public string SampletypePicsPath
        {
            get
            {
                string strlangfile = null;
                strlangfile = localPath + @"\..\..\..\..\lib\Pics\SampleType\";
                return strlangfile;
            }
           
        }


        public Color MessageColor(int nMessageType)
        {
            Color col = Color.Black;
            switch(nMessageType)
            {
                case (int) MessageType.MESSAGE_ALARM:
                  col = Color.Red;
                 break;
                case (int)MessageType.MESSAGE_WARNING:
                 col = Color.DarkBlue;
                 break;
                case (int)MessageType.MESSGAE_INFO:
                 col = Color.DarkBlue;
                 break;
            }
            return col;
        }

        public string MessageIconKeyword(int nMessageType)
        {
         
            switch (nMessageType)
            {
                case (int)MessageType.MESSAGE_ALARM:
                    return "Error";
                    
                case (int)MessageType.MESSAGE_WARNING:
                    return "Warning";
                case (int)MessageType.MESSGAE_INFO:
                    return "message_transparent";     
            }
            return null; 
        }
        public string ConnectionString
        {
            get
            {
                string strConnection = null;
                inis = IniStructure.ReadIni(PathIniFile);

                if (inis != null)
                {
                    strConnection = "server= " + inis.GetValue("DB", "server") + ";User Id=" + inis.GetValue("DB", "UserId") + ";password=mysql"  + ";database=" + inis.GetValue("DB", "database") + ";Persist Security Info=no";
         
                }

                return strConnection;
            }

        }
        public string LibPath
        {
            get
            {
                string strlangfile = null;
                strlangfile = localPath + @"\..\..\..\..\lib\";
                return strlangfile;
            }

        }
        public string PicsPath
        {
            get
            {
                string strlangfile = null;
                strlangfile = localPath + @"\..\..\..\..\lib\Pics\";
                return strlangfile;
            }

        }
        public string LogPath
        {
            get
            {
                string strlangfile = null;
                strlangfile = localPath + @"\..\..\..\..\Log\";
                return strlangfile;
            }

        }
		public string LanguageFile
        {
            get
            {
              string strlangfile = null;
              inis = IniStructure.ReadIni(localPathIniFile);

                 if (inis != null)
                 {
                     string lcode = inis.GetValue("Language", "Default_Language");
                     if (lcode != null)
                     {
                         if (lcode.Length == 2)
                         {
                             strlangfile = @"\..\..\..\..\INI\localization\" + lcode + ".ini";
                         }
                         else { strlangfile = @"\..\..\..\..\INI\localization\en.ini"; }
                     }
                     else { strlangfile = @"\..\..\..\..\INI\localization\en.ini"; }
                 }
                 else { strlangfile = @"\..\..\..\..\INI\localization\en.ini"; }

                 strlangfile = localPath + strlangfile;

                return strlangfile;
            }
        }
       public int D_RECEIVE
        {
            get
            {
                return LOCAL_RECEIVE;
            }
        }
        public int D_SEND
        {
            get
            {
                return LOCAL_SEND;
            }
        }
         public int D_ALARM
        {
            get
            {
                return LOCAL_ALARM;
            }
        }
        public int D_DEBUG
        {
            get
            {
                return LOCAL_DEBUG;
            }
        }

        public string ThorCustomFormat
        {
            get
            {
                return TimeStampCustomFormat;
            }
        }
        public string ThorEditMask
        {
            get
            {
                return TimeStampEditMask;
            }
        }
        public string TimeFormat
        {
            get
            {
                return TimeCustomFormat;
            }
        }

        public string DateFormat
        {
            get
            {
                return DateCustomFormat;
            }
        }

      

        public string AlarmTYPE_ALARM
        {
            get
            {
                return AlarmType_ALARM;
            }
        }
        public string AlarmTYPE_WARNING
        {
            get
            {
                return AlarmType_WARNING;
            }
        }
        public string AlarmTYPE_MESSAGE
        {
            get
            {
                return AlarmType_MESSAGE;
            }
        }

         public string AlarmTYPE_DEBUG
        {
            get
            {
                return AlarmType_DEBUG;
            }
        }
        public string AlarmTYPE_SEND
        {
            get
            {
                return AlarmType_SEND;
            }
        }
        public string AlarmTYPE_RECEIVE
        {
            get
            {
                return AlarmType_RECEIVE;
            }
        }
       

        public Color[] ColorArrayMachineStatistic
        {
            get
            {
                return colorArrayMachineStatistic;
            }
        }

        public string[] MachineStatusWord
        {
            get
            {
                return strMachineStatusWord;
            }
        }
        
    
        public string MachineInfo_ReportXML
        {
             
            get
            {
                return strMachineInfo_ReportXML;
            }
        }

        public string MachineInfo_ReportDataXML
        {

            get
            {
                return strMachineInfo_ReportDataXML;
            }
        }

        public string SampleOnPosNOTOccupiedWord
        {
            get
            {
                return strSampleOnPosNOTOccupiedWord;
            }
        }

        public string SampleOnPosOccupiedWord
        {
            get
            {
                return strSampleOnPosOccupiedWord;
            }
        }
        public string TrueWord
        {
            get
            {
                return strTrueWord;
            }
        }
        public string FalseWord
        {
            get
            {
                return strFalseWord;
            }
        }
      public  int DefaultPriority
      {
          get
          {
              return nDefaultPriority;
          }
      }

      public string ReservationPrefix
      {
          get
          {
              return strReservationPrefix;
          }
      }

      public string AdminFormLimit
      {
          get
          {
              return strAdminFormLimit;
          }
      }

      public string AdminFormSQLStatement
      {
          get
          {
              return strAdminFormSQLStatement;
          }
      }
        
    }

    
    public class StringValueAttribute : System.Attribute
    {

        private string _value;

        public StringValueAttribute(string value)
        {
            _value = value;
        }

        public string Value
        {
        get { return _value; }
        }

    }

    #region Class StringEnum

    /// <summary>
    /// Helper class for working with 'extended' enums using <see cref="StringValueAttribute"/> attributes.
    /// </summary>
    public class StringEnum
    {
        #region Instance implementation

        private Type _enumType;
        private static Hashtable _stringValues = new Hashtable();

        /// <summary>
        /// Creates a new <see cref="StringEnum"/> instance.
        /// </summary>
        /// <param name="enumType">Enum type.</param>
        public StringEnum(Type enumType)
        {
            if (!enumType.IsEnum)
                throw new ArgumentException(String.Format("Supplied type must be an Enum.  Type was {0}", enumType.ToString()));

            _enumType = enumType;
        }

        /// <summary>
        /// Gets the string value associated with the given enum value.
        /// </summary>
        /// <param name="valueName">Name of the enum value.</param>
        /// <returns>String Value</returns>
        public string GetStringValue(string valueName)
        {
            Enum enumType;
            string stringValue = null;
            try
            {
                enumType = (Enum)Enum.Parse(_enumType, valueName);
                stringValue = GetStringValue(enumType);
            }
            catch (Exception) { }//Swallow!

            return stringValue;
        }

        /// <summary>
        /// Gets the string values associated with the enum.
        /// </summary>
        /// <returns>String value array</returns>
        public Array GetStringValues()
        {
            ArrayList values = new ArrayList();
            //Look for our string value associated with fields in this enum
            foreach (FieldInfo fi in _enumType.GetFields())
            {
                //Check for our custom attribute
                StringValueAttribute[] attrs = fi.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];
                if (attrs.Length > 0)
                    values.Add(attrs[0].Value);

            }

            return values.ToArray();
        }

        /// <summary>
        /// Gets the values as a 'bindable' list datasource.
        /// </summary>
        /// <returns>IList for data binding</returns>
        public IList GetListValues()
        {
            Type underlyingType = Enum.GetUnderlyingType(_enumType);
            ArrayList values = new ArrayList();
            //Look for our string value associated with fields in this enum
            foreach (FieldInfo fi in _enumType.GetFields())
            {
                //Check for our custom attribute
                StringValueAttribute[] attrs = fi.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];
                if (attrs.Length > 0)
                    values.Add(new DictionaryEntry(Convert.ChangeType(Enum.Parse(_enumType, fi.Name), underlyingType), attrs[0].Value));

            }

            return values;

        }

        /// <summary>
        /// Return the existence of the given string value within the enum.
        /// </summary>
        /// <param name="stringValue">String value.</param>
        /// <returns>Existence of the string value</returns>
        public bool IsStringDefined(string stringValue)
        {
            return Parse(_enumType, stringValue) != null;
        }

        /// <summary>
        /// Return the existence of the given string value within the enum.
        /// </summary>
        /// <param name="stringValue">String value.</param>
        /// <param name="ignoreCase">Denotes whether to conduct a case-insensitive match on the supplied string value</param>
        /// <returns>Existence of the string value</returns>
        public bool IsStringDefined(string stringValue, bool ignoreCase)
        {
            return Parse(_enumType, stringValue, ignoreCase) != null;
        }

        /// <summary>
        /// Gets the underlying enum type for this instance.
        /// </summary>
        /// <value></value>
        public Type EnumType
        {
            get { return _enumType; }
        }

        #endregion

        #region Static implementation

        /// <summary>
        /// Gets a string value for a particular enum value.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <returns>String Value associated via a <see cref="StringValueAttribute"/> attribute, or null if not found.</returns>
        public static string GetStringValue(Enum value)
        {
            string output = null;
            Type type = value.GetType();

            if (_stringValues.ContainsKey(value))
                output = (_stringValues[value] as StringValueAttribute).Value;
            else
            {
                //Look for our 'StringValueAttribute' in the field's custom attributes
                FieldInfo fi = type.GetField(value.ToString());
                StringValueAttribute[] attrs = fi.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];
                if (attrs.Length > 0)
                {
                    _stringValues.Add(value, attrs[0]);
                    output = attrs[0].Value;
                }

            }
            return output;

        }

        /// <summary>
        /// Parses the supplied enum and string value to find an associated enum value (case sensitive).
        /// </summary>
        /// <param name="type">Type.</param>
        /// <param name="stringValue">String value.</param>
        /// <returns>Enum value associated with the string value, or null if not found.</returns>
        public static object Parse(Type type, string stringValue)
        {
            return Parse(type, stringValue, false);
        }

        /// <summary>
        /// Parses the supplied enum and string value to find an associated enum value.
        /// </summary>
        /// <param name="type">Type.</param>
        /// <param name="stringValue">String value.</param>
        /// <param name="ignoreCase">Denotes whether to conduct a case-insensitive match on the supplied string value</param>
        /// <returns>Enum value associated with the string value, or null if not found.</returns>
        public static object Parse(Type type, string stringValue, bool ignoreCase)
        {
            object output = null;
            string enumStringValue = null;

            if (!type.IsEnum)
                throw new ArgumentException(String.Format("Supplied type must be an Enum.  Type was {0}", type.ToString()));

            //Look for our string value associated with fields in this enum
            foreach (FieldInfo fi in type.GetFields())
            {
                //Check for our custom attribute
                StringValueAttribute[] attrs = fi.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];
                if (attrs.Length > 0)
                    enumStringValue = attrs[0].Value;

                //Check for equality then select actual enum value.
                if (string.Compare(enumStringValue, stringValue, ignoreCase) == 0)
                {
                    output = Enum.Parse(type, fi.Name);
                    break;
                }
            }

            return output;
        }

        /// <summary>
        /// Return the existence of the given string value within the enum.
        /// </summary>
        /// <param name="stringValue">String value.</param>
        /// <param name="enumType">Type of enum</param>
        /// <returns>Existence of the string value</returns>
        public static bool IsStringDefined(Type enumType, string stringValue)
        {
            return Parse(enumType, stringValue) != null;
        }

        /// <summary>
        /// Return the existence of the given string value within the enum.
        /// </summary>
        /// <param name="stringValue">String value.</param>
        /// <param name="enumType">Type of enum</param>
        /// <param name="ignoreCase">Denotes whether to conduct a case-insensitive match on the supplied string value</param>
        /// <returns>Existence of the string value</returns>
        public static bool IsStringDefined(Type enumType, string stringValue, bool ignoreCase)
        {
            return Parse(enumType, stringValue, ignoreCase) != null;
        }

        #endregion
    }

    #endregion
}