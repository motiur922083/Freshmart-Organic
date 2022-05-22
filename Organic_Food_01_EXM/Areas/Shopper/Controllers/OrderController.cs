using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Organic_Food_01_EXM.Data;
using Organic_Food_01_EXM.Models;
using Organic_Food_01_EXM.Utility;
using Microsoft.AspNetCore.Authorization;

namespace Organic_Food_01_EXM.Areas.Customer.Controllers
{
    [Area("Shopper")]
    [Authorize]
    public class OrderController : Controller
    {
        private ApplicationDbContext _db;
        public OrderController(ApplicationDbContext db)
        {
            _db=db;
        }

        //Get Checkout Action method
        public IActionResult CheckOut()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CheckOut(OrderProduct anOrder)
        {
            List<Products> products = HttpContext.Session.Get<List<Products>>("products");
            if (products!=null)
            {
                foreach (var product in products)
                {
                    OrderDetails orderDetails = new OrderDetails();
                    orderDetails.ProductId = product.Id;
                    anOrder.OrderDetails.Add(orderDetails);
                }
            }
            anOrder.OrderNo = GetOrderNo();
            _db.OrderProducts.Add(anOrder);
            await _db.SaveChangesAsync();
            HttpContext.Session.Set("products", new List<Products>());
            return View(nameof(CheckOut));
        }
        public string GetOrderNo()
        {
            int rowCount = _db.OrderProducts.ToList().Count() + 1;
            return rowCount.ToString("000");
        }
    }
}
