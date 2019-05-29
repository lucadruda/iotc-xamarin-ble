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
            string accessToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6IkhCeGw5bUFlNmd4YXZDa2NvT1UyVEhzRE5hMCIsImtpZCI6IkhCeGw5bUFlNmd4YXZDa2NvT1UyVEhzRE5hMCJ9.eyJhdWQiOiJodHRwczovL2FwcHMuYXp1cmVpb3RjZW50cmFsLmNvbSIsImlzcyI6Imh0dHBzOi8vc3RzLndpbmRvd3MubmV0LzcyZjk4OGJmLTg2ZjEtNDFhZi05MWFiLTJkN2NkMDExZGI0Ny8iLCJpYXQiOjE1NTkxNDE1ODUsIm5iZiI6MTU1OTE0MTU4NSwiZXhwIjoxNTU5MTQ1NDg1LCJhY3IiOiIxIiwiYWlvIjoiQVZRQXEvOExBQUFBeSs5M3VHbDRyb3N3UktIV1E5STRJa1U4QkNEbDVhT3ZNUnNLOHFBUXZ1bHl5bG5UVnJMZndnT21RU1lmT0lzS21hMkl6SG9IeW9vWit2ZVhMbi9PeC9WNVBjU1I1aFdPdXdPMlNtZmlvbzQ9IiwiYW1yIjpbInJzYSIsIndpYSIsIm1mYSJdLCJhcHBpZCI6IjA0YjA3Nzk1LThkZGItNDYxYS1iYmVlLTAyZjllMWJmN2I0NiIsImFwcGlkYWNyIjoiMCIsImZhbWlseV9uYW1lIjoiRHJ1ZGEiLCJnaXZlbl9uYW1lIjoiTHVjYSIsImlwYWRkciI6IjQ3LjUzLjQyLjIwMiIsIm5hbWUiOiJMdWNhIERydWRhIiwib2lkIjoiMzgxMmM1ZTAtMGRkNi00NTYyLTlhZTYtZmQ4OTUxMjk4MTg0Iiwib25wcmVtX3NpZCI6IlMtMS01LTIxLTIxMjc1MjExODQtMTYwNDAxMjkyMC0xODg3OTI3NTI3LTIxMDk1NTk4IiwicHVpZCI6IjEwMDMzRkZGOTdBMjE1NzkiLCJzY3AiOiJ1c2VyX2ltcGVyc29uYXRpb24iLCJzdWIiOiJPbVNfZVNYOVFoUVNCcW4xbl9wcm8yN0h6QWNNeG9hRVVjcEpxcGRLbUMwIiwidGlkIjoiNzJmOTg4YmYtODZmMS00MWFmLTkxYWItMmQ3Y2QwMTFkYjQ3IiwidW5pcXVlX25hbWUiOiJsdWRydWRhQG1pY3Jvc29mdC5jb20iLCJ1cG4iOiJsdWRydWRhQG1pY3Jvc29mdC5jb20iLCJ1dGkiOiJPNUxwR2JQOEswS3N3WE9BdkNxQkFBIiwidmVyIjoiMS4wIn0.J4IXfyY7NGbGBynBggf7oagfTOkPI51pwaHAyK_PCl0_P-Xy0uaTA3Jj4_kWoq5IwvQB4LmcMBxIxj3ff_F0mzrOZ-dhhFcLW0ht1Vp_B3qf0m_GyVdPpVtklKUJF4z2l4WjPihE8GeIm-XE98teQu-CsxzO0uvodcaO2m7aVpflfEH5SpW74sgjEccXSv_xQX7doayZ4KpM-Kvqv4aYvFBaB7UYPmnR3Z2vBhOueW0zj4cKy5hl4yVINl4zsVpIDdUdWacV2d2FPrPFfBgbPPlhyA-pjWy0rCuK50YW558Eox67ZpFcf-K2bGe5mwSIIWOzLc9h0BH2BoUms2aImw";
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
