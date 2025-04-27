using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace PNTest.Controllers
{
    public class BaseController : ControllerBase
    {
        private int? _userId;
        protected int UserId
        {
            get
            {
                if (_userId.HasValue)
                    return _userId.Value;

                if (HttpContext.Items.TryGetValue("UserId", out var userIdObj) && userIdObj is int id)
                {
                    _userId = id;
                    return id;
                }

                throw new Exception("UserId should be here because of middleware");
            }
        }
    }
}
