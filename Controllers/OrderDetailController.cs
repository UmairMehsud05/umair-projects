using Online_Shopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_Shopping.Controllers
{
    public class OrderDetailController : Controller
    {
        // GET: OrderDetail
        public ActionResult OrderDetails(int id)
        {
            List<MyModel> list = OrderDetails_Method.OrderDetails(id);
            return View(list);
        }
    }
}