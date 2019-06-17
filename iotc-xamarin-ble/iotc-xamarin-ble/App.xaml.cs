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
            if (Properties.ContainsKey("mocked"))

                Properties["mocked"] = true;

            else Properties.Add("mocked", true);

            FlowListView.Init();
            //string accessToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6IkhCeGw5bUFlNmd4YXZDa2NvT1UyVEhzRE5hMCIsImtpZCI6IkhCeGw5bUFlNmd4YXZDa2NvT1UyVEhzRE5hMCJ9.eyJhdWQiOiJodHRwczovL2FwcHMuYXp1cmVpb3RjZW50cmFsLmNvbSIsImlzcyI6Imh0dHBzOi8vc3RzLndpbmRvd3MubmV0LzcyZjk4OGJmLTg2ZjEtNDFhZi05MWFiLTJkN2NkMDExZGI0Ny8iLCJpYXQiOjE1NTk1NTcxOTYsIm5iZiI6MTU1OTU1NzE5NiwiZXhwIjoxNTU5NTYxMDk2LCJhY3IiOiIxIiwiYWlvIjoiQVZRQXEvOExBQUFBb2UrR1pMejkzaDJxV2hZM2h5TmZVN2hNQSsxRVRDNmxIbU9mc2h5NktHUmY2L1Nra1h6TE5ORktnUzFWaHJmM0l5Rm8ycWlPODA3cGJnSG5LdjFiU0Q1M0N4dW9EdDZIK0x0ekM2Q3BNbEE9IiwiYW1yIjpbInJzYSIsIndpYSIsIm1mYSJdLCJhcHBpZCI6IjA0YjA3Nzk1LThkZGItNDYxYS1iYmVlLTAyZjllMWJmN2I0NiIsImFwcGlkYWNyIjoiMCIsImZhbWlseV9uYW1lIjoiRHJ1ZGEiLCJnaXZlbl9uYW1lIjoiTHVjYSIsImlwYWRkciI6IjQ3LjUzLjQyLjIwMiIsIm5hbWUiOiJMdWNhIERydWRhIiwib2lkIjoiMzgxMmM1ZTAtMGRkNi00NTYyLTlhZTYtZmQ4OTUxMjk4MTg0Iiwib25wcmVtX3NpZCI6IlMtMS01LTIxLTIxMjc1MjExODQtMTYwNDAxMjkyMC0xODg3OTI3NTI3LTIxMDk1NTk4IiwicHVpZCI6IjEwMDMzRkZGOTdBMjE1NzkiLCJzY3AiOiJ1c2VyX2ltcGVyc29uYXRpb24iLCJzdWIiOiJPbVNfZVNYOVFoUVNCcW4xbl9wcm8yN0h6QWNNeG9hRVVjcEpxcGRLbUMwIiwidGlkIjoiNzJmOTg4YmYtODZmMS00MWFmLTkxYWItMmQ3Y2QwMTFkYjQ3IiwidW5pcXVlX25hbWUiOiJsdWRydWRhQG1pY3Jvc29mdC5jb20iLCJ1cG4iOiJsdWRydWRhQG1pY3Jvc29mdC5jb20iLCJ1dGkiOiJJcEtuOUEtN3hFdUZ6el9WQlpRX0FBIiwidmVyIjoiMS4wIn0.D9KQJGY19jwfYpDSi9WIQEjpMfR40rebOta7Iv3if8N67oNkzKU4SZHwH6IyRsaJVkNViHR2S5Sk4mDhRVkld8BrKhJTUief-fhE227X4VkrhqACOZPb95vrJz8M0gA8insMoAPU_zjtqmHlUDMU5zIHNykHqqzwS01ne2Va6DZPXw1I6904ggnWLmOnnBS1em8wJqBmffbmCXoEu0fOFv296ii0mWeO-_JY2zL9iDnWMmV3Muzkuj7tFV34X2BOlHF0z8lMN5Dju0oNPxcjY1cmvnyK1s3Ic1kCdKSaKadS5D1iZ9PRPlSRY2-16SngrW1grWhrTfoWCXU5xnOdgg";
            //IoTCentral.Current.ServiceClient = new DataClient(accessToken);
            var navigator = new NavigationService(new ViewLocator());
            navigator.PresentAsNavigatableMainPage(new AuthViewModel(navigator));
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
