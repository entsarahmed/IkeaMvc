using LinkDev.Ikea.DAL.Entities;
using LinkDev.Ikea.DAL.Persistance.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace LinkDev.Ikea.DAL.Persistance.Repositories._Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase 
    {
         private readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)// Ask CLR Create Object from ApplicationDbContext
        {
            _dbContext=dbContext;
        }

        public IEnumerable<T> GetAll(bool WithAsNoTracking = true)
        {
        if (WithAsNoTracking)
                return _dbContext.Set<T>().Where(X => !X.IsDeleted).AsNoTracking().ToList();
        return _dbContext.Set<T>().Where(X => !X.IsDeleted).ToList();
        }
        public IQueryable<T> GetIQueryable()
        {
            return _dbContext.Set<T>() ;
        }


        public IEnumerable<T> GetIEnumerable()
        {
            return _dbContext.Set<T>();
        }


        public T? Get(int id)
        {
            return _dbContext.Set<T>().Find(id);

           // return _dbContext.Find<T>(id);


        //var T = _dbContext.Ts.Local.FirstOrDefault(D => D.Id == id);   
        //   if (T == null)
        //        T = _dbContext.Ts.FirstOrDefault(D => D.Id == id);    
        //    return T;
        }

        public int Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            return _dbContext.SaveChanges();
        }
        public int Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            return _dbContext.SaveChanges();
        }

        public int Delete(T entity)
        {
            entity.IsDeleted = true;
            _dbContext.Set<T>().Update(entity);
            return _dbContext.SaveChanges();
        }
    }
}
