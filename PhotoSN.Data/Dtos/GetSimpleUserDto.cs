namespace PhotoSN.Data.Dtos
{
    public class GetSimpleUserDto
    {
        public int UserId { get; set; }
        public string Nickname { get; set; }
        public int? AvatarImageId { get; set; }
    }
}
