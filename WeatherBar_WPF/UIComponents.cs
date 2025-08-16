using Hardcodet.Wpf.TaskbarNotification;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Controls;
using WeatherBar_WPF.CustomUI;
using WeatherBar_WPF.UIStates;

namespace WeatherBar_WPF;

internal class UIComponents : IDisposable
{
    private TaskbarIcon _trayIcon;

    private Border _trayLayout;
    private StackPanel _stackPanel;

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

    public UIComponents(RoutedEventHandler exit, KeyEventHandler cityKeyPressHandler)
    {
        InitComponents();
        _exitButton.Click += exit;
        _cityInput.KeyDown += cityKeyPressHandler;
    }
    private void InitComponents()
    {
        Brush labelForeground = new SolidColorBrush(Colors.White);
        FontFamily labelFontFamily = new FontFamily("Segoe UI");
        double labelFontSize = 16;

        _cityInput = new CustomTextBox()
        {
            FontFamily = labelFontFamily,
            FontSize = labelFontSize,
            Foreground = labelForeground,
            Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0)),
            BorderBrush = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0)),
            HorizontalAlignment = HorizontalAlignment.Stretch,
        };

        _temperature = new CustomLabel(0.ToString(), "Температура : ", " °C")
        {
            FontFamily = labelFontFamily,
            FontSize = labelFontSize,
            Foreground = labelForeground,
        };
        _pressure = new CustomLabel(0.ToString(), "Давление : ", " mm Hg")
        {
            FontFamily = labelFontFamily,
            FontSize = labelFontSize,
            Foreground = labelForeground,
        };
        _humidity = new CustomLabel(0.ToString(), "Влажность : ", " %")
        {
            FontFamily = labelFontFamily,
            FontSize = labelFontSize,
            Foreground = labelForeground,
        };
        _windSpeed = new CustomLabel(0.ToString(), "Скорость ветра : ", " m/s")
        {
            FontFamily = labelFontFamily,
            FontSize = labelFontSize,
            Foreground = labelForeground,
        };
        _description = new CustomLabel(0.ToString(), "Погода : ", null)
        {
            FontFamily = labelFontFamily,
            FontSize = labelFontSize,
            Foreground = labelForeground,
        };

        _exitButton = new Button
        {
            Content = "Выход",
            Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0)),
            BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255)),
            Foreground = labelForeground,
            Width = 50,
            Padding = new Thickness(2),
            Margin = new Thickness(0, 7, 0, 3),
        };

        _stackPanel = new StackPanel()
        {
            Children = { _cityInput, _temperature, _pressure, _humidity, _windSpeed, _description, _exitButton },
            Margin = new Thickness(5),
        };

        _trayLayout = new Border
        {
            Background = new SolidColorBrush(Color.FromArgb(225, 44, 44, 44)),
            BorderBrush = new SolidColorBrush(Color.FromArgb(0, 28, 28, 28)),
            CornerRadius = new CornerRadius(7),
            Width = 225,
            Child = _stackPanel,
        };

        _trayIcon = new TaskbarIcon
        {
            Icon = System.Drawing.SystemIcons.Information,
            ToolTipText = "Weather Bar",
            TrayPopup = _trayLayout,
            PopupActivation = PopupActivationMode.LeftOrRightClick,
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
