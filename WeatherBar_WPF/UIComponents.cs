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
    private LanguageLocalization _languageLocalization;

    private TaskbarIcon _trayIcon;

    private Border _trayLayout;
    private StackPanel _mainPanel;

    private CustomTextBox _cityInput;
    private DataPanel _weatherDataPanel;
    private Button _exitButton;   

    public TaskbarIcon TrayIcon => _trayIcon;

    public CustomTextBox CityInput => _cityInput;

    public DataPanel WeatherDataPanel => _weatherDataPanel;

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
        
        _weatherDataPanel = new DataPanel(_languageLocalization, fontFamily, fontSize, foreground);

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
            Content = _languageLocalization.Exit,
        };

        _mainPanel = new StackPanel()
        {
            Margin = new Thickness(5),
            Children = { _cityInput, _weatherDataPanel, _exitButton },
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
