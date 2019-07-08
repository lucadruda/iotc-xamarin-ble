using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using iotc_ble_xamarin;
using iotc_xamarin_ble.Authentication.v1;
using iotc_xamarin_ble.Messages;
using iotc_xamarin_ble.Services.Storage;
using iotc_xamarin_ble.ViewModels.Navigation;
using Xamarin.Forms;

namespace iotc_xamarin_ble.ViewModels
{
    public class UserDetailsViewModel : BaseViewModel
    {
        private Theme selectedTheme;

        public string FullName { get; set; }
        public string Email { get; set; }

        public ObservableCollection<Theme> Themes { get; set; }

        private const string THEME = "theme";

        public Theme SelectedTheme
        {
            get
            {
                return selectedTheme;
            }
            set
            {
                if (value != null)
                {
                    selectedTheme = value;
                    selectedTheme.IsSelected = true;
                    foreach (var theme in Themes)
                    {
                        if (theme.Name != selectedTheme.Name)
                        {
                            theme.IsSelected = false;
                        }
                    }
                    OnPropertyChanged();
                    OnPropertyChanged("Themes");
                    ChangeTheme((Constants.THEMES)Enum.Parse(typeof(Constants.THEMES), selectedTheme.Name));
                }
            }
        }
        public UserDetailsViewModel(INavigationService navigation) : base(navigation)
        {
            Title = "User";
            Icon = "user";
            FullName = "N/A";
            Email = "N/A";
            Themes = new ObservableCollection<Theme>
            {
               new Theme{Name=Constants.THEMES.LIGHT.ToString(),IsSelected=true},
               new Theme{Name=Constants.THEMES.DARK.ToString(),IsSelected=false}
            };
            MessagingCenter.Subscribe<ResultMessage<AzureToken>>(this, Constants.IOTC_ACCESS_TOKEN, (msg) =>
            {
                var token = msg.Data as AzureToken;
                FullName = $"{token.GivenName} {token.FamilyName}";
                Email = token.Email;
                OnPropertyChanged("FullName");
                OnPropertyChanged("Email");
            });

            var theme = PreferencesStorage.Current[THEME];
            if (theme != null)
            {
                SelectedTheme = Themes.FirstOrDefault(t => t.Name == theme);
            }
        }

        private void ChangeTheme(Constants.THEMES theme)
        {
            if (theme == Constants.THEMES.DARK)
            {
                App.Current.Resources["backgroundColor"] = "#262626";
                App.Current.Resources["textColor"] = Color.White;
            }
            else
            {
                App.Current.Resources["backgroundColor"] = Color.White;
                App.Current.Resources["textColor"] = Color.Black;
            }
            PreferencesStorage.Current.Add(THEME, theme.ToString());
            PreferencesStorage.Current.Save();
        }

    }
    public class Theme : INotifyPropertyChanged
    {
        private bool isSelected;
        private string name;

        public string Name { get => name; set { name = value; OnPropertyChanged(); } }
        public bool IsSelected { get => isSelected; set { isSelected = value; OnPropertyChanged(); } }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
