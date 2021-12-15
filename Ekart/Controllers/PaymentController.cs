using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ekart.Controllers
{
    public class PaymentController : Controller
    {
        // GET: PaymentController
        public ActionResult Index()
        {
            return RedirectToAction("Checkout");
        }

        public IActionResult Checkout(float bill)
        {
            ViewBag.bill = bill;
            return View("Checkout");
        }
        public IActionResult Pay(float bill)
        {
            ViewBag.bill = bill;
            return View("Payment");
        }
    }
}
