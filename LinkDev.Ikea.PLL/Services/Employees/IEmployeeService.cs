using LinkDev.Ikea.BLL.Models.Departments;
using LinkDev.Ikea.BLL.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Ikea.BLL.Services.Employees
{
    public interface IEmployeeService
    {
       Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(string Search);
        Task<EmployeeDetailsDto?> GetEmployeeByIdAsync(int id);

        Task<int> CreatedEmployeeAsync(CreatedEmployeeDto employeeDto);
       Task<int> UpdatedEmployeeAsync(UpdatedEmployeeDto employeeDto);
       Task<bool> DeleteEmployeeAsync(int id);
    }
}
