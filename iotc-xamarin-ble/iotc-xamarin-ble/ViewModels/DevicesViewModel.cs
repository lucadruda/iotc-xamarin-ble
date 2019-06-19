using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using iotc_csharp_service.Types;
using iotc_xamarin_ble.Mocks;
using iotc_xamarin_ble.Services;
using iotc_xamarin_ble.Services.Dialog;
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
            var res = await (await IoTCentral.Current.GetServiceClient()).ListDevices(IoTCentral.Current.Application.Id, IoTCentral.Current.Model.Id);
            AddingAvailable = true;
            return res;
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

        public override void AddItem()
        {//open popup here

            var input = new InputDialogViewModel { IsVisible = true, InputLabel = "Choose device name ..." };
            input.OnOk += async (object source, EventArgs e) =>
            {
                (source as InputDialogViewModel).IsVisible = false;
                try
                {
                    IsBusy = true;
                    var device = await (await IoTCentral.Current.GetServiceClient()).CreateDevice(IoTCentral.Current.Application.Id, input.InputText, IoTCentral.Current.Model);
                    IsBusy = false;
                    await DialogService.Current.ShowMessage($"Device '{device.Name}' successfully created", "Device Creation", "Dismiss", async () =>
                       {
                           base.AddItem();
                           IoTCentral.Current.Device = device;
                           await Navigation.NavigateTo(new BleScanViewModel(Navigation));
                       });

                }
                catch (Exception ex)
                {
                    await DialogService.Current.ShowError(ex, "Device Creation", "Dismiss", null);
                }
            };
            input.OnCancel += async (object source, EventArgs e) =>
            {
                (source as InputDialogViewModel).IsVisible = false;
            };
            InputModel = input;

        }


    }
}
