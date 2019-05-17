﻿using Plugin.BLE.Abstractions.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace iotc_xamarin_ble.ViewModels.Bluetooth
{
    public class BluetoothServiceViewModel : ObservableCollection<BluetoothCharacteristicViewModel>
    {
        private bool expanded;
        //store all characteristics here so we can play with the outer collection
        public ObservableCollection<BluetoothCharacteristicViewModel> Characteristics { get; private set; }

        public BluetoothServiceViewModel(IService service, bool expanded = false)
        {
            Service = service;
            this.expanded = expanded;
            Characteristics = new ObservableCollection<BluetoothCharacteristicViewModel>();
        }

        public IService Service { get; private set; }

        public string Id
        {
            get { return Service.Id.ToString(); }
        }

        public string Name
        {
            get { return Service.Name; }
        }
        public string StateIcon { get { return Expanded ? "up" : "down"; } }
        public bool Expanded
        {
            get { return expanded; }
            set
            {
                if (expanded != value) //toggle
                {
                    expanded = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Expanded"));
                    OnPropertyChanged(new PropertyChangedEventArgs("StateIcon"));
                    if (expanded)
                    {
                        Populate();
                    }
                    else
                    {
                        Clear();
                    }
                }
            }
        }

        public async Task Init()
        {
            var chars = await Service.GetCharacteristicsAsync();
            foreach (var item in chars)
            {
                var c = new BluetoothCharacteristicViewModel(item);
                Characteristics.Add(c);
                if (expanded)
                    Add(c);
            }
        }

        private void Populate()
        {
            foreach (var item in Characteristics)
            {
                Add(item);
            }
        }
    }
}
