using System;
using System.Windows.Forms;
using Chess.Forms;
using System.ServiceModel.Web;
using System.Web.Services.Description;
using System.ServiceModel.Description;
using System.ServiceModel;

namespace Chess
{
    public static class Program
    {
        public static FormMain formMain;

        //        public static FormMain FormMain { get => formMain; set => formMain = value; }


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
                WebServiceHost host = new WebServiceHost(new ChessService(formMain.DisplayMonogame), new Uri("http://localhost:8000"));
                ServiceEndpoint ep = host.AddServiceEndpoint(typeof(IChessService), new WebHttpBinding(), "");
                ServiceDebugBehavior stp = host.Description.Behaviors.Find<ServiceDebugBehavior>();
                ServiceBehaviorAttribute sba = host.Description.Behaviors.Find<ServiceBehaviorAttribute>();
                sba.InstanceContextMode = InstanceContextMode.Single;
                stp.HttpHelpPageEnabled = false;
                host.Open();

                Application.Run(formMain);

                host.Close();
            }
        }
    }
}
