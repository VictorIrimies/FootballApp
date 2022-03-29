using AplicatieFotbal.DataAccess.Data.Repository.IRepository;
using AplicatieFotbal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AplicatieFotbal.DataAccess.Data.Repository
{
    public class PlayerRepository : Repository<Player>, IPlayerRepository
    {
        private readonly ApplicationDbContext _db;

        public PlayerRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public bool CheckIfExist(string name)
        {
            return _db.Player.Any(l => l.FullName == name);
        }

        public Player Get(string name)
        {
            return _db.Player.FirstOrDefault(l => l.FullName == name);
        }
    }
}
