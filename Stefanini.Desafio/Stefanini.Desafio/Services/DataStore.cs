
using System.Threading.Tasks;

using System.Collections.Generic;
using Xamarin.Forms;

using System.Linq;
using Stefanini.Desafio.Models;
using System;
using SQLite;
using System.IO;

namespace Stefanini.Desafio.Services
{
    public class DataStore : IDataStore<WeatherFavorites>
    {

        List<WeatherFavorites> items;

        public DataStore()
        {
            items = new List<WeatherFavorites>();

        }

        public void AddWeatherData(WeatherFavorites weathe)
        {
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.DB_PATH))
            {
                conn.CreateTable<WeatherFavorites>();
                var numberOfRows = conn.Insert(weathe);
            }

        }

        public List<WeatherFavorites> GetWeatherDataFavorites()
        {
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.DB_PATH))
            {
                conn.CreateTable<WeatherFavorites>();

                return conn.Table<WeatherFavorites>().GroupBy(p => p.Title).Select(g => g.First()).ToList();
            }
        }

    }
}