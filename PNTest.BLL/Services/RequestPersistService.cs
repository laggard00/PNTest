using Microsoft.AspNetCore.Http;
using PNTest.BLL.Models.RequestModel;
using PNTest.BLL.Services.Interfaces;
using PNTest.DAL.Context;
using PNTest.DAL.Entities;

namespace PNTest.BLL.Services
{
    public class RequestPersistService : IRequestPersistService
    {
        private readonly DataContext _context;
        public RequestPersistService(DataContext context)
        {
            _context = context;
        }
        public async Task<Request> PersistLocationRequest(LocationRequest locationRequest, int userId)
        {
            var request = new Request
            {
                Longitude = locationRequest.Longitude,
                Latitude = locationRequest.Latitude,
                Type = locationRequest.Type,
                UserId = userId
            };
            await _context.Requests.AddAsync(request);
            await _context.SaveChangesAsync();
            return request;
        }
    }
}
