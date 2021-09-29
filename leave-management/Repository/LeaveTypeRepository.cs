using leave_management.Contracts;
using leave_management.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    /// <summary>
    /// Class responsible for all CRUD operations related to LeaveType data
    /// </summary>
    public class LeaveTypeRepository : ILeaveTypeRepository
    {
        // Application DataBase Context
        private readonly ApplicationDbContext _db;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="db">ApplicationDbContext</param>
        public LeaveTypeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// ADDS the supplied LeaveType data to the Database LeaveTypes table
        /// </summary>
        /// <param name="entity">LeaveType</param>
        /// <returns>Boolean</returns>
        public bool Create(LeaveType entity)
        {
            _db.LeaveTypes.Add(entity);
            return Save();
        }

        /// <summary>
        /// DELETE the given LeaveType from the Database LeaveTypes table
        /// </summary>
        /// <param name="entity">LeaveType</param>
        /// <returns>Boolean</returns>
        public bool Delete(LeaveType entity)
        {
            _db.Remove(entity);
            return Save();
        }

        /// <summary>
        /// Grabs ALL LeaveType objects stored in the database LeaveTypes table
        /// </summary>
        /// <returns>ICollection<LeaveType></returns>
        public ICollection<LeaveType> FindAll()
        {
            var returnTypes = _db.LeaveTypes.ToList();
            return returnTypes;
        }

        /// <summary>
        /// Check to see if LeaveType with supplied ID exists returns that LeaveType object
        /// </summary>
        /// <param name="id"></param>
        /// <returns>LeaveType data object</returns>
        public LeaveType FindById(int id)
        {
            LeaveType leaveType = _db.LeaveTypes.Find(id);
            return leaveType;
        }

        public ICollection<LeaveType> GetEmployeesByLeaveType(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Check to see if LeaveType with supplied ID exists returns true or false
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Boolean</returns>
        public bool isExists(int id)
        {
            var typeExists = _db.LeaveTypes.Any(q => q.Id == id);
            return typeExists;
        }
    
        /// <summary>
        /// Save changes made to the database
        /// </summary>
        /// <returns>Boolean</returns>
        public bool Save()
        {
            var changes = _db.SaveChanges();

            return changes > 0;
        }

        /// <summary>
        /// Update the database context and save those changes
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Boolean</returns>
        public bool Update(LeaveType entity)
        {
            _db.LeaveTypes.Update(entity);
            return Save();
        }
    }
}
