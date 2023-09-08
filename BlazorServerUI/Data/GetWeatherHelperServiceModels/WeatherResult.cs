namespace BlazorServerUI.Data.GetWeatherHelperServiceModels;

public class WeatherResult
{
    public required DateTimeOffset Sunrize { get; set; }

    public required DateTimeOffset Sunset { get; set; }

    public required DateTimeOffset LocalTime { get; set; }

    public required TimeSpan SunsetAndSunrizeDuration { get; set; }

    public required string Location { get; set; }



    public required WeatherState CurrentWeather { get; set; }

    public required WeatherState[] HourlyWeather { get; set; }
}
