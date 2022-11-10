using System;
using System.ComponentModel.DataAnnotations;

namespace DataServiceLib.DBObjects
{
    public class SearchHistory
    {
        public int UserId { get; set; }
        public string StoredInput { get; set; }
        public DateTime DateTime { get; set; }
    }
}