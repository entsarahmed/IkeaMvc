﻿using LinkDev.Ikea.DAL.Entities.Departments;
using LinkDev.Ikea.DAL.Entities.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Ikea.DAL.Persistance.Data.Configurations.Departments
{
    internal class DepartmentConfigurations : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(D => D.Id).UseIdentityColumn(10,10);

            builder.Property(D => D.Name).HasColumnType("varchar(50)").IsRequired();
            builder.Property(D => D.Code).HasColumnType("varchar(30)").IsRequired();
            //  builder.Property(D => D.LastModifiedOn).HasComputedColumnSql("GETDATE()");
            builder.Property(D => D.CreatedOn).HasDefaultValueSql("GETUTCDATE()");

   builder .Property(e => e.LastModifiedOn)
    .HasComputedColumnSql("GETDATE()", stored: true);


            builder.HasMany(D => D.Employees)
                .WithOne(E => E.Departments)
                .HasForeignKey(E => E.DepartmentId)
               .OnDelete(DeleteBehavior.SetNull);




        }
    }
}
