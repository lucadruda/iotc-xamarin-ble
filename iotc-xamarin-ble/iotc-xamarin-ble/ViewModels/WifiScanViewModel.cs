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
using iotc_xamarin_ble.Graphics;
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
        public bool IsiOS { get; set; }
        public bool PairingCompleted { get; set; }
        public Result PairingResult { get; set; }

        public ImageSource WiFIInstruction { get; set; }

        private bool isScanning;
        private string currentSSID;

        private UdpSocketClient udpClient;
        private UdpSocketReceiver udpReceiver;
        private DeviceCredentials credentials;
        private string sasKey;
        private bool skipScan;
        int port = 4000;
        string address = "192.168.0.1";
        string deviceName;

        bool pairStarted = false;
        private object m_lock = new object();

        public WifiScanViewModel(INavigationService navigation) : base(navigation)
        {
            Devices = new ObservableCollection<string>();
            wifiManager = DependencyService.Get<IWiFiManager>();
            IsPairing = false;
            Connect = new Command(StartConnect);
            Title = IoTCentral.Current.Device.Name;
            ProgressText = "Searching for compatible devices...";
            PairingCompleted = false;
            PairingResult = new Result();
            MessagingCenter.Subscribe<IWiFiManager, string>(this, "FOUND", async (manager, ssid) =>
            {
                Associate(ssid);
                await Pair();

            });
            MessagingCenter.Subscribe<IWiFiManager, string>(this, "REGISTERED", async (manager, msg) =>
            {
                var ssid = msg.Split(':')[0];
                if (ssid.Equals(deviceName, StringComparison.CurrentCultureIgnoreCase))
                {
                    ProgressText = $"Device registered on the network with ip: {msg.Split(':')[1]}";
                    PairingResult.Text = ((char)0x2713).ToString();
                    PairingResult.Color = Color.Green;


                }
                else
                {
                    ProgressText = $"Failed to register device";
                    PairingResult.Text = "X";
                    PairingResult.Color = Color.Red;
                }
                PairingCompleted = true;
                OnPropertyChanged("ProgressText");
                OnPropertyChanged("PairingCompleted");
                OnPropertyChanged("PairingResult");

                //await Navigation.NavigateTo(new DeviceViewModel(Navigation));
            });
            WiFIInstruction = ImageSource.FromResource("iotc_xamarin_ble.Resources.wifi_2.jpg");
            udpClient = new UdpSocketClient();
            udpReceiver = new UdpSocketReceiver();
            skipScan = false;
            if (Device.RuntimePlatform == Device.iOS)
            {
                IsiOS = true;
                ((App)App.Current).ApplicationResumed += ResumeWifi;
            }
        }

        public bool IsPairing
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

        private async void StartConnect()
        {
            if (skipScan) // running on iOS. Scan is not supported
            {
                IsPairing = true;
                OnPropertyChanged("IsPairing");
                await Pair();
                return;
            }
            credentials = await (await IoTCentral.Current.GetServiceClient()).GetCredentials(IoTCentral.Current.Application.Id);
            credentials.DeviceId = IoTCentral.Current.Device.DeviceId;
            using (var hmac = new HMACSHA256(Convert.FromBase64String(credentials.PrimaryKey))) //get device key
            {
                sasKey = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(credentials.DeviceId)));
            }
            IsPairing = true;
            OnPropertyChanged("IsPairing");
            await Scan();
        }


        public override async Task OnAppearing()
        {

            if ((await ContainerService.Current.Resolve<Services.Permissions.IPermissions>().CheckPermissions()) != PermissionStatus.Granted)
            {
                //inform the user and return;
            }
            currentSSID = wifiManager.GetConnectedAp();
            SSID = currentSSID;
            OnPropertyChanged("SSID");
        }

        private void ResumeWifi()
        {
            currentSSID = wifiManager.GetConnectedAp();
            if (currentSSID.StartsWith(Constants.WIFI_SSID_PREFIX))
            {
                IsiOS = false;
                IsPairing = false;
                OnPropertyChanged("IsiOS");
                OnPropertyChanged("IsPairing");
                // skip the instructions and go on with pairing
                skipScan = true;
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

        private void Associate(string ssid)
        {
            deviceName = ssid.Substring(ssid.IndexOf('_') + 1);
            ProgressText = $"Found MXCHIP device {deviceName}";
            OnPropertyChanged("ProgressText");
            wifiManager.Connect(ssid, "12345678");
        }

        private async Task Pair()
        {
            await Task.Delay(4000);
            ProgressText = $"Start pairing";
            OnPropertyChanged("ProgressText");
            // give some time to connect
            await Task.Delay(10000);
            var currentIp = wifiManager.GetCurrentIp();
            ProgressText = $"Current Ip address is {currentIp}";
            OnPropertyChanged("ProgressText");

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
                    await Task.Delay(500);
                    retry++;
                }
            }
            catch (SocketException ex)
            {
                ProgressText = $"Error Sending pairing message {ex.Message}";
                OnPropertyChanged("ProgressText");
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
            lock (m_lock)
            {
                if (pairStarted)
                {
                    return;
                }
                if (e.RemoteAddress == "192.168.0.1" && e.RemotePort == "4000" && Encoding.UTF8.GetString(e.ByteData) == "PAIRING")
                {
                    Task.Run(async () =>
                    {
                        pairStarted = true;
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
                            await Task.Delay(2000);
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
}
