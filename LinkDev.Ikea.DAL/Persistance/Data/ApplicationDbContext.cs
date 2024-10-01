using LinkDev.Ikea.DAL.Entities.Departments;
using LinkDev.Ikea.DAL.Entities.Employees;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LinkDev.Ikea.DAL.Persistance.Data
{

    public class ApplicationDbContext : IdentityDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server = .; Database = Ikea_G03; Trusted_Connection = True; TrustServerCertificate = True;");
        //}
      
        
        
        
        
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
     
        public DbSet<Department> Departments { get; set; }

        //Employee
        public  DbSet<Employee> Employees { get; set; }

        public DbSet<IdentityUser> Users { get; set; }

        public DbSet<IdentityRole> Roles { get; set; }

    }
}
