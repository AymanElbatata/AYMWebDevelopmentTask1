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
    
    public partial class CallingAPISAuthTBL
    {
        public int CallingAPISAuthID { get; set; }
        public string CallingAPISAuthPinCode { get; set; }
        public string CallingAPISAuthPassword { get; set; }
        public Nullable<int> EmployeeIDD { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual EmployeeTBL EmployeeTBL { get; set; }
    }
}
