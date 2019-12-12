﻿using System;
using System.ComponentModel.DataAnnotations;

namespace PhotoSN.Data.DAL
{
    public class Image
    {
        public int ImageId { get; set; }
        [Required]
        [StringLength(50)]
        public string MimeType { get; set; }
        public DateTime Created { get; set; }
        [Required]
        public User User { get; set; }
        public int Guid { get; set; }
    }
}
