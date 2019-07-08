using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows.Input;
using iotc_ble_xamarin;
using iotc_xamarin_ble.Messages;
using iotc_xamarin_ble.ViewModels.Navigation;
using Plugin.BLE.Abstractions.Contracts;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace iotc_xamarin_ble.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public string About { get; set; }
        public string LicenseTerms { get; set; }
        public string Version { get; set; }
        public string Support { get; set; }
        public string Faq { get; set; }
        public string BluetoothInfo { get; set; }
        public string DeviceName { get; set; }
        public string ProjectPage { get; set; }
        public string Help { get; set; }

        public ICommand OpenLicense { get; set; }
        public ICommand ContactSupport { get; set; }
        public AboutViewModel(INavigationService navigation) : base(navigation)
        {
            Title = "About";
            Icon = "about";
            About = "About";
            LicenseTerms = "License terms";
            Version = AppInfo.VersionString;
            Support = "Contact support";
            Faq = "FAQs";
            BluetoothInfo = "Bluetooth info";
            DeviceName = "N/A";
            Help = "Help";
            ProjectPage = "<a href=\"https://github.com/lucadruda/iotc-xamarin-ble\"></a>";
            OpenLicense = new Command(() =>
            {
                // navigate to license page
            });
            ContactSupport = new Command(() =>
            {
                // navigate to license page
            });

            MessagingCenter.Subscribe<ResultMessage<IDevice>>(this, Constants.BLE_DEVICE_READY, OnDeviceConnected);


        }

        private void OnDeviceConnected(ResultMessage<IDevice> obj)
        {
            DeviceName = obj.Data.Name;
            OnPropertyChanged("DeviceName");
        }


    }
}
