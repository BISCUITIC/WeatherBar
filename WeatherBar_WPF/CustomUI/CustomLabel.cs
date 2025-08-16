using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace WeatherBar_WPF.CustomUI;

internal class CustomLabel : Label
{
    private string _data;
    private readonly string? _prefix;
    private readonly string? _postfix;

    private readonly StringBuilder _contentBuilder;

    private readonly TextBlock _textBlock;

    public string Data { get => _data; set => ChangeData(value); }

    public CustomLabel(string data, string? prefix, string? postfix) : base()
    {
        _prefix = prefix;
        _postfix = postfix;
        _contentBuilder = new StringBuilder();

        _textBlock = new TextBlock() { TextWrapping = TextWrapping.Wrap };
        base.AddChild(_textBlock);

        Data = data;
    }

    private void ChangeData(string value)
    {
        _data = value;

        _contentBuilder.Clear();
        _contentBuilder.Append(_prefix).Append(_data).Append(_postfix);
        _textBlock.Text = _contentBuilder.ToString();
    }
}
