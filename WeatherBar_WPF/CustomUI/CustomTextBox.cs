using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WeatherBar_WPF.CustomUI;

internal class CustomTextBox : UserControl
{
    private readonly Grid _wrapper;

    private readonly TextBox _value;
    private readonly TextBlock _state;
    private readonly TextBlock _lastUpdateTime;

    public string State { set => _state.Text = value; }
    public string LastUpdateTime { set => _lastUpdateTime.Text = value; }
    public string Text { set => _value.Text = value; get => _value.Text; }

    public CustomTextBox() : base()
    {
        _value = new TextBox()
        {
            FontFamily = new FontFamily("Segoe UI"),
            FontSize = 16,
            Foreground = new SolidColorBrush(Colors.White),
            Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0)),
            BorderBrush = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0)),
            Padding = new Thickness(0),
            HorizontalAlignment = HorizontalAlignment.Stretch,
        };

        _state = new TextBlock()
        {
            FontFamily = new FontFamily("Segoe UI"),
            FontSize = 10,
            Padding = new Thickness(2),
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Foreground = new SolidColorBrush(Color.FromArgb(180, 255, 255, 255)),
        };
        _lastUpdateTime = new TextBlock()
        {
            FontFamily = new FontFamily("Segoe UI"),
            FontSize = 12,
            Padding = new Thickness(2),
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Foreground = new SolidColorBrush(Color.FromArgb(180, 255, 255, 255)),
        };

        _wrapper = new Grid()
        {
            Children = { _state, _value, _lastUpdateTime },
            Margin = new Thickness(5),
        };
        _wrapper.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        _wrapper.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        _wrapper.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        _wrapper.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

        Grid.SetColumn(_state, 0);
        Grid.SetRow(_state, 0);
        Grid.SetColumnSpan(_state, 2);

        Grid.SetColumn(_value, 0);
        Grid.SetRow(_value, 1);

        Grid.SetColumn(_lastUpdateTime, 1);
        Grid.SetRow(_lastUpdateTime, 1);

        Content = _wrapper;
    }
}
