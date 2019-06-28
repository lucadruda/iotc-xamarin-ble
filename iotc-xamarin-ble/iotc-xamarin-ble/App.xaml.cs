using DLToolkit.Forms.Controls;
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

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace iotc_xamarin_ble
{
    public partial class App : XamarinApplication
    {
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

            //navigator.PresentAsMainPage(new MainViewModel(navigator));
            navigator.PresentAsNavigatableMainPage(new AppsViewModel(navigator));

        }
        public static object ParentWindow { get; set; }
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public DataClient IoTCentralClient { get; private set; }

    }
}
