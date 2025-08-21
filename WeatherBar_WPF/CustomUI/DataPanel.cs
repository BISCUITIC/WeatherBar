using Hardcodet.Wpf.TaskbarNotification;
using Localization;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media;

namespace WeatherBar_WPF.CustomUI;

internal class DataPanel: UserControl
{
    private readonly LanguageLocalization _languageLocalization;

    private readonly StackPanel _layout;

    private CustomLabel _temperature;
    private CustomLabel _temperatureFeelsLike;
    private CustomLabel _pressure;
    private CustomLabel _humidity;
    private CustomLabel _windSpeed;
    private CustomLabel _description;

    public CustomLabel Temperature => _temperature;
    public CustomLabel TemperatureFeelsLike => _temperatureFeelsLike;
    public CustomLabel Pressure => _pressure;
    public CustomLabel Humidity => _humidity;
    public CustomLabel WindSpeed => _windSpeed;
    public CustomLabel Description => _description;

    public DataPanel(LanguageLocalization languageLocalization,
                     System.Windows.Media.FontFamily fontFamily,
                     double fontSize,
                     System.Windows.Media.Brush foreground )
    {
        _languageLocalization = languageLocalization;

        _temperature = new CustomLabel(0.ToString(), $"{_languageLocalization.Temperature} : ", " °C")
        {
            FontFamily = fontFamily,
            FontSize = fontSize,
            Foreground = foreground,
        };
        _pressure = new CustomLabel(0.ToString(), $"{_languageLocalization.Pressure} : ", " mm Hg")
        {
            FontFamily = fontFamily,
            FontSize = fontSize,
            Foreground = foreground,
        };
        _humidity = new CustomLabel(0.ToString(), $"{_languageLocalization.Humidity} : ", " %")
        {
            FontFamily = fontFamily,
            FontSize = fontSize,
            Foreground = foreground,
        };
        _windSpeed = new CustomLabel(0.ToString(), $"{_languageLocalization.WindSpeed} : ", " m/s")
        {
            FontFamily = fontFamily,
            FontSize = fontSize,
            Foreground = foreground,
        };
        _description = new CustomLabel(0.ToString(), $"{_languageLocalization.Description} : ", null)
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
