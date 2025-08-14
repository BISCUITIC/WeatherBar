namespace API.Data;

public class LocationData : ILocationDataProvider
{
    public double Latitude { get; init; }

    public double Longitude { get; init; }

    public LocationData(LocationResponse location)
    {
        Latitude = location.Latitude;
        Longitude = location.Longitude;
    }
}
