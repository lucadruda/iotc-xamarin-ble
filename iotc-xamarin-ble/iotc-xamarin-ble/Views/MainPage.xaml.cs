using Xamarin.Forms;
using iotc_xamarin_ble.ViewModels;

namespace iotc_xamarin_ble.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        //async void OnSignInSignOut(object sender, EventArgs e)
        //{
        //    //AuthenticationContext context = Authenticator.create();
        //    //var gett = await Authenticator.GetToken(context, Constants.RM_TOKEN_AUDIENCE_v1, (IPlatformParameters)App.ParentWindow);
        //    //Debug.WriteLine(gett.AccessToken);
        //    //var context = AuthenticatorV2.create();
        //    //var authResult = await AuthenticatorV2.GetToken(context, Constants.IOTC_TOKEN_AUDIENCE_v2, App.ParentWindow);
        //    //Debug.WriteLine(authResult.AccessToken);
        //    Application[] applications = await ((App)App.Current).IoTCentralClient.ListApps();
        //    btnSignInSignOut.IsVisible = false;
        //    showApps(applications);


        //}


    }
}
