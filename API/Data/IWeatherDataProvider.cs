namespace API.Data;

public interface IWeatherDataProvider
{
    string Temperature { get; }
    string TemperatureFeelsLike { get; }
    string Pressure { get; }
    string Humidity { get; }
    string WindSpeed { get; }
    string Description { get; }
}
