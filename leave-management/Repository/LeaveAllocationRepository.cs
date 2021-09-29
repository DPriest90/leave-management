using System;
using leave_management.Contracts;
using leave_management.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace leave_management.Repository
{
    /// <summary>
    /// Class responsible for all CRUD operations related to LeaveAllocation data
    /// </summary>
    public class LeaveAllocationRepository : ILeaveAllocationRepository
    {

        // Application DataBase Context
        private readonly ApplicationDbContext _db;

        /// <summary>
        /// Retrieve the Period (in most if not all cases will be the current year)
        /// </summary>
        private int Period
        {
            get { return DateTime.Now.Year; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="db">ApplicationDbContext</param>
        public LeaveAllocationRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Check to see if LeaveAllocation data exists in Database LeaveAllocations table matching the given parameters, returns true or false
        /// </summary>
        /// <param name="leaveTypeId">int</param>
        /// <param name="employeeId">string</param>
        /// <returns>Boolean</returns>
        public bool CheckAllocation(int leaveTypeId, string employeeId)
        {
            var period = DateTime.Now.Year;

            bool doesAllocationExist = FindAll().Where(q => 
            q.EmployeeId == employeeId &&
            q.LeaveTypeId == leaveTypeId &&
            q.Period == period)
                .Any();

            return doesAllocationExist;
        }

        /// <summary>
        /// ADD new LeaveAllocation to the Database LeaveAllocations table and Save changes to the Database
        /// </summary>
        /// <param name="entity">LeaveAllocation</param>
        /// <returns>Boolean</returns>
        public bool Create(LeaveAllocation entity)
        {
            _db.LeaveAllocations.Add(entity);
            return Save();
        }

        /// <summary>
        /// DELETE the record from the Database LeaveAllocations tablematching the supplied record and Save Database changes
        /// </summary>
        /// <param name="entity">LeaveAllocation</param>
        /// <returns>Boolean</returns>
        public bool Delete(LeaveAllocation entity)
        {
            _db.Remove(entity);
            return Save();
        }

        public ICollection<LeaveAllocation> FindAll()
        {
            List<LeaveAllocation> leaveAllocations = _db.LeaveAllocations
                .Include(q => q.LeaveType)                
                .ToList();
            return leaveAllocations;
        }

        public LeaveAllocation FindById(int id)
        {
            LeaveAllocation leaveAllocation = _db.LeaveAllocations.Include(q => q.LeaveType).Include(q => q.Employee).FirstOrDefault(q => q.Id == id);

            _db.LeaveTypes.Find(id);
            return leaveAllocation;
        }

        public ICollection<LeaveAllocation> GetLeaveAllocationsByEmployee(string id)
        {
            return FindAll().
                Where(q => q.EmployeeId == id && q.Period == Period)
                .ToList();
        }

        public bool isExists(int id)
        {
            var typeExists = _db.LeaveAllocations.Any(q => q.Id == id);
            return typeExists;
        }

        public bool Save()
        {
            var changes = _db.SaveChanges();

            return changes > 0;
        }

        public bool Update(LeaveAllocation entity)
        {
            _db.LeaveAllocations.Update(entity);
            return Save();
        }
    }
}

