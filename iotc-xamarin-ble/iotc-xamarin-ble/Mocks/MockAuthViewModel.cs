using iotc_xamarin_ble.ViewModels;
using iotc_xamarin_ble.ViewModels.Authentication;
using iotc_xamarin_ble.ViewModels.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
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
            token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6IkN0ZlFDOExlLThOc0M3b0MyelFrWnBjcmZPYyIsImtpZCI6IkN0ZlFDOExlLThOc0M3b0MyelFrWnBjcmZPYyJ9.eyJhdWQiOiJodHRwczovL2FwcHMuYXp1cmVpb3RjZW50cmFsLmNvbSIsImlzcyI6Imh0dHBzOi8vc3RzLndpbmRvd3MubmV0LzcyZjk4OGJmLTg2ZjEtNDFhZi05MWFiLTJkN2NkMDExZGI0Ny8iLCJpYXQiOjE1NjE3NDQzNzUsIm5iZiI6MTU2MTc0NDM3NSwiZXhwIjoxNTYxNzQ4Mjc1LCJhY3IiOiIxIiwiYWlvIjoiQVZRQXEvOExBQUFBTTM4LzZkVWhyS0dSRzlPc3ZGaWJzakFsc1pnTDJFZ0FRcU9sOGhtb3RSWk9oZ2w1clJ1L20wb1ErUjdvQUkvNjY0RFdzZjZiU1ZWWC9JQk53Y0p3cHpvTUh2SUdOeTkvU0tobE9rV2RhSFU9IiwiYW1yIjpbInB3ZCIsIm1mYSJdLCJhcHBpZCI6IjA0YjA3Nzk1LThkZGItNDYxYS1iYmVlLTAyZjllMWJmN2I0NiIsImFwcGlkYWNyIjoiMCIsImZhbWlseV9uYW1lIjoiRHJ1ZGEiLCJnaXZlbl9uYW1lIjoiTHVjYSIsImlwYWRkciI6IjkzLjcwLjE3Mi44OSIsIm5hbWUiOiJMdWNhIERydWRhIiwib2lkIjoiMzgxMmM1ZTAtMGRkNi00NTYyLTlhZTYtZmQ4OTUxMjk4MTg0Iiwib25wcmVtX3NpZCI6IlMtMS01LTIxLTIxMjc1MjExODQtMTYwNDAxMjkyMC0xODg3OTI3NTI3LTIxMDk1NTk4IiwicHVpZCI6IjEwMDMzRkZGOTdBMjE1NzkiLCJzY3AiOiJ1c2VyX2ltcGVyc29uYXRpb24iLCJzdWIiOiJPbVNfZVNYOVFoUVNCcW4xbl9wcm8yN0h6QWNNeG9hRVVjcEpxcGRLbUMwIiwidGlkIjoiNzJmOTg4YmYtODZmMS00MWFmLTkxYWItMmQ3Y2QwMTFkYjQ3IiwidW5pcXVlX25hbWUiOiJsdWRydWRhQG1pY3Jvc29mdC5jb20iLCJ1cG4iOiJsdWRydWRhQG1pY3Jvc29mdC5jb20iLCJ1dGkiOiJJMXJGZXcyZkYwaTkwaGI0dmxBU0FBIiwidmVyIjoiMS4wIn0.EOeT69bPTUiUK5a74soOUN3f7-cValWKLqYfYYjuSTpbyGAXoqoLQcDVtf1Rfy1bX_KmDEydSLLVvEKrONVA4Gt9L4XdsRUpRFXPSlfVnRG5Cr9kCcc0Dz5njMpufhrSelKM307Sy65aITefb6EwjttY7c7g9CtGKaKzwk_W9ZDS6QHXcQmPX1iyH23_thhePTkJkn3jdW8f2yGF2xq13ovNzw2qcZIiHus4SF_ShrgSSZy03Yrc-Nl4iiSAyKqfUsiZM6oS4w5PqpQUy7qoNHuc6GfDM7tOSl-KW3DBnB6aWiTtZmElmwBu9tOfaspkRSif7S1xeQwZ9F44R7fQwg";
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
