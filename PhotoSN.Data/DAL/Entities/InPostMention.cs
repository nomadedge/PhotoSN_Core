using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoSN.Data.DAL.Entities
{
    public class InPostMention
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PostId { get; set; }
        [ForeignKey("PostId")]
        public Post Post { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
