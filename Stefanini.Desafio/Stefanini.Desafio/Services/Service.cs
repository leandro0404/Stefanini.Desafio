using Newtonsoft.Json;
using Stefanini.Desafio.Models;
using Stefanini.Desafio.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Stefanini.Desafio.Services
{
    public class Service
    {
        HttpClient _client;
        public static string OpenWeatherMapEndpoint = "https://api.openweathermap.org/data/2.5/weather";
        public static string OpenWeatherMapAPIKey = "2bac87e0cb16557bff7d4ebcbaa89d2f";
        public List<WeatherData> weatherDataFromJson;
        string fileName = "Stefanini.Desafio.city.list.json";
        private readonly List<string> _data = new List<string>();


        public Service()
        {
            _client = new HttpClient();
            ReadInJsonFile();
        }

        public async Task<WeatherData> GetWeatherData(string city)
        {
            string requestUri = OpenWeatherMapEndpoint;
            requestUri += $"?q=" + city;
            requestUri += "&units=imperial"; // or units=metric
            requestUri += $"&APPID=" + OpenWeatherMapAPIKey;

            WeatherData weatherData = null;
            try
            {
                var response = await _client.GetAsync(requestUri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    weatherData = JsonConvert.DeserializeObject<WeatherData>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\t\tERROR {0}", ex.Message);
            }

            return weatherData;
        }

        public   WeatherData GetWeatherDataSync(string city)
        {
            string requestUri = OpenWeatherMapEndpoint;
            requestUri += $"?q=" + city;
            requestUri += "&units=imperial"; // or units=metric
            requestUri += $"&APPID=" + OpenWeatherMapAPIKey;

            WeatherData weatherData = null;
            try
            {
                var response =  _client.GetAsync(requestUri).Result;
                if (response.IsSuccessStatusCode)
                {
                    var content =  response.Content.ReadAsStringAsync().Result;
                    weatherData = JsonConvert.DeserializeObject<WeatherData>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\t\tERROR {0}", ex.Message);
            }

            return weatherData;
        }

        public List<WeatherData> ReadInJsonFile()
        {
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream(fileName);

            using (var reader = new StreamReader(stream))
            {
                var jsonAsString = reader.ReadToEnd();
                weatherDataFromJson = JsonConvert.DeserializeObject<List<WeatherData>>(jsonAsString);
            }
            return weatherDataFromJson;
        }


        public async Task<List<WeatherData>> GetItemsAsync(int pageIndex, int pageSize)
        {
            return weatherDataFromJson.Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }

    }
}
