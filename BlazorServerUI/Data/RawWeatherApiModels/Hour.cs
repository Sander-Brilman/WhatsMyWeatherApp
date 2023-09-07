namespace BlazorServerUI.Data.RawWeatherApiModels;

public class Hour
{
    public int time_epoch { get; set; }
    public string time { get; set; }
    public float temp_c { get; set; }
    public float temp_f { get; set; }
    public int is_day { get; set; }
    public HourCondition condition { get; set; }
    public float wind_mph { get; set; }
    public float wind_kph { get; set; }
    public int wind_degree { get; set; }
    public string wind_dir { get; set; }
    public int pressure_mb { get; set; }
    public float pressure_in { get; set; }
    public int precip_mm { get; set; }
    public int precip_in { get; set; }
    public int humidity { get; set; }
    public int cloud { get; set; }
    public float feelslike_c { get; set; }
    public float feelslike_f { get; set; }
    public float windchill_c { get; set; }
    public float windchill_f { get; set; }
    public float heatindex_c { get; set; }
    public float heatindex_f { get; set; }
    public float dewpoint_c { get; set; }
    public float dewpoint_f { get; set; }
    public int will_it_rain { get; set; }
    public int chance_of_rain { get; set; }
    public int will_it_snow { get; set; }
    public int chance_of_snow { get; set; }
    public int vis_km { get; set; }
    public int vis_miles { get; set; }
    public float gust_mph { get; set; }
    public float gust_kph { get; set; }
    public int uv { get; set; }
}
