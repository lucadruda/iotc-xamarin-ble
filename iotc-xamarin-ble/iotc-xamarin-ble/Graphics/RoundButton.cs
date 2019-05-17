using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace iotc_xamarin_ble.Graphics
{
    public class RoundButton : Button
    {
        public RoundButton()
        {
            BackgroundColor = Color.FromRgb(136, 136, 136);
            CornerRadius = 4;
            TextColor = Color.Black;
        }
    }
}
