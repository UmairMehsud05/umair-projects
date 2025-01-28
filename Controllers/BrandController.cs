using Online_Shopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_Shopping.Controllers
{
    public class BrandController : Controller
    {
        // GET: Brand

        [HttpGet]
        public ActionResult AddBrand(int id=0)
        {
            Brand br = new Brand();
            if(id > 0)
            {
                br = Brand_Method.GetBrandByID(id);
            }
            BrandList();
            return View(br);
        }

        [HttpPost]
        public ActionResult AddBrand(Brand brnd)
        {
            if(ModelState.IsValid)
            {
                Brand_Method.AddBrand(brnd);
            }
            BrandList();
            return View();
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Brand_Method.Delete(id);
            return RedirectToAction("AddBrand");
        }

        public void BrandList()
        {
            List<Brand> brands = Brand_Method.GetAllBrand();
            ViewBag.BrandList = brands;
        }

        //[HttpGet]
        //public ActionResult BrandList()
        //{
        //    List<Brand> brands = Brand_Method.GetAllBrand();
        //    return View(brands);
        //}
    }
}