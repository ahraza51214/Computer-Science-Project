using AutoMapper;
using DataServiceLib.DBObjects;

namespace ProjectPortfolio2_Group11.Model.MappingProfile
{
    public class TitleBasicsProfile : Profile
    {
        public TitleBasicsProfile()
        {
            CreateMap<TitleBasics, SearchResultsDTO>();
        }
    }
}