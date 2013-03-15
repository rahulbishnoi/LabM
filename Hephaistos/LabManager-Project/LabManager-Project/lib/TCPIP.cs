using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Timers;
using System.Threading;
using Logging;
using MySQL_Helper_Class;


namespace LabManager
{
    class TCPIP_Channel
    {
        private TCPIP_Server tcpip_Server = null;
        private TCPIP_Client tcpip_Client = null;
        private MySQL_HelperClass myHC = new MySQL_HelperClass();
        private int nType = -1;
        private int nPort = -1;
        private TCPIP_Helper.TCPIPHelper parent = null;
        private TCPIP.InterfaceAnalyse analyser = null;
        private string strTerminationString = null;


        public TCPIP_Channel(int nPort, string strIP_Address, int nMachine_ID = -1, int nAnalyseTypeID = -1, int nType = -1, TCPIP_Helper.TCPIPHelper parent = null)
        {
            this.nType = nType;
            this.parent = parent;
            this.nPort = nPort;




            // get the TerminationString if exist
            strTerminationString = myHC.GetTerminationStringFromTCPIPConfigurationByMachine_ID(nMachine_ID);

           

            analyser = new TCPIP.InterfaceAnalyse(nMachine_ID, this, nAnalyseTypeID);

            if (nType == 0)
            {
                tcpip_Server = new TCPIP_Server(nPort, nMachine_ID, nAnalyseTypeID, analyser, strTerminationString, parent);
            }
            if (nType == 1)
            {
                tcpip_Client = new TCPIP_Client(nPort, strIP_Address, nMachine_ID, nAnalyseTypeID, analyser, strTerminationString, parent);
            }

        }

        public int SendCommand(int nNumber, string strCommand, int nSample_ID, int nMachine_ID)
        {
            int ret = -1;
            if (nType == 0)
            {
                ret = tcpip_Server.SendCommand(nNumber, strCommand, nSample_ID, nMachine_ID);
            }
            if (nType == 1)
            {
                ret = tcpip_Client.SendCommand(nNumber, strCommand, nSample_ID, nMachine_ID);
            }
            return ret;
        }

        public void StopThread()
        {
            if (nType == 0)
            {
                tcpip_Server.StopThread();
            }
            if (nType == 1)
            {
                tcpip_Client.StopThread();
            }
        }

        public void WriteStream(string strMessage)
        {
           

            // Server -> nType=0
            if (nType == 0)
            {
                if (tcpip_Server != null)
                tcpip_Server.WriteStream(strMessage);
             } 
            else
           // Cient -> nType=1
            if (nType == 1)
            {
                if(tcpip_Client!=null)
                tcpip_Client.WriteStream(strMessage);
            }
        }

        public void WriteLoggEntry(int LogType, string strLogString, int AlarmType = (int)Definition.Message.D_MESSAGE)
        {
            
            strLogString = "[PORT:" + nPort + "] " + strLogString;

            if (strLogString.EndsWith("\n"))
            {
                strLogString = strLogString.Substring(0, strLogString.Length - 1);
            }
            if (strLogString.EndsWith("\r"))
            {
                strLogString = strLogString.Substring(0, strLogString.Length - 1);
            }

            if (parent != null)
            {
                parent.InsertLoggingEntry(LogType, strLogString, AlarmType);
            }
        }

        public int GetInfo(int nInfoType, int nInfo)
        {
            int nRet = -1;

            // Server
            if (nType == 0 && tcpip_Server != null)
            {
                nRet = tcpip_Server.GetInfo(nInfoType, nInfo);
            }
            else
            // Client
            if (nType == 1 && tcpip_Client!=null)
            {
                nRet = tcpip_Client.GetInfo(nInfoType, nInfo);
            }

            return nRet;
        }
       
    }

    class TCPIP_Server
    {
      
        private Save mySave = new Save("LabManager-TCPIP-Server");
        private Thread listenThread;
       
        private bool bOnlineStatusChanged = true;
        private volatile bool bDoCommunication = true;
        private TcpClient tcpClient = null;
        private int bufferSize = 4096;
        private TcpListener tcpListener;
        private int nMachine_ID = -1;
        private TCPIP_Helper.TCPIPHelper parent;
        private TCPIP.InterfaceAnalyse analyser = null;
        private bool bOnline = false;
        private int nPort = -1;
        private const int BACKLOG = 5;  // Größe der ausstehenden Queue
        private string strTerminationString = null;
        private System.Timers.Timer TCPIPServerTimer = new System.Timers.Timer();
    
