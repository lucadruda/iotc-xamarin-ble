using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace iotc_xamarin_ble.Graphics
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlertDialog : AbsoluteLayout
    {
        public AlertDialog()
        {
            InitializeComponent();
        }
    }
}