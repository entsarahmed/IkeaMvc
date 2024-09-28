using LinkDev.Ikea.DAL.Persistance.Data;
using LinkDev.Ikea.DAL.Persistance.Repositories.Departments;
using LinkDev.Ikea.DAL.Persistance.Repositories.Employees;

namespace LinkDev.Ikea.DAL.Persistance.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public IEmployeeRepository EmployeeRepository =>  new EmployeeRepository(_dbContext); 
        public IDepartmentRepository DepartmentRepository => new DepartmentRepository(_dbContext); 
        public UnitOfWork(ApplicationDbContext dbContext)  //Ask CLR for  Creating Object from class "ApplicationDbContext"  Implicitly 
        {
            _dbContext=dbContext;
        }

        public int Complete()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
