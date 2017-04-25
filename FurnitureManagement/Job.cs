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
    
    public partial class Job
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Job()
        {
            this.JobItems = new HashSet<JobItem>();
        }
    
        public int Id { get; set; }
        public string JobNo { get; set; }
        public string CANo { get; set; }
        public string ContractorName { get; set; }
        public Nullable<decimal> AmountApproval { get; set; }
        public Nullable<System.DateTime> FinancialYear { get; set; }
        public string JobDescription { get; set; }
        public Nullable<decimal> AmountContract { get; set; }
        public Nullable<System.DateTime> CompletionDate { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public Nullable<int> Category { get; set; }
    
        public virtual Category Category1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JobItem> JobItems { get; set; }
    }
}
