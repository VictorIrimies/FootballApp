using AplicatieFotbal.Models;
using AplicatieFotbal.Utility;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplicatieFotbal.Scrape
{
    public static class GetData
    {
        public static List<Match> GetMatches(DateTime startDate, DateTime endDate)
        {

            var matches = new List<Match>();

            var web = new HtmlWeb();
            var startYear = startDate.Year;

            for (int y = startYear; y <= endDate.Year; y++)
            {
                var startMonth = startDate.Month;
                var finalMonth = endDate.Month;

                for (int m = startMonth; m <= finalMonth; m++)
                {
                    var numberOfDays = endDate.Day - 1;
                    var startDay = startDate.Day;
                    for (int d = startDay; d <= numberOfDays; d++)
                    {
                        var stringMonth = (m < 10 ? "0" + m : m.ToString());
                        var stringDay = (d < 10 ? "0" + d : d.ToString());

                        var url = $"https://www.espn.in/football/fixtures/_/date/{y}{stringMonth}{stringDay}";
                        var doc = web.Load(url);

                        var numberOfLeagues = 0;
                        var numberOfLeaguesNode = doc.DocumentNode.SelectNodes("/html/body/div[4]/section/section/div/section/div/div/div[2]/div");
                        if (numberOfLeaguesNode == null)
                        {                       
                            numberOfLeaguesNode = doc.DocumentNode.SelectNodes("/html/body/div[4]/section/section");
                            numberOfLeagues = numberOfLeaguesNode.Count();
                        }

                        if (numberOfLeaguesNode == null)                         
                        {
                            numberOfLeaguesNode = doc.DocumentNode.SelectNodes("/html/body/div[5]/section/section/div/section/div/div");
                            numberOfLeagues = numberOfLeaguesNode.Count();      
                        }

                        if (numberOfLeaguesNode == null)                       
                        {
                            numberOfLeaguesNode = doc.DocumentNode.SelectNodes("/html/body/div[5]/section/section/div/section/div/div/div[2]/div");
                            numberOfLeagues = numberOfLeaguesNode.Count();
                        }
                        else
                        {
                            numberOfLeagues = numberOfLeaguesNode.Count();
                        }

                        for (int i = 2; i <= numberOfLeagues; i++)
                        {
                            
                            var numberOfMatches = doc.DocumentNode.SelectNodes($"//*[@id=\"sched-container\"]/div[{i}]/table/tbody/tr[@class=\"odd has-results\"]").Count;

                            for (int j = 1; j <= numberOfMatches; j++)
                            {
                                

                                var xPathFirstTeam = $"//*[@id=\"sched-container\"]/div[{i}]/table/tbody/tr[@class=\"odd has-results\"][{j}]/td[1]/a/span";
                                var xPathSecondTeam = $"//*[@id=\"sched-container\"]/div[{i}]/table/tbody/tr[@class=\"odd has-results\"][{j}]/td[2]/a/span";
                                var xPathScore = $"//*[@id=\"sched-container\"]/div[{i}]/table/tbody/tr[@class=\"odd has-results\"][{j}]/td[1]/span[2]/a";

                                var nodeFirstTeam = doc.DocumentNode.SelectSingleNode(xPathFirstTeam);
                                var firstTeamName = nodeFirstTeam.InnerText;

                                var nodeSecondTeam = doc.DocumentNode.SelectSingleNode(xPathSecondTeam);
                                var secondTeamName = nodeSecondTeam.InnerText;

                                var nodeScore = doc.DocumentNode.SelectSingleNode(xPathScore);
                                var score = nodeScore.InnerText;

                                var matchUrl = "https://www.espn.in" + nodeScore.Attributes["href"].Value.Replace("report", "matchstats");

                                var docMatchPage = web.Load(matchUrl);

                                var leagueXPath = "//*[@id=\"gamepackage-matchup-wrap--soccer\"]/div[1]";
                                var leagueNode = docMatchPage.DocumentNode.SelectSingleNode(leagueXPath);
                                var leagueName = leagueNode?.InnerText;

                                var team1Goals = new List<MatchGoal>();

                                var resumeXPath = $"//*[@id=\"gamepackage-matchup-wrap--soccer\"]/div[2]/div[2]/span[1]";
                                var resumeNode = docMatchPage.DocumentNode.SelectSingleNode(resumeXPath);
                                var resumeText = resumeNode?.InnerText;

                                if (resumeText != "Postponed")
                                {
                                    var goalsNumberOfRowsFirstTeam = docMatchPage.DocumentNode.SelectNodes($"//*[@id=\"custom-nav\"]/div[1]/div/div/div[1]/div/ul")?.Count;

                                    if(goalsNumberOfRowsFirstTeam != null)
                                    {
                                        Parallel.For(1, (int)goalsNumberOfRowsFirstTeam + 1, nc =>
                                        {
                                            var goalsNumberOfColumnsPerRow = docMatchPage.DocumentNode.SelectNodes($"//*[@id=\"custom-nav\"]/div[1]/div/div/div[1]/div/ul[{nc}]/li").Count;

                                            Parallel.For(1, goalsNumberOfColumnsPerRow + 1, ngst =>
                                            {
                                                var goalPath = $"//*[@id=\"custom-nav\"]/div[1]/div/div/div[1]/div/ul[{nc}]/li[{ngst}]";
                                                var goalNode = docMatchPage.DocumentNode.SelectSingleNode(goalPath);
                                                var goal = goalNode?.InnerText.Replace("\n", "").Replace("\t", "").Replace("  ", "");
                                                var goalMinute = "0";
                                                if (goal != null)
                                                {
                                                    goalMinute = StringHelper.GetStringBetweenCharacters(goal, '(', '\'');
                                                }
                                                var playerName = goal.Split('(')[0];
                                                var matchGoal = new MatchGoal
                                                {
                                                    Minute = Convert.ToInt32(goalMinute),
                                                    Player = new Player
                                                    {
                                                        FullName = playerName
                                                    }
                                                };
                                                team1Goals.Add(matchGoal);
                                            });

                                        });
                                    }


                                    var team2Goals = new List<MatchGoal>();

                                    var goalsNumberOfRowsSecondTeam = docMatchPage.DocumentNode.SelectNodes($"//*[@id=\"custom-nav\"]/div[1]/div/div/div[2]/div/ul")?.Count;

                       
                                    

                                    if (goalsNumberOfRowsSecondTeam != null)
                                    {
                                        Parallel.For(1, (int)goalsNumberOfRowsSecondTeam + 1, nc =>
                                        {
                                            var goalsNumberOfColumnsPerRow = docMatchPage.DocumentNode.SelectNodes($"//*[@id=\"custom-nav\"]/div[1]/div/div/div[2]/div/ul[{nc}]/li").Count;
                                            Parallel.For(1, goalsNumberOfColumnsPerRow + 1, ngst =>
                                            {
                                                var goalPath = $"//*[@id=\"custom-nav\"]/div[1]/div/div/div[2]/div/ul[{nc}]/li[{ngst}]";

                                                var goalNode = docMatchPage.DocumentNode.SelectSingleNode(goalPath);
                                                var goal = goalNode?.InnerText.Replace("\n", "").Replace("\t", "").Replace("  ", "");
                                                var goalMinute = "0";
                                                if (goal != null)
                                                {
                                                    goalMinute = StringHelper.GetStringBetweenCharacters(goal, '(', '\'');
                                                }
                                                var playerName = goal.Split('(')[0];
                                                var matchGoal = new MatchGoal
                                                {
                                                    Minute = Convert.ToInt32(goalMinute),
                                                    Player = new Player
                                                    {
                                                        FullName = playerName
                                                    }
                                                };
                                                team2Goals.Add(matchGoal);
                                            });

                                        });
                                    }

                                    var xPathFoulsFirstTeam = $"//*[@id=\"gamepackage-soccer-match-stats\"]/div/div/div[2]/table/tbody/tr[1]/td[1]";
                                    var xPathFoulsSecondTeam = $"//*[@id=\"gamepackage-soccer-match-stats\"]/div/div/div[2]/table/tbody/tr[1]/td[3]";

                                    var nodeFoulsFirstTeam = docMatchPage.DocumentNode.SelectSingleNode(xPathFoulsFirstTeam);
                                    var foulsFirstTeam = nodeFoulsFirstTeam?.InnerText;

                                    var nodeFoulsSecondTeam = docMatchPage.DocumentNode.SelectSingleNode(xPathFoulsSecondTeam);
                                    var foulsSecondTeam = nodeFoulsSecondTeam?.InnerText;

                                    var xPathYellowFirstTeam = $"//*[@id=\"gamepackage-soccer-match-stats\"]/div/div/div[2]/table/tbody/tr[2]/td[1]";
                                    var xPathYellowSecondTeam = $"//*[@id=\"gamepackage-soccer-match-stats\"]/div/div/div[2]/table/tbody/tr[2]/td[3]";

                                    var nodeYellowFirstTeam = docMatchPage.DocumentNode.SelectSingleNode(xPathYellowFirstTeam);
                                    var yellowFirstTeam = nodeYellowFirstTeam?.InnerText;

                                    var nodeYellowSecondTeam = docMatchPage.DocumentNode.SelectSingleNode(xPathYellowSecondTeam);
                                    var yellowSecondTeam = nodeYellowSecondTeam?.InnerText;

                                    var xPathRedFirstTeam = $"//*[@id=\"gamepackage-soccer-match-stats\"]/div/div/div[2]/table/tbody/tr[3]/td[1]";
                                    var xPathRedSecondTeam = $"//*[@id=\"gamepackage-soccer-match-stats\"]/div/div/div[2]/table/tbody/tr[3]/td[3]";

                                    var nodeRedFirstTeam = docMatchPage.DocumentNode.SelectSingleNode(xPathRedFirstTeam);
                                    var redFirstTeam = nodeRedFirstTeam?.InnerText;

                                    var nodeRedSecondTeam = docMatchPage.DocumentNode.SelectSingleNode(xPathRedSecondTeam);
                                    var redSecondTeam = nodeRedSecondTeam?.InnerText;

                                    var xPathOffsideFirstTeam = $"//*[@id=\"gamepackage-soccer-match-stats\"]/div/div/div[2]/table/tbody/tr[4]/td[1]";
                                    var xPathOffsideSecondTeam = $"//*[@id=\"gamepackage-soccer-match-stats\"]/div/div/div[2]/table/tbody/tr[4]/td[3]";

                                    var nodeOffsideFirstTeam = docMatchPage.DocumentNode.SelectSingleNode(xPathOffsideFirstTeam);
                                    var offsideFirstTeam = nodeOffsideFirstTeam?.InnerText;

                                    var nodeOffsideSecondTeam = docMatchPage.DocumentNode.SelectSingleNode(xPathOffsideSecondTeam);
                                    var offsideSecondTeam = nodeOffsideSecondTeam?.InnerText;

                                    var xPathCornerFirstTeam = $"//*[@id=\"gamepackage-soccer-match-stats\"]/div/div/div[2]/table/tbody/tr[5]/td[1]";
                                    var xPathCornerSecondTeam = $"//*[@id=\"gamepackage-soccer-match-stats\"]/div/div/div[2]/table/tbody/tr[5]/td[3]";

                                    var nodeCornerFirstTeam = docMatchPage.DocumentNode.SelectSingleNode(xPathCornerFirstTeam);
                                    var cornerFirstTeam = nodeCornerFirstTeam?.InnerText;

                                    var nodeCornerSecondTeam = docMatchPage.DocumentNode.SelectSingleNode(xPathCornerSecondTeam);
                                    var cornerSecondTeam = nodeCornerSecondTeam?.InnerText;


                                    var xPathSaveFirstTeam = $"//*[@id=\"gamepackage-soccer-match-stats\"]/div/div/div[2]/table/tbody/tr[6]/td[1]";
                                    var xPathSaveSecondTeam = $"//*[@id=\"gamepackage-soccer-match-stats\"]/div/div/div[2]/table/tbody/tr[6]/td[3]";

                                    var nodeSaveFirstTeam = docMatchPage.DocumentNode.SelectSingleNode(xPathSaveFirstTeam);
                                    var saveFirstTeam = nodeSaveFirstTeam?.InnerText;

                                    var nodeSaveSecondTeam = docMatchPage.DocumentNode.SelectSingleNode(xPathSaveSecondTeam);
                                    var saveSecondTeam = nodeSaveSecondTeam?.InnerText;

                                    var match = new Match
                                    {
                                        League = new League
                                        {
                                            Name = leagueName
                                        },
                                        Team1 = new Team
                                        {
                                            Name = firstTeamName
                                        },
                                        Team2 = new Team
                                        {
                                            Name = secondTeamName
                                        },
                                        Date = new DateTime(y, m, d),
                                        Team1Corners = Convert.ToInt32(cornerFirstTeam),
                                        Team1Fouls = Convert.ToInt32(foulsFirstTeam),
                                        Team1GKSaves = Convert.ToInt32(saveFirstTeam),
                                        Team1Offside = Convert.ToInt32(offsideFirstTeam),
                                        Team1RedCards = Convert.ToInt32(redFirstTeam),
                                        Team1YellowCards = Convert.ToInt32(yellowFirstTeam),
                                        Team1Goals = team1Goals,
                                        Team2Corners = Convert.ToInt32(cornerSecondTeam),
                                        Team2Fouls = Convert.ToInt32(foulsSecondTeam),
                                        Team2GKSaves = Convert.ToInt32(saveSecondTeam),
                                        Team2Offside = Convert.ToInt32(offsideSecondTeam),
                                        Team2RedCards = Convert.ToInt32(redSecondTeam),
                                        Team2YellowCards = Convert.ToInt32(yellowSecondTeam),
                                        Team2Goals = team2Goals
                                    };

                                    matches.Add(match);
                                }
                            }
                        }
                    }
                }
            }
            return matches;
        }

    }
}
