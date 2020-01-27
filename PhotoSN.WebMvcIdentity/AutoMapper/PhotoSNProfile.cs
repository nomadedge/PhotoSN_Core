using AutoMapper;
using PhotoSN.Data.Entities;
using PhotoSN.Model.Dtos;
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

            CreateMap<Image, GetImageDto>()
                .ForMember(gid => gid.UserId, opt => opt.MapFrom(i => i.User.Id))
                .ForMember(gid => gid.Nickname, opt => opt.MapFrom(i => i.User.Nickname));

            CreateMap<CreateImageDto, Image>();

            CreateMap<AvatarDto, Avatar>();

            CreateMap<Avatar, AvatarsHistoryInputModel>();
        }
    }
}
