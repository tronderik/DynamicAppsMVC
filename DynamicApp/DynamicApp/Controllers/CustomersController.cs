using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DynamicApp.Models;
using DynamicApp.ViewModels;

namespace DynamicApp.Controllers
{
    public class CustomersController : Controller
    {
        private DynamicAppsEntities db = new DynamicAppsEntities();


        // GET: Customers
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }


        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        [HttpGet]
        public ActionResult AddApplicationsToCustomers(int[] appIDs)
        {
            ViewBag.AppIds = appIDs;
            return View(db.Customers.ToList());
        }


        [HttpPost]
        public ActionResult AddApplicationsToCustomers(int[] appIDs, int[] customerIDs)
        {
            foreach (int customerID in customerIDs)
            {
                var customerApps = db.DynamicAppCustomers.Where(c => c.CustomerID == customerID && c.ApplicationID != null)
                                           .Select(c => c.CMDynamicApplication.id)
                                           .ToList();
                foreach (int appID in appIDs)
                {
                    if (!customerApps.Contains(appID))
                    {
                        CMDynamicApplication currentApplication = db.CMDynamicApplications.Where(a => a.id == appID).FirstOrDefault();
                        DynamicAppCustomer newEntry = new DynamicAppCustomer();

                        var lastIndexValue = (db.DynamicAppCustomers.Where(c => c.CustomerID == customerID && c.ApplicationID != null)
                       .OrderByDescending(c => c.ApplicationIndex)
                       .FirstOrDefault(c => c.CustomerID == customerID)).ApplicationIndex;
                        int nextIndex = (int)lastIndexValue + 1;
                        newEntry.CustomerID = customerID;
                        newEntry.ApplicationID = currentApplication.id;
                        newEntry.ApplicationIndex = nextIndex;

                        db.DynamicAppCustomers.Add(newEntry);
                        db.SaveChanges();

                    }
                }
                var appsCustomerDoesntHave = db.CMDynamicApplications.Where(d => !customerApps.Contains(d.id)).OrderBy(a => a.Name);
            }
            return RedirectToAction("Index", "Home");
        }


