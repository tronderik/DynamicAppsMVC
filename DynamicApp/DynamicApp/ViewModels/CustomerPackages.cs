using DynamicApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicApp.ViewModels
{
    public class CustomerPackages
    {
        public List<CMDynamicPackage> PackageList { get; set; }
        public Customer Customer { get; set; }
    }
}