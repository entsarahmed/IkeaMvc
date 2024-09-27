﻿using LinkDev.Ikea.DAL.Entities.Employees;
using LinkDev.Ikea.DAL.Persistance.Data;
using LinkDev.Ikea.DAL.Persistance.Repositories._Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Ikea.DAL.Persistance.Repositories.Employees
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
       public EmployeeRepository(ApplicationDbContext dbContext):base(dbContext) 
        {
        }  
    }
}
