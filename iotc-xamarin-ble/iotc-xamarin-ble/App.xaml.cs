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
            Properties.Add("mocked", true);
            FlowListView.Init();
            string accessToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6IkhCeGw5bUFlNmd4YXZDa2NvT1UyVEhzRE5hMCIsImtpZCI6IkhCeGw5bUFlNmd4YXZDa2NvT1UyVEhzRE5hMCJ9.eyJhdWQiOiJodHRwczovL2FwcHMuYXp1cmVpb3RjZW50cmFsLmNvbSIsImlzcyI6Imh0dHBzOi8vc3RzLndpbmRvd3MubmV0LzcyZjk4OGJmLTg2ZjEtNDFhZi05MWFiLTJkN2NkMDExZGI0Ny8iLCJpYXQiOjE1NTkxNDYyOTcsIm5iZiI6MTU1OTE0NjI5NywiZXhwIjoxNTU5MTUwMTk3LCJhY3IiOiIxIiwiYWlvIjoiQVZRQXEvOExBQUFBb2UrR1pMejkzaDJxV2hZM2h5TmZVN2hNQSsxRVRDNmxIbU9mc2h5NktHUmY2L1Nra1h6TE5ORktnUzFWaHJmM0l5Rm8ycWlPODA3cGJnSG5LdjFiU0Q1M0N4dW9EdDZIK0x0ekM2Q3BNbEE9IiwiYW1yIjpbInJzYSIsIndpYSIsIm1mYSJdLCJhcHBpZCI6IjA0YjA3Nzk1LThkZGItNDYxYS1iYmVlLTAyZjllMWJmN2I0NiIsImFwcGlkYWNyIjoiMCIsImZhbWlseV9uYW1lIjoiRHJ1ZGEiLCJnaXZlbl9uYW1lIjoiTHVjYSIsImlwYWRkciI6IjQ3LjUzLjQyLjIwMiIsIm5hbWUiOiJMdWNhIERydWRhIiwib2lkIjoiMzgxMmM1ZTAtMGRkNi00NTYyLTlhZTYtZmQ4OTUxMjk4MTg0Iiwib25wcmVtX3NpZCI6IlMtMS01LTIxLTIxMjc1MjExODQtMTYwNDAxMjkyMC0xODg3OTI3NTI3LTIxMDk1NTk4IiwicHVpZCI6IjEwMDMzRkZGOTdBMjE1NzkiLCJzY3AiOiJ1c2VyX2ltcGVyc29uYXRpb24iLCJzdWIiOiJPbVNfZVNYOVFoUVNCcW4xbl9wcm8yN0h6QWNNeG9hRVVjcEpxcGRLbUMwIiwidGlkIjoiNzJmOTg4YmYtODZmMS00MWFmLTkxYWItMmQ3Y2QwMTFkYjQ3IiwidW5pcXVlX25hbWUiOiJsdWRydWRhQG1pY3Jvc29mdC5jb20iLCJ1cG4iOiJsdWRydWRhQG1pY3Jvc29mdC5jb20iLCJ1dGkiOiJ1X3lFZXpLU2kwbTFQYU05QXc2S0FBIiwidmVyIjoiMS4wIn0.QXyoG8qgP0FCgTXbpgcXRgjdjWGl96BGHfFCZb2Am03Q6EPHreEFBbfgvqSldpLIWP0d_UtPCpevoBy4oZMjHY4QLANsnfaGmL04-HV-yq1VWrp9n5itlu7TyZG4STrMLq0StBtRbWw1gYi5FclFA4bxA6bmH-QFScqapejY8-0Cu-8gzXX8jVwAszh1lgshjd6ztRimIqcxw6Kom1PZZXLKVRdRqEJPBYi0c7KXC7_2m5sIi1PXG5_8R_rkN2RVFgQ68mJ_AjmtR_v8kqnVnYtibx0e6KcW6HYvJa90lWJz9mTZm0s7jvOKOZkvbgzkJLetLU-ZjGoAEiH7_gyNsQ";
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
