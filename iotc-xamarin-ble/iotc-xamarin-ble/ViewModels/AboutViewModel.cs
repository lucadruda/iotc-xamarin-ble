using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using iotc_xamarin_ble.ViewModels.Navigation;
using Xamarin.Essentials;

namespace iotc_xamarin_ble.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public string About { get; set; }
        public string LicenseTerms { get; set; }
        public string Version { get; set; }
        public AboutViewModel(INavigationService navigation) : base(navigation)
        {
            Title = "About";
            Icon = "about";
            About = "About";
            LicenseTerms = "License terms";
            Version = AppInfo.VersionString;

        }
    }
}
