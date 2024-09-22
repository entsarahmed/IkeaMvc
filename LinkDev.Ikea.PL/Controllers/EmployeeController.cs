using LinkDev.Ikea.BLL.Models.Departments;
using LinkDev.Ikea.BLL.Models.Employees;
using LinkDev.Ikea.BLL.Services.Departments;
using LinkDev.Ikea.BLL.Services.Employees;
using LinkDev.Ikea.DAL.Entities.Departments;
using LinkDev.Ikea.PL.ViewModels.Departments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace LinkDev.Ikea.PL.Controllers
{
    public class EmployeeController : Controller
    {

        #region Services
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IWebHostEnvironment _environment;



        public EmployeeController(IEmployeeService employeeService, IWebHostEnvironment environment, ILogger<EmployeeController> logger)
        {
            _employeeService=employeeService;
            _environment=environment;
            _logger=logger;

        }
        #endregion



        #region Index
        [HttpGet] 
        public IActionResult Index()
        {
            var departments = _employeeService.GetEmployees();
            return View(departments);
        }

        #endregion


        #region Details

        [HttpGet] 
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var employee = _employeeService.GetEmployeeById(id.Value);
            if (employee is null)
                return NotFound();
            return View(employee);

        }

        #endregion



        #region Create
        [HttpGet] 
        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreatedEmployeeDto  employee)
        {
            if (!ModelState.IsValid)
                return View(employee);
            var message = string.Empty;
            try
            {
                var result = _employeeService.createdEmployee(employee);
                if (result > 0)
                    return RedirectToAction("Index");
                else
                {
                    message= "Employee is not Created";
                    ModelState.AddModelError(string.Empty, message);
                    return View(employee);
                }
            }
            catch (Exception ex)
            {
                //1. Log Exception 
                _logger.LogError(ex, ex.Message);
                //2. Set Message
                message=_environment.IsDevelopment() ? ex.Message : "an error has occured during Creation the employee :(";



            }
            ModelState.AddModelError(string.Empty, message);
            return View(employee);
        }

        #endregion





        #region Update

        [HttpGet] 

        public IActionResult Edit(int? id)
        {
            if (id is null)
                return BadRequest();//400

            var employee = _employeeService.GetEmployeeById(id.Value);

            if (employee is null)
                return NotFound();//404

            return View(new UpdatedEmployeeDto()
            {
                Name = employee.Name,
                Address= employee.Address,
                EmployeeType=employee.EmployeeType,
                Gender=employee.Gender,
                Email=employee.Email,
                Age=employee.Age,
                HiringDate=employee.HiringDate,
                IsActive=employee.IsActive,
                PhoneNumber=employee.PhoneNumber,
                Salary=employee.Salary,


             
            }
                );


        }

        [HttpPost] //Post
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, UpdatedEmployeeDto employee)
        {
            if (!ModelState.IsValid)//Sever-Side Validation
                return View(employee);

            var Message = string.Empty;

            try
            {
                

                var updated = _employeeService.UpdatedEmployee(employee) > 0;
                if (updated)
                    return RedirectToAction("Index");
                Message= "an error has occured during Updating the employee :(";
            }
            catch (Exception ex)
            {
                //1. Log Exception 
                _logger.LogError(ex, ex.Message);


                //2. Set Message
                Message=_environment.IsDevelopment() ? ex.Message : "an error has occured during Updating the employee :(";



            }
            ModelState.AddModelError(string.Empty, Message);
            return View(employee);
        }

        #endregion


        #region Delete

        [HttpGet] 
        public IActionResult Delete(int? id)
        {
            if (id is null)
                return BadRequest();

            var department = _employeeService.GetEmployeeById(id.Value);

            if (department is null)
                return NotFound();

            return View(department);

        }


        [HttpPost] //Post
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var message = string.Empty;

            try
            {

                var deleted = _employeeService.DeleteEmployee(id);
                if (deleted)
                    return RedirectToAction("Index");
                message= "an error has occured during deleting the employee :(";

            }
            catch (Exception ex)
            {

                //1. Log Exception 
                _logger.LogError(ex, ex.Message);


                //2. Set Message
                message=_environment.IsDevelopment() ? ex.Message : "an error has occured during deleting the employee :(";
            }
            //ModelState.AddModelError(string.Empty, message);
            return RedirectToAction(nameof(Index));

        } 
        #endregion





    }
}
