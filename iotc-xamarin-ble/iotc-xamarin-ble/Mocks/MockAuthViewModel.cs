using iotc_xamarin_ble.ViewModels;
using iotc_xamarin_ble.ViewModels.Authentication;
using iotc_xamarin_ble.ViewModels.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace iotc_xamarin_ble.Mocks
{
    public class MockAuthViewModel : BaseViewModel, IAuthViewModel
    {
        string token;
       
        public MockAuthViewModel(INavigationService navigation) : base(navigation)
        {
            //token = File.ReadAllText("accessToken.txt");
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"accessToken.txt");
            token = File.ReadAllText(path);
            //token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6InU0T2ZORlBId0VCb3NIanRyYXVPYlY4NExuWSIsImtpZCI6InU0T2ZORlBId0VCb3NIanRyYXVPYlY4NExuWSJ9.eyJhdWQiOiJodHRwczovL2FwcHMuYXp1cmVpb3RjZW50cmFsLmNvbSIsImlzcyI6Imh0dHBzOi8vc3RzLndpbmRvd3MubmV0LzcyZjk4OGJmLTg2ZjEtNDFhZi05MWFiLTJkN2NkMDExZGI0Ny8iLCJpYXQiOjE1NjUxNjc5NjIsIm5iZiI6MTU2NTE2Nzk2MiwiZXhwIjoxNTY1MTcxODYyLCJhY3IiOiIxIiwiYWlvIjoiQVZRQXEvOE1BQUFBQ3R1QTFOdHZVa3RmNUYwVzFvekhkd1luQjlSdmU3bnVFK0wxODNFVE9JTHBwZUYwUURRMkxPTWNGSzkvVG02UUhFblZrcW9MZS9peFh2c3ZIRG5PbndrQmJPTmRWcExjRjJRQkplTWNtZjA9IiwiYW1yIjpbIndpYSIsIm1mYSJdLCJhcHBpZCI6IjA0YjA3Nzk1LThkZGItNDYxYS1iYmVlLTAyZjllMWJmN2I0NiIsImFwcGlkYWNyIjoiMCIsImZhbWlseV9uYW1lIjoiRHJ1ZGEiLCJnaXZlbl9uYW1lIjoiTHVjYSIsImlwYWRkciI6IjEwOS4xMTUuMjExLjE2NyIsIm5hbWUiOiJMdWNhIERydWRhIiwib2lkIjoiMzgxMmM1ZTAtMGRkNi00NTYyLTlhZTYtZmQ4OTUxMjk4MTg0Iiwib25wcmVtX3NpZCI6IlMtMS01LTIxLTIxMjc1MjExODQtMTYwNDAxMjkyMC0xODg3OTI3NTI3LTIxMDk1NTk4IiwicHVpZCI6IjEwMDMzRkZGOTdBMjE1NzkiLCJzY3AiOiJ1c2VyX2ltcGVyc29uYXRpb24iLCJzdWIiOiJPbVNfZVNYOVFoUVNCcW4xbl9wcm8yN0h6QWNNeG9hRVVjcEpxcGRLbUMwIiwidGlkIjoiNzJmOTg4YmYtODZmMS00MWFmLTkxYWItMmQ3Y2QwMTFkYjQ3IiwidW5pcXVlX25hbWUiOiJsdWRydWRhQG1pY3Jvc29mdC5jb20iLCJ1cG4iOiJsdWRydWRhQG1pY3Jvc29mdC5jb20iLCJ1dGkiOiJGYm9SUUtqNUswS3dCMlNMcWZNRkFBIiwidmVyIjoiMS4wIn0.DeDvivlhwcXS7zLln2CmV5DXwAB1LndS5iMEtssIYHz75SrXNkPUiLUhd_vBIXIlROfIZcVN1C8vpS7KXaitMuYZwlegsEXD2X8rZ7lbPstWO2laQiq0qNCkhz-fhp24uQhmV7oyfqzLejkteCOPPUR7fUNjPf-FllpYKWkRMWuVpK07U25tvIFcbvTbH_F0aL7TBJOH0-7HSQ-gGB6iNrxJOhMS4Pt838peuw6vrOyQ_kcqKTzte06WagL8sr3Ibx_VjgjT1MCyl_yaLn3594LEVFhTQPDg4WYQE4ZoRvph9YQD5y6tW5SlP5Nzt1GCMVXaaYuojSlPdiU6GN8JCg";
        }

        public Task Clear()
        {
            return Task.CompletedTask;
        }

        public async Task<string> GetTokenAsync()
        {

            return await Task.FromResult(token);
        }

        public Task<string> GetTokenAsync(string resource)
        {
            return GetTokenAsync();

        }

        public Task<string> GetTokenAsync(string resource, string tenant)
        {
            return GetTokenAsync();

        }
    }
}
