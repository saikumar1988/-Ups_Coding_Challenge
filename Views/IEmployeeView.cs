using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeCrudApplication.Views
{
    public interface IEmployeeView
    {
        //Properties - Fields
        int EmpId { get; set; }
        string EmpName { get; set; }
        string EmpEmail { get; set; }
        string EmpGender { get; set; }
        string EmpStatus { get; set; }
        string SearchValue { get; set; }
        bool IsEdit { get; set; }
        bool IsSuccessful { get; set; }
        string Message { get; set; }

        //Events
        event EventHandler SearchEvent;
        event EventHandler AddNewEvent;
        event EventHandler EditEvent;
        event EventHandler DeleteEvent;
        event EventHandler SaveEvent;
        event EventHandler CancelEvent;

        //Methods
        void SetEmployeeListBindingSource(BindingSource empList);
        void Show();

    }
}
