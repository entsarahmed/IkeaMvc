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
        IEnumerable<EmployeeDto> GetEmployees();
        EmployeeDetailsDto? GetEmployeeById(int id);

        int createdEmployee(CreatedEmployeeDto employee);
        int UpdatedEmployee(UpdatedEmployeeDto employee);
        bool DeleteEmployee(int id);
    }
}
