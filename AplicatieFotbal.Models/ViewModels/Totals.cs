namespace AplicatieFotbal.Models.ViewModels
{
    public class Totals
    {
       public int TotalGoals { get; set; }
       public int TotalYCards { get; set; }
        public int TotalRCards { get; set; }
        public int TotalCorners { get; set; }
       public int TotalFouls { get; set; }
       public int TotalOffsides { get; set; }
       public int TotalGKSaves { get; set; }
        public int LeagueId { get; set; }
       
        
        public League League { get; set; }
    }
}
