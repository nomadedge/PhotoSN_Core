using System.Collections.Generic;

namespace PhotoSN.Data.Dtos
{
    public class CreatePostDto
    {
        public int UserId { get; set; }
        public string Description { get; set; }
        public List<int> ImageIds { get; set; }
    }
}
