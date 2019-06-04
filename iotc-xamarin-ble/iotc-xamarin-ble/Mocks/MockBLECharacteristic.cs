using iotc_xamarin_ble.Helpers;
using Plugin.BLE.Abstractions;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Threading;
using System.Threading.Tasks;

namespace iotc_xamarin_ble.Mocks
{
    public class MockBLECharacteristic : ICharacteristic
    {

        private bool notifying = false;
        private System.Timers.Timer charTimer;

        public MockBLECharacteristic(IService service)
        {
            Id = new Guid();
            Uuid = Id.ToString();
            Service = service;
            charTimer = new System.Timers.Timer(2000);
            charTimer.Elapsed += (s, e) =>
            {
                ValueUpdated(this, new CharacteristicUpdatedEventArgs(this));
            };
        }

        public MockBLECharacteristic(IService service, Guid guid) : this(service)
        {
            Id = guid;
            Uuid = Id.ToString();
        }
        public Guid Id { get; set; }

        public string Uuid { get; set; }

        public string Name { get; set; } = Utils.GetRandomString(8);

        public byte[] Value
        {
            get
            {
                byte[] b = new byte[1];
                new Random().NextBytes(b);
                return b;
            }
        }

        public string StringValue => Encoding.Default.GetString(Value);
        public CharacteristicPropertyType Properties => CharacteristicPropertyType.Notify;

        public CharacteristicWriteType WriteType { get; set; }

        public bool CanRead => true;

        public bool CanWrite => true;

        public bool CanUpdate => true;

        public IService Service { get; set; }

        public event EventHandler<CharacteristicUpdatedEventArgs> ValueUpdated;

        public Task<IDescriptor> GetDescriptorAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return Task.FromResult<IDescriptor>(new MockBLEDescriptor(this));
        }

        public Task<IList<IDescriptor>> GetDescriptorsAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult((IList<IDescriptor>)new List<IDescriptor> { new MockBLEDescriptor(this), new MockBLEDescriptor(this) });
        }

        public Task<byte[]> ReadAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(Value);
        }

        public Task StartUpdatesAsync()
        {
            charTimer.Enabled = true;
            return Task.CompletedTask;
        }

        public Task StopUpdatesAsync()
        {
            charTimer.Enabled = false;
            return Task.CompletedTask;
        }

        public Task<bool> WriteAsync(byte[] data, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(new Random().NextDouble() > 0.5);
        }
    }
}
