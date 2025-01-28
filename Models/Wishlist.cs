using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Online_Shopping.Models
{
    public class Wishlist
    {
        public int WishlistId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public DateTime Date { get; set; }
    }
    public class Wishlist_Methods
    {
        public static void Save(Wishlist o)
        {
            string query="insert into tbl_Wishlist values('"+o.CustomerId+"','"+o.ProductId+"','"+o.Date+"')";
            DB.Query(query);
        }
        public static List<MyModel> GetAll(int CustomerId)
        {
            string query = "select * from tbl_Wishlist w inner join tbl_Product p on w.ProductId = p.ProductID  where CustomerId = '" + CustomerId + "'";
            DataTable dt = DB.Table(query);
            List<MyModel> list = new List<MyModel>();
            foreach (DataRow dr in dt.Rows)
            {
                MyModel model = new MyModel()
                {
                    MyWishlist = new Wishlist(),
                    MyProduct = new Product()
                };
                model.MyWishlist.WishlistId = Convert.ToInt32(dr["WishlistId"]);
                model.MyWishlist.ProductId = Convert.ToInt32(dr["ProductId"]);
                model.MyWishlist.CustomerId = Convert.ToInt32(dr["CustomerId"]);
                model.MyWishlist.Date = Convert.ToDateTime(dr["Date"]);
                model.MyProduct.ProductName = Convert.ToString(dr["ProductName"]);
                model.MyProduct.SalePrice = Convert.ToInt32(dr["SalePrice"]);
                model.MyProduct.newprice = Convert.ToInt32(Convert.ToInt32(dr["SalePrice"]) - Convert.ToInt32(dr["SalePrice"]) * Convert.ToInt32(dr["Discount"]) / 100);
                model.MyProduct.avaQty = Convert.ToInt32(dr["avaQty"]);
                model.MyProduct.Image = Convert.ToString(dr["Image"]);
                model.MyProduct.Discount = Convert.ToInt32(dr["Discount"]);
                model.MyProduct.IsSale = Convert.ToBoolean(dr["IsSale"]);
                model.MyProduct.ISAvailable = Convert.ToBoolean(dr["ISAvailable"]);

                list.Add(model);
            }
            return list;
        }
    }
}