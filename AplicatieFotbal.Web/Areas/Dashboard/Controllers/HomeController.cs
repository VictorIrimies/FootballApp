using AplicatieFotbal.DataAccess.Data.Repository.IRepository;
using AplicatieFotbal.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AplicatieFotbal.Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var leagues = _unitOfWork.League.GetAll();
            return View(leagues);
        }

        [HttpGet]
        public JsonResult GetData(DateTime startDate, DateTime endDate)
        {

            var matches = Scrape.GetData.GetMatches(startDate,endDate);
            foreach (var match in matches)
            {
                int leagueId = AddLeague(match.League);
                int team1Id = AddTeam(match.Team1);
                int team2Id = AddTeam(match.Team2);

                match.League = null;
                AddMatch(match, leagueId, team1Id, team2Id);


                foreach (var matchGoal in match.Team1Goals)
                    AddGoal1(match, matchGoal);

                foreach (var matchGoal in match.Team2Goals)
                    AddGoal2(match, matchGoal);
            }

            return new JsonResult(true);
        }

        public void AddGoal1(Match match, MatchGoal matchGoal)
        {
            var player1Exist = _unitOfWork.Player.CheckIfExist(matchGoal.Player.FullName);
            var player1Id = 0;
            if (player1Exist)
            {
                var dbplayer1 = _unitOfWork.Player.Get(matchGoal.Player.FullName);
                player1Id = dbplayer1.Id;
            }
            else
            {
                matchGoal.Player.TeamId = match.TeamId1;
                var dbplayer1 = _unitOfWork.Player.Add(matchGoal.Player);
                player1Id = dbplayer1.Id;
            }

            matchGoal.PlayerId = player1Id;
            matchGoal.MatchId = match.Id;
            matchGoal.Player = null;
            _unitOfWork.MatchGoal.Add(matchGoal);
        }

        public void AddGoal2(Match match, MatchGoal matchGoal)
        {
            var player2Exist = _unitOfWork.Player.CheckIfExist(matchGoal.Player.FullName);
            var player2Id = 0;
            if (player2Exist)
            {
                var dbplayer2 = _unitOfWork.Player.Get(matchGoal.Player.FullName);
                player2Id = dbplayer2.Id;
            }
            else
            {
                matchGoal.Player.TeamId = match.TeamId2;
                var dbplayer2 = _unitOfWork.Player.Add(matchGoal.Player);
                player2Id = dbplayer2.Id;
            }


            matchGoal.PlayerId = player2Id;
            matchGoal.MatchId = match.Id;
            matchGoal.Player = null;
            _unitOfWork.MatchGoal.Add(matchGoal);
        }

        public void AddMatch(Match match, int leagueId, int team1Id, int team2Id)
        {
            match.LeagueId = leagueId;
            match.TeamId1 = team1Id;
            match.TeamId2 = team2Id;
            var dbMatch = _unitOfWork.Match.Add(match);
        }

        public int AddLeague(League league)
        {
            var leagExist = _unitOfWork.League.CheckIfExist(league.Name);
            int leagueId;
            if (leagExist)
            {
                var dbLeague = _unitOfWork.League.Get(league.Name);
                leagueId = dbLeague.Id;
            }
            else
            {
                var dbLeague = _unitOfWork.League.Add(league);
                leagueId = dbLeague.Id;
            }
            return leagueId;
        }

        public int AddTeam(Team team)
        {
            var teamExist = _unitOfWork.Team.CheckIfExist(team.Name);
            int teamId;
            if (teamExist)
            {
                var dbTeam = _unitOfWork.Team.Get(team.Name);
                teamId = dbTeam.Id;
            }
            else
            {
                var dbTeam = _unitOfWork.Team.Add(team);
                teamId = dbTeam.Id;
            }
            return teamId;
        }
                
    }

}
