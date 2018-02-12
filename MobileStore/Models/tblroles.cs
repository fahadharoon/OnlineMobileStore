using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MobileStore.Models
{
    public class tblroles
    {
        [Key]
        public int roleId { get; set; }
        [Required]
        public string rolename { get; set; }
        public virtual List<Users> users { get; set; }

    }
}