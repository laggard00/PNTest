
using Microsoft.AspNetCore.SignalR.Client;

Console.WriteLine("Hello, World!");
var connection = new HubConnectionBuilder()
    .WithUrl("https://localhost:7022/incoming-requests")
    .Build();

connection.On<string>("ReceiveNewRequest", message =>
{
    Console.WriteLine($"New mesage : {message}");
});

try
{
    await connection.StartAsync();
    Console.WriteLine("Connection successful!");

    await Task.Delay(-1);
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}