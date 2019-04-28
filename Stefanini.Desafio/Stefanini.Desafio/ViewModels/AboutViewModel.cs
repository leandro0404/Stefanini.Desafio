using Stefanini.Desafio.Models;
using Stefanini.Desafio.Services;
using Stefanini.Desafio.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Extended;

namespace Stefanini.Desafio.ViewModels
{
    public class AboutViewModel : INotifyPropertyChanged
    {

        private bool _isBusy;
        private const int PageSize = 10;
        readonly DataStore _dataService = new DataStore();
        readonly Service _Service = new Service();

        public ObservableCollection<WeatherData> Items { get; }
        public Command LoadItemsCommand { get; set; }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        public AboutViewModel()
        {

            Items = new ObservableCollection<WeatherData>();
            var list = _dataService.GetWeatherDataFavorites();

            foreach (var item in list)
            {
               var t =  _Service.GetWeatherDataSync(item.Title);
                Items.Add(t);
            }

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<WeatherDetailPage, WeatherFavorites>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as WeatherFavorites;

                if (!Items.Any(n => n.Title == newItem.Title))
                 
                {
                    var t = _Service.GetWeatherDataSync(item.Title);
                    Items.Add(t);
                    _dataService.AddWeatherData(newItem);
                }
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = _dataService.GetWeatherDataFavorites();
                foreach (var item in items)
                {
                    var t = _Service.GetWeatherDataSync(item.Title);
                    Items.Add(t);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}