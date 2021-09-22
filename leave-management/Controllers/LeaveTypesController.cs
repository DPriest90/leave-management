using AutoMapper;
using leave_management.Contracts;
using leave_management.Data;
using leave_management.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Controllers
{
    public class LeaveTypesController : Controller
    {
        // LeaveType specific Repository and Mapper
        private readonly ILeaveTypeRepository _repo;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="mapper"></param>
        public LeaveTypesController(ILeaveTypeRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET: LeaveTypesController
        // GET: LeaveTypesController/Create
        /// <summary>
        /// This function tells the controller to return the View related the current page's "Index" action. I.e. '~https:/localhost:44390/LeaveTypes'
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var leaveTypes = _repo.FindAll().ToList();
            var model = _mapper.Map<List<LeaveType>, List<LeaveTypeViewModel>>(leaveTypes);

            return View(model);
        }

        // GET: LeaveTypesController/Details/5
        // GET: LeaveTypesController/Create
        /// <summary>
        /// This function tells the controller to return the View related the current page's "Details" action. I.e. '~https:/localhost:44390/LeaveTypes/Details'
        /// </summary>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            if (!_repo.isExists(id))
            {
                return NotFound();
            }

            var leaveType = _repo.FindById(id);
            var model = _mapper.Map<LeaveTypeViewModel>(leaveType);

            return View(model);
        }

        // GET: LeaveTypesController/Create
        /// <summary>
        /// This function tells the controller to return the View related the current page's "Create" action. I.e. '~https:/localhost:44390/LeaveTypes/Create'
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        // POST: LeaveTypesController/Create
        // GET: LeaveTypesController/Create
        /// <summary>
        /// Attempts to create a new LeaveType record and insert it to the database. The data entered on the form is stored in the LeaveTypeViewModel function argument which gets mapped into
        /// a LeaveType object
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LeaveTypeViewModel data)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(data);
                }

                // Map the provided data (LeaveTypeViewModel that the user entered on the form) to the LeaveType model (data class) and add todays datetime to it.
                var leaveType = _mapper.Map<LeaveType>(data);
                leaveType.DateCreated = DateTime.Now;


                // Add the user created Leave Type to the database
                var isSuccess = _repo.Create(leaveType);

                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Something went wrong...");
                    return View(data);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                ModelState.AddModelError("", exception.Message);
                return View(data);
            }
        }

        // GET: LeaveTypesController/Edit/5        
        /// <summary>
        /// This function tells the controller to return the View related the current page's "Edit" action. I.e. '~https:/localhost:44390/LeaveTypes/Edit'
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            // Check the LeaveType actually exists
            if (!_repo.isExists(id))
            {
                return NotFound();
            }

            // LeaveType exists so store in a local variable and then map it to relevant object from 'LeaveType'
            var leaveType = _repo.FindById(id);
            var model = _mapper.Map<LeaveTypeViewModel>(leaveType);

            // Show the Edit view with field(s) filled
            return View(model);
        }

        // POST: LeaveTypesController/Edit/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LeaveTypeViewModel data)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(data);
                }

                var leaveType = _mapper.Map<LeaveType>(data);
                var isSuccess = _repo.Update(leaveType);

                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Something went wrong...");
                    return View(data);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                ModelState.AddModelError("", exception.Message);
                return View(data);
            }
        }

        // GET: LeaveTypesController/Delete/5
        public ActionResult Delete(int id, LeaveTypeViewModel model)
        {
            try
            {
                // Get requested Leave Type by the database table ID for that record
                var leaveType = _repo.FindById(id);

                // Null check
                if (leaveType == null)
                    return NotFound();

                // Attempt to delete record
                var isSuccess = _repo.Delete(leaveType);

                if (!isSuccess)
                {
                    return BadRequest();
                }

                // If deletion attempt was successful then return to our LeaveType index page
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }

        //// POST: LeaveTypesController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id)
        //{
        //    return NotFound();
        //    try
        //    {
        //        var leaveType = _repo.FindById(id);
        //        if (leaveType == null)
        //            return NotFound();

        //        var isSuccess = _repo.Delete(leaveType);

        //        if (!isSuccess)
        //        {
        //            return View(model);
        //        }

        //        // If deletion attempt was successful then return to our LeaveType index page
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View(model);
        //    }
        //}
    }
}
