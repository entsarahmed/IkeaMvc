﻿using LinkDev.Ikea.DAL.Persistance.Repositories.Departments;
using LinkDev.Ikea.DAL.Persistance.Repositories.Employees;

namespace LinkDev.Ikea.DAL.Persistance.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {
        public IEmployeeRepository EmployeeRepository { get; }

        public IDepartmentRepository DepartmentRepository { get;}

        int Complete();
    }
}