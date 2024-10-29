using Microsoft.AspNetCore.Mvc;
using Training.DataAccess.Repositories;
using Trainingtask.Models.DTO;
using Trainingtask.Models.Entity;

namespace TrainingTask.Controllers
{
    public class DesignationController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public DesignationController(IUnitOfWork unitofWork)
        {
            unitOfWork = unitofWork;
        }

        public async Task<IActionResult> DesigData(string Id)
        {
            var designations = await unitOfWork.DesignationRepo.GetAll();
            if (!string.IsNullOrEmpty(Id))
            {
                designations = designations.Where(d => d.Id == Id).ToList();
            }
            // Map Designation entities to DesigDTOs
            var designationDTOs = designations.Select(d => new DesigDTO
            {
                Id = d.Id,
                Name = d.Name
            }).ToList();

            return View(designationDTOs);

        }


        public async Task<IActionResult> totalDesignation()
        {
            var designations = await unitOfWork.DesignationRepo.GetAll();
            var total = designations.Count;
            return Ok(total);
        }

        // Method to get department by Id and return it as JSON response
        public async Task<IActionResult> GetById(string Id)
        {
            var desig = await unitOfWork.DesignationRepo.GetById(Id);

            if (desig is null)
            {
                return NotFound();
            }

            return Ok(desig);
        }



        [HttpGet]
        public async Task<IActionResult> DesignationSave(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                return View();
            }

            var designation = await unitOfWork.DesignationRepo.GetById(Id);
            if (designation is null)
            {
                return NotFound();
            }

           

            return View(designation);
        }

        [HttpPost]
        public async Task<IActionResult> DesignationSave(DesigDTO model)
        {
           

            if (!string.IsNullOrEmpty(model.Id))
            {
                var des = await unitOfWork.DesignationRepo.GetById(model.Id);

                if (des is null)
                {
                    return NotFound();
                }
                des.Name = model.Name;

                unitOfWork.DesignationRepo.Edit(des);
                await unitOfWork.DesignationRepo.Save();

            }
            else
            {
                var newDesignation = new Designation
                {
                    Name = model.Name
                };
                unitOfWork.DesignationRepo.Add(newDesignation);
                await unitOfWork.DesignationRepo.Save();
            }


            return RedirectToAction(nameof(DesigData));
        }

        [HttpGet]
        public async Task<IActionResult> DesignationdELETE(string id)
        {

            var des = await unitOfWork.DesignationRepo.GetById(id);

            if (des is null)
            {
                return NotFound();
            }
            unitOfWork.DesignationRepo.Delete(des);
            await unitOfWork.DesignationRepo.Save();
            return RedirectToAction(nameof(DesigData));

        }


    }
}
