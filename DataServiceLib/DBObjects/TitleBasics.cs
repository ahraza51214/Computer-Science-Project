using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataServiceLib.DBObjects
{
    public class TitleBasics
    {

       
        public string PrimaryTitle { get; set; }
        public string TConst { get; set; }
        public string TitleType { get; set; }
        public string OriginalTitle { get; set; }
        public bool IsAdult { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public int RunTimeMinutes { get; set; }
        public string Poster { get; set; }
        public string Awards { get; set; }
        public string Plot { get; set; }
        public decimal AvarageRating { get; set; }
        public int NumVotes { get; set; } 
    }
}