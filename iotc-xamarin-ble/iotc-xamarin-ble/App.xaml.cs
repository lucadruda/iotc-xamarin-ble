using DLToolkit.Forms.Controls;
using iotc_ble_xamarin;
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
            //string accessToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6IkhCeGw5bUFlNmd4YXZDa2NvT1UyVEhzRE5hMCIsImtpZCI6IkhCeGw5bUFlNmd4YXZDa2NvT1UyVEhzRE5hMCJ9.eyJhdWQiOiJodHRwczovL2FwcHMuYXp1cmVpb3RjZW50cmFsLmNvbSIsImlzcyI6Imh0dHBzOi8vc3RzLndpbmRvd3MubmV0LzcyZjk4OGJmLTg2ZjEtNDFhZi05MWFiLTJkN2NkMDExZGI0Ny8iLCJpYXQiOjE1NTc5MTQ2ODAsIm5iZiI6MTU1NzkxNDY4MCwiZXhwIjoxNTU3OTE4NTgwLCJhY3IiOiIxIiwiYWlvIjoiQVZRQXEvOExBQUFBTFpwbVdTNTk3ZC9JTlNNeVRaZmFIYnVMcmNTY1IwRFZVYkJlU3lyTGJhMjMxd3E5eDdXTHNEUHFub1AvWC83Y2MvYWx2MFErQk4xWGVyb2pUclhVTlVSS0hLME9rM2thdDg1UDh5RHNDZVU9IiwiYW1yIjpbInJzYSIsIndpYSIsIm1mYSJdLCJhcHBpZCI6IjA0YjA3Nzk1LThkZGItNDYxYS1iYmVlLTAyZjllMWJmN2I0NiIsImFwcGlkYWNyIjoiMCIsImZhbWlseV9uYW1lIjoiRHJ1ZGEiLCJnaXZlbl9uYW1lIjoiTHVjYSIsImlwYWRkciI6IjE4OC4xNTMuNjMuMTc2IiwibmFtZSI6Ikx1Y2EgRHJ1ZGEiLCJvaWQiOiIzODEyYzVlMC0wZGQ2LTQ1NjItOWFlNi1mZDg5NTEyOTgxODQiLCJvbnByZW1fc2lkIjoiUy0xLTUtMjEtMjEyNzUyMTE4NC0xNjA0MDEyOTIwLTE4ODc5Mjc1MjctMjEwOTU1OTgiLCJwdWlkIjoiMTAwMzNGRkY5N0EyMTU3OSIsInNjcCI6InVzZXJfaW1wZXJzb25hdGlvbiIsInN1YiI6Ik9tU19lU1g5UWhRU0JxbjFuX3BybzI3SHpBY014b2FFVWNwSnFwZEttQzAiLCJ0aWQiOiI3MmY5ODhiZi04NmYxLTQxYWYtOTFhYi0yZDdjZDAxMWRiNDciLCJ1bmlxdWVfbmFtZSI6Imx1ZHJ1ZGFAbWljcm9zb2Z0LmNvbSIsInVwbiI6Imx1ZHJ1ZGFAbWljcm9zb2Z0LmNvbSIsInV0aSI6Ik1YVEd3aWZLODBTZU1PVHBXZ2NkQUEiLCJ2ZXIiOiIxLjAifQ.Mc5d3ATZVQkdF_czqY_FJds09pYXZjT9yqU_OTpHn9W_85e_7B7MbmUJNDQXSlMKFIM0d_n5Zl2x0wmOr-7Zzc0vzJNqunxSPrUL3ZOzRSfjgrpOTw_loXe31AFFVV414HQ3ir6i7BCBCerB4rmmKmA6GgmTpKQtWG6HfWAjbq4J_Q9ytygHSuRno6bUyalM1sf4eES439-5Va7rYYlfPUcy6hu6jAG_viLNjbSw5wi-_P4sITmN8YlaiEGOtchOXc_jhcqL7-X7rBuxJTgHzYQ11HjJDNfhdYmmei89wp9oVikavna1GV4YXhtJ-GEdaWS1v41Q3jJoVo_f_9MEVg";
            //IoTCentralClient = new DataClient(accessToken);
            var navigator = new NavigationService(new ViewLocator());
            navigator.PresentAsNavigatableMainPage(new BleScanViewModel(navigator));
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
