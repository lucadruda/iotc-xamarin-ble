using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using iotc_csharp_service.Types;
using iotc_xamarin_ble.Mocks;
using iotc_xamarin_ble.Services;
using iotc_xamarin_ble.ViewModels.Navigation;

namespace iotc_xamarin_ble.ViewModels
{
    public class DevicesViewModel : ListViewModel<Device>
    {
        public DevicesViewModel(INavigationService navigation) : base(navigation)
        {
            Title = IoTCentral.Current.Model.Name;

        }
        public override async Task<IEnumerable<Device>> FetchData()
        {
            return await IoTCentral.Current.ServiceClient.ListDevices(IoTCentral.Current.Application.Id, IoTCentral.Current.Model.Id);
        }

        public override void OnItemTapped()
        {
            Device device = LastTappedItem as Device;
            if (device != null)
            {
                IoTCentral.Current.Device = device;
                Navigation.NavigateTo(new BleScanViewModel(Navigation));
                LastTappedItem = null;
                OnPropertyChanged("LastTappedItem");

            }
        }
    }
}
