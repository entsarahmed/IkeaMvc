using LinkDev.Ikea.DAL.Entities.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Ikea.DAL.Entities.Departments
{
    public class Department : ModelBase
    {
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        public DateOnly? CreationDate { get; set; }

        //Navigational Property [Many]
        public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();


    }
}
