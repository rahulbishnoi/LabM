using System;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace LabManager
{
    public partial class ScannerForm : Form
    {
   //     private byte[] byteBuffer = null;
   //     private byte[] rcvBuffer = new byte[20];
        private string server = null;
      //  int servPort = -1;
        Thread Receiver;
     //   TcpClient client = null;
     //   NetworkStream netStream = null;
        bool bOnline = false; // true wenn Formular beendet werden soll
            // The port number for the remote device.
        private  int port = 9004;
        Socket clientAsync = null;
        // ManualResetEvent instances signal completion.
        private  ManualResetEvent connectDone = new ManualResetEvent(false);
        private  ManualResetEvent sendDone =   new ManualResetEvent(false);
        private  ManualResetEvent receiveDone =  new ManualResetEvent(false);

        // The response from the remote device.
        private  String response = String.Empty;
       

        public ScannerForm()
        {
            InitializeComponent();
          
        }

        private void ScannerForm_Load(object sender, EventArgs e)
        {

            textBox_Port.Text = port.ToString();
            textBox_IP.Text = "192.168.0.1";
            textBox_Message.Text = "LON\r\n";
            textBox_Message2.Text = "LOFF\r\n";
            textBox_Receive.Text = "";
            

            // Verwende den angegebenen Host, ansonsten nimm den lokalen Host
            server = textBox_IP.Text;
            // Verwende den angegebenen Port

            Int32.TryParse(textBox_Port.Text, out port);

           // Receiver = new Thread(new ParameterizedThreadStart(Receive));
            Receiver = new Thread(new ThreadStart(Receive));
           
            Receiver.Start();
           // StartClient();
            checkBox_Online.Checked = bOnline;
        }

        private void WriteLoggEntry( string strLogString)
        {

            MethodInvoker Logging = delegate
            {
                textBox_Receive.AppendText(strLogString);
            };
            try
            {
                Invoke(Logging);
            }
            catch { }
        }

        private void button_Command1_Click(object sender, EventArgs e)
        {
           if (bOnline)
            {
                Send(clientAsync, textBox_Message.Text);
                sendDone.WaitOne();
            }

        }

        private void button_Command2_Click(object sender, EventArgs e)
        {
            if (bOnline)
            {

                Send(clientAsync, textBox_Message2.Text);
                sendDone.WaitOne();
            }

        }

        private byte[] StringToByteArray(string str)
        {
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            return enc.GetBytes(str);
        }

        private string ByteArrayToString(byte[] arr)
        {
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            return enc.GetString(arr);
        }
  
        //object portip
        // tread start
        private void Receive()
        {

            StartClient();
        }

       
        private void button_Stop_Click(object sender, EventArgs e)
        {

        }

        // close/stop the thread
        protected override void Dispose(bool disposing)
        {
          
            if (sendDone != null)
            {
                sendDone.Close();
            }

            if (receiveDone != null)
            {
                receiveDone.Close();
            }

            if (connectDone != null)
            {
                connectDone.Close();
            }

          //  Thread.Sleep(3000);

            if (clientAsync != null && clientAsync.Connected)
            {
                clientAsync.Shutdown(SocketShutdown.Both);
                clientAsync.Close();
            }

            if (Receiver != null && Receiver.IsAlive)
            {
                Receiver.Abort();
                Receiver.Join();
            }
           

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);

          

        }



        private void StartClient()
        {
            // Connect to a remote device.
            try
            {
                // Establish the remote endpoint for the socket.
                // The name of the 
                // remote device is "host.contoso.com".
             //   IPHostEntry ipHostInfo = Dns.Resolve("localhost");
                //  IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPAddress ipAddress = System.Net.IPAddress.Parse(textBox_IP.Text);
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

                // Create a TCP/IP socket.
                clientAsync = new Socket(AddressFamily.InterNetwork,  SocketType.Stream, ProtocolType.Tcp);

                // Connect to the remote endpoint.
                clientAsync.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), clientAsync);
               // if (bOnline)
                {
                    connectDone.WaitOne();
                }

                // Send test data to the remote device.
              //  Send(clientAsync, "LON<EOF>");
              //  sendDone.WaitOne();

                // Receive the response from the remote device.
                Receive(clientAsync);
            //    receiveDone.WaitOne();

                // Write the response to the console.
           //     Console.WriteLine("Response received : {0}", response);

                // Release the socket.
           //     clientAsync.Shutdown(SocketShutdown.Both);
            //   clientAsync.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                
                // Retrieve the socket from the state object.
                Socket client = (Socket)ar.AsyncState;

                // Complete the connection.
                client.EndConnect(ar);

                Console.WriteLine("Socket connected to {0}",
                    client.RemoteEndPoint.ToString());

                // Signal that the connection has been made.
                connectDone.Set();
               
                bOnline = true;
            }
            catch (System.Net.Sockets.SocketException) {
                if (connectDone != null)
                {
                    connectDone.Close();
                }
                if (Receiver != null && Receiver.IsAlive)
                {
                    try
                    {
                        Receiver.Abort();
                        Receiver.Join();
                    }
                    catch { }
                }
                WriteLoggEntry("can't connect to EndPoint: " + textBox_IP.Text + ":" +port);
                bOnline = false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                bOnline = false;
            }
            
        }

        private void Receive(Socket client)
        {
            try
            {
                // Create the state object.
                StateObject state = new StateObject();
                state.workSocket = clientAsync;

                // Begin receiving the data from the remote device.
                clientAsync.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the state object and the client socket 
                // from the asynchronous state object.
                StateObject state = (StateObject)ar.AsyncState;
                Socket client = state.workSocket;

                // Read data from the remote device.
                int bytesRead = client.EndReceive(ar);

                if (bytesRead > 0)
                {
                    // There might be more data, so store the data received so far.
                    state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));

                    // Get the rest of the data.
                    client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                    string strT = null;
                    strT = string.Copy(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
                    Console.WriteLine(strT);

                    try{
                        WriteLoggEntry(strT+"\r\n");
                      }catch{}
                    
               //     Send(client, "LOFF\r\n");
               //     sendDone.WaitOne();
              //      Thread.Sleep(2000);
               //     Send(client, "LON\r\n");
               //     sendDone.WaitOne();
                }
                else
                {
                    // All the data has arrived; put it in response.
                    if (state.sb.Length > 1)
                    {
                        response = state.sb.ToString();
                    }
                    // Signal that all bytes have been received.
                    receiveDone.Set();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void Send(Socket client, String data)
        {
            // Convert the string data to byte data using ASCII encoding.
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.
            client.BeginSend(byteData, 0, byteData.Length, 0,   new AsyncCallback(SendCallback), client);
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket client = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSent = client.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to server.", bytesSent);

                // Signal that all bytes have been sent.
                sendDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }


    
    }

    // State object for receiving data from remote device.
    public class StateObject
    {
        // Client socket.
        public Socket workSocket = null;
        // Size of receive buffer.
        public const int BufferSize = 256;
        // Receive buffer.
        public byte[] buffer = new byte[BufferSize];
        // Received data string.
        public StringBuilder sb = new StringBuilder();
    }

   
    


}
