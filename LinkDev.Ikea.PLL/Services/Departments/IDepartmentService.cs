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
        IEnumerable<DepartmentToReturnDto> GetDepartments();
        DepartmentDetailsReturnDto? GetDepartmentById(int id);

        int createdDepartment(CreatedDepartmentDto department);
        int UpdatedDepartment(UpdatedDepartmentDto department);
        bool DeleteDepartment(int id);
             

    }
}
