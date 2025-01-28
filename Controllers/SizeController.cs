using Online_Shopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_Shopping.Controllers
{
    public class SizeController : Controller
    {
        // GET: Size
        [HttpGet]
        public ActionResult AddSize(int id=0)
        {
            Size sze = new Size();
            if(id>0)
            {
                sze = Size_Method.GetSizeByID(id);
            }
            SizeList();
            return View(sze);
        }

        [HttpPost]
        public ActionResult AddSize(Size sze)
        {
            if(ModelState.IsValid)
            {
                Size_Method.AddSize(sze);
            }
            SizeList();
            return View();
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Size_Method.DeleteSize(id);
            return RedirectToAction("AddSize");
        }
        
        public void SizeList()
        {
            List<Size> sizes = Size_Method.GetAllSize();
            ViewBag.SizeList = sizes;
        }

        //[HttpGet]
        //public ActionResult SizeList()
        //{
        //    List<Size> sze = Size_Method.GetAllSize();
        //    return View(sze);
        //}

    }
}