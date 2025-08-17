namespace API.Data;

public class WeatherData : IWeatherDataProvider
{
    public string Temperature { get; init; }
    public string TemperatureFeelsLike { get; init; }
    public string Pressure { get; init; }
    public string Humidity { get; init; }
    public string WindSpeed { get; init; }
    public string Description { get; init; }

    private const double hpa_INTO_mmHg = 0.750062;

    public WeatherData(WeatherResponse weather)
    {
        Temperature = weather.Main.Temp.ToString();
        TemperatureFeelsLike = weather.Main.FeelsLike.ToString();
        Pressure = Math.Round(weather.Main.GrndLevel * hpa_INTO_mmHg, 3).ToString();
        Humidity = weather.Main.Humidity.ToString();
        WindSpeed = weather.Wind.Speed.ToString();
        Description = weather.Weather[0].Description;


    }
}
