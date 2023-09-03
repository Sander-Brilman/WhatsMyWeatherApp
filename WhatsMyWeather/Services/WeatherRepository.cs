using WhatsMyWeather.Enums;

namespace WhatsMyWeather.Services;

public class WeatherRepository
{
    private readonly HttpClient _httpClient;

    public WeatherRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }


}
