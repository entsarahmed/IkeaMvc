using LinkDev.Ikea.DAL.Models.Departments;
using LinkDev.Ikea.DAL.Persistance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Ikea.DAL.Persistance.Repositories.Departments
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DepartmentRepository(ApplicationDbContext dbContext)// Ask CLR Create Object from ApplicationDbContext
        {
            _dbContext=dbContext;
        }

        public IEnumerable<Department> GetAll(bool WithAsNoTracking = true)
        {
        if (WithAsNoTracking)
                return _dbContext.Departments.AsNoTracking().ToList();
        return _dbContext.Departments.ToList();
        }

        public Department? GetById(int id)
        {
            return _dbContext.Departments.Find(id);

           // return _dbContext.Find<Department>(id);


        //var department = _dbContext.Departments.Local.FirstOrDefault(D => D.Id == id);   
        //   if (department == null)
        //        department = _dbContext.Departments.FirstOrDefault(D => D.Id == id);    
        //    return department;
        }

        public int Add(Department entity)
        {
            _dbContext.Departments.Add(entity);
            return _dbContext.SaveChanges();
        }
        public int Update(Department entity)
        {
            _dbContext.Departments.Update(entity);
            return _dbContext.SaveChanges();
        }

        public int Delete(Department entity)
        {
            _dbContext.Departments.Remove(entity);
            return _dbContext.SaveChanges();
        }
        
    }
}
