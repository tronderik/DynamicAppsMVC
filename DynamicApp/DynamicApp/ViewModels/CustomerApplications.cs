using DynamicApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicApp.ViewModels
{
    public class CustomerApplications
    {
        public List<CMDynamicApplication> ApplicationList { get; set; }
        public Customer Customer { get; set; }
    }
}