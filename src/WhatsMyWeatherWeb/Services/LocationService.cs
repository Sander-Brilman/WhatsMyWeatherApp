using System;
using System.Threading.Channels;
using Microsoft.JSInterop;

namespace WhatsMyWeatherWeb.Services;

public class LocationService(IJSRuntime js)
{
    private readonly IJSRuntime js = js;


    [JSInvokable]
    public async Task OnLocationNotSupported()
    {
        await errorChannel.Writer.WriteAsync("Locatie opvragen is niet ondersteund op dit apparaat :(");
    }

    [JSInvokable]
    public async Task OnLocationDenied()
    {
        await errorChannel.Writer.WriteAsync("Toestemming voor locatie is geweigerd! Zorg dat je toestemming geeft voor locatie of vul zelf een plaats in");
    }

    [JSInvokable]
    public async Task OnLocationReceived(decimal latitude, decimal longitude)
    {
        await locationChannel.Writer.WriteAsync(new LocationResult(latitude, longitude));
    }

    private Channel<LocationResult> locationChannel = Channel.CreateBounded<LocationResult>(new BoundedChannelOptions(1));
    private Channel<string> errorChannel = Channel.CreateBounded<string>(new BoundedChannelOptions(1));


    public async Task<LocationResult> GetLocationFromBrowser()
    {
        await js.InvokeVoidAsync("getLocation", DotNetObjectReference.Create(this));

        Task<LocationResult> successResult = locationChannel.Reader.ReadAsync().AsTask();
        Task<string> errorResult = errorChannel.Reader.ReadAsync().AsTask();

        Task completedTask = await Task.WhenAny(successResult, errorResult);

        if (completedTask == errorResult)
        {
            throw new Exception(await errorResult);
        }

        return await successResult;
    }
}

public record LocationResult(decimal Latitude, decimal Longitude);