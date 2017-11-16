using DynamicApp.Models;
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
        }
        
        public ActionResult SelectCustomer(int id)
        {
            var customer = db.Customers.Find(id);
            ViewBag.CustomerName = customer?.Name;

            return View();
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