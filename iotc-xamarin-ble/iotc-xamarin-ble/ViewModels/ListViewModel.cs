using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using iotc_xamarin_ble.ViewModels.Navigation;
using Xamarin.Forms;

namespace iotc_xamarin_ble.ViewModels
{
    public abstract class ListViewModel<T> : BaseViewModel
    {
        private T _lastTappedItem;
        public ListViewModel(INavigationService navigation) : base(navigation)
        {
            ItemTappedCommand = new Command(OnItemTapped);
            Items = new ObservableCollection<T>();
        }
        public ObservableCollection<T> Items { get; set; }

        public abstract Task<IEnumerable<T>> FetchData();
        public ICommand ItemTappedCommand { get; private set; }

        public bool IsBusy { get; set; }

        public override Type ModelType { get => typeof(ListViewModel<T>); }
        public T LastTappedItem
        {
            get { return _lastTappedItem; }
            set
            {
                _lastTappedItem = value;
                OnItemTapped();
            }
        }

        public abstract void OnItemTapped();

        public async Task LoadData()
        {
            IsBusy = true;
            OnPropertyChanged("IsBusy");
            Items.Clear();
            Items = new ObservableCollection<T>(await FetchData());
            OnPropertyChanged("Items");
            IsBusy = false;
            OnPropertyChanged("IsBusy");
        }
        public override Task OnAppearing()
        {
            return LoadData();
        }

    }
}
