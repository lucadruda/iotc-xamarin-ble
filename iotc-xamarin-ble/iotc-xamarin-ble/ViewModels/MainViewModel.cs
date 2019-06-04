using DLToolkit.Forms.Controls;
using iotc_xamarin_ble.Services;
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
            Title = "Azure IoT Central";
            //LoginCommand = new Command(Fetch);
            OnApplicationTapped = new Command(() =>
              {
                  Application tappedApp = LastTappedItem as Application;
                  if (tappedApp != null)
                  {
                      IoTCentral.Current.Application = tappedApp;
                      Navigation.NavigateTo(new ModelsViewModel(Navigation));
                  }
              });
            Applications.Clear();

        }

        public FlowObservableCollection<Application> Applications { get; set; } = new FlowObservableCollection<Application>();

        public ICommand LoginCommand { get; private set; }

        public ICommand OnApplicationTapped { get; private set; }

        public object LastTappedItem { get; set; }

        public override async Task OnAppearing()
        {
            if (IoTCentral.Current.ServiceClient == null)
            {
                var authViewModel = new AuthViewModel(Navigation);
                authViewModel.TokenAcquired += OnTokenAvailable;
                await Navigation.NavigateTo(authViewModel);
            }
            else { await Fetch(); }
        }

        private async Task Fetch()
        {
            Applications.AddRange(await IoTCentral.Current.ServiceClient.ListApps());
            OnPropertyChanged("Applications");
        }
        private void OnTokenAvailable(object source, string accessToken)
        {
            IoTCentral.Current.InitServiceClient(accessToken);
            Fetch();
        }
    }
}
