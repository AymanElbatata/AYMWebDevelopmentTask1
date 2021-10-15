using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AYMWebDevelopment.Models.ModelDTO
{
    public class EmployeeDTO
    {
        public int EmployeeID { get; set; }
        public string AutoCode { get; set; }
        public string EmployeeFullName { get; set; }
        public string EmployeeEmail { get; set; }
        public string EmployeePhone1 { get; set; }
        public string EmployeePhone2 { get; set; }
        public string EmployeeAddress { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public Nullable<System.DateTime> DateOfHiring { get; set; }
        public Nullable<System.DateTime> DateOfeaving { get; set; }
        public string PassportOrIDNumber { get; set; }
        public string PassportORIDIMGURL { get; set; }
        public string ResumeFIleURL { get; set; }
        public string DocumentFileURL { get; set; }
        public Nullable<int> EmployeeTypeIDD { get; set; }
        public Nullable<int> MaritalStatusIDD { get; set; }
        public Nullable<int> GenderIDD { get; set; }
        public Nullable<int> EducationIDD { get; set; }
        public Nullable<int> ProfessionIDD { get; set; }
        public Nullable<int> SalaryIDD { get; set; }
        public Nullable<int> NationalityIDD { get; set; }
        public Nullable<int> CountryIDD { get; set; }
        public Nullable<int> GovernorateIDD { get; set; }
        public Nullable<int> CityIDD { get; set; }
        public Nullable<int> BranchIDD { get; set; }
        public Nullable<System.DateTime> DateOfMaking { get; set; }
        public bool IsStopped { get; set; }
        public bool IsDeleted { get; set; }
    }
}