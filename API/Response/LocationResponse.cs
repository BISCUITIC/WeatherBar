using System.Text.Json.Serialization;

public class LocationResponse
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("local_names")]
    public Dictionary<string, string> LocalNames { get; set; }

    [JsonPropertyName("lat")]
    public double Latitude { get; set; }

    [JsonPropertyName("lon")]
    public double Longitude { get; set; }

    [JsonPropertyName("country")]
    public string Country { get; set; }

    [JsonPropertyName("state")]
    public string State { get; set; }
}
