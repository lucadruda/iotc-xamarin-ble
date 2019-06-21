using System;
using iotc_xamarin_ble.Services.Container;
using iotc_xamarin_ble.Services.Dialog;

namespace iotc_xamarin_ble.Helpers
{
    public class IoTCException
    {
        private IDialogService dialogService;
        public IoTCException()
        {
            dialogService = ContainerService.Current.Resolve<IDialogService>();
        }

        public void Error(string message,Action callback=null)
        {       
            dialogService.ShowError(message, "Error", "Close",callback);
        }

        public void Warning()
        {

        }
    }
}
