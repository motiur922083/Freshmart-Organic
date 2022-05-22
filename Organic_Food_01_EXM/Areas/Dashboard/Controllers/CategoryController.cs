using Microsoft.AspNetCore.Mvc;
using Organic_Food_01_EXM.Data;
using Organic_Food_01_EXM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organic_Food_01_EXM.Areas.Admin.Controllers
{
    [Area("Dashboard")]
    public class CategoryController : Controller
    {
        private ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.Categories.ToList());
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
                //Check product Name Is already Available or not Available
                var searchProduct = _db.Categories.FirstOrDefault(c => c.CategoryName == category.CategoryName);
                if (searchProduct != null)
                {
                    ViewBag.message = "This product is already exist. Please Change Value and Save!!!";

                    return View(category);
                }

                _db.Categories.Add(category);
                await _db.SaveChangesAsync();
                TempData["save"] = "Product Save Successfully!!";
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        //Edit Action Page  Method
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productTag = _db.Categories.Find(id);
            if (productTag == null)
            {
                return NotFound();
            }
            return View(productTag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category productTag)
        {
            if (ModelState.IsValid)
            {
                _db.Update(productTag);
                await _db.SaveChangesAsync();
                TempData["SuccessMessage"] = "Product Edit Successfully!!!";
                return RedirectToAction(nameof(Index));
            }
            return View(productTag);
        }
        //Details Action Page  Method
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productTag = _db.Categories.Find(id);
            if (productTag == null)
            {
                return NotFound();
            }
            return View(productTag);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(Category productTag)
        {
            return RedirectToAction(nameof(Index));
        }
        //Delete Action Page  Method
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productTag = _db.Categories.Find(id);
            if (productTag == null)
            {
                return NotFound();
            }
            return View(productTag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id, Category productTag)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (id != productTag.Id)
            {
                return NotFound();
            }
            var productTags = _db.Categories.Find(id);
            if (productTags == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _db.Remove(productTags);
                await _db.SaveChangesAsync();
                TempData["DeleteMessage"] = "Data Delete Successfully!!!";
                return RedirectToAction(nameof(Index));
            }
            return View(productTags);
        }
    }
}
