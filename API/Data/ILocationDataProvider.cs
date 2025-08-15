namespace API.Data;

public interface ILocationDataProvider
{
    double Latitude { get; }
    double Longitude { get; }

    string Name { get; }
    string Country { get; }
}
