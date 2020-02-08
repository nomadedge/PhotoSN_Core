using System;
using System.Collections.Generic;

namespace PhotoSN.Data.Dtos
{
    public class GetFullPostDto
    {
        public int PostId { get; set; }
        public GetSimpleUserDto User { get; set; }
        public DateTime Created { get; set; }
        public string Description { get; set; }
        public List<int> ImageIds { get; set; }
        public List<GetCommentDto> Comments { get; set; }
        public List<GetSimpleUserDto> Likes { get; set; }
    }
}
