using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace UtilityClasses
{
    public class LogFile
    {
        public int MaxFileSizeInKb = 0;
        private DateTime TimeFileWasLastTruncated = DateTime.MinValue;

        public LogFile()
        {
            string CurrentExecutablePath = Assembly.GetExecutingAssembly().GetName().CodeBase;
            CurrentExecutablePath = CurrentExecutablePath.Substring(8).Replace("/", "\\");
            LogFilePath = Path.ChangeExtension(CurrentExecutablePath, "log");
            CreateLogFileIfNeeded();
        }

        public LogFile (string pLogFilePath)
        {
            LogFilePath = pLogFilePath;
            CreateLogFileIfNeeded();
        }

        private string logFilePath;
        public string LogFilePath
        {
            get
            {
                return this.logFilePath;
            }
            set
            {
                this.logFilePath = value;
            }
        }

        private void CreateLogFileIfNeeded ()
        {
            string LogFileFolder = Path.GetDirectoryName(LogFilePath);

            if ((!string.IsNullOrEmpty(LogFileFolder)) && !Directory.Exists(LogFileFolder))
            {
                Directory.CreateDirectory(LogFileFolder);
            }

            if (!File.Exists(LogFilePath))
            {
                StreamWriter sw = File.CreateText(LogFilePath);
                sw.Close();
            }
                
        }

        public void Write(string logMessage, bool writeIfThisValueIsTrue = true)
        {
            if (writeIfThisValueIsTrue)
            {
                logMessage = logMessage.Replace("\r\n", "\r\n                      ");
                logMessage = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " - " + logMessage + "\r\n";
                TruncateFile();
                try
                {
                    File.AppendAllText(LogFilePath, logMessage);
                }
                catch (Exception ex)
                {
                    Debug.Print("Could not write to log file. Exception details: " + ex.Message);
                }
            }
        }

        private void TruncateFile()
        {
            if ((MaxFileSizeInKb > 0) && (DateTime.Now > TimeFileWasLastTruncated.AddMinutes(5)))
            {
                int BytesToRetain = MaxFileSizeInKb * 1024;
                TimeFileWasLastTruncated = DateTime.Now;
                bool FileNotLargeEnoughToTruncate = false;
                try
                {
                    using (MemoryStream ms = new MemoryStream(BytesToRetain))
                    {
                        using (FileStream s = new FileStream(LogFilePath, FileMode.Open, FileAccess.ReadWrite))
                        {
                            try
                            {
                                s.Seek(-BytesToRetain, SeekOrigin.End);
                            }
                            catch (Exception)
                            {
                                FileNotLargeEnoughToTruncate = true;
                            }
                            if (!FileNotLargeEnoughToTruncate)
                            {
                                s.CopyTo(ms);
                                s.SetLength(BytesToRetain);
                                s.Position = 0;
                                ms.CopyTo(s);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.Print("Error while trying to truncate logfile.");
                    Debug.Print("Exception details: " + ex.Message);
                    if (ex.InnerException != null)
                    {
                        Debug.Print("Inner Exception details: " + ex.InnerException.Message);
                    }
                    Debug.Print("Stack Trace: " + ex.StackTrace);
                }
            }
        }
    }
}
