using System;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Stefanini.Desafio.Models;
using Stefanini.Desafio.ViewModels;
using Stefanini.Desafio.Services;

namespace Stefanini.Desafio.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class WeatherDetailPage : ContentPage
    {
        WeatherDetailViewModel viewModel;

        public WeatherDetailPage(WeatherDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        async void Button_Clicked(object sender, EventArgs e)
        {
            WeatherData data = ((Button)sender).BindingContext as WeatherData;

            DataStore repository = new DataStore();
            repository.AddWeatherData(new WeatherFavorites { Title = viewModel.Item.Title });
            MessagingCenter.Send(this, "AddItem", new WeatherFavorites { Title = viewModel.Item.Title });

        }

    }
}