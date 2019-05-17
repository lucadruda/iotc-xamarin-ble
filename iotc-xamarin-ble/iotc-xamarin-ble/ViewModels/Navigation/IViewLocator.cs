using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace iotc_xamarin_ble.ViewModels.Navigation
{
    public interface IViewLocator
    {
        Page CreateAndBindPageFor<TViewModel>(TViewModel viewModel) where TViewModel : BaseViewModel;
    }
}
