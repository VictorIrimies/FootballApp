using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AplicatieFotbal.Models
{
    public class Player
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Number { get; set; }
        public int TeamId { get; set; }
        [ForeignKey("TeamId")]
        public Team Team { get; set; }
        [NotMapped]
        public int NumberOfGoals { get; set; }
    }
}
