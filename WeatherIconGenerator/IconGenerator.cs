using System.Drawing;
using System.Windows.Forms;

namespace WeatherIconGenerator;

public class IconGenerator : IIconProvider
{
    private const int _defualtIconSize = 16;

    private readonly int _width;
    private readonly int _height;
    private readonly float _scale;

    private readonly Bitmap _bitmap;
    private readonly Graphics _graphics;

    private readonly Font _numbersFont;
    private readonly Font _symbolsFont;
    private readonly Brush _brush;

    private string? _valueToDraw;
    private bool _isValueNegative;

    public IconGenerator(int iconSize)
    {
        _scale = iconSize / _defualtIconSize;
        _width = iconSize;
        _height = iconSize;

        _numbersFont = new Font("Arial", 10 * _scale);
        _symbolsFont = new Font("Arial", 9 * _scale);
        _brush = Brushes.White;

        _bitmap = new Bitmap(_width, _height);
        _graphics = Graphics.FromImage(_bitmap);
        _graphics.Clear(Color.FromArgb(0, 0, 0, 0));
    }

    public Bitmap GetIconBitmap(int value)
    {
        if (IsMoreTwoDigit(value)) throw new ArgumentException("Аргумент не может принимать значение больше 99");

        _isValueNegative = value < 0;
        _valueToDraw = Math.Abs(value).ToString();
        _graphics.Clear(Color.FromArgb(0, 0, 0, 0));

        if (IsTwoDigit(value))
        {
            DrawSymbols(new PointF(9  * _scale, -3 * _scale), 
                        new PointF(-2 * _scale, -7 * _scale), 
                        new PointF(-3 * _scale,  1 * _scale));
        }
        else
        {
            DrawSymbols(new PointF(9  * _scale, -3 * _scale), 
                        new PointF(-2 * _scale, 1  * _scale), 
                        new PointF(3  * _scale, 1  * _scale));
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
