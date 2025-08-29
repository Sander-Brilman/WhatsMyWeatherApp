namespace WhatsMyWeatherWeb.Data.RawWeatherApiModels;

public class WeahterApiRootobject
{
    public required Location location { get; set; }
    public required Current current { get; set; }
    public required Forecast forecast { get; set; }
}

public class Location
{
    public required string name { get; set; }
    public required string region { get; set; }
    public required string country { get; set; }
    public required float lat { get; set; }
    public required float lon { get; set; }
    public required string tz_id { get; set; }
    public required int localtime_epoch { get; set; }
    public required string localtime { get; set; }
}

public class Current
{
    public required int last_updated_epoch { get; set; }
    public required string last_updated { get; set; }
    public required float temp_c { get; set; }
    public required float temp_f { get; set; }
    public required int is_day { get; set; }
    public required Condition condition { get; set; }
    public required float wind_mph { get; set; }
    public required float wind_kph { get; set; }
    public required int wind_degree { get; set; }
    public required string wind_dir { get; set; }
    public required float pressure_mb { get; set; }
    public required float pressure_in { get; set; }
    public required float precip_mm { get; set; }
    public required float precip_in { get; set; }
    public required int humidity { get; set; }
    public required int cloud { get; set; }
    public required float feelslike_c { get; set; }
    public required float feelslike_f { get; set; }
    public required float vis_km { get; set; }
    public required float vis_miles { get; set; }
    public required float uv { get; set; }
    public required float gust_mph { get; set; }
    public required float gust_kph { get; set; }
}

public class Condition
{
    public required string text { get; set; }
    public required string icon { get; set; }
    public required int code { get; set; }
}

public class Forecast
{
    public required Forecastday[] forecastday { get; set; }
}

public class Forecastday
{
    public required string date { get; set; }
    public required int date_epoch { get; set; }
    public required Day day { get; set; }
    public required Astro astro { get; set; }
    public required Hour[] hour { get; set; }
}

public class Day
{
    public required float maxtemp_c { get; set; }
    public required float maxtemp_f { get; set; }
    public required float mintemp_c { get; set; }
    public required float mintemp_f { get; set; }
    public required float avgtemp_c { get; set; }
    public required float avgtemp_f { get; set; }
    public required float maxwind_mph { get; set; }
    public required float maxwind_kph { get; set; }
    public required float totalprecip_mm { get; set; }
    public required float totalprecip_in { get; set; }
    public required float totalsnow_cm { get; set; }
    public required float avgvis_km { get; set; }
    public required float avgvis_miles { get; set; }
    public required float avghumidity { get; set; }
    public required int daily_will_it_rain { get; set; }
    public required int daily_chance_of_rain { get; set; }
    public required int daily_will_it_snow { get; set; }
    public required int daily_chance_of_snow { get; set; }
    public required Condition1 condition { get; set; }
    public required float uv { get; set; }
}

public class Condition1
{
    public required string text { get; set; }
    public required string icon { get; set; }
    public required int code { get; set; }
}

public class Astro
{
    public required string sunrise { get; set; }
    public required string sunset { get; set; }
    public required string moonrise { get; set; }
    public required string moonset { get; set; }
    public required string moon_phase { get; set; }
    public required int moon_illumination { get; set; }
    public required int is_moon_up { get; set; }
    public required int is_sun_up { get; set; }
}

public class Hour
{
    public required int time_epoch { get; set; }
    public required string time { get; set; }
    public required float temp_c { get; set; }
    public required float temp_f { get; set; }
    public required int is_day { get; set; }
    public required Condition2 condition { get; set; }
    public required float wind_mph { get; set; }
    public required float wind_kph { get; set; }
    public required int wind_degree { get; set; }
    public required string wind_dir { get; set; }
    public required float pressure_mb { get; set; }
    public required float pressure_in { get; set; }
    public required float precip_mm { get; set; }
    public required float precip_in { get; set; }
    public required int humidity { get; set; }
    public required int cloud { get; set; }
    public required float feelslike_c { get; set; }
    public required float feelslike_f { get; set; }
    public required float windchill_c { get; set; }
    public required float windchill_f { get; set; }
    public required float heatindex_c { get; set; }
    public required float heatindex_f { get; set; }
    public required float dewpoint_c { get; set; }
    public required float dewpoint_f { get; set; }
    public required int will_it_rain { get; set; }
    public required int chance_of_rain { get; set; }
    public required int will_it_snow { get; set; }
    public required int chance_of_snow { get; set; }
    public required float vis_km { get; set; }
    public required float vis_miles { get; set; }
    public required float gust_mph { get; set; }
    public required float gust_kph { get; set; }
    public required float uv { get; set; }
}

public class Condition2
{
    public required string text { get; set; }
    public required string icon { get; set; }
    public required int code { get; set; }
}
