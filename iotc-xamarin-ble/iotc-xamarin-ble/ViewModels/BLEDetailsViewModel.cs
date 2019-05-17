using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using iotc_xamarin_ble.Services;
using iotc_xamarin_ble.ViewModels.Bluetooth;
using iotc_xamarin_ble.ViewModels.Navigation;
using Plugin.BLE.Abstractions.Contracts;
using Xamarin.Forms;

namespace iotc_xamarin_ble.ViewModels
{
    public class BLEDetailsViewModel : BaseViewModel
    {
        public BLEDetailsViewModel(INavigationService navigation, IDevice device) : base(navigation)
        {
            Device = device;
            Services = new ObservableCollection<BluetoothServiceViewModel>();
            HeaderClickCommand = new Command<BluetoothServiceViewModel>((item) => ExecuteHeaderClickCommand(item));
        }

        public ICommand LoadDataCommand { get; private set; }
        public ICommand HeaderClickCommand { get; private set; }
        public IDevice Device { get; set; }



        public ObservableCollection<BluetoothServiceViewModel> Services { get; private set; }


        public override async Task OnAppearing()
        {
            IsBusy = true;
            await BLEService.Current.ConnectDevice(Device);
            var services = await Device.GetServicesAsync();
            Services = new ObservableCollection<BluetoothServiceViewModel>(await Task.WhenAll<BluetoothServiceViewModel>(services.Select(async s =>
            {
                var m = new BluetoothServiceViewModel(s);
                await m.Init();
                return m;
            })));
            OnPropertyChanged("Services");
            IsBusy = false;
            //return base.BeforeFirstShown();
        }

        private void ExecuteHeaderClickCommand(BluetoothServiceViewModel service)
        {//toggle expansion
            service.Expanded = !service.Expanded;
        }
    }
}
