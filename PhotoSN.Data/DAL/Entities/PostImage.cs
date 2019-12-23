using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoSN.Data.DAL.Entities
{
    public class PostImage
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ImageId { get; set; }
        [ForeignKey("ImageId")]
        public Image Image { get; set; }
        [Required]
        public Post Post { get; set; }
        public byte OrderNumber { get; set; }
    }
}
