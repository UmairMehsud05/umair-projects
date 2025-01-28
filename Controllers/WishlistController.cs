using Online_Shopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_Shopping.Controllers
{
    public class WishlistController : Controller
    {
        [HttpGet]
        public ActionResult AddToWishlist(int pid)
        {
            if (Session["CustomerID"] == null)
                return RedirectToAction("CustomerLogIn", "Customer");
            Wishlist o = new Wishlist();
            o.ProductId = pid;
            o.CustomerId = Convert.ToInt32(Session["CustomerID"]);
            o.Date = DateTime.Now;
            Wishlist_Methods.Save(o);
            return RedirectToAction("Index","Customer");
        }
        [HttpGet]
        public ActionResult Wishlist()
        {
            if (Session["CustomerID"] == null)
                return RedirectToAction("CustomerLogIn", "Customer");
            return View(Wishlist_Methods.GetAll(Convert.ToInt32(Session["CustomerID"])));
        }
    }
}