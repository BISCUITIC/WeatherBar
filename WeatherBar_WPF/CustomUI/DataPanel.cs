using Localization;
using System.Windows;
using System.Windows.Controls;

namespace WeatherBar_WPF.CustomUI;

internal class DataPanel : UserControl
{
    private readonly ILocalizationData _localization;

    private readonly StackPanel _layout;

    private readonly CustomLabel _temperature;
    private readonly CustomLabel _temperatureFeelsLike;
    private readonly CustomLabel _pressure;
    private readonly CustomLabel _humidity;
    private readonly CustomLabel _windSpeed;
    private readonly CustomLabel _description;

    public CustomLabel Temperature => _temperature;
    public CustomLabel TemperatureFeelsLike => _temperatureFeelsLike;
    public CustomLabel Pressure => _pressure;
    public CustomLabel Humidity => _humidity;
    public CustomLabel WindSpeed => _windSpeed;
    public CustomLabel Description => _description;

    public DataPanel(ILocalizationData languageLocalization,
                     System.Windows.Media.FontFamily fontFamily,
                     double fontSize,
                     System.Windows.Media.Brush foreground)
    {
        _localization = languageLocalization;

        _temperature = new CustomLabel(0.ToString(), $"{_localization.Temperature} : ", " °C")
        {
            FontFamily = fontFamily,
            FontSize = fontSize,
            Foreground = foreground,
        };
        _pressure = new CustomLabel(0.ToString(), $"{_localization.Pressure} : ", " mm Hg")
        {
            FontFamily = fontFamily,
            FontSize = fontSize,
            Foreground = foreground,
        };
        _humidity = new CustomLabel(0.ToString(), $"{_localization.Humidity} : ", " %")
        {
            FontFamily = fontFamily,
            FontSize = fontSize,
            Foreground = foreground,
        };
        _windSpeed = new CustomLabel(0.ToString(), $"{_localization.WindSpeed} : ", " m/s")
        {
            FontFamily = fontFamily,
            FontSize = fontSize,
            Foreground = foreground,
        };
        _description = new CustomLabel(0.ToString(), $"{_localization.Description} : ", null)
        {
            FontFamily = fontFamily,
            FontSize = fontSize,
            Foreground = foreground,
        };

        _layout = new StackPanel()
        {
            Margin = new Thickness(5),
            Children = { _temperature, _pressure, _humidity, _windSpeed, _description },
        };

        Content = _layout;
    }
}
