﻿using DLToolkit.Forms.Controls;
using iotc_ble_xamarin;
using iotc_xamarin_ble.Services;
using iotc_xamarin_ble.ViewModels;
using iotc_xamarin_ble.ViewModels.Navigation;
using iotc_xamarin_ble.Views;
using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using iotc_xamarin_ble.Services.Container;
using XamarinApplication = Xamarin.Forms.Application;
using Plugin.BLE.Abstractions.Contracts;
using iotc_xamarin_ble.ViewModels.Authentication;
using iotc_csharp_service;
using Plugin.BLE;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using iotc_xamarin_ble.Services.Permissions;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace iotc_xamarin_ble
{
    public partial class App : XamarinApplication
    {
        public event Action ApplicationResumed;
        public App()
        {
            InitializeComponent();
            FlowListView.Init();
            var navigator = new NavigationService(new ViewLocator());
#if MOCKBLE
            ContainerService.Current.RegisterInstance<IAdapter>(new Mocks.MockBLEAdapter());
#else
            ContainerService.Current.RegisterInstance<IAdapter>(CrossBluetoothLE.Current.Adapter);
#endif
#if MOCKAUTH
            ContainerService.Current.RegisterInstance<IAuthViewModel>(new Mocks.MockAuthViewModel(navigator));
#else
            ContainerService.Current.RegisterInstance<IAuthViewModel>(new AuthViewModel(navigator));

#endif

#if APIMOCK
            ContainerService.Current.RegisterInstance<DataClient>(new Mocks.Clients.MockServiceClient(null));
            ContainerService.Current.RegisterInstance<ARMClient>(new Mocks.Clients.MockArmClient(null));
#endif
            ContainerService.Current.RegisterInstance<IPermissions>(new Permissions());


            navigator.PresentAsMainPage(new MainViewModel(navigator));
            //navigator.PresentAsMainPage(new UserDetailsViewModel(navigator));
            //navigator.PresentAsNavigatableMainPage(new WifiScanViewModel(navigator));

        }
        public static object ParentWindow { get; set; }
        protected override void OnStart()
        {
            AppCenter.Start("ios=1d9f0a76-0c88-43ed-bcf4-68dee009c25a;android=e0920845-52e8-47e4-8de7-15cc0d9651b2",
                  typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            ApplicationResumed?.Invoke();
        }

        public DataClient IoTCentralClient { get; private set; }

    }
}
