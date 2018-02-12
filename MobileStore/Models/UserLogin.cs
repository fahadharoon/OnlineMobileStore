using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MobileStore.Models
{
    public class UserLogin
    {
        [Display(Name = "User Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email ID required")]
        public string UserEmail { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}