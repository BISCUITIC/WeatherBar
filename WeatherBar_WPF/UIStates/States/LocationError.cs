namespace WeatherBar_WPF.UIStates;

internal class LocationError : UIState
{
    private readonly string _city;
    public LocationError(string city)
    {
        _city = city;
    }
    public override void Apply(MainPanel ui)
    {
        ui.CityInput.State = "Couldn't get a city";
        ui.CityInput.Text = _city;
    }
}
