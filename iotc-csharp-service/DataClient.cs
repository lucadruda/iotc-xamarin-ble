
using iotc_csharp_service;
using iotc_csharp_service.Helpers;
using iotc_csharp_service.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

public class DataClient
{

    private string accessToken;
    private RequestFactory req;

    public DataClient(string accessToken)
    {
        this.accessToken = accessToken;
        req = new RequestFactory(this.accessToken);
        Dictionary<string, string> headers = new Dictionary<string, string>() {
                {"Content-Type", "application/json; charset=utf-8" },
        { "x-ms-client-request-id", Guid.NewGuid().ToString() },
        { "x-forwarded-port", "443" },
        { "accept-language", "en-US" }
    };
        req.SetHeaders(headers);
    }

    public Application GetAppByName(string resourceGroup, string applicationName)
    {
        return null;
    }

    public Application GetApp(string applicationId)
    {
        return null;
    }
    /// <summary>
    /// Lists all applications for the user
    /// </summary>
    /// <returns>Array of applications</returns>
    /// <exception cref="iotc_csharp_service.Exceptions.DataException">Thrown if request fails</exception>
    /// <exception cref="iotc_csharp_service.Exceptions.AuthenticationException">Thrown if authentication token is invalid</exception>
    public async Task<Application[]> ListApps()
    {
            string appsJson = await req.Get(Constants.IOTC_DATA_URL + "/applications");
            Application[] apps = ((JArray)JObject.Parse(appsJson).GetValue("value")).ToObject<Application[]>();
            return apps;
    }

