﻿using iotc_xamarin_ble.ViewModels.Navigation;
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
        private string loadingText = "Loading";

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
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public static event EventHandler<object> PageCompleted;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void Complete(object val = null)
        {
            PageCompleted?.Invoke(this, val);
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

        public virtual void OnNavigationBack(object sender, object e)
        {
        }



        public string Title { get; set; }
    }
}