using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BJMT.RsspII4net.ITest;
using BJMT.Log;

namespace BJMT.RsspII4net.ITest
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.ThreadExit += new EventHandler(Application_ThreadExit);

            LogManager.Initialize("log4net.config.xml", 30);
            
            Application.Run(new GuideForm());

            LogManager.Shutdown();
        }

        static void Application_ThreadExit(object sender, EventArgs e)
        {
            //System.Environment.Exit(0);
        }
    }
}
