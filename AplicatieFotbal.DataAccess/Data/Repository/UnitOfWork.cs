using AplicatieFotbal.DataAccess.Data.Repository.IRepository;

namespace AplicatieFotbal.DataAccess.Data.Repository
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            League = new LeagueRepository(_db);
            Team = new TeamRepository(_db);
            Player = new PlayerRepository(_db);
            Match = new MatchRepository(_db);
            MatchGoal = new MatchGoalRepository(_db);
        }

        public ILeagueRepository League { get; private set; }
        public ITeamRepository Team { get; private set; }
        public IPlayerRepository Player { get; private set; }
        public IMatchRepository Match { get; private set; }
        public IMatchGoalRepository MatchGoal { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
