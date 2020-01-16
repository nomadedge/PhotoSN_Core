using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhotoSN.Data.Entities
{
    public class User : IdentityUser<int>
    {
        public User()
        {
            Posts = new HashSet<Post>();
            Comments = new HashSet<Comment>();
            InPostMentions = new HashSet<InPostMention>();
            InCommentMentions = new HashSet<InCommentMention>();
            PostLikes = new HashSet<PostLike>();
            CommentLikes = new HashSet<CommentLike>();
            Blacklist = new HashSet<BlacklistRow>();
            BlockedBy = new HashSet<BlacklistRow>();
            Following = new HashSet<Subscription>();
            Followers = new HashSet<Subscription>();
            Images = new HashSet<Image>();
            Avatars = new HashSet<Avatar>();
        }

        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }
        [StringLength(300)]
        public string Bio { get; set; }
        public bool IsPrivate { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<InPostMention> InPostMentions { get; set; }
        public virtual ICollection<InCommentMention> InCommentMentions { get; set; }
        public virtual ICollection<PostLike> PostLikes { get; set; }
        public virtual ICollection<CommentLike> CommentLikes { get; set; }
        public virtual ICollection<BlacklistRow> Blacklist { get; set; }
        public virtual ICollection<BlacklistRow> BlockedBy { get; set; }
        public virtual ICollection<Subscription> Following { get; set; }
        public virtual ICollection<Subscription> Followers { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<Avatar> Avatars { get; set; }
    }
}
