using Ekart.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ekart.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly AppDBContext _db;

        public PaymentController(ILogger<PaymentController> logger, AppDBContext db)
        {
            _logger = logger;
            _db = db;
        }
        // GET: PaymentController
        public ActionResult Index()
        {
            return RedirectToAction("Checkout");
        }

        //public IActionResult Checkout(double bill)
        //{
        //    if (SessionCheck())
        //    {
        //        ViewBag.subtotal = HttpContext.Session.GetString("subtot");
        //        ViewBag.bill = bill;
        //    return View("Checkout");
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }
        //}

        //public IActionResult Checkout(OrderSummary obj)
        //{
        //    if (SessionCheck())
        //    {
        //        ViewBag.subtotal = HttpContext.Session.GetString("subtot");
        //        ViewBag.bill = HttpContext.Session.GetString("tot");
        //        return View("Checkout");
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }
        //}

        //public IActionResult Checkout(List<OrderSummary> obj)
        //{
        //    if (SessionCheck())
        //    {
        //        ViewBag.subtotal = HttpContext.Session.GetString("subtot");
        //        ViewBag.bill = HttpContext.Session.GetString("tot");
        //        return View("Checkout");
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }
        //}

        public IActionResult Checkout()
        {
            if (SessionCheck())
            {
                ViewBag.subtotal = HttpContext.Session.GetString("subtot");
                ViewBag.bill = HttpContext.Session.GetString("tot"); 
                return View("Checkout");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Pay()
        {
            if (SessionCheck()) {
                ViewBag.bill = HttpContext.Session.GetString("tot");
                ViewBag.subtotal = HttpContext.Session.GetString("subtot");
                return View("Payment");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }


        }

        public IActionResult Success(double bill)
        {
            if (SessionCheck())
            {
                ViewBag.bill = bill;
                ViewBag.subtotal = HttpContext.Session.GetString("subtot");
                //string script = "window.onload = function(){alert('Order Placed Successfully');}";
                string email = HttpContext.Session.GetString("id");
                var obj = getOrderSummary();
                float trans_value = obj.Sum(i => i.Subtotal);
                uint tid = getTid();
                uint oid = getOid();
                Orders ord;
                foreach (var ob in obj)
                {
                    ord = new Orders();
                    ord.OID = oid;
                    ord.PID = ob.PID;
                    ord.CID = email;
                    ord.Quantity = ob.Product_Quantity;
                    ord.Subtotal = ob.Subtotal;
                    _db.Orders.Add(ord);
                    oid++;
                }
                Transactions trans_ob = new Transactions();
                trans_ob.TID = tid;
                trans_ob.OID = oid;
                trans_ob.Transaction_Value = trans_value;
                _db.Transactions.Add(trans_ob);
                _db.SaveChanges();
                ViewBag.checkout = "success";
                return View("Success");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        public bool SessionCheck()
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("id")))
            {

                return true;
            }
            return false;
        }

        public List<OrderSummary> getOrderSummary()
        {
            string id = HttpContext.Session.GetString("id");
            var basket = _db.Basket.Where(cid => cid.email == id).ToList();
            List<OrderSummary> orderSummary = new List<OrderSummary>();
            OrderSummary os;
            foreach (var item in basket)
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
        public  uint getTid()
        {
            return Convert.ToUInt32(_db.Transactions.Max(t=>t.TID))+1;
             
        }
        public uint getOid()
        {
            return Convert.ToUInt32(_db.Orders.Max(t => t.OID)) + 1;

        }
    }
}
