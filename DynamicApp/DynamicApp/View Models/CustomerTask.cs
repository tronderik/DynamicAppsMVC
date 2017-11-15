using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicApp.View_Models
{
    public class CustomerTask
    {
        public List<Models.CMDynamicApplication> applicationList { get; set; }
        public List<Models.CMDynamicApplication> packageList { get; set; }
        public Models.CMOperatingSystem operatingSystem { get; set; }
        public Models.Customer customer { get; set; }

    }
}