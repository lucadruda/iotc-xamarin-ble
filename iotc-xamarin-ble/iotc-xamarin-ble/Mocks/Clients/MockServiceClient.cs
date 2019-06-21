using iotc_csharp_service;
using iotc_csharp_service.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iotc_xamarin_ble.Mocks.Clients
{
    public class MockServiceClient : DataClient
    {

        private readonly Application[] apps;

        private readonly DeviceTemplate[] models;

        private Device[] devices;

        public MockServiceClient(string accessToken) : base(accessToken)
        {
            apps = new Application[]{
                new Application("f92b55e7-3b2e-4377-8131-a17e25213503","demomobile","asfg") };
            models = new DeviceTemplate[]{
                new DeviceTemplate("model0","model0","1.0.0"),
                new DeviceTemplate("model1","model1","1.0.0"),
                new DeviceTemplate("model2","model2","1.0.0")
    };
            devices = new Device[]{
                new Device("device0","device0",models[0],"device0",false),
                new Device("device1","device1",models[1],"device1",false),
                new Device("device2","device2",models[2],"device2",false)
    };
        }

        public new async Task<Application[]> ListApps()
        {
            return await Task.FromResult(apps);
        }

        public new async Task<Device[]> ListDevices(string applicationId)
        {
            return await Task.FromResult(devices);
        }

        public new async Task<Device[]> ListDevices(string applicationId, string templateId)
        {
            return await Task.FromResult(devices);

        }

        public new async Task<Device> GetDeviceByName(string applicationId, string deviceName)
        {
            return await Task.FromResult(devices.FirstOrDefault(d => d.Name == deviceName));

        }

        public new async Task<Device> GetDeviceById(string applicationId, string deviceId)
        {
            return await Task.FromResult(devices.FirstOrDefault(d => d.DeviceId == deviceId));


        }

        public new async Task<DeviceTemplate[]> ListTemplates(string applicationId, bool latest = false)
        {
            return await Task.FromResult(models);

        }

        public new async Task<Device> CreateDevice(string applicationId, string deviceName, string modelId, string modelVersion)
        {
            return await Task.FromResult(new Device("device3", "device3", models[1], "device3", false));
        }

        public new async Task<Device> CreateDevice(string applicationId, string deviceName, string deviceId, string modelId,
                string modelVersion)
        {
            return await CreateDevice(applicationId, deviceName, modelId);
        }

        public new async Task<Device> CreateDevice(string applicationId, string deviceName, string modelId)
        {
            return await CreateDevice(applicationId, deviceName, modelId);
        }

        public new async Task<Device> CreateDevice(string applicationId, string deviceName, DeviceTemplate model)
        {
            return await CreateDevice(applicationId, deviceName, model.Id, model.Version);
        }

        public async Task<DeviceCredentials> GetCredentials(string applicationId)
        {
            return await Task.FromResult(new DeviceCredentials("scopeId", "key0", "key1"));
        }
    }
}
