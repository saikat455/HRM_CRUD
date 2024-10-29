using Microsoft.AspNetCore.Mvc;
using Training.DataAccess.Repositories;
using Trainingtask.Models.DTO;
using Trainingtask.Models.Entity;

namespace TrainingTask.Controllers
{
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public CompanyController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var companies = await unitOfWork.CompanyRepo.GetAllCompanies();
            return View(companies);
        }

        [HttpGet]
        public async Task<IActionResult> CompanySave(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return View();
            }
            var company = await unitOfWork.CompanyRepo.GetById(id);

            if (company is null)
            {
                return NotFound();
            }

            return View(company);
        }

        [HttpPost]
        public async Task<IActionResult> CompanySave(CompanyDTO model)
        {
            // Calculate House Rent and Medical Allowance based on Basic Salary
            var houseRent = model.Basic * 0.3m; // 30% of Basic Salary
            var medicalAllowance = model.Basic * 0.15m; // 15% of Basic Salary

            if (string.IsNullOrEmpty(model.ComId))
            {
                var company = new Company
                {
                    ComName = model.ComName,
                    Basic = model.Basic,
                    Hrent = houseRent,
                    Medical = medicalAllowance,
                    IsInactive = model.IsInactive
                };
                unitOfWork.CompanyRepo.Add(company);
            }
            else
            {
                var company = await unitOfWork.CompanyRepo.GetById(model.ComId);

                if (company is null)
                {
                    return NotFound();
                }

                company.ComName = model.ComName;
                company.Basic = model.Basic;
                company.Hrent = houseRent; // Update House Rent
                company.Medical = medicalAllowance; // Update Medical Allowance
                company.IsInactive = model.IsInactive;

                unitOfWork.CompanyRepo.Edit(company);
            }

            await unitOfWork.CompanyRepo.Save();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> CompanyDelete(string id)
        {
            var company = await unitOfWork.CompanyRepo.GetById(id);

            if (company is null)
            {
                return NotFound();
            }

            unitOfWork.CompanyRepo.Delete(company);
            await unitOfWork.CompanyRepo.Save();

            return RedirectToAction(nameof(Index));
        }

        // Method to get company by Id and return it as JSON response
        public async Task<IActionResult> GetByCompanyId(string id)
        {
            var company = await unitOfWork.CompanyRepo.GetById(id);

            if (company is null)
            {
                return NotFound();
            }

            return Ok(company);
        }
    }
}
