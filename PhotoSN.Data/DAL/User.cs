using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhotoSN.Data.DAL
{
    public class User
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

        public int UserId { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [StringLength(150)]
        public string PasswordHash { get; set; }
        [Required]
        [StringLength(150)]
        public string Salt { get; set; }
        [Required]
        [StringLength(50)]
        public string Username { get; set; }
        public DateTime RegDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }
        [StringLength(300)]
        public string Bio { get; set; }
        public bool IsPrivate { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<InPostMention> InPostMentions { get; set; }
        public ICollection<InCommentMention> InCommentMentions { get; set; }
        public ICollection<PostLike> PostLikes { get; set; }
        public ICollection<CommentLike> CommentLikes { get; set; }
        public ICollection<BlacklistRow> Blacklist { get; set; }
        public ICollection<BlacklistRow> BlockedBy { get; set; }
        public ICollection<Subscription> Following { get; set; }
        public ICollection<Subscription> Followers { get; set; }
        public ICollection<Image> Images { get; set; }
        public ICollection<Avatar> Avatars { get; set; }
    }
}
