using LinkDev.Ikea.BLL.Models.Departments;
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
        private readonly ILogger<DepartmentController> _logger;
        private readonly IWebHostEnvironment _environment;

        

        public DepartmentController(IDepartmentService departmentService, IWebHostEnvironment environment, ILogger<DepartmentController> logger)
        {
            _departmentService=departmentService;
            _environment=environment;
            _logger=logger;

        }

        [HttpGet] //Get: /Department/Index
        public IActionResult Index()
        {
            var departments = _departmentService.GetDepartments();
            return View(departments);
        }
        [HttpGet] //Get: /Department/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreatedDepartmentDto department)
        {
            if (!ModelState.IsValid)
                return View(department);
            var message = string.Empty;
            try
            {
                var result = _departmentService.createdDepartment(department);
                if (result > 0)
                    return RedirectToAction("Index");
                else
                {
                    message= "Department is not Created";
                    ModelState.AddModelError(string.Empty, message);
                    return View(department);
                }
            }
            catch (Exception ex)
             {
               //1. Log Exception 
               _logger.LogError(ex, ex.Message);
                //2. Set Message
                if (_environment.IsDevelopment())
                {
                    message=ex.Message;
                    return View(department);
                }
                else
                {
                    message="Department is not Created";
                    return View("Error", message);
                }
            }
            }

    }
}
