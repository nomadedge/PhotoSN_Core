using System;

namespace PhotoSN.Data.Dtos
{
    public class GetImageDto
    {
        public int ImageId { get; set; }
        public string MimeType { get; set; }
        public int UserId { get; set; }
        public string Nickname { get; set; }
        public Guid Guid { get; set; }
    }
}
