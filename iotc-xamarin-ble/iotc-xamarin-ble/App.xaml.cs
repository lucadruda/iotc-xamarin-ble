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
            string accessToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6IkhCeGw5bUFlNmd4YXZDa2NvT1UyVEhzRE5hMCIsImtpZCI6IkhCeGw5bUFlNmd4YXZDa2NvT1UyVEhzRE5hMCJ9.eyJhdWQiOiJodHRwczovL2FwcHMuYXp1cmVpb3RjZW50cmFsLmNvbSIsImlzcyI6Imh0dHBzOi8vc3RzLndpbmRvd3MubmV0LzcyZjk4OGJmLTg2ZjEtNDFhZi05MWFiLTJkN2NkMDExZGI0Ny8iLCJpYXQiOjE1NTkwNTUxOTIsIm5iZiI6MTU1OTA1NTE5MiwiZXhwIjoxNTU5MDU5MDkyLCJhY3IiOiIxIiwiYWlvIjoiQVZRQXEvOExBQUFBZGRTZW1QcGFBazZRcFFuMnROdlc5Mm9EREsybHBRa29SbHJOM0JDMXA0ak0vaTZxRGNBOEZWVm85NkE0akJKQ1VzeGVsanVsWGNlcWNKN3FzZHVkWmp0SjdHSHFvUVpjYSsvejRRY1VzRkk9IiwiYW1yIjpbInJzYSIsIndpYSIsIm1mYSJdLCJhcHBpZCI6IjA0YjA3Nzk1LThkZGItNDYxYS1iYmVlLTAyZjllMWJmN2I0NiIsImFwcGlkYWNyIjoiMCIsImZhbWlseV9uYW1lIjoiRHJ1ZGEiLCJnaXZlbl9uYW1lIjoiTHVjYSIsImlwYWRkciI6IjQ3LjUzLjQyLjIwMiIsIm5hbWUiOiJMdWNhIERydWRhIiwib2lkIjoiMzgxMmM1ZTAtMGRkNi00NTYyLTlhZTYtZmQ4OTUxMjk4MTg0Iiwib25wcmVtX3NpZCI6IlMtMS01LTIxLTIxMjc1MjExODQtMTYwNDAxMjkyMC0xODg3OTI3NTI3LTIxMDk1NTk4IiwicHVpZCI6IjEwMDMzRkZGOTdBMjE1NzkiLCJzY3AiOiJ1c2VyX2ltcGVyc29uYXRpb24iLCJzdWIiOiJPbVNfZVNYOVFoUVNCcW4xbl9wcm8yN0h6QWNNeG9hRVVjcEpxcGRLbUMwIiwidGlkIjoiNzJmOTg4YmYtODZmMS00MWFmLTkxYWItMmQ3Y2QwMTFkYjQ3IiwidW5pcXVlX25hbWUiOiJsdWRydWRhQG1pY3Jvc29mdC5jb20iLCJ1cG4iOiJsdWRydWRhQG1pY3Jvc29mdC5jb20iLCJ1dGkiOiJzYldYS0FHTHdrcUtJVl9ac1k1dEFBIiwidmVyIjoiMS4wIn0.mptwfA9MtBs2oM3Zo_GXAUQ5TFmW-tkwhp_uA_fqj83xyEBaPvzXxRJf5W5I7FeauaOZkKeIvbSbWMfDAwI1ISw1np0S4499e-V0U6NAeww0ucSMHm-dsPOAI4aulAye-UhU_hcRHvsjBG1VtNCqKinz-vMMVq0M7piyPD2u9pkSo2WJGXYPIJNDdJTDvktQ3m3pnSM_3gtC_iE1Cwa5zW0nJUNcdeLvOOypjbPxl_sBpI8PixInxi1kM8VbjszPUT2fZIGsPccFMPQ9qlJuKixyRuN65mz_ggpxJYAJe1b6aTOh6CwdVPrOpJeLlPwdXhbWTDSXmnXUr65YZccHBQ";
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
