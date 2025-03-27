using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;

var connection = new HubConnectionBuilder()
    .WithUrl("https://localhost:7018/notificationsHub", options =>
    options.AccessTokenProvider = () => Task.FromResult<string?>("Tokenni kirgizishingiz kerak."))
    .Build();

connection.On<string>("ReceiveNotification", message =>
{
    Console.WriteLine($"New notification: {message}");
});


await connection.StartAsync();

Console.WriteLine("Connected to SignalR hub.");

Console.ReadLine();



