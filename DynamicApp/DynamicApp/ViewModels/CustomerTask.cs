using DynamicApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicApp.ViewModels
{
    public class CustomerTask
    {
        public List<CMDynamicApplication> applicationList { get; set; }
        public List<CMDynamicPackage> packageList { get; set; }
        public List<CMOperatingSystem> operatingSystem { get; set; }
        public Customer customer { get; set; }
    }
}