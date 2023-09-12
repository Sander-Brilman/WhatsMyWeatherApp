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


        DateTime sunriseStart = coordinate.CelestialInfo.AdditionalSolarTimes.AstronomicalDawn.Value;

        TimeSpan sunriseDuration = coordinate.CelestialInfo.SunRise - sunriseStart 
            ?? throw new Exception($"{nameof(sunriseDuration)} is empty");




        DateTime sunsetStart = coordinate.CelestialInfo.SunSet.Value;

        TimeSpan sunsetDuration = coordinate.CelestialInfo.AdditionalSolarTimes.AstronomicalDusk - sunsetStart
            ?? throw new Exception($"{nameof(sunsetDuration)} timespan is empty");


        return (sunriseStart, sunriseDuration, sunsetStart, sunsetDuration);
    }
}
