using Logic_layer.DTO.Department;
using Logic_layer.Services.Departments_services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Web_mvd.View_Models;

namespace Web_mvd.Controllers
{
    public class DepartmentController : Controller
    {// serivce = > Department

        private readonly IDepartmentServices departmentServices;
        private readonly ILogger<DepartmentController> logger;
        private readonly IWebHostEnvironment environment;


        public DepartmentController(IDepartmentServices departmentServices, ILogger<DepartmentController> logger, IWebHostEnvironment environment)
        {
            this.departmentServices = departmentServices ?? throw new ArgumentNullException(nameof(departmentServices));
            this.logger = logger;
            this.environment = environment;
        }


        #region index
        [HttpGet]
        public IActionResult Index()
        {
            var Departments = departmentServices.GetAllDepartments();
            return View(Departments);
        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Create(DepartmentVM departmentVM)
        {//sericer side validation  
            if (!ModelState.IsValid)
                return View(departmentVM);
            var message = string.Empty;

            try
            {
                var departmentDto = new CreateDepartmentDto()
                {
                    Name = departmentVM.Name,
                    code = departmentVM.code,
                    CreationDate = departmentVM.CreationDate,
                    description = departmentVM.Description
                };

                var result = departmentServices.GreateDepartment(departmentDto);
                if (result > 0)
                    return RedirectToAction("Index");


                else
                {
                    message = "Error in creating Department";

                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

                if (environment.IsDevelopment())

                    message = ex.Message;

                else

                    message = "Error in creating Department";

            }
            ModelState.AddModelError(string.Empty, message);
            return View(departmentVM);

        }

        #endregion

        #region Details 
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id is null)
                return BadRequest();
            var department = departmentServices.GetDepartmentByid(id.Value);
            if (department is null)
                return NotFound();

            return View(department);
        }
        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null)
                return BadRequest();
            var department = departmentServices.GetDepartmentByid(id.Value);
            if (department is null)
                return NotFound();
            var MappedDepartment = new DepartmentVM()
            {//CREATE NEW OBJECT
                Id = department.Id,
                Name = department.Name,
                code = department.code,
                CreationDate = department.CreationDate,
                Description = department.Description
            };
            return View(MappedDepartment);


        }

        [HttpPost]
        public IActionResult Edit(DepartmentVM departmentVM)
        {
            if (!ModelState.IsValid)
                return View(departmentVM);
            var message = string.Empty;
            try
            {
                var departmentDto = new UpdateDepartmentDto()
                {
                    Id = departmentVM.Id,
                    Name = departmentVM.Name,
                    code = departmentVM.code,
                    CreationDate = departmentVM.CreationDate,
                    Description = departmentVM.Description
                };

                var result = departmentServices.updateDepartment(departmentDto);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    message = "Error in updating Department";
                }
            }
            catch (Exception ex)
            {
                //log the exception
                logger.LogError(ex, ex.InnerException?.Message ?? ex.Message);


                //set the message   
                message = environment.IsDevelopment() ? ex.Message : "Error in updating Department";
            }
            ModelState.AddModelError(string.Empty, message);
            return View(departmentVM);

        }

        #endregion

        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (id == 0)
                return BadRequest();
            var department = departmentServices.GetDepartmentByid(id);
            if (department is null)
                return NotFound();

            return View(department);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult delete(int DeptId)
        {
            var message = string.Empty;
            try
            {
                var IsDeleted = departmentServices.deleteDepartment(DeptId);

                if (IsDeleted)
                    return RedirectToAction(nameof(Index));

                message = "Error in deleting department";

            }
            catch (Exception ex)
            {
                //log the exception
                logger.LogError(ex, ex.InnerException?.Message ?? ex.Message);

                message = environment.IsDevelopment() ? ex.Message : "Error in deleting department";        
            }

            ModelState.AddModelError(string.Empty, message);
            return RedirectToAction(nameof(Delete), new { id = DeptId});




        }
    }

}       




               




                

              


            

















