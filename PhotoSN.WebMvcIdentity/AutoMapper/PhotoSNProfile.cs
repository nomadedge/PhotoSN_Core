using AutoMapper;
using PhotoSN.Data.Entities;
using PhotoSN.Data.Dtos;
using PhotoSN.WebMvcIdentity.IdentityViewModels;

namespace PhotoSN.WebMvcIdentity.AutoMapper
{
    public class PhotoSNProfile : Profile
    {
        public PhotoSNProfile()
        {
            CreateMap<RegisterViewModel, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(rvm => rvm.Email));

            CreateMap<User, ManageIndexViewModel>();

            CreateMap<Image, GetImageDto>()
                .ForMember(gid => gid.UserId, opt => opt.MapFrom(i => i.User.Id))
                .ForMember(gid => gid.Nickname, opt => opt.MapFrom(i => i.User.Nickname));

            CreateMap<CreateImageDto, Image>();

            CreateMap<AvatarDto, Avatar>();

            CreateMap<Avatar, AvatarsHistoryDto>();

            CreateMap<AvatarsHistoryDto, ManageAvatarsHistoryViewModel>();
        }
    }
}
