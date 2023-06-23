using AlberEOL.UI;
using System;
using System.Threading;
using System.Windows.Forms;

namespace AlberEOL
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        [STAThread]
        static void Main()
        {
            bool createdNew;
            Mutex mutex = new Mutex(false, "AppMutex", out createdNew);

            if (!createdNew)
            {
                MessageBox.Show("A program egy példánya már fut!");
                Application.Exit();

                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainScreen());
            mutex.Dispose();
            Application.Exit();
            Environment.Exit(0);
        }
    }
}
