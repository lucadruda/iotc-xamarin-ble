using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace iotc_xamarin_ble.ViewModels
{
    public class AlertDialogViewModel : INotifyPropertyChanged
    {
        private bool isVisible = false;

        public AlertDialogViewModel()
        {
           
        }
        public AlertDialogViewModel(string alertText) : this()
        {
            AlertText = alertText;

        }

        public string AlertText { get; set; }

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
