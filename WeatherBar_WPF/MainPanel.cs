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

internal class MainPanel : IDisposable
{
    private readonly ILocalizationData _localization;

    private readonly TaskbarIcon _trayIcon;

    private readonly Border _wrapper;
    private readonly StackPanel _layout;

    private readonly CustomTextBox _cityInput;
    private readonly DataPanel _weatherDataPanel;
    private readonly BottomPanel _bottomPanel;    

    public CustomTextBox CityInput => _cityInput;
    public DataPanel WeatherDataPanel => _weatherDataPanel;
    public TaskbarIcon TrayIcon => _trayIcon;     
    public StackPanel Layout => _layout;

    public MainPanel(ILocalizationData languageLocalization, 
                        RoutedEventHandler ExitButtonClick,
                        RoutedEventHandler SettingButtonClick,
                        KeyEventHandler CityInputKeyPress)
    {
        _localization = languageLocalization;

        FontFamily fontFamily = new FontFamily("Segoe UI");        
        Brush foreground = new SolidColorBrush(Colors.White);

        _cityInput = new CustomTextBox()
        {
            FontFamily = fontFamily,
            FontSize = 16,
            Foreground = foreground,
            Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0)),
            BorderBrush = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0)),
            HorizontalAlignment = HorizontalAlignment.Stretch,
        };        
        _cityInput.KeyDown += CityInputKeyPress;

        _weatherDataPanel = new DataPanel(_localization, fontFamily, 16, foreground);

        _bottomPanel = new BottomPanel(_localization, fontFamily, 12, foreground,  ExitButtonClick, SettingButtonClick);       

        _layout = new StackPanel()
        {
            Margin = new Thickness(5),
            Children = { _cityInput, _weatherDataPanel, _bottomPanel },
        };

        _wrapper = new Border
        {
            Background = new SolidColorBrush(Color.FromArgb(235, 44, 44, 44)),
            BorderBrush = new SolidColorBrush(Color.FromArgb(0, 28, 28, 28)),
            CornerRadius = new CornerRadius(7),
            Width = 230,
            Child = _layout,
        };        

        _trayIcon = new TaskbarIcon
        {
            Icon = System.Drawing.SystemIcons.Information,
            TrayPopup = _wrapper,
            PopupActivation = PopupActivationMode.LeftOrRightClick,
            ToolTipText = "Weather Bar",
        };
    }

    public void Update(UIState state)
    {
        state.Apply(this);
    }

    public void UpdateLocalization(ILocalizationData localization)
    {
        _weatherDataPanel.UpdateLocalization(localization);
        _bottomPanel.UpdateLocalization(localization);
    }

    public void Dispose()
    {
        _trayIcon.Dispose();
    }
}
