namespace iotc_csharp_service
{

    public class DeviceCredentials
    {

        private string idScope;
        private string primaryKey;
        private string secondaryKey;

        public DeviceCredentials()
        {
        }

        public DeviceCredentials(string IdScope, string primaryKey, string secondaryKey)
        {
            this.idScope = IdScope;
            this.primaryKey = primaryKey;
            this.secondaryKey = secondaryKey;
        }

        public string IdScope { get => idScope; set => idScope = value; }
        public string PrimaryKey { get => primaryKey; set => primaryKey = value; }
        public string SecondaryKey { get => secondaryKey; set => secondaryKey = value; }


        //public DeviceCredentials IdScope(string IdScope)
        //{
        //    this.idScope = IdScope;
        //    return this;
        //}

        //public DeviceCredentials PrimaryKey(string primaryKey)
        //{
        //    this.primaryKey = primaryKey;
        //    return this;
        //}

        //public DeviceCredentials SecondaryKey(string secondaryKey)
        //{
        //    this.secondaryKey = secondaryKey;
        //    return this;
        //}

    }
}