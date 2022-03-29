using AplicatieFotbal.DataAccess.Data.Repository.IRepository;
using AplicatieFotbal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AplicatieFotbal.DataAccess.Data.Repository
{
    public class MatchGoalRepository : Repository<MatchGoal>, IMatchGoalRepository
    {
        private readonly ApplicationDbContext _db;

        public MatchGoalRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public bool CheckIfExist(string name)
        {
            return _db.League.Any(l => l.Name == name);
        }

        public League Get(string name)
        {
            return _db.League.FirstOrDefault(l => l.Name == name);
        }

        MatchGoal IMatchGoalRepository.Get(string name)
        {
            throw new NotImplementedException();
        }
    }
}
