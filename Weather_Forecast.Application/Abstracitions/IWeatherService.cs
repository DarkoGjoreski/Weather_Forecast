using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather_Forecast.Application.Abstracitions
{
    public interface IWeatherService
    {
        Task<string> SearchByCity(string city);
    }
}
