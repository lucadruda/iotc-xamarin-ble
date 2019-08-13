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
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"accessToken.txt");
            token = File.ReadAllText(path);
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
