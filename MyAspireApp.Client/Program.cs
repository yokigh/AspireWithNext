using System.Net.Http;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyAspireApp.Client;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

// Load appsettings.json manually from wwwroot
using var http = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
var response = await http.GetAsync("appsettings.json");
using var stream = await response.Content.ReadAsStreamAsync();
var config = new ConfigurationBuilder()
    .AddJsonStream(stream)
    .Build();

// Now we can access settings!
var apiBaseAddress = config["MyAspireAppApi:BaseAddress"];

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(apiBaseAddress!)
});

await builder.Build().RunAsync();