        public TCPIP_Server(int nPort, int nMachine_ID = -1, int nAnalyseTypeID = -1, TCPIP.InterfaceAnalyse analyser = null, string strTerminationString = null, TCPIP_Helper.TCPIPHelper parent = null)
        {
   
            this.nMachine_ID = nMachine_ID;

            tcpListener = new TcpListener(IPAddress.Any, nPort);

            listenThread = new Thread(new ThreadStart(ListenForClients));
            listenThread.Name = "TCP/IP-Server Thread";
            listenThread.Start();

           // tcpListener = new TcpListener(IPAddress.Any, nPort);

            this.parent = parent;
            this.analyser = analyser;
            this.nPort = nPort;
            this.strTerminationString = strTerminationString;

            // Set the Interval to 5 seconds (5000 milliseconds).
            TCPIPServerTimer.Interval = 5000;
            TCPIPServerTimer.Enabled = true;
            TCPIPServerTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);

        }

   
        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
           
            // log that there is no connection (after a couple of seconds), if no client has connected during that time
            if (!bOnline)
            {
                CheckConnect();
            }
              
        }

        private void CheckConnect()
        {
            string strMessage = null;

            if (bOnlineStatusChanged != bOnline && bDoCommunication)
            {
                bOnlineStatusChanged = bOnline;
                if (tcpClient != null)
                {
                    if (tcpClient.Client != null)
                    {
                        if (tcpClient.Client.Connected)
                        {
                            strMessage = " to " + tcpClient.Client.RemoteEndPoint;
                        }
                    }
                }
                else
                {
                    WriteLoggEntry((int)Definition.ThorLogWindows.COMMUNICATION, "no client is connected!", (int)Definition.Message.D_MESSAGE);
                    return;
                }

                if (bOnline)
                {
                    WriteLoggEntry((int)Definition.ThorLogWindows.COMMUNICATION, "connection established" + strMessage , (int)Definition.Message.D_MESSAGE);
                }
                else
                {
                    WriteLoggEntry((int)Definition.ThorLogWindows.COMMUNICATION, "connection lost" + strMessage, (int)Definition.Message.D_MESSAGE);
                }
                
            }
           
         }

      

        private void ListenForClients()
        {
            try
            {
                this.tcpListener.Start();
                TcpClient client = null;
                while (bDoCommunication)
                {
                    //blocks until a client has connected to the server
                     client = this.tcpListener.AcceptTcpClient();
                      
                    //create a thread to handle communication
                    //with connected client
                   Thread clientThread = null;
                    clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                    clientThread.Name = "TCP/IP-Server tcpListener Thread";
                    clientThread.Start(client);
                    
                }
                if (client != null)
                {
                    client.Close();
                }
            }
            catch (Exception ex)
            {
                bDoCommunication = false; WriteLoggEntry((int)Definition.ThorLogWindows.COMMUNICATION, "Exception!\r\n" + ex.ToString(), (int)Definition.Message.D_MESSAGE);
            }
        }
        

        public int SendCommand(int nNumber, string strCommand, int nSample_ID, int nMachine_ID)
        {
            int ret = -1;
               ret = analyser.SendCommandFromRouting(nNumber, strCommand, nSample_ID, nMachine_ID);
            return ret;
        }

        private void HandleClientComm(object client)
        {
            tcpClient = (TcpClient)client;

            if (tcpClient.Connected)
            {
                NetworkStream clientStream = tcpClient.GetStream();
                // bool loop = true;
                byte[] message = new byte[bufferSize];
                int bytesRead;

                bOnline = true;
                bOnlineStatusChanged = !bOnline;

                CheckConnect();

                while (true)
                {
                    bytesRead = 0;

                    try
                    {
                        bOnline = true;
                        //blocks until a client sends a message
                        bytesRead = clientStream.Read(message, 0, bufferSize);

                    }
                    catch (SocketException se)
                    {
                        //a socket error has occured
                        WriteLoggEntry((int)Definition.ThorLogWindows.COMMUNICATION, "SocketException - errorcode=" + se.ErrorCode + ": " + se.Message, (int)Definition.Message.D_ALARM);

                        bOnline = false;
                      
                        break;

                    }
                    catch (Exception ex)
                    {
                        //a error has occured
                        bOnline = false;
                        WriteLoggEntry((int)Definition.ThorLogWindows.COMMUNICATION, "Exception:" + ex.ToString(), (int)Definition.Message.D_ALARM);
                
                        break;
                    }
                    
                    if (bytesRead == 0)
                    {
                        //the client has disconnected from the server
                        bOnline = false;
                       // WriteLoggEntry((int)Definition.ThorLogWindows.COMMUNICATION, "bytesRead == 0", (int)Definition.Message.D_ALARM);
                
                        break;
                    }

                    //message has successfully been received
                    ASCIIEncoding encoder = new ASCIIEncoding();

                    //    WriteLoggEntry((int)Definition.ThorLogWindows.COMMUNICATION, "RECEIVED: "+encoder.GetString(message, 0, bytesRead), (int)Definition.Message.D_RECEIVE);
                    string strMessage = encoder.GetString(message, 0, bytesRead);
                    if (strMessage.EndsWith("\0"))
                    {
                        strMessage = strMessage.Substring(0, strMessage.Length - 1);
                    }
                    string strAnalyseResult = analyser.MessageAnalyse(strMessage);
                    if (strAnalyseResult != null)
                    {
                        if (strAnalyseResult.Length > 0)
                        {
                            WriteAnswerBynAnalyseResult(strAnalyseResult);
                        }
                    }

                }
                bOnline = false;
                CheckConnect();
                tcpClient.Close();
                clientStream.Close();
              
            }
        }
        
        public void WriteStream(string strMessage)
        {
            if (strTerminationString.Length != 0)
                strMessage+= strTerminationString;

     
            NetworkStream clientStream = null;
            try
            {
                clientStream = tcpClient.GetStream();
                byte[] messageSend = new byte[bufferSize];
                if (strMessage.Length < bufferSize && strMessage != null && strMessage.Length > 0)
                {
                    ASCIIEncoding encoder = new ASCIIEncoding();
                    messageSend = encoder.GetBytes(strMessage);
                    clientStream.Write(messageSend, 0, messageSend.Length);
                    clientStream.Flush();

                }
                else
                {
                  //  clientStream.Close();
                    mySave.InsertRow((int)Definition.Message.D_ALARM, "Message to long to send (message > " + bufferSize + " characters)? or message empty");
                }
            }
            catch (Exception ex)
            {
                if (clientStream != null) { clientStream.Close(); }
                WriteLoggEntry((int)Definition.ThorLogWindows.COMMUNICATION, "EXCEPTION writing stream: " + ex.Message, (int)Definition.Message.D_ALARM);

            }
            finally
            {
                clientStream = null;
            }
        }

        private void WriteAnswerBynAnalyseResult(string strAnalyseResult)
        {
            if (strAnalyseResult != null)
            {
                WriteStream(strAnalyseResult);
            }
        }


        public void StopThread()
        {

            TCPIPServerTimer.Enabled = false;
            bDoCommunication = false;

            //stop the listener
            if (tcpListener != null)
            {
                try
                {
                    tcpListener.Stop();
                }
                catch (System.Net.Sockets.SocketException) { }
            }

          

            if (tcpClient != null)
            {
                if (tcpClient.Connected)
                {
                    try
                    {
                        if (tcpClient.Client.Connected)
                        {
                            tcpClient.Client.Shutdown(SocketShutdown.Both);
                        }
                        tcpClient.Close();
                    }
                    catch (System.Net.Sockets.SocketException) { }
                }
            }

            if (listenThread != null)
            {
                try
                {
                    // Wait for the the thread to stop.
                 //   listenThread.Join();
                    //stop the tcpListener thread
                    listenThread.Interrupt();
                    if (listenThread.IsAlive)
                    {
                        if(!listenThread.Join(200)) { // or an agreed resonable time
                            listenThread.Abort();
                        }
                     //   listenThread.Abort();
                        listenThread = null;
                    }
                }
                catch (System.Threading.ThreadAbortException) { }
            }
         
            
        }

        public void WriteLoggEntry(int LogType, string strLogString, int AlarmType = (int)Definition.Message.D_MESSAGE)
        {
            if (parent == null) { return; }

            strLogString = "[PORT:" + nPort + "] " + strLogString;

            if (strLogString.EndsWith("\n"))
            {
                strLogString = strLogString.Substring(0, strLogString.Length - 1);
            }
            if (strLogString.EndsWith("\r"))
            {
                strLogString = strLogString.Substring(0, strLogString.Length - 1);
            }
            if (strLogString.EndsWith("\0"))
            {
                strLogString = strLogString.Substring(0, strLogString.Length - 1);
            }

            parent.InsertLoggingEntry(LogType, strLogString, AlarmType);

               
        }

        public int GetInfo(int nInfoType, int nInfo)
        {
            int nRet = -1;

            switch (nInfoType)
            {
                // online status
                case 1:
                    if (bOnline)
                    {
                        nRet = 1;
                    }
                    else
                    {
                        nRet = 0;
                    }
                    break;

                // IO status
                case 10:
                    nRet = analyser.GetInfo(nInfo);
                    break;
            }
            return nRet;

        }

    }

    /// <summary>
    /// TCPIP_CLIENT
    /// </summary>
    class TCPIP_Client
    {
        private TCPIP_Helper.TCPIPHelper parent;
        private Save mySave = new Save("LabManager-TCPIP-Client");
        private System.Timers.Timer TCPIPReconnectTimer = new System.Timers.Timer();
        private System.Timers.Timer TCPIPCheckTimer = new System.Timers.Timer();
        private TCPIP.InterfaceAnalyse analyser = null;
   
        private NetworkStream myStream;
        private TcpClient myClient;
        private byte[] myBuffer;
        private Thread tListen = null;
        private ManualResetEvent TimeoutObject = new ManualResetEvent(false);
        private Exception socketexception;
     
        private string strServername = null;
        private string strSocketExceptionText = null;
        private string strTerminationString = null;
        private int nTimeout = 3000;
        private int nPort = -1;
        private int nReTryConnectCounter = 0;

        private bool bWaitForTimeout = false;
        private bool bFirstCall = true;
        private bool bOnlineStatusChanged = true;
        private bool bOnline = false;
        private volatile bool bDoCommunication = true;
     
        public TCPIP_Client(int nPort, string strServername, int nMachine_ID = -1, int nAnalyseTypeID = -1, TCPIP.InterfaceAnalyse analyser = null, string strTerminationString = null, TCPIP_Helper.TCPIPHelper parent = null)
        {

            this.parent = parent;
            this.strServername = strServername;
            this.nPort = nPort;
            this.strTerminationString = strTerminationString;
            this.analyser = analyser;

            /* create a new client object */
            myClient = new TcpClient();

           
            /* Vital: Create listening thread and assign it to ListenThread() */
             tListen = new Thread(new ThreadStart(ListenThread));
          
            /* Start listening thread */
            tListen.Start();
         
            // Hook up the Elapsed event for the timer.
            TCPIPReconnectTimer.Elapsed += new ElapsedEventHandler(OnReconnetTimedEvent);
            // Set the Interval to 10 seconds (10000 milliseconds).
            TCPIPReconnectTimer.Interval = 10000;
            TCPIPReconnectTimer.Enabled = true;

            // check every n seconds if the channel is alive
            TCPIPCheckTimer.Elapsed += new ElapsedEventHandler(OnCheckTimedEvent);
            // Set the Interval to n seconds (n*1000 milliseconds).
            TCPIPCheckTimer.Interval = 1000;
            TCPIPCheckTimer.Enabled = true;

            
            // try to connect, if servername and port is given
            if (strServername.Length > 0 && nPort > 0)
            {
                Connect();
            }
            else
            {
                WriteLoggEntry((int)Definition.ThorLogWindows.COMMUNICATION, "no servername and/or no port number available " , (int)Definition.Message.D_ALARM);
            }
         }


        private void OnCheckTimedEvent(object source, ElapsedEventArgs e)
        {
            // if "myClient" is available and no reconnection is still in process, try to reconnect
            if (myClient.Client != null && !bWaitForTimeout)
            {
            
                if (!IsConnected(myClient.Client))
                {
                    bOnline = false;
                    WriteConnectionStatus();

                }
                else
                {
                   
                }
            }
            // if the connection is established we can set the timer for reconnecting to false and only if the connection is down we have to reconnect
            if (bOnline)
            {
                TCPIPReconnectTimer.Enabled = false;
            }
            // if the connection is down set the timer to enable for reconnecting
            else
            {
                TCPIPReconnectTimer.Enabled = true;
            }

            
        }

        // function to check if the socket is alive
        // returns true if socket is alive, otherwise false
        public bool IsConnected(Socket socket)
        {
            
            try
            {

                return !(socket.Poll(1, SelectMode.SelectRead) && socket.Available == 0);
            }
            catch (SocketException) { return false; }
            catch (Exception) { return false; }
         
        }

        // timer function for reconnecting
        private void OnReconnetTimedEvent(object source, ElapsedEventArgs e)
        {

            WriteConnectionStatus();
            TryReConnect();

        }

        //if the connection is down try to reconnecting
        private void TryReConnect()
        {
           
               if (!bOnline && !bWaitForTimeout)
               {
                   nReTryConnectCounter++;
                 //  WriteLoggEntry((int)Definition.ThorLogWindows.COMMUNICATION, "try to reconnect ... count " + nReTryConnectCounter, (int)Definition.Message.D_MESSAGE);
              
                   Connect(); 
               }
            // to log the connection status
               WriteConnectionStatus();
            
        }

        // connect to the server
        // method will try to get the "myClient" object
        private void Connect()
        {
 
            try
                {
                  bOnline = false; 
                  bWaitForTimeout = true;
                  myClient = Connect(strServername, nPort, nTimeout);

                // if the TCP client is not null set the stream object to the client and setup a buffer
                       if (myClient != null)
                       {
                        
                           myStream = myClient.GetStream();
                           // Create data buffer 
                           myBuffer = new byte[myClient.ReceiveBufferSize];
                       
                           if (myStream != null)
                           {
                               bOnline = true;
                               // client is connected, so set the reconnect counter to 0
                               nReTryConnectCounter = 0;
                           }
                           else { bOnline = false; }
                       }
                    
                       
                }
            catch (System.Net.Sockets.SocketException ex)
            {
                strSocketExceptionText = ex.SocketErrorCode.ToString();
                bOnline = false;  
            }

            catch (Exception) { bOnline = false; }
            
        }

       

        public TcpClient Connect(string address, int nPort, int timeoutMSec)
        {
            TimeoutObject.Reset();
            socketexception = null;  
           
            TcpClient tcpclient = new TcpClient();

            tcpclient.BeginConnect(address, nPort, new AsyncCallback(CallBackMethod), tcpclient);

            if (TimeoutObject.WaitOne(timeoutMSec, false))
            {

                if (bOnline)
                {
                    bWaitForTimeout = false;
                    WriteConnectionStatus();
                    return tcpclient;
                }
                else
                {   bWaitForTimeout = false;
                    WriteConnectionStatus();
                    // "socketexception" is set in the callback function
                    throw socketexception;
                }
                
            }
            else // if timeout occurs, throw new Timeout exception
            {
                if (tcpclient.Client.Connected) { tcpclient.Close(); }
                strSocketExceptionText = "Connect: TimeOut Exception after " + nTimeout + " milli seconds";
                bWaitForTimeout = false;
                WriteConnectionStatus();
                throw new TimeoutException(strSocketExceptionText);
                
            }
           
        }
    
        private  void CallBackMethod(IAsyncResult asyncresult)
        {
            try
            {
                bOnline = false;
               
                TcpClient tcpclient = asyncresult.AsyncState as TcpClient;
             
                if (tcpclient.Client != null)
                {
                    tcpclient.EndConnect(asyncresult);
                    bOnline = true;
                    
                }
            }
            catch (Exception ex)
            {
                bOnline = false;
              
                socketexception = ex;
            }
            finally
            {
                TimeoutObject.Set();
            }

        }

        // method to log(in Labmanger communication window) the status of the connection
        private void WriteConnectionStatus()
        {

            if (bOnlineStatusChanged != bOnline || bFirstCall)
            {
                bFirstCall = false;
                bOnlineStatusChanged = bOnline;
                if (!bOnline)
                {
                    WriteLoggEntry((int)Definition.ThorLogWindows.COMMUNICATION, "no connection to " + strServername + "! " + strSocketExceptionText, (int)Definition.Message.D_MESSAGE);
                }
                else
                {
                    WriteLoggEntry((int)Definition.ThorLogWindows.COMMUNICATION, "Connection established to " + strServername, (int)Definition.Message.D_MESSAGE);
                }
            }

        }

      
        /* Thread responsible for "remote input" */
        private void ListenThread()
        {
            int lData = -1;
            while (bDoCommunication)
            {
                if (myClient != null && myStream!=null && bOnline)
                     {
                        try
                        {
                            // /* Reading data from socket (stores the length of data) 
                            lData = myStream.Read(myBuffer, 0, myClient.ReceiveBufferSize);  
                        }
                        catch(SocketException) {
                            bOnline = false;
                            WriteConnectionStatus();
                        }
                        catch (Exception)
                        {
                            bOnline = false;
                            WriteConnectionStatus();
                        }
                        if (lData > 0 && bOnline)
                        {
                            // /* String conversion (to be displayed on console) 
                            String myString = Encoding.ASCII.GetString(myBuffer);
                            // /* Trimming data to needed length, 
                            //    because TcpClient buffer is 8kb long 
                            // /* and we don't need that load of data 
                            //   to be displayed at all times 
                            // /* (this could be done better for sure) 
                            myString = myString.Substring(0, lData);
                            // /* Display message 
                            if (myString.Length > 0)
                            {

                                if (myString.EndsWith(strTerminationString))
                                {
                                    myString = myString.Substring(0, myString.Length - strTerminationString.Length);
                                }
                          
                          
                                // send the data to the analyser which analyses the string and returns the answer
                                string strAnalyseResult = analyser.MessageAnalyse(myString);

                                if (strAnalyseResult != null)
                                {
                                    if (strAnalyseResult.Length > 0)
                                    {
                                        //write the answer back to the endpoint
                                        WriteStream(strAnalyseResult);    
                                    }
                                }

                            }
                            else
                            {
                                if (bOnline)
                                {
                                    bOnline = false;
                                }
                            }
                        } 
                    }
                    
                 System.Threading.Thread.Sleep(50);  
                }
            
        }
       
        // stop the TCP connection, threads etc., this method is called from the LabManager dispoese method
        public void StopThread()
        {
           
            TCPIPReconnectTimer.Stop();
            TCPIPCheckTimer.Stop();

            // stop while loops
            bDoCommunication = false;

          
            //stop the tcp thread
            if (tListen != null )
            {
                this.tListen.Join(1000);
                if (tListen.IsAlive)
                {
                    this.tListen.Abort();
                }
                
            }

            // close the stream
            if (myStream != null) { myStream.Close(); }

            if (myClient != null)
            {
                if (myClient.Connected)
                {
                    myClient.Close();
                }
            }
          

           
        }

        public int SendCommand(int nNumber, string strCommand, int nSample_ID, int nMachine_ID)
        {
            int ret = -1;
            ret = analyser.SendCommandFromRouting(nNumber, strCommand, nSample_ID, nMachine_ID);
            return ret;
        }

        public int GetInfo(int nInfoType, int nInfo)
        {
            int nRet = -1;

            switch (nInfoType)
            {
                    // online status
                case 1:
                    if (bOnline)
                    {
                        nRet = 1;
                    }
                    else
                    {
                        nRet = 0;
                    }
                    break;

                    // IO status
                case 10:
                    nRet = analyser.GetInfo(nInfo);
                    break;
            }
            return nRet;

        }

        public int WriteStream(string strCommand)
        {
            int ret = -1;
            if (strTerminationString.Length!=0)
                strCommand += strTerminationString;

            if (bOnline)
            {
                // Console.WriteLine("Sending...");
                if (myStream != null && strCommand != null && strCommand.Length > 0)
                {
                    
                    try
                    {
                        myStream.Write(Encoding.ASCII.GetBytes(strCommand.ToCharArray()), 0, strCommand.Length);
                        //  WriteLoggEntry((int)Definition.ThorLogWindows.COMMUNICATION, "SEND: " + strCommand, (int)Definition.Message.D_SEND);
                        ret = 0;
                    }
                    catch (System.ObjectDisposedException exDis)
                    {
                        mySave.InsertRow((int)Definition.Message.D_ALARM, "TCPIP_Client::SendString: " + exDis.ToString());
                        ret = -2;
                    }
                    catch (System.Exception ex)
                    {
                        mySave.InsertRow((int)Definition.Message.D_ALARM, "TCPIP_Client::SendString: " + ex.ToString());
                        ret = -2;
                    }
                }
                else
                {
                    mySave.InsertRow((int)Definition.Message.D_ALARM, "stream object null - server not online?");
                    ret = -3;
                }
            }
            else { mySave.InsertRow((int)Definition.Message.D_ALARM, "server not online?"); ret = -4; }
            return ret;
        }

        public void WriteLoggEntry(int LogType, string strLogString, int AlarmType = (int)Definition.Message.D_MESSAGE)
        {
            if (parent == null) { return; }

            strLogString = "[PORT:" + nPort + "] " + strLogString;

            if (strLogString.EndsWith("\n"))
            {
                 strLogString = strLogString.Substring(0, strLogString.Length - 1); 
            }
            if (strLogString.EndsWith("\r"))
            {
                strLogString = strLogString.Substring(0, strLogString.Length - 1);
            }
            if (strLogString.EndsWith("\0"))
            {
                strLogString = strLogString.Substring(0, strLogString.Length - 1);
            }

            parent.InsertLoggingEntry(LogType, strLogString, AlarmType);

         
        }

       
    }

    

 
}
