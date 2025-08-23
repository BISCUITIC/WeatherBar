namespace Localization;

public abstract class LanguageLocalization: ILocalizationData
{
    public string Language {  get; private init; }
    public string Temperature { get; private init; }
    public string Pressure { get; private init; }
    public string Humidity { get; private init; }
    public string WindSpeed { get; private init; }
    public string Description { get; private init; }
    public string Exit { get; private init; }

    private readonly ILocalizationProvider _localizationProvider;

    public LanguageLocalization(string language, ILocalizationProvider localizationProvider)
    {
        _localizationProvider = localizationProvider;

        ILocalizationData localizationData = _localizationProvider.GetLocalization(language);

        Language = language;
        Temperature = localizationData.Temperature;
        Pressure = localizationData.Pressure;
        Humidity = localizationData.Humidity;
        WindSpeed = localizationData.WindSpeed;
        Description = localizationData.Description;
        Exit = localizationData.Exit;
    }
}
