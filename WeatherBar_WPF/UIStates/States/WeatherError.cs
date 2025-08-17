using API.Data;

namespace WeatherBar_WPF.UIStates;

internal class WeatherError : UIState
{
    private readonly ILocationDataProvider _location;

    public WeatherError(ILocationDataProvider locationDataProvider)
    {
        _location = locationDataProvider;
    }
    public override void Apply(UIComponents ui)
    {
        ui.CityInput.State = "Couldn't get a weather";
        ui.CityInput.Text = _location.Name + ", " + _location.Country;
    }
}
