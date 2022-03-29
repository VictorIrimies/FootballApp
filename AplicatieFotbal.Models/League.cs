using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AplicatieFotbal.Models
{
    public class League
    {
        [Key]

        public int Id { get; set; }
        public string Name { get; set; }
        public string Area { get; set; }
        [NotMapped]
        public List<Match> Matches { get; set; }
        [NotMapped]
        public List<Player> Players { get; set; } = new List<Player>();
        [NotMapped]
        public List<Team> Teams { get; set; } = new List<Team>();
    }
}
