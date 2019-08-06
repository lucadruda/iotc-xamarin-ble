using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Web;
using System.Windows.Input;
using iotc_ble_xamarin;
using iotc_csharp_service;
using iotc_xamarin_ble.Messages;
using iotc_xamarin_ble.Services;
using iotc_xamarin_ble.Services.Container;
using iotc_xamarin_ble.Services.Permissions;
using iotc_xamarin_ble.ViewModels.Navigation;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.Permissions.Abstractions;
using Sockets.Plugin;
using Sockets.Plugin.Abstractions;
using Xamarin.Forms;

namespace iotc_xamarin_ble.ViewModels
{
    public class WifiScanViewModel : BaseViewModel, IScanViewModel
    {
        private string _lastTappedItem;
        public bool close = false;
        private IWiFiManager wifiManager;

        public ICommand Connect { get; set; }


        public string Password { get; set; }
        public string SSID { get; set; }
        public bool Found { get; set; }
        public string ProgressText { get; set; }

        private bool isScanning;
        private string currentSSID;

        private UdpSocketClient udpClient;
        private UdpSocketReceiver udpReceiver;
        private DeviceCredentials credentials;
        private string sasKey;
        int port = 4000;
        string address = "192.168.0.1";

        public WifiScanViewModel(INavigationService navigation) : base(navigation)
        {
            Devices = new ObservableCollection<string>();
            wifiManager = DependencyService.Get<IWiFiManager>();
            IsScanning = false;
            Connect = new Command(StartConnect);
            ProgressText = "Searching for compatible devices...";
            MessagingCenter.Subscribe<IWiFiManager, string>(this, "FOUND", async (manager, ssid) =>
            {
                OnPropertyChanged("IsScanning");
                credentials = await (await IoTCentral.Current.GetServiceClient()).GetCredentials(IoTCentral.Current.Application.Id);
                credentials.DeviceId = IoTCentral.Current.Device.DeviceId;
                using (var hmac = new HMACSHA256(Convert.FromBase64String(credentials.PrimaryKey))) //get device key
                {
                    sasKey = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(credentials.DeviceId)));
                }
                await Pair(ssid);

            });
            MessagingCenter.Subscribe<IWiFiManager, string>(this, "REGISTERED", (manager, ip) =>
           {
               ProgressText = $"Device registered on the network with ip: {ip}";
               OnPropertyChanged("ProgressText");

           });

            udpClient = new UdpSocketClient();
            udpReceiver = new UdpSocketReceiver();
        }

        public bool IsScanning
        {
            get { return isScanning; }
            set
            {
                if (isScanning != value)
                {
                    isScanning = value;
                    ScanBtn = isScanning ? "Stop" : "Scan";
                    OnPropertyChanged();
                    OnPropertyChanged("ScanBtn");
                }
            }
        }

        public string ScanBtn { get; set; }


        public ObservableCollection<string> Devices { get; set; }


        public string LastTappedItem
        {
            get { return _lastTappedItem; }
            set
            {
                if (value != null)
                {
                    {
                        _lastTappedItem = value;
                        OnItemTapped();
                    }
                }
            }
        }
        public void OnItemTapped()
        {
            //Navigation.NavigateTo(new BLEDetailsViewModel(Navigation, this));
        }

        private void StartConnect()
        {
            IsScanning = true;
            OnPropertyChanged("IsScanning");
            currentSSID = wifiManager.GetConnectedAp();
            Scan();
        }


        public override async Task OnAppearing()
        {
            if ((await ContainerService.Current.Resolve<Services.Permissions.IPermissions>().CheckPermissions()) != PermissionStatus.Granted)
            {
                //inform the user and return;
            }
        }

        public void Close()
        {
            close = true;
        }

        public Task Scan()
        {
            return Task.Run(() => wifiManager.Scan());
        }

        public Task Stop()
        {
            throw new NotImplementedException();
        }

        private async Task Pair(string ssid)
        {
            ProgressText = $"Found MXCHIP device {ssid.Substring(ssid.IndexOf('_') + 1)}";
            OnPropertyChanged("ProgressText");

            wifiManager.Connect(ssid);
            // give some time to connect
            await Task.Delay(3000);
            var currentIp = wifiManager.GetCurrentIp();

            var msg = $"IOTC:{currentIp}:5000";
            var msgBytes = Encoding.UTF8.GetBytes(msg);

            udpReceiver.MessageReceived += StartSendCredentials;
            await udpReceiver.StartListeningAsync(5000);

            int retry = 0;
            try
            {
                while (retry < 5)
                {
                    await udpClient.SendToAsync(msgBytes, address, port);
                    await Task.Delay(1000);
                    retry++;
                }
            }
            catch (SocketException ex)
            {

            }
        }

        public override Task OnNavigatingBack()
        {
            return base.OnNavigatingBack();
        }

        public void Reset()
        {
            if (wifiManager != null && currentSSID != null)
            {
                wifiManager.Connect(currentSSID);
            }
        }

        private void StartSendCredentials(object source, UdpSocketMessageReceivedEventArgs e)
        {
            if (e.RemoteAddress == "192.168.0.1" && e.RemotePort == "4000" && Encoding.UTF8.GetString(e.ByteData) == "PAIRING")
            {
                Task.Run(async () =>
                {
                    int retry = 0;
                    ProgressText = $"Sending information to the device";
                    OnPropertyChanged("ProgressText");
                    udpReceiver.MessageReceived -= StartSendCredentials;
                    var msg = $"SSID={HttpUtility.UrlEncode(Encoding.UTF8.GetBytes(SSID))};PASS={HttpUtility.UrlEncode(Encoding.UTF8.GetBytes(Password))};SASKEY={HttpUtility.UrlEncode(Encoding.UTF8.GetBytes(sasKey))};SCOPEID={HttpUtility.UrlEncode(Encoding.UTF8.GetBytes(credentials.IdScope))};DEVICEID={HttpUtility.UrlEncode(Encoding.UTF8.GetBytes(credentials.DeviceId))};AUTH=S";
                    var msgBytes = Encoding.UTF8.GetBytes(msg);
                    await Task.Delay(2000);
                    try
                    {
                        while (retry < 5)
                        {
                            await udpClient.SendToAsync(msgBytes, address, port);
                            await Task.Delay(2000);
                            retry++;
                        }
                        wifiManager.Connect(currentSSID);
                        ProgressText = "Waiting for the device to connect to network...";
                        OnPropertyChanged("ProgressText");
                        await Task.Delay(6000);
                        wifiManager.ReceiveBroadcast();
                    }
                    catch (SocketException ex)
                    {

                    }
                });
            }
        }
    }
}
