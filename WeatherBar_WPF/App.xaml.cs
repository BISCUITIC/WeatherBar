using API;
using API.Data;
using ConfigHandler;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WeatherBar_WPF.CustomUI;
using WeatherBar_WPF.UIStates;
using WeatherIconGenerator;
using static System.Windows.Forms.AxHost;

namespace WeatherBar_WPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private UIComponents _UI;
    private RequestHandler _requestHandler;
    private ICityInputHandler _cityInputHandler;

    public void Start(object sender, StartupEventArgs e)
    {
        _UI = new UIComponents(OnExitButton_Click, OnCityInputKey_Press);
        _requestHandler = new RequestHandler();
        _cityInputHandler = new CityConfigHandler();
        _cityInputHandler.OnCityChange +=  async () => { await SearchForNewCity(); };

        Task.Run(UpdateLoop);
    }

    private async Task UpdateLoop()
    {
        while (true)
        {
            await UpdateData(_cityInputHandler.GetLastCity());
            await Task.Delay(TimeSpan.FromMinutes(5));            
        }
    }

    private async Task UpdateUI(UIState state)
    {
        Current.Dispatcher.Invoke(() =>
        {            
            _UI.Update(state);
        });
    }
    //private async Task UpdateUI_LocationError()
    //{
    //    Current.Dispatcher.Invoke(() =>
    //    {
    //        _UI.UpdateComponents(weatherData, location);
    //    });
    //}
    //private async Task UpdateUI_WeatherError()
    //{
    //    Current.Dispatcher.Invoke(() =>
    //    {
    //        _UI.UpdateComponents(weatherData, location);
    //    });
    //}
    private async Task UpdateData(string city)
    {
        UIState state;
        LocationResponse? locationResponse = await _requestHandler.TryGetLocationResponse(city);

        if (locationResponse is not null)
        {
            ILocationDataProvider locationData = new LocationData(locationResponse);
            WeatherResponse? weatherResponse = await _requestHandler.TryGetWeatherResponse(locationData.Latitude, locationData.Longitude);
            if (weatherResponse is not null)
            {
                IWeatherDataProvider weatherData = new WeatherData(weatherResponse);
                state = new SuccesfullUpdate(weatherData, locationData, new IconGenerator());
            }
            else
            {
                state = new WeatherErrorUpdate(locationData);
            }
        }
        else
        {
            state = new LocationErrorUpdate(city);
        }


        await UpdateUI(state);
    }

    private async Task SearchForNewCity()
    {
        await UpdateData(_cityInputHandler.GetLastCity());
    }

    private void OnExitButton_Click(object sender, RoutedEventArgs e)
    {
        Exit();
    }
    private void OnCityInputKey_Press(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {            
            CustomTextBox textBox = (sender as CustomTextBox) ?? throw new Exception("Объект должен быть типа TextBox");

            _cityInputHandler.SaveLastCity(textBox.Text);

            //await UpdateDate();
        }
    }

    private void Exit()
    {
        _UI.Dispose();
        Shutdown();
    }
}