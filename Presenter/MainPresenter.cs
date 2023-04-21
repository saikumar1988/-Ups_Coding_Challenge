using EmployeeCrudApplication.Models;
using EmployeeCrudApplication.Repositories;
using EmployeeCrudApplication.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCrudApplication.Presenter
{
    public class MainPresenter
    {
        private IMainView mainView;
        private readonly string url;


        public MainPresenter(IMainView mainView, string url)
        {
            this.mainView = mainView;
            this.mainView.ShowEmployeeView += ShowEmployeeView;
            this.url = url;
        }

        private void ShowEmployeeView(object sender, EventArgs e)
        {
            IEmployeeView view = EmployeeView.GetInstace((MainView)mainView);
            IEmployeeRepository repository = new EmployeeRepository(url);
            new EmployeePresenter(view,repository);
        }
    }

}

