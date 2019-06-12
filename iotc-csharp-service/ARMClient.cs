
using iotc_csharp_service.Exceptions;
using iotc_csharp_service.Helpers;
using iotc_csharp_service.Templates;
using iotc_csharp_service.Types;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;
using Microsoft.Rest;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iotc_csharp_service
{
    public class ARMClient
    {
        //private Azure azure;

        //private string resourceGroupName;
        const string API_VERSION = "2018-09-01";
        const string ENDPOINT = "https://management.azure.com";
        //private AzureTokenCredentials credentials;
        private RequestFactory req;
        public ARMClient(string token)
        {
            req = new RequestFactory(token);

        }

        private string GetTemplate(Application app)
        {

            return JsonConvert.SerializeObject(new Dictionary<string, object>
            {
                {"$schema","https://schema.management.azure.com/schemas/2015-01-01/deploymentTemplate.json#" },
                { "contentVersion","1.0.0.0" },
                {"resources",new []
                    {
                        new
                        {
                            type="Microsoft.IoTCentral/IoTApps",
                            name=app.Name,
                            location=app.Location,
                            apiVersion=API_VERSION,
                            tags=new{ },
                            sku = new
                            {
                                name = "S1"
                            },
                            properties = new
                            {
                                displayName=app.DisplayName,
                                subdomain=app.Subdomain,
                                template=app.IotcTemplate.Id,
                                users=(app.Users!=null&& app.Users.Length>0)?app.Users:new User[0]
                            }
                        }
                    }
                }

            });

        }

        //        public void createResourceGroup(string resourceGroupName, Region region) throws IOException
        //        {
        //            com.microsoft.azure.management.resources.ResourceGroup resourceGroup = this.azure.resourceGroups()
        //                .define(resourceGroupName).withRegion(region).create();
        //        System.out.println("Provisioning state: " + resourceGroup.provisioningState());
        //        if (!resourceGroup.provisioningState().equals(ProvisioningState.SUCCEEDED.tostring())) {
        //            throw new IllegalStateException("Error creating resource group");
        //    }
        //        this.resourceGroupName = resourceGroupName;
        //    }



        //    public Application getAppByName(string applicationName)
        //    {
        //        if (resourceGroupName == null || resourceGroupName.equals(""))
        //        {
        //            throw new IllegalArgumentException("Resource group does not exists");
        //        }
        //        Application app = new Application(azure.genericResources().get(this.resourceGroupName, "Microsoft.IoTCentral",
        //                null, "IoTApps", applicationName, API_VERSION));
        //        return app;
        //    }

        //    public Application[] listAllTenantApplications() throws DataException
        //    {
        //        ArrayList<Application> apps = new ArrayList<>();
        //        Subscription[] subs = listSubscriptions();
        //        for (Subscription sub : subs) {
        //            ResourceGroup[] rgs = listResourceGroups(sub.getSubscriptionId());
        //            for (ResourceGroup rg : rgs) {
        //                string appJson = req.get(string.format(
        //                        "%ssubscriptions/%s/resourceGroups/%s/providers/Microsoft.IoTCentral/IoTApps?api-version=2018-09-01",
        //                        Constants.RM_TOKEN_AUDIENCE, sub.getSubscriptionId(), rg.getName()));
        //    JsonParser parser = new JsonParser();
        //    JsonArray json = (JsonArray)((Jsonobject)parser.parse(appJson)).get("value");
        //                for (JsonElement el : json) {
        //                    Application app = new Gson().fromJson(((Jsonobject)el).get("properties"), Application.class);
        //                    apps.add(app);
        //                }
        //            }
        //        }
        //        return (Application[]) apps.toArray();
        //    }

        //    public void setResourceGroup(string resourceGroupName)
        //{
        //    this.resourceGroupName = resourceGroupName;
        //}


        //public void setSubscription(Subscription subscription)
        //{
        //    this.setSubscription(subscription.getSubscriptionId());
        //}

        public async Task<Application> CreateApplication(Application app, string subscriptionId, string resourceGroup)
        {
            var templateBody = $"{{\"properties\":{{ \"mode\":\"Incremental\", \"template\":{GetTemplate(app)} }}}}";
            //validation

            await req.Post($"{ENDPOINT}/subscriptions/{subscriptionId}/resourcegroups/{resourceGroup}/providers/Microsoft.Resources/deployments/dpIoTC/validate?api-version=2019-05-01", templateBody);
            var json = await req.Put($"{ENDPOINT}/subscriptions/{subscriptionId}/resourcegroups/{resourceGroup}/providers/Microsoft.Resources/deployments/dpIoTC?api-version=2019-05-01", templateBody);

            return await GetApplicationByName(app.Name, subscriptionId, resourceGroup);
        }

        public async Task<Application> GetApplicationByName(string appName, string subscriptionId, string resourceGroup)
        {
            var json = await req.Get($"{ENDPOINT}/subscriptions/{subscriptionId}/resourcegroups/{resourceGroup}/providers/Microsoft.IoTCentral/IoTApps/{appName}?api-version=2018-09-01");
            var appObj = JObject.Parse(json);
            return new Application(appObj["properties"]["applicationId"].Value<string>(), appObj["name"].Value<string>(), appObj["properties"]["subdomain"].Value<string>(), appObj["location"].Value<string>(), IoTCTemplate.GetTemplate(appObj["properties"]["template"].Value<string>()));
        }

        public async Task<ResourceGroup> CreateResourceGroup(string subscriptionId, string resourceGroup, string location)
        {
            string json = await req.Put($"{ENDPOINT}/subscriptions/{subscriptionId}/resourcegroups/{resourceGroup}?api-version={API_VERSION}",
                $"{{\"location\":\"{location}\"}}");
            return new ResourceGroup();
        }
        public async Task<List<ResourceGroup>> ListResourceGroups(string subscriptionId)
        {
            string json = await req.Get($"{ENDPOINT}/subscriptions/{subscriptionId}/resourcegroups?api-version={API_VERSION}");
            return JObject.Parse(json)["value"].Value<JArray>().ToObject<List<ResourceGroup>>();
        }

        public async Task<List<Subscription>> ListSubscriptions()
        {
            string json = await req.Get($"{ENDPOINT}/subscriptions?api-version={API_VERSION}");
            return JObject.Parse(json)["value"].Value<JArray>().ToObject<List<Subscription>>();
        }

        public async Task<List<Tenant>> ListTenants()
        {
            string json = await req.Get($"{ENDPOINT}/tenants?api-version={API_VERSION}");
            return JObject.Parse(json)["value"].Value<JArray>().ToObject<List<Tenant>>();
        }

    }
}