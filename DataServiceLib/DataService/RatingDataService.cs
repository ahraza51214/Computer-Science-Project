using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using DataServiceLib.DBObjects;
using DataServiceLib.IDataService;
using Microsoft.EntityFrameworkCore;

namespace DataServiceLib.DataService
{
    public class RatingDataService : IRatingDataService
    {
        public RatingDataService(string connStr)
        {
        
            _connectionString = connStr;
        }
        private readonly string _connectionString;
        public IList<UserTitleRate> GetRating( int userId)
        {
            Raw11Context _db = new Raw11Context(_connectionString);
            return _db.UserTitleRates.Where(x => x.UserId == userId).ToList();
        }

        public IList<TitleRateDto> CreateRating(UserTitleRate userTitleRate)
        {
            Raw11Context _db = new Raw11Context(_connectionString);

            var queery = _db.RatingTable.FromSqlInterpolated($"select * from rate({userTitleRate.UserId},{userTitleRate.TConst},{userTitleRate.TitleIndividRating})");

            _db.SaveChanges();

            return queery
                .ToList();
        }
    
    }
}
