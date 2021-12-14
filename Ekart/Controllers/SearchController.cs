﻿using Ekart.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ekart.Controllers
{
    public class SearchController : Controller
    {
        private readonly AppDBContext _db;

        public SearchController(AppDBContext db)
        {
          _db = db;
        }

        // GET: SearchController
        public ActionResult Index()
        {
            return RedirectToAction("ListAll");
        }


        public IActionResult ListAll()
        {
            return View(Product.GetProducts(_db));
        }

        public IActionResult Results(string searchString)
        {

            if (!String.IsNullOrEmpty(searchString))
            {
                HttpContext.Session.SetString("searchString", searchString);
                var products = _db.Product.Where(s => s.Product_Name!.Contains(searchString));
                return View("Search", products.ToList());
            }
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("searchString")))
            {
                searchString = HttpContext.Session.GetString("searchString");
                var products = _db.Product.Where(s => s.Product_Name!.Contains(searchString));
                return View("Search", products.ToList());
            }

            return NotFound();
        }

        public IActionResult Add(uint id)
        {
            //if (ModelState.IsValid)
            //{

            //    _db.Basket.Add(new Basket(ViewBag.id, obj.PID, obj.Product_Name, obj.Product_Quantity, obj.Price, obj.Brand, obj.Measure));
            //    _db.SaveChanges();
            //    return RedirectToAction("HomePage");
            //}
            //return Content(id + " " + name + " " + price);
            //return View();
            string email = HttpContext.Session.GetString("id");
            var obj = _db.Product.Find(id);
            obj.Product_Quantity = 1;
            _db.Basket.Add(new Basket(email, obj.PID, obj.Product_Name, obj.Product_Quantity, obj.Price, obj.Brand, obj.Measure));
            _db.SaveChanges();
            //return Content(email +" " +obj.PID);
            return RedirectToAction("Results") ;
            
        }


    }
}