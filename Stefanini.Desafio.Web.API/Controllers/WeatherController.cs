using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Stefanini.Desafio.Core;
using Stefanini.Desafio.Core.Dominio;

namespace Stefanini.Desafio.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        HttpClient _client;

        public WeatherController()
        {
            _client = new HttpClient();
        }


        public async Task<WeatherData> GetWeatherData(string search)
        {
            WeatherData weatherData = null;
            try
            {
                var response = await _client.GetAsync(GenerateRequestUri(search));
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    weatherData = JsonConvert.DeserializeObject<WeatherData>(content);
                }
            }
            catch (Exception ex)
            {
                //Debug.WriteLine("\t\tERROR {0}", ex.Message);
            }

            return weatherData;
        }


        string GenerateRequestUri(string search)
        {
            string requestUri = Constants.OpenWeatherMapEndpoint;
            requestUri += $"?q=" + search;
            requestUri += "&units=imperial"; // or units=metric
            requestUri += $"&APPID=" + Constants.OpenWeatherMapAPIKey;
            return requestUri;
        }
    }

}
