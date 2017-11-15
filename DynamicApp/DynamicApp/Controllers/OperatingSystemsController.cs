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
    public class OperatingSystemsController : Controller
    {
        private DynamicAppsEntities db = new DynamicAppsEntities();

        // GET: OperatingSystems
        public ActionResult Index()
        {
            return View(db.CMOperatingSystems.ToList());
        }

        // GET: OperatingSystems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CMOperatingSystem cMOperatingSystem = db.CMOperatingSystems.Find(id);
            if (cMOperatingSystem == null)
            {
                return HttpNotFound();
            }
            return View(cMOperatingSystem);
        }

        // GET: OperatingSystems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OperatingSystems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,inserted_at,Description,Language,Name,PackageId,PkgSourcePath,SourceSize,SourceVersion")] CMOperatingSystem cMOperatingSystem)
        {
            if (ModelState.IsValid)
            {
                db.CMOperatingSystems.Add(cMOperatingSystem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cMOperatingSystem);
        }

        // GET: OperatingSystems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CMOperatingSystem cMOperatingSystem = db.CMOperatingSystems.Find(id);
            if (cMOperatingSystem == null)
            {
                return HttpNotFound();
            }
            return View(cMOperatingSystem);
        }

        // POST: OperatingSystems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,inserted_at,Description,Language,Name,PackageId,PkgSourcePath,SourceSize,SourceVersion")] CMOperatingSystem cMOperatingSystem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cMOperatingSystem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cMOperatingSystem);
        }

        // GET: OperatingSystems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CMOperatingSystem cMOperatingSystem = db.CMOperatingSystems.Find(id);
            if (cMOperatingSystem == null)
            {
                return HttpNotFound();
            }
            return View(cMOperatingSystem);
        }

        // POST: OperatingSystems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CMOperatingSystem cMOperatingSystem = db.CMOperatingSystems.Find(id);
            db.CMOperatingSystems.Remove(cMOperatingSystem);
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
