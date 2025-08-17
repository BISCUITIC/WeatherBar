namespace ConfigHandler;

public interface ICityInputHandler
{
    event Action? OnCityChange;
    string GetLastCity();
    void SaveLastCity(string city);
}
