using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParkyWeb.Models;
using ParkyWeb.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ParkyWeb.Controllers
{
    public class TrailsController : Controller
    {
        private readonly INationalParkRepository _npRepo;
        private readonly ITrailRepository _trailRepo;
        public TrailsController(INationalParkRepository npRepo, ITrailRepository trailRepo)
        {
            _npRepo = npRepo;
            _trailRepo = trailRepo;
        }
        public IActionResult Index()
        {
            return View(new Trails() { });
        }
       public async Task<IActionResult> GetAllTrail()
        {
            return Json(new { data = await _trailRepo.GetAllAsync(SD.TrailParkAPIPath) });
        }
        public async Task<IActionResult> Upsert(int? id)
        {
            IEnumerable<NationalParks> npList = await _npRepo.GetAllAsync(SD.NaptionalParkAPIPath);
            TrailsVM objVM = new TrailsVM()
            {
                NationalParkList = npList.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                Trail = new Trails()
                
            };
            
            if(id == null)
            {
                return View(objVM);
            }
            objVM.Trail = await _trailRepo.GetAsync(SD.TrailParkAPIPath, id.GetValueOrDefault());
            if(objVM.Trail == null)
            {
                return NotFound();
            }
            return View(objVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(TrailsVM Obj)
        {
            if(ModelState.IsValid)
            {                
                if(Obj.Trail.Id ==0)
                {
                    await _trailRepo.CreateAsync(SD.TrailParkAPIPath, Obj.Trail);
                }
                else
                {
                    await _trailRepo.UpdateAsync(SD.TrailParkAPIPath + Obj.Trail.Id, Obj.Trail);
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                IEnumerable<NationalParks> npList = await _npRepo.GetAllAsync(SD.NaptionalParkAPIPath);
                TrailsVM objVM = new TrailsVM()
                {
                    NationalParkList = npList.Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    }),
                    Trail = Obj.Trail

                };
                return View(objVM);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var status = await _trailRepo.DeleteAsync(SD.TrailParkAPIPath, id);
            if(status)
            {
                return Json(new { success = true, message = "Delete Successful" });
            }
            return Json(new { success = false, message = "Delete not Successful" });
        }
    }
}