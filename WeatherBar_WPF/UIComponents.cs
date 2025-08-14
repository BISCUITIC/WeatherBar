using Hardcodet.Wpf.TaskbarNotification;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

using API.Data;
using WeatherIconGenerator;

namespace WeatherBar_WPF;

internal class UIComponents: IDisposable
{
    private TaskbarIcon _trayIcon;

    private Border _trayLayout;
    private StackPanel _stackPanel;

    private DataLabel _temperature;
    private DataLabel _temperatureFeelsLike;
    private DataLabel _pressure;
    private DataLabel _humidity;
    private DataLabel _windSpeed;
    private DataLabel _description;
    
    private Label _lastUpdateTime;

    private Button _exitButton;

    private readonly IIconProvider _iconProvider;

    public UIComponents(RoutedEventHandler exit)
    {
        InitComponents();
        _exitButton.Click += exit;

        _iconProvider = new IconGenerator();
    }
    private void InitComponents()
    {
        Brush labelForeground = new SolidColorBrush(Colors.White);
        FontFamily labelFontFamily = new FontFamily("Segoe UI");
        double labelFontSize = 16;

        _temperature = new DataLabel(0.ToString(), "Температура : ", " °C")
        {
            FontFamily = labelFontFamily,
            FontSize = labelFontSize,
            Foreground = labelForeground,
        };
        _pressure = new DataLabel(0.ToString(), "Давление : ", " mm Hg")
        {
            FontFamily = labelFontFamily,
            FontSize = labelFontSize,
            Foreground = labelForeground,
        };
        _humidity = new DataLabel(0.ToString(), "Влажность : ", " %")
        {
            FontFamily = labelFontFamily,
            FontSize = labelFontSize,
            Foreground = labelForeground,
        };
        _windSpeed = new DataLabel(0.ToString(), "Скорость ветра : ", " m/s")
        {
            FontFamily = labelFontFamily,
            FontSize = labelFontSize,
            Foreground = labelForeground,
        };
        _description = new DataLabel(0.ToString(), "Погода : ", null)
        {
            FontFamily = labelFontFamily,
            FontSize = labelFontSize,
            Foreground = labelForeground,
        };
        _lastUpdateTime = new Label()
        {
            FontFamily = labelFontFamily,
            FontSize = 12,
            HorizontalAlignment = HorizontalAlignment.Right,            
            VerticalContentAlignment = VerticalAlignment.Top,
            Foreground = new SolidColorBrush(Color.FromArgb(180,255,255,255)),
        };

        _exitButton = new Button
        {
            Content = "Выход",
            Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0)),
            BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255)),
            Foreground = labelForeground,
            Width = 50,
            Padding = new Thickness(2),
        };

        _stackPanel = new StackPanel()
        {
            Children = { _lastUpdateTime, _temperature, _pressure, _humidity, _windSpeed, _description,_exitButton },
            Margin = new Thickness(5),
        };

        _trayLayout = new Border
        {
            Background = new SolidColorBrush(Color.FromArgb(255, 44, 44, 44)),
            BorderBrush = new SolidColorBrush(Color.FromArgb(0, 28, 28, 28)),
            BorderThickness = new Thickness(2),
            CornerRadius = new CornerRadius(5),
            Width = 200,
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

    public void UpdateComponents(IWeatherDataProvider weatherData)
    {        
        _pressure.Data = weatherData.Pressure;
        _temperature.Data = weatherData.Temperature;        
        _humidity.Data = weatherData.Humidity;
        _windSpeed.Data = weatherData.WindSpeed;
        _description.Data = weatherData.Description;
        _lastUpdateTime.Content = DateTime.Now.ToShortTimeString();

        int roundedTemperature = (int)Math.Round(Convert.ToDouble(weatherData.Temperature));
        System.Drawing.Bitmap IconBitmap = _iconProvider.GetIconBitmap(roundedTemperature);
        _trayIcon.Icon = System.Drawing.Icon.FromHandle(IconBitmap.GetHicon());
    }

    public void Dispose()
    {
        _iconProvider.Dispose();
        _trayIcon.Dispose();
    }
}
