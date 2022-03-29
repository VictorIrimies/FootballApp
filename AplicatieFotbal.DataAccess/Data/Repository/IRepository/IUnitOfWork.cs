using System;

namespace AplicatieFotbal.DataAccess.Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ILeagueRepository League { get; }
        IMatchRepository Match { get; }
        IMatchGoalRepository MatchGoal { get; }
        IPlayerRepository Player { get; }
        ITeamRepository Team { get; }

        void Save();

        
        

    }
} 
