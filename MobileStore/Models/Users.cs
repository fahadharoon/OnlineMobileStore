using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MobileStore.Models
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string UserEmail { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public bool IsEmailVerified { get; set; }
        public int roleId { get; set; }

        public virtual tblroles roles { get; set; }
        public Guid ActivationCode { get; set; }
    }
}