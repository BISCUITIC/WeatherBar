namespace WeatherBar_WPF.UIStates;

internal abstract class UIState
{
    public abstract void Apply(MainPanel ui);
}
