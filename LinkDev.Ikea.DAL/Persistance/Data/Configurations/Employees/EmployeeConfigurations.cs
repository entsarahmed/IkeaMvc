using LinkDev.Ikea.DAL.Common.Enums;
using LinkDev.Ikea.DAL.Entities.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Ikea.DAL.Persistance.Data.Configurations.Employees
{
    internal class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E => E.Name).HasColumnType("varchar(50)").IsRequired();
            builder.Property(E => E.Address).HasColumnType("varchar(30)");

            builder.Property(E => E.Salary).HasColumnType("decimal(8,2)");

            builder.Property(E => E.Gender)
                .HasConversion(

                (gender) => gender.ToString(),
                (gender) => (Gender)Enum.Parse(typeof(Gender), gender)
                );


            builder.Property(E => E.EmployeeType)
                .HasConversion(

                (type) => type.ToString(),
                (type) => (EmployeeType)Enum.Parse(typeof(EmployeeType), type)
                );
            builder.Property(E => E.LastModifiedOn).HasComputedColumnSql("GETDATE()");

           // builder.Property(E => E.CreatedOn).HasComputedColumnSql("GETUTCDATE()");

    
    

            builder.HasOne(e => e.Departments)
                  .WithMany(d => d.Employees)
                  .HasForeignKey(e => e.DepartmentId);
        }
    }
}
