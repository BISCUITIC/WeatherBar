
namespace ConfigHandler;

public class CityConfigHandler: ICityInputHandler
{
    private static string _path = "config.txt";

    public event Action OnCityChange;

    public CityConfigHandler()
    {
        if (!File.Exists(_path)) { File.WriteAllText(_path, "Minsk"); }        
    }

    public string GetLastCity()
    {
        using StreamReader file = new StreamReader(_path);
        return file.ReadLine();        
    }

    public void SaveLastCity(string city)
    {
        using (StreamWriter file = new StreamWriter(_path, append: false)) 
        { 
            file.WriteLine(city);
        }
        OnCityChange.Invoke();
    }
}
