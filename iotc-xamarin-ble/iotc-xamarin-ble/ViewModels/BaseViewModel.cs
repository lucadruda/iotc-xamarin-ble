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
        private string title;
        private string icon;
        private string loadingText = "Loading";
        private InputDialogViewModel inputModel;

        public INavigationService Navigation { get; private set; }

        public virtual Type ModelType { get; set; }

        public InputDialogViewModel InputModel
        {
            get => inputModel;
            set
            {
                inputModel = value;
                OnPropertyChanged();
            }
        }

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                OnPropertyChanged();
            }
        }

        public string LoadingText
        {
            get { return loadingText; }
            set
            {
                loadingText = value;
                OnPropertyChanged();
            }
        }
        public BaseViewModel(INavigationService navigation)
        {
            Navigation = navigation;
            InputModel = new InputDialogViewModel { IsVisible = false, InputLabel = "pippo" };
            OnPropertyChanged("InputModel");
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

        public virtual Task OnNavigatingBack()
        {
            return Task.CompletedTask;
        }



        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }

        public string Icon
        {
            get { return icon; }
            set
            {
                icon = value;
                OnPropertyChanged();
            }
        }
    }
}
