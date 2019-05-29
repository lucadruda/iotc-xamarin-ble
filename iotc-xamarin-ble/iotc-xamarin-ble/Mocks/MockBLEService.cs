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
            Characteristics = new List<ICharacteristic> { new MockBLECharacteristic(this), new MockBLECharacteristic(this), new MockBLECharacteristic(this) };
        }
        public Guid Id { get; set; } = Guid.NewGuid();

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
            return Task.FromResult(Characteristics);
        }
    }
}
