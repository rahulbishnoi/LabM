using System;
using System.Windows.Forms;
using MySQL_Helper_Class;
using System.Threading;
using System.Diagnostics;
using System.Reflection;


namespace LabManager
{
    static class Program
    {
     
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool createdNew;
            new System.Threading.Mutex(true, Application.ProductName, out createdNew);

            if (!createdNew || Program.AlreadyRunning()) {
                MessageBox.Show("Instance of LM already running !!!", "Error");
                Application.Exit();
                return;
            }
            else
            { 
                 MySQL_HelperClass myHC = new MySQL_HelperClass();

                //exit if   no connection to DB
                if (myHC.ConnectionTest()) 
                
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    // Setup the server configuration from file...
                    // NOTE: Configuring this way allows reconfiguration without the need to recompile...
                    // Create form object and run...
                    
                    Application.Run(new LabManager());
                    
             
                }
            }
        }

        private static bool AlreadyRunning()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            foreach (Process process in processes)
            {
                if (process.Id != current.Id)
                {
                    
                        return true;
                    
                }
            }
            return false;
        }  
    }
}
