using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Online_Shopping.Models
{
    public class Checkout
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string Contact { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public int ShippingCharges { get; set; }
        public DateTime Date { get; set; }
        public int OrderStatus { get; set; }
        public int maxorder { get; set; }
        public int TotalProduct { get; set; }
    }

    public class Order_Method
    {

        public static void SaveOrder(Checkout o)
        {
            string q = "select * from tbl_Cart where CustomerId='" + o.CustomerId + "'";
            DataTable data = DB.Table(q);

            if(data.Rows.Count==0)
            {
                //NO Addition 
            }
            else
            {
                q = "insert into tbl_Order values('" + o.CustomerId + "','" + o.Contact + "','" + o.PostalCode + "','" + o.Address + "','" + o.ShippingCharges + "','" + o.Date + "','" + o.OrderStatus + "')";
                DB.Query(q);

                q = "select ISNULL(max(OrderId),0) from tbl_Order";
                DataTable dt = DB.Table(q);
                int maxorder = Convert.ToInt32(dt.Rows[0][0]) + 1;


                foreach (DataRow dr in data.Rows)
                {
                    q = "insert into tbl_OrderDetail values('" + maxorder + "','" + Convert.ToInt32(dr["ProductId"]) + "','" + Convert.ToInt32(dr["QTY"]) + "','" + Convert.ToInt32(dr["UnitPrice"]) + "','" + Convert.ToInt32(dr["Discount"]) + "')";
                    DB.Query(q);
                }
                q = "delete tbl_Cart where CustomerId='" + o.CustomerId + "'";
                DB.Query(q);
            }
            
        }



        public static List<MyModel> orderlist()
        {
            string q = "select o.OrderId,c.CusName,c.CusEmail,o.Date,count(d.QTY) as QTY,OrderStatus from tbl_Order o inner join tbl_Customer c on o.CustomerId=c.CustomerID inner join tbl_OrderDetail d on o.OrderId=d.OrderId group by o.OrderId,c.CusName, c.CusEmail,o.Date,OrderStatus";
            DataTable dt = DB.Table(q);


            List<MyModel> list = new List<MyModel>();
            foreach (DataRow dr in dt.Rows)
            {
                MyModel c = new MyModel()
                {
                    MyCustomer = new Customer(),
                    MyCheckout = new Checkout(),
                    MyOrderDetails = new OrderDetails()
                };
                c.MyCheckout.OrderId = Convert.ToInt32(dr["OrderId"]);
                c.MyCustomer.CusName = Convert.ToString(dr["CusName"]);
                c.MyCustomer.CusEmail = Convert.ToString(dr["CusEmail"]);
                c.MyCheckout.Date = Convert.ToDateTime(dr["Date"]);
                c.MyOrderDetails.QTY = Convert.ToInt32(dr["QTY"]);
                c.MyCheckout.OrderStatus = Convert.ToInt32(dr["OrderStatus"]);
                list.Add(c);
            }
            return list;
        }

        public static void PendingOrder(int id)
        {
            string q = "update tbl_Order set OrderStatus=0 where OrderId='" + id + "'";
            DB.Query(q);
        }

        public static void AprovedOrder(int id)
        {
            string q = "update tbl_Order set OrderStatus=1 where OrderId='" + id + "'";
            DB.Query(q);
        }


        public static void RejectOrder(int id)
        {
            string q = "update tbl_Order set OrderStatus=2 where OrderId='" + id + "'";
            DB.Query(q);
        }


    }

}