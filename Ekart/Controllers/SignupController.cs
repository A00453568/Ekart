using Ekart.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ekart.Controllers
{
    public class SignupController : Controller
    {

        private readonly AppDBContext _db;

        public SignupController(AppDBContext db)
        {
            _db = db;

        }
        // GET: Signup
        public ActionResult Index()
        {
            return View("Signup");
        }

        // POST: Signup/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Customer obj)
        {
            try
            {
                obj.Signup(_db);
                return RedirectToAction("Index", "Login");
            }
            catch
            {
                return View();
            }
        }

        
    }
}
