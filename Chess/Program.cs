using System;
using System.Windows.Forms;
using Chess.Forms;

namespace Chess
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

            using (FormMain formMain = new FormMain())
            {
                Application.Run(formMain);
            }
        }
    }
}
