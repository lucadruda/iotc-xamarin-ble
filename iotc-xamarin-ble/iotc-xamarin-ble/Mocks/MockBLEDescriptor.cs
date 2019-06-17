using iotc_xamarin_ble.Helpers;
using Plugin.BLE.Abstractions.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace iotc_xamarin_ble.Mocks
{
    public class MockBLEDescriptor : IDescriptor
    {

        public MockBLEDescriptor(ICharacteristic characteristic)
        {
            Characteristic = characteristic;
        }
        public Guid Id => Guid.NewGuid();


        public string Name => Utils.GetRandomString(8);

        public byte[] Value
        {
            get
            {
                byte[] b = new byte[2];
                new Random().NextBytes(b);
                return b;
            }
        }

        public ICharacteristic Characteristic { get; set; }
        public Task<byte[]> ReadAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(Value);
        }

        public Task WriteAsync(byte[] data, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}
