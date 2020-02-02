using System;
using System.Collections.Generic;

namespace PhotoSN.Data.Dtos
{
    public class GetPostDto
    {
        public UserDto User { get; set; }
        public DateTime Created { get; set; }
        public string Description { get; set; }
        public List<int> ImageIds { get; set; }
        public int CommentsAmount { get; set; }
        public int LikesAmount { get; set; }
    }
}
