using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Foundation;
using iotc_xamarin_ble.Services.BackgroundWorker;
using UIKit;

namespace iotc_xamarin_ble.iOS.Services
{
    public class IoTCentralService
    {
        private CancellationTokenSource _cts;
        private nint _taskId;

        public IoTCentralService(string scopeId, string symKey, string deviceId, string bleDeviceId, Dictionary<string, string> telemetryMap)
        {
            ScopeId = scopeId;
            SymKey = symKey;
            DeviceId = deviceId;
            BleDeviceId = bleDeviceId;
            TelemetryMap = telemetryMap;
        }

        public string ScopeId { get; }
        public string SymKey { get; }
        public string DeviceId { get; }
        public string BleDeviceId { get; }
        public Dictionary<string, string> TelemetryMap { get; }

        public void Start()
        {
            _cts = new CancellationTokenSource();

            _taskId = UIApplication.SharedApplication.BeginBackgroundTask("IoTCentralService", OnExpiration);

            try
            {
                new DeviceWorker().Start(ScopeId, SymKey, DeviceId, BleDeviceId, TelemetryMap);

            }
            catch (OperationCanceledException)
            {
            }
            finally
            {
                if (_cts.IsCancellationRequested)
                {
                    // TODO: implements
                }
            }
            
        }

        public void Stop()
        {
            _cts.Cancel();
            UIApplication.SharedApplication.EndBackgroundTask(_taskId);
        }

        void OnExpiration()
        {
            _cts.Cancel();
        }
    }
}