using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace COL.MultiGlycan
{
    public static class Logger
    {
        public static void WriteLog(string argMsg)
        {
            StreamWriter LogWriter = new StreamWriter(System.Windows.Forms.Application.StartupPath + "\\log.txt", true);
            LogWriter.WriteLine(DateTime.Now.ToString("MMdd HH:mm") + "\t\t" + argMsg);
            LogWriter.Close();
        }
    }
}
