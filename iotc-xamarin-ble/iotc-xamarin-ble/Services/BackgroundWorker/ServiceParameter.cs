using iotc_csharp_service;
using System;
using System.Collections.Generic;
using System.Text;

namespace iotc_xamarin_ble.Services.BackgroundWorker
{
    public class ServiceParameter
    {
        public ServiceParameter(DeviceCredentials deviceCredentials, string bLEDeviceId, Dictionary<string, string> telemetryMap)
        {
            DeviceCredentials = deviceCredentials;
            BLEDeviceId = bLEDeviceId;
            TelemetryMap = telemetryMap;
        }

        public DeviceCredentials DeviceCredentials { get; set; }
        public string BLEDeviceId { get; set; }
        public Dictionary<string, string> TelemetryMap { get; set; }
    }
}
