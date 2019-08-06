using iotc_ble_xamarin;
using iotc_csharp_device_client;
using iotc_csharp_device_client.enums;
using Device = iotc_csharp_service.Types.Device;
using iotc_csharp_service.Types;
using iotc_xamarin_ble.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using iotc_csharp_service;
using Plugin.BLE.Abstractions.Contracts;
using iotc_xamarin_ble.Bluetooth;
using iotc_xamarin_ble.Services.BackgroundWorker;
using iotc_xamarin_ble.ViewModels.Authentication;
using iotc_xamarin_ble.ViewModels;

namespace iotc_xamarin_ble.Services
{
    public class IoTCentral
    {
        private static IoTCentral _service;
        private Application application;
        private DeviceTemplate model;
        private Device device;
        private DataClient serviceClient;
        private ARMClient armClient;



        private IoTCentral()
        {
            MessagingCenter.Subscribe<ResultMessage<IDevice>>(this, Constants.BLE_DEVICE_READY, (dev) => { DeviceReady?.Invoke(this, dev.Data); });
            MessagingCenter.Subscribe<ResultMessage<IoTCConnectionState>>(this, Constants.IOTC_DEVICE_CLIENT_CONNECTED, result =>
            {
                DeviceConnectionChanged?.Invoke(this, result.Data);
            });
            MessagingCenter.Subscribe<ResultMessage<IList<BluetoothServiceModel>>>(this, Constants.BLE_SERVICES_FETCHED, (services) => { ServicesFetched?.Invoke(this, services.Data); });


        }

        public event EventHandler<IDevice> DeviceReady;
        public event EventHandler<IList<BluetoothServiceModel>> ServicesFetched;
        public event EventHandler<IoTCConnectionState> DeviceConnectionChanged;

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

        public async Task Clear()
        {
            var auth = Container.ContainerService.Current.Resolve<IAuthViewModel>();
            await auth.Clear();
            _service = null;
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
                if (model == null && (bool)App.Current.Properties["mocked"])
                    return new DeviceTemplate("c318d580-39fc-4aca-b995-843719821049", "fridge", "1.0.0");
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
                if (device == null && (bool)App.Current.Properties["mocked"])
                    return new Device("testdevice", "testdevice", Model, "testdevice", false);
                // if (device == null)
                // throw error;
                return device;
            }
            set { device = value; }
        }


        public async Task<DataClient> GetServiceClient()
        {
            if (serviceClient == null)
            {
                var client = Container.ContainerService.Current.Resolve<DataClient>();
                if (client != null)
                {
                    serviceClient = client;
                }
                else
                {
                    var auth = Container.ContainerService.Current.Resolve<IAuthViewModel>();
                    var token = await auth.GetTokenAsync();
                    serviceClient = new DataClient(token);
                }
            }
            return serviceClient;
        }

        public async Task<ARMClient> GetArmClient()
        {
            if (armClient == null)
            {
                var auth = Container.ContainerService.Current.Resolve<IAuthViewModel>();
                var token = await auth.GetTokenAsync(Constants.RM_TOKEN_AUDIENCE_v1);
                armClient = new ARMClient(token);
            }
            return armClient;
        }

        public async Task<ARMClient> GetArmClient(string tenant)
        {
            if (armClient != null)
            {
                if (armClient.Tenant == tenant)
                {
                    return armClient;
                }
            }
            var auth = Container.ContainerService.Current.Resolve<IAuthViewModel>();
            var token = await auth.GetTokenAsync(Constants.RM_TOKEN_AUDIENCE_v1, tenant);
            armClient = new ARMClient(token, tenant);
            return armClient;
        }

        public IIoTCClient DeviceClient
        {
            get; set;
        }


        public async Task StartService(string bleDeviceId, Dictionary<string, string> mapping)
        {
            var creds = await (await GetServiceClient()).GetCredentials(Application.Id);
            creds.DeviceId = Device.DeviceId;
            MessagingCenter.Send(new RequestMessage<ServiceParameter>(new ServiceParameter(creds, bleDeviceId, mapping)), Constants.SERVICE_START);
            //new DeviceWorker().Start(creds.IdScope, creds.PrimaryKey, creds.DeviceId, bleDeviceId, mapping);
        }

    }
}
