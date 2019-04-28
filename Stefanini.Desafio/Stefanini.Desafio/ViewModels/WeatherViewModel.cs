using Stefanini.Desafio.Models;
using Stefanini.Desafio.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms.Extended;

namespace Stefanini.Desafio.ViewModels
{

    public class WeatherViewModel : INotifyPropertyChanged
    {
        private bool _isBusy;
        private const int PageSize = 10;
        readonly Service _dataService = new Service();

        public InfiniteScrollCollection<WeatherData> Items { get; }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        public WeatherViewModel()
        {
            

            Items = new InfiniteScrollCollection<WeatherData>
            {
                OnLoadMore = async () =>
                {
                    IsBusy = true;

                    // load the next page
                    var page = Items.Count / PageSize;

                    var items = await _dataService.GetItemsAsync(page, PageSize);

                    IsBusy = false;

                    // return the items that need to be added
                    return items;
                },
                OnCanLoadMore = () =>
                {
                    return Items.Count < 44;
                }
            };

            DownloadDataAsync();
        }

        private async Task DownloadDataAsync()
        {
            var items = await _dataService.GetItemsAsync(pageIndex: 0, pageSize: PageSize);

            Items.AddRange(items);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
