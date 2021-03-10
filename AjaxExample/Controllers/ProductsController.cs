using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AjaxExample.Models;
using System.Data.SqlClient;

namespace AjaxExample.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowCategoryProducts()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem
            {
                Text = "Select Category",
                Value = "0",
                Selected = true
            });
            items.Add(new SelectListItem { Text = "Beverages", Value = "1" });
            items.Add(new SelectListItem { Text = "Seafood", Value = "2" });
            items.Add(new SelectListItem { Text = "Dairy", Value = "3" });
            items.Add(new SelectListItem { Text = "Spices", Value = "4" });
            ViewBag.CategoryType = items;
            return View();
        }

        public ActionResult Show()
        {
            if(System.Web.HttpContext.Current.Cache["time"]==null)
            {
                System.Web.HttpContext.Current.Cache["time"] = DateTime.Now;
            }
            ViewBag.Time = ((DateTime)System.Web.HttpContext.Current.Cache["time"]).ToString();
            return View();
        }

        public JsonResult GetProducts(string id)
        {
            List<Product> products = new List<Product>();
            string query = string.Format("select * from ajaxproduct where cid= {0}", id);
            using (SqlConnection con = new SqlConnection("Data Source=(local);Initial Catalog=Organization;Integrated Security=true"))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while(reader.Read())
                    {
                        products.Add(new Product
                        {
                            ProductID = Convert.ToInt16(reader.GetValue(0)),
                            ProductName = reader.GetString(1).ToString(),
                            QuantityPerUnit = Convert.ToInt16(reader.GetValue(2)),
                            UnitPrice = Convert.ToInt16(reader.GetValue(3)),
                            CategoryId = Convert.ToInt16(reader.GetValue(4))
                        });
                        
                    }
                    return Json(products, JsonRequestBehavior.AllowGet);
                }
            }
            
        }
    }
}