using EmployeeCrudApplication.Presenter;
using EmployeeCrudApplication.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeCrudApplication
{
    static class Program
    {
        // <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string url = ConfigurationManager.AppSettings["Endpoint"] ;
            IMainView view = new MainView();
            new MainPresenter(view, url);
            Application.Run((Form)view);
        }
    }
}
