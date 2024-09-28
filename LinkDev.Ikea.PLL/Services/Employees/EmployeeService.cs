using LinkDev.Ikea.BLL.Models.Employees;
using LinkDev.Ikea.DAL.Entities.Employees;
using LinkDev.Ikea.DAL.Persistance.Repositories.Employees;
using LinkDev.Ikea.DAL.Persistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Ikea.BLL.Services.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork) //Ask CLR for Creating Object from class Implementing "IUnitOfWork"
        {
            _unitOfWork=unitOfWork;

        }



        public IEnumerable<EmployeeDto> GetEmployees(string search)
        {
            var query = _unitOfWork.EmployeeRepository.GetIQueryable().Where(E => !E.IsDeleted && (string.IsNullOrEmpty(search) || E.Name.ToLower().Contains(search.ToLower()))).Include(E => E.Departments).Select(employee => new EmployeeDto()
            {
                Id=employee.Id,
                Name=employee.Name,
                Age=employee.Age,
                IsActive=employee.IsActive,
                Email=employee.Email,
                Salary=employee.Salary,
                Gender =employee.Gender.ToString(),
                EmployeeType=employee.EmployeeType.ToString(),
                Department = employee.Departments != null ? employee.Departments.Name : string.Empty,
                //DepartmentId=employee.Departments.Id,
                


            }).ToList();

            return query;

        }
        



        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.Get(id);

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
                    DepartmentId= employee.DepartmentId,
                    PhoneNumber=employee.PhoneNumber,

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
                DepartmentId=employeeDto.DepartmentId,

                CreatedBy=1,
                LastModifiedBy=1,
                CreatedOn= DateTime.UtcNow,

            };

            //Add 
            //Update
            //Delete

            _unitOfWork.EmployeeRepository.Add(employee);
          return  _unitOfWork.Complete();

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
                DepartmentId=employeeDto.DepartmentId,

                CreatedBy=1,
                LastModifiedBy=1,
                CreatedOn= DateTime.UtcNow,

            };
             _unitOfWork.EmployeeRepository.Update(employee);
            return _unitOfWork.Complete();
        }

        public bool DeleteEmployee(int id)
        {
            var employeeRepo = _unitOfWork.EmployeeRepository;

            var employee = employeeRepo.Get(id);
            if (employee is { })
                 employeeRepo.Delete(employee);
            return _unitOfWork.Complete() > 0;

        }

       

       
    }
}
