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
    public class SermonsController : Controller
    {
        private ilahiyyetEntities db = new ilahiyyetEntities();

        // GET: Admin/Sermons
        public ActionResult Index()
        {
            return View(db.Sermons.ToList());
        }

        // GET: Admin/Sermons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sermon sermon = db.Sermons.Find(id);
            if (sermon == null)
            {
                return HttpNotFound();
            }
            return View(sermon);
        }

        // GET: Admin/Sermons/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Sermons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,xutbə_adi,xutbə_məzmunu,xutbə_audio")] Sermon sermon,HttpPostedFileBase xutbə_audio)
        {
            if (ModelState.IsValid)
            {
                Random random = new Random();
                int randomNumber = random.Next(0, 100000);

                var filename = Path.GetFileName(xutbə_audio.FileName);
                var src = Path.Combine(Server.MapPath("~/Audios/"), randomNumber + filename);
                xutbə_audio.SaveAs(src);
                sermon.xutbə_audio = "/Audios/" + randomNumber + filename;
                db.Sermons.Add(sermon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sermon);
        }

        // GET: Admin/Sermons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sermon sermon = db.Sermons.Find(id);
            if (sermon == null)
            {
                return HttpNotFound();
            }
            return View(sermon);
        }

        // POST: Admin/Sermons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,xutbə_adi,xutbə_məzmunu,xutbə_audio")] Sermon sermon, HttpPostedFileBase xutbə_audio)
        {
            if (ModelState.IsValid)
            {
                Random random = new Random();
                int randomNumber = random.Next(0, 100000);

                var filename = Path.GetFileName(xutbə_audio.FileName);
                var src = Path.Combine(Server.MapPath("~/Audios/"), randomNumber + filename);
                xutbə_audio.SaveAs(src);
                sermon.xutbə_audio = "/Audios/" + randomNumber + filename;
                db.Entry(sermon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sermon);
        }

        // GET: Admin/Sermons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sermon sermon = db.Sermons.Find(id);
            if (sermon == null)
            {
                return HttpNotFound();
            }
            return View(sermon);
        }

        // POST: Admin/Sermons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sermon sermon = db.Sermons.Find(id);
            db.Sermons.Remove(sermon);
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
            IEnumerable<Sermon> srmn_list = db.Sermons.Where(e => e.xutbə_adi.Contains(search)).ToList();
            return View("PartialSermon", srmn_list);
        }
    }
}
