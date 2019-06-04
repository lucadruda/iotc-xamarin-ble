namespace iotc_csharp_service
{

    public class DeviceCredentials
    {
        public DeviceCredentials()
        {
        }

        public DeviceCredentials(string idScope, string primaryKey, string secondaryKey)
        {
            IdScope = idScope;
            PrimaryKey = primaryKey;
            SecondaryKey = secondaryKey;
        }

        public DeviceCredentials(string deviceId, string idScope, string primaryKey, string secondaryKey) : this(idScope, primaryKey, secondaryKey)
        {
            DeviceId = deviceId;
        }

        public string IdScope { get; set; }
        public string PrimaryKey { get; set; }
        public string SecondaryKey { get; set; }
        public string DeviceId { get; set; }
    }
}