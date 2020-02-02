using AutoMapper;
using PhotoSN.Data.Dtos;
using PhotoSN.Data.Entities;
using PhotoSN.WebMvcIdentity.IdentityViewModels;
using PhotoSN.WebMvcIdentity.Models;

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

            CreateMap<PostModel, CreatePostDto>();
        }
    }
}
