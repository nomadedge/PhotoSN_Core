using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoSN.Data.DAL
{
    public class BlacklistRow
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FirstUserId { get; set; }
        public User FirstUser { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SecondUserId { get; set; }
        public User SecondUser { get; set; }
    }
}
