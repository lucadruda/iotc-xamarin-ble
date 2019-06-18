﻿using DLToolkit.Forms.Controls;
using iotc_ble_xamarin;
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
            if (IoTCentral.Current.ServiceClient == null)
            {

                var authModel = new AuthViewModel(Navigation, Constants.IOTC_TOKEN_AUDIENCE_v1);
                authModel.TokenAcquired += async (s, t) =>
                 {
                     IoTCentral.Current.InitServiceClient(t);
                     await Fetch();
                 };
                await Navigation.NavigateToModal(authModel);
            }
            else
            {
                await Fetch();
            }
        }

        private async Task Fetch()
        {
            Applications.AddRange(await IoTCentral.Current.ServiceClient.ListApps());
            IsBusy = false;
            OnPropertyChanged("Applications");
        }

        private void CreateNewApplication()
        {
            Navigation.NavigateTo(new CreateAppViewModel(Navigation));
        }
    }
}
