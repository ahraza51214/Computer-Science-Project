using System;
using System.Collections.Generic;
using DataServiceLib.DBObjects;

namespace DataServiceLib.IDataService
{
    public interface ISearchDataService
    {
        IList<SearchResults> AddToSearchHistory(int page, int pageSize, int userId, string searchInput);

        IList<SearchResults> AddToSearchHistory(int page, int pageSize, int userId, string Titles,string Plot, string Characters, string Names);
        IList<SearchHistory> GetSearchHistory( int userId);
 
        public int NumberOfElements(int userId, string searchInput);

        public int NumberOfElements(int userId, string Titles, string Plot, string Characters, string Names);
    }
}