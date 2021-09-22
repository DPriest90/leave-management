﻿using leave_management.Contracts;
using leave_management.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class LeaveTypeRepository : ILeaveTypeRepository
    {
        // Application DataBase Context
        private readonly ApplicationDbContext _db;

        public LeaveTypeRepository(ApplicationDbContext db)
        {
            _db = db;
        } 

        public bool Create(LeaveType entity)
        {
            _db.LeaveTypes.Add(entity);
            return Save();
        }

        public bool Delete(LeaveType entity)
        {
            _db.Remove(entity);
            return Save();
        }

        public ICollection<LeaveType> FindAll()
        {
            var returnTypes = _db.LeaveTypes.ToList();
            return returnTypes;
        }

        public LeaveType FindById(int id)
        {
            LeaveType leaveType = _db.LeaveTypes.Find(id);
            return leaveType;
        }

        public ICollection<LeaveType> GetEmployeesByLeaveType(int id)
        {
            throw new NotImplementedException();
        }

        public bool isExists(int id)
        {
            var typeExists = _db.LeaveTypes.Any(q => q.Id == id);
            return typeExists;
        }
    

        public bool Save()
        {
            var changes = _db.SaveChanges();

            return changes > 0;
        }

        public bool Update(LeaveType entity)
        {
            _db.LeaveTypes.Update(entity);
            return Save();
        }
    }
}
