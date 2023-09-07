using BlazorServerUI.Data.GetWeatherHelperServiceModels;
using BlazorServerUI.Data.RawWeatherApiModels;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Web;

namespace BlazorServerUI.Services;

public class GetWeatherHelper
{
    private readonly HttpClient _httpClient;

    public GetWeatherHelper(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }




    public async Task<(WeatherGenerationOptions currently, WeatherGenerationOptions[] hourly)> GetWeatherStatusForLocationAsync(string location)
    {
        string uri = $"forecast.json?q={HttpUtility.UrlEncode(location)}&days=1&key=411ecacff37b4ba1a2d101159232306&lang=nl";


        WeatherRootObject? apiResponse = await _httpClient.GetFromJsonAsync<WeatherRootObject>(uri);

        if (apiResponse is null)
        {
            throw new Exception("APi response is null");
        }

        DateTimeOffset time = DateTimeOffset.Parse(apiResponse.location.localtime);

        WeatherGenerationOptions currentWeather = GenerateWeatherOptionsFromWeatherCode(
            apiResponse.current.condition.code,
            time
        );

        WeatherGenerationOptions[] hourlyWeather = apiResponse.forecast.forecastday.First().hours
            .Select(x => {
                return GenerateWeatherOptionsFromWeatherCode(x.condition.code, DateTimeOffset.Parse(x.time));
            })
            .ToArray();

        return (currentWeather, hourlyWeather);
    }

