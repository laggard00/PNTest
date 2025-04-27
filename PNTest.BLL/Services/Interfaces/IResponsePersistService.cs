using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNTest.BLL.Services.Interfaces
{
    public interface IResponsePersistService
    {
        Task PersistLocationResponse(int requestId, string responseJson);
    }
}
