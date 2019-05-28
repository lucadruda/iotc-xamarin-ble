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
using XamarinApplication = Xamarin.Forms.Application;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace iotc_xamarin_ble
{
    public partial class App : XamarinApplication
    {
        public App()
        {
            InitializeComponent();
            FlowListView.Init();
            string accessToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6IkhCeGw5bUFlNmd4YXZDa2NvT1UyVEhzRE5hMCIsImtpZCI6IkhCeGw5bUFlNmd4YXZDa2NvT1UyVEhzRE5hMCJ9.eyJhdWQiOiJodHRwczovL2FwcHMuYXp1cmVpb3RjZW50cmFsLmNvbSIsImlzcyI6Imh0dHBzOi8vc3RzLndpbmRvd3MubmV0LzcyZjk4OGJmLTg2ZjEtNDFhZi05MWFiLTJkN2NkMDExZGI0Ny8iLCJpYXQiOjE1NTg5NzA4MDMsIm5iZiI6MTU1ODk3MDgwMywiZXhwIjoxNTU4OTc0NzAzLCJhY3IiOiIxIiwiYWlvIjoiQVZRQXEvOExBQUFBNnpaR3cyeU15bElYa3ovMU8yWlRBS1ZYM0o3c0c1UGJ6a3JyQTh3aldUSStOUHF2QVJoWW92Ykg4cmFqalZjb0JhMXRzWUM4RGJNNXdoMllocmh5ZXFNNzl5WFZidVlNeEZaUWpjTTNtRUk9IiwiYW1yIjpbInJzYSIsIndpYSIsIm1mYSJdLCJhcHBpZCI6IjA0YjA3Nzk1LThkZGItNDYxYS1iYmVlLTAyZjllMWJmN2I0NiIsImFwcGlkYWNyIjoiMCIsImZhbWlseV9uYW1lIjoiRHJ1ZGEiLCJnaXZlbl9uYW1lIjoiTHVjYSIsImlwYWRkciI6IjE4OC4xNTMuNjMuMTc2IiwibmFtZSI6Ikx1Y2EgRHJ1ZGEiLCJvaWQiOiIzODEyYzVlMC0wZGQ2LTQ1NjItOWFlNi1mZDg5NTEyOTgxODQiLCJvbnByZW1fc2lkIjoiUy0xLTUtMjEtMjEyNzUyMTE4NC0xNjA0MDEyOTIwLTE4ODc5Mjc1MjctMjEwOTU1OTgiLCJwdWlkIjoiMTAwMzNGRkY5N0EyMTU3OSIsInNjcCI6InVzZXJfaW1wZXJzb25hdGlvbiIsInN1YiI6Ik9tU19lU1g5UWhRU0JxbjFuX3BybzI3SHpBY014b2FFVWNwSnFwZEttQzAiLCJ0aWQiOiI3MmY5ODhiZi04NmYxLTQxYWYtOTFhYi0yZDdjZDAxMWRiNDciLCJ1bmlxdWVfbmFtZSI6Imx1ZHJ1ZGFAbWljcm9zb2Z0LmNvbSIsInVwbiI6Imx1ZHJ1ZGFAbWljcm9zb2Z0LmNvbSIsInV0aSI6ImxMSzhDQURBNkUta0x4anNucjFDQUEiLCJ2ZXIiOiIxLjAifQ.PxaVvYhZDuRafxNxF4V94M0S-sk2NF8NuMy0Xgl73XQOTbCYtGbX0X_hFw8Frb2enZJjzFd9FzP5kGTpKotoGnbwdCEYBJ-bND2uJRl8DCphkt5P7XesPJ-tXV3ZerGxddZGh1J1Crz5_2v9Mn9RpC-Pzo_-LTBQr6ZqFC_PrBnA9QxVrY53ji9HvZB-8l2xCCv23S9-W2xmEkVRfDbjOrJ1Ue7DL-wd6cHnpWKvL1SG2s-IVNwhnQ-FpSx9lnmczuXhDK0NFDWjCxFANowiejL-1WMoWzQd_DFR4zbofjcKIOvplpmgZgHTGs6QOx0d-kjkj7B_DbQrfNrDGZnPCA";
            IoTCentral.Current.ServiceClient = new DataClient(accessToken);
            var navigator = new NavigationService(new ViewLocator());
            navigator.PresentAsNavigatableMainPage(new MainViewModel(navigator));
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
