using API;
using API.Data;
using ConfigHandler;
using Localization;
using Localization.Localizations;
using WinForm = System.Windows.Forms;
using System.Windows;
using System.Windows.Input;
using WeatherBar_WPF.CustomUI;
using WeatherBar_WPF.UIStates;
using WeatherIconGenerator;

namespace WeatherBar_WPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private UIComponents _UI;
    private RequestHandler _requestHandler;
    private ICityInputHandler _cityInputHandler;

    private LocalizationConfigHandler _localizator;
    private LanguageLocalization _localization;

    public void Start(object sender, StartupEventArgs e)
    {
        _localizator = new LocalizationConfigHandler();
        _localization = new FrLocalization(_localizator);

        _UI = new UIComponents(_localization, OnExitButton_Click, OnCityInputKey_Press);

        _requestHandler = new RequestHandler();
        _cityInputHandler = new CityConfigHandler();

        _cityInputHandler.OnCityChange += async () => { await SearchForNewCity(); };

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
                state = new Succesfull(weatherData, locationData, new IconGenerator(WinForm.SystemInformation.SmallIconSize.Width));
            }
            else
            {
                state = new WeatherError(locationData);
            }
        }
        else
        {
            state = new LocationError(city);
        }


        await UpdateUI(state);
    }

    private async Task SearchForNewCity()
    {
        await UpdateData(_cityInputHandler.GetLastCity());
    }

    private void OnExitButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
    private void OnCityInputKey_Press(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            CustomTextBox textBox = (sender as CustomTextBox) ?? throw new Exception("Объект должен быть типа TextBox");

            _cityInputHandler.SaveLastCity(textBox.Text);
        }
    }

    private void Close()
    {
        _UI.Dispose();
        Shutdown();
    }
}