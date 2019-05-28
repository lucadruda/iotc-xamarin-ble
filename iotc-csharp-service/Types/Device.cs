namespace iotc_csharp_service.Types
{
    public class Device
    {
        private string id;
        private string deviceId;
        private DeviceTemplate deviceTemplate;
        private string name;
        private bool simulated;

        public Device()
        {
        }

        public Device(string id, string deviceId, DeviceTemplate deviceTemplate, string name, bool simulated)
        {
            this.id = id;
            this.deviceId = deviceId;
            this.deviceTemplate = deviceTemplate;
            this.name = name;
            this.simulated = simulated;
        }

        public string Id { get => id; set => id = value; }
        public string DeviceId { get => deviceId; set => deviceId = value; }
        public DeviceTemplate DeviceTemplate { get => deviceTemplate; set => deviceTemplate = value; }
        public string Name { get => name; set => name = value; }
        public bool Simulated { get => simulated; set => simulated = value; }

        public override string ToString()
        {
            return this.name;
        }
    }
}