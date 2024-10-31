using Microsoft.AspNetCore.Mvc;
using Training.DataAccess.Repositories;
using Trainingtask.Models.DTO;
using Trainingtask.Models.Entity;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TrainingTask.Controllers
{
    public class ShiftController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public ShiftController(IUnitOfWork unitofWork)
        {
            this.unitOfWork = unitofWork;
        }


        // Privacyy View
        public async Task<IActionResult> Privacyy()
        {
            var shifts = await unitOfWork.ShiftRepo.GetIncludeShift();
            return View(shifts);
        }

        // Get total shifts count
        public async Task<IActionResult> TotalShift()
        {
            var total = (await unitOfWork.ShiftRepo.GetAll()).Count;
            return Ok(total);
        }

        // Get Shift by Id
        public async Task<IActionResult> GetByShiftId(Guid id)
        {
            var shift = await unitOfWork.ShiftRepo.GetById(id);

            if (shift is null)
            {
                return NotFound();
            }

            return Ok(shift);
        }

        [HttpGet]
        public async Task<IActionResult> ShiftSave(Guid id)
        {
            // Fetch companies and ensure ViewBag.Companies is not null
            var companies = await unitOfWork.CompanyRepo.GetAll() ?? new List<Company>();
            ViewBag.Companies = new SelectList(companies, "ComId", "ComName");

            // Handle case for a new shift creation
            if (id == Guid.Empty) // Check if the Guid is empty
            {
                return View(new ShiftDTO());
            }

            // Fetch the shift details for editing
            var shift = await unitOfWork.ShiftRepo.GetById(id);
            if (shift == null)
            {
                return NotFound();
            }

            var shiftDto = new ShiftDTO
            {
                ShiftId = shift.ShiftId,
                ShiftName = shift.ShiftName,
                In = shift.In,
                Out = shift.Out,
                Late = shift.Late,
                ComId = shift.ComId
            };

            return View(shiftDto);
        }






        // POST: ShiftSave for saving a new or updated shift
        [HttpPost]
        public async Task<IActionResult> ShiftSave(ShiftDTO model)
        {
            if (!ModelState.IsValid)
            {
                var companies = await unitOfWork.CompanyRepo.GetAll();
                ViewBag.Companies = companies ?? new List<Company>();
                return View(model);
            }

            var companyExists = await unitOfWork.CompanyRepo.GetById(model.ComId);
            if (companyExists == null)
            {
                ModelState.AddModelError("ComId", "The selected company does not exist.");
                var companies = await unitOfWork.CompanyRepo.GetAll();
                ViewBag.Companies = companies ?? new List<Company>();
                return View(model);
            }

            if (model.ShiftId != Guid.Empty)
            {
                var shift = await unitOfWork.ShiftRepo.GetById(model.ShiftId);
                if (shift == null)
                {
                    return NotFound();
                }

                // Update properties
                shift.ShiftName = model.ShiftName;
                shift.In = model.In;
                shift.Out = model.Out;
                shift.Late = model.Late;
                shift.ComId = model.ComId;

                unitOfWork.ShiftRepo.Edit(shift);
            }
            else
            {
                var newShift = new Shift
                {
                    ShiftName = model.ShiftName,
                    In = model.In,
                    Out = model.Out,
                    Late = model.Late,
                    ComId = model.ComId
                };

                unitOfWork.ShiftRepo.Add(newShift);
            }

            await unitOfWork.ShiftRepo.Save();
            return RedirectToAction(nameof(Privacyy));
        }


        // GET: ShiftDelete for deleting a shift
        [HttpGet]
        public async Task<IActionResult> ShiftDelete(Guid id)
        {
            var shift = await unitOfWork.ShiftRepo.GetById(id);
            if (shift == null)
            {
                return NotFound();
            }

            unitOfWork.ShiftRepo.Delete(shift);
            await unitOfWork.ShiftRepo.Save();
            return RedirectToAction(nameof(Privacyy));
        }
    }
}
