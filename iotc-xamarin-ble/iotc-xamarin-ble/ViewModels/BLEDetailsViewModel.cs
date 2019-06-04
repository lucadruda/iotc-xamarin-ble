using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using iotc_ble_xamarin;
using iotc_ble_xamarin.Bluetooth;
using iotc_xamarin_ble.Bluetooth;
using iotc_xamarin_ble.Messages;
using iotc_xamarin_ble.Services;
using iotc_xamarin_ble.Services.BackgroundWorker;
using iotc_xamarin_ble.ViewModels.Bluetooth;
using iotc_xamarin_ble.ViewModels.Navigation;
using Plugin.BLE.Abstractions.Contracts;
using Xamarin.Forms;

namespace iotc_xamarin_ble.ViewModels
{
    public class BLEDetailsViewModel : BaseViewModel
    {
        public BLEDetailsViewModel(INavigationService navigation, BleScanViewModel scanner) : base(navigation)
        {
            this.scanner = scanner;
            Device = this.scanner.LastTappedItem;
            Services = new ObservableCollection<BluetoothServiceViewModel>();
            HeaderClickCommand = new Command<BluetoothServiceViewModel>((item) => ExecuteHeaderClickCommand(item));
            Save = new Command(SaveMapping);

        }

        private BleScanViewModel scanner;
        public ICommand LoadDataCommand { get; private set; }
        public ICommand HeaderClickCommand { get; private set; }
        public ICommand Save { get; private set; }
        public IDevice Device { get; set; }

        public ObservableCollection<BluetoothServiceViewModel> Services { get; private set; }

        public string SaveText { get; set; } = "Save";

        public override async Task OnAppearing()
        {
            IsBusy = true;
            await scanner.BleService.Connect(Device);
            var services = await Device.GetServicesAsync();
            foreach (var service in services)
            {
                var chars = await service.GetCharacteristicsAsync();
                Services.Add(new BluetoothServiceViewModel(new BluetoothServiceModel(service.Id.ToString(), service.Name, chars.Select(c => new BluetoothCharacteristicModel(c.Id.ToString(), c.Name, service.Id.ToString())).ToList())));
            }
            OnPropertyChanged("Services");
            IsBusy = false;
            await scanner.BleService.DisconnectDevice(Device); // disconnect to cleanup resources
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
                    var pair = new GattPair(service.Id, characteristic.Id);
                    MappingStorage.Current.Add(pair.GattKey, characteristic.SelectedMeasure?.FieldName);
                }
            }
            MappingStorage.Current.Save();
            await IoTCentral.Current.StartService(Device.Id.ToString(), MappingStorage.Current.GetAll());
            scanner.Close();
            await Navigation.NavigateBack(); // TODO: change to tabs
        }

    }
}
