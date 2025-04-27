using Microsoft.EntityFrameworkCore;
using PNTest.BLL.Models;
using PNTest.BLL.Services.Interfaces;
using PNTest.DAL.Context;
using PNTest.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNTest.BLL.Services
{
    public class ResponsePersistService : IResponsePersistService
    {
        private readonly DataContext _context;
        public ResponsePersistService(DataContext context)
        {
            _context = context;

        }

        public async Task PersistLocationResponse(int requestId, IEnumerable<LocationResponse> locations)
        {
            var response = new Response
            {
                RequestId = requestId
            };

            _context.Responses.Add(response);

            await _context.SaveChangesAsync();

            foreach (var locationResponse in locations)
            {
                var existingLocation = await _context.Locations
                    .FirstOrDefaultAsync(l => l.PlaceId == locationResponse.PlaceId);

                Location location;

                if (existingLocation == null)
                {
                    location = new Location
                    {
                        PlaceId = locationResponse.PlaceId,
                        LocationName = locationResponse.LocationName,
                        LocationType = locationResponse.LocationType,
                        Address = locationResponse.Address,
                        Longitude = locationResponse.Longitude,
                        Latitude = locationResponse.Latitude
                    };

                    _context.Locations.Add(location);
                    await _context.SaveChangesAsync(); 
                }
                else
                {
                    location = existingLocation;
                }

                var responseLocation = new ResponseLocation
                {
                    ResponseId = response.Id,
                    LocationId = location.PlaceId
                };

                _context.ResponseLocations.Add(responseLocation);
            }
            await _context.SaveChangesAsync();
        }
    }
}
