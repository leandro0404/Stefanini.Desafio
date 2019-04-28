using Stefanini.Desafio.Models;
using Stefanini.Desafio.Services;
using Stefanini.Desafio.ViewModels;
using System;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Stefanini.Desafio.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class FavoritesPage : ContentPage
    {
       
        Service service;
        AboutViewModel viewModel;
        public FavoritesPage()
        {
            InitializeComponent();
            service = new Service();
            BindingContext = viewModel = new AboutViewModel();
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