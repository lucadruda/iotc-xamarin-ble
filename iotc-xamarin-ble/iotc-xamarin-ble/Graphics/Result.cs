using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace iotc_xamarin_ble.Graphics
{
    public class Result : INotifyPropertyChanged
    {
        private string text;
        private Color color;

        public string Text { get => text; set { text = value; OnPropertyChanged(); } }
        public Color Color { get => color; set { color = value; OnPropertyChanged(); } }

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
