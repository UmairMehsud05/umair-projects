using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Online_Shopping.Models
{
    public class OrderDetails
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int QTY { get; set; }
        public int UnitPrice { get; set; }
        public int Discount { get; set; }
        public int newPrice { get; set; }

    }

    public class OrderDetails_Method
    {
        public static List<MyModel> OrderDetails(int id)
        {
            string q = "select * from tbl_OrderDetail od inner join tbl_Product p on od.ProductId=p.ProductId where OrderId='" + id + "'";
            DataTable dt = DB.Table(q);
            List<MyModel> list = new List<MyModel>();
            foreach(DataRow dr in dt.Rows)
            {
                MyModel m = new MyModel()
                {
                    MyOrderDetails = new OrderDetails(),
                    MyProduct = new Product()
                };
                m.MyOrderDetails.OrderId = Convert.ToInt32(dr["OrderId"]);
                m.MyProduct.ProductName = Convert.ToString(dr["ProductName"]);
                m.MyProduct.SalePrice = Convert.ToInt32(dr["SalePrice"]);
                m.MyProduct.Discount = Convert.ToInt32(dr["Discount"]);
                m.MyOrderDetails.QTY = Convert.ToInt32(dr["QTY"]);
                m.MyOrderDetails.newPrice = Convert.ToInt32(Convert.ToInt32(dr["SalePrice"]) - Convert.ToInt32(dr["SalePrice"]) * Convert.ToInt32(dr["Discount"]) / 100);
                list.Add(m);
            }
            return list;
        }
    }
}