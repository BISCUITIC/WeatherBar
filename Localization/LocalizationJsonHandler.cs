using System.Text.Json;

namespace Localization;

public class LocalizationJsonHandler : ILocalizationProvider
{
    private const string PATH_LOCALZATION_CONFIGS = "localization";

    public LocalizationJsonHandler() { }

    public ILocalizationData GetLocalization(string language)
    {
        string content = GetContent(GetPath(language));

        return JsonSerializer.Deserialize<LocalizationData>(content) ??
               throw new NullReferenceException("Не удалось десерилизовать файл локализации");
    }

    private string GetPath(string language)
    {
        return $"{PATH_LOCALZATION_CONFIGS}\\{language}.json";
    }

    private string GetContent(string path)
    {
        using StreamReader stream = new StreamReader(path, System.Text.Encoding.ASCII);
        return stream.ReadToEnd();
    }
}
