using System;
//using System.Linq;
using System.Text;
using cs_IniHandlerDevelop;
using Definition;
using System.Windows.Forms;

namespace AssemblyLoad
{
    class Assembly_Load
    {
        string Path;
        IniStructure inis = new IniStructure();
        Definitions CDef = new Definitions();



        public int RemoteObject(int nForm, int nMachine, int nReserve, string strSampleID)
        {
            int returnValue = -1;
            string IniFileName = CDef.PathIniFileName;
            string IniFilePath = null;
            string localPath = System.IO.Path.GetDirectoryName(new System.Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            IniFilePath = localPath + @"\..\..\..\..\" + IniFileName;
            inis = IniStructure.ReadIni(IniFilePath);
            string ProjectPath = localPath + @"\..\..\..\..\";
            string strAssemblyInfo = "";

            if (inis == null)
                MessageBox.Show("Something went wrong", "error");


            switch (nForm)
            {
                case 1:
                    {
                        Path = inis.GetValue("Path", "Machine_Info");
                        strAssemblyInfo = "Machine_Info.RemoteObject";
                        break;
                    }
                case 2:
                    {
                        Path = inis.GetValue("Path", "Routing_Table");
                        strAssemblyInfo = "Routing_Table.RemoteObject";
                        break;
                    }

                case 3:
                    {
                        Path = inis.GetValue("Path", "Sample_Info");
                        strAssemblyInfo = "Sample_Info.RemoteObject";
                        break;
                    }
                case 4:
                    {
                        Path = inis.GetValue("Path", "Logging_Info");
                        strAssemblyInfo = "Logging_Info.RemoteObject";
                        break;
                    }

                case 5:
                    {
                        Path = inis.GetValue("Path", "Sample_Registration");
                        strAssemblyInfo = "Sample_Registration.RemoteObject";
                        break;
                    }
                case 6:
                    {
                        Path = inis.GetValue("Path", "Sample_Statistic");
                        strAssemblyInfo = "Sample_Statistic.RemoteObject";
                        break;
                    }
                case 7:
                    {
                        Path = inis.GetValue("Path", "Sample_ValueStatistic");
                        strAssemblyInfo = "SampleValue_Statistic.RemoteObject";
                        break;
                    }
                case 8:
                    {
                        Path = inis.GetValue("Path", "Magazine_Info");
                        strAssemblyInfo = "Magazine_Info.RemoteObject";
                        break;
                    }
                case 9:
                    {
                        Path = inis.GetValue("Path", "Lab_Manager");
                        strAssemblyInfo = "LabManager.RemoteObject";  
                        break;
                    }
                case 10:
                    {
                        Path = @"d:\Arbeit\Hephaistos\WindowsFormsApplication1\WindowsFormsApplication1\bin\Debug\WindowsFormsApplication1.exe";
                        strAssemblyInfo = "WindowsFormsApplication1.RemoteObject";
                        object[] argstopass2 = new object[] { (object)nForm, (object)nMachine, (object)nReserve, (object)strSampleID };

                        System.Reflection.Assembly newAssembly2 = System.Reflection.Assembly.LoadFrom( Path);
                             newAssembly2.CreateInstance(strAssemblyInfo,
                           false, //do not ignore the case
                           System.Reflection.BindingFlags.CreateInstance, //specifies we want to call a ctor method
                           null, //a null binder specifies the default binder will be used (works in most cases)
                           argstopass2, //the arguments (based on the arguments, CreateInstance() will decide which ctor to invoke)
                           null, //CultureInfo is null so we will use the culture info from the current thread
                           null //no activation attributes
                           );
                        return 10;
                    }
            }
            //the arguments we will pass
            object[] argstopass = new object[] { (object)nForm, (object)nMachine, (object)nReserve, (object)strSampleID };

            System.Reflection.Assembly newAssembly = System.Reflection.Assembly.LoadFrom(ProjectPath+Path);

            newAssembly.CreateInstance(strAssemblyInfo,
            false, //do not ignore the case
            System.Reflection.BindingFlags.CreateInstance, //specifies we want to call a ctor method
            null, //a null binder specifies the default binder will be used (works in most cases)
            argstopass, //the arguments (based on the arguments, CreateInstance() will decide which ctor to invoke)
            null, //CultureInfo is null so we will use the culture info from the current thread
            null //no activation attributes
            );
     

            return returnValue;
        }
    }
}
