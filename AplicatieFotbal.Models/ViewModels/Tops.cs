using System.Collections.Generic;

namespace AplicatieFotbal.Models.ViewModels
{
    public class Tops
    {

        public List<Models.Team> TopTeamCorners { get; set; }
        public List<Models.Team> TopTeamOffsides { get; set; }
        public List<Models.Team> TopTeamYCards { get; set; }
        public List<Models.Team> TopTeamRCards { get; set; }
        public List<Models.Team> TopTeamDraws { get; set; }
        public List<Models.Team> TopTeamWins { get; set; }
        public List<Models.Team> TopTeamLosses { get; set; }
        public List<Models.Team> TopTeamGoals { get; set; }
        public List<Models.Team> TopTeamGkSaves { get; set; }
        public List<Models.Team> TopTeamFouls { get; set; }


        public League League { get; set; }
        public Team Team { get; set; }
        public List<Player> TopScorers { get; set; }
    }
}
