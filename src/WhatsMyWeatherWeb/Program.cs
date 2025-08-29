using WhatsMyWeatherWeb.Components;
using WhatsMyWeatherWeb.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

string apiKey = File.ReadAllText(builder.Configuration.GetValue<string>("API_KEY_FILE") ?? throw new Exception("Environment variable API_KEY_FILE was not found"));

builder.Services.AddSingleton(new WeatherServiceOptions(apiKey));
builder.Services.AddScoped<WeatherService>();
builder.Services.AddHttpClient<WeatherService>();

builder.Services.AddTransient<LocationService>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
