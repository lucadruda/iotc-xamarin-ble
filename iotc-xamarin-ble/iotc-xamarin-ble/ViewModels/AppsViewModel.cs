using DLToolkit.Forms.Controls;
using iotc_ble_xamarin;
using iotc_xamarin_ble.Services;
using iotc_xamarin_ble.ViewModels.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace iotc_xamarin_ble.ViewModels
{
    public class AppsViewModel : BaseViewModel
    {
        public AppsViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Azure IoT Central";
            Icon = "home";
            //IsBusy = true;
            Add = new Command(CreateNewApplication);
            ReloadApplications = new Command(async () =>
            {
                IsBusy = true;
                await Fetch();
            });
            OnApplicationTapped = new Command(() =>
              {
                  Application tappedApp = LastTappedItem as Application;
                  if (tappedApp != null)
                  {
                      IoTCentral.Current.Application = tappedApp;
                      Navigation.NavigateTo(new ModelsViewModel(Navigation));
                  }
              });

        }
        public string Prova { get; set; }

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
            Applications.Clear();
            var client = await IoTCentral.Current.GetServiceClient();
            var apps = await client.ListApps();
            Applications.AddRange(apps);
            IsBusy = false;
            OnPropertyChanged("Applications");
        }

        private void CreateNewApplication()
        {
            Navigation.NavigateTo(new CreateAppViewModel(Navigation));
        }
    }
}
