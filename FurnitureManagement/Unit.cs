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
    
    public partial class Unit
    {
        public int id { get; set; }
        public string unit_Number { get; set; }
        public Nullable<int> Location { get; set; }
    
        public virtual Location Location1 { get; set; }
    }
}
