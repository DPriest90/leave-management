using System;
using leave_management.Contracts;
using leave_management.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class LeaveAllocationRepository : ILeaveAllocationRepository
    {

        // Application DataBase Context
        private readonly ApplicationDbContext _db;

        public LeaveAllocationRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool Create(LeaveAllocation entity)
        {
            _db.LeaveAllocations.Add(entity);
            return Save();
        }

        public bool Delete(LeaveAllocation entity)
        {
            _db.Remove(entity);
            return Save();
        }

        public ICollection<LeaveAllocation> FindAll()
        {
            List<LeaveAllocation> leaveAllocations = _db.LeaveAllocations.ToList();
            return leaveAllocations;
        }

        public LeaveAllocation FindById(int id)
        {
            LeaveAllocation leaveAllocation = _db.LeaveAllocations.Find(id);

            _db.LeaveTypes.Find(id);
            return leaveAllocation;
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

