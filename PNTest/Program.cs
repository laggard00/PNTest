using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PNTest.BLL.Services;
using PNTest.BLL.Services.Interfaces;
using PNTest.BLL.Settings;
using PNTest.DAL.Context;
using PNTest.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DataContext>(options =>
            options.UseInMemoryDatabase("LocationDatabase"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "PNTest API",
        Version = "v1",
        Description = "API for Google Places testing"
    });
});
builder.Services.AddOptions<GoogleApiSettings>()
    .Bind(builder.Configuration.GetSection(nameof(GoogleApiSettings)))
    .ValidateDataAnnotations()
    .ValidateOnStart();
builder.Services.AddHttpClient<IGoogleApiService, GoogleApiService>();
builder.Services.AddScoped<IResponsePersistService, ResponsePersistService>();
builder.Services.AddScoped<IRequestPersistService, RequestPersistService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "PNTest API v1");
});

app.UseHttpsRedirection();
app.UseMiddleware<BasicApiKeyMiddleware>();
app.UseMiddleware<SyncMiddlware>();
app.UseAuthorization();


app.MapControllers();

app.Run();
