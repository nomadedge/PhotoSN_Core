using AutoMapper;
using PhotoSN.Data.Dtos;
using PhotoSN.Data.Entities;

namespace PhotoSN.Data.AutoMapper
{
    public class EntityDtoProfile : Profile
    {
        public EntityDtoProfile()
        {
            CreateMap<Image, GetImageDto>()
                .ForMember(gid => gid.UserId, opt => opt.MapFrom(i => i.User.Id))
                .ForMember(gid => gid.Nickname, opt => opt.MapFrom(i => i.User.Nickname));

            CreateMap<CreateImageDto, Image>();

            CreateMap<AvatarDto, Avatar>();

            CreateMap<Avatar, AvatarsHistoryDto>();

            CreateMap<User, GetPostUserDto>()
                .ForMember(gpud => gpud.UserId, opt => opt.MapFrom(u => u.Id));

            CreateMap<Post, GetPostDto>()
                .ForMember(gpd => gpd.CommentsAmount, opt => opt.MapFrom(p => p.Comments.Count))
                .ForMember(gpd => gpd.LikesAmount, opt => opt.MapFrom(p => p.PostLikes.Count));
            //.ForMember(gpd => gpd.ImageIds, opt => opt.ConvertUsing(Post p =>
            //{
            //    var imageIds = new List<int>();
            //    foreach (var postImage in p.PostImages)
            //        imageIds.Add(postImage.ImageId);
            //    return imageIds;
            //}));
        }
    }
}
