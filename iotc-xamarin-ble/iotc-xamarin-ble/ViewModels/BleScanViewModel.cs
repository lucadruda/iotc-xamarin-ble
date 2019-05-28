using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using iotc_xamarin_ble.Services;
using iotc_xamarin_ble.ViewModels.Navigation;
using Plugin.BLE.Abstractions.Contracts;
using Xamarin.Forms;

namespace iotc_xamarin_ble.ViewModels
{
    public class BleScanViewModel : BaseViewModel
    {
        private IDevice _lastTappedItem;
        private Timer scanTimer;

        public ICommand ScanStop { get; set; }

        private bool isScanning;
        public BleScanViewModel(INavigationService navigation) : base(navigation)
        {
            Devices = new ObservableCollection<IDevice>();
            ScanBtn = "Stop";
            ScanStop = new Command(async () =>
            {
                if (ScanBtn == "Scan")
                    await Scan();
                else await Stop();
            });

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


        public ObservableCollection<IDevice> Devices { get; set; }


        public IDevice LastTappedItem
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
            BLEService.Current.Device = LastTappedItem;
            LastTappedItem = null;
            OnPropertyChanged("LastTappedItem");
            Navigation.NavigateTo(new BLEDetailsViewModel(Navigation));
        }

        public async Task Scan()
        {
            Devices.Clear();
            await BLEService.Current.StartScan(OnDeviceScanned);
        }

        public async Task Stop()
        {
            await BLEService.Current.StopScan();
        }


        public override async Task OnAppearing()
        {

            scanTimer = new Timer(1000);
            scanTimer.Elapsed += (s, e) =>
            {
                IsScanning = BLEService.Current.IsScanning;
            };
            scanTimer.Enabled = true;
            await Scan();
        }

        private void OnDeviceScanned(IDevice device)
        {
            if (device.Name != null)
                Devices.Add(device);
        }

        public override Task AfterDismissed()
        {
            PageCompleted -= OnNavigationBack;
            scanTimer.Dispose();
            return base.AfterDismissed();
        }

        public override async void OnNavigationBack(object sender, object e)
        {
            await Navigation.NavigateBack();
        }

        public override Task BeforeFirstShown()
        {
            PageCompleted += OnNavigationBack;
            return base.BeforeFirstShown();
        }
    }
}
