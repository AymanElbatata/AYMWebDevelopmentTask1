//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AYMWebDevelopment.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ContactUsReplyTBL
    {
        public int ContactUsReplyID { get; set; }
        public string ContactUSMessage { get; set; }
        public Nullable<int> ContactIDD { get; set; }
        public Nullable<int> EmployeeIDD { get; set; }
        public Nullable<System.DateTime> DateOfMaking { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual ContactUSTBL ContactUSTBL { get; set; }
        public virtual EmployeeTBL EmployeeTBL { get; set; }
    }
}
