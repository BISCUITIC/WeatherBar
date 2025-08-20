using System.Text.Json.Serialization;

namespace Localization;

public interface ILocalizationData
{    
    string Temperature { get; }    
    string Pressure { get; }   
    string Humidity { get; }   
    string WindSpeed { get; }   
    string Description { get; }
    string Exit { get; }
}
