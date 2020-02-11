using AutoMapper;
using PhotoSN.Data.Dtos;
using PhotoSN.Data.Entities;
using PhotoSN.WebMvcIdentity.IdentityViewModels;
using PhotoSN.WebMvcIdentity.Models;

namespace PhotoSN.WebMvcIdentity.AutoMapper
{
    public class DtoModelProfile : Profile
    {
        public DtoModelProfile()
        {
            CreateMap<RegisterViewModel, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(rvm => rvm.Email));

            CreateMap<User, ManageIndexViewModel>();

            CreateMap<AvatarsHistoryDto, ManageAvatarsHistoryViewModel>();

            CreateMap<PostModel, CreatePostDto>();

            CreateMap<GetUserDto, UserModel>();

            CreateMap<CommentModel, CreateCommentDto>();
        }
    }
}
