using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Stefanini.Desafio.Models;
using Stefanini.Desafio.Views;
using Stefanini.Desafio.ViewModels;
using System.Reflection;
using System.IO;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using Stefanini.Desafio.Services;

namespace Stefanini.Desafio.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class WeatherPage : ContentPage
    {
        public List<WeatherData> weatherDataFromJson;
        Service service;

        public WeatherPage()
        {
            InitializeComponent();
          
            service = new Service();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as WeatherData;
            if (item == null)
                return;

            WeatherData weatherData = await service.GetWeatherData(item.Title);

            await Navigation.PushAsync(new WeatherDetailPage(new WeatherDetailViewModel(weatherData)));

            // Manually deselect item.
            ListViewWithCustomCells.SelectedItem = null;
        }


    }

    
    
}