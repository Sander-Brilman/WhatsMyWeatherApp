using WhatsMyWeatherWeb.Enums;

namespace WhatsMyWeatherWeb;

public static class Extentions
{
    public static TimeOfDay GetTimeOfDay(
        this DateTimeOffset localtime, 
        DateTimeOffset sunriseStart, 
        DateTimeOffset sunsetStart, 
        TimeSpan SunriseDuration,
        TimeSpan SunsetDuration
    ) {
        TimeOfDay result;

        DateTimeOffset sunriseEnd = sunriseStart.Add(SunriseDuration);
        DateTimeOffset sunsetEnd = sunsetStart.Add(SunsetDuration);

        if (localtime.Hour == 7) 
            Console.WriteLine("e");


        if (localtime < sunriseStart)
        {
            result = TimeOfDay.Night;
        }
        else if (localtime >= sunriseStart && localtime <= sunriseEnd) 
        {
            result = TimeOfDay.Sunrise;
        }
        else if (localtime > sunriseEnd && localtime < sunsetStart)
        {
            result = TimeOfDay.Day;
        }
        else if (localtime >= sunsetStart && localtime <= sunsetEnd)
        {
            result = TimeOfDay.Sunset;
        } 
        else
        {
            result = TimeOfDay.Night;
        }

        return result;
    }

    public static string ToCssClass(this TimeOfDay timeOfDay, bool setDaylightToDark = false)
    {
        return (timeOfDay switch
        {
            TimeOfDay.Night => WeatherBackgroundCSSClass.Night,
            TimeOfDay.Sunrise => WeatherBackgroundCSSClass.Sunrise,
            TimeOfDay.Day => setDaylightToDark ? WeatherBackgroundCSSClass.DarkDay : WeatherBackgroundCSSClass.Day,
            TimeOfDay.Sunset => WeatherBackgroundCSSClass.Sunset,
            _ => throw new NotImplementedException($"{timeOfDay} is not implemented"),
        }).ToString();
    }
}
