using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Online_Shopping.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int QTY { get; set; }
        public int UnitPrice { get; set; }
        public int Discount { get; set; }
        public DateTime Date { get; set; }
    }

    public class Cart_Method
    {
        public static void addtocart(Cart c)
        {
            string q = "select * from tbl_Product where ProductId='" + c.ProductId + "'";
            DataTable dt = DB.Table(q);
            c.UnitPrice = Convert.ToInt32(dt.Rows[0]["SalePrice"]);
            c.Discount = Convert.ToInt32(dt.Rows[0]["Discount"]);
            q = "select * from tbl_Cart where ProductId='" + c.ProductId + "'";
            DataTable data = DB.Table(q);
            if (data.Rows.Count > 0)
            {
                c.QTY = Convert.ToInt32(data.Rows[0]["QTY"]) + 1;
                string query = "update tbl_Cart set QTY='" + c.QTY + "' where ProductId='"+c.ProductId+"'";
                DB.Query(query);
            }
            else
            {
                c.QTY = 1;
                string query = "insert into tbl_Cart values('" + c.CustomerId + "','" + c.ProductId + "','" + c.QTY + "','" + c.UnitPrice + "','" + c.Discount + "','" + c.Date + "')";
                DB.Query(query);
            }
        }

        public static void delete(int id)
        {
            string q = "delete tbl_Cart where CartId='" + id + "'";
            DB.Query(q);
        }

        public static void CartIncreament(int id)
        {
            string q = "select * from tbl_Cart where ProductId='" + id + "'";
            DataTable dt = DB.Table(q);
            int qty = 0;
            qty = Convert.ToInt32(dt.Rows[0]["QTY"]) + 1;

            q = "select * from tbl_Product where ProductId='" + id + "'";
            DataTable Product_DT = DB.Table(q);
            int pQTY = 0;
            pQTY = Convert.ToInt32(Product_DT.Rows[0]["avaQty"]);
            if (dt.Rows.Count > 0 && pQTY>=qty)
            {

                int avqty = 0;
                avqty = pQTY - qty;

                q = "update tbl_Cart set QTY='" + qty++ + "' where CartId='" + Convert.ToInt32(dt.Rows[0]["CartId"]) + "'";
                DB.Query(q);
                
                q = "update tbl_product set avaQty='" + avqty + "' where ProductId='" + id + "'";
                DB.Query(q);

            }
        }

        public static void CartDecreament(int id)
        {

                string q = "select * from tbl_Cart where ProductId='" + id + "'";
            DataTable dt = DB.Table(q);
                int qty = 0;
                qty = Convert.ToInt32(dt.Rows[0]["QTY"]) - 1;

                q = "select * from tbl_Product where ProductId='" + id + "'";
                DataTable Product_DT = DB.Table(q);
                int pQTY = 0;
                pQTY = Convert.ToInt32(Product_DT.Rows[0]["avaQty"]);


                if (dt.Rows.Count > 0)
                {
                if (qty <= 0)
                {
                    int avqty = 0;
                    qty = 1;
                    avqty = pQTY + qty;
                    q = "update tbl_product set avaQty='" + avqty + "' where ProductId='" + id + "'";
                    DB.Query(q);

                    q = "delete tbl_Cart where CartId='" + Convert.ToInt32(dt.Rows[0]["CartId"]) + "'";
                    DB.Query(q);
                }
                else
                {
                    int avqty = 0;
                    avqty = pQTY + qty;

                    q = "update tbl_Cart set QTY='" + qty + "' where CartId='" + Convert.ToInt32(dt.Rows[0]["CartId"]) + "'";
                    DB.Query(q);

                    q = "update tbl_product set avaQty='" + avqty + "' where ProductId='" + id + "'";
                    DB.Query(q);
                }
            }
        }


        public static List<MyModel> GetAllCart(int CustomerID)
        {
            string q = "select * from tbl_Cart c inner join tbl_Product p on c.ProductId=p.ProductID where CustomerId='" + CustomerID + "'";
            DataTable dt = DB.Table(q);
            List<MyModel> list = new List<MyModel>();
            foreach (DataRow dr in dt.Rows)
            {
                MyModel model = new MyModel()
                {
                    MyProduct = new Product(),
                    MyCart = new Cart()
                };
                model.MyCart.CartId = Convert.ToInt32(dr["CartId"]);
                model.MyCart.ProductId = Convert.ToInt32(dr["ProductId"]);
                model.MyCart.CustomerId = Convert.ToInt32(dr["CustomerId"]);
                model.MyCart.QTY = Convert.ToInt32(dr["QTY"]);
                model.MyCart.Date = Convert.ToDateTime(dr["Date"]);
                model.MyCart.Discount = Convert.ToInt32(dr["Discount"]);
                model.MyProduct.ProductName = Convert.ToString(dr["ProductName"]);
                model.MyProduct.SalePrice = Convert.ToInt32(dr["SalePrice"]);
                model.MyProduct.IsSale = Convert.ToBoolean(dr["IsSale"]);
                model.MyProduct.Image = Convert.ToString(dr["Image"]);
                model.MyProduct.newprice = Convert.ToInt32(Convert.ToInt32(model.MyProduct.SalePrice) - Convert.ToInt32(model.MyProduct.SalePrice) * Convert.ToInt32(model.MyCart.Discount) / 100); 
                model.MyCart.UnitPrice = Convert.ToInt32(dr["UnitPrice"]);
                
                list.Add(model);
            }
            return list;
        }

    }
}