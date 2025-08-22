namespace API.Data;

public class WeatherData : IWeatherDataProvider
{
    public string Temperature { get; private init; }
    public string TemperatureFeelsLike { get; private init; }
    public string Pressure { get; private init; }
    public string Humidity { get; private init; }
    public string WindSpeed { get; private init; }
    public string Description { get; private init; }

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
