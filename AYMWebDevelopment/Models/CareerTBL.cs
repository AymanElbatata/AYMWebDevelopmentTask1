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
    
    public partial class CareerTBL
    {
        public int CandidateID { get; set; }
        public string AutoCode { get; set; }
        public string CandidateName { get; set; }
        public string CandidateEmail { get; set; }
        public string CandidatePosition { get; set; }
        public string CandidateFileURL { get; set; }
        public string CandidateIPAddress { get; set; }
        public Nullable<System.DateTime> DateOfMaking { get; set; }
        public bool IsDeleted { get; set; }
    }
}
