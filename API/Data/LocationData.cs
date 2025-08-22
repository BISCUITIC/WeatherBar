namespace API.Data;

public class LocationData : ILocationDataProvider
{
    public double Latitude { get; private init; }
    public double Longitude { get; private init; }
    public string Name { get; private init; }
    public string Country { get; private init; }

    public LocationData(LocationResponse location)
    {
        Latitude = location.Latitude;
        Longitude = location.Longitude;

        Name = location.Name;
        Country = location.Country;
    }
}
