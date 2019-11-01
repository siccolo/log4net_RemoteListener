using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using Unity;

namespace log4netTester
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
            //Application.Run(new Form1());
            Run(args).GetAwaiter().GetResult();
        }

        private static async Task Run(string[] args)
        {
            using (var container = new Unity.UnityContainer().EnableDiagnostic())
            {

                container.RegisterSingleton<ILogger, Logger>();
                
                Application.Run(container.Resolve<Form1>());
            }
        }
    }
}
