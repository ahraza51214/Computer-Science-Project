using System.ComponentModel.DataAnnotations;

namespace DataServiceLib.DBObjects
{
    public class TitleEpisode
    {
        public string TConst { get; set; }
        public string ParentTConst { get; set; }
        public int SeasonNumber { get; set; }
        public int EpisodeNumber { get; set; }
    }
}