using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace Online_Shopping.Models
{
    public class Color
    {
        public int ColorID { get; set; }

        [Required(ErrorMessage = "Color Name is Required")]
        public string ColorName { get; set; }

        [Required(ErrorMessage = "Color Code is Required")]
        public string ColorCode { get; set; }
    }

    public class Color_Method
    {
        public static void AddColor(Color clr)
        {
            string q;
            if(clr.ColorID>0)
            {
                q = "update tbl_Color set ColorName='"+clr.ColorName+"',ColorCode='"+clr.ColorCode+"' where ColorID='" + clr.ColorID + "'";
            }
            else
            {
                q = "insert into tbl_Color values('" + clr.ColorName + "','" + clr.ColorCode + "')";
            }
            DB.Query(q);
        }

        public static void DeleteColor(int id)
        {
            string q = "delete tbl_Color where ColorID='" + id + "'";
            DB.Query(q);
        }

        public static Color GetColorByID(int id)
        {
            string q = "select * from tbl_Color where ColorID='" + id + "'";
            DataTable dt = DB.Table(q);
            Color clr = new Color();
            if(dt.Rows.Count>0)
            {
                clr.ColorID = Convert.ToInt32(dt.Rows[0]["ColorID"]);
                clr.ColorName = Convert.ToString(dt.Rows[0]["ColorName"]);
                clr.ColorCode = Convert.ToString(dt.Rows[0]["ColorCode"]);
            }
            return clr;
        }

        public static List<Color> GetAllColors()
        {
            string q = "select * from tbl_Color";
            DataTable dt = DB.Table(q);
            List<Color> list = new List<Color>();
            foreach(DataRow dr in dt.Rows)
            {
                Color brnd = new Color();
                brnd.ColorID = Convert.ToInt32(dr["ColorID"]);
                brnd.ColorName = Convert.ToString(dr["ColorName"]);
                brnd.ColorCode = Convert.ToString(dr["ColorCode"]);
                list.Add(brnd);
            }
            return list;
        }
    }
}