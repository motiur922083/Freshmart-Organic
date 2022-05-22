using Microsoft.AspNetCore.Authorization;
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
    public class TagController : Controller
    {

        private ApplicationDbContext _db;
        public TagController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index(int pg = 1)
        {
            //Paination Start
            var products = _db.Tags.ToList();
            const int pageSize = 5;
            if (pg < 1)
                pg = 1;
            int recsCount = products.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = products.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;

            return View(data);
            //return View(_db.Tags.ToList());
        }

        //Create Action Page  Method
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tag tag)
        {
            if (ModelState.IsValid)
            {
                _db.Tags.Add(tag);
                await _db.SaveChangesAsync();
                TempData["save"] = "Product Save Successfully!!";
                return RedirectToAction(nameof(Index));
            }
            return View(tag);
            
            //Ajex Message

            //if (ModelState.IsValid)
            //{
            //    _db.Tags.Add(tag);
            //    await _db.SaveChangesAsync();
            //    TempData["SuccessMessage"] = "Tag Name Save Successfully";
            //    return PartialView("_success");
            //}
            //return PartialView("_error");
        }
        //Edit Action Page  Method
        public IActionResult Edit(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            var productType = _db.Tags.Find(id);
            if (productType==null)
            {
                return NotFound();
            }
            return View(productType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Tag productCategory)
        {
            if (ModelState.IsValid)
            {
                _db.Update(productCategory);
                await _db.SaveChangesAsync();
                TempData["edit"] = "Product Edit Successfully!!";
                return RedirectToAction(nameof(Index));
            }
            return View(productCategory);
        }
        //Details Action Page  Method
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productCategory = _db.Tags.Find(id);
            if (productCategory == null)
            {
                return NotFound();
            }
            return View(productCategory);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(Tag productCategory)
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
            var productCategory = _db.Tags.Find(id);
            if (productCategory == null)
            {
                return NotFound();
            }
            return View(productCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id, Tag productCategory)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (id != productCategory.Id)
            {
                return NotFound();
            }
            var productCategories = _db.Tags.Find(id);
            if (productCategories == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _db.Remove(productCategories);
                await _db.SaveChangesAsync();
                TempData["delete"] = "Product Delete Successfully!!";
                return RedirectToAction(nameof(Index));
            }
            return View(productCategories);
        }

    }
}
