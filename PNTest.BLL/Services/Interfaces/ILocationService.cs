namespace PNTest.BLL.Services.Interfaces
{
    public interface ILocationService
    {
        Task AddFavoriteLocation(string placeId, int userId);
    }
}
