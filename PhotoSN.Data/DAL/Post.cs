﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhotoSN.Data.DAL
{
    public class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
            InPostMentions = new HashSet<InPostMention>();
            PostLikes = new HashSet<PostLike>();
            PostImages = new HashSet<PostImage>();
        }

        public int PostId { get; set; }
        [Required]
        public User User { get; set; }
        [StringLength(300)]
        public string Description { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<InPostMention> InPostMentions { get; set; }
        public ICollection<PostLike> PostLikes { get; set; }
        public ICollection<PostImage> PostImages { get; set; }
    }
}
