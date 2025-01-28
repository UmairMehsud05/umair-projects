using Online_Shopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_Shopping.Controllers
{
    public class SubCategoryController : Controller
    {
        // GET: SubCategory

        [HttpGet]
        public ActionResult AddSubCategory(int id = 0)
        {
            SubCategory subCategory = new SubCategory();
            if(id > 0)
            {
                subCategory=SubCat_Method.GetSubCatByID(id);
            }
            SubCategoryList();
            LoadDropDowns();
            return View(subCategory);
        }

        [HttpPost]
        public ActionResult AddSubCategory(SubCategory subcat)
        {
            if(ModelState.IsValid)
            {
                SubCat_Method.AddSubCat(subcat);
            }
            SubCategoryList();
            LoadDropDowns();
            return View("AddSubCategory");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            SubCat_Method.DeleteSubCat(id);
            return RedirectToAction("AddSubCategory");
        }

        void LoadDropDowns()
        {
            ViewBag.CatList = Category_Method.AllCategory();
        }

        public void SubCategoryList()
        {
            List<MyModel> subCategories = SubCat_Method.GetAllSubCat();
            ViewBag.SubCategoryList = subCategories;
        }

        //[HttpGet]
        //public ActionResult SubCategoryList()
        //{
        //    List <SubCategory> list= SubCat_Method.GetAllSubCat();
        //    return View(list);
        //}

    }
}