namespace WhatsMyWeather.Services;

public class GetLocationHelper
{
    private readonly HttpClient _httpClient;

    public GetLocationHelper(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public (float latitude, float longitude) GetLatitudeAndLongitudeFromCity(string city)
    {
        HttpClient httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://api.opencagedata.com/geocode/v1/");



        };
    }
}
