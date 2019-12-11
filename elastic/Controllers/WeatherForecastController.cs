using System;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult<string> Get()
        {
            //var response = _elastic.Add();
            return Ok();
        }
        public class User
        {
            public Guid ItemId { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
        }
    }
}
