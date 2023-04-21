using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeCrudApplication.Views
{
    public partial class EmployeeView : Form, IEmployeeView
    {
       
          //Fields
        private string message;
        private bool isSuccessful;
        private bool isEdit;

        //Constructor
        public EmployeeView()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();
            tabControl1.TabPages.Remove(tabPageEmpDetail);
            btnClose.Click += delegate { this.Close(); };
        }

        private void AssociateAndRaiseViewEvents()
        {
            //Search
            btnSearch.Click += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
            txtSearch.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                    SearchEvent?.Invoke(this, EventArgs.Empty);
            };
            //Add new
            btnAddNew.Click += delegate
            {
                AddNewEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(tabPageEmpList);
                tabControl1.TabPages.Add(tabPageEmpDetail);
                tabPageEmpDetail.Text = "Add New Employee";
            };
            //Edit
            btnEdit.Click += delegate
            {
                EditEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(tabPageEmpList);
                tabControl1.TabPages.Add(tabPageEmpDetail);
                tabPageEmpDetail.Text = "Edit Employee";
            };
            //Save changes
            btnSave.Click += delegate
            {
                SaveEvent?.Invoke(this, EventArgs.Empty);
                if (isSuccessful)
                {
                    tabControl1.TabPages.Remove(tabPageEmpDetail);
                    tabControl1.TabPages.Add(tabPageEmpList);
                }
                MessageBox.Show(Message);
            };
            //Cancel
            btnCancel.Click += delegate
            {
                CancelEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(tabPageEmpDetail);
                tabControl1.TabPages.Add(tabPageEmpList);
            };
            //Delete
            btnDelete.Click += delegate
            {
                var result = MessageBox.Show("Are you sure you want to delete the selected Employee?", "Warning",
                      MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    DeleteEvent?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show(Message);
                }
            };
        }

        //Properties
        public int EmpId
        {
            get { return Convert.ToInt32(txtEmpId.Text); }
            set { txtEmpId.Text = value.ToString(); }
        }

        public string EmpName
        {
            get { return txtEmpName.Text; }
            set { txtEmpName.Text = value; }
        }

        public string EmpEmail
        {
            get { return txtEmpEmail.Text; }
            set { txtEmpEmail.Text = value; }
        }

        public string EmpGender
        {
            get { return txtEmpGender.Text; }
            set { txtEmpGender.Text = value; }
        }
        public string EmpStatus
        {
            get { return txtEmpStatus.Text; }
            set { txtEmpStatus.Text = value; }
        }

        public string SearchValue
        {
            get { return txtSearch.Text; }
            set { txtSearch.Text = value; }
        }

        public bool IsEdit
        {
            get { return isEdit; }
            set { isEdit = value; }
        }

        public bool IsSuccessful
        {
            get { return isSuccessful; }
            set { isSuccessful = value; }
        }

        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        //Events
        public event EventHandler SearchEvent;
        public event EventHandler AddNewEvent;
        public event EventHandler EditEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler SaveEvent;
        public event EventHandler CancelEvent;

        //Methods
        public void SetEmployeeListBindingSource(BindingSource empList)
        {
            dataGridView.DataSource = empList;
        }

        //Singleton pattern (Open a single form instance)
        private static EmployeeView instance;
        public static EmployeeView GetInstace(Form parentContainer)
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new EmployeeView();
                instance.MdiParent = parentContainer;
                instance.FormBorderStyle = FormBorderStyle.None;
                instance.Dock = DockStyle.Fill;
            }
            else
            {
                if (instance.WindowState == FormWindowState.Minimized)
                    instance.WindowState = FormWindowState.Normal;
                instance.BringToFront();
            }
            return instance;
        }
    }
}
