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
    
    public partial class CMDynamicApplication
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CMDynamicApplication()
        {
            this.DynamicAppCustomers = new HashSet<DynamicAppCustomer>();
        }
    
        public int id { get; set; }
        public Nullable<System.DateTime> inserted_at { get; set; }
        public string Index { get; set; }
        public string Name { get; set; }
        public Nullable<long> PackageId { get; set; }
        public string Version { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DynamicAppCustomer> DynamicAppCustomers { get; set; }
    }
}