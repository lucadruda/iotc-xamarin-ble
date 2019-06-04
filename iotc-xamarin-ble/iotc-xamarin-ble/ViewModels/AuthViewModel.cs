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
using iotc_xamarin_ble.ViewModels.Navigation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace iotc_xamarin_ble.ViewModels
{
    public class AuthViewModel : BaseViewModel
    {
        private RestClient client;
        private TaskCompletionSource<AzureToken> interactiveLoginTask;
        public AuthViewModel(INavigationService navigation) : base(navigation)
        {
            Navigated = new Command<WebNavigatedEventArgs>(OnNavigated);
            client = new RestClient();
            Authenticating = false;
            interactiveLoginTask = new TaskCompletionSource<AzureToken>();
        }

        public string Url { get; set; }
        public bool Authenticating { get; set; }
        public ICommand Navigated { get; set; }

        public event EventHandler<string> TokenAcquired;

        private async void OnNavigated(WebNavigatedEventArgs e)
        {
            var matches = new Regex(@"[\S]+code=([\S]+)&[\S]+").Match(e.Url).Groups;
            if (matches.Count > 1)
            {
                Authenticating = false;
                OnPropertyChanged("Authenticating");
                var resp = await TryGetTokenWithAuthCode(Constants.IOTC_TOKEN_AUDIENCE_v1, matches[1].Value);
                if (resp.Success)
                {
                    var token = new AzureToken(resp.ResponseBody);
                    SecureStorage.Current.Add(token.Resource, JsonConvert.SerializeObject(token));
                    interactiveLoginTask.SetResult(token);
                }
            }
        }

        public override async Task OnAppearing()
        {
            var token = await AcquireTokenAsync(Constants.IOTC_TOKEN_AUDIENCE_v1);
            SecureStorage.Current.Save();
            TokenAcquired?.Invoke(this, token);
            Navigation.NavigateBack();
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

            Url = $"https://{Constants.DEFAULT_AUTHORITY}/{Constants.DEFAULT_TENANT}/oauth2/authorize?client_id={Constants.CLIENT_ID}&response_type=code&redirect_uri={HttpUtility.UrlEncode(Constants.REDIRECT_URL)}&response_mode=query&resource={HttpUtility.UrlEncode(resourceUri)}&state=12345";
            OnPropertyChanged("Url");
            Authenticating = true;
            OnPropertyChanged("Authenticating");
            return await interactiveLoginTask.Task;

        }

        private async Task<RestResponse> TryGetTokenWithRefresh(string resourceUri, string refreshToken)
        {
            return await client.Post($"https://{Constants.DEFAULT_AUTHORITY}/{Constants.DEFAULT_TENANT}/oauth2/token", null, $"client_id={Constants.CLIENT_ID}&refresh_token={refreshToken}&grant_type=refresh_token&resource={HttpUtility.UrlEncode(resourceUri)}", "application/x-www-form-urlencoded");
        }

        private async Task<RestResponse> TryGetTokenWithAuthCode(string resourceUri, string authcode)
        {
            return await client.Post($"https://{Constants.DEFAULT_AUTHORITY}/{Constants.DEFAULT_TENANT}/oauth2/token", null, $"client_id={Constants.CLIENT_ID}&code={authcode}&grant_type=authorization_code&redirect_uri={HttpUtility.UrlEncode(Constants.REDIRECT_URL)}&resource={HttpUtility.UrlEncode(resourceUri)}", "application/x-www-form-urlencoded", new Dictionary<string, string> { { "Host", Constants.DEFAULT_AUTHORITY } });
        }

    }
}
