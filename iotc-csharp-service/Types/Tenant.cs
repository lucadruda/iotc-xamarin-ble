namespace iotc_csharp_service.Types
{

    public class Tenant
    {

        private string tenantId;
        private string displayName;

        public Tenant()
        {

        }

        public Tenant(string tenantId, string displayName)
        {
            this.tenantId = tenantId;
            this.displayName = displayName;
        }

        public string TenantId { get => tenantId; set => tenantId = value; }
        public string DisplayName { get => displayName; set => displayName = value; }

        public override string ToString()
        {
            return displayName;
        }
    }
}