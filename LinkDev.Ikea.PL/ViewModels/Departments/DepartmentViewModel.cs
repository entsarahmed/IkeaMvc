using System.ComponentModel.DataAnnotations;

namespace LinkDev.Ikea.PL.ViewModels.Departments
{
    public class DepartmentViewModel
    {
        public int? Id { get; set; } 
        [Required(ErrorMessage ="Code is Required Ya Prince!")]
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } =null!;

        [Display(Name = "Creation Date")]
        public DateOnly CreationDate { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
