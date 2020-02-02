using System.Collections.Generic;

namespace PhotoSN.WebMvcIdentity.Models
{
    public class PostModel
    {
        public string Description { get; set; }
        public List<int> ImageIds { get; set; }
    }
}
