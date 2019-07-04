using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iotc_xamarin_ble.Helpers;
using iotc_xamarin_ble.ViewModels.Navigation;
using Xamarin.Forms;

namespace iotc_xamarin_ble.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private BaseViewModel selectedVM;


        public MainViewModel(INavigationService navigation) : base(navigation)
        {

            Models = new ObservableCollection<BaseViewModel>
        {
            new AppsViewModel(navigation),
            new AboutViewModel(navigation),
            new UserDetailsViewModel(navigation)

        };
            TemplateSelector = new ViewModelPageSelector();
        }

        public ObservableCollection<BaseViewModel> Models { get; set; }
        public ObservableCollection<DataTemplate> Templates { get; set; }

        public DataTemplateSelector TemplateSelector { get; set; }

        public override Task OnAppearing()
        {
            foreach (var model in Models)
            {
                model.OnAppearing();
            }
            return Task.CompletedTask;
        }
        public BaseViewModel SelectedVM
        {
            get
            {
                return selectedVM;
            }

            set
            {
                selectedVM = value;
                if (selectedVM != null)
                {
                    selectedVM.OnAppearing();
                }
                OnPropertyChanged();
            }
        }
    }
}
