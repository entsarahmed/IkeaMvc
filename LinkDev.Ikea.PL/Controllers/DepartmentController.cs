﻿using LinkDev.Ikea.BLL.Models.Departments;
using LinkDev.Ikea.BLL.Services.Departments;
using LinkDev.Ikea.DAL.Entities.Departments;
using LinkDev.Ikea.PL.ViewModels.Departments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

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



        [HttpGet] // Get: /Department/Details
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null)
                return NotFound();
            return View(department);

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
                message=_environment.IsDevelopment() ? ex.Message : "an error has occured during Creation the department :(";



            }
            ModelState.AddModelError(string.Empty, message);
            return View(department);
            }





        [HttpGet] //Get: /Department/Edit/id

        public IActionResult Edit(int? id)
        {
            if (id is null)
                 return BadRequest();//400

            var department = _departmentService.GetDepartmentById(id.Value);

            if (department is null)
                return NotFound();//404

            return View(new DepartmentEditViewModel ()
            {
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreationDate = department.CreationDate,
            }
                );

            
        }

        [HttpPost] //Post
        public IActionResult Edit([FromRoute]int id, DepartmentEditViewModel departmentVM)
        {
            if (!ModelState.IsValid)//Sever-Side Validation
                 return View(departmentVM);

            var Message = string.Empty;

            try
            {
                var departmentToUpdate = new UpdatedDepartmentDto()
                {
                    Id=id,
                    Code= departmentVM.Code,
                    Name= departmentVM.Name,
                    Description=departmentVM.Description,
                    CreationDate=departmentVM.CreationDate,
                };

                var updated = _departmentService.UpdatedDepartment(departmentToUpdate) > 0;
                if (updated)
                    return RedirectToAction("Index");
                Message= "an error has occured during Updating the department :(";
            }
            catch (Exception ex)
            {
                //1. Log Exception 
                _logger.LogError(ex, ex.Message);


                //2. Set Message
                Message=_environment.IsDevelopment() ? ex.Message : "an error has occured during Updating the department :(";



            }
            ModelState.AddModelError(string.Empty,Message);
            return View(departmentVM);
        }

    }
}