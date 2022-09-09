using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FLP_organizer
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //debug purposes
            //Properties.Settings.Default.projectFolder = "undefined";

            if (!Properties.Settings.Default.projectFolder.Equals("undefined"))
            {
                Application.Run(new Main());
            }
            else
            {
                Application.Run(new Setup());
            }
        }
    }
}
