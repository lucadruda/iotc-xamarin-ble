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
using XamarinDevice = Xamarin.Forms.Device;

namespace iotc_xamarin_ble.Services
{
    public class BLEService
    {
        private IBluetoothLE ble;
        private static BLEService _service;
        public BLEService()
        {
            ble = CrossBluetoothLE.Current;
            Adapter = ((bool)App.Current.Properties["mocked"]) ? new Mocks.MockBLEAdapter() : CrossBluetoothLE.Current.Adapter;
        }

        public BLEService(int timeout) : this()
        {
            Adapter.ScanTimeout = timeout;
        }

        public BluetoothState State { get { return ble.State; } }

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

        public async Task Connect(IDevice device)
        {
            try
            {
                await Adapter.ConnectToDeviceAsync(device);
            }
            catch (DeviceConnectionException e) { }
        }

        public async Task<IDevice> Connect(string deviceId)
        {
            try
            {
                return await Adapter.ConnectToKnownDeviceAsync(new Guid(deviceId));
            }
            catch (DeviceConnectionException e)
            {
                return null;
            }
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
            if (XamarinDevice.RuntimePlatform == XamarinDevice.iOS)
            {
                return PermissionStatus.Granted;
            }
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
