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
    
    public partial class ProjectTypeTBL
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProjectTypeTBL()
        {
            this.ProjectTBL = new HashSet<ProjectTBL>();
        }
    
        public int ProjectTypeID { get; set; }
        public string ProjectTypeName { get; set; }
        public string ProjectTypeNotes { get; set; }
        public bool IsDeleted { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProjectTBL> ProjectTBL { get; set; }
    }
}
