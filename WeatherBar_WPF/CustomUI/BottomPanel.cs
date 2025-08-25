using Localization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WeatherBar_WPF.CustomUI;

internal class BottomPanel : UserControl
{
    private readonly Grid _layout;

    private readonly Button _settingsButton;
    private readonly Button _exitButton;

    public Button SettingsButton => _settingsButton;
    public Button ExitButton => _exitButton;

    public BottomPanel(ILocalizationData localization,
                       FontFamily fontFamily,
                       double fontSize,
                       Brush foreground,
                       RoutedEventHandler ExitButtonClick,
                       RoutedEventHandler SettingButtonClick)
    {
        _exitButton = new Button
        {
            FontFamily = fontFamily,
            FontSize = fontSize,
            Foreground = foreground,
            Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0)),
            BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255)),
            Margin = new Thickness(15, 0, 15, 0),            
            Content = localization.Exit,               
        };
        _exitButton.Click += ExitButtonClick;
     
        _settingsButton = new Button()
        {
            FontFamily = fontFamily,
            FontSize = fontSize,
            Foreground = foreground,
            Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0)),
            BorderBrush = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255)),            
            Content = (localization as LanguageLocalization)?.Language,                
        };    
        _settingsButton.Click += SettingButtonClick;

        _layout = new Grid()
        {
            Children = { _exitButton, _settingsButton },
            Margin = new Thickness(5),        
        };
        _layout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        _layout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(5, GridUnitType.Star) });
        _layout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        _layout.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });    

        Grid.SetColumn(_exitButton, 1);
        Grid.SetRow(_exitButton, 0);

        Grid.SetColumn(_settingsButton, 2);
        Grid.SetRow(_settingsButton, 0);  

        Content = _layout;
    }
}
