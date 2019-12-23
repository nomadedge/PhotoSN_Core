using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoSN.Data.DAL.Entities
{
    public class Subscription
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FirstUserId { get; set; }
        public User FirstUser { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SecondUserId { get; set; }
        public User SecondUser { get; set; }

        public bool IsApproved { get; set; }
    }
}
