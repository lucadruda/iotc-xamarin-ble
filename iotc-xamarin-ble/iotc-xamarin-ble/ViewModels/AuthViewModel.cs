using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Input;
using iotc_ble_xamarin;
using iotc_xamarin_ble.Authentication.v1;
using iotc_xamarin_ble.Services.Http;
using iotc_xamarin_ble.ViewModels.Authentication;
using iotc_xamarin_ble.ViewModels.Navigation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace iotc_xamarin_ble.ViewModels
{
    public class AuthViewModel : BaseViewModel, IAuthViewModel
    {
        private RestClient client;
        private TaskCompletionSource<AzureToken> interactiveLoginTask;

        public event EventHandler<string> TokenAcquired;

        public AuthViewModel(INavigationService navigation) : base(navigation)
        {
            Navigated = new Command<WebNavigatedEventArgs>(OnNavigated);
            Navigating = new Command<WebNavigatingEventArgs>(OnNavigating);
            Title = "Azure IoTCentral";
            client = new RestClient();
            Authenticating = false;
            interactiveLoginTask = new TaskCompletionSource<AzureToken>();
        }


        public string Tenant { get; set; }
        public string Resource { get; set; }

        public string Url { get; set; }
        public bool Authenticating { get; set; }
        public ICommand Navigated { get; set; }
        public ICommand Navigating { get; set; }


        private async void OnNavigated(WebNavigatedEventArgs e)
        {
            if (!Authenticating)
            {// not authenticating. event came from somewhere else
                return;
            }
            var matches = new Regex(@"[\S]+code=([\S]+)&[\S]+").Match(e.Url).Groups;
            if (matches.Count > 1)
            {
                await ParseAuthorizationCode(matches[1].Value);
            }
        }

        /*
         Hack: for iOS looks like the response never goes to the OnNavigated.
         Intercept code here
             */
        private async void OnNavigating(WebNavigatingEventArgs e)
        {
            if (!Authenticating)
            {// not authenticating. event came from somewhere else
                return;
            }
            var matches = new Regex(@"[\S]+code=([\S]+)&[\S]+").Match(e.Url).Groups;
            if (matches.Count > 1)
            {
                await ParseAuthorizationCode(matches[1].Value);
            }
        }

        private async Task ParseAuthorizationCode(string authCode)
        {
            Authenticating = false;
            OnPropertyChanged("Authenticating");
            var resp = await TryGetTokenWithAuthCode(Resource, authCode);
            if (resp.Success)
            {
                var token = new AzureToken(resp.ResponseBody);
                SecureStorage.Current.Add(token.Resource, JsonConvert.SerializeObject(token));
                interactiveLoginTask.SetResult(token);
            }
        }

        private async Task<string> AcquireTokenAsync(string resourceUri)
        {
            var storedToken = SecureStorage.Current[resourceUri];
            if (storedToken != null)
            {
                var token = JsonConvert.DeserializeObject<AzureToken>(storedToken);
                if (!token.Expired())
                {
                    return token.AccessToken;
                }
            }
            return (await AcquireTokenWithRefresh(resourceUri)).AccessToken;

        }

        private async Task<AzureToken> AcquireTokenWithRefresh(string resourceUri)
        {

            foreach (var storedToken in SecureStorage.Current.GetAll())
            {
                var token = JsonConvert.DeserializeObject<AzureToken>(storedToken.Value);
                var refreshToken = token.RefreshToken;
                var resp = await TryGetTokenWithRefresh(resourceUri, refreshToken);
                if (resp.Success)
                {
                    return new AzureToken(resp.ResponseBody);
                }
            }

            return await AcquireTokenInteractive(resourceUri);

        }

        private async Task<AzureToken> AcquireTokenInteractive(string resourceUri)
        {

            Url = $"https://{Constants.DEFAULT_AUTHORITY}/{Tenant}/oauth2/authorize?client_id={Constants.CLIENT_ID}&response_type=code&redirect_uri={HttpUtility.UrlEncode(Constants.REDIRECT_URL)}&response_mode=query&resource={HttpUtility.UrlEncode(resourceUri)}&state=12345";
            OnPropertyChanged("Url");
            IsBusy = false;
            Authenticating = true;
            OnPropertyChanged("Authenticating");
            return await interactiveLoginTask.Task;

        }

        private async Task<RestResponse> TryGetTokenWithRefresh(string resourceUri, string refreshToken)
        {
            return await client.Post($"https://{Constants.DEFAULT_AUTHORITY}/{Tenant}/oauth2/token", null, $"client_id={Constants.CLIENT_ID}&refresh_token={refreshToken}&grant_type=refresh_token&resource={HttpUtility.UrlEncode(resourceUri)}", "application/x-www-form-urlencoded");
        }

        private async Task<RestResponse> TryGetTokenWithAuthCode(string resourceUri, string authcode)
        {
            return await client.Post($"https://{Constants.DEFAULT_AUTHORITY}/{Tenant}/oauth2/token", null, $"client_id={Constants.CLIENT_ID}&code={authcode}&grant_type=authorization_code&redirect_uri={HttpUtility.UrlEncode(Constants.REDIRECT_URL)}&resource={HttpUtility.UrlEncode(resourceUri)}", "application/x-www-form-urlencoded", new Dictionary<string, string> { { "Host", Constants.DEFAULT_AUTHORITY } });
        }

        public async Task<string> GetTokenAsync()
        {
            return await GetTokenAsync(Constants.IOTC_TOKEN_AUDIENCE_v1, Constants.DEFAULT_TENANT);
        }

        public async Task<string> GetTokenAsync(string resource)
        {
            return await GetTokenAsync(resource, Constants.DEFAULT_TENANT);
        }

        public async Task<string> GetTokenAsync(string resource, string tenant)
        {
            IsBusy = true;
            Resource = resource;
            Tenant = tenant;
            var token = await AcquireTokenAsync(resource);
            SecureStorage.Current.Save();
            IsBusy = false;
            await Navigation.NavigateBackModal();
            return token;
        }
    }
}
