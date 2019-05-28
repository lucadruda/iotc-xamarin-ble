using Plugin.BLE.Abstractions;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace iotc_xamarin_ble.Mocks
{
    public class CustomBLEDevice : DeviceBase
    {
        public CustomBLEDevice(IAdapter adapter) : base(adapter) { }
        public override object NativeDevice => throw new NotImplementedException();

        public override Task<bool> UpdateRssiAsync()
        {
            throw new NotImplementedException();
        }

        protected override async Task<IEnumerable<IService>> GetServicesNativeAsync()
        {
            var services = new List<IService>();
            services.Add(new CustomBLEService(this));
            services.Add(new CustomBLEService(this));
            services.Add(new CustomBLEService(this));
            return services;

        }

        protected override DeviceState GetState()
        {
            return DeviceState.Connected;
        }

        protected override async Task<int> RequestMtuNativeAsync(int requestValue)
        {
            throw new NotImplementedException();
        }

        protected override bool UpdateConnectionIntervalNative(ConnectionInterval interval)
        {
            throw new NotImplementedException();
        }
    }

    public class CustomBLEService : ServiceBase
    {
        public CustomBLEService(IDevice device) : base(device) { }

        public override Guid Id => new Guid();

        public override bool IsPrimary => true;

        protected override async Task<IList<ICharacteristic>> GetCharacteristicsNativeAsync()
        {
            var chars = new List<ICharacteristic>();
            chars.Add(new CustomBLECharacteristic(this));
            chars.Add(new CustomBLECharacteristic(this));
            chars.Add(new CustomBLECharacteristic(this));
            return chars;
        }
    }

    public class CustomBLECharacteristic : CharacteristicBase
    {
        public class WriteOperation
        {
            public byte[] Value { get; }
            public CharacteristicWriteType WriteType { get; }

            public WriteOperation(byte[] value, CharacteristicWriteType writeType)
            {
                Value = value;
                WriteType = writeType;
            }
        }

        public CustomBLECharacteristic(IService service = null) : base(service)
        {
        }

        public CharacteristicPropertyType MockProperties { get; set; }
        public byte[] MockValue { get; set; }
        public List<WriteOperation> WriteHistory { get; } = new List<WriteOperation>();


        public override event EventHandler<CharacteristicUpdatedEventArgs> ValueUpdated;
        public override Guid Id { get; } = Guid.Empty;
        public override string Uuid { get; } = string.Empty;
        public override byte[] Value => MockValue;

        public override CharacteristicPropertyType Properties => MockProperties;

        protected override Task<IList<IDescriptor>> GetDescriptorsNativeAsync()
        {
            throw new NotImplementedException();
        }

        protected override Task<byte[]> ReadNativeAsync()
        {
            throw new NotImplementedException();
        }

        protected override Task<bool> WriteNativeAsync(byte[] data, CharacteristicWriteType writeType)
        {
            WriteHistory.Add(new WriteOperation(data, writeType));
            return Task.FromResult(true);
        }

        protected override Task StartUpdatesNativeAsync()
        {
            throw new NotImplementedException();
        }

        protected override Task StopUpdatesNativeAsync()
        {
            throw new NotImplementedException();
        }
    }
}
