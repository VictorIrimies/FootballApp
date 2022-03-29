using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AplicatieFotbal.Models
{
    public class Match
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int LeagueId { get; set; }
        [ForeignKey("LeagueId")]
        public League League { get; set; }
        public int TeamId1 { get; set; }
        public int TeamId2 { get; set; }

        public int Team1YellowCards { get; set; }
        public int Team2YellowCards { get; set; }
        public int Team1RedCards { get; set; }
        public int Team2RedCards { get; set; }

        public int Team1Corners { get; set; }
        public int Team2Corners { get; set; }

        public int Team1Fouls { get; set; }
        public int Team2Fouls { get; set; }

        public int Team1Offside { get; set; }
        public int Team2Offside { get; set; }

        public int Team1GKSaves { get; set; }
        public int Team2GKSaves { get; set; }

        [NotMapped]
        public Team Team1 { get; set; }
        [NotMapped]
        public Team Team2 { get; set; }

        [NotMapped]
        public List<MatchGoal> Team1Goals { get; set; }
        [NotMapped]
        public List<MatchGoal> Team2Goals { get; set; }
    }
}
