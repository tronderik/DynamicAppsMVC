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
            List<string> customerList = new List<string>();
            customerList = (from customer in db.Customers
                         select customer.Name).ToList();

            customerList.Insert(0,"Select Customer" );
            ViewBag.ListOfCustomers = customerList;
            return View();
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