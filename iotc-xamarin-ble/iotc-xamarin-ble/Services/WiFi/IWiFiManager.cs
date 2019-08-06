using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace iotc_xamarin_ble.Services
{
    public interface IWiFiManager
    {
        void Scan();
        void Connect(string ssid, string passphrase = null);

        string GetConnectedAp();

        void ReceiveBroadcast();

        string GetCurrentIp();
    }
}
