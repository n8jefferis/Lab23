using Lab23.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab23.Controllers
{
    public class UserItemsController : Controller
    {
        private UserItemDbContext db = new UserItemDbContext();

        public ActionResult Index()
        {
            return View(db.UserItems.ToList());
        }

        public ActionResult List()
        {
            return View();
        }

        public ActionResult Details()
        {
            return View();
        }

        public ActionResult Delete()
        {
            return View();
        }
    }
}