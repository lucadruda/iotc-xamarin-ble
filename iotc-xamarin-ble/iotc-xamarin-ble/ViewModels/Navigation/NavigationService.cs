using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace iotc_xamarin_ble.ViewModels.Navigation
{
    class NavigationService : INavigationService
    {
        private readonly IViewLocator viewLocator;
        private INavigation navigator;

        private INavigation Navigator
        {
            get
            {
                if (navigator == null)
                {
                    return navigator = App.Current.MainPage.Navigation;
                }
                return navigator;
            }
            set => navigator = value;
        }
        public NavigationService(IViewLocator viewLocator)
        {
            this.viewLocator = viewLocator;
        }

        public Page CreatePage(BaseViewModel viewModel)
        {
            viewModel.BeforeFirstShown();
            return viewLocator.CreateAndBindPageFor(viewModel);
        }
        public async Task NavigateBack()
        {
            var dismissing = Navigator.NavigationStack.Last().BindingContext as BaseViewModel;

            await Navigator.PopAsync();

            var current = Navigator.NavigationStack.Last().BindingContext as BaseViewModel;
            await current.OnNavigatingBack();

        }

        public async Task NavigateBackModal()
        {
            var dismissing = Navigator.NavigationStack.Last().BindingContext as BaseViewModel;

            await Navigator.PopModalAsync();

            var current = Navigator.NavigationStack.Last().BindingContext as BaseViewModel;
            await current.OnNavigatingBack();

        }

        public async Task NavigateBackToRoot()
        {
            var toDismiss = Navigator.NavigationStack.Skip(1).Select(vw => vw.BindingContext).OfType<BaseViewModel>().ToArray();

            await Navigator.PopToRootAsync();

            foreach (BaseViewModel viewModel in toDismiss)
            {
                viewModel.AfterDismissed().Start();
            }
        }

        public async Task NavigateTo(BaseViewModel viewModel)
        {
            Page page = viewLocator.CreateAndBindPageFor(viewModel);

            await viewModel.BeforeFirstShown();

            await Navigator.PushAsync(page);

            await viewModel.OnAppearing();
        }

        public async Task NavigateToModal(BaseViewModel viewModel)
        {
            Page page = viewLocator.CreateAndBindPageFor(viewModel);

            await viewModel.BeforeFirstShown();

            await Navigator.PushModalAsync(page);

            await viewModel.OnAppearing();
        }

        public void PresentAsMainPage(BaseViewModel viewModel)
        {
            Page currentPage = viewLocator.CreateAndBindPageFor(viewModel);
            var viewModelsToDismiss = FindViewModelsToDismiss(App.Current.MainPage);
            if (App.Current.MainPage is NavigationPage navPage)
            {
                // If we're replacing a navigation page, unsub from events
                navPage.PopRequested -= NavPagePopRequested;
            }

            viewModel.BeforeFirstShown();

            App.Current.MainPage = currentPage;
            viewModel.OnAppearing();


            foreach (BaseViewModel toDismiss in viewModelsToDismiss)
            {
                toDismiss.AfterDismissed();
            }
        }

        public void PresentAsNavigatableMainPage(BaseViewModel viewModel)
        {
            Page page = viewLocator.CreateAndBindPageFor(viewModel);

            NavigationPage newNavigationPage = new NavigationPage(page);

            IEnumerable<BaseViewModel> viewModelsToDismiss = FindViewModelsToDismiss(App.Current.MainPage);

            if (App.Current.MainPage is NavigationPage navPage)
            {
                navPage.PopRequested -= NavPagePopRequested;
            }

            viewModel.BeforeFirstShown();

            // Listen for back button presses on the new navigation bar
            newNavigationPage.PopRequested += NavPagePopRequested;
            App.Current.MainPage = newNavigationPage;
            viewModel.OnAppearing();

            foreach (BaseViewModel toDismiss in viewModelsToDismiss)
            {
                toDismiss.AfterDismissed();
            }
        }


        private IEnumerable<BaseViewModel> FindViewModelsToDismiss(Page dismissingPage)
        {
            var viewmodels = new List<BaseViewModel>();

            if (dismissingPage is NavigationPage)
            {
                viewmodels.AddRange(
                    Navigator
                        .NavigationStack
                        .Select(p => p.BindingContext)
                        .OfType<BaseViewModel>()
                );
            }
            else
            {
                var viewmodel = dismissingPage?.BindingContext as BaseViewModel;
                if (viewmodel != null) viewmodels.Add(viewmodel);
            }

            return viewmodels;
        }
        /// <summary>
        /// Catch page pop request. When exiting from a page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavPagePopRequested(object sender, NavigationRequestedEventArgs e)
        {
            if (Navigator.NavigationStack.LastOrDefault()?.BindingContext is BaseViewModel poppingPage)
            {
                poppingPage.AfterDismissed();
            }
        }

        public void SetNavigator(INavigation navigator)
        {
            Navigator = navigator;
        }
    }
}
