namespace BlazorServerUI.Data.RawWeatherApiModels;

public class WeatherRootObject
{
    public Location location { get; set; }
    public Current current { get; set; }
    public Forecast forecast { get; set; }
}
