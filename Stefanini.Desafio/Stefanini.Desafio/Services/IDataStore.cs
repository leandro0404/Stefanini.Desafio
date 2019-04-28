using Stefanini.Desafio.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stefanini.Desafio.Services
{
    public interface IDataStore<T>
    {
        void AddWeatherData(WeatherFavorites weathe);

        List<WeatherFavorites> GetWeatherDataFavorites();
    }
}
