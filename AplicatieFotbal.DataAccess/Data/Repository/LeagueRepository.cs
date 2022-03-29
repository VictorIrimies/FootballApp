using AplicatieFotbal.DataAccess.Data.Repository.IRepository;
using AplicatieFotbal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AplicatieFotbal.DataAccess.Data.Repository
{
    public class LeagueRepository : Repository<League>, ILeagueRepository
    {
        private readonly ApplicationDbContext _db;

        public LeagueRepository(ApplicationDbContext db) : base(db)
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

        public League GetAllData(int id)
        {
            var league = _db.League.FirstOrDefault(l => l.Id == id);
            league.Matches = _db.Match.Where(m => m.LeagueId == league.Id).ToList();
            foreach(var match in league.Matches)
            {
                match.Team1 = _db.Team.Find(match.TeamId1);
                match.Team2 = _db.Team.Find(match.TeamId2);
                var goals = _db.MatchGoal.Where(mg => mg.MatchId == match.Id).ToList();
                foreach(var goal in goals)
                {
                    goal.Player = _db.Player.Find(goal.PlayerId);
                }
                match.Team1Goals = goals.Where(g => g.Player.TeamId == match.Team1.Id).ToList();
                match.Team2Goals = goals.Where(g => g.Player.TeamId == match.Team2.Id).ToList();

                if(!league.Teams.Any(t => t == match.Team1))
                    league.Teams.Add(match.Team1);

                if (!league.Teams.Any(t => t == match.Team2))
                    league.Teams.Add(match.Team2);

                var playersTeam1 = match.Team1Goals.Select(tg => tg.Player).Distinct().ToList();
                var playersTeam2 = match.Team2Goals.Select(tg => tg.Player).Distinct().ToList();

                var players = playersTeam1.Concat(playersTeam2);

                foreach(var player in players)
                {
                    if (!league.Players.Any(p => p == player))
                    {
                        league.Players.Add(player);
                    }
                }

            }
            return league;
        }
    }
}