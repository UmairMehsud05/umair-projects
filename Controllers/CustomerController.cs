using Online_Shopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Online_Shopping.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        [HttpGet]
        public ActionResult Index(int? CatId=null,int? BrandID=null, int? ColorID=null, int? SizeID=null)
        {
            LoadDropdowns(CatId,BrandID,ColorID,SizeID);
            return View();
        }

        void LoadDropdowns(int? CatId = null, int? BrandID = null, int? ColorID = null, int? SizeID = null)
        {
            ViewBag.CategoryList = Category_Method.AllCategory();
            ViewBag.SubCategoryList = SubCat_Method.GetAllSubCat();
            ViewBag.BrandList = Brand_Method.GetAllBrand();
            ViewBag.ColorList = Color_Method.GetAllColors();
            ViewBag.SizeList = Size_Method.GetAllSize();
            ViewBag.ProductList = Product_Method.GetAllProduct(CatId, BrandID, ColorID, SizeID);
        }

        [HttpGet]
        public ActionResult CustomerSignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CustomerSignUp(Customer c)
        {
            if (ModelState.IsValid)
            {
                Customer_method.signUp(c);
            }
            return RedirectToAction("CustomerLogIn");
        }

        [HttpGet]
        public ActionResult CustomerLogIn()
        {
            if (Request.Cookies["CusEmail"] != null && Request.Cookies["CusPassword"] != null)
            {
                Customer cu = new Customer();
                cu.CusEmail = Request.Cookies["CusEmail"].Value;
                cu.CusPassword = Request.Cookies["CusPassword"].Value;
                return View(cu);
            }
            return View();
        }

        [HttpGet]
        public ActionResult ProductDetails(int id)
        {
            MyModel pro=new MyModel();
            {
                pro = Product_Method.GetByIdProduct(id);
                ViewBag.sugProducts = Product_Method.suggestsAllProduct();
            }
            return View(pro);
        }



        [HttpPost]
        public ActionResult CustomerLogIn(Customer c)
        {
            

            Customer cus=Customer_method.signIn(c);
            if(cus!=null)
            {
                FormsAuthentication.SetAuthCookie("ali", false);
                Session["CustomerID"] = cus.CustomerID;
                if (c.chkKeepMeSignIn == true)
                {
                    Response.Cookies["CusEmail"].Value = cus.CusEmail;
                    Response.Cookies["CusPassword"].Value = cus.CusPassword;
                    Response.Cookies["CusEmail"].Expires = DateTime.Now.AddDays(10);
                    Response.Cookies["CusPassword"].Expires = DateTime.Now.AddDays(10);
                }
               
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.msg = "Login Failed. Email or Password is invalid.";
                return View();
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Response.Cookies["CusEmail"].Value = "";
            Response.Cookies["CusPassword"].Value = "";
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            return RedirectToAction("CustomerLogIn");
        }

        [HttpGet]
        public ActionResult ForgetPassword()
        {
            return View();
        }


        [HttpPost]
        public ActionResult ForgetPassword(Customer c)
        {
            Customer cus = Customer_method.ForgetPassword(c);
            if(cus!= null)
            {
                return RedirectToAction("CustomerLogIn");
            }
            else
            {
                return View();
            }
        }
    }
}