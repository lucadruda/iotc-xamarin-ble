using System;
using System.Collections.Generic;
using System.Text;
using iotc_xamarin_ble.ViewModels.Navigation;

namespace iotc_xamarin_ble.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel(INavigationService navigation) : base(navigation)
        {
            Title = "About";
            Icon = "about";
        }
    }
}
