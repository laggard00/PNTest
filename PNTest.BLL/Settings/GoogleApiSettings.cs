using System.ComponentModel.DataAnnotations;

namespace PNTest.BLL.Settings
{
    public class GoogleApiSettings
    {
        [Required]
        public required string ApiKey { get; init; }
        [Required]
        public required string BaseUrl { get; init; }
    }
}
