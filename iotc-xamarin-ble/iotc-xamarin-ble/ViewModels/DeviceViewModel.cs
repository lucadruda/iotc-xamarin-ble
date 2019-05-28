using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using iotc_ble_xamarin.Bluetooth;
using iotc_csharp_device_client;
using iotc_csharp_service.Types;
using iotc_xamarin_ble.Bluetooth;
using iotc_xamarin_ble.Extensions;
using iotc_xamarin_ble.Services;
using iotc_xamarin_ble.ViewModels.Navigation;
using Plugin.BLE.Abstractions.EventArgs;
using Xamarin.Forms;
using Device = iotc_csharp_service.Types.Device;

namespace iotc_xamarin_ble.ViewModels
{
    public class DeviceViewModel : BaseViewModel
    {

        public DeviceViewModel(INavigationService navigation) : base(navigation)
        {
            FormattedText = new FormattedString();
            Pair = new Command(OnPairing);
            Paired = false;
            Title = IoTCentral.Current.Device.Name;
            BLEService.Current.OnValueAvailable = OnNotification;

        }

        public Device IoTCDevice
        {
            get => IoTCentral.Current.Device;
        }
        public string ConnectionStatus //localize
        {
            get; set;
        }
        public FormattedString FormattedText { get; set; }

        public ICommand Pair { get; set; }

        public bool Paired { get; set; }

        public override async Task OnAppearing()
        {
            try
            {
                IsBusy = true;
                OnPropertyChanged("IsBusy");
                await IoTCentral.Current.ConnectDevice();
                ConnectionStatus = "Connected";
                OnPropertyChanged("ConnectionStatus");
                FormattedText.Spans.Add(new Span { Text = $"Connected to {IoTCentral.Current.Application.Name}", ForegroundColor = Color.Red });
                OnPropertyChanged("FormattedText");
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
            catch (IoTCentralException e)
            {
                ConnectionStatus = "Failed";
            }
        }

        private void OnPairing()
        {
            Navigation.NavigateTo(new BleScanViewModel(Navigation));
        }

        public override void OnNavigationBack(object sender, object e)
        {
            if (BLEService.Current.Device != null)
            {
                Paired = true;
                OnPropertyChanged("Paired");
                string pairingMsg = $"Paired to {BLEService.Current.Device.Name}.\n" +
                    $"Exporting features:\n" +
                    $"";
                FormattedText.Spans.Add(new Span { Text = pairingMsg, ForegroundColor = Color.Green });
            }
        }

        private async void OnNotification(object sender, CharacteristicUpdatedEventArgs e)
        {
            var pair = new GattPair(e.Characteristic);
            var measureField = MappingStorage.Current[pair.GattKey];
            var value = e.Characteristic.GetValue();
            FormattedText.Spans.Add(new Span { Text = $"Sending {measureField}", ForegroundColor = Color.Green });
            await IoTCentral.Current.DeviceClient.SendTelemetry($"{{\"{measureField}\":{value}}}", null);
        }

        public override Task BeforeFirstShown()
        {
            PageCompleted += OnNavigationBack;
            return base.BeforeFirstShown();
        }

        public override Task AfterDismissed()
        {
            PageCompleted -= OnNavigationBack;
            return base.AfterDismissed();
        }
    }
}
