using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AplicatieFotbal.Models
{
    public class MatchGoal
    {
        [Key]
        public int Id { get; set; }
        public int MatchId { get; set; }
        [ForeignKey("MatchId")]
        public Match Match { get; set; }
        public int Minute { get; set; }
        public int PlayerId { get; set; }
        [ForeignKey("PlayerId")]
        public Player Player { get; set; }
        public bool IsOwnGoal { get; set; }
        
        
        
    }
}
