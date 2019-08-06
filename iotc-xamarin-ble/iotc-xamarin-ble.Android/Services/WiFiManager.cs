using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Net.Wifi;
using Android.Text.Format;
using iotc_xamarin_ble.Droid.Services;
using iotc_xamarin_ble.Services;
using Java.Net;
using Xamarin.Forms;

[assembly: Dependency(typeof(WiFiManager))]

namespace iotc_xamarin_ble.Droid.Services
{
    public class WiFiManager : IWiFiManager
    {
        public static WifiManager Wifi { get; set; }
        public static List<string> Devices { get; set; }

        private static int currentNetwork = -1;
        public WiFiManager()
        {
            var context = Android.App.Application.Context;
            Wifi = (WifiManager)context.GetSystemService(Context.WifiService);
            context.RegisterReceiver(new WifiReceiver(this), new IntentFilter(WifiManager.ScanResultsAvailableAction));
            Devices = new List<string>();
        }
        public void Scan()
        {

            // Start a scan and register the Broadcast receiver to get the list of Wifi Networks
            Wifi.StartScan();
        }


        public void Connect(string ssid, string passphrase = null)
        {
            var conf = new WifiConfiguration
            {
                Ssid = ssid
            };
            // very important for unprotected networks otherwise connection doesn't go on
            if (passphrase == null)
                conf.AllowedKeyManagement.Set((int)KeyManagementType.None);

            var netId = Wifi.AddNetwork(conf);
            currentNetwork = Wifi.ConnectionInfo.NetworkId;
            Wifi.Disconnect();
            var connected = Wifi.EnableNetwork(netId, true);
            if (connected)
            {
                Wifi.Reconnect();
            }
        }

        public string GetConnectedAp()
        {
            return Wifi.ConnectionInfo.SSID;
        }

        public async void ReceiveBroadcast()
        {
            using (var client = new UdpClient())
            {
                client.EnableBroadcast = true;
                client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                client.ExclusiveAddressUse = false; // only if you want to send/receive on same machine
                client.Client.Bind(new IPEndPoint(IPAddress.Any, 9000));
                await Task.Run(async () =>
                {
                    while (true)
                    {
                        var recvBuffer = await client.ReceiveAsync();
                        MessagingCenter.Send((IWiFiManager)this, "REGISTERED", Encoding.UTF8.GetString(recvBuffer.Buffer));
                        return;
                    }
                });
                client.Close();
            }
        }

        public string GetCurrentIp()
        {
            var ip = Wifi.ConnectionInfo.IpAddress;
            return $"{(ip & 0xff)}.{(ip >> 8 & 0xff)}.{(ip >> 16 & 0xff)}.{(ip >> 24 & 0xff)}";
        }

        public class WifiReceiver : BroadcastReceiver
        {
            private readonly WiFiManager manager;

            public WifiReceiver(WiFiManager manager)
            {
                this.manager = manager;
            }
            public override void OnReceive(Context context, Intent intent)
            {
                IList<ScanResult> scanwifinetworks = Wifi.ScanResults;
                foreach (ScanResult wifinetwork in scanwifinetworks)
                {
                    if (wifinetwork.Ssid.StartsWith("AZ3166"))
                    {
                        MessagingCenter.Send((IWiFiManager)this.manager, "FOUND", wifinetwork.Ssid);

                    }
                }

            }
        }
    }


}