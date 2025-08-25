using Localization;

namespace ConfigHandler;

public class LocalizationConfigHandler : ILocalizationHandler
{
    private const string _path = "localization_config..txt";
    private const string _defaultLanguage = Languages.EN;

    public event Action? OnLocalizationChange;

    public LocalizationConfigHandler()
    {
        if (!File.Exists(_path)) { File.WriteAllText(_path, _defaultLanguage); }
    }

    public string GetLastLocalization()
    {
        using (StreamReader file = new StreamReader(_path))
        {
            return file.ReadLine() ?? throw new FileLoadException("Файл конфига оказался пустым");
        }
    }

    public void SaveLastLocalization(string localization)
    {
        using (StreamWriter file = new StreamWriter(_path, append: false))
        {
            file.WriteLine(localization);
        }
        OnLocalizationChange?.Invoke();
    }
}
