using System;
using System.IO;

namespace AlberEOL.CustomClasses
{
    public static class Logger
    {
        private static object locking { get; set; }

        static Logger()
        {
            locking = new object();
        }

        /// <summary>
        /// Kivételek logolására
        /// </summary>
        /// <param name="message">Hibaüzenet</param>
        /// <param name="code">Hibakód, egyedi legyen</param>
        public static void WriteExceptionLog(string message, string code)
        {

            string ToFile = string.Format("{0:yyyy-MM-dd hh:mm:ss}", DateTime.Now) + " - " + code + " - " + message + Environment.NewLine;

            string dir = Directory.GetCurrentDirectory() + "/" + "LOG";

            string filename = dir + "/" + "ExceptionLOG_" + string.Format("{0:yyyy-MM-dd}.log", DateTime.Now);

            lock (locking)
            {
                try
                {
                    Directory.CreateDirectory(dir);
                    File.AppendAllText(filename, ToFile);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public static void WriteGeneralLog(string message, string code)
        {
            string ToFile = string.Format("{0:yyyy-MM-dd hh:mm:ss}", DateTime.Now) + " - " + code + " - " + message + Environment.NewLine;

            string dir = Directory.GetCurrentDirectory() + "/" + "LOG";

            string filename = dir + "/" + "GeneralLOG_" + string.Format("{0:yyyy-MM-dd}.log", DateTime.Now);

            lock (locking)
            {
                try
                {
                    Directory.CreateDirectory(dir);
                    File.AppendAllText(filename, ToFile);
                }
                catch (Exception ex)
                {
                    WriteExceptionLog(ex.Message, "GeneralLOGerror");
                }
            }
        }

        public static void WriteErrorLog(string message, string code, string ProductID)
        {
            string ToFile = string.Format("{0:yyyy-MM-dd hh:mm:ss}", DateTime.Now) + " - " + code + " - " + message + Environment.NewLine;

            string dir = Directory.GetCurrentDirectory() + "/" + "LOG";

            string filename = dir + "/" + "ErrorLOG_" + string.Format("{0:yyyy-MM-dd}.log", DateTime.Now);

            lock (locking)
            {
                try
                {
                    Directory.CreateDirectory(dir);
                    File.AppendAllText(filename, ToFile);
                }
                catch (Exception ex)
                {
                    WriteExceptionLog(ex.Message, "GeneralLOGerror");
                }
            }
        }

        public static void WriteAuthLog(string user, bool success, string message)
        {
            string ToFile = string.Format("{0:yyyy-MM-dd hh:mm:ss}", DateTime.Now) + " - " + user + " - " + (success ? "Sikeres bejelentkezés." : message) + Environment.NewLine;

            string dir = Directory.GetCurrentDirectory() + "/" + "LOG";

            string filename = dir + "/" + "AuthLOG_" + string.Format("{0:yyyy-MM-dd}.log", DateTime.Now);

            lock (locking)
            {
                try
                {
                    Directory.CreateDirectory(dir);
                    File.AppendAllText(filename, ToFile);
                }
                catch (Exception ex)
                {
                    WriteExceptionLog(ex.Message, "AuthLOGerror");
                }
            }
        }
    }
}
