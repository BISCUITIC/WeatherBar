using System.Data;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;

namespace API;

public class RequestHandler: ILocationResponseProvider, IWeatherResponseProvider
{
    private const string LANGUAGE = "ru";
    private const string WEATHER_METRIC = "metric";
    private const int LOCATION_LIMIT = 1;

    private const string API_KEY = "aaf113d0f1c1a70d48b069d99485c79f";
    private const string WEATHER_URL = "https://api.openweathermap.org/data/2.5/weather?";
    private const string LOCATION_URL = "http://api.openweathermap.org/geo/1.0/direct?";

    private readonly HttpClient _client;
    private StringBuilder _requestUrlBuilder;

    public RequestHandler()
    {
        _client = new HttpClient();
        _requestUrlBuilder = new StringBuilder();
    }

    public async Task<LocationResponse?> TryGetLocationResponse(string city)
    {
        try
        {
            _requestUrlBuilder.Clear();
            _requestUrlBuilder.AppendJoin("", LOCATION_URL,"q=", city, "&limit=", LOCATION_LIMIT, "&appid=", API_KEY);

            string content = await GetResponse(_requestUrlBuilder.ToString());
            //string content = "[{\"name\":\"Homyel\",\"local_names\":{\"es\":\"Gómel\",\"el\":\"Γόμελ\",\"ka\":\"გომელი\",\"ja\":\"ホメリ\",\"sk\":\"Homeľ\",\"eo\":\"Homel\",\"feature_name\":\"Homieĺ\",\"be\":\"Гомель\",\"vo\":\"Homyel\",\"sr\":\"Гомељ\",\"th\":\"กอเมล\",\"de\":\"Homel\",\"ru\":\"Гомель\",\"fr\":\"Homiel\",\"ascii\":\"Homieĺ\",\"ta\":\"கோமெல்\",\"lt\":\"Gomelis\",\"bg\":\"Гомел\",\"ko\":\"호몔\",\"uk\":\"Гомєль\",\"no\":\"Homjel\",\"fa\":\"گومل\",\"zh\":\"戈梅利\",\"en\":\"Homyel\",\"et\":\"Gomel\",\"tr\":\"Gomel\",\"hu\":\"Homel\",\"ar\":\"غوميل\",\"pl\":\"Homel\",\"lv\":\"Homjeļa\",\"he\":\"הומל\",\"nl\":\"Homel\",\"ur\":\"گومل\",\"sg\":\"戈梅利\",\"tw\":\"戈梅利\"},\"lat\":52.4238936,\"lon\":31.0131698,\"country\":\"BY\",\"state\":\"Homyel Region\"}]";

            LocationResponse location = JsonSerializer.Deserialize<List<LocationResponse>>(content)?[0] ??
                                        throw new NullReferenceException("Не удалось десериализовать JSON");

            return location;
        }
        catch
        {
            //logger
            return null;
        }
    }

    public async Task<WeatherResponse?> TryGetWeatherResponse(double latitude, double longitude)
    {
        try
        {
            _requestUrlBuilder.Clear();
            _requestUrlBuilder.AppendJoin("", WEATHER_URL,"lat=", latitude, "&lon=", longitude,
                                          "&units=", WEATHER_METRIC, "&lang=", LANGUAGE, "&appid=", API_KEY);

            string content = await GetResponse(_requestUrlBuilder.ToString());
            //string content = "{\"coord\":{\"lon\":31.0132,\"lat\":52.4239},\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"ясно\",\"icon\":\"01n\"}],\"base\":\"stations\",\"main\":{\"temp\":16.98,\"feels_like\":17.03,\"temp_min\":14.9,\"temp_max\":16.98,\"pressure\":1013,\"humidity\":88,\"sea_level\":1013,\"grnd_level\":998},\"visibility\":7000,\"wind\":{\"speed\":3,\"deg\":240},\"clouds\":{\"all\":3},\"dt\":1754087577,\"sys\":{\"type\":1,\"id\":8933,\"country\":\"BY\",\"sunrise\":1754101054,\"sunset\":1754156805},\"timezone\":10800,\"id\":619762,\"name\":\"Якубовка\",\"cod\":200}\r\n";

            WeatherResponse weatherResponse = JsonSerializer.Deserialize<WeatherResponse>(content) ??
                                             throw new NullReferenceException("Не удалось десериализовать JSON");

            return weatherResponse;
        }
        catch
        {            
            //logger
            return null; 
        }
    }


    private async Task<string> GetResponse(string url)
    {
        using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
        using HttpResponseMessage response = await _client.SendAsync(request);

        string content = await response.Content.ReadAsStringAsync();

        return content;
    }
}
