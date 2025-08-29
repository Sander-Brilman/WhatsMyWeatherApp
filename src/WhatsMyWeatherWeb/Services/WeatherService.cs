using WhatsMyWeatherWeb.Data.GetWeatherHelperServiceModels;
using WhatsMyWeatherWeb.Data.RawWeatherApiModels;
using WhatsMyWeatherWeb.Enums;
using System.Web;

namespace WhatsMyWeatherWeb.Services;

public class WeatherService(HttpClient httpClient, WeatherServiceOptions options)
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly WeatherServiceOptions _options = options;

    public async Task<WeatherResult?> GetWeatherStatusForLocationAsync(string location, string lang)
    {
        string uri = $"https://api.weatherapi.com/v1/forecast.json?q={HttpUtility.UrlEncode(location)}&days=1&key={_options.ApiKey}&lang={lang}";

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



        return new WeatherResult(sunriseStart, sunsetStart, localtime, SunsetDuration, SunriseDuration, $"{apiResponse.location.name}, {apiResponse.location.region} {apiResponse.location.country}", new WeatherState()
            {
                WeatherGenerationOptions = GenerateWeatherOptionsFromWeatherCode(apiResponse.current.condition.code, timeOfDay),
                Status = apiResponse.current.condition.text,

                AverageTempInCelcius = apiResponse.current.temp_c,
                PrecipitationInMM = apiResponse.current.precip_mm,

                Time = localtime,
            }, forecastday.hour
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
                    .ToArray());
    }

    // i try not to think about this code too much.
    private static BackgroundWeatherGenerationOptions GenerateWeatherOptionsFromWeatherCode(int weatherCode, TimeOfDay timeOfDay)
    {
        return weatherCode switch
        {

            // Partly cloudy
            1003 => new()
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
            },

            // Cloudy
            1006 or 1009 => new()
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
            },

            // Patchy rain possible
            1063 or 1066 or 1069 or 1186 or 1189 or 1192 or 1195 or 1201 or 1243 or 1246 or 1276 => new()
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
            },

            // Patchy freezing drizzle possible
            1072 or 1183 or 1198 or 1150 or 1153 or 1168 or 1171 or 1240 or 1180 or 1273 => new()
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
            },

            // Blowing snow
            1114 or 1117 or 1207 or 1210 or 1216 or 1219 or 1222 or 1225 or 1237 or 1252 or 1258 or 1261 or 1264 or 1282 => new()
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
            },

            // Light sleet
            1204 or 1249 or 1213 or 1255 or 1279 => new()
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
            },

            // Fog
            1135 or 1147 or 1030 => new()
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
            },

            // Thundery outbreaks possible
            1087 => new()
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
            },

            // nothin, Sunny, Clear
            _ => new()
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
            },
        };
    }
}


public record WeatherServiceOptions(string ApiKey);