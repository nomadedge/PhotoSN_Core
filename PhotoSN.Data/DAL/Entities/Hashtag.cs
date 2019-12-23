using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhotoSN.Data.DAL.Entities
{
    public class Hashtag
    {
        public Hashtag()
        {
            InPostHashtags = new HashSet<InPostHashtag>();
        }

        public int HashtagId { get; set; }
        [Required]
        [StringLength(20)]
        public string Text { get; set; }
        public virtual ICollection<InPostHashtag> InPostHashtags { get; set; }
    }
}
