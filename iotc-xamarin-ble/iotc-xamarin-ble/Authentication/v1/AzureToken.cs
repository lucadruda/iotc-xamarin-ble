using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace iotc_xamarin_ble.Authentication.v1
{
    public class AzureToken
    {
        public AzureToken(string accessToken, int expiration, string refreshToken, string resource)
        {
            AccessToken = accessToken;
            Expiration = expiration;
            RefreshToken = refreshToken;
            Resource = resource;
        }

        public AzureToken()
        {

        }

        public AzureToken(string jsonToken)
        {
            JObject resObj = JObject.Parse(jsonToken);
            AccessToken = resObj["access_token"].Value<string>();
            RefreshToken = resObj["refresh_token"].Value<string>();
            Expiration = resObj["expires_on"].Value<int>();
            Resource = resObj["resource"].Value<string>();
        }

        public string AccessToken { get; set; }
        public int Expiration { get; set; }

        public string RefreshToken { get; set; }

        public string Resource { get; set; }


        public bool Expired()
        {
            var unixTimestamp = (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            if (unixTimestamp > Expiration)
                return true;
            return false;
        }
    }
}
