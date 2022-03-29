using System.Collections.Generic;

namespace AplicatieFotbal.Models.ViewModels
{
    public class DataPerLeague
    {
        public Tops Tops { get; set; }
        public Totals Totals { get; set; }
        public List<Match> Matches { get; set; }
    }
}
