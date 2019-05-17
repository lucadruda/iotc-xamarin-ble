using DLToolkit.Forms.Controls;
using iotc_xamarin_ble.ViewModels.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace iotc_xamarin_ble.ViewModels
{
    public class MainViewModel : BaseViewModel
    {


        public MainViewModel(INavigationService navigationService) : base(navigationService)
        {
            LoginCommand = new Command(Fetch);
            OnApplicationTapped = new Command(() =>
              {
                  Application tappedApp = LastTappedItem as Application;
                  if (tappedApp != null)
                  {
                      Navigation.NavigateTo(new ModelsViewModel(Navigation, tappedApp));
                  }
              });

            Fetch();
        }

        public FlowObservableCollection<Application> Applications { get; set; } = new FlowObservableCollection<Application>();

        public ICommand LoginCommand { get; private set; }

        public ICommand OnApplicationTapped { get; private set; }

        public object LastTappedItem { get; set; }


        private async void Fetch()
        {
            Applications.Clear();
            Applications.AddRange(await ((App)App.Current).IoTCentralClient.ListApps());
            OnPropertyChanged("Applications");
        }

    }
}
