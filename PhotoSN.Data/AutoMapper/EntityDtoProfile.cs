using AutoMapper;
using PhotoSN.Data.Dtos;
using PhotoSN.Data.Entities;
using System.Linq;

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

            CreateMap<User, GetSimpleUserDto>()
                .ForMember(gsud => gsud.UserId, opt => opt.MapFrom(u => u.Id))
                .ForMember(gsud => gsud.AvatarImageId, opt => opt.MapFrom(
                    u => u.GetCurrentAvatarImageId()));

            CreateMap<Comment, GetCommentDto>()
                .ForMember(gcd => gcd.Likes, opt => opt.MapFrom(c => c.CommentLikes.Select(cl => cl.UserId)))
                .ForMember(gpd => gpd.Created, opt => opt.MapFrom(p => p.Created.ToString("g")))
                .ForMember(gpd => gpd.LikesAmount, opt => opt.MapFrom(p => p.CommentLikes.Count));

            CreateMap<Post, GetPostDto>()
                .ForMember(gpd => gpd.CommentsAmount, opt => opt.MapFrom(p => p.Comments.Count))
                .ForMember(gpd => gpd.Likes, opt => opt.MapFrom(p => p.PostLikes.Select(pl => pl.UserId)))
                .ForMember(gpd => gpd.ImageIds, opt => opt.MapFrom(
                    p => p.PostImages
                        .OrderBy(p => p.OrderNumber)
                        .Select(p => p.ImageId)))
                .ForMember(gpd => gpd.Created, opt => opt.MapFrom(p => p.Created.ToString("g")))
                .ForMember(gpd => gpd.LikesAmount, opt => opt.MapFrom(p => p.PostLikes.Count));

            CreateMap<Post, GetFullPostDto>()
                .ForMember(gfpd => gfpd.ImageIds, opt => opt.MapFrom(
                    p => p.PostImages
                        .OrderBy(p => p.OrderNumber)
                        .Select(p => p.ImageId)))
                .ForMember(gfpd => gfpd.Likes, opt => opt.MapFrom(p => p.PostLikes.Select(pl => pl.User)))
                .ForMember(gfpd => gfpd.CommentsAmount, opt => opt.MapFrom(p => p.Comments.Count))
                .ForMember(gpd => gpd.Created, opt => opt.MapFrom(p => p.Created.ToString("g")));

            CreateMap<User, GetUserDto>()
                .ForMember(gud => gud.UserId, opt => opt.MapFrom(u => u.Id))
                .ForMember(gud => gud.AvatarImageId, opt => opt.MapFrom(
                    u => u.GetCurrentAvatarImageId()))
                .ForMember(gud => gud.Followers, opt => opt.Ignore())
                .ForMember(gud => gud.Following, opt => opt.Ignore());

            CreateMap<CreateCommentDto, Comment>();
        }
    }
}
