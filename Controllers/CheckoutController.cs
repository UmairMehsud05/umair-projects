using Online_Shopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_Shopping.Controllers
{
    public class CheckoutController : Controller
    {
        // GET: Order
        [HttpGet]
        public ActionResult Checkout()
        {
                if (Session["CustomerId"] ==null)
                return RedirectToAction("CustomerLogIn","Customer");
            List<MyModel> list = Cart_Method.GetAllCart(Convert.ToInt32(Session["CustomerID"]));
            if(list!=null)
            {
                return View(list);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Checkout(Checkout o)
        {
            if (Session["CustomerId"] == null)
                return RedirectToAction("CustomerLogIn","Customer");
            o.CustomerId = Convert.ToInt32(Session["CustomerId"]);
            o.ShippingCharges = 200;
            o.Date=DateTime.Now;
            o.OrderStatus = 0;
            Order_Method.SaveOrder(o);
            List<MyModel> list = Cart_Method.GetAllCart(Convert.ToInt32(Session["CustomerID"]));
            if (list != null)
            {
                return View(list);
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult OrderList()
        {
            List<MyModel> list= Order_Method.orderlist();
            return View(list);
        }


        [HttpGet]
        public ActionResult PendingOrder(int id)
        {
            Order_Method.PendingOrder(id);
            return RedirectToAction("OrderList");
        }

        [HttpGet]
        public ActionResult ApprovedOrder(int id)
        {
            Order_Method.AprovedOrder(id);
            return RedirectToAction("OrderList");
        }

        [HttpGet]
        public ActionResult RejectedOrder(int id)
        {
            Order_Method.RejectOrder(id);
            return RedirectToAction("OrderList");
        }

    }
}