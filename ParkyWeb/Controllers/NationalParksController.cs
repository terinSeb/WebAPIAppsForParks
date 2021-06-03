using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParkyWeb.Models;
using ParkyWeb.Repository.IRepository;

namespace ParkyWeb.Controllers
{
    public class NationalParksController : Controller
    {
        private readonly INationalParkRepository _npRepo;
        public NationalParksController(INationalParkRepository npRepo)
        {
            _npRepo = npRepo;
        }
        public IActionResult Index()
        {
            return View(new NationalParks() { });
        }
       public async Task<IActionResult> GetAllNationalParks()
        {
            return Json(new { data = await _npRepo.GetAllAsync(SD.NaptionalParkAPIPath) });
        }
        public async Task<IActionResult> Upsert(int? id)
        {
            NationalParks Obj = new NationalParks();
            if(id == null)
            {
                return View(Obj);
            }
            Obj = await _npRepo.GetAsync(SD.NaptionalParkAPIPath, id.GetValueOrDefault());
            if(Obj == null)
            {
                return NotFound();
            }
            return View(Obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(NationalParks Obj)
        {
            if(ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if(files.Count >0)
                {
                    byte[] p1 = null;
                    using(var fs1 = files[0].OpenReadStream())
                    {
                        using (var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            p1 = ms1.ToArray();
                        }
                    }
                    Obj.Picture = p1;
                }
                else
                {
                    var ObjFromDb = await _npRepo.GetAsync(SD.NaptionalParkAPIPath, Obj.Id);
                    Obj.Picture = ObjFromDb.Picture;
                }
                if(Obj.Id ==0)
                {
                    await _npRepo.CreateAsync(SD.NaptionalParkAPIPath, Obj);
                }
                else
                {
                    await _npRepo.UpdateAsync(SD.NaptionalParkAPIPath + Obj.Id, Obj);
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(Obj);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var status = await _npRepo.DeleteAsync(SD.NaptionalParkAPIPath, id);
            if(status)
            {
                return Json(new { success = true, message = "Delete Successful" });
            }
            return Json(new { success = false, message = "Delete not Successful" });
        }
    }
}