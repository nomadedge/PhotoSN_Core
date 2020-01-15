using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhotoSN.Data.Entities
{
    public class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
            InPostMentions = new HashSet<InPostMention>();
            PostLikes = new HashSet<PostLike>();
            PostImages = new HashSet<PostImage>();
            InPostHashtags = new HashSet<InPostHashtag>();
        }

        public int PostId { get; set; }
        [Required]
        public User User { get; set; }
        [StringLength(300)]
        public string Description { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<InPostMention> InPostMentions { get; set; }
        public virtual ICollection<PostLike> PostLikes { get; set; }
        public virtual ICollection<PostImage> PostImages { get; set; }
        public virtual ICollection<InPostHashtag> InPostHashtags { get; set; }
    }
}
