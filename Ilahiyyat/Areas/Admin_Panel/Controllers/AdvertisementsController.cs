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
    public class AdvertisementsController : Controller
    {
        private ilahiyyetEntities db = new ilahiyyetEntities();

        // GET: Admin/Advertisements
        public ActionResult Index()
        {
            return View(db.Advertisements.ToList());
        }

        // GET: Admin/Advertisements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advertisement advertisement = db.Advertisements.Find(id);
            if (advertisement == null)
            {
                return HttpNotFound();
            }
            return View(advertisement);
        }

        // GET: Admin/Advertisements/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Advertisements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,elanin_adi,elanin_şəkili,elanin_məzmunu,elanin_başlanqic_günü,elanin_bitdiyi_gün,elanin_yeri,elanin_təşkilaçinin_adi,elanin_təşkilaçinin_nomrəsi,elanin_təşkilaçinin_email")] Advertisement advertisement, HttpPostedFileBase elanin_şəkili)
        {
            if (ModelState.IsValid)
            {
                Random random = new Random();
                int randomNumber = random.Next(0, 100000);
                var date = DateTime.Now;
                var filename = Path.GetFileName(elanin_şəkili.FileName);
                var src = Path.Combine(Server.MapPath("~/Uploads/"), randomNumber + filename);
                elanin_şəkili.SaveAs(src);
                advertisement.elanin_şəkili = "/Uploads/" + randomNumber + filename;
                db.Advertisements.Add(advertisement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(advertisement);
        }

        // GET: Admin/Advertisements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advertisement advertisement = db.Advertisements.Find(id);
            if (advertisement == null)
            {
                return HttpNotFound();
            }
            return View(advertisement);
        }

        // POST: Admin/Advertisements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,elanin_adi,elanin_şəkili,elanin_məzmunu,elanin_başlanqic_günü,elanin_bitdiyi_gün,elanin_yeri,elanin_təşkilaçinin_adi,elanin_təşkilaçinin_nomrəsi,elanin_təşkilaçinin_email")] Advertisement advertisement, HttpPostedFileBase elanin_şəkili)
        {
            if (ModelState.IsValid)

            {
                Random random = new Random();
                int randomNumber = random.Next(0, 100000);
                var date = DateTime.Now;
                var filename = Path.GetFileName(elanin_şəkili.FileName);
                var src = Path.Combine(Server.MapPath("~/Uploads/"), randomNumber + filename);
                elanin_şəkili.SaveAs(src);
                advertisement.elanin_şəkili = "/Uploads/" + randomNumber + filename;
                db.Entry(advertisement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(advertisement);
        }

        // GET: Admin/Advertisements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advertisement advertisement = db.Advertisements.Find(id);
            if (advertisement == null)
            {
                return HttpNotFound();
            }
            return View(advertisement);
        }

        // POST: Admin/Advertisements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Advertisement advertisement = db.Advertisements.Find(id);
            db.Advertisements.Remove(advertisement);
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
            IEnumerable<Advertisement> adv_list = db.Advertisements.Where(e => e.elanin_adi.Contains(search) || e.elanin_məzmunu.Contains(search)).ToList();
            return View("PartialAdvert", adv_list);
        }
    }
}
