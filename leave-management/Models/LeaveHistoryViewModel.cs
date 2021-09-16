using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Models
{
    public class LeaveHistoryViewModel
    {     
        public int Id { get; set; }

        public EmployeeViewModel RequestingEmployee { get; set; }

        public string RequestingEmployeeId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public DetailsLeaveTypeViewModel LeaveType { get; set; }

        public int LeaveTypeId { get; set; }

        public DateTime DateRequested { get; set; }

        public DateTime DateActioned { get; set; }

        public bool? Approved { get; set; }

        public EmployeeViewModel ApprovedBy { get; set; }
        
        public string ApprovedById { get; set; }

        /* Lists will allow us to populate a drop down box or some other control to allow us to select from a list */
        public IEnumerable<SelectListItem> LeaveTypes { get; set; }
    }
}
