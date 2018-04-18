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
    public class BooksController : Controller
    {
        private ilahiyyetEntities db = new ilahiyyetEntities();

        // GET: Admin/Books
        public ActionResult Index()
        {
            return View(db.Books.ToList());
        }

        // GET: Admin/Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Admin/Books/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,kitabin_adi,kitabin_faili")] Book book, HttpPostedFileBase kitabin_faili)
        {
            if (ModelState.IsValid)
            {
                Random random = new Random();
                int randomNumber = random.Next(0, 100000);
                
                var filename = Path.GetFileName(kitabin_faili.FileName);
                var src = Path.Combine(Server.MapPath("~/Files/"), randomNumber + filename);
                kitabin_faili.SaveAs(src);
                book.kitabin_faili = "/Files/" + randomNumber + filename;
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(book);
        }

        // GET: Admin/Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Admin/Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,kitabin_adi,kitabin_faili")] Book book, HttpPostedFileBase kitabin_faili)
        {
            if (ModelState.IsValid)
            {
                Random random = new Random();
                int randomNumber = random.Next(0, 100000);

                var filename = Path.GetFileName(kitabin_faili.FileName);
                var src = Path.Combine(Server.MapPath("~/Files/"), randomNumber + filename);
                kitabin_faili.SaveAs(src);
                book.kitabin_faili = "/Files/" + randomNumber + filename;
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: Admin/Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Admin/Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
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
            IEnumerable<Book> book_list = db.Books.Where(e => e.kitabin_adi.Contains(search)).ToList();
            return View("PartialBook", book_list);
        }
    }
}
