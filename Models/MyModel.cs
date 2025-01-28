using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Online_Shopping.Models
{
    public class MyModel
    {
        public Customer MyCustomer { get; set; }
        public Category MyCat { get; set; }
        public SubCategory MySubCat { get; set; }
        public Brand MyBrand { get; set; }
        public Color MyColor { get; set; }
        public Size MySize { get; set; }
        public Product MyProduct { get; set; }
        public Wishlist MyWishlist { get; set; }
        public Cart MyCart { get; set; }
        public Checkout MyCheckout { get; set; }
        public OrderDetails MyOrderDetails { get; set; }
    }
}