using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using iotc_xamarin_ble.iOS.Helpers;
using iotc_xamarin_ble.Services.Cookies;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(Cookies))]
namespace iotc_xamarin_ble.iOS.Helpers
{
    public class Cookies : ICookies
    {
        public void Clear()
        {
            NSHttpCookieStorage CookieStorage = NSHttpCookieStorage.SharedStorage;
            foreach (var cookie in CookieStorage.Cookies)
                CookieStorage.DeleteCookie(cookie);
        }
    }
}