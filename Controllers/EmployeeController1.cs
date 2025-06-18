using Data_Access_Layer.Models.Department;
using Data_Access_Layer.Models.Employees;
using Logic_layer.DTO.Department;
using Logic_layer.DTO.Employee;
using Logic_layer.Services.Departments_services;
using Logic_layer.Services.EmployeeServices;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Web_mvd.View_Models;

namespace Web_mvd.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeServices _employeeServices;
        private readonly ILogger<EmployeeController> logger;
        private readonly IWebHostEnvironment environment;

        public EmployeeController(IEmployeeServices employeeServices , IDepartmentServices departmentServices, ILogger<EmployeeController> logger, IWebHostEnvironment environment)
        {
            _employeeServices = employeeServices;
            this.logger = logger;
            this.environment = environment;
        }


        [HttpGet]
        public IActionResult Index(string Search )
        {
            var employees = _employeeServices.GetAllEmployees(Search);
            return View(employees);
        }

        [HttpGet]   
        public IActionResult Create()
        {
           
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  
        public IActionResult Create(EmployeeVM employeeVM)
        {//sericer side validation  
            

            if (!ModelState.IsValid)
                return View(employeeVM);
            var message = string.Empty;
            try
            {

                var employeeDto = new CreateEmployeeDto()
                {
                    Name = employeeVM.Name,
                    age = employeeVM.Age,   
                   HiringDate = employeeVM.HiringDate,  
                    salary = employeeVM.Salary,
                    Address = employeeVM.Address,
                    Email = employeeVM.Email,
                    PhoneNumber = employeeVM.PhoneNumber,   
                    EmployeeType = employeeVM.EmployeeType, 
                    Gender = employeeVM.Gender,
                    createdon = employeeVM.createdon,
                    IsActive = employeeVM.IsActive,
                    DepartmentId = employeeVM.DepartmentId, 
                    

                };


                var result = _employeeServices.GreateEmployee(employeeDto);
                if (result > 0)
                    return RedirectToAction("Index");


                else
                {
                    message = "Error in creating Department";
                    ModelState.AddModelError(string.Empty, message);
                    return View(employeeDto);
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                ModelState.AddModelError(string.Empty, message);
                return View(employeeVM);
            }
        }

        [HttpGet]

        public IActionResult Details(int? id)
        {
            if (id is null)
                return BadRequest();

            var employee = _employeeServices.GetEmployeesByid(id.Value);
            if (employee is null)
                return NotFound();

            return View(employee); // Assuming employee is of type EmployeeDetailsDto
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (id == 0)
                return BadRequest();
            var employee = _employeeServices.GetEmployeesByid(id);
            if (employee is null)
                return NotFound();

            return View(employee);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult delete(int EmpId)
        {
            var message = string.Empty;
            try
            {
                var isDeleted = _employeeServices.deleteEmployee(EmpId);

                if (isDeleted)
                    return RedirectToAction(nameof(Index));

                message = "Error in deleting employee";
            }
            catch (Exception ex)
            {
                // Log the exception
                logger.LogError(ex, ex.InnerException?.Message ?? ex.Message);

                message = environment.IsDevelopment() ? ex.Message : "Error in deleting employee";
            }

            ModelState.AddModelError(string.Empty, message);
            return RedirectToAction(nameof(Delete), new { id = EmpId });
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null)
                return BadRequest();
            var employee = _employeeServices.GetEmployeesByid(id.Value);
            if (employee is null)
                return NotFound();
            var employeevm = new EmployeeVM()
            {//CREATE NEW OBJECT
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.age,
                HiringDate = employee.HiringDate,
                Salary = employee.salary,
                Address = employee.Address,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                EmployeeType = employee.EmployeeType,
                Gender = employee.Gender,
                createdon = employee.createdon,
                IsActive = employee.IsActive,
               
            };
              // Assuming you have a method to get all departments 
            return View(employeevm);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]  
        public IActionResult Edit(EmployeeVM employeeVM)
        {
            if (!ModelState.IsValid)
                return View(employeeVM);
            var Message = string.Empty; 

            try
            {
                var employeeDto = new UpdateEmployeeDto()
                {
                    Id = employeeVM.Id,
                    Name = employeeVM.Name,
                    age = employeeVM.Age,
                    HiringDate = employeeVM.HiringDate,
                    salary = employeeVM.Salary,
                    Address = employeeVM.Address,
                    Email = employeeVM.Email,
                    PhoneNumber = employeeVM.PhoneNumber,
                    EmployeeType = employeeVM.EmployeeType,
                    DepartmentId = employeeVM.DepartmentId,
                    Gender = employeeVM.Gender,
                    CreationDate = employeeVM.createdon,
                    IsActive = employeeVM.IsActive,
                    Description = employeeVM.Description ?? string.Empty
                    
     

                };
                var result = _employeeServices.updateEmployee(employeeDto);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                  Message = "Error in updating Employee"; 
            }
            catch (Exception ex)
            {
                //log the exception
                logger.LogError(ex, ex.Message);

                //set the message   
                Message = environment.IsDevelopment() ? ex.Message : "Error in updating employee";
            }
            ModelState.AddModelError(string.Empty, Message);
            return View(employeeVM);










        }





    }
}
