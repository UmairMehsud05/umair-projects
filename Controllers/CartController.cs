using Online_Shopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_Shopping.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        [HttpGet]
        public ActionResult AddToCart(int id)
        {
            if (Session["CustomerId"] == null)
                return RedirectToAction("CustomerLogIn", "Customer");
            Cart o = new Cart();
            o.ProductId = id;
            o.CustomerId = Convert.ToInt32(Session["CustomerID"]);
            o.Date = DateTime.Now;
            Cart_Method.addtocart(o);
            return RedirectToAction("Index", "Customer");
        }

        [HttpGet]
        public ActionResult CartDelete(int id)
        {
            Cart_Method.delete(id);
            return RedirectToAction("Cart");
        }

        [HttpGet]
        public ActionResult Cart()
        {
            if (Session["CustomerID"] == null)
                return RedirectToAction("CustomerLogIn", "Customer");
            List<MyModel> list = Cart_Method.GetAllCart(Convert.ToInt32(Session["CustomerID"]));
            return View(list);
        }

        [HttpGet]
        public ActionResult Cartincreament(int id)
        {
            Cart_Method.CartIncreament(id);
            return RedirectToAction("Cart");
        }

        [HttpGet]
        public ActionResult Cartdecreament(int id)
        {
            Cart_Method.CartDecreament(id);
            return RedirectToAction("Cart");
        }
    }
}