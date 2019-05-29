using iotc_xamarin_ble.Helpers;
using Plugin.BLE.Abstractions;
using Plugin.BLE.Abstractions.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace iotc_xamarin_ble.Mocks
{
    public class MockBLEDevice : IDevice
    {

        public MockBLEDevice()
        {
            NativeDevice = new MockBLENativeDevice(Name, Utils.GetRandomMac(":"));
        }
        public Guid Id => Guid.NewGuid();

        public string Name => Utils.GetRandomString(8);

        public int Rssi => (-100 + new Random().Next(50));

        public object NativeDevice { get; set; }

        public IList<IService> Services { get; set; }

        public DeviceState State => DeviceState.Connected;

        public IList<AdvertisementRecord> AdvertisementRecords => new List<AdvertisementRecord>();

        public void Dispose()
        {
            return;
        }

        public Task<IService> GetServiceAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(Services.FirstOrDefault(c => c.Id == id));
        }

        public Task<IList<IService>> GetServicesAsync(CancellationToken cancellationToken = default)
        {
            Services = new List<IService> { new MockBLEService(this), new MockBLEService(this), new MockBLEService(this) };
            return Task.FromResult(Services);
        }

        public Task<int> RequestMtuAsync(int requestValue)
        {
            return Task.FromResult(requestValue);
        }

        public bool UpdateConnectionInterval(ConnectionInterval interval)
        {
            return true;
        }

        public Task<bool> UpdateRssiAsync()
        {
            return Task.FromResult(true);

        }
    }
}
