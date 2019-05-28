using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using iotc_ble_xamarin.Bluetooth;
using iotc_xamarin_ble.Bluetooth;
using iotc_xamarin_ble.Services;
using iotc_xamarin_ble.ViewModels.Bluetooth;
using iotc_xamarin_ble.ViewModels.Navigation;
using Plugin.BLE.Abstractions.Contracts;
using Xamarin.Forms;

namespace iotc_xamarin_ble.ViewModels
{
    public class BLEDetailsViewModel : BaseViewModel
    {
        public BLEDetailsViewModel(INavigationService navigation) : base(navigation)
        {
            Device = BLEService.Current.Device;
            Services = new ObservableCollection<BluetoothServiceViewModel>();
            HeaderClickCommand = new Command<BluetoothServiceViewModel>((item) => ExecuteHeaderClickCommand(item));
            Save = new Command(SaveMapping);
        }

        public ICommand LoadDataCommand { get; private set; }
        public ICommand HeaderClickCommand { get; private set; }
        public ICommand Save { get; private set; }
        public IDevice Device { get; set; }

        public ObservableCollection<BluetoothServiceViewModel> Services { get; private set; }

        public string SaveText { get; set; } = "Save";

        private bool saved = false;

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

        private async void SaveMapping()
        {
            foreach (var service in Services)
            {
                foreach (var characteristic in service.Characteristics)
                {
                    var pair = new GattPair(characteristic.Characteristic);
                    if (characteristic.SelectedMeasure != null)
                    {
                        MappingStorage.Current.Add(pair.GattKey, characteristic.SelectedMeasure.FieldName);
                        await BLEService.Current.EnableNotification(characteristic.Characteristic);
                    }
                    else
                    {
                        if (MappingStorage.Current[pair.GattKey] != null)
                        {
                            MappingStorage.Current.Remove(pair.GattKey);
                            await BLEService.Current.DisableNotification(characteristic.Characteristic);
                        }
                    }
                }
            }
            await MappingStorage.Current.Save();
            saved = true;
            await Navigation.NavigateBack(); // TODO: change to tabs
        }

        public async override Task AfterDismissed()
        {
            //await BLEService.Current.DisconnectDevice(BLEService.Current.Device);
            if (saved) {
                Complete();
            }
            await Task.CompletedTask;
        }
    }
}
