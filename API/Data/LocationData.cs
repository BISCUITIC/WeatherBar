using System.Diagnostics.Metrics;
using System.Xml.Linq;

namespace API.Data;

public class LocationData : ILocationDataProvider
{
    public double Latitude { get; init; }
    public double Longitude { get; init; }
    public string Name { get; init; }
    public string Country { get; init; }

    public LocationData(LocationResponse location)
    {
        Latitude = location.Latitude;
        Longitude = location.Longitude;
        
        Name = location.Name;
        Country = location.Country;
    }
}
