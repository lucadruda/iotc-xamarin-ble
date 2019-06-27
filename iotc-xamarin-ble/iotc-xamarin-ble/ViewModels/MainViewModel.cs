using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using iotc_xamarin_ble.ViewModels.Navigation;
using Xamarin.Forms;

namespace iotc_xamarin_ble.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private Page selectedTabPage;

        public MainViewModel(INavigationService navigation) : base(navigation)
        {
            Pages = new ObservableCollection<Page>();
        }

        public ObservableCollection<Page> Pages { get; set; }

        public override Task OnAppearing()
        {
            //Pages.Add(new NavigationPage(Navigation.CreatePage(new AppsViewModel(Navigation))));
            Pages.Add(Navigation.CreatePage(new AboutViewModel(Navigation)));
            OnPropertyChanged("Pages");
            return Task.CompletedTask;
        }

        public Page SelectedTabPage
        {
            get
            {
                return selectedTabPage;
            }

            set
            {
                selectedTabPage = value;
                if (selectedTabPage != null)
                {
                    if (selectedTabPage is NavigationPage)
                    {
                        ((selectedTabPage as NavigationPage).CurrentPage.BindingContext as BaseViewModel).OnAppearing();
                    }
                    else
                    {
                        (selectedTabPage.BindingContext as BaseViewModel).OnAppearing();
                    }
                }
                OnPropertyChanged();
            }
        }
    }
}
