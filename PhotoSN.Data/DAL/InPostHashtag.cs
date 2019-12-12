using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoSN.Data.DAL
{
    public class InPostHashtag
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PostId { get; set; }
        [ForeignKey("PostId")]
        public Post Post { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int HashtagId { get; set; }
        [ForeignKey("HashtagId")]
        public Hashtag Hashtag { get; set; }
    }
}
