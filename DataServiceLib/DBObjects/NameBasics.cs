using System.ComponentModel.DataAnnotations;

namespace DataServiceLib.DBObjects
{
    public class NameBasics
    {
        public string NConst { get; set; }
        public string PrimaryName { get; set; }
        public int BirthYear { get; set; }
        public int DeathYear { get; set; }
    }
}