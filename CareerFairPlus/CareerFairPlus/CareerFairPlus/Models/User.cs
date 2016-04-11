using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CareerFairPlus.Models
{
    public class User
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember on this computer")]
        public bool RememberMe { get; set; }
        
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        [Display(Name="Role")]
        public string Role { get; set; }
        
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "UIN")]
        public double UIN { get; set; }

        [Display(Name = "Major")]
        public string Major { get; set; }

        [Display(Name = "Degree")]
        public string Degree { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Phone")]
        public double Phone { get; set; }
    }
}