using iotc_xamarin_ble.ViewModels.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace iotc_xamarin_ble.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged, IViewModelLifecycle

    {
        private bool isBusy = false;
        public INavigationService Navigation { get; private set; }

        public virtual Type ModelType { get; set; }

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                OnPropertyChanged();
            }
        }
        public BaseViewModel(INavigationService navigation)
        {
            Navigation = navigation;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(propertyName));
        }


        public virtual Task BeforeFirstShown()
        {
            return Task.CompletedTask;
        }

        public virtual Task AfterDismissed()
        {
            return Task.CompletedTask;
        }

        public virtual Task OnAppearing()
        {
            return Task.CompletedTask;
        }
    }
}
