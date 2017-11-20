using DynamicApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicApp.ViewModels
{
    public class CustomerTaskVM
    {
        public List<CMDynamicApplication> ApplicationList { get; set; }
        public List<CMDynamicPackage> PackageList { get; set; }
        public List<CMOperatingSystem> OperatingSystem { get; set; }
        public Customer Customer { get; set; }
    }
}