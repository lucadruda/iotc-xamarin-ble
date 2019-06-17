
namespace iotc_csharp_service.Types
{
    public class Subscription
    {

        private string subscriptionId;
        private string displayName;
        private string tenantId;

        public Subscription()
        {

        }

        public Subscription(string subscriptionId, string displayName, string tenantId)
        {
            this.subscriptionId = subscriptionId;
            this.displayName = displayName;
            this.tenantId = tenantId;
        }

        public string SubscriptionId { get => subscriptionId; set => subscriptionId = value; }
        public string DisplayName { get => displayName; set => displayName = value; }
        public string TenantId { get => tenantId; set => tenantId = value; }

        public override string ToString()
        {
            return displayName;
        }
    }
}