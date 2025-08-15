namespace API;

public interface ILocationResponseProvider
{
    Task<LocationResponse?> TryGetLocationResponse(string city);
}
