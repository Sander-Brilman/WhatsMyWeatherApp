using BlazorServerUI.Enums;

namespace BlazorServerUI.Data.GetWeatherHelperServiceModels;

public class BackgroundWeatherGenerationOptions
{

    /// <summary>
    /// Decices the number of clouds spawned on the screen by deviding the screenwidth in pixels by this number
    /// </summary>
    public required int CloudGenerationFactor { get; set; }
    public required int TimeBetweenCloudSpawnInMiliseconds { get; set; }


    public required int MaxCloudSpeedInPixelsPerSecond { get; set; }
    public required int MinCloudSpeedInPixelsPerSecond { get; set; }


    public required int MaxCloudElevationInPixels { get; set; }
    public required int MinCloudElevationInPixels { get; set; }


    public required int MaxCloudScale { get; set; }
    public required int MinCloudScale { get; set; }


    public required int MaxCloudOpacity { get; set; }
    public required int MinCloudOpacity { get; set; }


    public string? TypeOfPrecipitation { get; set; }
    public double PrecipitationSpawnIntervalInSeconds { get; set; }
    public bool Thunder { get; set; } = false;

    public required string BackgroundCssClass { get; set; }
}
