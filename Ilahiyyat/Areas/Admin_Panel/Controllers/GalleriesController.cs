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
    public class GalleriesController : Controller
    {
        private ilahiyyetEntities db = new ilahiyyetEntities();

        // GET: Admin/Galleries
        public ActionResult Index()
        {
            return View(db.Galleries.ToList());
        }

        // GET: Admin/Galleries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery gallery = db.Galleries.Find(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }

        // GET: Admin/Galleries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Galleries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,şəkilin_adi,şəkilin_kodu")] Gallery gallery, HttpPostedFileBase şəkilin_kodu)
        {
            if (ModelState.IsValid)
            {
                Random random = new Random();
                int randomNumber = random.Next(0, 100000);
                var date = DateTime.Now;
                var filename = Path.GetFileName(şəkilin_kodu.FileName);
                var src = Path.Combine(Server.MapPath("~/Uploads/"), randomNumber + filename);
                şəkilin_kodu.SaveAs(src);
                gallery.şəkilin_kodu = "/Uploads/" + randomNumber + filename;
                db.Galleries.Add(gallery);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gallery);
        }

        // GET: Admin/Galleries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery gallery = db.Galleries.Find(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }

        // POST: Admin/Galleries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,şəkilin_adi,şəkilin_kodu")] Gallery gallery, HttpPostedFileBase şəkilin_kodu)
        {
            if (ModelState.IsValid)
            {
                Random random = new Random();
                int randomNumber = random.Next(0, 100000);
                var date = DateTime.Now;
                var filename = Path.GetFileName(şəkilin_kodu.FileName);
                var src = Path.Combine(Server.MapPath("~/Uploads/"), randomNumber + filename);
                şəkilin_kodu.SaveAs(src);
                gallery.şəkilin_kodu = "/Uploads/" + randomNumber + filename;
                db.Entry(gallery).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gallery);
        }

        // GET: Admin/Galleries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery gallery = db.Galleries.Find(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }

        // POST: Admin/Galleries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gallery gallery = db.Galleries.Find(id);
            db.Galleries.Remove(gallery);
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
    }
}
