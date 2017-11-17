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
    public class PackagesController : Controller
    {
        private DynamicAppsEntities db = new DynamicAppsEntities();

        // GET: Packages
        public ActionResult Index()
        {
            return View(db.CMDynamicPackages.ToList());
        }

        public ActionResult PackageList(int customerID)
        {

            var customerPackages = new CustomerPackages();

            var existingcustomerPackages = db.DynamicAppCustomers.Where(c => c.CustomerID == customerID && c.PackageID != null)
                                                       .Select(c => c.CMDynamicPackage.id)
                                                        .ToList();
            var packagesCustomerDoesntHave = db.CMDynamicPackages.Where(d => !existingcustomerPackages.Contains(d.id)).ToList();
            Customer customer = db.Customers.Where(c => c.id == customerID).FirstOrDefault();
            customerPackages.Customer = customer;
            customerPackages.PackageList = packagesCustomerDoesntHave;
            return View(customerPackages);
        }

        [HttpPost]
        public ActionResult AddPackageToCustomer(int[] ids, int customerID)
        {
            foreach (int id in ids)
            {
                CMDynamicPackage currentPackage = db.CMDynamicPackages.Where(a => a.id == id).FirstOrDefault();
                DynamicAppCustomer appCustomer = new DynamicAppCustomer();
                var lastIndexValue = (db.DynamicAppCustomers.Where(c => c.CustomerID == customerID && c.PackageID != null)
                        .OrderByDescending(c => c.PackageIndex)
                        .FirstOrDefault(c => c.CustomerID == customerID)).PackageIndex;
                int nextIndex = (int)lastIndexValue + 1;
                appCustomer.CustomerID = customerID;
                appCustomer.PackageID = currentPackage.id;
                appCustomer.PackageIndex = nextIndex;

                db.DynamicAppCustomers.Add(appCustomer);
                db.SaveChanges();
            }
            return RedirectToAction("SelectCustomer", "Home", new { id = customerID });
        }

    }
}
