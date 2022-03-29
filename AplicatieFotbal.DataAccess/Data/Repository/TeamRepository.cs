using AplicatieFotbal.DataAccess.Data.Repository.IRepository;
using AplicatieFotbal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AplicatieFotbal.DataAccess.Data.Repository
{
    public class TeamRepository : Repository<Team>, ITeamRepository
    {
        private readonly ApplicationDbContext _db;

        public TeamRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public bool CheckIfExist(string name)
        {
            return _db.Team.Any(l => l.Name == name);
        }

        public Team Get(string name)
        {
            return _db.Team.FirstOrDefault(l => l.Name == name);
        }
    }
}
