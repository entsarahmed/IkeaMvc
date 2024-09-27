﻿using LinkDev.Ikea.DAL.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace LinkDev.Ikea.PL.ViewModels.Employees
{
    public class EmployeeViewModel
    {

        [MaxLength(50, ErrorMessage = "Max Length of Name is 50 Chars")]
        [MinLength(5, ErrorMessage = "Min Length of Name is 5 Chars")]

        public string Name { get; set; } = null!;

        [Range(22, 30)]
        public int? Age { get; set; }

        [RegularExpression(@"^\d{1,3}-[A-Za-z]+(?:-[A-Za-z]+)*-[A-Za-z]+(?:-[A-Za-z]+)*-[A-Za-z]+(?:-[A-Za-z]+)*$",
            ErrorMessage = "Address must be like 123-Street-City-Country")]
        public string? Address { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        [Phone]
        //[DataType(DateType.PhoneNumber)]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Hiring Date")]
        public DateOnly HiringDate { get; set; }

        public Gender Gender { get; set; }

        [Display(Name = "Employee Type")]

        public EmployeeType EmployeeType { get; set; }


        [Display(Name = "Department")]
        public int? DepartmentId { get; set; }

    }
}