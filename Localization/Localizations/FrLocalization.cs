namespace Localization.Localizations;

public class FrLocalization : LanguageLocalization
{
    public FrLocalization(ILocalizationProvider localizationProvider) :
           base(Languages.FR, localizationProvider)
    { }
}
