using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AYMWebDevelopment.Areas.AllMyAPI.Data
{
    public class ForLoginData
    {
        [Display(Name = "E-mail"), Required]
        [DataType(DataType.EmailAddress)]
        public string loginemail { get; set; }

        //[Required(ErrorMessage = "Password is required.")]
        [Display(Name = "Password"), Required]
        [DataType(DataType.Password)]
        public string loginpassword { get; set; }
    }
}