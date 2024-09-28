using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Ikea.BLL.Models.Departments
{
    public class DepartmentDetailsDto
    {
        public int Id { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int LastModifiedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        [Display(Name = "Creation Date")]
        public DateOnly CreationDate { get; set; }
        [Display(Name = "Phone Number")]
        [Phone]
        //[DataType(DateType.PhoneNumber)]
        public string? PhoneNumber { get; set; }

        //public int? DepartmentId { get; set; }

    }
}
