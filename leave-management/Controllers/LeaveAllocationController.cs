using AutoMapper;
using Microsoft.AspNetCore.Razor.TagHelpers;
using leave_management.Contracts;
using leave_management.Data;
using leave_management.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Controllers
{
    public class LeaveAllocationController : Controller
    {
        // Repositories and AutoMapper Instantiation
        private readonly ILeaveTypeRepository _leaveTypeRepo;
        private readonly ILeaveAllocationRepository _leaveAllocationRepo;
        private readonly UserManager<Employee> _userManager;
        private readonly IMapper _mapper;

        /// <summary>
        /// Get and return a list of all registered 'Employee' users
        /// </summary>
        private IList<Employee> GetEmployeesList
        {
            get { return _userManager.GetUsersInRoleAsync("Employee").Result; }
        }        

        /// <summary>
        /// Constructor - Initialize Repo's and AutoMapper Instances
        /// </summary>
        /// <param name="leaveTypeRepo"></param>
        /// <param name="leaveAllocationRepo"></param>
        /// <param name="mapper"></param>
        public LeaveAllocationController(ILeaveTypeRepository leaveTypeRepo,
            ILeaveAllocationRepository leaveAllocationRepo,
            IMapper mapper,
            UserManager<Employee> userManager)
        {
            _leaveAllocationRepo = leaveAllocationRepo;
            _leaveTypeRepo = leaveTypeRepo;
            _userManager = userManager;
            _mapper = mapper;   
        }

        // GET: LeaveAllocationController
        // Only Administrators are allowed access to pages in this Controller
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            var leaveTypes = _leaveTypeRepo.FindAll().ToList();
            var mappedLeaveTypes = _mapper.Map<List<LeaveType>, List<LeaveTypeViewModel>>(leaveTypes);
            var model = new CreateLeaveAllocationViewModel
            {
                LeaveTypes = mappedLeaveTypes,
                NumberUpdated = 0
            };

            return View(model);            
        }

        /// <summary>
        /// For all 'Employee' users, allocate the default number of days leave for the selected Leave Type
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Redirects to LeaveAllocation Index View</returns>
        public ActionResult SetLeave(int id)
        {
            // Get the leave the user selected, based on LeaveTypeId
            var leaveType = _leaveTypeRepo.FindById(id);
            // Get a list of ALL 'Employee' users
            var employees = GetEmployeesList;

            // Iterate through all the found 'Employee' users
            foreach (Employee emp in employees)
            {   
                // If this user already has their days allocated for this LeaveType then skip to the newxt 'Employee' user
                if (_leaveAllocationRepo.CheckAllocation(id, emp.Id))
                    continue;

                // Create a new LeaveAllocation object and fill with the required data
                var allocation = new LeaveAllocationViewModel
                {
                    DateCreated = DateTime.Now,
                    EmployeeId = emp.Id,
                    LeaveTypeId = id,
                    NumberOfDays = leaveType.DefaultDays,
                    Period = DateTime.Now.Year,
                };

                // We want to Map our new allocation from the ViewModel to the Data class
                var leaveAllocation = _mapper.Map<LeaveAllocation>(allocation);                

                // Add our new LeaveAllocation record this 'Employee' user to the LeaveAllocation Database Context and then Save to the Database
                _leaveAllocationRepo.Create(leaveAllocation);
            }

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Opens a new page with a list of all 'Employee' users
        /// </summary>
        /// <returns>View(model)</returns>
        public ActionResult ListEmployees()
        {
            var employees = GetEmployeesList;
            var model = _mapper.Map<List<EmployeeViewModel>>(employees);

            return View(model);
        }

        // GET: LeaveAllocationController/Details/5
        public ActionResult Details(string id)
        {
            // Retrieve the employee record and Map from ViewModel class to Data Class
            var employee = _mapper.Map<EmployeeViewModel>(_userManager.FindByIdAsync(id).Result);

            // Retriece the LeaveAllocation records pertaining to this employee and the current period and then map
            var allocations = _mapper.Map<List<LeaveAllocationViewModel>>(_leaveAllocationRepo.GetLeaveAllocationsByEmployee(id));

            var model = new ViewAllocationsViewModel{
                Employee = employee,
                LeaveAllocations = allocations,
            };

            return View(model);
        }

        // GET: LeaveAllocationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LeaveAllocationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LeaveAllocationController/Edit/5
        public ActionResult Edit(int id)
        {
            // Get LeaveAllocation record by Id and Map from LeaveAllocation object to EditLeaveAllocationViewModel object
            var model = _mapper.Map<EditLeaveAllocationViewModel>(_leaveAllocationRepo.FindById(id));
                
            return View(model);
        }

        // POST: LeaveAllocationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditLeaveAllocationViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                #region Old error prone code

                /* Run into null object errors */
                //var allocation = _mapper.Map<LeaveAllocation>(model);

                //var isSuccess = _leaveAllocationRepo.Update(allocation);

                #endregion

                // Get the LeaveAllocation from the Database table 'LeaveAllocations' by Id
                var record = _leaveAllocationRepo.FindById(model.Id);

                // Manually assign the NEW number of days value
                record.NumberOfDays = model.NumberOfDays;

                // Attempt to Update databse context and then Save the changes to the actual Database
                var isSuccess = _leaveAllocationRepo.Update(record);                

                // If Update failed then add an error to the ModelState and go to Index screen...? Need to check
                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Error when Saving...");
                    return View(model);
                }

                // Everything has worked now return to the "/LeaveAllocations/Details /Id" View
                return RedirectToAction(nameof(Details), new { id = model.EmployeeId });
            }
            catch
            {
                return View(model);
            }
        }

        // GET: LeaveAllocationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LeaveAllocationController/Delete/5        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
