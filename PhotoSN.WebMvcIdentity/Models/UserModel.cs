using PhotoSN.Data.Dtos;
using System.Collections.Generic;

namespace PhotoSN.WebMvcIdentity.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Nickname { get; set; }
        public string Bio { get; set; }
        public int AvatarImageId { get; set; }
        public List<GetSimpleUserDto> Following { get; set; }
        public List<GetSimpleUserDto> Followers { get; set; }
        public List<int> PostIds { get; set; }
    }
}
