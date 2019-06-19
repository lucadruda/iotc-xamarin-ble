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
        string token = "";

        public MockAuthViewModel(INavigationService navigation) : base(navigation)
        {
        }

        public Task<string> GetTokenAsync()
        {
            return Task.FromResult(token);
        }

        public Task<string> GetTokenAsync(string resource)
        {
            return Task.FromResult(token);

        }

        public Task<string> GetTokenAsync(string resource, string tenant)
        {
            return Task.FromResult(token);
        }
    }
}
