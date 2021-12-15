using Ekart.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Ekart.Controllers
{
    
    public class LoginController : Controller
    {
        private readonly AppDBContext _db;

        public LoginController(AppDBContext db)
        {
            _db = db; 
        }

        public IActionResult Index()
        {
            ViewBag.WrongLogin = "".ToString();
            if (SessionCheck()) { return RedirectToAction("HomePage", "Home"); }
            return View("Login");
        }

        [HttpPost]
        public IActionResult Login(Customer obj)
        {
            
            if (SessionCheck()) { return RedirectToAction("HomePage", "Home"); }
            bool sig = obj.Login_Check(_db);

            if (sig)
            {
                ViewBag.id = obj.Id;
                ViewBag.WrongLogin = "".ToString();
                HttpContext.Session.SetString("id", obj.Id);
                return RedirectToAction("HomePage", "Home");
            }
            else
            {
                ViewBag.WrongLogin = "Either your email or password is incorrect".ToString();
                return View("Login");
            }

    }

        [HttpGet]
        public IActionResult Login()
        {

            if (SessionCheck()) { ViewBag.WrongLogin = "";
                return RedirectToAction("HomePage", "Home"); }
            else
            {
                ViewBag.WrongLogin = "Either your email or password is incorrect";
                return RedirectToAction("Index");
            }

        }

        public bool SessionCheck()
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("id"))) { return true; }
            return false;
        }
    }
}
