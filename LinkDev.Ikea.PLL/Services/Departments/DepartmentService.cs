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

        public DepartmentService(IUnitOfWork unitOfWork) //Ask CLR for Creating Object from Class Implement IUnitOfWork
        {
            _unitOfWork=unitOfWork;
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync()
        {
            var DepartmentRepo = _unitOfWork.DepartmentRepository;
            var departments = await DepartmentRepo
                .GetIQueryable()
                .Where(D => !D.IsDeleted)
                .Select(D => new DepartmentDto
            {
                Id = D.Id,
                Name = D.Name,
                Code = D.Code,
               // Description = D.Description ?? "No Description",
                CreationDate = D.CreationDate ?? DateOnly.MinValue // Handle potential NULLs

            }).AsNoTracking().ToListAsync();

            return departments;
            

        
        }
        public async Task<DepartmentDetailsDto?> GetDepartmentByIdAsync(int id)
        {
            var department = await _unitOfWork.DepartmentRepository.GetAsync(id);
            if (department is not null)
                return new DepartmentDetailsDto()
                {
                    Id=department.Id,
                    Name=department.Name,
                    Code=department.Code,
                    Description = department.Description ?? "No Description Available", // Handle potential NULLs
                    CreationDate = department.CreationDate ?? DateOnly.MinValue, // Handle potential NULLs
                    CreatedBy=department.CreatedBy,
                    CreatedOn = department.CreatedOn.GetValueOrDefault(DateTime.Now),
                    LastModifiedBy=department.LastModifiedBy,
                    LastModifiedOn=department.LastModifiedOn.GetValueOrDefault(DateTime.Now),

                };
            return null;
        }


        public async Task<int> createdDepartmentAsync(CreatedDepartmentDto departmentDto)
        {
            var CreatedDepartment =  new Department()
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

            return await _unitOfWork.CompleteAsync();

        }

        public async Task<int> UpdatedDepartmentAsync(UpdatedDepartmentDto departmentDto)
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
               CreatedOn = departmentDto.CreatedOn
            };   
            _unitOfWork.DepartmentRepository.Update(department);
            return await _unitOfWork.CompleteAsync();
        }
        public  async Task<bool> DeleteDepartmentAsync(int id)
        {
            var DepartmentRepo=  _unitOfWork.DepartmentRepository;
            var department = await DepartmentRepo.GetAsync(id);

            if (department is { })
                DepartmentRepo.Delete(department);
            return await _unitOfWork.CompleteAsync() > 0;
        }

    }
}
