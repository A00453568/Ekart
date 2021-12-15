using Ekart.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Ekart.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDBContext _db;

        public HomeController(ILogger<HomeController> logger, AppDBContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {

                if (SessionCheck())
                {
                    return RedirectToAction("HomePage", "Home"); 
                }
                else
                {
                    return View();
                }

        }

        public void getCartValue()
        {
            string id = HttpContext.Session.GetString("id");
            int cartValue = (int)_db.Basket.Where(cid => cid.email == id).Sum(p => p.Product_Quantity);
            ViewBag.CartValue = cartValue.ToString();
            //return cartValue.ToString();
        }

        public IActionResult Delete(int? id)
        {
            getCartValue();
            var obj = _db.Product.Find(id);
            //return View();
            if (obj == null)
            { return NotFound(); }
            _db.Product.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("HomePage");
            //return Content(id + " " + name + " " + price);
        }

        public IActionResult HomePage()
        {


                if (SessionCheck())
                {
                    getCartValue();
                    return View();
                }
                else { return RedirectToAction("Index"); }
   
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Index");
        }
        public bool SessionCheck()
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("id"))) {
                
                return true; }
            return false;
        }
    }
}
