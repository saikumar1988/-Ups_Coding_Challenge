using System;
using EmployeeCrudApplication.Models;
using EmployeeCrudApplication.Views;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace EmployeeCrudApplication.Presenter
{
    public class EmployeePresenter
    {
        //Fields
        private IEmployeeView view;
        private IEmployeeRepository repository;
        private BindingSource empBindingSource;
        private IEnumerable<Employee> empList;

        #region Constructor
        public EmployeePresenter(IEmployeeView view, IEmployeeRepository repository)
        {
            this.empBindingSource = new BindingSource();
            this.view = view;
            this.repository = repository;
            //Subscribe event handler methods to view events
            this.view.SearchEvent += SearchEmployee;
            this.view.AddNewEvent += AddNewEmployee;
            this.view.EditEvent += LoadSelectedEmployeeToEdit;
            this.view.DeleteEvent += DeleteSelectedEmployee;
            this.view.SaveEvent += SaveEmployee;
            this.view.CancelEvent += CancelAction;
            //Set employeees bindind source
            this.view.SetEmployeeListBindingSource(empBindingSource);
            //Load employeees list view
            LoadAllEmployeeList();
            //Show view
            this.view.Show();
        }

        public EmployeePresenter()
        {

        }

       


        #endregion

        #region Methods

        /* Method to get all employees from API*/
        public async void LoadAllEmployeeList()
        {
            empList = await repository.GetEmployees();
            empBindingSource.DataSource = empList;//Set data source.
        }

        /* Method to search employees by value from datagrid */
        public async void SearchEmployee(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.view.SearchValue))
            {
                empList = await repository.GetEmployeesByValue(this.view.SearchValue);
            }
            else
            {
                empList = await repository.GetEmployees();
            }
            empBindingSource.DataSource = empList;
        }

        /* Method to add a new employee to the API */
        public void AddNewEmployee(object sender, EventArgs e)
        {
            view.IsEdit = false;
        }

        /* Method to edit an employee by selecting a particular record from datagrid */
        public void LoadSelectedEmployeeToEdit(object sender, EventArgs e)
        {
            var emp = (Employee)empBindingSource.Current;
            view.EmpId = emp.Id;
            view.EmpName = emp.Name;
            view.EmpEmail = emp.Email;
            view.EmpGender = emp.Gender;
            view.EmpStatus = emp.Status;
            view.IsEdit = true;
        }

        /* Method to save an employee and display it in datagrid */
        public void SaveEmployee(object sender, EventArgs e)
        {
            var model = new Employee();
            model.Id = view.EmpId;
            model.Name = view.EmpName;
            model.Email = view.EmpEmail;
            model.Gender = view.EmpGender;
            model.Status = view.EmpStatus;
            try
            {
                new Common.ModelDataValidation().Validate(model);
                if (view.IsEdit)//Edit model
                {
                    repository.Edit(model);
                    view.Message = "Employee edited successfuly";
                }
                else //Add new model
                {
                    repository.Add(model);
                    view.Message = "Employee added sucessfully";
                }
                view.IsSuccessful = true;
                LoadAllEmployeeList();
                CleanviewFields();
            }
            catch (Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = ex.Message;
            }
        }

        /* Method to delete an employee based on Id column */
        public void DeleteSelectedEmployee(object sender, EventArgs e)
        {
            try
            {
                var emp = (Employee)empBindingSource.Current;
                repository.Delete(emp.Id);
                view.IsSuccessful = true;
                view.Message = "Employee deleted successfully";
                LoadAllEmployeeList();
            }
            catch (Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = "An error ocurred, could not delete employee";
            }
        }
        public void CleanviewFields()
        {
            view.EmpId = 0;
            view.EmpName = "";
            view.EmpEmail = "";
            view.EmpGender = "";
            view.EmpStatus = "";
        }
        public void CancelAction(object sender, EventArgs e)
        {
            CleanviewFields();
        }

        #endregion

    }
}

