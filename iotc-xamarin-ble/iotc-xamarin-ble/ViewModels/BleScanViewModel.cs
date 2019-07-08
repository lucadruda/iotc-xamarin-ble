using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using iotc_ble_xamarin;
using iotc_xamarin_ble.Messages;
using iotc_xamarin_ble.Services;
using iotc_xamarin_ble.ViewModels.Navigation;
using Plugin.BLE.Abstractions.Contracts;
using Xamarin.Forms;

namespace iotc_xamarin_ble.ViewModels
{
    public class BleScanViewModel : BaseViewModel
    {
        private IDevice _lastTappedItem;
        public bool close = false;

        public ICommand ScanStop { get; set; }

        public BLEService BleService { get; set; }

        private bool isScanning;
        public BleScanViewModel(INavigationService navigation) : base(navigation)
        {
            Title = "Scan devices";
            Icon = "home";
            Devices = new ObservableCollection<IDevice>();
            BleService = new BLEService();
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
            Navigation.NavigateTo(new BLEDetailsViewModel(Navigation, this));
        }

        public async Task Scan()
        {
            Devices.Clear();
            await BleService.StartScan(OnDeviceDiscovered);
            IsScanning = true;
        }

        public async Task Stop()
        {
            await BleService.StopScan();
            IsScanning = false;
        }


        public override async Task OnAppearing()
        {
            await Scan();
            await base.OnAppearing();
        }

        public void Close()
        {
            close = true;
        }

        private void OnDeviceDiscovered(IDevice device)
        {
            if (device.Name != null)
                Devices.Add(device);
        }

        public override Task AfterDismissed()
        {
            return base.AfterDismissed();
        }

        public override Task OnNavigatingBack()
        {
            LastTappedItem = null;
            OnPropertyChanged("LastTappedItem");
            return base.OnNavigatingBack();
        }

        public override Task BeforeFirstShown()
        {
            return base.BeforeFirstShown();
        }
    }
}
