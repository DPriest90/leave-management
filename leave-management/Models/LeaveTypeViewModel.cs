﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Models
{
    public class LeaveTypeViewModel
    {   
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]        
        [Display(Name="Default Number Of Days")]
        [Range(1, 30, ErrorMessage = "Please enter a valid number")]
        public int DefaultDays { get; set; }

        [Display(Name="Date Created")]
        public DateTime? DateCreated { get; set; }
    }

    
}
