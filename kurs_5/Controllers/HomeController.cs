using kurs_5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace kurs_5.Controllers
{
    public class HomeController : Controller
    {
        private kursa4Context context = new kursa4Context();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult MobilePhones()
        {
            return View(context.Product);
        }

        public ActionResult Sale(string Price)
        {
            return View();
        }

        public ActionResult Payment(int ProductId)
        {
            if (Session["User"] == null)
            {
                return Redirect("/Account/Login?ProductId=" + ProductId);
            }
            Users User = (Users) Session["User"];
            Product ProductObj = context.Product.Where(p => p.Id.Equals(ProductId)).FirstOrDefault(); 
            ViewBag.User = User;
            ViewBag.ProductId = ProductId;
            ViewBag.Price = ProductObj.Price;

            Order order = new Order()
            {
                ProductId = ProductId,
                UserId = User.Id,
                StatusId = 1
            };
            context.Order.Add(order);
            context.SaveChanges();

            return View();
        }

        [HttpPost]
        public ActionResult Order(int ProductId)
        {
            if (Session["User"] == null)
            {
                return Redirect("/Account/Login?ProductId=" + ProductId);
            }
            Users User = (Users)Session["User"];

            Order order = context.Order.Where(o => o.ProductId.Equals(ProductId)).First();
            order.StatusId = 2;
            context.SaveChanges();

            return Redirect("/Account/Profile");
        }

        public ActionResult CompleteProduct(int ProductId)
        {
            Order order = context.Order.First(o => o.ProductId == ProductId);
            order.StatusId = 3;
            context.SaveChanges();

            return Redirect("/Account/Admin");
        }

        public ActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ActionAddProduct(string name, long price, string description, long amount)
        {
            Product product = new Product()
            {
                Name = name,
                Price = price,
                Description = description,
                Amount = amount
            };
            context.Product.Add(product);
            context.SaveChanges();

            return Redirect("/Account/Admin");
        }

        public ActionResult ChangeProduct(long ProductId)
        {
            ViewBag.Product = context.Product.First(p => p.Id == ProductId);
            return View();
        }

        [HttpPost]
        public ActionResult ActionChangeProduct(long id, string name, long price, string description, long amount)
        {
            Product product = context.Product.First(p => p.Id == id);
            product.Name = name;
            product.Price = price;
            product.Description = description;
            product.Amount = amount;
            context.Product.Update(product);
            context.SaveChanges();

            return Redirect("/Account/Admin");
        }
    }
}