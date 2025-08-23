using API;
using API.Data;
using ConfigHandler;
using Localization;
using Localization.Localizations;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using WeatherBar_WPF.CustomUI;
using WeatherBar_WPF.UIStates;
using WeatherIconGenerator;
using WinForm = System.Windows.Forms;

namespace WeatherBar_WPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private MainPanel _mainPanel;
    private UIState _UIstate;

    private SettingsPanel _settingsPanel;

    private RequestHandler _requestHandler;
    private ICityInputHandler _cityInputHandler;

    private LanguageLocalization _localization;
    private ILocalizationProvider _localizator;

    public void Start(object sender, StartupEventArgs e)
    {
        _localizator = new LocalizationConfigHandler();
        _localization = new RuLocalization(_localizator);
        
        _mainPanel = new MainPanel(_localization, OnExitButton_Click, OnSettingButton_Click, OnCityInputKey_Press);
        _settingsPanel = new SettingsPanel()
        {
            PlacementTarget = _mainPanel.Layout,
        };
        _settingsPanel.ChangeLanguage += OnChangeLanguage;

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
            _mainPanel.Update(state);
        });
    }

    private async Task UpdateData(string city)
    {
        LocationResponse? locationResponse = await _requestHandler.TryGetLocationResponse(city);

        if (locationResponse is not null)
        {
            ILocationDataProvider locationData = new LocationData(locationResponse);
            WeatherResponse? weatherResponse = await _requestHandler.TryGetWeatherResponse(locationData.Latitude, 
                                                                                           locationData.Longitude,
                                                                                           _localization.Language);
            if (weatherResponse is not null)
            {
                IWeatherDataProvider weatherData = new WeatherData(weatherResponse);
                _UIstate = new Succesfull(weatherData, locationData, new IconGenerator(WinForm.SystemInformation.SmallIconSize.Width));
            }
            else
            {
                _UIstate = new WeatherError(locationData);
            }
        }
        else
        {
            _UIstate = new LocationError(city);
        }

        await UpdateUI(_UIstate);
    }

    private async Task SearchForNewCity()
    {
        await UpdateData(_cityInputHandler.GetLastCity());
    }

    private void OnExitButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
    private void OnSettingButton_Click(object sender, RoutedEventArgs e)
    {
        //var popup = new Popup
        //{            
        //    Placement = PlacementMode.Relative,
        //    PlacementTarget = _UI.TrayLayout,
        //    //HorizontalOffset = SystemParameters.WorkArea.Width - 50,
        //    HorizontalOffset = - 50,
        //    VerticalOffset = 0,
        //    Width = 50,            
        //    StaysOpen = false,
        //    Child = new Border
        //    {
        //         Background = System.Windows.Media.Brushes.White,
        //         BorderBrush = System.Windows.Media.Brushes.Gray,
        //         BorderThickness = new Thickness(1),
        //         Child = new StackPanel
        //         {
        //             Children =
        //                {                            
        //                    new Button { Content = "en" },
        //                    new Button { Content = "ru" },
        //                    new Button { Content = "fr" }
        //                }
        //         }
        //     }
        // };        
        //popup.IsOpen = true;
        _settingsPanel.IsOpen = true;
    }
    private void OnCityInputKey_Press(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            CustomTextBox textBox = (sender as CustomTextBox) ?? throw new Exception("Объект должен быть типа TextBox");

            _cityInputHandler.SaveLastCity(textBox.Text);
        }
    }
    private void OnChangeLanguage(string language)
    {
        _localization = new LanguageLocalization(language, _localizator);
        _mainPanel.UpdateLocalization(_localization);
    }

    private void Close()
    {
        _mainPanel.Dispose();
        Shutdown();
    }
}