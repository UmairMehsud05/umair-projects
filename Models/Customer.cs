using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace Online_Shopping.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        public string CusName { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        public string CusEmail { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [StringLength(11, MinimumLength = 10, ErrorMessage = "Enter Correct Contact with pattern '03'  code")]
        public string CusContact { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "Password must be 8-16 characters")]
        public string CusPassword { get; set; }
        public string ConformPassword { get; set; }
        public bool chkKeepMeSignIn { get; set; }
    }

    public class Customer_method
    {
        public static void signUp(Customer c)
        {
            string q = "select * from tbl_Customer where CusEmail='" + c.CusEmail + "'";
            DataTable dt = DB.Table(q);
            if(dt.Rows.Count>0)
            {
                //Already have an account
            }
            else
            {
                q = "insert into tbl_Customer values('" + c.CusName + "','" + c.CusEmail + "','" + c.CusContact + "','" + c.CusPassword + "')";
                DB.Query(q);
            }
        }

        public static Customer signIn(Customer c)
        {
            string q = "select * from tbl_Customer where CusEmail='" + c.CusEmail + "' and CusPassword='" + c.CusPassword + "'";
            DataTable dt = DB.Table(q);
            Customer cus = new Customer();
            if(dt.Rows.Count>0)
            {
                cus.CustomerID = Convert.ToInt32(dt.Rows[0]["CustomerID"]);
                cus.CusName = Convert.ToString(dt.Rows[0]["CusName"]);
                cus.CusEmail = Convert.ToString(dt.Rows[0]["CusEmail"]);
                cus.CusContact = Convert.ToString(dt.Rows[0]["CusContact"]);
                cus.CusPassword = Convert.ToString(dt.Rows[0]["CusPassword"]);
            }
            else
            {
                cus = null;
                //we have to show a message for Register/SignUp/Create an account.
            }
            return cus;
        }


        public static Customer ForgetPassword(Customer c)
        {
            string q = "select * from tbl_Customer where CusEmail='" + c.CusEmail + "'";
            DataTable dt = DB.Table(q);
            if (dt.Rows.Count>0)
            {
                if(c.CusPassword==c.ConformPassword)
                {
                    q = "update tbl_Customer set CusPassword='" + c.ConformPassword + "' where CusEmail='" + c.CusEmail + "'";
                    DB.Query(q);

                    c.CustomerID = Convert.ToInt32(dt.Rows[0]["CustomerID"]);
                    c.CusName = Convert.ToString(dt.Rows[0]["CusName"]);
                    c.CusEmail = Convert.ToString(dt.Rows[0]["CusEmail"]);
                    c.CusContact = Convert.ToString(dt.Rows[0]["CusContact"]);
                    c.CusPassword = c.ConformPassword;
                }
                else
                {
                    c = null;
                }
            }
            else
            {
                c = null;
            }
            return c;
        }
    }
}