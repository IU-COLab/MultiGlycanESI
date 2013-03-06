using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Win32;
namespace COL.MultiGlycan
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            RegistryKey key;
            key = Registry.ClassesRoot.OpenSubKey(@"TypeLib\");
            bool foundXCalibur = false;
            foreach (String keyName in key.GetSubKeyNames())
            {
                if (keyName == "{5FE970A2-29C3-11D3-811D-00104B304896}")
                {
                    foundXCalibur = true;
                    break;
                }    
            }
            if (!foundXCalibur)
            {     
                MessageBox.Show("Please install Xcalibur to support .raw or use mzXML as input");              
            }
            Application.Run(new frmMainESI());
        }
    }
}
