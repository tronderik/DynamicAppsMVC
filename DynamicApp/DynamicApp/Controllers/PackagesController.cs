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
    public class PackagesController : Controller
    {
        private DynamicAppsEntities db = new DynamicAppsEntities();

        // GET: Packages
        public ActionResult Index()
        {
            return View(db.CMDynamicPackages.ToList());
        }

        // GET: Packages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CMDynamicPackage cMDynamicPackage = db.CMDynamicPackages.Find(id);
            if (cMDynamicPackage == null)
            {
                return HttpNotFound();
            }
            return View(cMDynamicPackage);
        }

        // GET: Packages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Packages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,inserted_at,Index,PackageID,PackageName,PackageVersion,ProgramID,ProgramName")] CMDynamicPackage cMDynamicPackage)
        {
            if (ModelState.IsValid)
            {
                db.CMDynamicPackages.Add(cMDynamicPackage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cMDynamicPackage);
        }

        // GET: Packages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CMDynamicPackage cMDynamicPackage = db.CMDynamicPackages.Find(id);
            if (cMDynamicPackage == null)
            {
                return HttpNotFound();
            }
            return View(cMDynamicPackage);
        }

        // POST: Packages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,inserted_at,Index,PackageID,PackageName,PackageVersion,ProgramID,ProgramName")] CMDynamicPackage cMDynamicPackage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cMDynamicPackage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cMDynamicPackage);
        }

        // GET: Packages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CMDynamicPackage cMDynamicPackage = db.CMDynamicPackages.Find(id);
            if (cMDynamicPackage == null)
            {
                return HttpNotFound();
            }
            return View(cMDynamicPackage);
        }

        // POST: Packages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CMDynamicPackage cMDynamicPackage = db.CMDynamicPackages.Find(id);
            db.CMDynamicPackages.Remove(cMDynamicPackage);
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
