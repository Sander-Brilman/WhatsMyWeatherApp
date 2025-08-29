namespace WhatsMyWeatherWeb.Data.GetWeatherHelperServiceModels;

public record WeatherResult(
    DateTimeOffset SunriseStart, 
    DateTimeOffset SunsetStart, 
    DateTimeOffset LocalTime, 
    TimeSpan SunsetDuration, 
    TimeSpan SunriseDuration, 
    string Location, 
    WeatherState CurrentWeather, 
    WeatherState[] HourlyWeather
);
