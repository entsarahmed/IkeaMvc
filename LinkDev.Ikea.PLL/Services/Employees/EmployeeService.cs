using LinkDev.Ikea.BLL.Common.Services.Attachments;
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
        private readonly IAttachmentService _attachmentService;

        public EmployeeService(IUnitOfWork unitOfWork, IAttachmentService attachmentService) //Ask CLR for Creating Object from class Implementing "IUnitOfWork"
        {
            _unitOfWork=unitOfWork;

            _attachmentService=attachmentService;
        }



        public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(string Search)
        {
            var query = await _unitOfWork.EmployeeRepository
                .GetIQueryable()
                .Where(E => !E.IsDeleted && (string.IsNullOrEmpty(Search) || E.Name.ToLower().Contains(Search.ToLower())))
                .Include(E => E.Departments)
                .Select(employee => new EmployeeDto()
            {
                Id=employee.Id,
                Name=employee.Name,
                Age=employee.Age,
                IsActive=employee.IsActive,
                Email=employee.Email,
                Salary=employee.Salary,
                Gender =nameof(employee.Gender),
                EmployeeType=nameof(employee.EmployeeType),
                Department = employee.Departments != null ? employee.Departments.Name : string.Empty,
                DepartmentId=employee.DepartmentId
            }).ToListAsync();

            return query;

        }
        



        public async Task<EmployeeDetailsDto?> GetEmployeeByIdAsync(int id)
        {
            var employee = await _unitOfWork.EmployeeRepository.GetAsync(id);

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
                    HiringDate=employee.HiringDate,
                    Department =employee.Departments.Name, 
                    Image=employee.Image

                };
            return null;

        }


        public async Task<int> CreatedEmployeeAsync(CreatedEmployeeDto employeeDto)
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

            if(employeeDto.Image is not null) 
         employee.Image= await  _attachmentService.UploadFileAsync(employeeDto.Image, "Files");


            //Add 
            //Update
            //Delete

            _unitOfWork.EmployeeRepository.Add(employee);
          return await  _unitOfWork.CompleteAsync();

        }

        public async Task<int> UpdatedEmployeeAsync(UpdatedEmployeeDto employeeDto)
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
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employeeRepo = _unitOfWork.EmployeeRepository;

            var employee = await employeeRepo.GetAsync(id);
            if (employee is { })
                 employeeRepo.Delete(employee);
            return await _unitOfWork.CompleteAsync() > 0;

        }

       

       
    }
}
