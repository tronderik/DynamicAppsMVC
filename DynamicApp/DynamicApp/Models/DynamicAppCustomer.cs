//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DynamicApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DynamicAppCustomer
    {
        public int id { get; set; }
        public int CustomerID { get; set; }
        public Nullable<int> PackageID { get; set; }
        public Nullable<int> PackageIndex { get; set; }
        public Nullable<int> ApplicationID { get; set; }
        public Nullable<int> ApplicationIndex { get; set; }
        public Nullable<int> OSID { get; set; }
        public Nullable<System.DateTime> inserted_at { get; set; }
        public Nullable<System.DateTime> updated_at { get; set; }
    
        public virtual CMDynamicApplication CMDynamicApplication { get; set; }
        public virtual CMDynamicPackage CMDynamicPackage { get; set; }
        public virtual CMOperatingSystem CMOperatingSystem { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
