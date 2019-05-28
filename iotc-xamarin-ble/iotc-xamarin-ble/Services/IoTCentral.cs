using iotc_csharp_device_client;
using iotc_csharp_device_client.enums;
using iotc_csharp_service.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace iotc_xamarin_ble.Services
{
    public class IoTCentral
    {
        private static IoTCentral _service;
        private Application application;
        private DeviceTemplate model;
        private Device device;
        private DataClient serviceClient;
        private IIoTCClient deviceClient;

        private IoTCentral()
        {

        }

        public static IoTCentral Current
        {
            get
            {
                if (_service == null)
                {
                    _service = new IoTCentral();
                }
                return _service;
            }
        }

        public Application Application
        {
            get
            {
                // if (application == null)
                // throw error;
                return application;
            }
            set { application = value; }
        }

        public DeviceTemplate Model
        {
            get
            {
                // if (modelId == null)
                // throw error;
                return model;
            }
            set { model = value; }
        }

        public Device Device
        {
            get
            {
                // if (device == null)
                // throw error;
                return device;
            }
            set { device = value; }
        }

        public DataClient ServiceClient
        {
            get
            {
                // if (serviceClient == null)
                // throw error;
                return serviceClient;
            }
            set { serviceClient = value; }
        }


        public IIoTCClient DeviceClient
        {
            get; set;
        }

        public async Task ConnectDevice()
        {
            if (DeviceClient == null)
            {
                var creds = await ServiceClient.GetCredentials(Application.Id);
                DeviceClient = new IoTCClient(Device.DeviceId, creds.IdScope, IoTCConnect.SYMM_KEY, creds.PrimaryKey);
            }
            await DeviceClient.Connect();


        }
    }
}
