﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhotoSN.Data.DAL
{
    public class Hashtag
    {
        public int HashtagId { get; set; }
        [Required]
        [StringLength(20)]
        public string Text { get; set; }
        public ICollection<InPostHashtag> InPostHashtags { get; set; }
    }
}