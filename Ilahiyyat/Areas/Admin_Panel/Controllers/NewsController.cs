using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ilahiyyat.Models;

namespace Ilahiyyat.Areas.Admin_Panel.Controllers
{
    [FilterAdmin]
    public class NewsController : Controller
    {
        private ilahiyyetEntities db = new ilahiyyetEntities();
        private object news;

        // GET: Admin/News
        public ActionResult Index()
        {
            return View(db.News.ToList());
        }

        // GET: Admin/News/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // GET: Admin/News/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,xəberin_adi,xəberin_məzmunu,xəberin_əlavə_edilən_gün,xəberin_baxiş_sayi,xəberin_şəkili")] News news, HttpPostedFileBase xəberin_şəkili)
        {
            if (ModelState.IsValid)
            {
                Random random = new Random();
                int randomNumber = random.Next(0, 100000);
                var date = DateTime.Now;
                var filename = Path.GetFileName(xəberin_şəkili.FileName);
                var src = Path.Combine(Server.MapPath("~/Uploads/"), randomNumber + filename);
                xəberin_şəkili.SaveAs(src);
                news.xəberin_şəkili = "/Uploads/" + randomNumber + filename;
                news.xəberin_əlavə_edilən_gün = date;
                news.xəberin_baxiş_sayi = 0;
                db.News.Add(news);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(news);
        }

        // GET: Admin/News/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: Admin/News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,xəberin_adi,xəberin_məzmunu,xəberin_əlavə_edilən_gün,xəberin_baxiş_sayi,xəberin_şəkili")] News news, HttpPostedFileBase xəberin_şəkili)
        {
            if (ModelState.IsValid)
            {
                Random random = new Random();
                int randomNumber = random.Next(0, 100000);
                var date = DateTime.Now;
                var filename = Path.GetFileName(xəberin_şəkili.FileName);
                var src = Path.Combine(Server.MapPath("~/Uploads/"), randomNumber + filename);
                xəberin_şəkili.SaveAs(src);
                news.xəberin_şəkili = "/Uploads/" + randomNumber + filename;
                news.xəberin_əlavə_edilən_gün = date;
                news.xəberin_baxiş_sayi = 0;
                db.Entry(news).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(news);
        }

        // GET: Admin/News/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: Admin/News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            News news = db.News.Find(id);
            db.News.Remove(news);
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
        public ActionResult Search(string search)
        {
            IEnumerable<News> news_list = db.News.Where(e => e.xəberin_adi.Contains(search) || e.xəberin_məzmunu.Contains(search)).ToList();
            return View("Partial", news_list);
        }
    }
}
