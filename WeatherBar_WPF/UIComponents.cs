using Hardcodet.Wpf.TaskbarNotification;
using Localization;
//using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WeatherBar_WPF.CustomUI;
using WeatherBar_WPF.UIStates;

namespace WeatherBar_WPF;

internal class UIComponents : IDisposable
{
    private ILocalizationData _localization;

    private readonly TaskbarIcon _trayIcon;

    private readonly Border _trayLayout;
    private readonly StackPanel _mainPanel;

    private readonly CustomTextBox _cityInput;
    private readonly DataPanel _weatherDataPanel;
    private readonly BottomPanel _bottomPanel;

    public  CustomTextBox CityInput => _cityInput;
    public DataPanel WeatherDataPanel => _weatherDataPanel;
    public TaskbarIcon TrayIcon => _trayIcon;

    public RoutedEventHandler? ExitButtonClick { get; set; }
    public RoutedEventHandler? SettingButtonClick { get; set; }
    public KeyEventHandler? CityInputKeyPress { get; set; }

    public UIComponents(ILocalizationData languageLocalization)
    {
        _localization = languageLocalization;

        FontFamily fontFamily = new FontFamily("Segoe UI");
        double fontSize = 16;
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
        _cityInput.KeyDown += CityInputKeyPress;

        _weatherDataPanel = new DataPanel(_localization, fontFamily, fontSize, foreground);

        _bottomPanel = new BottomPanel(_localization, fontFamily, fontSize, foreground);
        _bottomPanel.ExitButtonClick += ExitButtonClick;
        _bottomPanel.SettingButtonClick += SettingButtonClick;

        _mainPanel = new StackPanel()
        {
            Margin = new Thickness(5),
            Children = { _cityInput, _weatherDataPanel, _bottomPanel },
        };

        _trayLayout = new Border
        {
            Background = new SolidColorBrush(Color.FromArgb(225, 44, 44, 44)),
            BorderBrush = new SolidColorBrush(Color.FromArgb(0, 28, 28, 28)),
            CornerRadius = new CornerRadius(7),
            Width = 230,
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

    public void Update(UIState state)
    {
        state.Apply(this);
    }

    public void Dispose()
    {
        _trayIcon.Dispose();
    }
}
