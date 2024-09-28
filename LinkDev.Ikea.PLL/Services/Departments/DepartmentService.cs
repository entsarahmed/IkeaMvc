using LinkDev.Ikea.BLL.Models.Departments;
using LinkDev.Ikea.DAL.Entities.Departments;
using LinkDev.Ikea.DAL.Persistance.Repositories.Departments;
using LinkDev.Ikea.DAL.Persistance.UnitOfWork;
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
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork=unitOfWork;
        }

        public IEnumerable<DepartmentDto> GetDepartments()
        {
            var departments = _unitOfWork.DepartmentRepository.GetIQueryable().Where(D => !D.IsDeleted).Select(D => new DepartmentDto
            {
                Id = D.Id,
                Name = D.Name,
                Code = D.Code,
                Description = D.Description ?? "No Description",
                CreationDate = D.CreationDate ?? DateOnly.MinValue // Handle potential NULLs

            }).AsNoTracking().ToList();

            return departments;
            

        
        }
        public DepartmentDetailsDto? GetDepartmentById(int id)
        {
            var department = _unitOfWork.DepartmentRepository.Get(id);
            if (department is not null)
                return new DepartmentDetailsDto()
                {
                    Id=department.Id,
                    Name=department.Name,
                    Code=department.Code,
                    Description = department.Description ?? "No Description Available", // Handle potential NULLs
                    CreationDate = department.CreationDate ?? DateOnly.MinValue, // Handle potential NULLs
                    CreatedBy=department.CreatedBy,
                    CreatedOn=department.CreatedOn,
                    LastModifiedBy=department.LastModifiedBy,
                    LastModifiedOn=department.LastModifiedOn,

                };
            return null;
        }

        public int createdDepartment(CreatedDepartmentDto departmentDto)
        {
            var CreatedDepartment = new Department()
            {
                Code = departmentDto.Code,
                Name = departmentDto.Name,
                Description= departmentDto.Description,
                CreationDate = departmentDto.CreationDate,
                CreatedBy=1,
                LastModifiedBy = 1,
                LastModifiedOn=DateTime.UtcNow,
            };

             _unitOfWork.DepartmentRepository.Add(CreatedDepartment);
            return _unitOfWork.Complete();

        }

        public int UpdatedDepartment(UpdatedDepartmentDto departmentDto)
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
            _unitOfWork.DepartmentRepository.Update(department);
            return _unitOfWork.Complete();
        }
        public bool DeleteDepartment(int id)
        {
            var DepartmentRepo= _unitOfWork.DepartmentRepository;
            var department = DepartmentRepo.Get(id);

            if (department is { })
                DepartmentRepo.Delete(department);
            return _unitOfWork.Complete() > 0;
        }

    }
}
