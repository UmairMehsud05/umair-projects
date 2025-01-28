using Online_Shopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_Shopping.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        [HttpGet]
        public ActionResult AddCategory(int id=0)
        {
            Category cat = new Category();
            if(id > 0)
            {
                cat=Category_Method.CatById(id);
            }
            CategoryList();
            return View(cat);
        }

        [HttpPost]
        public ActionResult AddCategory(Category cat)
        {
            if (ModelState.IsValid)
            {
                Category_Method.AddCategory(cat);
            }
           
            CategoryList();
            return View();
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Category_Method.DeleteCategory(id);
            return RedirectToAction("AddCategory");
        }

        public void CategoryList()
        {
            List<Category> list = Category_Method.AllCategory();
            ViewBag.CategoryList = list;
        }

        //[HttpGet]
        //public ActionResult CategoryList()
        //{
        //    List<Category> list= Category_Method.AllCategory();
        //    return View(list);
        //}


    }
}