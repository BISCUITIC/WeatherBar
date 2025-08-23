namespace API;

public interface IWeatherResponseProvider
{
    Task<WeatherResponse?> TryGetWeatherResponse(double latitude, double longitude, string language);
}
