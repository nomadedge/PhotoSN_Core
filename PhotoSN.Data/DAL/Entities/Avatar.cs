using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoSN.Data.DAL.Entities
{
    public class Avatar
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ImageId { get; set; }
        [ForeignKey("ImageId")]
        public Image Image { get; set; }
    }
}
