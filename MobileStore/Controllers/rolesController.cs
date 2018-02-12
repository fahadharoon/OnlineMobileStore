using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MobileStore.Models;

namespace MobileStore.Controllers
{
    public class rolesController : Controller
    {
        private MobileStoreContext db = new MobileStoreContext();

        // GET: roles
        public ActionResult Index()
        {
            return View(db.tblroles.ToList());
        }

        // GET: roles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblroles tblroles = db.tblroles.Find(id);
            if (tblroles == null)
            {
                return HttpNotFound();
            }
            return View(tblroles);
        }

        // GET: roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "roleId,rolename")] tblroles tblroles)
        {
            if (ModelState.IsValid)
            {
                db.tblroles.Add(tblroles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblroles);
        }

        // GET: roles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblroles tblroles = db.tblroles.Find(id);
            if (tblroles == null)
            {
                return HttpNotFound();
            }
            return View(tblroles);
        }

        // POST: roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "roleId,rolename")] tblroles tblroles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblroles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblroles);
        }

        // GET: roles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblroles tblroles = db.tblroles.Find(id);
            if (tblroles == null)
            {
                return HttpNotFound();
            }
            return View(tblroles);
        }

        // POST: roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblroles tblroles = db.tblroles.Find(id);
            db.tblroles.Remove(tblroles);
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
