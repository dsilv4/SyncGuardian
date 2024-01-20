using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncGuardian.Services
{
    public class LogService
    {
        /// <summary>
        /// registers messages on a log file
        /// </summary>
        public static void LogAction(string logMessage, string logFilePath)
        {
            var pathInfo = new FileInfo(logFilePath + "_LogFile.txt");
            try
            {
                if (pathInfo.Exists) 
                {
                    using (StreamWriter sw = new StreamWriter(pathInfo.FullName, true))
                    {
                        sw.WriteLine(logMessage + " - " + DateTime.Now);
                    }
                }
                else
                {
                    using (StreamWriter sw = new StreamWriter(pathInfo.FullName, false))
                    {
                        sw.WriteLine(logMessage + " - " + DateTime.Now);
                    }
                }

                Console.WriteLine(logMessage + " - " + DateTime.Now);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

        }

        public static void LogActionSpacer(string logFilePath)
        {
            var pathInfo = new FileInfo(logFilePath + "_LogFile.txt");
            try
            {
                if (pathInfo is null)
                    throw new Exception("pathInfo is null - " + DateTime.Now);

                using (StreamWriter sw = new StreamWriter(pathInfo.FullName, true))
                {
                    sw.WriteLine(Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

        }


    }
}
