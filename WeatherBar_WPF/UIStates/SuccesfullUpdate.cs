using API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherIconGenerator;

namespace WeatherBar_WPF.UIStates;

internal class SuccesfullUpdate : UIState
{
    private readonly IWeatherDataProvider _weather;
    private readonly ILocationDataProvider _location;
    private readonly IIconProvider _iconProvider;

    public SuccesfullUpdate(IWeatherDataProvider weatherData, ILocationDataProvider locationData, IIconProvider iconProvider)
    {
        _weather = weatherData;
        _location = locationData;
        _iconProvider = iconProvider;
    }

    public override void Apply(UIComponents ui)
    {
        ui.CityInput.Text = _location.Name + ", " + _location.Country;
        ui.CityInput.LastUpdateTime = DateTime.Now.ToShortTimeString();
        ui.CityInput.State = "Updated";

        ui.Temperature.Data = _weather.Temperature;
        ui.Pressure.Data = _weather.Pressure;
        ui.Humidity.Data = _weather.Humidity;
        ui.WindSpeed.Data = _weather.WindSpeed;
        ui.Description.Data = _weather.Description;

        int roundedTemperature = (int)Math.Round(Convert.ToDouble(_weather.Temperature));
        System.Drawing.Bitmap IconBitmap = _iconProvider.GetIconBitmap(roundedTemperature);
        ui.TrayIcon.Icon = System.Drawing.Icon.FromHandle(IconBitmap.GetHicon());

        _iconProvider.Dispose();
    }
}
