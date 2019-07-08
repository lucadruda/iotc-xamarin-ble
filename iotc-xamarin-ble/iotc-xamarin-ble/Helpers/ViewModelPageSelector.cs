﻿using iotc_xamarin_ble.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
using System.Text;
using Xamarin.Forms;

namespace iotc_xamarin_ble.Helpers
{
    public class ViewModelPageSelector : DataTemplateSelector
    {
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is BaseViewModel)
            {
                var vm = item as BaseViewModel;
                return new DataTemplate(() =>
                {
                    var page = vm.Navigation.CreatePage(vm);
                   //
                        page = new NavigationPage(page);
                        page.Title = vm.Title;
                        page.IconImageSource = vm.Icon;
                        vm.Navigation.SetNavigator(page.Navigation);
                    //}
                    vm.BeforeFirstShown();
                    return page;
                });
            }
            return null;
        }
    }
}
