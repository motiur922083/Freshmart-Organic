using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Organic_Food_01_EXM.Data;
using Organic_Food_01_EXM.Models;
using Microsoft.AspNetCore.Authorization;

namespace Organic_Food_01_EXM.Areas.Admin.Controllers
{
    [Area("Dashboard")]
    public class ProductController : Controller
    {
        private ApplicationDbContext _db;
        private IWebHostEnvironment _he;
        public ProductController(ApplicationDbContext db, IWebHostEnvironment he)
        {
            _db = db;
            _he = he;
        }

        //Index Page
        public IActionResult Index(int pg = 1)
        {
            //Paination Start
            var products = _db.Products.Include(c => c.Tag).Include(f => f.Category).ToList();
            const int pageSize = 10;
            if (pg < 1)
                pg = 1;
            int recsCount = products.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = products.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;

            return View(data);

            //return View(_db.Products.Include(c=>c.Tag).Include(f=>f.Category).ToList());
        }
        [HttpPost]
        public IActionResult Index(decimal? lowAmount, decimal? largeAmount)
        {
            var products = _db.Products.Include(c => c.Tag).Include(f => f.Category).Where(c => c.ProductPrice >= lowAmount && c.ProductPrice <= largeAmount).ToList();
            if (lowAmount==null || largeAmount==null)
            {
                products = _db.Products.Include(c => c.Tag).Include(f => f.Category).ToList();
            }
            return View(products);
        }

        //Create Action Page  Method

        public IActionResult Create()
        {
            //Dropdwon ProductType & TagName
            ViewData["tagId"] = new SelectList(_db.Tags.ToList(), "Id", "TagName");
            ViewData["categoryId"] = new SelectList(_db.Categories.ToList(), "Id", "CategoryName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Products products, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                //Check product Name Is already Available or not Available
                var searchProduct = _db.Products.FirstOrDefault(c => c.ProductTitle == products.ProductTitle);
                if (searchProduct != null)
                {
                    ViewBag.message = "This product is already exist. Please Change Value and Save!!!";
                    //Dropdwon ProductType & TagName
                    ViewData["tagId"] = new SelectList(_db.Tags.ToList(), "Id", "TagName");
                    ViewData["categoryId"] = new SelectList(_db.Categories.ToList(), "Id", "CategoryName");
                    return View(products);
                }

                //Image Loading 
                if (image!=null)
                {
                    var name = Path.Combine(_he.WebRootPath+"/images",Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    products.Image ="images/" + image.FileName;
                }
                if (image==null)
                {
                    products.Image = "images/noimage.jpg";
                }

                //Save processing
                _db.Products.Add(products);
                await _db.SaveChangesAsync();
                TempData["save"] = "Product Add Successfully!!";
                return RedirectToAction(nameof(Index));
            }
            return View(products);
        }
        //Edit Action Page Method
        public IActionResult Edit(int? id)
        {
            //Dropdwon ProductType & TagName
            ViewData["tagId"] = new SelectList(_db.Tags.ToList(), "Id", "TagName");
            ViewData["categoryId"] = new SelectList(_db.Categories.ToList(), "Id", "CategoryName");
            if (id == null)
            {
                return NotFound();
            }
            var product = _db.Products.Include(c => c.Tag).Include(c => c.Category).FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Products products, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    products.Image = "images/" + image.FileName;
                }
                if (image == null)
                {
                    products.Image = "images/noimage.jpg";
                }
                _db.Update(products);
                await _db.SaveChangesAsync();
                TempData["save"] = "Update Successfully!!";
                return RedirectToAction(nameof(Index));
            }
            return View(products);
        }
        //Details Action Page  Method
        public IActionResult Details(int? id)
        {
            //Dropdwon ProductType & TagName
            ViewData["tagId"] = new SelectList(_db.Tags.ToList(), "Id", "TagName");
            ViewData["categoryId"] = new SelectList(_db.Categories.ToList(), "Id", "CategoryName");
            if (id == null)
            {
                return NotFound();
            }
            var product = _db.Products.Include(c => c.Tag).Include(c => c.Category).FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        
        //Delete Action Page Method
        public IActionResult Delete(int? id)
        {
            //Dropdwon ProductType & TagName
            ViewData["tagId"] = new SelectList(_db.Tags.ToList(), "Id", "TagName");
            ViewData["categoryId"] = new SelectList(_db.Categories.ToList(), "Id", "CategoryName");
            if (id == null)
            {
                return NotFound();
            }
            var product = _db.Products.Include(c => c.Tag).Include(c => c.Category).Where(c => c.Id == id).FirstOrDefault();
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Products products, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    products.Image = "images/" + image.FileName;
                }
                if (image == null)
                {
                    products.Image = "images/noimage.jpg";
                }
                _db.Remove(products);
                await _db.SaveChangesAsync();
                TempData["delete"] = "Product Delete Successfully!!";
                return RedirectToAction(nameof(Index));
            }
            return View(products);
        }
    }
}
