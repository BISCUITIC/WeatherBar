using Hardcodet.Wpf.TaskbarNotification;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Controls;
using WeatherBar_WPF.CustomUI;
using WeatherBar_WPF.UIStates;
using Localization;

namespace WeatherBar_WPF;

internal class UIComponents : IDisposable
{
    private LanguageLocalization _languageLocalization;

    private TaskbarIcon _trayIcon;

    private Border _trayLayout;
    private StackPanel _mainPanel;

    private CustomTextBox _cityInput;

    private CustomLabel _temperature;
    private CustomLabel _temperatureFeelsLike;
    private CustomLabel _pressure;
    private CustomLabel _humidity;
    private CustomLabel _windSpeed;
    private CustomLabel _description;

    private Button _exitButton;   

    public TaskbarIcon TrayIcon => _trayIcon;

    public CustomTextBox CityInput => _cityInput;

    public CustomLabel Temperature => _temperature;
    public CustomLabel TemperatureFeelsLike => _temperatureFeelsLike;
    public CustomLabel Pressure => _pressure;
    public CustomLabel Humidity => _humidity;
    public CustomLabel WindSpeed => _windSpeed;
    public CustomLabel Description => _description;

    

    public UIComponents(LanguageLocalization languageLocalization ,RoutedEventHandler exit, KeyEventHandler cityKeyPressHandler)
    {
        _languageLocalization = languageLocalization;
        InitComponents();
        _exitButton.Click += exit;
        _cityInput.KeyDown += cityKeyPressHandler;
    }
    private void InitComponents()
    {
        FontFamily fontFamily = new FontFamily("Segoe UI");
        double fontSize   = 16;
        Brush foreground = new SolidColorBrush(Colors.White);

        _cityInput = new CustomTextBox()
        {
            FontFamily = fontFamily,
            FontSize = fontSize,
            Foreground = foreground,
            Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0)),
            BorderBrush = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0)),
            HorizontalAlignment = HorizontalAlignment.Stretch,
        };

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

        _exitButton = new Button
        {
            FontFamily = fontFamily,
            FontSize = 12,
            Foreground = foreground,
            Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0)),
            BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255)),            
            Width = 50,
            Padding = new Thickness(2),
            Margin = new Thickness(0, 7, 0, 3),
            Content = "Выход",
        };

        _mainPanel = new StackPanel()
        {
            Margin = new Thickness(5),
            Children = { _cityInput, _temperature, _pressure, _humidity, _windSpeed, _description, _exitButton },
        };

        _trayLayout = new Border
        {
            Background = new SolidColorBrush(Color.FromArgb(225, 44, 44, 44)),
            BorderBrush = new SolidColorBrush(Color.FromArgb(0, 28, 28, 28)),
            CornerRadius = new CornerRadius(7),
            Width = 225,
            Child = _mainPanel,
        };

        _trayIcon = new TaskbarIcon
        {
            Icon = System.Drawing.SystemIcons.Information,
            TrayPopup = _trayLayout,
            PopupActivation = PopupActivationMode.LeftOrRightClick,
            ToolTipText = "Weather Bar",
        };
    }

    public void ChangeState(string state)
    {
        _cityInput.State = state;
    }

    public void Update(UIState state)
    {
        state.Apply(this);
    }

    public void Dispose()
    {      
        _trayIcon.Dispose();
    }
}
