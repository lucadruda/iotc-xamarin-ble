using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace iotc_xamarin_ble.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        //public static readonly BindableProperty ChildrenProperty = BindableProperty.Create(nameof(Children), typeof(IEnumerable<Page>), typeof(MainPage), null, BindingMode.TwoWay, null, OnItemsSourcePropertyChanged);

        public MainPage()
        {
            InitializeComponent();
        }

        //public new IEnumerable<Page> Children
        //{
        //    get { return base.Children; }
        //    set { SetValue(ChildrenProperty, value); }
        //}

        //private static void OnItemsSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        //{
        //    var tabbedPage = (TabbedPage)bindable;
        //    var notifyCollection = newValue as INotifyCollectionChanged;
        //    if (notifyCollection != null)
        //    {
        //        notifyCollection.CollectionChanged += (sender, args) =>
        //        {
        //            if (args.NewItems != null)
        //            {
        //                foreach (var newItem in args.NewItems)
        //                {
        //                    tabbedPage.Children.Add((Page)newItem);
        //                }
        //            }
        //            if (args.OldItems != null)
        //            {
        //                foreach (var oldItem in args.OldItems)
        //                {
        //                    tabbedPage.Children.Remove((Page)oldItem);
        //                }
        //            }
        //        };
        //    }

        //    if (newValue == null) return;

        //    tabbedPage.Children.Clear();

        //    foreach (var item in (newValue as IEnumerable)) tabbedPage.Children.Add((Page)item);
        //}
    }
}