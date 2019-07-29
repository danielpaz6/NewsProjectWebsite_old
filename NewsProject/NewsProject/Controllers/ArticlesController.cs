using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using NewsProject.Models;

namespace NewsProject.Controllers
{
    public class ArticlesController : Controller
    {
        private NewsDbContext db = new NewsDbContext();

        // GET: Articles
        public ActionResult Index()
        {
            var articles = db.Articles.Include(a => a.Category).Include(a => a.User);
            return View(articles.ToList());
        }

        // GET: Articles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: Articles/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Name");
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArticleId,Title,Description,Date,NumOfLikes,ImageLink,ArticleLink,CategoryId,UserId")] Article article)
        {
            if (ModelState.IsValid)
            {
                db.Articles.Add(article);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", article.CategoryId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Name", article.UserId);
            return View(article);
        }

        // GET: Articles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", article.CategoryId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Name", article.UserId);
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArticleId,Title,Description,Date,NumOfLikes,ImageLink,ArticleLink,CategoryId,UserId")] Article article)
        {
            if (ModelState.IsValid)
            {
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", article.CategoryId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Name", article.UserId);
            return View(article);
        }

        // GET: Articles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = db.Articles.Find(id);
            db.Articles.Remove(article);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // Add Article from Mako
        [HttpPost, ActionName("makoNews")]
        public void AddMakoNews(string url, Category cat, User user)
        {
            int counter = 0;
            int k = 0;
            //string url = "http://rcs.mako.co.il/rss/31750a2610f26110VgnVCM1000005201000aRCRD.xml";
            bool flag = false;

            string[,] arrayNews = new string[25, 11];

            XmlTextReader reader = new XmlTextReader(url);
            while (reader.Read())
            {
                if (flag == false)
                {
                    if (reader.Name != "item")
                    {
                        continue;
                    }
                    else
                        flag = true;
                }

                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: // The node is an element.
                        //Console.Write("<" + reader.Name);
                        //Console.WriteLine(">");
                        break;

                    case XmlNodeType.Text: //Display the text in each element.
                        //Console.WriteLine(reader.Value);
                        arrayNews[k, counter] = reader.Value;

                        break;

                    case XmlNodeType.EndElement: //Display the end of the element.
                        //Console.Write("</" + reader.Name);
                        //Console.WriteLine(">");
                        counter++;
                        break;
                }

                if (counter == 12)
                {
                    k++;
                    counter = 0;
                }
            }

            //Console.WriteLine(arrayNews[3, 2]);
            //Console.ReadKey();

            //return arrayNews;


            for (int i = 0; i < 18; i++)
            {
                Article a = new Article();

                a.Title = arrayNews[i, 0];
                a.Description = arrayNews[i, 1];
                a.Date = arrayNews[i, 3];
                a.NumOfLikes = 0;
                a.ImageLink = arrayNews[i, 9];
                a.ArticleLink = arrayNews[i, 2];
                a.Category = cat;
                a.CategoryId = cat.CategoryId;
                a.User = user;
                a.UserId = user.UserId;

                this.Create(a);
            }
        }

        
        public void CNN_News()
        {
            GetNews gn = new GetNews();
            List<string[]> lst = gn.Add_CNN_News();
            Category c = new Category();
            c.Color = "blue";
            c.Name = "blue";
            c.CategoryId = 0;
            User u = new User();
            u.Email = "bla@gmail.com";
            u.Name = "CNN";
            u.Password = "";
            u.Permission = 0;

            foreach(string[] str in lst)
            {
                Article a = new Article();
                a.ArticleLink = str[2];
                a.Category = c;
                a.Date = DateTime.Now.ToString("M/d/yyyy");
                a.Description = str[1];
                a.ImageLink = str[3];
                a.Title = str[0];
                a.User = u;
                a.NumOfLikes = 0;
                this.Create(a);
            }
        }

        public void FOX_News()
        {
            GetNews gn = new GetNews();
            List<string[]> lst = gn.Add_FOX_News();
            Category c = new Category();
            c.Color = "blue";
            c.Name = "blue";
            c.CategoryId = 0;
            User u = new User();
            u.Email = "bla@gmail.com";
            u.Name = "FOX";
            u.Password = "";
            u.Permission = 0;

            foreach (string[] str in lst)
            {
                Article a = new Article();
                a.ArticleLink = str[2];
                a.Category = c;
                a.Date = DateTime.Now.ToString("M/d/yyyy");
                a.Description = str[1];
                a.ImageLink = str[3];
                a.Title = str[0];
                a.User = u;
                a.NumOfLikes = 0;

                this.Create(a);
            }
        }

        public void YNET_News()
        {
            GetNews gn = new GetNews();
            List<string[]> lst = gn.Add_Ynet_News();
            Category c = new Category();
            c.Color = "blue";
            c.Name = "blue";
            c.CategoryId = 0;
            User u = new User();
            u.Email = "bla@gmail.com";
            u.Name = "YNET";
            u.Password = "";
            u.Permission = 0;

            foreach (string[] str in lst)
            {
                Article a = new Article();
                a.ArticleLink = str[2];
                a.Category = c;
                a.Date = DateTime.Now.ToString("M/d/yyyy");
                a.Description = str[1];
                a.ImageLink = str[3];
                a.Title = str[0];
                a.User = u;
                a.NumOfLikes = 0;
                this.Create(a);
            }
        }
    }

}
