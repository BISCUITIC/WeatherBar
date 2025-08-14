using System.CodeDom;
using System.Text;
using System.Windows.Controls;

namespace WeatherBar_WPF;

internal class DataLabel : Label
{
    private string _data;
    private readonly string? _prefix;
    private readonly string? _postfix;

    private readonly StringBuilder _contentBuilder;

    public string Data { get => _data; set => ChangeData(value); }

    public DataLabel(string data, string? prefix, string? postfix) : base()
    {
        _prefix = prefix;
        _postfix = postfix;
        _contentBuilder = new StringBuilder();

        Data = data;
    }

    private void ChangeData(string value)
    {
        _data = value;

        _contentBuilder.Clear();
        _contentBuilder.Append(_prefix).Append(_data).Append(_postfix);
        Content = _contentBuilder.ToString();
    }
}
