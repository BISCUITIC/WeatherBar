using Hardcodet.Wpf.TaskbarNotification;
using Localization;
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

    private TaskbarIcon _trayIcon;

    private Border _trayLayout;
    private StackPanel _mainPanel;

    private CustomTextBox _cityInput;
    private DataPanel _weatherDataPanel;
    private BottomPanel _bottomPanel;
    
    public CustomTextBox CityInput => _cityInput;
    public DataPanel WeatherDataPanel => _weatherDataPanel;
    public TaskbarIcon TrayIcon => _trayIcon;

    public UIComponents(ILocalizationData languageLocalization, RoutedEventHandler exit, KeyEventHandler cityKeyPressHandler)
    {
        _localization = languageLocalization;
        InitComponents(exit);        
        _cityInput.KeyDown += cityKeyPressHandler;
    }

    private void InitComponents(RoutedEventHandler exit)
    {
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

        _weatherDataPanel = new DataPanel(_localization, fontFamily, fontSize, foreground);

        _bottomPanel = new BottomPanel(_localization, fontFamily, fontSize, foreground, exit);       

        _mainPanel = new StackPanel()
        {
            Margin = new Thickness(5),
            Children = { _cityInput, _weatherDataPanel, _bottomPanel},
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
