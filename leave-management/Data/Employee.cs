using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace leave_management.Data
{
    public class Employee : IdentityUser
    {
        [Display(Name="First Name")]
        public string Firstname { get; set; }

        [Display(Name = "Last Name")]
        public string Lastname { get; set; }

        public int TaxId { get; set; }

        [Display(Name = "DoB")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Date Joined")]
        public DateTime DateJoined { get; set; }

        public DateTime DateCreated { get; set; }

    }
}
