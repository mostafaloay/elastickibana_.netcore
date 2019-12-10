using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using elastic.Controllers;
using Nest;

namespace elastic
{
    public interface IElastic : IDisposable
    {
        IndexResponse Add();
        List<WeatherForecastController.User> Search();
        UpdateResponse<WeatherForecastController.User> Update();
        DeleteResponse Delete();
    }
}