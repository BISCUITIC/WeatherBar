using API.Data;
using Localization;
using Localization.Localizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace WeatherBar_WPF;

internal class SettingsPanel: Popup
{
    private readonly Border _wrapper;    
    private readonly StackPanel  _layout;

    public event Action<string> ChangeLanguage;

    public SettingsPanel()
    {
        _layout = new StackPanel();

        foreach(string lan in Languages.Data)
        {
            Button button = new Button() { Content = lan};
            button.Click += (s, e) => { ChangeLanguage.Invoke(lan); };
            _layout.Children.Add(button);
        }
      
        _wrapper = new Border()
        {
            Background = System.Windows.Media.Brushes.White,
            BorderBrush = System.Windows.Media.Brushes.Gray,
            BorderThickness = new Thickness(1),
            Child = _layout
        };

        Placement = PlacementMode.RelativePoint;
        Width = 50;
        HorizontalOffset = -Width;        
        StaysOpen = false;

        Child = _wrapper;       
    }
}
