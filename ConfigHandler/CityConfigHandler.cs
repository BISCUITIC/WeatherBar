namespace ConfigHandler;

public class CityConfigHandler : ICityInputHandler
{
    private const string _path = "config.txt";
    private const string _defaultСity = "Minsk";

    public event Action? OnCityChange;

    public CityConfigHandler()
    {
        if (!File.Exists(_path)) { File.WriteAllText(_path, _defaultСity); }
    }

    public string GetLastCity()
    {
        using (StreamReader file = new StreamReader(_path))
        {
            return file.ReadLine() ?? throw new FileLoadException("Файл конфига оказался пустым");
        }
    }

    public void SaveLastCity(string city)
    {
        using (StreamWriter file = new StreamWriter(_path, append: false))
        {
            file.WriteLine(city);
        }
        OnCityChange?.Invoke();
    }
}
