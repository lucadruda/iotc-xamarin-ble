using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace iotc_xamarin_ble.ViewModels
{
    public class InputDialogViewModel : INotifyPropertyChanged
    {
        private bool isVisible = false;

        public InputDialogViewModel()
        {
            DialogOk = new Command(() =>
            {
                OnOk?.Invoke(this, null);
            });

            DialogCancel = new Command(() =>
            {
                OnCancel?.Invoke(this, null);
            });
        }
        public InputDialogViewModel(string inputLabel) : this()
        {
            InputLabel = inputLabel;

        }

        public string InputLabel { get; set; }
        public string InputText { get; set; }

        public bool IsVisible
        {
            get => isVisible;
            set
            {
                isVisible = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsVisible"));
            }
        }

        public ICommand DialogOk { get; set; }
        public ICommand DialogCancel { get; set; }

        public event EventHandler OnOk;
        public event EventHandler OnCancel;
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
