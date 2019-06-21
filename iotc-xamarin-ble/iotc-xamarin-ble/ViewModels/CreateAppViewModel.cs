using iotc_ble_xamarin;
using iotc_csharp_service.Templates;
using iotc_csharp_service.Types;
using iotc_xamarin_ble.Services;
using iotc_xamarin_ble.Services.Dialog;
using iotc_xamarin_ble.ViewModels.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace iotc_xamarin_ble.ViewModels
{
    public class CreateAppViewModel : BaseViewModel
    {
        private Tenant selectedTenant;
        private Subscription selectedSubscription;
        private ResourceGroup selectedResourceGroup;
        private string selectedRegion;

        private AuthViewModel authViewModel;
        private ContosoTemplate contosoTemplate;
        private DevKitTemplate devKitTemplate;

        public CreateAppViewModel(INavigationService navigation) : base(navigation)
        {
            Tenants = new ObservableCollection<Tenant>();
            Subscriptions = new ObservableCollection<Subscription>();
            ResourceGroups = new ObservableCollection<ResourceGroup>();
            Regions = new ObservableCollection<string>(Constants.RM_REGIONS);
            contosoTemplate = new ContosoTemplate();
            devKitTemplate = new DevKitTemplate();
            Template = contosoTemplate;
            SelectTemplate = new Command((templateName) =>
              {
                  switch (templateName)
                  {
                      case "DevKit":
                          Template = devKitTemplate;
                          break;
                      case "Contoso":
                          Template = contosoTemplate;
                          break;
                  }
              });

            CreateApplication = new Command(async () =>
              {
                  // manage exception
                  try
                  {
                      IsBusy = true;
                      var app = await (await IoTCentral.Current.GetArmClient()).CreateApplication(new Application(ApplicationName, ApplicationName, ApplicationDomain, SelectedRegion, Template), SelectedSubscription.SubscriptionId, SelectedResourceGroup.Name);
                      await DialogService.Current.ShowMessage($"Application '{app.Name}' successfully created", "Application Creation", "Dismiss", async () =>
                      {
                          IoTCentral.Current.Application = app;
                          IsBusy = false;
                          await Navigation.NavigateBack();

                      });

                  }
                  catch (Exception ex)
                  {
                      await DialogService.Current.ShowError(ex, "Application Creation", "Dismiss", null);
                  }
              });

        }

        public ICommand SelectTemplate { get; set; }
        public ICommand CreateApplication { get; set; }

        public ObservableCollection<Tenant> Tenants { get; set; }
        public ObservableCollection<Subscription> Subscriptions { get; set; }
        public ObservableCollection<ResourceGroup> ResourceGroups { get; set; }
        public ObservableCollection<string> Regions { get; set; }
        public string ApplicationName { get; set; }
        public string ApplicationDomain { get; set; }

        public IoTCTemplate Template { get; set; }

        public bool Validated { get; set; }

        public Tenant SelectedTenant
        {
            get => selectedTenant;
            set
            {
                if (value != null)
                {
                    IsBusy = true;
                    if (selectedTenant != null && selectedTenant != value)
                    {
                        ClearAll();
                    }
                    selectedTenant = value;
                    Validate();
                    OnPropertyChanged();
                    LoadSubscriptions(selectedTenant.TenantId);
                }
            }
        }
        public Subscription SelectedSubscription
        {
            get => selectedSubscription; set
            {
                if (value != null)
                {
                    IsBusy = true;
                    selectedSubscription = value;
                    Validate();
                    OnPropertyChanged();
                    LoadResourceGroups(selectedSubscription.SubscriptionId);
                }
            }
        }
        public ResourceGroup SelectedResourceGroup
        {
            get => selectedResourceGroup; set
            {
                if (value != null)
                {
                    selectedResourceGroup = value;
                    OnPropertyChanged();
                    Regions = new ObservableCollection<string>(Constants.RM_REGIONS);
                    OnPropertyChanged("Regions");
                    Validate();
                }
            }
        }
        public string SelectedRegion { get => selectedRegion; set => selectedRegion = value; }


        public async override Task OnAppearing()
        {
            IsBusy = true;
            LoadTenants();
        }


        private async void LoadTenants()
        {
            Tenants.Clear();
            (await (await IoTCentral.Current.GetArmClient()).ListTenants()).ForEach(t => Tenants.Add(t));
            OnPropertyChanged("Tenants");
            IsBusy = false;
        }

        private async void LoadSubscriptions(string tenantId)
        {
            Subscriptions.Clear();
            (await (await IoTCentral.Current.GetArmClient(tenantId)).ListSubscriptions()).ForEach(s => Subscriptions.Add(s));
            OnPropertyChanged("Subscriptions");
            IsBusy = false;
        }

        private async void LoadResourceGroups(string subscriptionId)
        {
            ResourceGroups.Clear();
            (await (await IoTCentral.Current.GetArmClient()).ListResourceGroups(subscriptionId)).ForEach(r => ResourceGroups.Add(r));

            OnPropertyChanged("ResourceGroups");
            IsBusy = false;
        }

        private void ClearAll()
        {
            Subscriptions.Clear();
            OnPropertyChanged("Subscriptions");
            ResourceGroups.Clear();
            OnPropertyChanged("ResourceGroups");
            Regions.Clear();
            OnPropertyChanged("Regions");

        }

        private void Validate()
        {
            if (selectedTenant != null && selectedSubscription != null && selectedResourceGroup != null && selectedRegion != null && !string.IsNullOrEmpty(ApplicationName) && !string.IsNullOrEmpty(ApplicationDomain))
            {
                Validated = true;

            }
            else Validated = false;

            OnPropertyChanged("Validated");
        }
    }

}

