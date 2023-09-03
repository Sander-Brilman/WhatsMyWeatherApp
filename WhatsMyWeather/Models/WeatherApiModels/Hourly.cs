namespace WhatsMyWeather.Models.WeatherApiModels;

public class Hourly
{
    public string icon { get; set; }
    public HourlyData[] data { get; set; }
}

