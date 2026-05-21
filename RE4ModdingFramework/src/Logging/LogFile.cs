using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RE4ModdingFramework.src.Logging
{
    public static class LogFile
    {
        private static readonly string? projectName = Assembly.GetEntryAssembly().GetName().Name;

        private static readonly string dirPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), projectName, "Logs");

        public static void CreateDirectory()
        {
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
        }

        public static void WriteToFile(string msg)
        {
            try
            {
                File.AppendAllText(Path.Combine(dirPath, "Log_[" + Time.GetDay() + "].txt"), msg + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("LOGGING FAILURE: " + ex.Message);
                Console.ResetColor();
            }
        }
    }
}
