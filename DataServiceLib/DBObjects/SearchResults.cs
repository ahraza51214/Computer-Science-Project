using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataServiceLib.DBObjects
{
    public class SearchResults
    {
        public string PrimaryTitle { get; set; }
        public string Tconst { get; set; }
    }
}