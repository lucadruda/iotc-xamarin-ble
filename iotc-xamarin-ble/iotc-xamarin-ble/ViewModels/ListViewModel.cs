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
        private bool addingAvailable;

        public ListViewModel(INavigationService navigation) : base(navigation)
        {
            ItemTappedCommand = new Command(OnItemTapped);
            Add = new Command(AddItem);
            AddingAvailable = false;
            Items = new ObservableCollection<T>();
        }
        public ObservableCollection<T> Items { get; set; }

        public abstract Task<IEnumerable<T>> FetchData();
        public ICommand ItemTappedCommand { get; private set; }
        public ICommand Add { get; private set; }
        public bool AddingAvailable
        {
            get => addingAvailable; protected set { addingAvailable = value; OnPropertyChanged(); }
        }


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
        public virtual void AddItem()
        {
            OnPropertyChanged("Items");
        }

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
