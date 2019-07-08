using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;

namespace iotc_xamarin_ble.Authentication.v1
{
    public class AzureToken
    {
        public AzureToken(string accessToken, int expiration, string refreshToken, string resource, string familyName, string givenName, string email)
        {
            AccessToken = accessToken;
            Expiration = expiration;
            RefreshToken = refreshToken;
            Resource = resource;
            FamilyName = familyName;
            GivenName = givenName;
            Email = email;
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
            var parsedToken = new JwtSecurityToken(AccessToken);
            var email = parsedToken.Claims.FirstOrDefault(c => c.Type == "email");
            if (email == null)
            {
                email = parsedToken.Claims.FirstOrDefault(c => c.Type == "upn");
            }
            Email = email.Value;
            FamilyName = parsedToken.Claims.FirstOrDefault(c => c.Type == "family_name").Value;
            GivenName = parsedToken.Claims.FirstOrDefault(c => c.Type == "given_name").Value;
        }

        public string AccessToken { get; set; }
        public int Expiration { get; set; }

        public string RefreshToken { get; set; }

        public string Resource { get; set; }
        public string FamilyName { get; set; }
        public string GivenName { get; set; }
        public string Email { get; set; }




        public bool Expired()
        {
            var unixTimestamp = (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            if (unixTimestamp > Expiration)
                return true;
            return false;
        }
    }
}
