using LinkDev.Ikea.DAL.Entities.Departments;
using LinkDev.Ikea.DAL.Persistance.Data;
using LinkDev.Ikea.DAL.Persistance.Repositories._Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Ikea.DAL.Persistance.Repositories.Departments
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository   
    {
     
        public DepartmentRepository(ApplicationDbContext dbContext):base(dbContext) //Ask CLR for Object from ApplicationDbContext
        {

        }

       
    }
}
