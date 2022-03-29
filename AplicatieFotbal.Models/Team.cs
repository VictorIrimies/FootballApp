using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AplicatieFotbal.Models
{
    public class Team
    {
        [Key]

        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }

        [NotMapped]
        public int NumberOfCorners { get; set; }
        [NotMapped]
        public int NumberOfOffsides { get; set; }
        [NotMapped]
        public int NumberOfGoals { get; set; }
        [NotMapped]
        public int NumberOfRCards { get; set; }
        [NotMapped]
        public int NumberOfYCards { get; set; }
        [NotMapped]
        public int NumberOfDraws { get; set; }
        [NotMapped]
        public int NumberOfWins { get; set; }
        [NotMapped]
        public int NumberOfLosses { get; set; }

        [NotMapped]
        public int NumberOfGkSaves { get; set; }
        [NotMapped]
        public int NumberOfFouls { get; set; }
       
    }
}
