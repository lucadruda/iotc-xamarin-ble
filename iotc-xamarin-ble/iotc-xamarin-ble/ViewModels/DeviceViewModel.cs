using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using iotc_ble_xamarin;
using iotc_ble_xamarin.Bluetooth;
using iotc_csharp_device_client;
using iotc_csharp_device_client.enums;
using iotc_csharp_service.Types;
using iotc_xamarin_ble.Bluetooth;
using iotc_xamarin_ble.Extensions;
using iotc_xamarin_ble.Messages;
using iotc_xamarin_ble.Services;
using iotc_xamarin_ble.ViewModels.Navigation;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;
using Xamarin.Forms;
using Device = iotc_csharp_service.Types.Device;
using XamarinDevice = Xamarin.Forms.Device;

namespace iotc_xamarin_ble.ViewModels
{
    public class DeviceViewModel : BaseViewModel
    {

        public DeviceViewModel(INavigationService navigation) : base(navigation)
        {
            IsBusy = true;
            FormattedText = new FormattedString();
            Title = IoTCentral.Current.Device.Name;
            IoTCentral.Current.DeviceReady += OnDeviceReady;
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
                IoTCentral.Current.DeviceConnectionChanged += OnDeviceConnectionChanged;
                await base.OnAppearing();
            }
            catch (IoTCentralException e)
            {
                ConnectionStatus = "Failed";
            }
        }


        private void OnDeviceReady(object sender, IDevice device)
        {
            IsBusy = false;
            string pairingMsg = $"Paired to {device.Name}.\n" +
                $"Exporting features:\n" +
                MappingStorage.Current.GetAll().Values.Aggregate("",(a, b) =>
                {
                    return b ?? $"{a}, {b}";
                });
            XamarinDevice.BeginInvokeOnMainThread(() =>
            {
                FormattedText.Spans.Add(new Span
                {
                    Text = pairingMsg,
                    ForegroundColor = Color.Green
                });
            });

        }


        public override Task BeforeFirstShown()
        {
            return base.BeforeFirstShown();
        }

        public override Task AfterDismissed()
        {
            return base.AfterDismissed();
        }

        private void OnDeviceConnectionChanged(object source, IoTCConnectionState state)
        {
            Span span;
            switch (state)
            {
                case IoTCConnectionState.CONNECTION_OK:
                    ConnectionStatus = "Connected";
                    span = new Span { Text = $"Connected to {IoTCentral.Current.Application.Name}\n", ForegroundColor = Color.Red };
                    break;
                default:
                    ConnectionStatus = "Not Connected";
                    span = new Span { Text = $"Failed to connect to {IoTCentral.Current.Application.Name}\n", ForegroundColor = Color.Red };
                    break;
            }
            XamarinDevice.BeginInvokeOnMainThread(() =>
            {
                FormattedText.Spans.Add(span);
            });
            IsBusy = false;
            OnPropertyChanged("ConnectionStatus");
            OnPropertyChanged("FormattedText");

        }
    }
}
