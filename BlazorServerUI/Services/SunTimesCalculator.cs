using CoordinateSharp;

namespace BlazorServerUI.Services;

public static class SunTimesCalculator
{
    public static (DateTimeOffset sunriseStart, TimeSpan sunriseDuration, DateTimeOffset sunsetStart, TimeSpan sunsetDuration) CalculateSunsetSunrise(double latitude, double longitude, DateTimeOffset localtime)
    {
        Coordinate coordinate = new(latitude, longitude, DateTime.UtcNow)
        {
            Offset = localtime.Offset.Hours
        };

        coordinate.LoadCartesianInfo();


        DateTimeOffset sunriseStart = new(coordinate.CelestialInfo.AdditionalSolarTimes.NauticalDawn.Value, localtime.Offset);

        TimeSpan sunriseDuration = new DateTimeOffset(coordinate.CelestialInfo.SunRise.Value, localtime.Offset) - sunriseStart;




        DateTimeOffset sunsetStart = new(coordinate.CelestialInfo.SunSet.Value, localtime.Offset);

        TimeSpan sunsetDuration = new DateTimeOffset(coordinate.CelestialInfo.AdditionalSolarTimes.NauticalDusk.Value, localtime.Offset) - sunsetStart;


        return (sunriseStart, sunriseDuration, sunsetStart, sunsetDuration);
    }
}
