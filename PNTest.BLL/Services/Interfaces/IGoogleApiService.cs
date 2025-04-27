using PNTest.BLL.Models.RequestModel;
using PNTest.BLL.Models.ResponseModel;

namespace PNTest.BLL.Services.Interfaces
{
    public interface IGoogleApiService
    {
        Task<IEnumerable<LocationResponse>> GetNearbyLocations(LocationRequest locationRequest);
    }
}
