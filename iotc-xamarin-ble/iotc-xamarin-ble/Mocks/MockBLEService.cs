using iotc_xamarin_ble.Helpers;
using Plugin.BLE.Abstractions.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iotc_xamarin_ble.Mocks
{
    public class MockBLEService : IService
    {
        public MockBLEService(IDevice device)
        {
            Device = device;
            Id = new Guid();
        }

        public MockBLEService(IDevice device, Guid guid) : this(device)
        {
            Id = guid;
        }
        public Guid Id { get; set; }

        public string Name { get; set; } = Utils.GetRandomString(8);

        public bool IsPrimary => true;

        public IDevice Device { get; set; }

        public IList<ICharacteristic> Characteristics { get; set; }

        public void Dispose()
        {
            return;
        }

        public Task<ICharacteristic> GetCharacteristicAsync(Guid id)
        {
            return Task.FromResult(Characteristics.FirstOrDefault(c => c.Id == id));
        }

        public Task<IList<ICharacteristic>> GetCharacteristicsAsync()
        {
            Characteristics = new List<ICharacteristic> { new MockBLECharacteristic(this, new Guid($"00000001{Id.ToString().Substring(8)}")), new MockBLECharacteristic(this, new Guid($"00000010{Id.ToString().Substring(8)}")), new MockBLECharacteristic(this, new Guid($"00000011{Id.ToString().Substring(8)}")) };
            return Task.FromResult(Characteristics);
        }
    }
}
