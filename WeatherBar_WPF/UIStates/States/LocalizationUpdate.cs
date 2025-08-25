using API.Data;
using Localization;
using System.Windows;
using WeatherBar_WPF.CustomUI;
using WeatherIconGenerator;

namespace WeatherBar_WPF.UIStates.States;

internal class LocalizationUpdate: UIState
{
    private readonly ILocalizationData _localization;

    public LocalizationUpdate(ILocalizationData localization)
    {
        _localization = localization;
    }

    public override void Apply(MainPanel ui)
    {                
        ui.WeatherDataPanel.Temperature.Prefix = $"{_localization.Temperature} : ";
        ui.WeatherDataPanel.Pressure.Prefix = $"{_localization.Pressure} : ";
        ui.WeatherDataPanel.Humidity.Prefix = $"{_localization.Humidity} : ";
        ui.WeatherDataPanel.WindSpeed.Prefix = $"{_localization.WindSpeed} : ";
        ui.WeatherDataPanel.Description.Prefix = $"{_localization.Description} : ";
        
        ui.BottomPanel.ExitButton.Content = _localization.Exit;
        ui.BottomPanel.SettingsButton.Content = (_localization as LanguageLocalization)?.Language;
    }
}
