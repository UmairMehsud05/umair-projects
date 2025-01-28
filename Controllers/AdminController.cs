using Online_Shopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_Shopping.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(Admin ad)
        {
            if(ModelState.IsValid)
            {
                Admin_Method.SignUp(ad);
            }
                return View();
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }


        [HttpPost]
        public ActionResult LogIn(Admin ad)
        {
            Admin adm = Admin_Method.SignIn(ad);
            if (adm!= null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
    }
}