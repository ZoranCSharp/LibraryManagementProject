using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibraryManagement2.Models;

namespace LibraryManagement2.Controllers
{
    public class IssuedsController : Controller
    {
        private LibraryManagement2DbContext db = new LibraryManagement2DbContext();



        // GET: Issueds
        public ActionResult Index()
        {
            var issueds = db.Issueds.Include(i => i.Book).Include(i => i.Employee).Include(i => i.Member);
            return View(issueds.ToList());
        }
        public ActionResult NotInStock()
        {
            var issueds = db.Issueds.Include(i => i.Book).Include(i => i.Employee).Include(i => i.Member);
            return View(issueds.ToList());
        }

        // GET: Issueds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Issued issued = db.Issueds.Find(id);
            if (issued == null)
            {
                return HttpNotFound();
            }
            return View(issued);
        }

        // GET: Issueds/Create
        public ActionResult Create()
        {
            ViewBag.BookID = new SelectList(db.Books, "BookID", "Title");
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FullName");
            ViewBag.MemberID = new SelectList(db.Members, "MemberID", "FullName");

            return View();
        }

        // POST: Issueds/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IssueID,IssuedDate,ReturnDate,BookID,EmployeeID,MemberID")] Issued issued)
        {
            var books = db.Books.ToList();
            var iss1 = issued.BookID;
            var book = new Book();
            var book1 = db.Books.First(b => b.BookID == iss1);

            System.Diagnostics.Debug.WriteLine("Knjiga " + book.Title + "----" + book1.Quantity);



            if (ModelState.IsValid)
            {



                if ((issued.ReturnDate == null) && (book1.Quantity > 0))
                {

                    book1.Quantity--;

                    db.SaveChanges();


                    System.Diagnostics.Debug.WriteLine("Izdata " + iss1 + "stanje " + book1.Quantity);
                }
                else if (book1.Quantity == 0)
                {
                    return RedirectToAction("NotInStock");

                }
                if (issued.ReturnDate != null)
                {
                    book1.Quantity++;
                    db.SaveChanges();
                    System.Diagnostics.Debug.WriteLine("Vracena " + iss1 + "stanje " + book1.Quantity);

                }


                db.Issueds.Add(issued);

                db.SaveChanges();


                return RedirectToAction("Index");

            }

            ViewBag.BookID = new SelectList(db.Books, "BookID", "Title", issued.BookID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FullName", issued.EmployeeID);
            ViewBag.MemberID = new SelectList(db.Members, "MemberID", "FullName", issued.MemberID);
            return View(issued);
        }

        // GET: Issueds/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Issued issued = db.Issueds.Find(id);
            if (issued == null)
            {
                return HttpNotFound();
            }

            ViewBag.BookID = new SelectList(db.Books, "BookID", "Title", issued.BookID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FullName", issued.EmployeeID);
            ViewBag.MemberID = new SelectList(db.Members, "MemberID", "FullName", issued.MemberID);
            return View(issued);
        }

        // POST: Issueds/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IssueID,IssuedDate,ReturnDate,BookID,EmployeeID,MemberID")] Issued issued)
        {


            var iss1 = issued.BookID;

            var book1 = db.Books.First(b => b.BookID == iss1);

            System.Diagnostics.Debug.WriteLine("Knjiga " + book1.Title + "----" + book1.Quantity);
            if (ModelState.IsValid)
            {
                if (issued.ReturnDate != null)
                {
                    book1.Quantity++;
                    db.SaveChanges();
                    System.Diagnostics.Debug.WriteLine("Vracena " + book1.Title + "stanje " + book1.Quantity);

                }
                db.Entry(issued).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BookID = new SelectList(db.Books, "BookID", "Title", issued.BookID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FullName", issued.EmployeeID);
            ViewBag.MemberID = new SelectList(db.Members, "MemberID", "FullName", issued.MemberID);
            return View(issued);
        }

        // GET: Issueds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Issued issued = db.Issueds.Find(id);
            if (issued == null)
            {
                return HttpNotFound();
            }
            return View(issued);
        }

        // POST: Issueds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Issued issued = db.Issueds.Find(id);
            db.Issueds.Remove(issued);
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
