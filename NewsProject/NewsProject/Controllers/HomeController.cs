using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
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
        public ActionResult Register(string name, string email, string password)
        {
            ViewBag.isRegistered = false;
            ViewBag.ErrorMessage = new List<String>();
            Md5Code r = new Md5Code();
            User newUser = new User();
            newUser.Name = name;
            newUser.Email = email;
            using (MD5 md5Hash = MD5.Create()) {
                newUser.Password = r.GetMd5Hash(md5Hash, password);
            }

            if(name.Length > 20 || name.Length < 2)
                ViewBag.ErrorMessage.Add("The name must be between 2-20 characters.");

            if(!Regex.Match(name, "^[a-zA-Z0-9 ]*$").Success)
                ViewBag.ErrorMessage.Add("The name must contain only: a-zA-Z0-9 or spaces.");

            if(!Regex.Match(email, "^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\\.[a-zA-Z0-9-.]+$").Success)
                ViewBag.ErrorMessage.Add("Invalid email, please try again");

            if (password.Length > 50 || password.Length < 5)
                ViewBag.ErrorMessage.Add("Password must be between 5-50 characters.");

            if (ViewBag.ErrorMessage.Count == 0)
            {
                foreach(var user in db.Users) // How to make it faster?
                {
                    if(user.Name.Equals(name))
                    {
                        ViewBag.ErrorMessage.Add("The username is already exists!");
                        break;
                    }

                    if (user.Email.Equals(email))
                    {
                        ViewBag.ErrorMessage.Add("The email is already exists!");
                        break;
                    }
                }
            }

            // We need to check again because error's might occur in the foreach(var user in db.Users) section
            if (ViewBag.ErrorMessage.Count == 0)
            {
                ViewBag.isRegistered = true;
                // --- insert the user to the 
                db.Users.Add(newUser);
                db.SaveChanges();
            }

            return View();
        }
    }
}