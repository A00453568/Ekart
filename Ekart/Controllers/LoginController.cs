using Ekart.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;

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
            return View("Login");
        }

        [HttpPost]
        public IActionResult Login(Customer obj)
        {
            
            bool sig = obj.Login_Check(_db);

            if (sig)
            {
                ViewBag.id = obj.Id;
                HttpContext.Session.SetString("id", obj.Id);
                return RedirectToAction("HomePage", "Home");
            }
            else
            {
                return RedirectToAction("Index");
            }

        }
    }
}
