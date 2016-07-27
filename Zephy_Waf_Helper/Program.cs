using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using Microsoft.Win32;

namespace Zephy_Waf_Helper
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Count() == 1)
            {
                if (String.Equals(args[0], "ZephySetup"))
                {
                    RegistryKey classesRoot = Registry.ClassesRoot;
                    RegistryKey key = classesRoot.OpenSubKey(@"Directory\Background\shell\ZephyWafHelper\command", true);

                    string path = Application.ExecutablePath;
                    path = "\"" + path;
                    path += "\" %V";
                    bool derp = true;

                    if(key == null)
                    {
                        key = classesRoot.CreateSubKey(@"Directory\Background\shell\ZephyWafHelper\command", RegistryKeyPermissionCheck.ReadWriteSubTree);
                        
                    }
                    
                    key.SetValue(null, path, RegistryValueKind.String);


                    bool herp = true;
                }
            }
            if (args.Count() == 0)
            {
                Application.Run(new MainWindow("NoPathSpecifiedAsArgs"));
            }                
            else
                Application.Run(new MainWindow(args[0]));
        }
    }
}
