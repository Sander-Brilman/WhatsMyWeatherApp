namespace BlazorServerUI.Data.GetWeatherHelperServiceModels;

public class WeatherState
{
    public DateTimeOffset Time { get; set; }

    public required string Status { get; set; } = "";

    public required float AverageTempInCelcius { get; set; }

    public required float PrecipitationInMM { get; set; }

    public required BackgroundWeatherGenerationOptions WeatherGenerationOptions { get; set; }
}


