namespace PNTest.BLL.Models.ResponseModel
{
    public class LocationResponse
    {
        public string PlaceId { get; set; } = string.Empty;
        public string LocationName { get; set; } = string.Empty;
        public string LocationType { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public float Longitude { get; set; }
        public float Latitude { get; set; }
    }
}
