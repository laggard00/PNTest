namespace PNTest.DAL.Entities
{
    public class UserFavoriteLocation
    {
        public int UserId { get; set; }
        public User? User { get; set; }
        public string LocationId { get; set; }
        public Location? Location { get; set; }
    }
}
