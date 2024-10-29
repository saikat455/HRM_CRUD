using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Training.DataAccess;
using Training.DataAccess.Repositories;
using Training.DataAccess.Repositories.Interface;
using Trainingtask.Models.DTO;
using Trainingtask.Models.Entity;
using TrainingTask.Models;

namespace TrainingTask.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TrainingDbContext db;

        private readonly IUnitOfWork unitOfWork;

        public HomeController(ILogger<HomeController> logger, TrainingDbContext db,IUnitOfWork unitOfWork)
        {
            _logger = logger;
            this.db = db;
            this.unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> EmployeeList()
        {
           var empList = await unitOfWork.EmployeeRepo.GetIncludedept();
            return Ok(empList);
        }

      

        // Index method to get the list of departments
        public async Task<IActionResult> Index(string Id, string name)
        {
            var query = db.Departments.AsQueryable();

            if (!string.IsNullOrEmpty(Id))
            {
                query = query.Where(x => x.Id == Id);
            }

            var Departmentlist = await query.ToListAsync(); // Await the async operation

            return View(Departmentlist); // Return the actual data to the view
        }

        // Method to count the total number of departments
        public async Task<IActionResult> totalDepartment(string Id)
        {
            var total = await db.Departments.Where(x => x.Id == Id).AnyAsync();
            return Ok(total);
        }

        // Method to get department by Id and return it as JSON response
        public async Task<IActionResult> GetById(string Id)
        {
            var dept = await db.Departments.Where(x => x.Id == Id).SingleOrDefaultAsync();

            if (dept is null)
            {
                return NotFound();
            }

            return Ok(dept);
        }

        // Method to get department by Id and render it in a view
        public async Task<IActionResult> GetByIdview(string Id)
        {
            var total = await db.Departments.CountAsync();
            var dept = await db.Departments.Where(x => x.Name == Id).FirstOrDefaultAsync();

            if (dept is null)
            {
                return NotFound();
            }

            ViewBag.Total = total;
            return View(dept);
        }

       



        public async Task<IActionResult> Departmentlist2()
        {
            var department = await db.Departments.Select(x => new DeptListDTO
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();

            return Ok(department);
        }

        [HttpGet]
        public async Task<IActionResult> DepartmentSave(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return View();
            }
            var dept = await db.Departments.Where(x => x.Id == id).SingleOrDefaultAsync();

            if (dept is null)
            {
                return NotFound();
            }

            return View(dept);
        }

        [HttpPost]
        public async Task<IActionResult> DepartmentSave(DeptListDTO model)
        {
            if (!string.IsNullOrEmpty(model.Id))
            {
                //var dept = await departmentRepo.GetById(model.Id);
                var dept = await unitOfWork.DepartmentRepo.GetById(model.Id);

                if (dept is null)
                {
                    return NotFound();
                }
                dept.Name = model.Name;
                unitOfWork.DepartmentRepo.Edit(dept);
                await unitOfWork.DepartmentRepo.Save();
                return RedirectToAction(nameof(Index));

            }
            var department = new Department()
            {
                Name = model.Name,
            };
            unitOfWork.DepartmentRepo.Add(department);
            await unitOfWork.DepartmentRepo.Save();
          

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> DepartmentdELETE(string id)
        {

            var dept = await db.Departments.Where(x => x.Id == id).SingleOrDefaultAsync();

            if (dept is null)
            {
                return NotFound();
            }
            db.Remove(dept);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }


      

        // Privacy method
      

        // Error handling method
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}


