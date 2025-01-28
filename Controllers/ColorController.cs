using Online_Shopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_Shopping.Controllers
{
    public class ColorController : Controller
    {
        // GET: Color

        [HttpGet]
        public ActionResult AddColor(int id = 0)
        {
            Color color = new Color();
            if (id > 0)
            {
                color = Color_Method.GetColorByID(id);
            }
            ColorList();
            return View(color);
        }

        [HttpPost]
        public ActionResult AddColor(Color clr)
        {
            if (ModelState.IsValid)
            {
                Color_Method.AddColor(clr);
            }
            ColorList();
            return View();
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Color_Method.DeleteColor(id);
            return RedirectToAction("AddColor");
        }

        public void ColorList()
        {
            List<Color> colors=Color_Method.GetAllColors();
            ViewBag.ColorList = colors;
        }

        //[HttpGet]
        //public ActionResult ColorList()
        //{
        //    List<Color> list = Color_Method.GetAllColors();
        //    return View(list);
        //}

    }
}