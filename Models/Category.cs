using Online_Shopping.Controllers;
using Online_Shopping.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Online_Shopping.Models
{
    public class Category
    {
        public int CatID { get; set; }

        [Required(ErrorMessage ="Category Name is Required")]
        public string CatName { get; set; }
    }

    public class Category_Method
    {
        public static void AddCategory(Category cat)
        {
            if (cat.CatID>0)
            {
                SqlParameter[] up = new SqlParameter[3];
                up[0] = new SqlParameter("@ActionType", Action.Update);
                up[1] = new SqlParameter("@CatName", cat.CatName);
                up[2] = new SqlParameter("@CatID", cat.CatID);
                DB.sp_Query("sp_Category", up);
            }
            else
            {
                SqlParameter[] insi = new SqlParameter[2];
                insi[0] = new SqlParameter("@ActionType", Action.Insert);
                insi[1] = new SqlParameter("@CatName", cat.CatName);
                DB.sp_Query("sp_Category", insi);
            }

            //SqlParameter[] prm = new SqlParameter[3];
            //prm[0] = new SqlParameter("@ActionType", cat.CatID>0? Action.Update : Action.Insert);
            //prm[1] = new SqlParameter("@CatName", cat.CatName);
            //prm[2] = new SqlParameter("@CatID", cat.CatID);
            //DB.sp_Query("sp_Category", prm);


        }

        public static void DeleteCategory(int id)
        {
            SqlParameter[] det = new SqlParameter[2];
            det[0] = new SqlParameter("@ActionType", Action.Delete);
            det[1] = new SqlParameter("@CatID", id);
            DB.sp_Query("sp_Category", det);
        }

        public static Category CatById(int CatID)
        {
            SqlParameter[] sel = new SqlParameter[2];
            sel[0] = new SqlParameter("@ActionType", Action.Select);
            sel[1] = new SqlParameter("@CatID", CatID);
            DataTable dt= DB.sp_Table("sp_Category", sel);
            Category o = new Category();
            if(dt.Rows.Count>0)
            {
                o.CatID = Convert.ToInt32(dt.Rows[0]["CatID"]);
                o.CatName = Convert.ToString(dt.Rows[0]["CatName"]);
            }
            return o;
        }

        public static List<Category> AllCategory()
        {
            SqlParameter[] sel = new SqlParameter[1];
            sel[0] = new SqlParameter("@ActionType", Action.Select);
            DataTable dt= DB.sp_Table("sp_Category", sel);
            List<Category> list = new List<Category>();
            foreach (DataRow dr in dt.Rows)
            {
                Category o = new Category();
                o.CatID = Convert.ToInt32(dr["CatID"]);
                o.CatName = Convert.ToString(dr["CatName"]);
                list.Add(o);
            }
            return list;
        }
    }
}