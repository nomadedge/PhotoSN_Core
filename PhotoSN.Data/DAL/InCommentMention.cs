using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoSN.Data.DAL
{
    public class InCommentMention
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CommentId { get; set; }
        [ForeignKey("CommentId")]
        public Comment Comment { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
