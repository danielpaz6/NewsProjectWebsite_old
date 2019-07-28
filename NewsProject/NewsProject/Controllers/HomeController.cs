using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NewsProject.Models;

namespace NewsProject.Controllers
{
    public class HomeController : Controller
    {
        private NewsDbContext db = new NewsDbContext();
        public ActionResult Index()
        {
            var articles = db.Articles.Include(a => a.Category).Include(a => a.User);
            return View(articles.ToList());
            //return View();
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
        [HttpPost]
        public ActionResult register([Bind(Include = "UserId,Name,Password,Email,Permission")] User user)
        {
            //hii
            if (ModelState.IsValid)
            {
                if (!db.Users.Contains(user))
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                }
            }

            return View();
        }
    }
}