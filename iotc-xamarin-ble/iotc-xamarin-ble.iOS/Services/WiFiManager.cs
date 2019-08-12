using Foundation;
using iotc_xamarin_ble.iOS.Services;
using iotc_xamarin_ble.Services;
using NetworkExtension;
using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using SystemConfiguration;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(WiFiManager))]
namespace iotc_xamarin_ble.iOS.Services
{
    public class WiFiManager : IWiFiManager
    {
        public void Connect(string ssid, string passphrase = null)
        {
            throw new NotImplementedException();
        }

        public string GetConnectedAp()
        {
            //foreach (var network in NEHotspotHelper.SupportedNetworkInterfaces)
            //{
            //    if (!string.IsNullOrEmpty(network.Ssid))
            //        return network.Ssid;
            //}
            //return string.Empty; //check for iOS 11+
            NSDictionary dict;
            var status = CaptiveNetwork.TryCopyCurrentNetworkInfo("en0", out dict);
            if (status == StatusCode.NoKey)
            {
                return string.Empty;
            }

            var bssid = dict[CaptiveNetwork.NetworkInfoKeyBSSID];
            var ssid = dict[CaptiveNetwork.NetworkInfoKeySSID];
            var ssiddata = dict[CaptiveNetwork.NetworkInfoKeySSIDData];

            return ssid.ToString();
        }

        public string GetCurrentIp()
        {
            String ipAddress = "";

            foreach (var netInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (netInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 ||
                    netInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    foreach (var addrInfo in netInterface.GetIPProperties().UnicastAddresses)
                    {
                        if (addrInfo.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            ipAddress = addrInfo.Address.ToString();

                        }
                    }
                }
            }

            return ipAddress;
        }

        public void ReceiveBroadcast()
        {
            throw new NotImplementedException();
        }

        public void Scan()
        {
            var WiFiURL = new NSUrl("prefs:root=WIFI");
            if (UIApplication.SharedApplication.CanOpenUrl(WiFiURL))
            {   //Pre iOS 10
                UIApplication.SharedApplication.OpenUrl(WiFiURL);
            }
            else
            {   //iOS 10    UIApplication.SharedApplication.OpenUrl(new NSUrl("App-Prefs:root=WIFI"));
            }

        }
    }
}