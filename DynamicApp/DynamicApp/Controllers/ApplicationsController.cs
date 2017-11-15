using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DynamicApp.Models;

namespace DynamicApp.Controllers
{
    public class ApplicationsController : Controller
    {
        private DynamicAppsEntities db = new DynamicAppsEntities();

        // GET: Applications
        public ActionResult Index()
        {
            return View(db.CMDynamicApplications.ToList());
        }

        // GET: Applications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CMDynamicApplication cMDynamicApplication = db.CMDynamicApplications.Find(id);
            if (cMDynamicApplication == null)
            {
                return HttpNotFound();
            }
            return View(cMDynamicApplication);
        }

        // GET: Applications/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Applications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,inserted_at,Index,Name,PackageId,Version")] CMDynamicApplication cMDynamicApplication)
        {
            if (ModelState.IsValid)
            {
                db.CMDynamicApplications.Add(cMDynamicApplication);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cMDynamicApplication);
        }

        // GET: Applications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CMDynamicApplication cMDynamicApplication = db.CMDynamicApplications.Find(id);
            if (cMDynamicApplication == null)
            {
                return HttpNotFound();
            }
            return View(cMDynamicApplication);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,inserted_at,Index,Name,PackageId,Version")] CMDynamicApplication cMDynamicApplication)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cMDynamicApplication).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cMDynamicApplication);
        }

        // GET: Applications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CMDynamicApplication cMDynamicApplication = db.CMDynamicApplications.Find(id);
            if (cMDynamicApplication == null)
            {
                return HttpNotFound();
            }
            return View(cMDynamicApplication);
        }

        // POST: Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CMDynamicApplication cMDynamicApplication = db.CMDynamicApplications.Find(id);
            db.CMDynamicApplications.Remove(cMDynamicApplication);
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
