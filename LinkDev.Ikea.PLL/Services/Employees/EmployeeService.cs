using LinkDev.Ikea.BLL.Models.Employees;
using LinkDev.Ikea.DAL.Entities.Employees;
using LinkDev.Ikea.DAL.Persistance.Repositories.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Ikea.BLL.Services.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository) 
        {
            _employeeRepository=employeeRepository;
        }



        public IEnumerable<EmployeeDto> GetEmployees()
        {
            return _employeeRepository.GetAllAsIQueryable().Select(employee => new EmployeeDto()
            {
                Id=employee.Id,
                Name=employee.Name,
                Age=employee.Age,
                IsActive=employee.IsActive,
                Email=employee.Email,
                Salary=employee.Salary,
                Gender =employee.Gender.ToString(),
                EmployeeType=employee.EmployeeType.ToString(),


            });

        }
        var Emp = _employeeRepostrie.GetAllAsIQueryable().Where(x => !x.IsDeleted).Select(employee => new EmployeeDto

        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var employee = _employeeRepository.Get(id);

            if (employee is { })

                return new EmployeeDetailsDto()
                {

                    Id=employee.Id,
                    Name=employee.Name,
                    Age=employee.Age,
                    Address=employee.Address,
                    IsActive=employee.IsActive,
                    Email=employee.Email,
                    Salary=employee.Salary,
                    Gender = employee.Gender,
                    EmployeeType= employee.EmployeeType,

                };
            return null;

        }

        public int createdEmployee(CreatedEmployeeDto employeeDto)
        {
            var employee = new Employee()
            {
                Name=employeeDto.Name,
                Age=employeeDto.Age,
                Address=employeeDto.Address,
                IsActive=employeeDto.IsActive,
                Email=employeeDto.Email,
                Salary=employeeDto.Salary,
                PhoneNumber=employeeDto.PhoneNumber,
                HiringDate=employeeDto.HiringDate,
                Gender =employeeDto.Gender,
                EmployeeType=employeeDto.EmployeeType,
                CreatedBy=1,
                LastModifiedBy=1,
                CreatedOn= DateTime.UtcNow,

            };

            return _employeeRepository.Add(employee);
        }

        public int UpdatedEmployee(UpdatedEmployeeDto employeeDto)
        {
            var employee = new Employee()
            {
                Id=employeeDto.Id,
                Name=employeeDto.Name,
                Age=employeeDto.Age,
                Address=employeeDto.Address,
                IsActive=employeeDto.IsActive,
                Email=employeeDto.Email,
                Salary=employeeDto.Salary,
                PhoneNumber=employeeDto.PhoneNumber,
                HiringDate=employeeDto.HiringDate,
                Gender =employeeDto.Gender,
                EmployeeType=employeeDto.EmployeeType,
                CreatedBy=1,
                LastModifiedBy=1,
                CreatedOn= DateTime.UtcNow,

            };
            return _employeeRepository.Update(employee);
        }

        public bool DeleteEmployee(int id)
        {
            var employee = _employeeRepository.Get(id);
            if (employee is { })
                return _employeeRepository.Delete(employee) > 0;
            return false;

        }

       

       
    }
}
