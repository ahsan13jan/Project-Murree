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
    
    public partial class Block
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Block()
        {
            this.Block1 = new HashSet<Block>();
        }
    
        public int Id { get; set; }
        public Nullable<int> ParentId { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CategoryId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Block> Block1 { get; set; }
        public virtual Block Block2 { get; set; }
        public virtual Category Category { get; set; }
    }
}
