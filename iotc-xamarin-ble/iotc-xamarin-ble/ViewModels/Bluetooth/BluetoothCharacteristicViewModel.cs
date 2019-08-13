using iotc_ble_xamarin.Bluetooth;
using iotc_csharp_service.Templates;
using iotc_csharp_service.Types;
using iotc_xamarin_ble.Bluetooth;
using iotc_xamarin_ble.Services;
using Plugin.BLE.Abstractions.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace iotc_xamarin_ble.ViewModels.Bluetooth
{
    public class BluetoothCharacteristicViewModel : INotifyPropertyChanged
    {
        public delegate void TelemetryAssigned(object sender, string charId, string newField, string oldField);
        public static event TelemetryAssigned TelemetryFieldAssigned;
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Measure> CurrentMeasures { get; private set; }
        private List<Measure> AvailableMeasures { get; set; }

        public ICommand ClearSelection { get; set; }

        private Measure selectedMeasure;

        public int SelectedIndex { get; set; }
        public Measure SelectedMeasure
        {
            get { return selectedMeasure; }
            set
            {
                if (selectedMeasure == value)
                    return;
                TelemetryFieldAssigned(this, Characteristic.Id, value?.FieldName, selectedMeasure?.FieldName);
                selectedMeasure = value;
                PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs("SelectedMeasure"));
            }
        }
        public BluetoothCharacteristicViewModel(BluetoothCharacteristicModel characteristic)
        {
            AvailableMeasures = new ContosoTemplate().GetMeasures(IoTCentral.Current.Model.Id);
            Characteristic = characteristic;
            InitMeasures();
            TelemetryFieldAssigned += OnTelemetryAssigned;
            ClearSelection = new Command(Clear);
        }

        public BluetoothCharacteristicModel Characteristic { get; private set; }

        private void InitMeasures()
        {
            CurrentMeasures = new ObservableCollection<Measure>(AvailableMeasures);
            var current = MappingStorage.Current[new GattPair(Characteristic.ServiceId, Characteristic.Id).GattKey];
            PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs("CurrentMeasures"));
            if (current != null)
            {
                SelectedMeasure = CurrentMeasures.FirstOrDefault(m => m.FieldName == current);
            }
        }

        public string Id
        {
            get { return Characteristic.Id.ToString(); }
        }

        public string Name
        {
            get { return Characteristic.Name; }
        }


        private void OnTelemetryAssigned(object sender, string charId, string newField, string oldField)
        {
            if (charId == Characteristic.Id)
            {
                return;
            }
            CurrentMeasures.Remove(CurrentMeasures.FirstOrDefault(x => x.FieldName == newField));
            if (!string.IsNullOrEmpty(oldField))
            {
                CurrentMeasures.Add(AvailableMeasures.FirstOrDefault(x => x.FieldName == oldField));
            }
        }

        private void Clear()
        {
            SelectedIndex = -1;
            SelectedMeasure = null;
        }

        public void UpdateMeasure(Measure measure)
        {
            selectedMeasure = measure;
            PropertyChanged?.Invoke(this,
           new PropertyChangedEventArgs("SelectedMeasure"));
        }

    }
}
