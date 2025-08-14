//using System.Drawing;
using System.Windows;
using API;
using API.Data;

namespace WeatherBar_WPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private UIComponents _UI;
    private RequestHandler _requestHandler;

    public void Start(object sender, StartupEventArgs e)
    {
        _UI = new UIComponents(OnExitButton_Click);
        _requestHandler = new RequestHandler();

        Task.Run(UpdateLoop);
    }


    private async Task UpdateLoop()
    {
        LocationData locationData;
        WeatherData weatherData;
        while (true)
        {
            LocationResponse? locationResponse = await _requestHandler.TryGetLocationResponse("gomel");
            if (locationResponse is not null)
            {
                locationData = new LocationData(locationResponse);
                WeatherResponse? weatherResponse = await _requestHandler.TryGetWeatherResponse(locationData.Latitude, locationData.Longitude);
                if (weatherResponse is not null)
                {
                    weatherData = new WeatherData(weatherResponse);
                    await UpdateUI(weatherData);
                }
            }                       
            await Task.Delay(TimeSpan.FromMinutes(5));            
        }
    }

    private async Task UpdateUI(IWeatherDataProvider weatherData)
    {

        Current.Dispatcher.Invoke(() =>
        {
            _UI.UpdateComponents(weatherData);
        });
    }
    //private async Task UpdateData(ILocationProvider locationProvider)
    //{            

    //}

    private void OnExitButton_Click(object sender, RoutedEventArgs e)
    {
        Exit();
    }

    private void Exit()
    {
        _UI.Dispose();
        Shutdown();
    }
}