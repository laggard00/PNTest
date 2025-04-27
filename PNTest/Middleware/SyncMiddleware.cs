using PNTest.DAL.Context;
using System.Collections.Concurrent;

namespace PNTest.Middleware
{
    public class SyncMiddlware
    {
        private readonly RequestDelegate _next;
        private static readonly ConcurrentDictionary<int, SemaphoreSlim> UserSemaphore = new ConcurrentDictionary<int, SemaphoreSlim>();

        public SyncMiddlware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, DataContext dbContext)
        {
            //previous basickeyapi middleware should insert userid into context
            if (context.Request.Path.ToString().Contains("/incoming-requests"))
            {
                await _next(context);
                return;

            }
            if (!context.Items.ContainsKey("UserId"))
            {
                context.Response.StatusCode = 400; // Bad Request
                await context.Response.WriteAsJsonAsync(new { message = "User ID not found" });
                return;
            }

            var userId = (int)context.Items["UserId"];

            //create or get semaphore for specific user
            var semaphore = UserSemaphore.GetOrAdd(userId, new SemaphoreSlim(1, 1));

            await semaphore.WaitAsync();

            try
            {
                await _next(context);
            }
            finally
            {
                semaphore.Release();
            }
        }
    }
}
