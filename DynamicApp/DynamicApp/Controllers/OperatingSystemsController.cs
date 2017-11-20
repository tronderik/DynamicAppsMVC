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
    public class OperatingSystemsController : Controller
    {
        private DynamicAppsEntities db = new DynamicAppsEntities();

        // GET: OperatingSystems
        public ActionResult Index()
        {
            return View(db.CMOperatingSystems.ToList());
        }

        public ActionResult OSList(int customerID)
        {

            var customerOS = new CustomerOSVM();

            var existingcustomerOS = db.DynamicAppCustomers.Where(c => c.CustomerID == customerID && c.OSID != null)
                                                       .Select(c => c.CMOperatingSystem.id)
                                                        .ToList();
            var osCustomerDoesntHave = db.CMOperatingSystems.Where(d => !existingcustomerOS.Contains(d.id)).ToList();
            Customer customer = db.Customers.Where(c => c.id == customerID).FirstOrDefault();
            customerOS.Customer = customer;
            customerOS.OSList = osCustomerDoesntHave;
            return View(customerOS);
        }

        [HttpPost]
        public ActionResult AddOSToCustomer(int[] ids, int customerID)
        {
            foreach (int id in ids)
            {
                CMOperatingSystem currentOS = db.CMOperatingSystems.Where(a => a.id == id).FirstOrDefault();
                DynamicAppCustomer appCustomer = new DynamicAppCustomer();

                appCustomer.CustomerID = customerID;
                appCustomer.OSID = id;

                db.DynamicAppCustomers.Add(appCustomer);
                db.SaveChanges();
            }
            return RedirectToAction("SelectCustomer", "Home", new { id = customerID });
        }
    }
}
