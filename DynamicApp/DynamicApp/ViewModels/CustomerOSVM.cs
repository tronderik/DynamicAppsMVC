using DynamicApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicApp.ViewModels
{
    public class CustomerOSVM
    {
        public List<CMOperatingSystem> OSList { get; set; }
        public Customer Customer { get; set; }
    }
}