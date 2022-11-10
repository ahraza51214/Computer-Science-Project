using System;
using System.Collections.Generic;
using System.Linq;
using DataServiceLib.DBObjects;
using DataServiceLib.IDataService;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace DataServiceLib.DataService
{
    public class SearchDataService : ISearchDataService
    {
        private readonly Raw11Context _db;
        
        public SearchDataService(string connStr)
        {
            _db = new Raw11Context(connStr);
        }

        public IList<SearchHistory> GetSearchHistory( int userId)
        {
            return _db.SearchHistory.Where(x => x.UserId == userId).ToList(); // Gets searchs, returns results as a list
        }
  
        public IList<SearchResults> AddToSearchHistory(int page, int pageSize, int userId, string searchInput)
        {
            var queery = _db.SearchResults.FromSqlInterpolated($"select * from string_search({userId},{searchInput})");
        
         
             return queery
                 .Skip(page * pageSize)
                 .Take(pageSize)
                 .ToList();
        }


        public IList<SearchResults> AddToSearchHistory(int page, int pageSize, int userId, string Titles, string Plot, string Characters, string Names)
        {
            var queery2 = _db.SearchResults.FromSqlInterpolated($"select * from string_search({userId},{Titles},{Plot},{Characters},{Names})");
            
 
            return queery2
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();
        }


        public int NumberOfElements(int userId, string searchInput)
        {
            return _db.SearchResults
                .FromSqlInterpolated($"select * from string_search({userId},{searchInput})").Count();
        }

        public int NumberOfElements(int userId, string Titles, string Plot, string Characters, string Names)
        {
            return _db.SearchResults.FromSqlInterpolated($"select * from string_search({userId},{Titles},{Plot},{Characters},{Names})").Count();

        }
    }
}