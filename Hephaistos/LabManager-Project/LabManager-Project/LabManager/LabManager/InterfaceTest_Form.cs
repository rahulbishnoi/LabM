using System;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using Logging;

namespace LabManager
{
    public partial class InterfaceTest_Form : Form
    {
       // TCPIP_Client tcpip_connection = null;
        Save mySave = new Save("LabManager-TCPIP-Client");
        Thread MyThread = null;
        int nButton = -1;
        public InterfaceTest_Form()
        {
            InitializeComponent();
               
        }

        private void InterfaceConfig_Form_Load(object sender, EventArgs e)
        {
            textBox_Port.Text = "2501";
            textBox_IP.Text = "127.0.0.1";
            textBox_Message.Text = "ARE_YOU_THERE@";
           
        //   tcpip_connection = new TCPIP_Client(System.Convert.ToInt32(textBox_Port.Text), textBox_IP.Text, textBox_Message.Text);
       
        }
        private void DisableFields()
        {
            textBox_Port.Enabled = false;
            textBox_IP.Enabled = false;
            textBox_Message.Enabled = false;
            button_Test.Enabled = false;
        }



        private void EnableFields()
        {
            textBox_Port.Enabled = true;
            textBox_IP.Enabled = true;
            textBox_Message.Enabled = true;
            button_Test.Enabled = true;
        }

        private void WriteToStatusBar(string Message)
        {
            EnableFields();
            statusStrip_TCPIP.Text = Message;
        }

        private void button_Test_Click(object sender, EventArgs e)
        {
            if (textBox_Message.Text.Length > 0 && textBox_IP.Text.Length > 0)
            {
                DisableFields();
                nButton = 1;
               // tcpip_connection.DoNetworkingConnection();
                DoNetworkingConnection();
                WriteToStatusBar(textBox_Message.Text);
               
            }

        }

        public void DoNetworkingConnection()
        {
            try
            {
                ThreadStart ThreadMethod = new ThreadStart(ConnectTo);

                MyThread = new Thread(ThreadMethod);
            }
            catch (Exception e)
            {
                mySave.InsertRow((int)Definition.Message.D_ALARM, "Failed to create thread with error: " + e.Message);
                return;
            }

            try
            {
                MyThread.Start();
            }
            catch (Exception e)
            {
                mySave.InsertRow((int)Definition.Message.D_ALARM, "The thread failed to start with error: " + e.Message);
            }

        }


        private void ConnectTo()
        {

            mySave.InsertRow((int)Definition.Message.D_MESSAGE, "IP Address: " + textBox_IP.Text + "Port: " + System.Convert.ToInt32(textBox_Port.Text));
            Socket ClientSocket = null;

            try
            {
                // Let's connect to a listening server
                try
                {
                    ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                    if (mySave.DEBUG_MODE) { mySave.InsertRow((int)Definition.Message.D_DEBUG, "Socket is OK..."); }
                }
                catch (Exception e)
                {
                     mySave.InsertRow((int)Definition.Message.D_ALARM, "Failed to create client Socket: " + e.Message);
                    throw new Exception("Failed to create client Socket: " + e.Message);
                }

                IPEndPoint ServerEndPoint = new IPEndPoint(IPAddress.Parse(textBox_IP.Text), Convert.ToInt16(System.Convert.ToInt32(textBox_Port.Text)));

                try
                {
                    ClientSocket.Connect(ServerEndPoint);
                    if (mySave.DEBUG_MODE) { mySave.InsertRow((int)Definition.Message.D_DEBUG, "Connect() is OK..."); }
                }
                catch (Exception e)
                {
                    mySave.InsertRow((int)Definition.Message.D_ALARM, "Failed to connect client Socket: " + e.Message);
                    throw new Exception("Failed to connect client Socket: " + e.Message);
                }
            }
            catch (Exception e)
            {
                mySave.InsertRow((int)Definition.Message.D_ALARM, e.Message);
                ClientSocket.Close();
                return;
            }

            // Let's create a network stream to communicate over the connected Socket.
            NetworkStream ClientNetworkStream = null;

            try
            {
                try
                {
                    // Setup a network stream on the client Socket
                    ClientNetworkStream = new NetworkStream(ClientSocket, true);
                    if (mySave.DEBUG_MODE) { mySave.InsertRow((int)Definition.Message.D_DEBUG, "Instantiating NetworkStream..."); }
                }
                catch (Exception e)
                {
                    // We have to close the client socket here because the network
                    // stream did not take ownership of the socket.
                    ClientSocket.Close();
                    throw new Exception("Failed to create a NetworkStream with error: " + e.Message);
                }

                StreamWriter ClientNetworkStreamWriter = null;

                try
                {
                    // Setup a Stream Writer
                    ClientNetworkStreamWriter = new StreamWriter(ClientNetworkStream);
                    if (mySave.DEBUG_MODE) { mySave.InsertRow((int)Definition.Message.D_DEBUG, "Setting up StreamWriter..."); }
                }
                catch (Exception e)
                {
                    ClientNetworkStream.Close();
                    throw new Exception("Failed to create a StreamWriter with error: " + e.Message);
                }

                try
                {
                    if (nButton == 1)
                    {
                        ClientNetworkStreamWriter.Write(textBox_Message.Text);
                        
                    }

                    ClientNetworkStreamWriter.Flush();
                    StreamReader ClientNetworkStreamReader = null;
                    ClientNetworkStreamReader = new StreamReader(ClientNetworkStream);
                    //ClientNetworkStreamReader.Read();
                   // textBox_Receive.AppendText(ClientNetworkStreamReader.ReadToEnd());
                   // textBox_Receive.AppendText(ClientNetworkStreamReader.ReadLine());

                    if (mySave.DEBUG_MODE) { mySave.InsertRow((int)Definition.Message.D_SEND, "wrote " + textBox_Message.Text + " (" + textBox_Message.Text.Length.ToString() + " character(s)) to the server."); }
                    if (ClientSocket != null)
                    {
                        ClientSocket.Close();
                    }
                    
                }
                catch (Exception e)
                {
                    throw new Exception("Failed to write to client NetworkStream with error: " + e.Message);
                }
                
            }

            catch (Exception e)
            {
                if (mySave.DEBUG_MODE) { mySave.InsertRow((int)Definition.Message.D_DEBUG, e.Message); }
            }
            finally
            {
                // Close the network stream once everything is done
                ClientNetworkStream.Close();
                
            }
        }

       
    }
}
