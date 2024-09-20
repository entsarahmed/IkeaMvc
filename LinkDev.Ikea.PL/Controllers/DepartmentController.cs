using LinkDev.Ikea.BLL.Services.Departments;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Ikea.PL.Controllers
{
    //1. Inheritance: DepartmentController is a  Controller
    //2. Composition: DepartmentController has a IDepartmentService
    public class DepartmentController : Controller
    {
        //[FromServices]
        //public IDepartmentService DepartmentService { get; } = null!;

        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService=departmentService;
        }

        [HttpGet] //Get: /Department/Index
        public IActionResult Index()
        {
            var departments = _departmentService.GetDepartments();
            return View(departments);
        }
    }
}
