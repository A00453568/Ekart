using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Ekart.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System;

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
            if (SessionCheck())
            {
                var ordersumm = getOrderSummary();
                double subtot = ordersumm.Sum(i => i.Subtotal);
                double tax = Math.Round(subtot * 0.15, 2);
                double tot = subtot + tax;
                HttpContext.Session.SetString("subtot", subtot.ToString());
                HttpContext.Session.SetString("tax", tax.ToString());
                HttpContext.Session.SetString("tot", tot.ToString());
                return RedirectToAction("Details");
            }
            else
            {
                return RedirectToAction("Index","Home");
            }
        }

        // GET: CartController/Details/5
        public ActionResult Details()
        {
            if (SessionCheck())
            {
                var ordersumm = getOrderSummary();
                double subtot = ordersumm.Sum(i => i.Subtotal);
                double tax = Math.Round(subtot * 0.15, 2);
                double tot = subtot + tax;
                HttpContext.Session.SetString("subtot", subtot.ToString());
                HttpContext.Session.SetString("tax", tax.ToString());
                HttpContext.Session.SetString("tot", tot.ToString());
                return View("OrderSummary", ordersumm);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
            

        public void getCartValue()
        {
            string id = HttpContext.Session.GetString("id");
            int cartValue = (int)_db.Basket.Where(cid => cid.email == id).Sum(p => p.Product_Quantity);
            ViewBag.CartValue = cartValue.ToString();
            //return cartValue.ToString();
        }

        public ActionResult Delete(string id)
        {
            if (SessionCheck())
            {
                string email = HttpContext.Session.GetString("id");
                var obj2 = _db.Basket.Where(cid => cid.email == email && cid.Id==Convert.ToUInt32(id)).ToList();
                //return View();
                if (obj2 == null)
                { return NotFound(); }
                foreach(var ob in obj2) { _db.Basket.Remove(ob); }
                _db.SaveChanges();
                getCartValue();
                var ordersumm = getOrderSummary();
                double subtot = ordersumm.Sum(i => i.Subtotal);
                double tax = Math.Round(subtot * 0.15, 2);
                double tot = subtot + tax;
                HttpContext.Session.SetString("subtot", subtot.ToString());
                HttpContext.Session.SetString("tax", tax.ToString());
                HttpContext.Session.SetString("tot", tot.ToString());
                return View("OrderSummary", ordersumm);
            
            }
            return RedirectToAction("Index", "Home");
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

        public bool SessionCheck()
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("id")))
            {

                return true;
            }
            return false;
        }

    }
}
