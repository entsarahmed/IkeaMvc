using AutoMapper;
using LinkDev.Ikea.BLL.Models.Employees;
using LinkDev.Ikea.BLL.Services.Departments;
using LinkDev.Ikea.BLL.Services.Employees;
using LinkDev.Ikea.PL.ViewModels.Employees;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Ikea.PL.Controllers
{
    public class EmployeeController : Controller
    {

        #region Services
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        private readonly IDepartmentService _departmentService;

      

        public EmployeeController(IEmployeeService employeeService, IDepartmentService departmentService, IWebHostEnvironment environment, ILogger<EmployeeController> logger,IMapper mapper)
        {
            _employeeService=employeeService;
            _environment=environment;
            _logger=logger;
            _mapper=mapper;
            _departmentService=departmentService;


        }
        #endregion



        #region Index
        [HttpGet]
        public async Task<IActionResult> Index(string Search)
        {


               var employees =await _employeeService.GetEmployeesAsync(Search);
            return View(employees);
        }


        [HttpGet]
        public async Task<IActionResult> Search(string Search)
        {


            var employees = await _employeeService.GetEmployeesAsync(Search);

            return PartialView("Partial/_EmployeeListPartial",employees);
        }



        //[HttpGet]
        //public IActionResult Search(string search)
        //{
        //    var employees = _employeeService.GetEmployees(search);
        //    if (!string.IsNullOrEmpty(search))
        //       return PartialView("Partial/_EmployeeListPartial", employees);
        //}






        #endregion


        #region Details

        [HttpGet] 
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return BadRequest();
            
            var employee = await _employeeService.GetEmployeeByIdAsync(id.Value);
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
        public async Task<IActionResult> Create(/*CreatedEmployeeDto*/  EmployeeViewModel employeeVM)
        {

            if (!ModelState.IsValid)
                return View(employeeVM);
            var message = string.Empty;
            try
            {
                var CreatedEmployee = _mapper.Map<CreatedEmployeeDto>(employeeVM);   
                //var createdEmployee =
                //    new CreatedEmployeeDto()
                //    {
                //        Name = employeeVM.Name,
                //        Address= employeeVM.Address,
                //        EmployeeType=employeeVM.EmployeeType,
                //        Gender=employeeVM.Gender,
                //        Email=employeeVM.Email,
                //        Age=employeeVM.Age,
                //        HiringDate=employeeVM.HiringDate,
                //        IsActive=employeeVM.IsActive,
                //        PhoneNumber=employeeVM.PhoneNumber,
                //        Salary=employeeVM.Salary,
                //        DepartmentId = employeeVM.DepartmentId
                        
                   



                //    };
                var result =await _employeeService.CreatedEmployeeAsync(CreatedEmployee);

                


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

        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return BadRequest();//400

            var employee =await _employeeService.GetEmployeeByIdAsync(id.Value);

            if (employee is null)
                return NotFound();//404
            var EmployeeVM= _mapper.Map<EmployeeViewModel>(employee);
            return View(EmployeeVM);

            //return View(new EmployeeViewModel()
            //{
            //    Name = employee.Name,
            //    Address= employee.Address,
            //    EmployeeType=employee.EmployeeType,
            //    Gender=employee.Gender,
            //    Email=employee.Email,
            //    Age=employee.Age,
            //    HiringDate=employee.HiringDate,
            //    IsActive=employee.IsActive,
            //    PhoneNumber=employee.PhoneNumber,
            //    Salary=employee.Salary,
              
                


             
            //}
               
            
            //);


        }
        

        [HttpPost] //Post
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, /*UpdatedEmployeeDto*/ EmployeeViewModel employee)
        {
            if (!ModelState.IsValid)//Sever-Side Validation
                return View(employee);

            var Message = string.Empty;

            try
            {
                var EditEmployee = _mapper.Map<UpdatedEmployeeDto>(employee);


                //var result = new UpdatedEmployeeDto() {
                //    Id=id,
                //    DepartmentId = employee.DepartmentId,
                //HiringDate= employee.HiringDate,
                //Salary= employee.Salary,
                //Address= employee.Address,
                //EmployeeType=employee.EmployeeType,
                //Gender=employee.Gender,
                //Email=employee.Email,
                //Age=employee.Age,
                //PhoneNumber = employee.PhoneNumber,
                //IsActive = employee.IsActive,
                //Name=employee.Name,




               // };

                var updated =await _employeeService.UpdatedEmployeeAsync(EditEmployee) ;
               
                //if (updated)

                //    return RedirectToAction("Index");
                //Message= "an error has occured during Updating the employee :(";

                if (updated > 0)
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

            var employee = _employeeService.GetEmployeeByIdAsync(id.Value);

            if (employee is null)
                return NotFound();

            return View(employee);

        }


        [HttpPost] //Post
        [ValidateAntiForgeryToken]
        public  async Task<IActionResult> Delete(int id)
        {
            var message = string.Empty;

            try
            {

                var deleted = await _employeeService.DeleteEmployeeAsync(id);
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
