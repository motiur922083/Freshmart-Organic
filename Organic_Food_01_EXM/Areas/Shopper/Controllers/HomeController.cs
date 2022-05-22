using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Organic_Food_01_EXM.Data;
using Organic_Food_01_EXM.Models;
using Organic_Food_01_EXM.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Organic_Food_01_EXM.Controllers
{
    [Area("Shopper")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationDbContext _db;
        private IWebHostEnvironment _he;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db, IWebHostEnvironment he)
        {
            _db = db;
            _he = he;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(_db.Products.Include(f => f.Category).ToList());
        }
        //Product Page
        public IActionResult Product(int id = 1)
        {
            int TotalVegetablesQnt = _db.Products.Where(c => c.Category.CategoryName == "Vegetable").Count();
            ViewBag.Vegetable = TotalVegetablesQnt;
            int TotalFruitsQnt = _db.Products.Where(c => c.Category.CategoryName == "Fruit").Count();
            ViewBag.Fruit = TotalFruitsQnt;
            int TotalBreadQnt = _db.Products.Where(c => c.Category.CategoryName == "Bread").Count();
            ViewBag.Bread = TotalBreadQnt;
            int TotalJuicesQnt = _db.Products.Where(c => c.Category.CategoryName == "Juice").Count();
            ViewBag.Juice = TotalJuicesQnt;
            int TotalTeaCoffeeQnt = _db.Products.Where(c => c.Category.CategoryName == "TeaCoffee").Count();
            ViewBag.TeaCoffee = TotalTeaCoffeeQnt;
            //item
            int TotalTomatoQnt = _db.Products.Where(c => c.Category.CategoryName == "Tomato").Count();
            ViewBag.Tomato = TotalTomatoQnt;
            int TotalBroccoliQnt = _db.Products.Where(c => c.Category.CategoryName == "Broccoli").Count();
            ViewBag.Broccoli = TotalBroccoliQnt;

            //Product Color Count
            int PinkColorQnt = _db.Products.Where(c => c.ProductColor == "Pink").Count();
            ViewBag.Pink = PinkColorQnt;
            int BlueColorQnt = _db.Products.Where(c => c.ProductColor == "Blue").Count();
            ViewBag.Blue = BlueColorQnt;
            int YellowColorQnt = _db.Products.Where(c => c.ProductColor == "Yellow").Count();
            ViewBag.Yellow = YellowColorQnt;
            int GreenColorQnt = _db.Products.Where(c => c.ProductColor == "Green").Count();
            ViewBag.Green = GreenColorQnt;
            int BrownColorQnt = _db.Products.Where(c => c.ProductColor == "Brown").Count();
            ViewBag.Brown = BrownColorQnt;
            int RedColorQnt = _db.Products.Where(c => c.ProductColor == "Red").Count();
            ViewBag.Red = RedColorQnt;

         
            //Paination Start
            var data = _db.Products
                .OrderBy(c => c.Id)
                .Skip((id - 1) * 9)
                .Take(9);
            int totalPage = (int)Math.Ceiling((decimal)_db.Products.Count() / 9);
            ViewBag.Total = totalPage;
            ViewBag.CurrentPage = id;


            //return View(_db.Products.Include(f => f.ProductTag).ToList().ToPagedList(page ?? 1, 8));
            return View(data);
        }
        //Search Action
        [HttpGet]
        public async Task<IActionResult> Search(string SearchText)
        {
            int TotalVegetablesQnt = _db.Products.Where(c => c.Category.CategoryName == "Vegetable").Count();
            ViewBag.Vegetable = TotalVegetablesQnt;
            int TotalFruitsQnt = _db.Products.Where(c => c.Category.CategoryName == "Fruit").Count();
            ViewBag.Fruit = TotalFruitsQnt;
            int TotalBreadQnt = _db.Products.Where(c => c.Category.CategoryName == "Bread").Count();
            ViewBag.Bread = TotalBreadQnt;
            int TotalJuicesQnt = _db.Products.Where(c => c.Category.CategoryName == "Juice").Count();
            ViewBag.Juice = TotalJuicesQnt;
            int TotalTeaCoffeeQnt = _db.Products.Where(c => c.Category.CategoryName == "TeaCoffee").Count();
            ViewBag.TeaCoffee = TotalTeaCoffeeQnt;
            //item
            int TotalTomatoQnt = _db.Products.Where(c => c.Category.CategoryName == "Tomato").Count();
            ViewBag.Tomato = TotalTomatoQnt;
            int TotalBroccoliQnt = _db.Products.Where(c => c.Category.CategoryName == "Broccoli").Count();
            ViewBag.Broccoli = TotalBroccoliQnt;

            //Product Color Count
            int PinkColorQnt = _db.Products.Where(c => c.ProductColor == "Pink").Count();
            ViewBag.Pink = PinkColorQnt;
            int BlueColorQnt = _db.Products.Where(c => c.ProductColor == "Blue").Count();
            ViewBag.Blue = BlueColorQnt;
            int YellowColorQnt = _db.Products.Where(c => c.ProductColor == "Yellow").Count();
            ViewBag.Yellow = YellowColorQnt;
            int GreenColorQnt = _db.Products.Where(c => c.ProductColor == "Green").Count();
            ViewBag.Green = GreenColorQnt;
            int BrownColorQnt = _db.Products.Where(c => c.ProductColor == "Brown").Count();
            ViewBag.Brown = BrownColorQnt;
            int RedColorQnt = _db.Products.Where(c => c.ProductColor == "Red").Count();
            ViewBag.Red = RedColorQnt;

            //Searching
            ViewData["GetProduct"] = SearchText;
            var product = from x in _db.Products select x;
            if (!string.IsNullOrEmpty(SearchText))
            {
                product = product.Where(x => x.ProductTitle.Contains(SearchText));
            }

            var cd = await product.AsNoTracking().ToListAsync();

            return View(cd);
        }
        //Details Action Page  Method
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _db.Products.Include(c => c.Category).FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        [HttpPost]
        [ActionName("Details")]
        public IActionResult ProductDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _db.Products.Include(c => c.Category).Include(c => c.Tag).FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            //Session Get Data
            List<Products> products = new List<Products>();
            products = HttpContext.Session.Get<List<Products>>("products");
            if (products == null)
            {
                products = new List<Products>();
            }
            products.Add(product);
            HttpContext.Session.Set("products", products);
            return RedirectToAction(nameof(Index));
        }
        //Remove Cart Action Page  Method
        public IActionResult Remove(int? id)
        {
            //Session Get Data
            List<Products> products = HttpContext.Session.Get<List<Products>>("products");
            if (products != null)
            {
                var product = products.FirstOrDefault(c => c.Id == id);
                if (product != null)
                {
                    products.Remove(product);
                    HttpContext.Session.Set("products", products);
                }
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ActionName("Remove")]
        public IActionResult RemoveToCart(int? id)
        {
            //Session Get Data
            List<Products> products = HttpContext.Session.Get<List<Products>>("products");
            if (products != null)
            {
                var product = products.FirstOrDefault(c => c.Id == id);
                if (product != null)
                {
                    products.Remove(product);
                    HttpContext.Session.Set("products", products);
                }
            }
            return RedirectToAction(nameof(Index));
        }
        //Card Action Page  Method
        public IActionResult Cart()
        {
            //Session Get Data
            List<Products> products = HttpContext.Session.Get<List<Products>>("products");
            if (products == null)
            {
                products = new List<Products>();
            }
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Vegetable(int page = 1)
        {
            int TotalVegetablesQnt = _db.Products.Where(c => c.Category.CategoryName == "Vegetable").Count();
            ViewBag.Vegetable = TotalVegetablesQnt;

            int TotalFruitsQnt = _db.Products.Where(c => c.Category.CategoryName == "Fruit").Count();
            ViewBag.Fruit = TotalFruitsQnt;

            int TotalBreadQnt = _db.Products.Where(c => c.Category.CategoryName == "Bread").Count();
            ViewBag.Bread = TotalBreadQnt;

            int TotalJuicesQnt = _db.Products.Where(c => c.Category.CategoryName == "Juice").Count();
            ViewBag.Juice = TotalJuicesQnt;

            int TotalTeaCoffeeQnt = _db.Products.Where(c => c.Category.CategoryName == "TeaCoffee").Count();
            ViewBag.TeaCoffee = TotalTeaCoffeeQnt;

            //item
            int TotalTomatoQnt = _db.Products.Where(c => c.Category.CategoryName == "Tomato").Count();
            ViewBag.Tomato = TotalTomatoQnt;

            int TotalBroccoliQnt = _db.Products.Where(c => c.Category.CategoryName == "Broccoli").Count();
            ViewBag.Broccoli = TotalBroccoliQnt;

            var data = _db.Products.Where(c => c. Category.CategoryName == "Vegetable").OrderBy(c => c.Id).Skip((page - 1) * 9).Take(9);
            int totalpage = (int)Math.Ceiling((decimal)data.Count() / 9);
            ViewBag.total = totalpage;
            ViewBag.CurrentPage = page;

            //var Product = _db.Products.Where(c => c.Category == "Vegetables").ToList();
            return View(data);
        }
        public IActionResult Fruit(int page = 1)
        {
            int TotalVegetablesQnt = _db.Products.Where(c => c.Category.CategoryName == "Vegetable").Count();
            ViewBag.Vegetable = TotalVegetablesQnt;

            int TotalFruitsQnt = _db.Products.Where(c => c.Category.CategoryName == "Fruit").Count();
            ViewBag.Fruit = TotalFruitsQnt;

            int TotalBreadQnt = _db.Products.Where(c => c.Category.CategoryName == "Bread").Count();
            ViewBag.Bread = TotalBreadQnt;

            int TotalJuicesQnt = _db.Products.Where(c => c.Category.CategoryName == "Juice").Count();
            ViewBag.Juice = TotalJuicesQnt;

            int TotalTeaCoffeeQnt = _db.Products.Where(c => c.Category.CategoryName == "TeaCoffee").Count();
            ViewBag.TeaCoffee = TotalTeaCoffeeQnt;

            //item
            int TotalTomatoQnt = _db.Products.Where(c => c.Category.CategoryName == "Tomato").Count();
            ViewBag.Tomato = TotalTomatoQnt;

            int TotalBroccoliQnt = _db.Products.Where(c => c.Category.CategoryName == "Broccoli").Count();
            ViewBag.Broccoli = TotalBroccoliQnt;

            var data = _db.Products.Where(c => c.Category.CategoryName == "Fruit").OrderBy(c => c.Id).Skip((page - 1) * 9).Take(9);
            int totalpage = (int)Math.Ceiling((decimal)data.Count() / 9);
            ViewBag.total = totalpage;
            ViewBag.CurrentPage = page;

            //var Product = _db.Products.Where(c => c.Category == "Fruit").ToList();
            return View(data);
        }
        public IActionResult Bread(int page = 1)
        {
            int TotalVegetablesQnt = _db.Products.Where(c => c.Category.CategoryName == "Vegetable").Count();
            ViewBag.Vegetable = TotalVegetablesQnt;

            int TotalFruitsQnt = _db.Products.Where(c => c.Category.CategoryName == "Fruit").Count();
            ViewBag.Fruit = TotalFruitsQnt;

            int TotalBreadQnt = _db.Products.Where(c => c.Category.CategoryName == "Bread").Count();
            ViewBag.Bread = TotalBreadQnt;

            int TotalJuicesQnt = _db.Products.Where(c => c.Category.CategoryName == "Juice").Count();
            ViewBag.Juice = TotalJuicesQnt;

            int TotalTeaCoffeeQnt = _db.Products.Where(c => c.Category.CategoryName == "TeaCoffee").Count();
            ViewBag.TeaCoffee = TotalTeaCoffeeQnt;

            //item
            int TotalTomatoQnt = _db.Products.Where(c => c.Category.CategoryName == "Tomato").Count();
            ViewBag.Tomato = TotalTomatoQnt;

            int TotalBroccoliQnt = _db.Products.Where(c => c.Category.CategoryName == "Broccoli").Count();
            ViewBag.Broccoli = TotalBroccoliQnt;

            var data = _db.Products.Where(c => c.Category.CategoryName == "Bread").OrderBy(c => c.Id).Skip((page - 1) * 9).Take(9);
            int totalpage = (int)Math.Ceiling((decimal)data.Count() / 9);
            ViewBag.total = totalpage;
            ViewBag.CurrentPage = page;

            //var Product = _db.Products.Where(c => c.Category == "Bread").ToList();
            return View(data);
        }
        public IActionResult Juice(int page = 1)
        {
            int TotalVegetablesQnt = _db.Products.Where(c => c.Category.CategoryName == "Vegetable").Count();
            ViewBag.Vegetable = TotalVegetablesQnt;

            int TotalFruitsQnt = _db.Products.Where(c => c.Category.CategoryName == "Fruit").Count();
            ViewBag.Fruit = TotalFruitsQnt;

            int TotalBreadQnt = _db.Products.Where(c => c.Category.CategoryName == "Bread").Count();
            ViewBag.Bread = TotalBreadQnt;

            int TotalJuicesQnt = _db.Products.Where(c => c.Category.CategoryName == "Juice").Count();
            ViewBag.Juice = TotalJuicesQnt;

            int TotalTeaCoffeeQnt = _db.Products.Where(c => c.Category.CategoryName == "TeaCoffee").Count();
            ViewBag.TeaCoffee = TotalTeaCoffeeQnt;

            //item
            int TotalTomatoQnt = _db.Products.Where(c => c.Category.CategoryName == "Tomato").Count();
            ViewBag.Tomato = TotalTomatoQnt;

            int TotalBroccoliQnt = _db.Products.Where(c => c.Category.CategoryName == "Broccoli").Count();
            ViewBag.Broccoli = TotalBroccoliQnt;

            var data = _db.Products.Where(c => c.Category.CategoryName == "Juice").OrderBy(c => c.Id).Skip((page - 1) * 9).Take(9);
            int totalpage = (int)Math.Ceiling((decimal)data.Count() / 9);
            ViewBag.total = totalpage;
            ViewBag.CurrentPage = page;

            //var Product = _db.Products.Where(c => c.Category == "Juice").ToList();
            return View(data);
        }
        public IActionResult TeaCoffee(int page = 1)
        {
            int TotalVegetablesQnt = _db.Products.Where(c => c.Category.CategoryName == "Vegetable").Count();
            ViewBag.Vegetable = TotalVegetablesQnt;

            int TotalFruitsQnt = _db.Products.Where(c => c.Category.CategoryName == "Fruit").Count();
            ViewBag.Fruit = TotalFruitsQnt;

            int TotalBreadQnt = _db.Products.Where(c => c.Category.CategoryName == "Bread").Count();
            ViewBag.Bread = TotalBreadQnt;

            int TotalJuicesQnt = _db.Products.Where(c => c.Category.CategoryName == "Juice").Count();
            ViewBag.Juice = TotalJuicesQnt;

            int TotalTeaCoffeeQnt = _db.Products.Where(c => c.Category.CategoryName == "TeaCoffee").Count();
            ViewBag.TeaCoffee = TotalTeaCoffeeQnt;

            //item
            int TotalTomatoQnt = _db.Products.Where(c => c.Category.CategoryName == "Tomato").Count();
            ViewBag.Tomato = TotalTomatoQnt;

            int TotalBroccoliQnt = _db.Products.Where(c => c.Category.CategoryName == "Broccoli").Count();
            ViewBag.Broccoli = TotalBroccoliQnt;

            var data = _db.Products.Where(c => c.Category.CategoryName == "TeaCoffee").OrderBy(c => c.Id).Skip((page - 1) * 9).Take(9);
            int totalpage = (int)Math.Ceiling((decimal)data.Count() / 9);
            ViewBag.total = totalpage;
            ViewBag.CurrentPage = page;

            //var Product = _db.Products.Where(c => c.Category == "TeaCoffee").ToList();
            return View(data);
        }
        public IActionResult Tomato(int page = 1)
        {
            int TotalVegetablesQnt = _db.Products.Where(c => c.Category.CategoryName == "Vegetable").Count();
            ViewBag.Vegetable = TotalVegetablesQnt;

            int TotalFruitsQnt = _db.Products.Where(c => c.Category.CategoryName == "Fruit").Count();
            ViewBag.Fruit = TotalFruitsQnt;

            int TotalBreadQnt = _db.Products.Where(c => c.Category.CategoryName == "Bread").Count();
            ViewBag.Bread = TotalBreadQnt;

            int TotalJuicesQnt = _db.Products.Where(c => c.Category.CategoryName == "Juice").Count();
            ViewBag.Juice = TotalJuicesQnt;

            int TotalTeaCoffeeQnt = _db.Products.Where(c => c.Category.CategoryName == "TeaCoffee").Count();
            ViewBag.TeaCoffee = TotalTeaCoffeeQnt;

            //item
            int TotalTomatoQnt = _db.Products.Where(c => c.Category.CategoryName == "Tomato").Count();
            ViewBag.Tomato = TotalTomatoQnt;

            int TotalBroccoliQnt = _db.Products.Where(c => c.Category.CategoryName == "Broccoli").Count();
            ViewBag.Broccoli = TotalBroccoliQnt;

            var data = _db.Products.Where(c => c.Category.CategoryName == "Tomato").OrderBy(c => c.Id).Skip((page - 1) * 9).Take(9);
            int totalpage = (int)Math.Ceiling((decimal)data.Count() / 9);
            ViewBag.total = totalpage;
            ViewBag.CurrentPage = page;

            //var Product = _db.Products.Where(c => c.Category == "Tomato").ToList();
            return View(data);
        }
        public IActionResult Broccoli(int page = 1)
        {
            int TotalVegetablesQnt = _db.Products.Where(c => c.Category.CategoryName == "Vegetable").Count();
            ViewBag.Vegetable = TotalVegetablesQnt;

            int TotalFruitsQnt = _db.Products.Where(c => c.Category.CategoryName == "Fruit").Count();
            ViewBag.Fruit = TotalFruitsQnt;

            int TotalBreadQnt = _db.Products.Where(c => c.Category.CategoryName == "Bread").Count();
            ViewBag.Bread = TotalBreadQnt;

            int TotalJuicesQnt = _db.Products.Where(c => c.Category.CategoryName == "Juice").Count();
            ViewBag.Juice = TotalJuicesQnt;

            int TotalTeaCoffeeQnt = _db.Products.Where(c => c.Category.CategoryName == "TeaCoffee").Count();
            ViewBag.TeaCoffee = TotalTeaCoffeeQnt;

            //item
            int TotalTomatoQnt = _db.Products.Where(c => c.Category.CategoryName == "Tomato").Count();
            ViewBag.Tomato = TotalTomatoQnt;

            int TotalBroccoliQnt = _db.Products.Where(c => c.Category.CategoryName == "Broccoli").Count();
            ViewBag.Broccoli = TotalBroccoliQnt;

            var data = _db.Products.Where(c => c.Category.CategoryName == "Broccoli").OrderBy(c => c.Id).Skip((page - 1) * 9).Take(9);
            int totalpage = (int)Math.Ceiling((decimal)data.Count() / 9);
            ViewBag.total = totalpage;
            ViewBag.CurrentPage = page;

            //var Product = _db.Products.Where(c => c.Category == "Broccoli").ToList();
            return View(data);
        }

    }
}
