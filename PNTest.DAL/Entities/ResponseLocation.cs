namespace PNTest.DAL.Entities
{
    public class ResponseLocation
    {
        public int ResponseId { get; set; }
        public string LocationId { get; set; }
        public Response? Response { get; set; }
        public Location? Location { get; set; }
    }
}
