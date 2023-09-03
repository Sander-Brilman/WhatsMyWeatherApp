 using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WhatsMyWeather;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


string apiKey = "w1VGICvBWnNFDzS7";
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri($"https://api.pirateweather/forecast/{apiKey}/") });

await builder.Build().RunAsync();
