using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace BlazorApp1;

public class WeatherForecastService
{
    HttpClient client = new HttpClient();
    JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };

    Uri uri = new Uri("https://localhost:44313/WeatherForecast");

    public async Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
    {
        WeatherForecast[] result = null;

        try
        {
            HttpResponseMessage response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                result = JsonSerializer.Deserialize<WeatherForecast[]>(content, jsonSerializerOptions);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($@"\tERROR {ex.Message}");
        }

        return result;
    }

    public async Task PostForecastAsync(WeatherForecast forecast)
    {
        try
        {
            string json = JsonSerializer.Serialize(forecast, jsonSerializerOptions);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
                Debug.WriteLine(@"\WeatherForecast successfully saved.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\tERROR {ex.Message}");
        }
    }
}