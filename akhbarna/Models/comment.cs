//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace akhbarna.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class comment
    {
        public int id { get; set; }
        public string details { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public string owner { get; set; }
        public string mail { get; set; }
        public Nullable<int> news_id { get; set; }
    
        public virtual news news { get; set; }
    }
}