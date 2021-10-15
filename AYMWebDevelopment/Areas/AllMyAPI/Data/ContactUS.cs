using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AYMWebDevelopment.Areas.AllMyAPI.Data
{
    public class ContactUS
    {
        [Display(Name = "Name"), Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Display(Name = "E-mail"), Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Message"), Required]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }
    }
}