namespace BlazorServerUI.Services;

public class GetWeatherHelper
{
    private readonly HttpClient _httpClient;

    public GetWeatherHelper(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
}
