using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Organic_Food_01_EXM.Data;
using Organic_Food_01_EXM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organic_Food_01_EXM.Areas.Shopper.Controllers
{
    [Area("Shopper")]
    public class ContentController : Controller
    {
        private ApplicationDbContext _db;
        public ContentController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.ContactInfos.ToList());
        }

        //Create Action Page  Method
        public IActionResult Information()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Information(ContactInfo contactInfo)
        {
            if (ModelState.IsValid)
            {
                _db.ContactInfos.Add(contactInfo);
                await _db.SaveChangesAsync();
                TempData["save"] = "Product Save Successfully!!";
                return RedirectToAction(nameof(Information));
            }
            return View(contactInfo);
        }


        //Create Action Page  Method
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Add(category);
                await _db.SaveChangesAsync();
                TempData["save"] = "Product Save Successfully!!";
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }




        public IActionResult AboutUs()
        {
            return View();
        }



    }
}
