using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iotc_ble_xamarin.Authentication.v2
{
    public class Authenticator
    {

        /* Constant to store user id in shared preferences */
        //    private static final String USER_ID = "user_id";
        //private static final String TAG = "ADALAUTH";

        /* Azure AD Constants */
        /* Authority is in the form of https://login.microsoftonline.com/yourtenant.onmicrosoft.com */
        public static string AUTHORITY = "https://login.microsoftonline.com/common";
       //public static string AUTHORITY = "https://login.microsoftonline.com/common/oauth2/authorize";

        /* The clientID of your app_activity is a unique identifier which can be obtained from the app registration portal */
        public static string CLIENT_ID = Constants.CLIENT_ID;
        /* Resource URI of the endpoint which will be accessed */
        /* The Redirect URI of the app_activity (Optional) */
        public static string REDIRECT_URI = $"msal{CLIENT_ID}://auth";
        //public static string REDIRECT_URI = "http://localhost";

        private static Microsoft.IdentityModel.Clients.ActiveDirectory.TokenCache mainCache;

        private static object ParentWindow { get; set; }


        public static IPublicClientApplication create()
        {
            return create(AUTHORITY);
        }

        public static IPublicClientApplication create(string authority)
        {
            return PublicClientApplicationBuilder.Create(CLIENT_ID).WithAuthority(authority).WithRedirectUri(REDIRECT_URI).Build();
        }

        public static async Task<AuthenticationResult> GetToken(IPublicClientApplication context, string[] resource, object parent)
        {

            AuthenticationResult authResult = null;
            IEnumerable<IAccount> accounts = await context.GetAccountsAsync();
            // let's see if we have a user in our belly already
            try
            {
                IAccount firstAccount = accounts.FirstOrDefault();
                authResult = await context.AcquireTokenSilent(resource, firstAccount)
                                      .ExecuteAsync();
            }
            catch (MsalUiRequiredException ex)
            {
                try
                {
                    authResult = await context.AcquireTokenInteractive(resource)
                                              .WithParentActivityOrWindow(parent)
                                              .ExecuteAsync();
                }
                catch (Exception ex2)
                {

                }
            }
            return authResult;
        }


    }
}
