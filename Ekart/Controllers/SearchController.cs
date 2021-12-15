using Ekart.Models;
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
            if (SessionCheck())
            {
                getCartValue();
                ViewBag.Count = "Showing All".ToString();
                if (String.IsNullOrEmpty(ViewBag.WishList)) { ViewBag.WishList = "0"; }
                return RedirectToAction("ListAll");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }


        public IActionResult ListAll()
        {
            if (SessionCheck())
            {
                getCartValue();
                if (String.IsNullOrEmpty(ViewBag.WishList)) { ViewBag.WishList = "0"; }
                ViewBag.Count = "Showing All".ToString();
                return View("Search", Product.GetProducts(_db));
            }
            else
            {
                return RedirectToAction("Index", "Home");

            }
        }

        public IActionResult Results(string searchString)
        {
            if (SessionCheck())
            {
                getCartValue();
                if (String.IsNullOrEmpty(ViewBag.WishList)) { ViewBag.WishList = "0"; }
                if (!String.IsNullOrEmpty(searchString))
                {
                    HttpContext.Session.SetString("searchString", searchString);
                    var products = _db.Product.Where(s => s.Product_Name!.Contains(searchString));
                    ViewBag.Count = products.Count();
                    return View("Search", products.ToList());
                }
                ViewBag.Count = "Showing All".ToString();
                return RedirectToAction("ListAll");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Ascending(string searchString)
        {
            if (SessionCheck())
            {

                getCartValue();
                if (!String.IsNullOrEmpty(searchString))
                {
                    HttpContext.Session.SetString("searchString", searchString);
                    var products = _db.Product.Where(s => s.Product_Name!.Contains(searchString));
                    ViewBag.Count = products.Count();
                    return View("Search", products.ToList().OrderBy(x => x.Price));
                }
                else if (!String.IsNullOrEmpty(HttpContext.Session.GetString("searchString")))
                {
                    searchString = HttpContext.Session.GetString("searchString");
                    var products = _db.Product.Where(s => s.Product_Name!.Contains(searchString));
                    ViewBag.Count = products.Count();
                    return View("Search", products.ToList().OrderBy(x => x.Price));
                }
                ViewBag.Count = "Showing All".ToString();
                return View("Search", Product.GetProducts(_db).OrderBy(x => x.Price));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Descending(string searchString)
        {
            if (SessionCheck())
            {
                getCartValue();
            if (!String.IsNullOrEmpty(searchString))
            {
                HttpContext.Session.SetString("searchString", searchString);
                var products = _db.Product.Where(s => s.Product_Name!.Contains(searchString));
                ViewBag.Count = products.Count();
                    ViewBag.SearchTerm = HttpContext.Session.GetString("searchString");
                    return View("Search", products.ToList().OrderByDescending(x => x.Price));
            }
            else if(!String.IsNullOrEmpty(HttpContext.Session.GetString("searchString")))
            {
                searchString = HttpContext.Session.GetString("searchString");
                var products = _db.Product.Where(s => s.Product_Name!.Contains(searchString));
                ViewBag.Count = products.Count();
                    ViewBag.SearchTerm = HttpContext.Session.GetString("searchString");
                    return View("Search", products.ToList().OrderByDescending(x => x.Price));
            }
            ViewBag.Count = "Showing All".ToString();
            return View("Search", Product.GetProducts(_db).OrderByDescending(x => x.Price));
        }
            else
            {
                return RedirectToAction("Index", "Home");
    }
}

        public IActionResult Add(uint id)
        {
            getCartValue();
            string email = HttpContext.Session.GetString("id");
            var obj = _db.Product.Find(id);
            int PID = Convert.ToInt32(obj.PID);
            var obj2 = _db.Basket.FirstOrDefault(bas => bas.Id == PID);


            if (obj2 == null)
            {

                _db.Basket.Add(new Basket(email, obj.PID, obj.Product_Name, 1, obj.Price, obj.Brand, obj.Measure,obj.Image_url));

            }
            else
            {
                obj2.Product_Quantity += 1;

            }
            _db.SaveChanges();
            return RedirectToAction("Results");

        }


        public IActionResult AddHeart()
        {
            string Wishlist = HttpContext.Session.GetString("Wishlist");
            int heart = 0;
            if (!String.IsNullOrEmpty(Wishlist))
            {
                heart = Convert.ToInt32(Wishlist);
                heart++;
                ViewBag.WishList = heart.ToString();
                HttpContext.Session.SetString("Wishlist", heart.ToString());
            }
            else {
                heart = 1;
                ViewBag.WishList = heart.ToString();
                HttpContext.Session.SetString("Wishlist", heart.ToString()); }
            return RedirectToAction("Results");

        }

        public void getCartValue()
        {
            string id = HttpContext.Session.GetString("id");
            int cartValue = (int)_db.Basket.Where(cid => cid.email == id).Sum(p => p.Product_Quantity);
            ViewBag.CartValue = cartValue.ToString();
            //return cartValue.ToString();
        }

        public bool SessionCheck()
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("id")))
            {
                ViewBag.WishList = HttpContext.Session.GetString("Wishlist");
                ViewBag.SearchTerm = HttpContext.Session.GetString("searchString");
                return true;
            }
            return false;
        }
    }
}