        public ActionResult CopyCustomer(int? id)
        {
            if( id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CopyCustomerVM copyCustomerVM = new CopyCustomerVM();
            copyCustomerVM.OriginalCustomer = GetCustomerInfo(id);
            Customer newCustomer = CreateNewCustomerAndCopyCustomerDetailsFromOriginal(copyCustomerVM.OriginalCustomer.Customer);
            copyCustomerVM.Customer = newCustomer;
            copyCustomerVM.NewApplicationList = copyCustomerVM.OriginalCustomer.ApplicationList;
            copyCustomerVM.NewPackageList = copyCustomerVM.OriginalCustomer.PackageList;
            copyCustomerVM.NewOperatingSystem = copyCustomerVM.OriginalCustomer.OperatingSystem;
            return View(copyCustomerVM);
        }
       
        [HttpPost]
        public ActionResult CopyCustomer(int? id, CopyCustomerVM model)
        {
            DateTime now = DateTime.Now;
            CustomerTaskVM oldCustomer = GetCustomerInfo(id);

            model.Customer.inserted_at = now;
            db.Customers.Add(model.Customer);
            db.SaveChanges();
            int customerID = db.Customers.Where(c => c.Name == model.Customer.Name
                                                        && c.Location == model.Customer.Location
                                                        && c.OfficeVersion == model.Customer.OfficeVersion
                                                        && c.RDSVersion == model.Customer.RDSVersion
                                                        && c.System == model.Customer.System
                                                        && c.Domain == model.Customer.Domain).Select(c => c.id).FirstOrDefault();

            List<DynamicAppCustomer> entries = CreateEntryToDynamicAppCustomers(oldCustomer, customerID);
            foreach(var item in entries)
            {
                db.DynamicAppCustomers.Add(item);
            }


            db.SaveChanges();
            return RedirectToAction("SelectCustomer", "Home", new { id = model.Customer.id });
        }


        private List<DynamicAppCustomer> CreateEntryToDynamicAppCustomers(CustomerTaskVM task, int id)
        {
            List<DynamicAppCustomer> list = new List<DynamicAppCustomer>();
            int appIndex = 1;
            foreach(CMDynamicApplication app in task.ApplicationList)
            {

                DynamicAppCustomer newCustomerAppEntry = new DynamicAppCustomer();
                newCustomerAppEntry.ApplicationID = app.id;
                newCustomerAppEntry.ApplicationIndex = appIndex;
                newCustomerAppEntry.CustomerID = id;
                list.Add(newCustomerAppEntry);
                appIndex++;
            }

            int pckIndex = 1;
            foreach(CMDynamicPackage pck in task.PackageList)
            {
                DynamicAppCustomer newCustomerAppEntry = new DynamicAppCustomer();
                newCustomerAppEntry.PackageID = pck.id;
                newCustomerAppEntry.PackageIndex = pckIndex;
                newCustomerAppEntry.CustomerID = id;
                list.Add(newCustomerAppEntry);
                pckIndex++;
            }

            DynamicAppCustomer newCustomerOSEntry = new DynamicAppCustomer();
            newCustomerOSEntry.CustomerID = id;
            var os = task.OperatingSystem.FirstOrDefault();
            newCustomerOSEntry.OSID = os.id;
            list.Add(newCustomerOSEntry);
            return list;
        }

        private Customer CreateNewCustomerAndCopyCustomerDetailsFromOriginal(Customer originalCustomer)
        {
            Customer _customer = new Customer
            {
                Domain = originalCustomer.Domain,
                DomainShort = originalCustomer.DomainShort,
                Location = originalCustomer.Location,
                Name = originalCustomer.Name,
                OfficeVersion = originalCustomer.OfficeVersion,
                OU = originalCustomer.OU,
                RDSVersion = originalCustomer.RDSVersion,
                SCOM = originalCustomer.SCOM,
                XAController = originalCustomer.XAController,
                XenAppFarmName = originalCustomer.XenAppFarmName,
                System = originalCustomer.System
            };
            return _customer;
        }

        private CustomerTaskVM GetCustomerInfo(int? id)
        {
            CustomerTaskVM taskVm = new CustomerTaskVM();
            var customer = db.Customers.Find(id);
            var applicationList = db.DynamicAppCustomers
                .Where(c => customer.id == c.CustomerID && c.ApplicationID != null)
                .OrderBy(c => c.ApplicationIndex)
                .Select(c => c.CMDynamicApplication)
                .ToList();

            taskVm.ApplicationList = applicationList;

            var packageList = db.DynamicAppCustomers
                .Where(c => customer.id == c.CustomerID && c.PackageID != null)
                .OrderBy(c => c.PackageIndex)
                .Select(c => c.CMDynamicPackage)
                .ToList();

            taskVm.PackageList = packageList;

            var osList = db.DynamicAppCustomers
                .Where(c => customer.id == c.CustomerID && c.OSID != null)
                .Select(c => c.CMOperatingSystem)
                .ToList();

            taskVm.OperatingSystem = osList;
            taskVm.Customer = customer;

            return taskVm;
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,inserted_at,CollectionID,Domain,DomainShort,Name,Location,OU,RDSVersion,SCOM,System,XAController,OfficeVersion,XenAppFarmName")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                DateTime now = DateTime.Now;
                customer.inserted_at = now;
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,inserted_at,CollectionID,Domain,DomainShort,Name,Location,OU,RDSVersion,SCOM,System,XAController,OfficeVersion,XenAppFarmName")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            List<DynamicAppCustomer> list = db.DynamicAppCustomers.Where(c => c.CustomerID == id).ToList();
            foreach(var item in list)
            {
                db.DynamicAppCustomers.Remove(item);
            }
            db.SaveChanges();
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
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
