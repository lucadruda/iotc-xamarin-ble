using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;
using Plugin.BLE.Abstractions.Exceptions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace iotc_xamarin_ble.Services
{
    class BLEService
    {
        private IBluetoothLE ble;
        private static BLEService _service;
        private BLEService()
        {
            ble = CrossBluetoothLE.Current;
            Adapter = ((bool)App.Current.Properties["mocked"]) ? new Mocks.MockBLEAdapter() : CrossBluetoothLE.Current.Adapter;
        }

        public static BLEService Current
        {
            get
            {
                if (_service == null)
                {
                    _service = new BLEService();
                }
                return _service;
            }
        }
        public BLEService(int timeout) : this()
        {
            Adapter.ScanTimeout = timeout;
        }

        public BluetoothState State { get { return ble.State; } }
        public IDevice Device { get; set; }

        public bool IsScanning { get { return Adapter.IsScanning; } }

        public IAdapter Adapter { get; set; }

        public EventHandler<CharacteristicUpdatedEventArgs> OnValueAvailable { get; set; }

        public async Task StartScan(Action<IDevice> onDeviceDiscovered)
        {
            if ((await CheckPermissions()) != PermissionStatus.Granted)
            {
                //inform the user and return;
            }
            void run(object s, DeviceEventArgs e)
            {
                onDeviceDiscovered(e.Device);
            }
            //remember to cleanup event listeners
            Adapter.DeviceDiscovered += run;
            await Adapter.StartScanningForDevicesAsync();
            Adapter.DeviceDiscovered -= run;
        }

        public async Task StopScan()
        {
            if (Adapter.IsScanning)
                await Adapter.StopScanningForDevicesAsync();
        }

        public async Task ConnectDevice(IDevice device)
        {
            try
            {
                await Adapter.ConnectToDeviceAsync(device);
            }
            catch (DeviceConnectionException e) { }
        }

        public async Task DisconnectDevice(IDevice device)
        {
            try
            {
                await Adapter.DisconnectDeviceAsync(device);
            }
            catch (DeviceConnectionException e) { }
        }

        private async Task<PermissionStatus> CheckPermissions()
        {
            var permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
            if (permissionStatus == PermissionStatus.Granted)
            {
                return permissionStatus;
            }
            var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
            if (results.ContainsKey(Permission.Location))
                permissionStatus = results[Permission.Location];
            return permissionStatus;
        }

        public async Task EnableNotification(ICharacteristic characteristic)
        {
            characteristic.ValueUpdated += OnValueAvailable;
            await characteristic.StartUpdatesAsync();
        }

        public async Task DisableNotification(ICharacteristic characteristic)
        {
            characteristic.ValueUpdated -= OnValueAvailable;
            await characteristic.StopUpdatesAsync();
        }


    }
}
