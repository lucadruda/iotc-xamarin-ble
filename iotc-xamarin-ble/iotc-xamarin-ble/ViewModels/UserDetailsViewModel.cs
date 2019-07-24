using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using iotc_ble_xamarin;
using iotc_xamarin_ble.Authentication.v1;
using iotc_xamarin_ble.Messages;
using iotc_xamarin_ble.Models;
using iotc_xamarin_ble.Services.Container;
using iotc_xamarin_ble.Services.Storage;
using iotc_xamarin_ble.ViewModels.Navigation;
using Xamarin.Forms;

namespace iotc_xamarin_ble.ViewModels
{
    public class UserDetailsViewModel : BaseViewModel
    {
        private Theme selectedTheme;
        private ConnectionMode selectedMode;

        public string FullName { get; set; }
        public string Email { get; set; }

        public ObservableCollection<Theme> Themes { get; set; }
        public ObservableCollection<ConnectionMode> Modes { get; set; }

        private const string THEME = "theme";
        private const string CONNECTION_MODE = "connmode";

        public ICommand Logout { get; set; }
        public string LogOutIcon { get; set; }

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
                    ChangeTheme();
                }
            }
        }

        public ConnectionMode SelectedMode
        {
            get
            {
                return selectedMode;
            }
            set
            {
                if (value != null)
                {
                    selectedMode = value;
                    selectedMode.IsSelected = true;
                    foreach (var mode in Modes)
                    {
                        if (mode.Name != selectedMode.Name)
                        {
                            mode.IsSelected = false;
                        }
                    }
                    OnPropertyChanged();
                    OnPropertyChanged("Modes");
                    // trigger mode changes
                }
            }
        }
        public UserDetailsViewModel(INavigationService navigation) : base(navigation)
        {
            Title = "User";
            Icon = "user";
            FullName = "N/A";
            Email = "N/A";
            Logout = new Command(OnLogout);
            LogOutIcon = "logout";
            Themes = new ObservableCollection<Theme>
            {
               new Theme{Name=Constants.THEMES.LIGHT.ToString(),IsSelected=true},
               new Theme{Name=Constants.THEMES.DARK.ToString(),IsSelected=false}
            };
            Modes = new ObservableCollection<ConnectionMode>
            {
               new ConnectionMode{Name=Constants.CONNECTION_MODE.BLE.ToString(),IsSelected=true},
               new ConnectionMode{Name=Constants.CONNECTION_MODE.WIFI.ToString(),IsSelected=false}

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

            //select mode
            var mode = PreferencesStorage.Current[CONNECTION_MODE];
            if (mode != null)
            {
                SelectedMode = Modes.FirstOrDefault(t => t.Name == theme);

            }
        }

        private void ChangeTheme()
        {
            var theme = (Constants.THEMES)Enum.Parse(typeof(Constants.THEMES), selectedTheme.Name);
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

        private void ChangeMode()
        {
            var mode = (Constants.CONNECTION_MODE)Enum.Parse(typeof(Constants.CONNECTION_MODE), selectedMode.Name);
            if (mode == Constants.CONNECTION_MODE.BLE)
            {
                ContainerService.Current.RegisterType<IScanViewModel>(typeof(BleScanViewModel));
            }
            else
            {
                ContainerService.Current.RegisterType<IScanViewModel>(typeof(WifiScanViewModel));

            }
            PreferencesStorage.Current.Add(CONNECTION_MODE, mode.ToString());
            PreferencesStorage.Current.Save();
        }

        private void OnLogout()
        {
            MessagingCenter.Send(new RequestMessage(), Constants.LOGOUT);
        }

    }
}
