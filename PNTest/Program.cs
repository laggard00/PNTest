using Microsoft.OpenApi.Models;
using PNTest.BLL.Services.Interfaces;
using PNTest.BLL.Services;
using PNTest.BLL.Settings;

var builder = WebApplication.CreateBuilder(args);


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

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "PNTest API v1");
});

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers(); 

app.Run(); 
