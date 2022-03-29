using AplicatieFotbal.DataAccess.Data.Repository.IRepository;
using AplicatieFotbal.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AplicatieFotbal.Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class LeagueController : Controller
    {
        public IUnitOfWork _unitOfWork;
        public LeagueController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(int leagueId)
        {
            var league = _unitOfWork.League.GetAllData(leagueId);
            if(league.Matches != null)
            {
                var totalGoals = league.Matches.Sum(m => m.Team1Goals.Count) + league.Matches.Sum(m => m.Team2Goals.Count);
                var totalYCards = league.Matches.Sum(m => m.Team1YellowCards) + league.Matches.Sum(m => m.Team2YellowCards);
                var totalRcards = league.Matches.Sum(m => m.Team1RedCards) + league.Matches.Sum(m => m.Team2RedCards);
                var totalCorners = league.Matches.Sum(m => m.Team1Corners) + league.Matches.Sum(m => m.Team2Corners);
                var totalFouls = league.Matches.Sum(m => m.Team1Fouls) + league.Matches.Sum(m => m.Team2Fouls);
                var totalOffsides = league.Matches.Sum(m => m.Team1Offside) + league.Matches.Sum(m => m.Team2Offside);
                var totalGkSaves = league.Matches.Sum(m => m.Team1GKSaves) + league.Matches.Sum(m => m.Team2GKSaves);

                var teams = league.Matches.Select(m => m.Team1).Union(league.Matches.Select(m => m.Team2));
                foreach (var team in teams)
                {
                    var wins = 0;
                    var draws = 0;
                    var losses = 0;
                 

                    team.NumberOfCorners = league.Matches.Where(m => m.TeamId1 == team.Id).Sum(m => m.Team1Corners) + league.Matches.Where(m => m.TeamId2 == team.Id).Sum(m => m.Team2Corners);
                    team.NumberOfYCards = league.Matches.Where(m => m.TeamId1 == team.Id).Sum(m => m.Team1YellowCards) + league.Matches.Where(m => m.TeamId2 == team.Id).Sum(m => m.Team2YellowCards);
                    team.NumberOfRCards = league.Matches.Where(m => m.TeamId1 == team.Id).Sum(m => m.Team1RedCards) + league.Matches.Where(m => m.TeamId2 == team.Id).Sum(m => m.Team2RedCards);
                    team.NumberOfGoals = league.Matches.Where(m => m.Team1.Id == team.Id).Sum(m => m.Team1Goals.Count) + league.Matches.Where(m => m.Team2.Id == team.Id).Sum(m => m.Team2Goals.Count);
                    team.NumberOfOffsides = league.Matches.Where(m => m.TeamId1 == team.Id).Sum(m => m.Team1Offside) + league.Matches.Where(m => m.TeamId2 == team.Id).Sum(m => m.Team2Offside);
                    team.NumberOfGkSaves = league.Matches.Where(m => m.TeamId1 == team.Id).Sum(m => m.Team1GKSaves) + league.Matches.Where(m => m.TeamId2 == team.Id).Sum(m => m.Team2GKSaves);
                    team.NumberOfFouls = league.Matches.Where(m => m.TeamId1 == team.Id).Sum(m => m.Team1Fouls) + league.Matches.Where(m => m.TeamId2 == team.Id).Sum(m => m.Team2Fouls);

                    var matches = league.Matches.Where(m => m.TeamId1 == team.Id || m.TeamId2 == team.Id).ToList();
                    foreach (var match in matches)
                    {

                        var firstTeamGoals = match.Team1Goals.Count;
                        var secoundTeamGoals = match.Team2Goals.Count;

                        if(secoundTeamGoals == firstTeamGoals)
                        {
                            draws++;
                        }
                        else
                        {
                            var isFirstTeam = match.Team1.Id == team.Id;
                            if (isFirstTeam)
                            {
                                if (firstTeamGoals > secoundTeamGoals)
                                    wins++;

                                if (firstTeamGoals < secoundTeamGoals)
                                    losses++;
                            }
                            else
                            {
                                if (firstTeamGoals < secoundTeamGoals)
                                    wins++;

                                if (firstTeamGoals > secoundTeamGoals)
                                    losses++;
                            }
                        }
                    }

                    team.NumberOfDraws = draws;
                    team.NumberOfLosses = losses;
                    team.NumberOfWins = wins;
                }

                var players = league.Players;
                var team1Goals = league.Matches.Select(m => m.Team1Goals).ToList();
                var team2Goals = league.Matches.Select(m => m.Team2Goals).ToList();

                var matchGoals = new List<Models.MatchGoal>();

                foreach (var match in league.Matches)
                {
                    matchGoals.AddRange(match.Team1Goals);
                    matchGoals.AddRange(match.Team2Goals);
                }
                foreach (var player in players)
                {
                    player.NumberOfGoals = matchGoals.Count(mg => mg.PlayerId == player.Id);
                }

              


                var topCornersTeamList = teams.OrderByDescending(t => t.NumberOfCorners).ToList();
                var topYCardsTeamList = teams.OrderByDescending(t => t.NumberOfYCards).ToList();
                var topRCardsteamList = teams.OrderByDescending(t => t.NumberOfRCards).ToList();
                var topGoalsTeamList = teams.OrderByDescending(t => t.NumberOfGoals).ToList();
                var topOffsidesTeamList = teams.OrderByDescending(t => t.NumberOfOffsides).ToList();
                var topDrawsTeamList = teams.OrderByDescending(t => t.NumberOfDraws).ToList();
                var topWinsTeamList = teams.OrderByDescending(t => t.NumberOfWins).ToList();
                var topLossesTeamList = teams.OrderByDescending(t => t.NumberOfLosses).ToList();
                var topGkSavesTeamList = teams.OrderByDescending(t => t.NumberOfGkSaves).ToList();
                var topFoulsTeamList = teams.OrderByDescending(t => t.NumberOfFouls).ToList();
                var topScorers = players.OrderByDescending(p => p.NumberOfGoals).ToList();

                var dataPerLeague = new DataPerLeague
                {
                    Totals = new Totals
                    {
                        TotalGoals = totalGoals,
                        TotalYCards = totalYCards,
                        TotalRCards = totalRcards,
                        TotalCorners = totalCorners,
                        TotalFouls = totalFouls,
                        TotalOffsides = totalOffsides,
                        TotalGKSaves = totalGkSaves,
                    },
                    Tops = new Tops
                    {
                        TopTeamCorners = topCornersTeamList,
                        TopTeamOffsides = topOffsidesTeamList,
                        TopTeamYCards = topYCardsTeamList,
                        TopTeamRCards = topRCardsteamList,
                        TopTeamGoals = topGoalsTeamList,
                        TopTeamDraws = topDrawsTeamList,
                        TopTeamWins = topWinsTeamList,
                        TopTeamLosses = topLossesTeamList,
                        TopTeamGkSaves = topGkSavesTeamList,
                        TopTeamFouls = topFoulsTeamList,
                        TopScorers = topScorers
                    },
                    Matches = league.Matches
                };

                return View(dataPerLeague);
            }
            return View(new DataPerLeague());
        }

       
        public IActionResult TopStats()
        {
            return View();
        }
        public IActionResult LeagueStats()
        {
            return View();
        }
        public IActionResult TopWins()
        {
            return View();
        }
        public IActionResult TopDraws()
        {
            return View();
        }
        public IActionResult TopLosses()
        {
            return View();
        }
        public IActionResult TopCorners()
        {
            return View();
        }
        public IActionResult TopGoals()
        {
            return View();
        }
        public IActionResult TopFouls()
        {
            return View();
        }
        public IActionResult TopRedCards()
        {
            return View();
        }
        public IActionResult TopYellowCards()
        {
            return View();
        }
        public IActionResult TopScorers()
        {
            return View();
        }
        public IActionResult TopGkSaves()
        {
            return View();
        }
        public IActionResult Matches()
        {
            return View();

        }

    }
}
