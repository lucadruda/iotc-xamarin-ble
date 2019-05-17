using iotc_csharp_service.Types;
using iotc_xamarin_ble;
using iotc_xamarin_ble.Graphics;
using iotc_xamarin_ble.ViewModels;
using iotc_xamarin_ble.ViewModels.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace iotc_xamarin_ble
{
    public class ModelsViewModel : ListViewModel<DeviceTemplate>
    {
        public Application CurrentApplication { get; set; }
        public ModelsViewModel(INavigationService navigation, Application currentApplication) : base(navigation)
        {
            CurrentApplication = currentApplication;
        }
        public override async Task<IEnumerable<DeviceTemplate>> FetchData()
        {
            return await ((App)App.Current).IoTCentralClient.ListTemplates(CurrentApplication.Id);
        }

        public override void OnItemTapped()
        {
            DeviceTemplate model = LastTappedItem as DeviceTemplate;
            if (model != null)
            {
                Navigation.NavigateTo(new DevicesViewModel(Navigation, CurrentApplication, model));
            }
        }
    }
}