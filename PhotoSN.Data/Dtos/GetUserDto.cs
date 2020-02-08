using System.Collections.Generic;

namespace PhotoSN.Data.Dtos
{
    public class GetUserDto
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Nickname { get; set; }
        public string Bio { get; set; }
        public int AvatarImageId { get; set; }
        public List<GetSimpleUserDto> Following { get; set; }
        public List<GetSimpleUserDto> Followers { get; set; }
    }
}
