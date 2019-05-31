using Lab23.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Lab23.Controllers
{
    public class HomeController : Controller
    {
        masterEntities db = new masterEntities();
        public ActionResult Index()
        {
            //var user = Session["LoggedInUser"];
            //List<User> p = (List<User>)user;
            //User u = p[0];

            //ViewBag.User = u;

            User u = (User)Session["LoggedInUser"];

            ViewBag.User = u.UserName;
             ViewBag.Password = u.Password;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult MakeNewUser(User u)
        {
            db.Users.Add(u);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string UserName, string Password)
        {
            List<User> Users = db.Users.ToList();

            //var output = Users
            //    .Where(u => 
            //    u.UserName == UserName && 
            //    u.Password == Password);

            foreach(User u in Users)
            {
                if(u.UserName == UserName && u.Password == Password)
                {
                    Session["LoggedInUser"] = u;
                }
            }

           

            return RedirectToAction("Index");

        }

        public ActionResult Shop()
        {
            List<Item> i = db.Items.ToList();
            return View(i);
        }

        public ActionResult Buy(int id)
        {
            if (Session["LoggedInUser"] != null)
            {
                Item purchase = db.Items.Find(id);
                if (Session["LoggedInUser"] != null)
                {
                    User buyer = (User)Session["LoggedInUser"];
                    if (buyer.Money < purchase.Price)
                    {
                        Session["Error"] = $"{buyer.UserName} cannot afford {purchase.ProductName} at {purchase.Price}";
                    }
                    else
                    {
                        buyer.Money -= purchase.Price;
                        purchase.Quantity--;
                        db.Users.AddOrUpdate(buyer);
                        db.Items.AddOrUpdate(purchase);
                        db.SaveChanges();
                    }
                }
                else
                {
                    //Add Session  Error saying you need to log in 
                    return RedirectToAction("Login");
                }
            }
            return RedirectToAction("Error");
            
        }
    }
}