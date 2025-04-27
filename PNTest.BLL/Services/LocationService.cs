using Microsoft.EntityFrameworkCore;
using PNTest.BLL.Services.Interfaces;
using PNTest.DAL.Context;
using PNTest.DAL.Entities;

namespace PNTest.BLL.Services
{
    public sealed class LocationService : ILocationService
    {
        private readonly DataContext _context;
        public LocationService(DataContext context)
        {
            _context = context;
        }
        public async Task AddFavoriteLocation(string placeId, int userId)
        {
            bool alreadyExists = await _context.UserFavoriteLocations
                .AnyAsync(ufl => ufl.UserId == userId && ufl.LocationId == placeId);

            if (alreadyExists)
            {
                return; // that combination already exists retruns 
            }
            bool locationExists = await _context.Locations.AnyAsync(x=> x.PlaceId == placeId);
            if (!locationExists)
            {
                throw new Exception("Location not exist");
            }
            var favorite = new UserFavoriteLocation
            {
                UserId = userId,
                LocationId = placeId
            };

            _context.UserFavoriteLocations.Add(favorite);
            await _context.SaveChangesAsync();
        }
    }
}
