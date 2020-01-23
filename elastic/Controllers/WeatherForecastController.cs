using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nest;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace elastic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IElastic _elastic;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IElastic elastic)
        {
            _logger = logger;
            _elastic = elastic;
        }

        [HttpGet]
        public async Task<ActionResult<string>> Get()
        {
            //var response = _elastic.Add();
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "APM-Sample-App");
            //This HTTP request is automatically captured by the Elastic APM .NET Agent:
            var responseMsg = await httpClient
                .GetAsync("https://api.github.com/repos/elastic/apm-agent-dotnet");
            var responseStr = await responseMsg.Content.ReadAsStringAsync();
            return Ok(responseStr);
        }
        public class User
        {
            public Guid ItemId { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
        }
    }
}
