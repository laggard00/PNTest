using Microsoft.EntityFrameworkCore;
using PNTest.DAL.Context;

namespace PNTest.Middleware
{
    public class BasicApiKeyMiddleware
    {
        private readonly RequestDelegate _next;

        private const string API_KEY_HEADER_NAME = "X-API-Key";

        public BasicApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, DataContext dbContext)
        {

            // Trying get API key from header
            if (!context.Request.Headers.TryGetValue(API_KEY_HEADER_NAME, out var apiKeyHeaderValues))
            {

                context.Response.StatusCode = 401;
                await context.Response.WriteAsJsonAsync(new { message = "API key is required" });
                return;
            }

            string apiKey = apiKeyHeaderValues.FirstOrDefault()!;
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsJsonAsync(new { message = "Valid API key is required" });
                return;
            }
            //getting user by api key
            var user = await dbContext.Users
                .FirstOrDefaultAsync(u => u.ApiKey == apiKey);

            if (user == null)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsJsonAsync(new { message = "Invalid API key" });
                return;
            }
            context.Items["UserId"] = user.Id;

            await _next(context);
        }
    }
}
