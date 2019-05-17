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
        private IAdapter adapter;
        private static BLEService _service;
        private BLEService()
        {
            ble = CrossBluetoothLE.Current;
            adapter = CrossBluetoothLE.Current.Adapter;
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
            adapter.ScanTimeout = timeout;
        }

        public BluetoothState State { get { return ble.State; } }

        public bool IsScanning { get { return adapter.IsScanning; } }

        public async Task StartScan(Action<IDevice> onDeviceDiscovered)
        {
            if ((await CheckPermissions()) != PermissionStatus.Granted)
            {
                //inform the user and return;
            }
            EventHandler<DeviceEventArgs> run = delegate (object s, DeviceEventArgs e)
            {
                onDeviceDiscovered(e.Device);
            };
            //remember to cleanup event listeners
            adapter.DeviceDiscovered += run;
            await adapter.StartScanningForDevicesAsync();
            adapter.DeviceDiscovered -= run;
        }

        public async Task StopScan()
        {
            if (adapter.IsScanning)
                await adapter.StopScanningForDevicesAsync();
        }

        public async Task ConnectDevice(IDevice device)
        {
            try
            {
                await adapter.ConnectToDeviceAsync(device);
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


    }
}