    /// <summary>
    /// Lists all devices for the application
    /// </summary>
    /// <param name="applicationId">The application Id</param>
    /// <returns>Array of devices</returns>
    /// <exception cref="iotc_csharp_service.Exceptions.DataException">Thrown if request fails</exception>
    /// <exception cref="iotc_csharp_service.Exceptions.AuthenticationException">Thrown if authentication token is invalid</exception>
    public async Task<Device[]> ListDevices(string applicationId)
    {
        string devicesJsonstring = await req.Get(Constants.IOTC_DATA_URL + "/applications/" + applicationId + "/devices");
        Device[] devices = ((JArray)JObject.Parse(devicesJsonstring).GetValue("value")).ToObject<Device[]>();
        string templatesJsonstring = await req.Get(Constants.IOTC_DATA_URL + "/display/applications/" + applicationId + "/deviceTemplates");

        DeviceTemplate[] deviceTemplates = ((JArray)JObject.Parse(templatesJsonstring).GetValue("value")).ToObject<DeviceTemplate[]>();
        Func<Device, string, Device> map = ((dev, templateName) =>
        {
            dev.DeviceTemplate.Name = templateName;
            return dev;
        });
        Device[] result = (from deviceTemplate in deviceTemplates
                           from device in devices
                           let idVer = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(deviceTemplate.Id)).Split('/')
                           let templateId = idVer[0]
                           let templateVer = idVer[1]
                           where device.DeviceTemplate.Id == templateId && device.DeviceTemplate.Version == templateVer
                           select map(device, deviceTemplate.Name)).ToArray();
        return result;
    }

    /// <summary>
    /// Lists all devices of the specified model for the application
    /// </summary>
    /// <param name="applicationId">The application Id</param>
    /// <param name="templateId">The model Id</param>
    /// <returns>Array of devices</returns>
    /// <exception cref="iotc_csharp_service.Exceptions.DataException">Thrown if request fails</exception>
    /// <exception cref="iotc_csharp_service.Exceptions.AuthenticationException">Thrown if authentication token is invalid</exception>
    public async Task<Device[]> ListDevices(string applicationId, string templateId)
    {
        Device[] devices = await ListDevices(applicationId);
        return devices.Where(d => d.DeviceTemplate.Id == templateId).ToArray();
    }


    /// <summary>
    /// Get a device by its name
    /// </summary>
    /// <param name="applicationId">The application Id</param>
    /// <param name="deviceName">The device name</param>
    /// <returns>The requested device or null if not found</returns>
    /// <exception cref="iotc_csharp_service.Exceptions.DataException">Thrown if request fails</exception>
    /// <exception cref="iotc_csharp_service.Exceptions.AuthenticationException">Thrown if authentication token is invalid</exception>
    public async Task<Device> GetDeviceByName(string applicationId, string deviceName)
    {
        Device[]
        devices = await ListDevices(applicationId);
        return devices.Where(d => d.Name == deviceName).FirstOrDefault();
    }

    /// <summary>
    /// Get a device by its id
    /// </summary>
    /// <param name="applicationId">The application Id</param>
    /// <param name="deviceId">The device Id</param>
    /// <returns>The requested device or null if not found</returns>
    /// <exception cref="iotc_csharp_service.Exceptions.DataException">Thrown if request fails</exception>
    /// <exception cref="iotc_csharp_service.Exceptions.AuthenticationException">Thrown if authentication token is invalid</exception>
    public async Task<Device> GetDeviceById(string applicationId, string deviceId)
    {
        Device[] devices = await ListDevices(applicationId);
        return devices.Where(d => d.Id == deviceId).FirstOrDefault();
    }

    /// <summary>
    /// List available models in the application
    /// </summary>
    /// <param name="applicationId">The application Id</param>
    /// <param name="latest">Optionally specifies if only show latest versions of the models</param>
    /// <returns>Array of models</returns>
    /// <exception cref="iotc_csharp_service.Exceptions.DataException">Thrown if request fails</exception>
    /// <exception cref="iotc_csharp_service.Exceptions.AuthenticationException">Thrown if authentication token is invalid</exception>
    public async Task<DeviceTemplate[]> ListTemplates(string applicationId, bool latest = false)
    {
        Dictionary<string, DeviceTemplate> models = new Dictionary<string, DeviceTemplate>();
        Device[] devices = await ListDevices(applicationId);
        foreach (Device device in devices)
        {
            if (models.ContainsKey(latest ? device.DeviceTemplate.Id : $"{device.DeviceTemplate.Id}/{device.DeviceTemplate.Version}"))
            {
                if (latest)
                {
                    if (VersionUtility.Compare(models[device.DeviceTemplate.Id].Version,
                            device.DeviceTemplate.Version) <= 0)
                    {
                        models[device.DeviceTemplate.Id] = device.DeviceTemplate;
                        continue;
                    }

                }
            }
            else
            {
                models.Add(latest ? device.DeviceTemplate.Id : $"{device.DeviceTemplate.Id}/{device.DeviceTemplate.Version}", device.DeviceTemplate);
            }
        }
        return models.Values.ToArray();
    }

    public async Task<Device> CreateDevice(string applicationId, string deviceName, string modelId, string modelVersion)
    {
        return await CreateDevice(applicationId, deviceName, deviceName.ToLower(), modelId, modelVersion);
    }

    public async Task<Device> CreateDevice(string applicationId, string deviceName, string deviceId, string modelId,
            string modelVersion)
    {
        string devicesJson = await req.Post(Constants.IOTC_DATA_URL + "/applications/" + applicationId + "/devices",
                    string.Format(
                            "{{\"name\":\"{0}\",\"deviceId\":\"{1}\",\"simulated\":false,\"deviceTemplate\":{{\"id\":\"{2}\",\"version\": \"{3}\"}}}}",
                            deviceName, deviceId, modelId, modelVersion));
        return JsonConvert.DeserializeObject<Device>(devicesJson);
    }

    public async Task<Device> CreateDevice(string applicationId, string deviceName, string modelId)
    {
        DeviceTemplate[] temps = await ListTemplates(applicationId);
        string version = "1.0.0";
        version = temps.Where(t => t.Id == modelId).Select(t => t.Version).FirstOrDefault() ?? version;
        return await CreateDevice(applicationId, deviceName, modelId, version);
    }

    public async Task<Device> CreateDevice(string applicationId, string deviceName, DeviceTemplate model)
    {
        return await CreateDevice(applicationId, deviceName, model.Id, model.Version);
    }

    public async Task<DeviceCredentials> GetCredentials(string applicationId)
    {
        string dpsJson = await req.Get(Constants.IOTC_DATA_URL + "/applications/" + applicationId + "/dps");
        JObject json = JObject.Parse(dpsJson);
        JArray enrollmentGroups = (JArray)json.GetValue("enrollmentGroups");

        return (
        from enrollmentGroup in enrollmentGroups
        let attestation = ((JObject)enrollmentGroup)["attestation"].Value<JObject>()
        where attestation["type"].Value<string>() == "symmetricKey"
        let keys = (JObject)attestation["symmetricKey"].Value<JObject>()
        select new DeviceCredentials(json["idScope"].Value<string>(), keys["primaryKey"].Value<string>(), keys["secondaryKey"].Value<string>())
        ).FirstOrDefault();
    }
}