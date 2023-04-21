using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Newtonsoft.Json;

namespace EmployeeCrudApplication.Models
{
    public class Employee
    {
        //Fields
        private int id;
        private string name;
        private string email;
        private string gender;
        private string status;

        //Properties - Validations
        [JsonProperty("id")]
        [DisplayName("Emp ID")]
        [Required(ErrorMessage = "Employee Id is required")]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        [JsonProperty("name")]
        [DisplayName("Emp Name")]
        [Required(ErrorMessage = "Employee name is required")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [JsonProperty("email")]
        [DisplayName("Emp Email")]
        [Required(ErrorMessage = "Employee  email is required")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$")]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        [JsonProperty("gender")]
        [DisplayName("Emp Gender")]
        [Required(ErrorMessage = "Employee gender is required")]
        public string Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        [JsonProperty("status")]
        [DisplayName("Emp Status")]
        [Required(ErrorMessage = "Employee Status is required")]
        public string Status
        {
            get { return status; }
            set { status = value; }
        }

    }

}

