using System.Collections.Generic;

namespace PhotoSN.Data.Dtos
{
    public class GetCommentDto
    {
        public int CommentId { get; set; }
        public string Created { get; set; }
        public GetSimpleUserDto User { get; set; }
        public string Text { get; set; }
        public List<int> Likes { get; set; }
        public int LikesAmount { get; set; }
        public bool IsAuthorized { get; set; }
        public bool IsLiked { get; set; }
        public bool IsNotLiked { get; set; }
    }
}
