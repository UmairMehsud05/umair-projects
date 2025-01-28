using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace Online_Shopping.Models
{
    public class Size
    {
        public int SizeID { get; set; }

        [Required(ErrorMessage ="Size Name is Required")]
        public string SizeName { get; set; }

        [Required(ErrorMessage ="Size Dimension is RequiredAttribute")]
        public string Dimension { get; set; }
    }

    public class Size_Method
    {
        public static void AddSize(Size size)
        {
            string q;
            if(size.SizeID>0)
            {
                q = "update tbl_Size set SizeName='" + size.SizeName + "',Dimension='" + size.Dimension + "' where SizeID='" + size.SizeID + "'";
            }
            else
            {
                q="insert into tbl_Size values('"+size.SizeName+"','" + size.Dimension + "')";
            }
            DB.Query(q);
        }

        public static void DeleteSize(int id)
        {
            string q= "delete tbl_Size where SizeID='" + id + "'";
            DB.Query(q);
        }

        public static Size GetSizeByID(int id)
        {
            string q= "select * from tbl_Size where SizeID='" + id +"'";
            DataTable dt = DB.Table(q);
            Size sze = new Size();
            if(dt.Rows.Count>0)
            {
                sze.SizeID = Convert.ToInt32(dt.Rows[0]["SizeID"]);
                sze.SizeName = Convert.ToString(dt.Rows[0]["SizeName"]);
                sze.Dimension = Convert.ToString(dt.Rows[0]["Dimension"]);
            }
            return sze;
        }

        public static List<Size> GetAllSize()
        {
            string q = "select * from tbl_Size";
            DataTable dt = DB.Table(q);
            List<Size> list = new List<Size>();
            foreach(DataRow dr in dt.Rows)
            {
                Size sze=new Size();
                sze.SizeID = Convert.ToInt32(dr["SizeID"]);
                sze.SizeName = Convert.ToString(dr["SizeName"]);
                sze.Dimension = Convert.ToString(dr["Dimension"]);
                list.Add(sze);
            }
            return list;
        }
    }
}