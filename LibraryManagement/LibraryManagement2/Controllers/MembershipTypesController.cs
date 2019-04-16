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
    public class MembershipTypesController : Controller
    {
        private LibraryManagement2DbContext db = new LibraryManagement2DbContext();

        // GET: MembershipTypes
        public ActionResult Index()
        {
            return View(db.MembershipTypes.ToList());
        }

        // GET: MembershipTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MembershipType membershipType = db.MembershipTypes.Find(id);
            if (membershipType == null)
            {
                return HttpNotFound();
            }
            return View(membershipType);
        }

        // GET: MembershipTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MembershipTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MembershipTypeID,Name,Price")] MembershipType membershipType)
        {
            if (ModelState.IsValid)
            {
                db.MembershipTypes.Add(membershipType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(membershipType);
        }

        // GET: MembershipTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MembershipType membershipType = db.MembershipTypes.Find(id);
            if (membershipType == null)
            {
                return HttpNotFound();
            }
            return View(membershipType);
        }

        // POST: MembershipTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MembershipTypeID,Name,Price")] MembershipType membershipType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(membershipType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(membershipType);
        }

        // GET: MembershipTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MembershipType membershipType = db.MembershipTypes.Find(id);
            if (membershipType == null)
            {
                return HttpNotFound();
            }
            return View(membershipType);
        }

        // POST: MembershipTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MembershipType membershipType = db.MembershipTypes.Find(id);
            db.MembershipTypes.Remove(membershipType);
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
