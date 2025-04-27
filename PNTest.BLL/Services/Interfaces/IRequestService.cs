using PNTest.BLL.Models.RequestModel;
using PNTest.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNTest.BLL.Services.Interfaces
{
    public interface IRequestService
    {
        Task<Request> SaveLocationRequest(LocationRequest locationRequest, int userId);
        Task<IEnumerable<Request>> GetRequests(string? type, string? search);
    }
}
