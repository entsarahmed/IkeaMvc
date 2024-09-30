using LinkDev.Ikea.DAL.Entities.Departments;
using LinkDev.Ikea.DAL.Persistance.Repositories._Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Ikea.DAL.Persistance.Repositories.Departments
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
      // IEnumerable<Department> GetSpecificDepartments();



    }
}
