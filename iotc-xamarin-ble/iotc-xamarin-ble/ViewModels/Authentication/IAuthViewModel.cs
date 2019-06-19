using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using iotc_xamarin_ble.ViewModels.Navigation;

namespace iotc_xamarin_ble.ViewModels.Authentication
{
    public interface IAuthViewModel
    {
        
        Task<string> GetTokenAsync();
        Task<string> GetTokenAsync(string resource);

        Task<string> GetTokenAsync(string resource, string tenant);


    }
}
