using AutoMapper;
using WorkPlatformBn.Model;
using WorkPlatformBn.Model.Request;

namespace WorkPlatformBn.Utility
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<UserRequestModel,User>().ReverseMap();

            CreateMap<CategoryRequestModel,Category>().ReverseMap();
        }
    }
}
