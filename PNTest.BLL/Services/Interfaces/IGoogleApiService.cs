using PNTest.BLL.Models;
using PNTest.BLL.Models.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNTest.BLL.Services.Interfaces
{
    public interface IGoogleApiService
    {
        Task<IEnumerable<LocationResponse>> GetNearbyLocations(LocationRequest locationRequest);
    }
}
