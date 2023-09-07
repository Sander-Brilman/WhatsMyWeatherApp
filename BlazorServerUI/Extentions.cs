using BlazorServerUI.Enums;

namespace BlazorServerUI;

public static class Extentions
{
    public static WeatherBackground GetBackgroundFromTime(this DateTimeOffset time, bool cloudyIfDay = false)
    {
        if (time.Hour >= 5 && time.Hour <= 9)
        {
            return WeatherBackground.morning;
        }
        else if (time.Hour >= 10 && time.Hour <= 18)
        {
            return cloudyIfDay ? WeatherBackground.darkdaylight : WeatherBackground.daylight;
        }

        return WeatherBackground.midnight;
    }
}
