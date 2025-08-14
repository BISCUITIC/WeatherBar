namespace API;

internal interface ILocationResponseProvider
{
    Task<LocationResponse?> TryGetLocationResponse(string city);    
}
