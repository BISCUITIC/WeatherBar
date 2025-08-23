using Localization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WeatherBar_WPF.CustomUI;

internal class BottomPanel : UserControl
{
    private readonly StackPanel _layout;

    private readonly Button _settingButton;
    private readonly Button _exitButton;

    public RoutedEventHandler ExitButtonClick { get; set; }

    public BottomPanel(ILocalizationData localization,
                       FontFamily fontFamily,
                       double fontSize,
                       Brush foreground)
    {
        _settingButton = new Button()
        {
            FontFamily = fontFamily,
            FontSize = 12,
            Foreground = foreground,
            Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0)),
            BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255)),
            Width = 50,
            Padding = new Thickness(2),
            Margin = new Thickness(0, 7, 0, 3),
            Content = "Settings",
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
            Content = localization.Exit,               
        };

        _exitButton.Click += ExitButtonClick;

        _layout = new StackPanel()
        {
            Margin = new Thickness(5),
            Orientation = Orientation.Horizontal,
            Children = { _exitButton, _settingButton},
        };

        Content = _layout;
    }
}
