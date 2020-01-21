using System;

namespace PhotoSN.Model.Dtos
{
    public class CreateImageDto
    {
        public Guid Guid { get; set; }
        public string MimeType { get; set; }
        public int UserId { get; set; }
    }
}
