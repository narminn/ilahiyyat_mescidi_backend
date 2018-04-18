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
    public class PillarsController : Controller
    {
        private ilahiyyetEntities db = new ilahiyyetEntities();

        // GET: Admin/Pillars
        public ActionResult Index()
        {
            return View(db.Pillars.ToList());
        }

        // GET: Admin/Pillars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pillar pillar = db.Pillars.Find(id);
            if (pillar == null)
            {
                return HttpNotFound();
            }
            return View(pillar);
        }

        // GET: Admin/Pillars/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Pillars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,şərtlərin_adi,şərtlərin_şəkili,şərtlərin_məzmunu")] Pillar pillar, HttpPostedFileBase şərtlərin_şəkili)
        {
            if (ModelState.IsValid)
            {
                Random random = new Random();
                int randomNumber = random.Next(0, 100000);
                var date = DateTime.Now;
                var filename = Path.GetFileName(şərtlərin_şəkili.FileName);
                var src = Path.Combine(Server.MapPath("~/Uploads/"), randomNumber + filename);
                şərtlərin_şəkili.SaveAs(src);
                pillar.şərtlərin_şəkili = "/Uploads/" + randomNumber + filename;
                db.Pillars.Add(pillar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pillar);
        }

        // GET: Admin/Pillars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pillar pillar = db.Pillars.Find(id);
            if (pillar == null)
            {
                return HttpNotFound();
            }
            return View(pillar);
        }

        // POST: Admin/Pillars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,şərtlərin_adi,şərtlərin_şəkili,şərtlərin_məzmunu")] Pillar pillar, HttpPostedFileBase şərtlərin_şəkili)
        {
            if (ModelState.IsValid)
            {
                Random random = new Random();
                int randomNumber = random.Next(0, 100000);
                var date = DateTime.Now;
                var filename = Path.GetFileName(şərtlərin_şəkili.FileName);
                var src = Path.Combine(Server.MapPath("~/Uploads/"), randomNumber + filename);
                şərtlərin_şəkili.SaveAs(src);
                pillar.şərtlərin_şəkili = "/Uploads/" + randomNumber + filename;
                db.Entry(pillar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pillar);
        }

        // GET: Admin/Pillars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pillar pillar = db.Pillars.Find(id);
            if (pillar == null)
            {
                return HttpNotFound();
            }
            return View(pillar);
        }

        // POST: Admin/Pillars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pillar pillar = db.Pillars.Find(id);
            db.Pillars.Remove(pillar);
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
