using Online_Shopping.Controllers;
using Online_Shopping.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Windows;

namespace Online_Shopping.Models
{
    
    public class Admin
    {
        public int AdminID { get; set; }

        [Required(ErrorMessage ="Name is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Email is Required")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Email is invalid")]

        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [StringLength(11,MinimumLength = 10,ErrorMessage = "Enter Correct Contact with pattern '03'  code")]
        public string Contact { get; set; }

        [Required(ErrorMessage ="Password is Required")]
        [StringLength(16,MinimumLength =8,ErrorMessage ="Password must be 8-16 characters")]
        public string Password { get; set; }
        public string image { get; set; }
    }

    public class Admin_Method
    {
        
        public static void SignUp(Admin ad)
        {
            SqlParameter[] sign=new SqlParameter[2];
            sign[0] = new SqlParameter("@ActionType", Action.Select);
            sign[1] = new SqlParameter("@Email", ad.Email);
            DataTable dt = DB.sp_Table("sp_Admin", sign);
            if(dt.Rows.Count > 0)
            {
                //MessageBox.Show("Already have an Account");
            }
            else
            {
                SqlParameter[] insi = new SqlParameter[6];
                insi[0] = new SqlParameter("@ActionType", Action.Insert);
                insi[1] = new SqlParameter("@Name", ad.Name);
                insi[2] = new SqlParameter("@Email", ad.Email);
                insi[3] = new SqlParameter("@Contact", ad.Contact);
                insi[4] = new SqlParameter("@Password", ad.Password);
                insi[5] = new SqlParameter("@image", ad.image);
                DB.sp_Query("sp_Admin", insi);
            }
        }
        public static Admin SignIn(Admin ad)
        {
            SqlParameter[] sgin = new SqlParameter[3];
            sgin[0] = new SqlParameter("@ActionType", Action.Select);
            sgin[1] = new SqlParameter("@Email", ad.Email);
            sgin[2] = new SqlParameter("@Password", ad.Password);
            DataTable dt = DB.sp_Table("sp_Admin", sgin);
            Admin adm = new Admin();
            if(dt.Rows.Count>0)
            {
            adm.AdminID = Convert.ToInt32(dt.Rows[0]["AdminID"]);
            adm.Name = Convert.ToString(dt.Rows[0]["Name"]);
            adm.Email = Convert.ToString(dt.Rows[0]["Email"]);
            adm.Password = Convert.ToString(dt.Rows[0]["Password"]);
            adm.Contact = Convert.ToString(dt.Rows[0]["Contact"]);
            }
            else
            {
                adm = null;
            }
            return adm;
        }

        public static Admin Profile(int id)
        {
            SqlParameter[] sgin = new SqlParameter[2];
            sgin[0] = new SqlParameter("@ActionType", Action.Select);
            sgin[1] = new SqlParameter("@AdminID", id);
            DataTable dt = DB.sp_Table("sp_Admin", sgin);
            Admin adm = new Admin();
            if (dt.Rows.Count > 0)
            {
                adm.AdminID = Convert.ToInt32(dt.Rows[0]["AdminID"]);
                adm.Name = Convert.ToString(dt.Rows[0]["Name"]);
                adm.Email = Convert.ToString(dt.Rows[0]["Email"]);
                adm.Password = Convert.ToString(dt.Rows[0]["Password"]);
                adm.Contact = Convert.ToString(dt.Rows[0]["Contact"]);
                adm.image = Convert.ToString(dt.Rows[0]["image"]);
            }
            return adm;
        }

    }
}