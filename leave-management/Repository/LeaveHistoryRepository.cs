using System;
using leave_management.Contracts;
using leave_management.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    /// <summary>
    /// Class responsible for all CRUD operations related to LeaveHistory data
    /// </summary>
    public class LeaveHistoryRepository : ILeaveHistoryRepository
    {
        // Application DataBase Context
        private readonly ApplicationDbContext _db;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="db">ApplicationDbContext</param>
        public LeaveHistoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// ADD supplied LeaveHistory data to the Database LeaveHistories table
        /// </summary>
        /// <param name="entity">LeaveHistory</param>
        /// <returns>Boolean</returns>
        public bool Create(LeaveHistory entity)
        {
            _db.LeaveHistories.Add(entity);
            return Save();
        }

        /// <summary>
        /// DELETE supplied LeaveHistory data to the Database LeaveHistories table
        /// </summary>
        /// <param name="entity">LeaveHistory<param>
        /// <returns>Boolean</returns>
        public bool Delete(LeaveHistory entity)
        {
            _db.Remove(entity);
            return Save();
        }

        /// <summary>
        /// Grab all data from the Database LeaveHistories table and returns it as a List<LeaveHistory>
        /// </summary>
        /// <returns>ICollection<LeaveHistory></returns>
        public ICollection<LeaveHistory> FindAll()
        {
            List<LeaveHistory> leaveHistories = _db.LeaveHistories.ToList();
            return leaveHistories;
        }

        /// <summary>
        /// Check to see if LeaveHistory with supplied ID exists and returns that LeaveHistory object
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>LeaveHistory</returns>
        public LeaveHistory FindById(int id)
        {
            LeaveHistory leaveHistory = _db.LeaveHistories.Find(id);

            _db.LeaveTypes.Find(id);
            return leaveHistory;
        }

        /// <summary>
        /// Check to see if LeaveHistory with supplied ID exists and returns true or false
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>Boolean</returns>
        public bool isExists(int id)
        {
            var typeExists = _db.LeaveHistories.Any(q => q.Id == id);
            return typeExists;
        }

        /// <summary>
        /// Saves changes made to the databse
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            var changes = _db.SaveChanges();

            return changes > 0;
        }

        /// <summary>
        /// UPDATES the Database context and then Saves the changes to the Database itself
        /// </summary>
        /// <param name="entity">LeaveHistory</param>
        /// <returns>Boolean</returns>
        public bool Update(LeaveHistory entity)
        {
            _db.LeaveHistories.Update(entity);
            return Save();            
        }
    }
}
