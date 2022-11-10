using System;
using System.Collections.Generic;
using DataServiceLib.DBObjects;

namespace DataServiceLib.IDataService
{
    public interface IRatingDataService
    {
        IList<UserTitleRate> GetRating(int userId);
    
        IList<TitleRateDto> CreateRating(UserTitleRate userTitleRate);
         
    }
}