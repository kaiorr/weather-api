using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using weather_api.Application;

namespace weather_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private string[] sumaries;
        public WeatherForecastController(ILogger<WeatherForecastController> logger,
        IWeatherSummary weatherSummary )
        {
            _logger = logger;
            sumaries = weatherSummary.getSummaries();
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = sumaries[rng.Next(sumaries.Length)]
            })
            .ToArray();
        }
    }
}
