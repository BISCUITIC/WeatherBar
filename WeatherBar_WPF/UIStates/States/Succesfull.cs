using API.Data;
using WeatherIconGenerator;

namespace WeatherBar_WPF.UIStates;

internal class Succesfull : UIState
{
    private readonly IWeatherDataProvider _weather;
    private readonly ILocationDataProvider _location;
    private readonly IIconProvider _iconProvider;

    public Succesfull(IWeatherDataProvider weatherData, ILocationDataProvider locationData, IIconProvider iconProvider)
    {
        _weather = weatherData;
        _location = locationData;
        _iconProvider = iconProvider;
    }

    public override void Apply(MainPanel ui)
    {
        ui.CityInput.Text = _location.Name + ", " + _location.Country;
        ui.CityInput.LastUpdateTime = DateTime.Now.ToShortTimeString();
        ui.CityInput.State = "Updated";

        ui.WeatherDataPanel.Temperature.Data = _weather.Temperature;
        ui.WeatherDataPanel.Pressure.Data = _weather.Pressure;
        ui.WeatherDataPanel.Humidity.Data = _weather.Humidity;
        ui.WeatherDataPanel.WindSpeed.Data = _weather.WindSpeed;
        ui.WeatherDataPanel.Description.Data = _weather.Description;

        ui.TrayIcon.Icon = GenerateIcon();

        _iconProvider.Dispose();
    }

    private System.Drawing.Icon GenerateIcon()
    {
        int roundedTemperature = (int)Math.Round(Convert.ToDouble(_weather.Temperature));
        System.Drawing.Bitmap IconBitmap = _iconProvider.GetIconBitmap(roundedTemperature);
        return System.Drawing.Icon.FromHandle(IconBitmap.GetHicon());
    }
}
