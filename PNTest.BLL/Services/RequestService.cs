using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PNTest.BLL.Models.RequestModel;
using PNTest.BLL.Services.Interfaces;
using PNTest.DAL.Context;
using PNTest.DAL.Entities;

namespace PNTest.BLL.Services
{
    public class RequestService : IRequestService
    {
        private readonly DataContext _context;
        public RequestService(DataContext context)
        {
            _context = context;
        }
        public async Task<Request> SaveLocationRequest(LocationRequest locationRequest, int userId)
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

        public async Task<IEnumerable<Request>> GetRequests(string? type, string? search)
        {
            var query = _context.Requests.AsQueryable();

            if (!string.IsNullOrWhiteSpace(type))
            {
                query = query.Where(r => r.Type != null && r.Type.Contains(type));
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(r => r.Longitude.ToString().Contains(search) || r.Latitude.ToString().Contains(search));
            }

            return await query.ToListAsync();
        }
    }
}
