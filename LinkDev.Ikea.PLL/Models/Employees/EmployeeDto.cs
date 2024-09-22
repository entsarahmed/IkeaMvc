using LinkDev.Ikea.DAL.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Ikea.BLL.Models.Employees
{
    public class EmployeeDto
    {
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "Max Length of Name is 50 Chars")]
        [MinLength(5, ErrorMessage = "Min Length of Name is 5 Chars")]

        public string Name { get; set; } = null!;

        [Range(22,30)]
        public int? Age { get; set; }


        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [Display(Name ="Is Active")]
        public bool IsActive { get; set; }

        [EmailAddress]
         [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        public string? Gender { get; set; }

        public string? EmployeeType { get; set; }

        public string Department { get; set; } = null!;


        

    }
}
