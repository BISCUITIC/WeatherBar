namespace Localization;

public interface ILocalizationProvider
{
    ILocalizationData GetLocalization(string language);
}
