namespace PNTest.DAL.Entities
{
    public class UserFavoriteLocation
    {
        public int UserId { get; set; }
        public User? User { get; set; }
        public int LocationId { get; set; }
        public Location? Location { get; set; }
    }
}
