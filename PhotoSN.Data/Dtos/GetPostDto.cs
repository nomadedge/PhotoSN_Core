using System.Collections.Generic;

namespace PhotoSN.Data.Dtos
{
    public class GetPostDto
    {
        public int PostId { get; set; }
        public GetSimpleUserDto User { get; set; }
        public string Created { get; set; }
        public string Description { get; set; }
        public List<int> ImageIds { get; set; }
        public int CommentsAmount { get; set; }
        public List<int> Likes { get; set; }
        public int LikesAmount { get; set; }
        public bool IsAuthorized { get; set; }
        public bool IsLiked { get; set; }
        public bool IsNotLiked { get; set; }
        public List<string> Hashtags { get; set; }
        public bool HasHashtags { get; set; }
    }
}
