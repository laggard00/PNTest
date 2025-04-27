using PNTest.BLL.Models;

namespace PNTest.BLL.Services.Interfaces
{
    public interface IResponsePersistService
    {
        Task PersistLocationResponse(int requestId, IEnumerable<LocationResponse> locations);
    }
}
