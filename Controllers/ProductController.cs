using Online_Shopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_Shopping.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product

        [HttpGet]
        public ActionResult AddProduct(int id=0)
        {
            Product pt = new Product();
            if(id > 0)
            {
                pt=Product_Method.GetProductByID(id);
            }
            ProductList();
            LoadDropDowns();
            return View(pt);
        }

        [HttpPost]
        public ActionResult AddProduct(Product prdt)
        {
            if (string.IsNullOrEmpty(prdt.Image) || prdt.MyImage != null)
            {
                prdt.Image = UploadImage(prdt.MyImage);
            }
            //if (prdt.Image != null || prdt.Image != "")
            //{
            //}
            if(ModelState.IsValid)
            {
                Product_Method.AddProduct(prdt);
            }
            ProductList();
            LoadDropDowns();
            return View();
        }

        string UploadImage(HttpPostedFileBase img)
        {
            string uniqueFileName = null;
            if (img != null)
            {
                uniqueFileName = Guid.NewGuid() + img.FileName;
                img.SaveAs(Server.MapPath("~/UploadedImages/") + uniqueFileName);
            }
            return uniqueFileName;
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Product_Method.DeleteProduct(id);
            return RedirectToAction("AddProduct");
        }        

        void LoadDropDowns()
        {
            ViewBag.CatList = Category_Method.AllCategory();
            ViewBag.SubCatList = SubCat_Method.GetAllSubCat();
            ViewBag.ColorList = Color_Method.GetAllColors();
            ViewBag.BrandList = Brand_Method.GetAllBrand();
            ViewBag.SizeList = Size_Method.GetAllSize();
        }
        public void ProductList()
        {
            List<MyModel> list = Product_Method.GetAllProduct();
            ViewBag.ProductList = list;
        }

        //public static dynamic ProductList()
        //{
        //    List<Product> list = Product_Method.GetAllProduct();
        //    return list;
        //}
    }
}