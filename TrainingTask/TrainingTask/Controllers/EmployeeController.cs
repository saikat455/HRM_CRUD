using Microsoft.AspNetCore.Mvc;
using Training.DataAccess.Repositories;
using Trainingtask.Models.DTO;
using Trainingtask.Models.Entity;

namespace TrainingTask.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public EmployeeController(IUnitOfWork unitofWork)
        {
           this.unitOfWork = unitofWork;
        }

        public async Task<IActionResult> Privacy()
        {
            var employees = await unitOfWork.EmployeeRepo.GetIncludedept();
            return View(employees);
          
        }

        
        public async Task<IActionResult> totalEmployee()
        {
            var total = (await unitOfWork.EmployeeRepo.GetAll()).Count;
            return Ok(total);
        }

        // Method to get department by Id and return it as JSON response
        public async Task<IActionResult> GetByEmployeeId(string Id)
        {
            var emp = await unitOfWork.EmployeeRepo.GetById(Id);

            if (emp is null)
            {
                return NotFound();
            }

            return Ok(emp);
        }



        [HttpGet]
        public async Task<IActionResult> EmployeeSave(string id)
        {
            ViewBag.Departments = await unitOfWork.DepartmentRepo.GetAll();
            ViewBag.Designations = await unitOfWork.DesignationRepo.GetAll();


            if (string.IsNullOrEmpty(id))
            {
                return View();
            }
            var emp = await unitOfWork.EmployeeRepo.GetById(id);

            if (emp is null)
            {
                return NotFound();
            }

            return View(emp);
        }



        [HttpPost]
        public async Task<IActionResult> EmployeeSave(EmployeeDTO model)
        {
            var departmentExists = await unitOfWork.DepartmentRepo.GetById(model.DeptId);
            if (departmentExists == null)
            {

                ModelState.AddModelError("DeptId", "The selected department does not exist.");
                return View(model);
            }

            if (!string.IsNullOrEmpty(model.DesigId) &&
                await unitOfWork.DesignationRepo.GetById(model.DesigId) is null)
            {
                ModelState.AddModelError("DesigId", "The selected designation does not exist.");
                return View(model);
            }

            if (!string.IsNullOrEmpty(model.Id))
            {
                var emp = await unitOfWork.EmployeeRepo.GetById(model.Id);

                if (emp is null)
                {
                    return NotFound();
                }
                emp.Name = model.Name;
                emp.Address = model.Address;
                emp.City = model.City;
                emp.Phone = model.Phone;
                emp.DeptId = model.DeptId;
                emp.DesigId = model.DesigId;

                unitOfWork.EmployeeRepo.Edit(emp);
               

            }
            else
            {
                var employee = new Employee
                {
                    Name = model.Name,
                    Address = model.Address,
                    City = model.City,
                    Phone = model.Phone,
                    DeptId = model.DeptId,
                    DesigId = model.DesigId
                };
                unitOfWork.EmployeeRepo.Add(employee);

            }

            await unitOfWork.EmployeeRepo.Save();

            return RedirectToAction(nameof(Privacy));
        }

        [HttpGet]
        public async Task<IActionResult> EmployeeDelete(string id)
        {

            var emp = await unitOfWork.EmployeeRepo.GetById(id);

            if (emp is null)
            {
                return NotFound();
            }
            unitOfWork.EmployeeRepo.Delete(emp);
            await unitOfWork.EmployeeRepo.Save();
            return RedirectToAction(nameof(Privacy));

        }


    }
}
