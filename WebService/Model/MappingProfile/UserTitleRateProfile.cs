using AutoMapper;
using DataServiceLib.DBObjects;

namespace ProjectPortfolio2_Group11.Model.MappingProfile
{
    public class UserTitleRateProfile : Profile
    {
        public UserTitleRateProfile()
        {
            CreateMap<UserTitleRate, UserTitleRateDto>();
            CreateMap<TitleBasics, TitleRateDto>();
            CreateMap<UserTitleRateForCreationDto, UserTitleRate>();
        }
    }
}