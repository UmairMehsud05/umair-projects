using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace Online_Shopping.Models
{
    public class Brand
    {
        public int BrandID { get; set; }

        [Required(ErrorMessage = "Brand Name is Required")]
        public string BrandName { get; set; }
    }

    public class Brand_Method
    {

        public static void AddBrand(Brand brands)
        {
            string q;
            if (brands.BrandID>0)
            {
                q = "update tbl_Brand set BrandName='"+brands.BrandName+"' where BrandID='" + brands.BrandID + "'";
            }
            else
            {
                q = "insert into tbl_Brand values('" + brands.BrandName + "')";
            }
            DB.Query(q);
        }

        public static void Delete(int id)
        {
            string q = "delete tbl_Brand where BrandId='" + id + "'";
            DB.Query(q);
        }

        public static Brand GetBrandByID(int id)
        {
            string q = "select * from tbl_Brand where BrandID='" + id + "'";
            DataTable dt = DB.Table(q);
            Brand brnd = new Brand();
            if(dt.Rows.Count>0)
            {
                brnd.BrandID = Convert.ToInt32(dt.Rows[0]["BrandID"]);
                brnd.BrandName = Convert.ToString(dt.Rows[0]["BrandName"]);
            }
            return brnd;
        }
        public static List<Brand> GetAllBrand()
        {
            string q = "select * from tbl_Brand";
            DataTable dt = DB.Table(q);
            List<Brand> list = new List<Brand>();
            foreach (DataRow dr in dt.Rows)
            {
                Brand brnds = new Brand();
                brnds.BrandID = Convert.ToInt32(dr["BrandID"]);
                brnds.BrandName = Convert.ToString(dr["BrandName"]);
                list.Add(brnds);
            }
            return list;
        }
    }
}