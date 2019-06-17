using System;
using System.Collections.Generic;
using System.Text;

namespace iotc_xamarin_ble.Mocks
{
    public class MockBLENativeDevice
    {

        public MockBLENativeDevice(string name, string address)
        {
            Name = name;
            Address = address;
        }

        public string Name { get; set; }

        public string Address { get; set; }
    }
}
