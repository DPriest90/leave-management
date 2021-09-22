﻿using System;
using leave_management.Contracts;
using leave_management.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class LeaveHistoryRepository : ILeaveHistoryRepository
    {
        // Application DataBase Context
        private readonly ApplicationDbContext _db;

        public LeaveHistoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool Create(LeaveHistory entity)
        {
            _db.LeaveHistories.Add(entity);
            return Save();
        }
        
        public bool Delete(LeaveHistory entity)
        {
            _db.Remove(entity);
            return Save();
        }

        public ICollection<LeaveHistory> FindAll()
        {
            List<LeaveHistory> leaveHistories = _db.LeaveHistories.ToList();
            return leaveHistories;
        }

        public LeaveHistory FindById(int id)
        {
            LeaveHistory leaveHistory = _db.LeaveHistories.Find(id);

            _db.LeaveTypes.Find(id);
            return leaveHistory;
        }

        public bool isExists(int id)
        {
            var typeExists = _db.LeaveHistories.Any(q => q.Id == id);
            return typeExists;
        }

        public bool Save()
        {
            var changes = _db.SaveChanges();

            return changes > 0;
        }

        public bool Update(LeaveHistory entity)
        {
            _db.LeaveHistories.Update(entity);
            return Save();            
        }
    }
}
