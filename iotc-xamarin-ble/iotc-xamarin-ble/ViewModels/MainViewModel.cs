using DLToolkit.Forms.Controls;
using iotc_ble_xamarin;
using iotc_csharp_service.Exceptions;
using iotc_xamarin_ble.Helpers;
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
            IsBusy = true;
            Add = new Command(CreateNewApplication);
            ReloadApplications = new Command(async () =>
            {
                IsBusy = true;
                await Fetch();
            });
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
        public ICommand Add { get; private set; }
        public ICommand ReloadApplications { get; private set; }

        public object LastTappedItem { get; set; }

        public override async Task OnAppearing()
        {
            IsBusy = true;
            await Fetch();
        }

        private async Task Fetch()
        {
            var client = await IoTCentral.Current.GetServiceClient();
            try
            {
                Applications.AddRange(await client.ListApps());
                OnPropertyChanged("Applications");
            }
            catch(Exception ex)
            {
                if(ex is AuthenticationException)
                {
                    new IoTCException().Error("Invalid authentication");
                }
            }
            finally
            {
                IsBusy = false;
            }
            
            
        }

        private void CreateNewApplication()
        {
            Navigation.NavigateTo(new CreateAppViewModel(Navigation));
        }
    }
}
