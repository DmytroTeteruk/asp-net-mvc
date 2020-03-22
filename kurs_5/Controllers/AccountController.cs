using kurs_5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace kurs_5.Controllers
{
    public class AccountController : Controller
    {
        private kursa4Context context = new kursa4Context();

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        public ActionResult ActionLogin(string Email, string Password, string ProductId)
        {
            Users User = context.Users.Where(u => u.Email.Equals(Email)).FirstOrDefault();
            if (User != null && (User.Password == Password))
            {
                Session["user"] = User;
                if (!ProductId.Equals(""))
                {
                    return Redirect("/Home/Payment?ProductId=" + ProductId);
                }
                if (User.RoleId == 2)
                {
                    return Redirect("/Account/Admin");
                }

                return Redirect("/Home/Index");
            }
            return Redirect("/Shared/Error");
        }

        // GET: Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Register
        [HttpPost]
        public ActionResult ActionRegister(string FirstName, string LastName, string Email, string Password, string Address)
        {
            Users User = new Users
            {
                Name = FirstName + " " + LastName,
                Email = Email,
                Address = Address,
                Password = Password,
                RoleId = 1
            };

            context.Users.Add(User);
            context.SaveChanges();

            Session["User"] = User;
            return Redirect("/Home/Index");
        }

        // POST: Update
        [HttpPost]
        public ActionResult ActionUpdate(string Name, string Email, string Address)
        {
            Users User = (Users)Session["User"];
            User.Name = Name;
            User.Email = Email;
            User.Address = Address;

            context.Users.Update(User);
            context.SaveChanges();

            Session["User"] = User;
            return Redirect("/Account/Profile");
        }

        // GET: Logout
        public ActionResult Logout()
        {
            Session["User"] = null;

            return Redirect("/Home/Index");
        }

        // GET: Profile
        public ActionResult Profile()
        {
            Users User = (Users)Session["User"];
            ViewBag.User = User;

            var order = context.Order.AsQueryable();
            var product = context.Product.AsQueryable();
            var status = context.Status.AsQueryable();

            var result = (from o in order
                         join p in product on o.ProductId equals p.Id 
                         join s in status on o.StatusId equals s.Id
                         where o.UserId == User.Id
                         select new UserOrder(p.Id, p.Name, p.Price, s.Name)).ToList();
            ViewBag.UserResults = result;

            return View();
        }

        // GET: Admin
        public ActionResult Admin()
        {
            var order = context.Order.AsQueryable();
            var product = context.Product.AsQueryable();
            var status = context.Status.AsQueryable();

            var result = (from p in product
                          join o in order on p.Id equals o.ProductId
                          join s in status on o.StatusId equals s.Id
                          select new UserOrder(p.Id, p.Name, p.Price, s.Name)).ToList();
            ViewBag.UserResults = result;
            ViewBag.Products = context.Product;

            return View();
        }
    } 
}