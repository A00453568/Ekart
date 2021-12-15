using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Ekart.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Ekart.Controllers
{
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;
        private readonly AppDBContext _db;

        public CartController(ILogger<CartController> logger, AppDBContext db)
        {
            _logger = logger;
            _db = db;
        }

        // GET: CartController
        public ActionResult Index()
        {
            return RedirectToAction("Details");
        }

        // GET: CartController/Details/5
        public ActionResult Details()
        {
            return View("OrderSummary", getOrderSummary());
        }

        public void getCartValue()
        {
            string id = HttpContext.Session.GetString("id");
            int cartValue = (int)_db.Basket.Where(cid => cid.email == id).Sum(p => p.Product_Quantity);
            ViewBag.CartValue = cartValue.ToString();
            //return cartValue.ToString();
        }

        public List<OrderSummary> getOrderSummary()
        {
            string id = HttpContext.Session.GetString("id");
            var basket = _db.Basket.Where(cid => cid.email == id).ToList();
            List<OrderSummary> orderSummary = new List<OrderSummary>();
            OrderSummary os;
            foreach(var item in basket)
            {
                os = new OrderSummary();
                os.PID = item.Id;
                os.Email = item.email;
                os.Product_Name = item.Product_Name;
                os.Brand = item.Brand;
                os.Measure = item.Measure;
                os.Product_Quantity = item.Product_Quantity;
                os.Price = item.Price;
                os.Subtotal = item.Price * item.Product_Quantity;
                os.Image_url = item.Image_url;
                orderSummary.Add(os);
            }
            return orderSummary;
        }

    }
}
