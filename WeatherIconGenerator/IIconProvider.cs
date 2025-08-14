using System.Drawing;

namespace WeatherIconGenerator;

public interface IIconProvider: IDisposable
{
    Bitmap GetIconBitmap(int value);
}
