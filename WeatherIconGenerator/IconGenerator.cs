using System.Drawing;

namespace WeatherIconGenerator;

public class IconGenerator : IIconProvider
{
    private const int _width = 32;
    private const int _height = 32;

    private readonly Bitmap _bitmap;
    private readonly Graphics _graphics;

    private readonly Font _celciusFont;
    private readonly Font _symbolsFont;
    private readonly Font _minusFont;
    private readonly Brush _brush;

    private string? _valueToDraw;
    private bool _isValueNegative;

    public IconGenerator()
    {
        _celciusFont = new Font("Arial", 12);
        _symbolsFont = new Font("Arial", 13);
        _minusFont = new Font("Arial", 15);
        _brush = Brushes.White;

        _bitmap = new Bitmap(_width, _height);
        _graphics = Graphics.FromImage(_bitmap);
        _graphics.Clear(Color.FromArgb(0, 0, 0, 0));
    }

    public Bitmap GetIconBitmap(int value)
    {
        _graphics.Clear(Color.FromArgb(0, 0, 0, 0));

        if (IsMoreTwoDigit(value)) throw new ArgumentException("Аргумент не может принимать значение больше 99");

        _isValueNegative = value < 0;
        _valueToDraw = Math.Abs(value).ToString();

        if (IsTwoDigit(value))
        {
            DrawSymbols(new PointF(15, -4), new PointF(-6, -17), new PointF(-6, 3));
        }
        else
        {
            DrawSymbols(new PointF(15, -4), new PointF(-6, 0), new PointF(6, 3));
        }

        return _bitmap;
    }

    private bool IsTwoDigit(int value) => Math.Abs(value) > 10;
    private bool IsMoreTwoDigit(int value) => Math.Abs(value) > 99;
    private void DrawSymbols(PointF сelsiusSymbolPosition, PointF minusSymbolPosition, PointF valuePosition)
    {
        if (_isValueNegative) _graphics.DrawString("-", _minusFont, _brush, minusSymbolPosition);
        _graphics.DrawString("°", _celciusFont, _brush, сelsiusSymbolPosition);
        _graphics.DrawString(_valueToDraw, _symbolsFont, _brush, valuePosition);
    }

    public void Dispose()
    {
        _bitmap?.Dispose();
        _graphics?.Dispose();
    }
}
