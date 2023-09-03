namespace WhatsMyWeather.Models.WeatherApiModels;

public class WeatherApiRootObject
{
    public string timezone { get; set; }
    public float offset { get; set; }
    public Currently currently { get; set; }
    public Hourly hourly { get; set; }
}

