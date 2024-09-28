using LinkDev.Ikea.BLL.Models.Departments;
using LinkDev.Ikea.BLL.Models.Employees;
using LinkDev.Ikea.BLL.Services.Departments;
using LinkDev.Ikea.BLL.Services.Employees;
using LinkDev.Ikea.DAL.Entities.Departments;
using LinkDev.Ikea.PL.ViewModels.Departments;
using LinkDev.Ikea.PL.ViewModels.Employees;
using Microsoft.AspNetCore.Http.HttpResults;
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
        private readonly IDepartmentService _departmentService;

      

        public EmployeeController(IEmployeeService employeeService, IDepartmentService departmentService, IWebHostEnvironment environment, ILogger<EmployeeController> logger)
        {
            _employeeService=employeeService;
            _environment=environment;
            _logger=logger;
            _departmentService=departmentService;


        }
        #endregion



        #region Index
        [HttpGet]
        public IActionResult Index(string search)
        {
            var employees = _employeeService.GetEmployees(search);
            if (!string.IsNullOrEmpty(search))
                return PartialView("Partial/_EmployeeListPartial", employees);
            return View(employees);
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
        public IActionResult Create(/*CreatedEmployeeDto*/  EmployeeViewModel employeeVM)
        {

            if (!ModelState.IsValid)
                return View(employeeVM);
            var message = string.Empty;
            try
            {
                var createdEmployee =
                    new CreatedEmployeeDto()
                    {
                        Name = employeeVM.Name,
                        Address= employeeVM.Address,
                        EmployeeType=employeeVM.EmployeeType,
                        Gender=employeeVM.Gender,
                        Email=employeeVM.Email,
                        Age=employeeVM.Age,
                        HiringDate=employeeVM.HiringDate,
                        IsActive=employeeVM.IsActive,
                        PhoneNumber=employeeVM.PhoneNumber,
                        Salary=employeeVM.Salary,
                        DepartmentId = employeeVM.DepartmentId
                        
                   



                    };
                var result = _employeeService.createdEmployee(createdEmployee);

                


                //if (result > 0)
                //    return RedirectToAction("Index");
                //else
                //{
                //    message= "Employee is not Created";
                //    ModelState.AddModelError(string.Empty, message);
                //    return View(employeeVM);
                //}

                if (result >0)
                    TempData["Message"] = "Employee is Created";
                else

                    TempData["Message"] = "Employee is not Created";
                return RedirectToAction(nameof(Index));

            }

            catch (Exception ex)
            {
                //1. Log Exception 
                _logger.LogError(ex, ex.Message);
                //2. Set Message
                message=_environment.IsDevelopment() ? ex.Message : "an error has occured during Creation the employee :(";
            }

           ModelState.AddModelError(string.Empty, message);
            return View(employeeVM);
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

            return View(new EmployeeViewModel()
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
        public IActionResult Edit([FromRoute] int id, /*UpdatedEmployeeDto*/ EmployeeViewModel employee)
        {
            if (!ModelState.IsValid)//Sever-Side Validation
                return View(employee);

            var Message = string.Empty;

            try
            {
                var result = new UpdatedEmployeeDto() {
                    Id=id,
                    DepartmentId = employee.DepartmentId,
                HiringDate= employee.HiringDate,
                Salary= employee.Salary,
                Address= employee.Address,
                EmployeeType=employee.EmployeeType,
                Gender=employee.Gender,
                Email=employee.Email,
                Age=employee.Age,
                PhoneNumber = employee.PhoneNumber,
                IsActive = employee.IsActive,
                Name=employee.Name,
                

                
                };

                var updated = _employeeService.UpdatedEmployee(result) > 0;
                //if (updated)

                //    return RedirectToAction("Index");
                //Message= "an error has occured during Updating the employee :(";

                if (updated)
                    TempData["Message"] = "Employee is Updated";
                else

                    TempData["Message"] = "Employee is not Updated";
                return RedirectToAction(nameof(Index));

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

            var employee = _employeeService.GetEmployeeById(id.Value);

            if (employee is null)
                return NotFound();

            return View(employee);

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
           // ModelState.AddModelError(string.Empty, message);
            return RedirectToAction(nameof(Index));

        } 
        #endregion





    }
}
