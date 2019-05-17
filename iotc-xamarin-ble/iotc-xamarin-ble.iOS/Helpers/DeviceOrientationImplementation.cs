using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using iotc_ble_xamarin.Helpers;
using iotc_xamarin_ble.iOS.Helpers;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(DeviceOrientationImplementation))]
namespace iotc_xamarin_ble.iOS.Helpers
{

    public class DeviceOrientationImplementation : IDeviceOrientation
    {
        public DeviceOrientationImplementation() { }

        public DeviceOrientations GetOrientation()
        {
            var currentOrientation = UIApplication.SharedApplication.StatusBarOrientation;
            bool isPortrait = currentOrientation == UIInterfaceOrientation.Portrait
                || currentOrientation == UIInterfaceOrientation.PortraitUpsideDown;

            return isPortrait ? DeviceOrientations.Portrait : DeviceOrientations.Landscape;
        }
    }
}