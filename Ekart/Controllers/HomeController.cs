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
            ViewBag.CartValue = "10";
            return View();
            //if(HttpContext.Session.GetString())
        }

       

        public IActionResult Delete(int? id)
        {
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
            return View();
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
