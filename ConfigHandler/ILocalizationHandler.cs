namespace ConfigHandler;

public interface ILocalizationHandler
{
    event Action? OnLocalizationChange;
    string GetLastLocalization();
    void SaveLastLocalization(string localization);
}
