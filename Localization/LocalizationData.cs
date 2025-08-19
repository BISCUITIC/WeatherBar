using System.Text.Json.Serialization;

namespace Localization;

public class LocalizationData : ILocalizationData
{
    [JsonPropertyName("Temperature")]
    public string Temperature { get; set; }
    [JsonPropertyName("Pressure")]
    public string Pressure { get; set; }
    [JsonPropertyName("Humidity")]
    public string Humidity { get; set; }
    [JsonPropertyName("WindSpeed")]
    public string WindSpeed { get; set; }
    [JsonPropertyName("Description")]
    public string Description { get; set; }
}
