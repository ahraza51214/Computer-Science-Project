using DataServiceLib.DataService;
using DataServiceLib.IDataService;
using Microsoft.Extensions.Configuration;

namespace DataServiceLib
{
    public class DataServiceFacade
    {
        public readonly IBookmarkingDataService BookmarkingDs;
        public readonly IRatingDataService RatingDs;
        public readonly IUsersDataService UsersDs;
        public readonly ISearchDataService SearchDs;
        
        public DataServiceFacade(IConfiguration configuration)
        {
            var connStr = configuration.GetSection("connectionString").Value;
            
            BookmarkingDs = new BookmarkingDataService(connStr);
            RatingDs = new RatingDataService(connStr);
            UsersDs = new UsersDataService(connStr);
            SearchDs = new SearchDataService(connStr);
        }
    }
}