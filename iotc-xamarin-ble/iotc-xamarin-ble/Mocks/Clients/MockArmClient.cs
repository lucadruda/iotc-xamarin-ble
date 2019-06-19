using iotc_csharp_service;
using iotc_csharp_service.Templates;
using iotc_csharp_service.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iotc_xamarin_ble.Mocks.Clients
{
    public class MockArmClient : ARMClient
    {
        const string API_VERSION = "2018-09-01";


        public MockArmClient(string token) : base(token)
        {

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


        public new async Task<Application> CreateApplication(Application app, string subscriptionId, string resourceGroup)
        {
            return await GetApplicationByName(app.Name, subscriptionId, resourceGroup);
        }

        public new async Task<Application> GetApplicationByName(string appName, string subscriptionId, string resourceGroup)
        {
            return await Task.FromResult(new Application("f92b55e7-3b2e-4377-8131-a17e25213503", "demomobile", "asfg", "westeurope", new DevKitTemplate()));
        }

        public new async Task<ResourceGroup> CreateResourceGroup(string subscriptionId, string resourceGroup, string location)
        {
            return await Task.FromResult(new ResourceGroup());
        }
        public new async Task<List<ResourceGroup>> ListResourceGroups(string subscriptionId)
        {
            return await Task.FromResult(new List<ResourceGroup> { new ResourceGroup("reg0", "westeurope"), new ResourceGroup("reg1", "eastus") });

        }

        public new async Task<List<Subscription>> ListSubscriptions()
        {
            return await Task.FromResult(new List<Subscription> { new Subscription("sub0", "sub0", "tenant0"), new Subscription("sub1", "sub1", "tenant0") });

        }

        public new async Task<List<Tenant>> ListTenants()
        {
            return await Task.FromResult(new List<Tenant> { new Tenant("tenant0", "tenant0"), new Tenant("tenant1", "tenant1") });

        }

    }
}
