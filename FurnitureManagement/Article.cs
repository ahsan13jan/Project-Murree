//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FurnitureManagement
{
    using System;
    using System.Collections.Generic;
    
    public partial class Article
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Article()
        {
            this.JobItems = new HashSet<JobItem>();
        }
    
        public int Article_Id { get; set; }
        public string Article_DESC { get; set; }
        public string Acc_Unit { get; set; }
        public Nullable<int> Category { get; set; }
        public Nullable<decimal> Rate { get; set; }
        public string Prefix { get; set; }
    
        public virtual Category Category1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JobItem> JobItems { get; set; }
    }
}
