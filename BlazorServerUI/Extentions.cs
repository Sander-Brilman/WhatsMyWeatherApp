using BlazorServerUI.Enums;

namespace BlazorServerUI;

public static class Extentions
{
    public static TimeOfDay GetTimeOfDay(
        this DateTimeOffset localtime, 
        DateTimeOffset sunrise, 
        DateTimeOffset sunset, 
        TimeSpan SunriseDuration,
        TimeSpan SunsetDuration
    ) {
        return
            localtime > sunrise && localtime < sunset
                ? localtime < sunrise.Add(SunriseDuration)
                    ? TimeOfDay.Sunrise
                    : localtime > sunset.Subtract(SunsetDuration)
                        ? TimeOfDay.Sunset
                        : TimeOfDay.Day
                : TimeOfDay.Night;
    }

    public static string ToCssClass(this TimeOfDay timeOfDay, bool setDaylightToDark = false)
    {
        return (timeOfDay switch
        {
            TimeOfDay.Night => WeatherBackgroundCSSClass.Night,
            TimeOfDay.Sunrise => WeatherBackgroundCSSClass.Sunrise,
            TimeOfDay.Day => setDaylightToDark ? WeatherBackgroundCSSClass.DarkDay : WeatherBackgroundCSSClass.Day,
            TimeOfDay.Sunset => WeatherBackgroundCSSClass.Sunset,
        }).ToString();
    }
}
