using DynamicApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicApp.ViewModels
{
    public class CopyCustomerVM
    {
        public CustomerTaskVM OriginalCustomer { get; set; }
        public List<CMDynamicApplication> NewApplicationList { get; set; }
        public List<CMDynamicPackage> NewPackageList { get; set; }
        public List<CMOperatingSystem> NewOperatingSystem { get; set; }
        public Customer Customer { get; set; }

        //public CustomerTaskVM NewCustomer { get; set; }

    }
}