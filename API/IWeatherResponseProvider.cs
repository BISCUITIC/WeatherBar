namespace API;

internal interface IWeatherResponseProvider
{
    Task<WeatherResponse?> TryGetWeatherResponse(double latitude, double longitude);
}
