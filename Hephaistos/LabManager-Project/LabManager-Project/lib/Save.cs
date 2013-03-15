using System;
using MySql.Data.MySqlClient;
using Definition;
using cs_IniHandlerDevelop;
namespace Logging
{

 public class Save
 {
    private string strCall="";

    Definitions myDefinitions = new Definitions();
    IniStructure inis = new IniStructure();
    string myConnectionString = null;
    bool DEBUGMODE = false;
    bool DEBUG = false;
    bool MESSAGE = true;
    bool ALARM = true;
    bool RECEIVE = true;
    bool SEND = true;
    bool bINIReaderError = false;
    MySqlConnection myConnection = null;
    MySqlCommand myCommand = null;
    MySqlConnection myConnectionLogBook = null;
    MySqlCommand myCommandLogBook = null;

    public Save(string strCall)
    {
        this.strCall = strCall;
        if (this.myConnectionString == null)
        {
            this.myConnectionString = GetConnectionString();
        }
        myConnection = new MySqlConnection(myConnectionString);
    }

    ~Save()
    {
        if (myConnection != null)
        {
            if ((myConnection.State) == global::System.Data.ConnectionState.Open)
            {
                myConnection.Close();
            }
        }
    }

    public string GetConnectionString()
    {
        string conString = "";
        string IniFilePath = myDefinitions.PathIniFile;
        inis = IniStructure.ReadIni(IniFilePath);

        if (inis == null)
        {
            if (!bINIReaderError)
            {
                //   MessageBox.Show("Something went wrong with the ini-Reader path!", "error");
                InsertRow((int)Definition.Message.D_ALARM, "Something went wrong with the ini-Reader path!");
                bINIReaderError = true;
            }
        }
        else
        {
            bINIReaderError = false;
        }

       // conString = inis.GetValue("DB", "ConnectionString");
        conString = "server= " + inis.GetValue("DB", "server") + ";User Id=" + inis.GetValue("DB", "UserId") + ";password=" + inis.GetValue("DB", "password") + ";database=" + inis.GetValue("DB", "database") + ";Persist Security Info=no"; 
          
        try
        {
            string debug = inis.GetValue("Logging", "DebugMode");
            if (String.Equals(debug, "true", StringComparison.Ordinal)) { DEBUGMODE = true; }
        }
        catch { }
        try
        {
            string debug = inis.GetValue("Logging", "Debug");
            if (String.Equals(debug, "true", StringComparison.Ordinal)) { DEBUG = true; }
        }
        catch { }
        try
        {
            string debug = inis.GetValue("Logging", "Message");
            if (String.Equals(debug, "true", StringComparison.Ordinal)) { MESSAGE = true; }
        }
        catch { }
        try
        {
            string debug = inis.GetValue("Logging", "Alarm");
            if (String.Equals(debug, "true", StringComparison.Ordinal)) { ALARM = true; }
        }
        catch { }
        try
        {
            string debug = inis.GetValue("Logging", "Receive");
            if (String.Equals(debug, "true", StringComparison.Ordinal)) { RECEIVE = true; }
        }
        catch { }
        try
        {
            string debug = inis.GetValue("Logging", "Send");
            if (String.Equals(debug, "true", StringComparison.Ordinal)) { SEND = true; }
        }
        catch { }
        return conString;
    }

    public bool DEBUG_MODE
    {
        get
        {
            return DEBUGMODE;
        }
    }

    private bool MessageTypeFilter(int nType)
    {
        switch (nType)
        {
            case (int)Definition.Message.D_ALARM:
                if (ALARM) { return true; } else { return false; }
                
            case (int)Definition.Message.D_MESSAGE:
                if (MESSAGE) { return true; } else { return false; }
                
            case (int)Definition.Message.D_DEBUG:
                if (DEBUG) { return true; } else { return false; }
               
            case (int)Definition.Message.D_SEND:
                if (SEND) { return true; } else { return false; }
                
            case (int)Definition.Message.D_RECEIVE:
                if (RECEIVE) { return true; } else { return false; }
                
            default:
                return false; 
        }
    }

    public void InsertRow(int nType,string strMessage) 
    {
      DateTime time = DateTime.Now;
      string strtime = time.ToString(myDefinitions.ThorCustomFormat);
        try
         {
             if (MessageTypeFilter(nType)) // check if Type should be written - set in LabManager.ini
             {
                 if (strMessage.IndexOf("'") != -1)
                 {
                     strMessage = strMessage.Replace("'", " ");
                 }
                 if (strMessage.Length > 65535) { Console.WriteLine("Save-Message to long: " + strMessage); }
                 else
                 {
                   
                     if ((myConnection.State & global::System.Data.ConnectionState.Open) != global::System.Data.ConnectionState.Open)
                         {   
                             myConnection.Open();
                         }
                     if (myConnection != null)
                     {
                         string myInsertQuery = "INSERT INTO logging_data (Timestamp,Type_Name, Location, Message) Values('" + strtime + "','" + GETTypeString(nType) + "','" + strCall + "','" + strMessage + "')";
                         myCommand = new MySqlCommand(myInsertQuery);
                         myCommand.Connection = myConnection;                      
                         myCommand.ExecuteNonQuery();
                         
                     }
                 }
             }
         }
        catch (Exception ex) { Console.WriteLine(ex.ToString()); }
    }
    
    public void InsertLogBookEntry(int nMachine_ID, string strMessage)
    {
     
        DateTime time = DateTime.Now;
        string strtime = time.ToString(myDefinitions.ThorCustomFormat);
       
        try
        {
            if (strMessage.IndexOf("'") != -1)
            {
                strMessage = strMessage.Replace("'", " ");
            }
            if (strMessage.Length > 65535) { Console.WriteLine("Save-Message to long: " + strMessage); }
            else
            {
                myConnectionLogBook = new MySqlConnection(myConnectionString);
                string myInsertQuery = "INSERT INTO logBook (Timestamp,Machine_ID, Message) Values('" + strtime + "'," + nMachine_ID + ",'" + strMessage + "')";
                myCommandLogBook = new MySqlCommand(myInsertQuery);
                //  System.Windows.Forms.MessageBox.Show(myInsertQuery);
                myCommandLogBook.Connection = myConnectionLogBook;
                myConnectionLogBook.Open();
                myCommandLogBook.ExecuteNonQuery();
                myCommandLogBook.Connection.Close();
            }
        }
        catch (Exception ex) { Console.WriteLine(ex.ToString()); }
    }

    private string GETTypeString(int nType)
    {
        string strTypeString = null;
        switch (nType)
        {
            case (int) Definition.Message.D_ALARM:
                strTypeString = myDefinitions.AlarmTYPE_ALARM;
            break;
            case (int)Definition.Message.D_MESSAGE:
                strTypeString = myDefinitions.AlarmTYPE_MESSAGE;
            break;
            case (int)Definition.Message.D_DEBUG:
              strTypeString = myDefinitions.AlarmTYPE_DEBUG;
            break;
            case (int)Definition.Message.D_SEND:
              strTypeString = myDefinitions.AlarmTYPE_SEND;
            break;
            case (int)Definition.Message.D_RECEIVE:
              strTypeString = myDefinitions.AlarmTYPE_RECEIVE;
            break;
            default:
              strTypeString = "Unknown";
              break;
        }
        return strTypeString;
    }

   
 }
}