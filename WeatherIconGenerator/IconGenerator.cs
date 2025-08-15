using System.Drawing;

namespace WeatherIconGenerator;

public class IconGenerator : IIconProvider
{
    private const int _width = 16;
    private const int _height = 16;

    private readonly Bitmap _bitmap;
    private readonly Graphics _graphics;

    private readonly Font _numbersFont;
    private readonly Font _symbolsFont;
    private readonly Brush _brush;

    private string _valueToDraw;
    private bool _isValueNegative;

    public IconGenerator()
    {
        _numbersFont = new Font("Arial", 10);
        _symbolsFont = new Font("Arial", 9);
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
            DrawSymbols(new PointF(9, -3), new PointF(-2, -7), new PointF(-3, 1));
        }
        else
        {
            DrawSymbols(new PointF(9, -3), new PointF(-2, 1), new PointF(3, 1));
        }

        return _bitmap;
    }

    private bool IsTwoDigit(int value) => Math.Abs(value) > 10;
    private bool IsMoreTwoDigit(int value) => Math.Abs(value) > 99;
    private void DrawSymbols(PointF сelsiusSymbolPosition, PointF minusSymbolPosition, PointF valuePosition)
    {
        if (_isValueNegative) _graphics.DrawString("-", _symbolsFont, _brush, minusSymbolPosition);
        _graphics.DrawString("°", _symbolsFont, _brush, сelsiusSymbolPosition);
        _graphics.DrawString(_valueToDraw, _numbersFont, _brush, valuePosition);
    }

    public void Dispose()
    {
        _bitmap?.Dispose();
        _graphics?.Dispose();
    }
}
