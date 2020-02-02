using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhotoSN.Data.Entities
{
    public class Comment
    {
        public Comment()
        {
            CommentLikes = new HashSet<CommentLike>();
        }

        public int CommentId { get; set; }
        [Required]
        public Post Post { get; set; }
        [Required]
        public User User { get; set; }
        [Required]
        [StringLength(300)]
        public string Text { get; set; }
        public DateTime Created { get; set; }
        public virtual ICollection<CommentLike> CommentLikes { get; set; }
    }
}
