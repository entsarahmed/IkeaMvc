using LinkDev.Ikea.BLL.Models.Departments;
using LinkDev.Ikea.DAL.Entities.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Ikea.BLL.Services.Departments
{
    public interface IDepartmentService
    {
       Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync();
        Task<DepartmentDetailsDto?> GetDepartmentByIdAsync(int id);

        Task<int> createdDepartmentAsync(CreatedDepartmentDto departmentDto);
       Task<int> UpdatedDepartmentAsync(UpdatedDepartmentDto departmentDto);
        Task<bool> DeleteDepartmentAsync(int id);
             

    }
}
