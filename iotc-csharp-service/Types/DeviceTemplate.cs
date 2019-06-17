namespace iotc_csharp_service.Types
{

    public class DeviceTemplate
    {
        private string id;
        private string version;
        private string name;

        public DeviceTemplate()
        {
        }

        public DeviceTemplate(string id, string name, string version)
        {
            this.id = id;
            this.name = name;
            this.version = version;
        }

        public string Id { get => id; set => id = value; }
        public string Version { get => version; set => version = value; }
        public string Name { get => name; set => name = value; }

        public override string ToString()
        {
            return this.name;
        }

    }
}