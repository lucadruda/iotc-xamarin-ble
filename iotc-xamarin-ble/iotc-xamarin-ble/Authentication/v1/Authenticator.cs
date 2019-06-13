using iotc_ble_xamarin;
using iotc_xamarin_ble.Services.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Forms;

namespace iotc_xamarin_ble.Authentication.v1
{
    public class Authenticator
    {

        /* Constant to store user id in shared preferences */
        //    private static final String USER_ID = "user_id";
        //private static final String TAG = "ADALAUTH";

        /* Azure AD Constants */
        /* Authority is in the form of https://login.microsoftonline.com/yourtenant.onmicrosoft.com */
        //public static string AUTHORITY = "https://login.microsoftonline.com/organizations/v2.0";
        public static string AUTHORITY = "https://login.microsoftonline.com/common";
        //public static string AUTHORITY = "https://login.microsoftonline.com/common/oauth2/v2.0/authorize";

        /* The clientID of your app_activity is a unique identifier which can be obtained from the app registration portal */
        public static string CLIENT_ID = Constants.CLIENT_ID;
        /* Resource URI of the endpoint which will be accessed */
        /* The Redirect URI of the app_activity (Optional) */
        public static string REDIRECT_URI = "http://localhost";
        public static string RESOURCE_URI = "https://apps.azureiotcentral.com";
        //public static Uri REDIRECT_URI = new Uri("https://login.microsoftonline.com/common/oauth2/nativeclient");




        private static object ParentWindow { get; set; }

        private WebView browser;
        private RestClient client;

        //public static Authenticator()
        //{
        //    client = new RestClient();
        //    browser = new WebView();
        //    browser.Source = $"{AUTHORITY}/oauth2/authorize?client_id={CLIENT_ID}&response_type=code&redirect_uri={HttpUtility.UrlEncode(REDIRECT_URI)}&response_mode=query&resource={HttpUtility.UrlEncode(RESOURCE_URI)}&state=12345";
        //}

        public async Task<string> AcquireTokenAsync(string resourceUri)
        {
            var token = JsonConvert.DeserializeObject<AzureToken>(SecureStorage.Current[resourceUri]);
            if (token.Expired())
            {
                token = await AcquireTokenWithRefresh(resourceUri, token.RefreshToken);
            }
            return token.AccessToken;

        }

        public async Task<AzureToken> AcquireTokenWithRefresh(string resourceUri, string refreshToken)
        {
            var resp = await client.Post($"{AUTHORITY}/oauth2/token", null, $"client_id={CLIENT_ID}&refresh_token={refreshToken}&grant_type=refresh_token&resource={HttpUtility.UrlEncode(resourceUri)}");
            if (resp.Success)
            {
                return JsonConvert.DeserializeObject<AzureToken>(resp.ResponseBody);
            }
            return await AcquireTokenInteractive(resourceUri);

        }

        public async Task<AzureToken> AcquireTokenInteractive(string resourceUri)
        {
            return null;
        }


    }
}
