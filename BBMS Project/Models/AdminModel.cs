using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BBMS_Project.Models
{
    public class AdminModel
    {
        [Key]
        [Required(ErrorMessage = "Email id is required!")]
        [Display(Name = "Email")]
        [EmailAddress]
        [RegularExpression(@"^([a-zA-Z0-9.])*@(gmail|amnex|yahoo)\.com$", ErrorMessage = "Entered email is not valid.")]
        public string email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [Display(Name = "Password")]
        public string password { get; set; }
    }
}