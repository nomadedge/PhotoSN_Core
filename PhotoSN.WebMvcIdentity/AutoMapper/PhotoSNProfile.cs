using AutoMapper;
using PhotoSN.Data.Entities;
using PhotoSN.Model.IdentityInputModels;

namespace PhotoSN.WebMvcIdentity.AutoMapper
{
    public class PhotoSNProfile : Profile
    {
        public PhotoSNProfile()
        {
            CreateMap<RegisterInputModel, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(rim => rim.Email));

            CreateMap<User, ManageIndexInputModel>();
        }
    }
}
