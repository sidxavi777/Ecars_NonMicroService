using AutoMapper;
using Ecars.Model;
using Ecars.Model.Dto_s;

namespace Ecars.Common
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<BaseDetail, PostBaseDetail_DTO>().ReverseMap();
            CreateMap<BaseDetail, EditBaseDetail_DTO>().ReverseMap();
        }
    }
}
