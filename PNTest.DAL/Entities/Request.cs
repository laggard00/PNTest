namespace PNTest.DAL.Entities
{
    public class Request
    {
        public int Id { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public string? Type { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }

    }
}
