using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Summary z API 1", "Summary z API 2", "Summary z API 3"
        };

        private static List<WeatherForecast> data = new List<WeatherForecast>
        {
            new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }
        };

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return data;
        }

        [HttpPost(Name = "PostWeatherForecast")]
        public WeatherForecast Post(WeatherForecast weatherForecast)
        {
            data.Add(weatherForecast);

            return weatherForecast;
        }
    }
}