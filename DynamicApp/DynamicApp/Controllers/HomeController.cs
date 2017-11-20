using DynamicApp.Models;
using DynamicApp.View_Models;
using DynamicApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DynamicApp.Controllers
{
    public class HomeController : Controller
    {
        private DynamicAppsEntities db = new DynamicAppsEntities();

        /*
        public ActionResult Index()
        {
            var customers = db.Customers
                .OrderBy(c => c.Name)
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.id.ToString()
                })
                .ToList();

            ViewBag.ListOfCustomers = customers;

            return View();
        }*/

        public ActionResult Index()
        {
            var customers = db.Customers.OrderBy(c => c.Name).ToList();
            return View(customers);
        }
        
        public ActionResult SelectCustomer(int id)
        {
            CustomerTaskVM customerTask = new CustomerTaskVM();
            var customer = db.Customers.Find(id);
            var applicationList = db.DynamicAppCustomers
                .Where(c => customer.id == c.CustomerID && c.ApplicationID != null)
                .OrderBy(c => c.ApplicationIndex)
                .Select(c => c.CMDynamicApplication)
                .ToList();

            customerTask.ApplicationList = applicationList;

            var packageList = db.DynamicAppCustomers
                .Where(c => customer.id == c.CustomerID && c.PackageID != null)
                .OrderBy(c => c.PackageIndex)
                .Select(c => c.CMDynamicPackage)
                .ToList();
            
            customerTask.PackageList = packageList;

            var osList = db.DynamicAppCustomers
                .Where(c => customer.id == c.CustomerID && c.OSID != null)
                .Select(c => c.CMOperatingSystem)
                .ToList();
            
            customerTask.OperatingSystem = osList;
            customerTask.Customer = customer;
            ViewBag.CustomerName = customer?.Name;

            return View(customerTask);
        }

        [HttpPost]
        public ActionResult ApplicationMoveUp (int customerId, int appid)
        {
            var dynamicApp = db.DynamicAppCustomers.SingleOrDefault(d => d.CustomerID == customerId && d.ApplicationID == appid);

            if (dynamicApp != null)
            {
                var appBeforeIt = db.DynamicAppCustomers
                    .OrderByDescending(d => d.ApplicationIndex)
                    .FirstOrDefault(d => d.CustomerID == customerId && d.ApplicationIndex < dynamicApp.ApplicationIndex);

                if (appBeforeIt != null)
                {
                    var appBeforeIndex = appBeforeIt.ApplicationIndex;
                    appBeforeIt.ApplicationIndex = dynamicApp.ApplicationIndex;
                    dynamicApp.ApplicationIndex = appBeforeIndex;

                    db.SaveChanges();
                }
            }

            return RedirectToAction("SelectCustomer", new { id = customerId });
        }

        [HttpPost]
        public ActionResult ApplicationMoveDown(int customerId, int appid)
        {
            var dynamicApp = db.DynamicAppCustomers.SingleOrDefault(d => d.CustomerID == customerId && d.ApplicationID == appid);

            if (dynamicApp != null)
            {
                var appAfterIt = db.DynamicAppCustomers.OrderBy(d => d.ApplicationIndex)
                    .FirstOrDefault(d => d.CustomerID == customerId && d.ApplicationIndex > dynamicApp.ApplicationIndex);

                if (appAfterIt != null)
                {
                    var appAfterIndex = appAfterIt.ApplicationIndex;
                    appAfterIt.ApplicationIndex = dynamicApp.ApplicationIndex;
                    dynamicApp.ApplicationIndex = appAfterIndex;

                    db.SaveChanges();
                }
            }

            return RedirectToAction("SelectCustomer", new { id = customerId });
        }

        [HttpPost]
        public ActionResult PackageMoveUp(int customerId, int pckid)
        {
            var dynamicPackage = db.DynamicAppCustomers.SingleOrDefault(d => d.CustomerID == customerId && d.PackageID == pckid);

            if (dynamicPackage != null)
            {
                var appBeforeIt = db.DynamicAppCustomers
                    .OrderByDescending(d => d.PackageIndex)
                    .FirstOrDefault(d => d.CustomerID == customerId && d.PackageIndex < dynamicPackage.PackageIndex);

                if (appBeforeIt != null)
                {
                    var appBeforeIndex = appBeforeIt.PackageIndex;
                    appBeforeIt.PackageIndex = dynamicPackage.PackageIndex;
                    dynamicPackage.PackageIndex = appBeforeIndex;

                    db.SaveChanges();
                }
            }

            return RedirectToAction("SelectCustomer", new { id = customerId });
        }

        [HttpPost]
        public ActionResult PackageMoveDown(int customerId, int pckid)
        {
            var dynamicPackage = db.DynamicAppCustomers.SingleOrDefault(d => d.CustomerID == customerId && d.PackageID == pckid);

            if (dynamicPackage != null)
            {
                var appAfterIt = db.DynamicAppCustomers.OrderBy(d => d.PackageIndex)
                    .FirstOrDefault(d => d.CustomerID == customerId && d.PackageIndex > dynamicPackage.PackageIndex);

                if (appAfterIt != null)
                {
                    var appAfterIndex = appAfterIt.PackageIndex;
                    appAfterIt.PackageIndex = dynamicPackage.PackageIndex;
                    dynamicPackage.PackageIndex = appAfterIndex;

                    db.SaveChanges();
                }
            }

            return RedirectToAction("SelectCustomer", new { id = customerId });
        }

        [HttpPost]
        public ActionResult PackageRemove(int customerID, int pckid)
        {
            var dynamicPackage = db.DynamicAppCustomers.SingleOrDefault(d => d.CustomerID == customerID && d.PackageID == pckid);
            db.DynamicAppCustomers.Remove(dynamicPackage);
            db.SaveChanges();

            return RedirectToAction("SelectCustomer", new { id = customerID });
        }

        [HttpPost]
        public ActionResult ApplicationRemove(int customerID, int appid)
        {
            var dynamicPackage = db.DynamicAppCustomers.SingleOrDefault(d => d.CustomerID == customerID && d.ApplicationID == appid);
            db.DynamicAppCustomers.Remove(dynamicPackage);
            db.SaveChanges();

            return RedirectToAction("SelectCustomer", new { id = customerID });
        }

        [HttpPost]
        public ActionResult OSRemove(int customerID, int osid)
        {
            var dynamicPackage = db.DynamicAppCustomers.SingleOrDefault(d => d.CustomerID == customerID && d.OSID == osid);
            db.DynamicAppCustomers.Remove(dynamicPackage);
            db.SaveChanges();

            return RedirectToAction("SelectCustomer", new { id = customerID });
        }

        [HttpPost]
        public ActionResult AddApplication(int customerID)
        {
            return RedirectToAction("AddApplication", new { id = customerID });
        }

        public ViewResult CustomerChoosen(string customer)
        {
            ViewBag.messageString = customer;
            return View("information");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}