using BlazorServerUI.Data.GetWeatherHelperServiceModels;
using BlazorServerUI.Data.RawWeatherApiModels;
using BlazorServerUI.Enums;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.VisualBasic;
using System.Web;

namespace BlazorServerUI.Services;

public class GetWeatherHelper
{
    private readonly HttpClient _httpClient;

    private readonly IConfiguration _configurationManager;


    public GetWeatherHelper(HttpClient httpClient, IConfiguration configurationManager)
    {
        _httpClient = httpClient;
        _configurationManager = configurationManager;
    }




    public async Task<WeatherResult?> GetWeatherStatusForLocationAsync(string location, string lang)
    {
        string apiKey = _configurationManager["apiKey"] ?? throw new Exception("api key not found!");

        string uri = $"forecast.json?q={HttpUtility.UrlEncode(location)}&days=1&key={apiKey}lang={lang}";

        WeahterApiRootobject? apiResponse;
            
        var content = await _httpClient.GetAsync(uri);

        if (content.IsSuccessStatusCode is false)
        {
            return null;
        }

        apiResponse = await content.Content.ReadFromJsonAsync<WeahterApiRootobject?>();
            
        if (apiResponse is null)
        {
            throw new Exception("APi response is null");
        }




        // there is only 1 :skull:
        Forecastday forecastday = apiResponse.forecast.forecastday.First();



        DateTime UTCdateTime = DateTimeOffset.FromUnixTimeSeconds(apiResponse.location.localtime_epoch).UtcDateTime;
        DateTime rawLocalTime = DateTime.Parse(apiResponse.location.localtime);

        // rawLocalTime does not have seconds included.
        // so to make sure the calulated timezone offset does not include seconds we add the same amount of seconds from the epoch time to the rawlocaltme
        rawLocalTime = rawLocalTime.AddSeconds(UTCdateTime.Second);

        TimeSpan timezoneOffset = rawLocalTime - UTCdateTime;


        DateTimeOffset localtime = new(rawLocalTime, timezoneOffset);

        var (sunriseStart, SunriseDuration, sunsetStart, SunsetDuration) = SunTimesCalculator.CalculateSunsetSunrise(apiResponse.location.lat, apiResponse.location.lon, localtime);

        TimeOfDay timeOfDay = localtime.GetTimeOfDay(
            sunriseStart, 
            sunsetStart,
            SunriseDuration,
            SunsetDuration
        );



        return new WeatherResult()
        {
            SunriseStart = sunriseStart,
            SunriseDuration = SunriseDuration, 

            SunsetStart = sunsetStart,
            SunsetDuration = SunsetDuration,

            LocalTime = localtime,
            Location = $"{apiResponse.location.name}, {apiResponse.location.region} {apiResponse.location.country}",

            CurrentWeather = new WeatherState()
            {
                WeatherGenerationOptions = GenerateWeatherOptionsFromWeatherCode(apiResponse.current.condition.code, timeOfDay),
                Status = apiResponse.current.condition.text,

                AverageTempInCelcius = apiResponse.current.temp_c,
                PrecipitationInMM = apiResponse.current.precip_mm,

                Time = localtime,
            },
            HourlyWeather = forecastday.hour
                    .Select(x => new WeatherState()
                    {
                        Time = new(DateTime.Parse(x.time), timezoneOffset),

                        WeatherGenerationOptions = GenerateWeatherOptionsFromWeatherCode(
                            x.condition.code, 
                            new DateTimeOffset(DateTime.Parse(x.time), timezoneOffset)
                                .GetTimeOfDay(
                                    sunriseStart, 
                                    sunsetStart, 
                                    SunriseDuration, 
                                    SunsetDuration)
                        ),
                        Status = x.condition.text,

                        AverageTempInCelcius = x.temp_c,
                        PrecipitationInMM = x.precip_mm,
                    })
                    .ToArray()
        };
    }

    private static BackgroundWeatherGenerationOptions GenerateWeatherOptionsFromWeatherCode(int weatherCode, TimeOfDay timeOfDay)
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

                    BackgroundCssClass = timeOfDay.ToCssClass(),
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

                    MaxCloudScale = 75,
                    MinCloudScale = 60,

                    MaxCloudOpacity = 65,
                    MinCloudOpacity = 50,

                    BackgroundCssClass = timeOfDay.ToCssClass(),
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

                    BackgroundCssClass = timeOfDay.ToCssClass(true),
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

                    BackgroundCssClass = timeOfDay.ToCssClass(true),
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

                    BackgroundCssClass = timeOfDay.ToCssClass(true),
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

                    BackgroundCssClass = timeOfDay.ToCssClass(true),
                };


            // == fog ==
            case 1135: // Fog
            case 1147: // Freezing fog
            case 1030: // Mist
                return new()
                {
                    CloudGenerationFactor = 50,
                    TimeBetweenCloudSpawnInMiliseconds = 1000,

                    MaxCloudSpeedInPixelsPerSecond = 45,
                    MinCloudSpeedInPixelsPerSecond = 30,

                    MaxCloudElevationInPixels = 20,
                    MinCloudElevationInPixels = -40,

                    MaxCloudScale = 75,
                    MinCloudScale = 55,

                    MaxCloudOpacity = 40,
                    MinCloudOpacity = 30,

                    BackgroundCssClass = timeOfDay.ToCssClass(),
                };


            // == thunder ==
            case 1087: // Thundery outbreaks possible
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

                    Thunder = true,

                    BackgroundCssClass = timeOfDay.ToCssClass(true),
                };


            // == nothin ==
            case 1000: // Sunny, Clear
            default:
                return new()
                {
                    CloudGenerationFactor = 500,
                    TimeBetweenCloudSpawnInMiliseconds = 5000,

                    MaxCloudSpeedInPixelsPerSecond = 90,
                    MinCloudSpeedInPixelsPerSecond = 65,

                    MaxCloudElevationInPixels = 350,
                    MinCloudElevationInPixels = 100,

                    MaxCloudScale = 55,
                    MinCloudScale = 35,

                    MaxCloudOpacity = 65,
                    MinCloudOpacity = 50,

                    BackgroundCssClass = timeOfDay.ToCssClass(),
                };
        }
    }
}
