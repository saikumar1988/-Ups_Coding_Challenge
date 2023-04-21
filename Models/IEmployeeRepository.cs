using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeCrudApplication.Models
{
    public interface IEmployeeRepository
    {

        void Add(Employee empModel);
        void Edit(Employee empModel);
        void Delete(int id);
        Task<IEnumerable<Employee>> GetEmployees();
        Task<IEnumerable<Employee>> GetEmployeesByValue(string value);

    }
}
