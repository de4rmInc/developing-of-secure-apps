using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Laba1_sql_injection.Models
{
    public class UserViewModel
    {
        [Display(Name = "Username or Email")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Use safe verification")]
        public bool Checked { get; set; }
    }
}