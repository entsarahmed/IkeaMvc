using AutoMapper;
using LinkDev.Ikea.BLL.Models.Departments;
using LinkDev.Ikea.BLL.Services.Departments;
using LinkDev.Ikea.DAL.Entities.Departments;
using LinkDev.Ikea.PL.ViewModels.Departments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace LinkDev.Ikea.PL.Controllers
{
    //1. Inheritance: DepartmentController is a  Controller
    //2. Composition: DepartmentController has a IDepartmentService
    [Authorize]
    public class DepartmentController : Controller
    {
        //[FromServices]
        //public IDepartmentService DepartmentService { get; } = null!;

        #region Services
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly IMapper _mapper;

       

        public DepartmentController(IDepartmentService departmentService, IWebHostEnvironment environment, ILogger<DepartmentController> logger, IMapper mapper)
        {
            _departmentService=departmentService;
            _environment=environment;
            _logger=logger;
            _mapper=mapper;


        }
        #endregion



        #region Index

        // View's Dictionary: Pass Data From Controller[Action] to View (from View  -> [PartialView,Layout]

        //Pass Data from View to Partial View

        //Pass Data from View to Layout that using View


        [HttpGet] //Get: /Department/Index
        public  async Task<IActionResult> Index()
        {
            //1. ViewData ia a Dictionary Type Property (introduced in ASP.NET FrameWork 3.5
            /////   => It helps us to transfer the data from Controller[Action] to View 
            ///
            //ViewData["Message"] = "Hello ViewData";


            //2. ViewBags is a Dynamic Type Property (introduced in ASP.NET Framework 4.0
            // => It helps us to transfer the data from controller

           // ViewBag.Message="Hello ViewBag";

            var departments =await _departmentService.GetAllDepartmentsAsync();
            return View(departments);
        }

        #endregion


        #region Details

        [HttpGet] // Get: /Department/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var department =await _departmentService.GetDepartmentByIdAsync(id.Value);
            if (department is null)
                return NotFound();
            return View(department);

        }

        #endregion



        #region Create
        [HttpGet] //Get: /Department/Create
        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmentViewModel  departmentVM)
        {
            if (!ModelState.IsValid)
                return View(departmentVM);
            var message = string.Empty;
            try
            {
                
                var CreatedDepartment= _mapper.Map<CreatedDepartmentDto>(departmentVM);  
                
                //Manual Mapping
                //var createdDepartment = new CreatedDepartmentDto()
                //{
                //    Code= departmentVM.Code,
                //    Name= departmentVM.Name,
                //    Description=departmentVM.Description,
                //    CreationDate=departmentVM.CreationDate,
                //};

                var created =await _departmentService.createdDepartmentAsync(CreatedDepartment) > 0;


                //3. TempData is a Property of type Dictionary Object (introduced in .NET Framework 3.5)
                //        :Used for Transfering the Data Between 2 Consuctive Request 
                if (!created)
                    TempData["Message"] = "Department is Created";
               else
                
                    TempData["Message"] = "Department is not Created";

                //ModelState.AddModelError(string.Empty, message);
                return RedirectToAction(nameof(Index));


            }
            catch (Exception ex)
            {
                //1. Log Exception 
                _logger.LogError(ex, ex.Message);
                //2. Set Message
                message=_environment.IsDevelopment() ? ex.Message : "an error has occured during Creation the department :(";

                //TempData["Message"] =message;
                //return RedirectToAction(nameof(Index));
                ModelState.AddModelError(string.Empty, message);
                return View(departmentVM);



            }
        }

        #endregion





        #region Update

        [HttpGet] //Get: /Department/Edit/id

        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return BadRequest();//400

            var department =await _departmentService.GetDepartmentByIdAsync(id.Value);

            if (department is null)
                return NotFound();//404

            var departmentVM = _mapper.Map<DepartmentViewModel>(department);

            return View(departmentVM);
           //Manual Mapping
            //return View(new DepartmentViewModel()
            //{
            //    Code = department.Code,
            //    Name = department.Name,
            //    Description = department.Description,
            //    CreationDate = department.CreationDate,
            //}
            //    );


        }

        [HttpPost] //Post
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, DepartmentViewModel departmentVM)
        {
            //if (id is null)
            //    return BadRequest();
            if (!ModelState.IsValid)//Sever-Side Validation
                return View(departmentVM);
                

            var Message = string.Empty;

            try
            {
                var departmentToUpdate = _mapper.Map<UpdatedDepartmentDto>(departmentVM);

                //var departmentToUpdate = new UpdatedDepartmentDto()
                //{
                //    Id=id.Value,
                //    Code= departmentVM.Code,
                //    Name= departmentVM.Name,
                //    Description=departmentVM.Description,
                //    CreationDate=departmentVM.CreationDate,
                //};

                var updated =await _departmentService.UpdatedDepartmentAsync(departmentToUpdate) ;

                if (updated>0)
                    TempData["Message"] = "Department is Updated";
                else

                    TempData["Message"] = "Department is not Updated";
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                //1. Log Exception 
                _logger.LogError(ex, ex.Message);


                //2. Set Message
                Message=_environment.IsDevelopment() ? ex.Message : "an error has occured during Updating the department :(";



            }
            ModelState.AddModelError(string.Empty, Message);
            return View(departmentVM);
        }

        #endregion


        #region Delete

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
                return BadRequest();

            var department =await _departmentService.GetDepartmentByIdAsync(id.Value);

            if (department is null)
                return NotFound();

            return View(department);

        }




        //[HttpPost] //Post
        //[ValidateAntiForgeryToken]
        //public IActionResult Delete(int id)
        //{
        //    var message = string.Empty;

        //    try
        //    {

        //        var deleted = _departmentService.DeleteDepartment(id);
        //        if (deleted)
        //            return RedirectToAction("Index");
        //        message= "an error has occured during deleting the department :(";

        //    }
        //    catch (Exception ex)
        //    {

        //        //1. Log Exception 
        //        _logger.LogError(ex, ex.Message);


        //        //2. Set Message
        //        message=_environment.IsDevelopment() ? ex.Message : "an error has occured during deleting the department :(";
        //    }
        //    //ModelState.AddModelError(string.Empty, message);
        //    return RedirectToAction(nameof(Index));

        //} 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid department ID.";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                var deleted =await _departmentService.DeleteDepartmentAsync(id);
                if (deleted)
                {
                    TempData["SuccessMessage"] = "Department deleted successfully.";
                    return RedirectToAction(nameof(Index));
                }

                TempData["ErrorMessage"] = "An error occurred while deleting the department.";
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, ex.Message);

                // Set error message
                TempData["ErrorMessage"] = _environment.IsDevelopment() ? ex.Message : "An unexpected error occurred.";
            }

            return RedirectToAction(nameof(Index));
        }

        #endregion





    }
}
