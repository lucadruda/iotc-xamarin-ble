﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace iotc_xamarin_ble.ViewModels.Navigation
{
    public interface INavigationService
    {
        /// <summary>
        /// Sets the viewmodel to be the main page of the application
        /// </summary>
        void PresentAsMainPage(BaseViewModel viewModel);

        /// <summary>
        /// Sets the viewmodel as the main page of the application, and wraps its page within a Navigation page
        /// </summary>
        void PresentAsNavigatableMainPage(BaseViewModel viewModel);

        /// <summary>
        /// Navigate to the given page on top of the current navigation stack
        /// </summary>
        Task NavigateTo(BaseViewModel viewModel);

        /// <summary>
        /// Navigate to the previous item in the navigation stack
        /// </summary>
        Task NavigateBack();

        /// <summary>
        /// Navigate back to the element at the root of the navigation stack
        /// </summary>
        Task NavigateBackToRoot();
    }
}
