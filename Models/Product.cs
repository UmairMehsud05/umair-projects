using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Online_Shopping.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public int CatID { get; set; }
        public int SubCatID { get; set; }
        public int BrandID { get; set; }
        public int SizeID { get; set; }
        public int ColorID { get; set; }

        [Required(ErrorMessage = "Product Name is Required")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Sale Price is Required")]
        public int SalePrice { get; set; }

        [Required(ErrorMessage = "Purchase Price is Required")]
        public int PurchasePrice { get; set; }

        [Required(ErrorMessage = "Available Quantity is Required")]
        public int avaQty { get; set; }

        [Required(ErrorMessage = "Total Quantity is Required")]
        public int TotalQty { get; set; }

        [Required(ErrorMessage = "Product Detail is Required")]
        public string ProductDetail { get; set; }
        public int Discount { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime RegDate { get; set; }
        public bool IsSale { get; set; }
        public bool ISAvailable { get; set; }
        public string Image { get; set; }
        public HttpPostedFileBase MyImage { get; set; }

        public int newprice { set; get; }
    }

    public class Product_Method
    {
        static int cat;
        static int Brand;

        public static void AddProduct(Product pr)
        {
            string q;
            if (pr.ProductID > 0)
            {
                q = "update tbl_Product set CatID='" + pr.CatID + "',SubCatID='" + pr.SubCatID + "',BrandID='" + pr.BrandID + "',SizeID='" + pr.SizeID + "',ColorID='" + pr.ColorID + "',ProductName='" + pr.ProductName + "',SalePrice='" + pr.SalePrice + "',PurchasePrice='" + pr.PurchasePrice + "',avaQty='" + pr.avaQty + "',TotalQty='" + pr.TotalQty + "',ProductDetail='" + pr.ProductDetail + "',Discount='" + pr.Discount + "',RegDate='" + pr.RegDate + "',IsSale='" + pr.IsSale + "',ISAvailable='" + pr.ISAvailable + "',Image='" + pr.Image + "' where ProductID='" + pr.ProductID + "'";
            }
            else
            {
                q = "insert into tbl_Product values('" + pr.CatID + "','" + pr.SubCatID + "','" + pr.BrandID + "','" + pr.SizeID + "','" + pr.ColorID + "','" + pr.ProductName + "','" + pr.SalePrice + "','" + pr.PurchasePrice + "','" + pr.avaQty + "','" + pr.TotalQty + "','" + pr.ProductDetail + "','" + pr.Discount + "','" + pr.RegDate + "','" + pr.IsSale + "','" + pr.ISAvailable + "','" + pr.Image + "')";
            }
            DB.Query(q);
            //pr.ProductID = 0;
        }

        public static void DeleteProduct(int id)
        {
            string q = "delete tbl_Product where ProductID='" + id + "'";
            DB.Query(q);
        }

        public static Product GetProductByID(int id)
        {
            string q = "select * from tbl_product where ProductID='" + id + "'";
            DataTable dt = DB.Table(q);
            Product pt = new Product();
            if (dt.Rows.Count > 0)
            {
                pt.ProductID = Convert.ToInt32(dt.Rows[0]["ProductID"]);
                pt.CatID = Convert.ToInt32(dt.Rows[0]["CatID"]);
                pt.SubCatID = Convert.ToInt32(dt.Rows[0]["SubCatID"]);
                pt.BrandID = Convert.ToInt32(dt.Rows[0]["BrandID"]);
                pt.SizeID = Convert.ToInt32(dt.Rows[0]["SizeID"]);
                pt.ColorID = Convert.ToInt32(dt.Rows[0]["ColorID"]);
                pt.ProductName = Convert.ToString(dt.Rows[0]["ProductName"]);
                pt.SalePrice = Convert.ToInt32(dt.Rows[0]["SalePrice"]);
                pt.PurchasePrice = Convert.ToInt32(dt.Rows[0]["PurchasePrice"]);
                pt.avaQty = Convert.ToInt32(dt.Rows[0]["avaQty"]);
                pt.TotalQty = Convert.ToInt32(dt.Rows[0]["TotalQty"]);
                pt.ProductDetail = Convert.ToString(dt.Rows[0]["ProductDetail"]);
                pt.Discount = Convert.ToInt32(dt.Rows[0]["Discount"]);
                pt.RegDate = Convert.ToDateTime(dt.Rows[0]["RegDate"]);
                pt.IsSale = Convert.ToBoolean(dt.Rows[0]["IsSale"]);
                pt.ISAvailable = Convert.ToBoolean(dt.Rows[0]["ISAvailable"]);
                pt.Image = Convert.ToString(dt.Rows[0]["Image"]);
            }
            return pt;
        }

        public static MyModel GetByIdProduct(int id)
        {
            string q = "select * from vw_ProductDetails where ProductID='" + id + "'";
            DataTable dt = DB.Table(q);
            cat = Convert.ToInt32(dt.Rows[0]["CatID"]);
            Brand = Convert.ToInt32(dt.Rows[0]["BrandID"]);
            MyModel pt = new MyModel()
            {
                MyBrand = new Brand(),
                MyCat = new Category(),
                MyColor = new Color(),
                MyProduct = new Product(),
                MySize = new Size(),
                MySubCat = new SubCategory()
            };
            pt.MyProduct.ProductID = Convert.ToInt32(dt.Rows[0]["ProductID"]);
            pt.MyCat.CatName = Convert.ToString(dt.Rows[0]["CatName"]);
            pt.MySubCat.SubCatName = Convert.ToString(dt.Rows[0]["SubCatName"]);
            pt.MyBrand.BrandName = Convert.ToString(dt.Rows[0]["BrandName"]);
            pt.MySize.SizeName = Convert.ToString(dt.Rows[0]["SizeName"]);
            pt.MyColor.ColorName = Convert.ToString(dt.Rows[0]["ColorName"]);
            pt.MyProduct.ProductName = Convert.ToString(dt.Rows[0]["ProductName"]);
            pt.MyProduct.SalePrice = Convert.ToInt32(dt.Rows[0]["SalePrice"]);
            pt.MyProduct.newprice = Convert.ToInt32(pt.MyProduct.SalePrice - ((pt.MyProduct.SalePrice * pt.MyProduct.Discount) / 100));
            pt.MyProduct.PurchasePrice = Convert.ToInt32(dt.Rows[0]["PurchasePrice"]);
            pt.MyProduct.avaQty = Convert.ToInt32(dt.Rows[0]["avaQty"]);
            pt.MyProduct.TotalQty = Convert.ToInt32(dt.Rows[0]["TotalQty"]);
            pt.MyProduct.ProductDetail = Convert.ToString(dt.Rows[0]["ProductDetail"]);
            pt.MyProduct.Discount = Convert.ToInt32(dt.Rows[0]["Discount"]);
            pt.MyProduct.RegDate = Convert.ToDateTime(dt.Rows[0]["RegDate"]);
            pt.MyProduct.IsSale = Convert.ToBoolean(dt.Rows[0]["IsSale"]);
            pt.MyProduct.ISAvailable = Convert.ToBoolean(dt.Rows[0]["ISAvailable"]);
            pt.MyProduct.Image = Convert.ToString(dt.Rows[0]["Image"]);
            return pt;

        }


        public static List<MyModel> suggestsAllProduct()
        {
            string q = "select * from vw_ProductDetails where CatID='" + cat + "' or BrandID='" + Brand + "'";
            DataTable dt = DB.Table(q);
            //int BrandId = Convert.ToInt32(dt.Rows[0]["BrandID"]);
            //string q = "select * from vw_ProductDetails where CatID='" + CatId + "' and BrandID='" + BrandId + "'";
            //dt=DB.Table(q);
            List<MyModel> list = new List<MyModel>();
            foreach (DataRow dr in dt.Rows)
            {
                MyModel pt = new MyModel()
                {
                    MyBrand = new Brand(),
                    MyCat = new Category(),
                    MyColor = new Color(),
                    MyProduct = new Product(),
                    MySize = new Size(),
                    MySubCat = new SubCategory()
                };
                pt.MyProduct.ProductID = Convert.ToInt32(dr["ProductID"]);
                pt.MyCat.CatName = Convert.ToString(dr["CatName"]);
                pt.MySubCat.SubCatName = Convert.ToString(dr["SubCatName"]);
                pt.MyBrand.BrandName = Convert.ToString(dr["BrandName"]);
                pt.MySize.SizeName = Convert.ToString(dr["SizeName"]);
                pt.MyColor.ColorName = Convert.ToString(dr["ColorName"]);
                pt.MyProduct.ProductName = Convert.ToString(dr["ProductName"]);
                pt.MyProduct.SalePrice = Convert.ToInt32(dr["SalePrice"]);
                pt.MyProduct.newprice = Convert.ToInt32(pt.MyProduct.SalePrice - (pt.MyProduct.SalePrice * pt.MyProduct.Discount / 100));
                pt.MyProduct.PurchasePrice = Convert.ToInt32(dr["PurchasePrice"]);
                pt.MyProduct.avaQty = Convert.ToInt32(dr["avaQty"]);
                pt.MyProduct.TotalQty = Convert.ToInt32(dr["TotalQty"]);
                pt.MyProduct.ProductDetail = Convert.ToString(dr["ProductDetail"]);
                pt.MyProduct.Discount = Convert.ToInt32(dr["Discount"]);
                pt.MyProduct.RegDate = Convert.ToDateTime(dr["RegDate"]);
                pt.MyProduct.IsSale = Convert.ToBoolean(dr["IsSale"]);
                pt.MyProduct.ISAvailable = Convert.ToBoolean(dr["ISAvailable"]);
                pt.MyProduct.Image = Convert.ToString(dr["Image"]);
                list.Add(pt);
            }
            return list;
        }


        public static List<MyModel> GetAllProduct(int? CatId = null, int? BrandID = null, int? ColorID = null, int? SizeID = null)
        {
            SqlParameter[] p = new SqlParameter[5];
            p[0] = new SqlParameter("@ActionType", Action.Select);
            p[1] = new SqlParameter("CatID", CatId);
            p[2] = new SqlParameter("BrandID", BrandID);
            p[3] = new SqlParameter("ColorID", ColorID);
            p[4] = new SqlParameter("SizeID", SizeID);
            DataTable dt = DB.sp_Table("sp_Products", p);
            List<MyModel> list = new List<MyModel>();
            foreach (DataRow dr in dt.Rows)
            {
                MyModel pt = new MyModel()
                {
                    MyBrand = new Brand(),
                    MyCat = new Category(),
                    MyColor = new Color(),
                    MyProduct = new Product(),
                    MySize = new Size(),
                    MySubCat = new SubCategory()
                };
                pt.MyProduct.ProductID = Convert.ToInt32(dr["ProductID"]);
                pt.MyCat.CatName = Convert.ToString(dr["CatName"]);
                pt.MySubCat.SubCatName = Convert.ToString(dr["SubCatName"]);
                pt.MyBrand.BrandName = Convert.ToString(dr["BrandName"]);
                pt.MySize.SizeName = Convert.ToString(dr["SizeName"]);
                pt.MyColor.ColorName = Convert.ToString(dr["ColorName"]);
                pt.MyProduct.ProductName = Convert.ToString(dr["ProductName"]);
                pt.MyProduct.SalePrice = Convert.ToInt32(dr["SalePrice"]);
                pt.MyProduct.newprice = Convert.ToInt32(Convert.ToInt32(dr["SalePrice"]) - ((Convert.ToInt32(dr["SalePrice"]) * Convert.ToInt32(dr["Discount"]) / 100)));
                pt.MyProduct.PurchasePrice = Convert.ToInt32(dr["PurchasePrice"]);
                pt.MyProduct.avaQty = Convert.ToInt32(dr["avaQty"]);
                pt.MyProduct.TotalQty = Convert.ToInt32(dr["TotalQty"]);
                pt.MyProduct.ProductDetail = Convert.ToString(dr["ProductDetail"]);
                pt.MyProduct.Discount = Convert.ToInt32(dr["Discount"]);
                pt.MyProduct.RegDate = Convert.ToDateTime(dr["RegDate"]);
                pt.MyProduct.IsSale = Convert.ToBoolean(dr["IsSale"]);
                pt.MyProduct.ISAvailable = Convert.ToBoolean(dr["ISAvailable"]);
                pt.MyProduct.Image = Convert.ToString(dr["Image"]);
                list.Add(pt);
            }
            return list;
        }

    }
}