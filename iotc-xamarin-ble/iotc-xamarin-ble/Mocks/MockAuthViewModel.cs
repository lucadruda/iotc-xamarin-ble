using iotc_xamarin_ble.ViewModels;
using iotc_xamarin_ble.ViewModels.Authentication;
using iotc_xamarin_ble.ViewModels.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace iotc_xamarin_ble.Mocks
{
    public class MockAuthViewModel : BaseViewModel, IAuthViewModel
    {
        string token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6IkN0ZlFDOExlLThOc0M3b0MyelFrWnBjcmZPYyIsImtpZCI6IkN0ZlFDOExlLThOc0M3b0MyelFrWnBjcmZPYyJ9.eyJhdWQiOiJodHRwczovL2FwcHMuYXp1cmVpb3RjZW50cmFsLmNvbSIsImlzcyI6Imh0dHBzOi8vc3RzLndpbmRvd3MubmV0LzcyZjk4OGJmLTg2ZjEtNDFhZi05MWFiLTJkN2NkMDExZGI0Ny8iLCJpYXQiOjE1NjEwNDEwNTQsIm5iZiI6MTU2MTA0MTA1NCwiZXhwIjoxNTYxMDQ0OTU0LCJhY3IiOiIxIiwiYWlvIjoiQVZRQXEvOExBQUFBR204SXlGaGxDYzZhODJFZnNCQ3RkbHRCNTBtekpCRXlaQ0VxMDVBNDZJWURSMDdIUmRRSWloWmRob1dUdDkzOG9YemlqeExRNlc0bVFFL1gySm9tTmZsR0szS1J2RFpFUzlWK0RTWUY2bTA9IiwiYW1yIjpbInJzYSIsIndpYSIsIm1mYSJdLCJhcHBpZCI6IjA0YjA3Nzk1LThkZGItNDYxYS1iYmVlLTAyZjllMWJmN2I0NiIsImFwcGlkYWNyIjoiMCIsImZhbWlseV9uYW1lIjoiRHJ1ZGEiLCJnaXZlbl9uYW1lIjoiTHVjYSIsImlwYWRkciI6IjkzLjY1LjE3My44OCIsIm5hbWUiOiJMdWNhIERydWRhIiwib2lkIjoiMzgxMmM1ZTAtMGRkNi00NTYyLTlhZTYtZmQ4OTUxMjk4MTg0Iiwib25wcmVtX3NpZCI6IlMtMS01LTIxLTIxMjc1MjExODQtMTYwNDAxMjkyMC0xODg3OTI3NTI3LTIxMDk1NTk4IiwicHVpZCI6IjEwMDMzRkZGOTdBMjE1NzkiLCJzY3AiOiJ1c2VyX2ltcGVyc29uYXRpb24iLCJzdWIiOiJPbVNfZVNYOVFoUVNCcW4xbl9wcm8yN0h6QWNNeG9hRVVjcEpxcGRLbUMwIiwidGlkIjoiNzJmOTg4YmYtODZmMS00MWFmLTkxYWItMmQ3Y2QwMTFkYjQ3IiwidW5pcXVlX25hbWUiOiJsdWRydWRhQG1pY3Jvc29mdC5jb20iLCJ1cG4iOiJsdWRydWRhQG1pY3Jvc29mdC5jb20iLCJ1dGkiOiJrblVHaWUtMjlVU1poRWhSSEtjTUFBIiwidmVyIjoiMS4wIn0.EDBvKYcN4_PITDwLpGPah5i6Q4At1rMPl5H7IvVAjnLqA1uOuU5GG51tL85d73meTLS_N5TnT5dibZN1gh4FBa6NRtyAssb1GECBWmkaUwL6ian5I6V6CtlPgrBneCG2j1EuyU43HIW0xM4yX0eSM6FZG4EEW4BtqqCdppvYNB2hoxijJn5VTuY4gZxeqzxZc-ets7QyVGexDRuun9epZuIg0JhE9n38M8MwMehY69FuIUkpZmYAYYpLMUfhfCpVaXtIZCL2ASa4ll8gg7b7TRsYtCxpOwRDvgcZsDcEuaHdKGaCb7Qf5RQQ7vtJ6zD3YyK1uTmg8jpS2ojECuFDLQ";

        public MockAuthViewModel(INavigationService navigation) : base(navigation)
        {
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
