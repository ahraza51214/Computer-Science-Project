using System;
using System.ComponentModel.DataAnnotations;

namespace DataServiceLib.DBObjects
{
    public class UserTitleRate
    {
        public int UserId { get; set; }
        public int TitleIndividRating { get; set; }
        public string TConst { get; set; }
        public DateTime UserTitleRateDate { get; set; }
    }
}