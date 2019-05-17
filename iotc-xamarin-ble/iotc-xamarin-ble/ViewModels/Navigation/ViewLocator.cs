using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace iotc_xamarin_ble.ViewModels.Navigation
{
    public class ViewLocator : IViewLocator
    {
        public Page CreateAndBindPageFor<TViewModel>(TViewModel viewModel) where TViewModel : BaseViewModel
        {
            var pageType = FindPageForViewModel(viewModel.ModelType ?? viewModel.GetType());

            var page = (Page)Activator.CreateInstance(pageType);

            page.BindingContext = viewModel;

            return page;
        }

        protected virtual Type FindPageForViewModel(Type viewModelType)
        {
            string pageTypeName = "";
            if (viewModelType.IsGenericType)
            {
                pageTypeName = Regex.Replace(viewModelType.AssemblyQualifiedName, @"(.+)`1\[\[.+\]\](.+)", "$1$2");
            }
            else
            {
                pageTypeName = viewModelType.AssemblyQualifiedName;
            }
            pageTypeName = pageTypeName.Replace("ViewModel,", "Page,").Replace(".ViewModels.", ".Views.");
            var pageType = Type.GetType(pageTypeName);
            if (pageType == null)
                throw new ArgumentException(pageTypeName + " type does not exist");

            return pageType;
        }
    }
}
