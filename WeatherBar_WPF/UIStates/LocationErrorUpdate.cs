namespace WeatherBar_WPF.UIStates;

internal class LocationErrorUpdate : UIState
{
    private readonly string _city;
    public LocationErrorUpdate(string city)
    {
        _city = city;
    }
    public override void Apply(UIComponents ui)
    {
        ui.CityInput.State = "Couldn't get a city";
        ui.CityInput.Text = _city;
    }
}
