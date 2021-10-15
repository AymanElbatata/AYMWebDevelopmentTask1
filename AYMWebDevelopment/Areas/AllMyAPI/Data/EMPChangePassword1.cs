using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AYMWebDevelopment.Areas.AllMyAPI.Data
{
    public class EMPChangePassword1
    {

        [Display(Name = "New Password"), Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [NotMapped]
        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Confirm Password required")]
        [DataType(DataType.Password)]
        [CompareAttribute("NewPassword", ErrorMessage = "Password doesn't match.")]
        public string PasswordConfirm { get; set; }
    }
}