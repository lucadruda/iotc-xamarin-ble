using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace iotc_xamarin_ble.Services.Dialog
{
    public class DialogService : IDialogService
    {
        private static DialogService service;
        public static DialogService Current
        {
            get
            {
                if (service == null)
                {
                    service = new DialogService();
                }
                return service;
            }
        }

        public async Task ShowError(string message,
            string title,
            string buttonText,
            Action afterHideCallback)
        {
            await Xamarin.Forms.Application.Current.MainPage.DisplayAlert(
                title,
                message,
                buttonText);
            afterHideCallback?.Invoke();
        }

        public async Task ShowError(
            Exception error,
            string title,
            string buttonText,
            Action afterHideCallback)
        {
            await Xamarin.Forms.Application.Current.MainPage.DisplayAlert(
                title,
                error.Message,
                buttonText);

            afterHideCallback?.Invoke();
        }

        public async Task ShowMessage(
            string message,
            string title)
        {
            await Xamarin.Forms.Application.Current.MainPage.DisplayAlert(
                title,
                message,
                "OK");
        }

        public async Task ShowMessage(
            string message,
            string title,
            string buttonText,
            Action afterHideCallback)
        {
            await Xamarin.Forms.Application.Current.MainPage.DisplayAlert(
                title,
                message,
                buttonText);

            afterHideCallback?.Invoke();
        }

        public async Task<bool> ShowMessage(
            string message,
            string title,
            string buttonConfirmText,
            string buttonCancelText,
            Action<bool> afterHideCallback)
        {
            var result = await Xamarin.Forms.Application.Current.MainPage.DisplayAlert(
                title,
                message,
                buttonConfirmText,
                buttonCancelText);

            if (afterHideCallback != null)
            {
                afterHideCallback(result);
            }
            return result;
        }

        public async Task ShowMessageBox(
            string message,
            string title)
        {
            await Xamarin.Forms.Application.Current.MainPage.DisplayAlert(
                title,
                message,
                "OK");
        }
    }
}
