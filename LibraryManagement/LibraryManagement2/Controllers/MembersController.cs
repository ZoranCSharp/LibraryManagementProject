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
    public class MembersController : Controller
    {
        private LibraryManagement2DbContext db = new LibraryManagement2DbContext();

        public ActionResult ReadOnly()
        {
            var mem = db.Members.Include(m => m.MembershipType);

            return View( mem.ToList());
        }

        // GET: Members
        public ActionResult Index()
        {
            
                var members = db.Members.Include(m => m.MembershipType);
                return View(members.ToList());
            
        }

        // GET: Members/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // GET: Members/Create
        //[Authorize(Roles = "CanManageBooks")]
        public ActionResult Create()
        {
            ViewBag.MembershipTypeID = new SelectList(db.MembershipTypes, "MembershipTypeID", "Name");
            return View();
        }

        // POST: Members/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "CanManageBooks")]
        public ActionResult Create([Bind(Include = "MemberID,FullName,Email,Phone,MembershipTypeID")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Members.Add(member);
                db.SaveChanges();
                if (User.IsInRole("CanManageBooks"))
                {
                return RedirectToAction("Index");
                }
                else
                {
                return RedirectToAction("ReadOnly");
                }
            }

            ViewBag.MembershipTypeID = new SelectList(db.MembershipTypes, "MembershipTypeID", "Name", member.MembershipTypeID);
            return View(member);
        }

        // GET: Members/Edit/5
        [Authorize(Roles = "CanManageBooks")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            ViewBag.MembershipTypeID = new SelectList(db.MembershipTypes, "MembershipTypeID", "Name", member.MembershipTypeID);
            return View(member);
        }

        // POST: Members/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "CanManageBooks")]
        public ActionResult Edit([Bind(Include = "MemberID,FullName,Email,Phone,MembershipTypeID")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MembershipTypeID = new SelectList(db.MembershipTypes, "MembershipTypeID", "Name", member.MembershipTypeID);
            return View(member);
        }

        // GET: Members/Delete/5
        [Authorize(Roles = "CanManageBooks")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Member member = db.Members.Find(id);
            db.Members.Remove(member);
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
