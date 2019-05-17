using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthenticationResult = Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationResult;

namespace iotc_ble_xamarin.Authentication.v1
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
        //public static string REDIRECT_URI = "http://localhost";
        public static Uri REDIRECT_URI = new Uri("https://login.microsoftonline.com/common/oauth2/nativeclient");



        private static IPublicClientApplication publicClientApplication = null;

        private static Microsoft.IdentityModel.Clients.ActiveDirectory.TokenCache mainCache;

        private static object ParentWindow { get; set; }


        public static AuthenticationContext create()
        {
            AuthenticationContext authContext = create(AUTHORITY);
            mainCache = authContext.TokenCache;
            return authContext;
        }

        public static AuthenticationContext create(string authority)
        {
            if (mainCache != null)
            {
                return new AuthenticationContext(authority, true, mainCache);
            }
            return new AuthenticationContext(authority);
        }

        public static async Task<AuthenticationResult> GetToken(AuthenticationContext context, string resource, IPlatformParameters parent)
        {

            AuthenticationResult result = null;
            try
            {
                result = await context.AcquireTokenSilentAsync(resource, CLIENT_ID);
            }
            catch (AdalSilentTokenAcquisitionException ex)
            {
                try
                {
                    // interaction required
                    result = await context.AcquireTokenAsync(resource, CLIENT_ID, REDIRECT_URI, parent);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return result;
        }

    }
}
