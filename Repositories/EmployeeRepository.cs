using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeCrudApplication.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System;
using System.Text;


namespace EmployeeCrudApplication.Repositories
{
    public class EmployeeRepository : BaseRepository, IEmployeeRepository
    {
     
        //Constructor
        public EmployeeRepository(string url)
        {
            this.url = url;
        }

      

        #region Methods
        public  void Add(Employee employee)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "0bf7fb56e6a27cbcadc402fc2fce8e3aa9ac2b40d4190698eb4e8df9284e2023");
                    var serializedProduct = JsonConvert.SerializeObject(employee);
                    var content = new StringContent(serializedProduct, Encoding.UTF8, "application/json");
                    var result =   client.PostAsync(url, content).Result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occurred while Adding employee details" + ex.Message);
            }
        }
        public  void Delete(int id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "0bf7fb56e6a27cbcadc402fc2fce8e3aa9ac2b40d4190698eb4e8df9284e2023");
                    var result = client.DeleteAsync(String.Format("{0}/{1}", url, id)).Result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occurred while deleting employee details" + ex.Message);
            }
        }
        public  void Edit(Employee employee)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "0bf7fb56e6a27cbcadc402fc2fce8e3aa9ac2b40d4190698eb4e8df9284e2023");
                    var serializedProduct = JsonConvert.SerializeObject(employee).ToString();
                    var content = new StringContent(serializedProduct, Encoding.UTF8, "application/json");
                    var result = client.PutAsync(String.Format("{0}/{1}", url, employee.Id), content).Result;
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Exception occurred while updating employee details" + ex.Message);
            }
        }
        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            var empList = new List<Employee>();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "0bf7fb56e6a27cbcadc402fc2fce8e3aa9ac2b40d4190698eb4e8df9284e2023");
                try
                {
                    using (var response = await client.GetAsync(url))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var employeeJsonString = await response.Content.ReadAsStringAsync();

                            empList = JsonConvert.DeserializeObject<Employee[]>(employeeJsonString).ToList();

                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception occurred while getting employee details" + ex.Message);
                }

                return empList;
            }

        }
        public async Task<IEnumerable<Employee>> GetEmployeesByValue(string value)
        {
            var empList = new List<Employee>();
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "0bf7fb56e6a27cbcadc402fc2fce8e3aa9ac2b40d4190698eb4e8df9284e2023");
                    var response = await client.GetAsync(url+"?name="+value);
                    if (response.IsSuccessStatusCode)
                    {
                        var employeeJsonString = await response.Content.ReadAsStringAsync();

                        empList = JsonConvert.DeserializeObject<Employee[]>(employeeJsonString).ToList();

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return empList;
        }

        #endregion
    }
}
