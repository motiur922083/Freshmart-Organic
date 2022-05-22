using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Organic_Food_01_EXM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organic_Food_01_EXM.Areas.Admin.Controllers
{
    [Area("Dashboard")]
    public class DashboardController : Controller
    {
        private ApplicationDbContext _db;
        private IWebHostEnvironment _he;
        public DashboardController(ApplicationDbContext db, IWebHostEnvironment he)
        {
            _db = db;
            _he = he;
        }
        public IActionResult Index()
        {
            return View(_db.OrderProducts.ToList());
        }
        public IActionResult OrderDetails()
        {
            return View(_db.OrderProducts.ToList());
        }
        public IActionResult ContectInfo()
        {
            return View(_db.ContactInfos.ToList());
        }
    }
}
