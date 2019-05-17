using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using iotc_csharp_service.Types;
using iotc_xamarin_ble.ViewModels.Navigation;

namespace iotc_xamarin_ble.ViewModels
{
    public class DevicesViewModel : ListViewModel<Device>
    {
        public Application CurrentApplication { get; set; }
        public DeviceTemplate CurrentModel { get; set; }
        public DevicesViewModel(INavigationService navigation, Application currentApplication, DeviceTemplate currentModel) : base(navigation)
        {
            CurrentApplication = currentApplication;
            CurrentModel = currentModel;
        }
        public override async Task<IEnumerable<Device>> FetchData()
        {
            return await ((App)App.Current).IoTCentralClient.ListDevices(CurrentApplication.Id, CurrentModel.Id);
        }

        public override void OnItemTapped()
        {
            Device model = LastTappedItem as Device;
            if (model != null)
            {
               Navigation.NavigateTo(new BleScanViewModel(Navigation));
            }
        }
    }
}