    private static WeatherGenerationOptions GenerateWeatherOptionsFromWeatherCode(int weatherCode, DateTimeOffset time)
    {
        switch (weatherCode)
        {
            // == partly cloudly ==
            case 1003: // Partly cloudy
                return new()
                {
                    CloudGenerationFactor = 100,
                    TimeBetweenCloudSpawnInMiliseconds = 2000,

                    MaxCloudSpeedInPixelsPerSecond = 70,
                    MinCloudSpeedInPixelsPerSecond = 40,

                    MaxCloudElevationInPixels = 300,
                    MinCloudElevationInPixels = 160,

                    MaxCloudScale = 50,
                    MinCloudScale = 40,

                    MaxCloudOpacity = 65,
                    MinCloudOpacity = 50,

                    Background = time.GetBackgroundFromTime(),
                };


            // == cloudy ==
            case 1006: // Cloudy
            case 1009: // Overcast
                return new()
                {
                    CloudGenerationFactor = 40,
                    TimeBetweenCloudSpawnInMiliseconds = 1000,

                    MaxCloudSpeedInPixelsPerSecond = 70,
                    MinCloudSpeedInPixelsPerSecond = 40,

                    MaxCloudElevationInPixels = 300,
                    MinCloudElevationInPixels = 160,

                    MaxCloudScale = 50,
                    MinCloudScale = 40,

                    MaxCloudOpacity = 65,
                    MinCloudOpacity = 50,

                    Background = time.GetBackgroundFromTime(true),
                };


            // == rain ==
            case 1063: // Patchy rain possible
            case 1066: // Patchy snow possible
            case 1069: // Patchy sleet possible
            case 1186: // Moderate rain at times
            case 1189: // Moderate rain
            case 1192: // Heavy rain at times
            case 1195: // Heavy rain
            case 1201: // Moderate or heavy freezing rain
            case 1243: // Moderate or heavy rain shower
            case 1246: // Torrential rain shower
            case 1276: // Moderate or heavy rain with thunder
                return new()
                {
                    CloudGenerationFactor = 40,
                    TimeBetweenCloudSpawnInMiliseconds = 1000,

                    MaxCloudSpeedInPixelsPerSecond = 70,
                    MinCloudSpeedInPixelsPerSecond = 40,

                    MaxCloudElevationInPixels = 225,
                    MinCloudElevationInPixels = 175,

                    MaxCloudScale = 60,
                    MinCloudScale = 50,

                    MaxCloudOpacity = 100,
                    MinCloudOpacity = 75,

                    TypeOfPrecipitation = "rain",
                    PrecipitationSpawnIntervalInSeconds = 0.7,
                    Thunder = weatherCode == 1276, // Moderate or heavy rain with thunder

                    Background = time.GetBackgroundFromTime(true),
                };


            // == light rain ==
            case 1072: // Patchy freezing drizzle possible
            case 1183: // Light rain
            case 1198: // Light freezing rain
            case 1150: // Patchy light drizzle
            case 1153: // Light drizzle
            case 1168: // Freezing drizzle
            case 1171: // Heavy freezing drizzle
            case 1240: // Light rain shower
            case 1180: // Patchy light rain
            case 1273: // Patchy light rain with thunder
                return new()
                {
                    CloudGenerationFactor = 40,
                    TimeBetweenCloudSpawnInMiliseconds = 1000,

                    MaxCloudSpeedInPixelsPerSecond = 70,
                    MinCloudSpeedInPixelsPerSecond = 40,

                    MaxCloudElevationInPixels = 225,
                    MinCloudElevationInPixels = 175,

                    MaxCloudScale = 60,
                    MinCloudScale = 50,

                    MaxCloudOpacity = 100,
                    MinCloudOpacity = 75,

                    TypeOfPrecipitation = "rain",
                    PrecipitationSpawnIntervalInSeconds = 1.4,
                    Thunder = weatherCode == 1273, // Patchy light rain with thunder

                    Background = time.GetBackgroundFromTime(),
                };


            // == snow ==
            case 1114: // Blowing snow
            case 1117: // Blizzard
            case 1207: // Moderate or heavy sleet
            case 1210: // Patchy light snow
            case 1216: // Patchy moderate snow
            case 1219: // Moderate snow
            case 1222: // Patchy heavy snow
            case 1225: // Heavy snow
            case 1237: // Ice pellets
            case 1252: // Moderate or heavy sleet showers
            case 1258: // Moderate or heavy snow showers
            case 1261: // Light showers of ice pellets
            case 1264: // Moderate or heavy showers of ice pellets
            case 1282: // Moderate or heavy snow with thunder

                return new()
                {
                    CloudGenerationFactor = 40,
                    TimeBetweenCloudSpawnInMiliseconds = 1000,

                    MaxCloudSpeedInPixelsPerSecond = 70,
                    MinCloudSpeedInPixelsPerSecond = 50,

                    MaxCloudElevationInPixels = 225,
                    MinCloudElevationInPixels = 175,

                    MaxCloudScale = 60,
                    MinCloudScale = 50,

                    MaxCloudOpacity = 100,
                    MinCloudOpacity = 75,

                    TypeOfPrecipitation = "snow",
                    PrecipitationSpawnIntervalInSeconds = 0.7,
                    Thunder = weatherCode == 1282, // Moderate or heavy snow with thunder

                    Background = time.GetBackgroundFromTime(true),
                };


            // == light snow ==
            case 1204: // Light sleet
            case 1249: // Light sleet showers
            case 1213: // Light snow
            case 1255: // Light snow showers
            case 1279: // Patchy light snow with thunder
                return new()
                {
                    CloudGenerationFactor = 40,
                    TimeBetweenCloudSpawnInMiliseconds = 1000,

                    MaxCloudSpeedInPixelsPerSecond = 70,
                    MinCloudSpeedInPixelsPerSecond = 40,

                    MaxCloudElevationInPixels = 225,
                    MinCloudElevationInPixels = 175,

                    MaxCloudScale = 60,
                    MinCloudScale = 50,

                    MaxCloudOpacity = 100,
                    MinCloudOpacity = 75,

                    TypeOfPrecipitation = "snow",
                    PrecipitationSpawnIntervalInSeconds = 1.4,
                    Thunder = weatherCode == 1279, // Patchy light snow with thunder

                    Background = time.GetBackgroundFromTime(),
                };


            // == fog ==
            case 1135: // Fog
            case 1147: // Freezing fog
            case 1030: // Mist
                return new()
                {
                    CloudGenerationFactor = 50,
                    TimeBetweenCloudSpawnInMiliseconds = 3000,

                    MaxCloudSpeedInPixelsPerSecond = 25,
                    MinCloudSpeedInPixelsPerSecond = 20,

                    MaxCloudElevationInPixels = 20,
                    MinCloudElevationInPixels = -40,

                    MaxCloudScale = 75,
                    MinCloudScale = 55,

                    MaxCloudOpacity = 40,
                    MinCloudOpacity = 30,

                    Background = time.GetBackgroundFromTime(),
                };


            // == thunder ==
            case 1087: // Thundery outbreaks possible
                return new()
                {
                    CloudGenerationFactor = 40,
                    TimeBetweenCloudSpawnInMiliseconds = 1000,

                    MaxCloudSpeedInPixelsPerSecond = 70,
                    MinCloudSpeedInPixelsPerSecond = 40,

                    MaxCloudElevationInPixels = 300,
                    MinCloudElevationInPixels = 160,

                    MaxCloudScale = 50,
                    MinCloudScale = 40,

                    MaxCloudOpacity = 99,
                    MinCloudOpacity = 90,
                    Thunder = true,

                    Background = time.GetBackgroundFromTime(true),
                };


            // == nothin ==
            case 1000: // Sunny, Clear
            default:
                return new()
                {
                    CloudGenerationFactor = 320,
                    TimeBetweenCloudSpawnInMiliseconds = 5000,

                    MaxCloudSpeedInPixelsPerSecond = 90,
                    MinCloudSpeedInPixelsPerSecond = 65,

                    MaxCloudElevationInPixels = 350,
                    MinCloudElevationInPixels = 100,

                    MaxCloudScale = 55,
                    MinCloudScale = 35,

                    MaxCloudOpacity = 65,
                    MinCloudOpacity = 50,

                    Background = time.GetBackgroundFromTime(),
                };
        }
    }


}
