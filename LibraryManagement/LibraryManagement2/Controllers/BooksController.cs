using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LibraryManagement2.Models;


namespace LibraryManagement2.Controllers
{
    
    public class BooksController : Controller
    {
        private LibraryManagement2DbContext db = new LibraryManagement2DbContext();

        // GET: Books
        
        public ActionResult ReadOnly()
        {
            var read = db.Books.Include(b => b.Genre).Include(b => b.Library).Include(b => b.Publisher);

            return View(read.ToList());
        }

        // [Authorize(Roles = "CanManageBooks")]
        public ActionResult Index()
        {
            var books = db.Books.Include(b => b.Genre).Include(b => b.Library).Include(b => b.Publisher);

            return View(books.ToList());

        }

        // GET: Books/Details/5
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

        // GET: Books/Create
        //[Authorize(Roles = "CanManageBooks")]
        public ActionResult Create()
        {
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "Name");
            ViewBag.LibraryID = new SelectList(db.Libraries, "LibraryID", "Name");
            ViewBag.PublisherID = new SelectList(db.Publishers, "PublisherID", "Name");
            return View();
        }

        // POST: Books/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookID,Title,Author,Quantity,PublisherID,GenreID,LibraryID")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                if (User.IsInRole("CanManageBooks")) { 
                return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("ReadOnly");
                }
            }

            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "Name", book.GenreID);
            ViewBag.LibraryID = new SelectList(db.Libraries, "LibraryID", "Name", book.LibraryID);
            ViewBag.PublisherID = new SelectList(db.Publishers, "PublisherID", "Name", book.PublisherID);
            return View(book);
        }

        // GET: Books/Edit/5
        [Authorize(Roles = "CanManageBooks")]
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
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "Name", book.GenreID);
            ViewBag.LibraryID = new SelectList(db.Libraries, "LibraryID", "Name", book.LibraryID);
            ViewBag.PublisherID = new SelectList(db.Publishers, "PublisherID", "Name", book.PublisherID);
            return View(book);
        }

        // POST: Books/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "CanManageBooks")]
        public ActionResult Edit([Bind(Include = "BookID,Title,Author,Quantity,PublisherID,GenreID,LibraryID")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "Name", book.GenreID);
            ViewBag.LibraryID = new SelectList(db.Libraries, "LibraryID", "Name", book.LibraryID);
            ViewBag.PublisherID = new SelectList(db.Publishers, "PublisherID", "Name", book.PublisherID);
            return View(book);
        }

        // GET: Books/Delete/5
        [Authorize(Roles = "CanManageBooks")]
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

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "CanManageBooks")]
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
    }
}
