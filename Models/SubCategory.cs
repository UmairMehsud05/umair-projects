using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace Online_Shopping.Models
{
    public class SubCategory
    {
        public int SubCatID { get; set; }

        [Required(ErrorMessage ="Category is Required")]
        public int CatID { get; set; }

        [Required(ErrorMessage ="SubCategory Name is Required")]
        public string SubCatName { get; set; }
    }

    public class SubCat_Method
    {
        public static void AddSubCat(SubCategory subcat)
        {
            string q;
            if(subcat.SubCatID>0)
            {
                q = "update tbl_SubCategory set CatID='" + subcat.CatID + "',SubCatName='" + subcat.SubCatName + "' where SubCatID='"+subcat.SubCatID+"'";   
            }
            else
            {
                q = "insert into tbl_SubCategory values('" + subcat.CatID + "','" + subcat.SubCatName + "')";
            }
            DB.Query(q);
        }

        public static void DeleteSubCat(int id)
        {
            string q = "delete tbl_SubCategory where SubCatID='" + id + "'";
            DB.Query(q);
        }

        public static SubCategory GetSubCatByID(int id)
        {
            string q = "select * from tbl_SubCategory where SubCatID='" + id + "'";
            DataTable dt=DB.Table(q);
            SubCategory subCat = new SubCategory();
            if (dt.Rows.Count > 0)
            {
                subCat.SubCatID=Convert.ToInt32(dt.Rows[0]["SubCatID"]);
                subCat.CatID = Convert.ToInt32(dt.Rows[0]["CatID"]);
                subCat.SubCatName = Convert.ToString(dt.Rows[0]["SubCatName"]);
            }
            return subCat;
        }

        public static List<MyModel> GetAllSubCat()
        {
            string q = "select * from vw_SubCategoryDetails";
            DataTable dt=DB.Table(q);
            List<MyModel> list = new List<MyModel>();
            foreach (DataRow dr in dt.Rows)
            {
                MyModel myM = new MyModel()
                {
                    MySubCat = new SubCategory(),
                    MyCat =new Category()
                };
                myM.MySubCat.SubCatID = Convert.ToInt32(dr["SubCatID"]);
                myM.MyCat.CatName = Convert.ToString(dr["CatName"]);
                myM.MySubCat.SubCatName = Convert.ToString(dr["SubCatName"]);
                list.Add(myM);
            }
            return list;
        }
    }
}