using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AYMWebDevelopment.Areas.AllMyAPI.Data
{
    public class ForLoginDataEmail
    {
        [Display(Name = "E-mail"), Required]
        [DataType(DataType.EmailAddress)]
        public string loginemail { get; set; }
    }
}