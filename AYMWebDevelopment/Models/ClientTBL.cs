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
    
    public partial class ClientTBL
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClientTBL()
        {
            this.ExpenseRevenueTBL = new HashSet<ExpenseRevenueTBL>();
            this.ProjectTBL = new HashSet<ProjectTBL>();
        }
    
        public int ClientID { get; set; }
        public string AutoCode { get; set; }
        public string ClientFullName { get; set; }
        public string ClientEmail1 { get; set; }
        public string ClientEmail2 { get; set; }
        public string ClientFaceBookURL { get; set; }
        public string ClientWhatsAppNumber { get; set; }
        public string ClientPhone1 { get; set; }
        public string ClientPhone2 { get; set; }
        public string ClientAddress { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string PassportOrIDNumber { get; set; }
        public string OfficialDocumentURL { get; set; }
        public Nullable<int> MaritalStatusIDD { get; set; }
        public Nullable<int> GenderIDD { get; set; }
        public Nullable<int> ProfessionIDD { get; set; }
        public Nullable<int> EducationIDD { get; set; }
        public Nullable<int> NationalityIDD { get; set; }
        public Nullable<int> CountryIDD { get; set; }
        public Nullable<int> GovernorateIDD { get; set; }
        public Nullable<int> CityIDD { get; set; }
        public string ClientNotes { get; set; }
        public Nullable<System.DateTime> DateOfMaking { get; set; }
        public Nullable<System.DateTime> DateOfLastUpdate { get; set; }
        public Nullable<int> EmployeeIDDLastUpdate { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual CityTBL CityTBL { get; set; }
        public virtual CountryTBL CountryTBL { get; set; }
        public virtual GenderTBL GenderTBL { get; set; }
        public virtual GovernorateTBL GovernorateTBL { get; set; }
        public virtual MaritalStatusTBL MaritalStatusTBL { get; set; }
        public virtual NationalityTBL NationalityTBL { get; set; }
        public virtual ProfessionTBL ProfessionTBL { get; set; }
        public virtual EducationTBL EducationTBL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExpenseRevenueTBL> ExpenseRevenueTBL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProjectTBL> ProjectTBL { get; set; }
    }
}
