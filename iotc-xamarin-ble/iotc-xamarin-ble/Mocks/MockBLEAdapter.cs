using Plugin.BLE.Abstractions;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace iotc_xamarin_ble.Mocks
{
    public class MockBLEAdapter : IAdapter
    {
        private System.Timers.Timer scanTimer;

        private Guid[] deviceIds = { Guid.Parse("e76506f0-06da-4705-83af-451d62b6f061"), Guid.Parse("fb274836-c071-4f82-aefc-cf3cf5ee2e91"), Guid.Parse("95870de3-d4ea-4210-b5ef-93bf3df555fc"), Guid.Parse("ec74c0bd-b036-49aa-8048-db78b589348d") };
        public MockBLEAdapter()
        {
            // DiscoveredDevices = new List<IDevice> { new CustomBLEDevice(this) };
            DiscoveredDevices = new List<IDevice> { new MockBLEDevice(deviceIds[0]), new MockBLEDevice(deviceIds[1]), new MockBLEDevice(deviceIds[2]), new MockBLEDevice(deviceIds[3]) };
            ConnectedDevices = DiscoveredDevices;
            scanTimer = new System.Timers.Timer(500);
        }

        private int scansCount = 0;
        public bool IsScanning { get; set; }

        public int ScanTimeout { get; set; }
        public ScanMode ScanMode { get; set; }

        public IList<IDevice> DiscoveredDevices { get; set; }

        public IList<IDevice> ConnectedDevices { get; set; }

        public event EventHandler<DeviceEventArgs> DeviceAdvertised;
        public event EventHandler<DeviceEventArgs> DeviceDiscovered;
        public event EventHandler<DeviceEventArgs> DeviceConnected;
        public event EventHandler<DeviceEventArgs> DeviceDisconnected;
        public event EventHandler<DeviceErrorEventArgs> DeviceConnectionLost;
        public event EventHandler ScanTimeoutElapsed;


        public Task ConnectToDeviceAsync(IDevice device, ConnectParameters connectParameters = default, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task<IDevice> ConnectToKnownDeviceAsync(Guid deviceGuid, ConnectParameters connectParameters = default, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(DiscoveredDevices.FirstOrDefault(d => d.Id == deviceGuid));
        }

        public Task DisconnectDeviceAsync(IDevice device)
        {
            return Task.CompletedTask;

        }

        public List<IDevice> GetSystemConnectedOrPairedDevices(Guid[] services = null)
        {
            if (services != null)
            {
                return DiscoveredDevices.Where(d => services.Contains(d.Id)).ToList();
            }
            return DiscoveredDevices.ToList();
        }

        public Task StartScanningForDevicesAsync(Guid[] serviceUuids = null, Func<IDevice, bool> deviceFilter = null, bool allowDuplicatesKey = false, CancellationToken cancellationToken = default)
        {

            scanTimer.Elapsed += (s, e) =>
            {
                var dev = new DeviceEventArgs();
                dev.Device = DiscoveredDevices[scansCount];
                this.DeviceDiscovered?.Invoke(this, dev);
                scansCount++;
                if (scansCount == 4)
                {
                    scanTimer.Enabled = false;
                    IsScanning = false;
                    scansCount = 0;
                }
            };
            scanTimer.Enabled = true;
            IsScanning = true;

            return Task.Run(() =>
            {
                while (IsScanning) ;
            });
        }

        public Task StopScanningForDevicesAsync()
        {
            scanTimer.Enabled = false;
            IsScanning = false;
            scansCount = 0;
            return Task.CompletedTask;

        }
    }
}
