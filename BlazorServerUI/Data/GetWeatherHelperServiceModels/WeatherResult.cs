namespace BlazorServerUI.Data.GetWeatherHelperServiceModels;

public class WeatherResult
{
    public required DateTimeOffset SunriseStart { get; set; }

    public required DateTimeOffset SunsetStart { get; set; }

    public required DateTimeOffset LocalTime { get; set; }



    public required TimeSpan SunsetDuration { get; set; }

    public required TimeSpan SunriseDuration { get; set; }


    public required string Location { get; set; }

    public required WeatherState CurrentWeather { get; set; }

    public required WeatherState[] HourlyWeather { get; set; }
}
