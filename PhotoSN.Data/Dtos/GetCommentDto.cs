using System;
using System.Collections.Generic;

namespace PhotoSN.Data.Dtos
{
    public class GetCommentDto
    {
        public int CommentId { get; set; }
        public DateTime Created { get; set; }
        public GetSimpleUserDto User { get; set; }
        public string Text { get; set; }
        public List<GetSimpleUserDto> Likes { get; set; }
    }
}
