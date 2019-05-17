using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace iotc_xamarin_ble.ViewModels
{
    public interface IViewModelLifecycle
    {

        /// <summary>
        /// Called exactly once, before the viewmodel enters the navigation stack
        /// </summary>
        Task BeforeFirstShown();

        /// <summary>
        /// Called every time viewmodel shows.
        /// </summary>
        /// <returns></returns>
        Task OnAppearing();

        /// <summary>
        /// Called exactly once, when the viewmodel leaves the navigation stack
        /// </summary>
        Task AfterDismissed();

        // You may also wish to implement additional lifecycle hooks eg.
        // Before a viewmodel is shown when navigating backwards, or after a viewmodel has been shown
    }
}
