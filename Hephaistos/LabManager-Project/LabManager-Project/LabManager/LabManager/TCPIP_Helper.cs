using System.Windows.Forms;

namespace TCPIP_Helper
{
    class TCPIPHelper
    {
        private LabManager.LabManager parent;
        public TCPIPHelper( LabManager.LabManager parent)
        {
            this.parent = parent;
         }

        public  void InsertLoggingEntry(int defLogWindow, string logEntry, int AlarmType = (int)Definition.Message.D_MESSAGE)
        {
            
            MethodInvoker Logging = delegate
            {
                // doing Logging entries
                parent.WriteTCPIPLoggEntry(defLogWindow, logEntry, AlarmType);
            };
            try
            {
                parent.Invoke(Logging);
            }
            catch { }
        }
       
    }
}
