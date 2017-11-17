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
    public class ApplicationsController : Controller
    {
        private DynamicAppsEntities db = new DynamicAppsEntities();

        // GET: Applications
        public ActionResult Index()
        {
            return View(db.CMDynamicApplications.ToList());
        }

        public ActionResult ApplicationList(int customerID)
        {

            var customerApplications = new CustomerApplications();
            
            var customerApps = db.DynamicAppCustomers.Where(c => c.CustomerID == customerID && c.ApplicationID != null)
                                                       .Select(c=> c.CMDynamicApplication.id)
                                                        .ToList();
            var appsCustomerDoesntHave = db.CMDynamicApplications.Where(d => !customerApps.Contains(d.id)).ToList();
            Customer customer = db.Customers.Where(c => c.id == customerID).FirstOrDefault();
            customerApplications.Customer = customer;
            customerApplications.ApplicationList = appsCustomerDoesntHave;
            return View(customerApplications);
        }

        [HttpPost]
        public ActionResult AddApplicationToCustomer(int[] ids, int customerID)
        {
            foreach(int id in ids)
            {
                CMDynamicApplication currentApplication = db.CMDynamicApplications.Where(a => a.id == id).FirstOrDefault();
                DynamicAppCustomer appCustomer = new DynamicAppCustomer();
                var lastIndexValue = (db.DynamicAppCustomers.Where(c => c.CustomerID == customerID && c.ApplicationID != null)
                        .OrderByDescending(c => c.ApplicationIndex)
                        .FirstOrDefault(c => c.CustomerID == customerID)).ApplicationIndex;
                int nextIndex = (int) lastIndexValue + 1;
                appCustomer.CustomerID = customerID;
                appCustomer.ApplicationID = currentApplication.id;
                appCustomer.ApplicationIndex = nextIndex;

                db.DynamicAppCustomers.Add(appCustomer);
                db.SaveChanges();
            }
            return RedirectToAction("SelectCustomer", "Home", new { id = customerID });
        }

    }
       
}
