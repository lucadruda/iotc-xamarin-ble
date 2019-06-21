using iotc_csharp_service.Types;
using iotc_xamarin_ble;
using iotc_xamarin_ble.Graphics;
using iotc_xamarin_ble.Services;
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
        public ModelsViewModel(INavigationService navigation) : base(navigation)
        {
            Title = IoTCentral.Current.Application.Name;
        }
        public override async Task<IEnumerable<DeviceTemplate>> FetchData()
        {
            return await (await IoTCentral.Current.GetServiceClient()).ListTemplates(IoTCentral.Current.Application.Id);
        }

        public override void OnItemTapped()
        {
            DeviceTemplate model = LastTappedItem as DeviceTemplate;
            if (model != null)
            {
                IoTCentral.Current.Model = model;
                LastTappedItem = null;
                OnPropertyChanged("LastTappedItem");
                Navigation.NavigateTo(new DevicesViewModel(Navigation));
            }
        }
    }
}