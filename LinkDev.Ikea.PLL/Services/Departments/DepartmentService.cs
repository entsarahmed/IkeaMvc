using LinkDev.Ikea.BLL.Models.Departments;
using LinkDev.Ikea.DAL.Entities.Departments;
using LinkDev.Ikea.DAL.Persistance.Repositories.Departments;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Ikea.BLL.Services.Departments
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository=departmentRepository;
        }

        public IEnumerable<DepartmentDto> GetDepartments()
        {
            var departments = _departmentRepository.GetAllAsIQueryable().Select(D => new DepartmentDto
            {
                Id = D.Id,
                Name = D.Name,
                Code = D.Code,
                Description = D.Description,
                CreationDate = D.CreationDate,

            }).AsNoTracking().ToList();

            return departments;
            

        
        }
        public EmployeeDetailsDto? GetDepartmentById(int id)
        {
            var department = _departmentRepository.Get(id);
            if (department is not null)
                return new EmployeeDetailsDto()
                {
                    Id=department.Id,
                    Name=department.Name,
                    Code=department.Code,
                    Description=department.Description,
                    CreationDate=department.CreationDate,
                    CreatedBy=department.CreatedBy,
                    CreatedOn=department.CreatedOn,
                    LastModifiedBy=department.LastModifiedBy,
                    LastModifiedOn=department.LastModifiedOn,

                };
            return null;
        }
        public int createdDepartment(CreatedEmployeeDto departmentDto)
        {
            var CreatedDepartment = new Department()
            {
                Code = departmentDto.Code,
                Name = departmentDto.Name,
                CreationDate = departmentDto.CreationDate,
                CreatedBy=1,
                LastModifiedBy = 1,
                LastModifiedOn=DateTime.UtcNow,
            };

            return _departmentRepository.Add(CreatedDepartment);
        }

        public int UpdatedDepartment(UpdatedEmployeeDto departmentDto)
        {
            var department = new Department()
            {
                Id = departmentDto.Id,
                Code= departmentDto.Code,
               Name = departmentDto.Name,
               Description = departmentDto.Description,
               CreationDate=departmentDto.CreationDate,
               LastModifiedBy=1,
               LastModifiedOn=DateTime.UtcNow,
            };   
            return _departmentRepository.Update(department);
        }
        public bool DeleteDepartment(int id)
        {
            var department = _departmentRepository.Get(id);

            if (department is { })
                return _departmentRepository.Delete(department) > 0;
            return false;
        }

    }
}
