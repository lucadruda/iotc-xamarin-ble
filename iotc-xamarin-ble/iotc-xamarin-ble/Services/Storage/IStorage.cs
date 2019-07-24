using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace iotc_xamarin_ble.Services.Storage
{
    public interface IStorage
    {

        void Save();
        void Add(string key, string value);
        void Remove(string key);

        string this[string key] { get; }

        Dictionary<string, string> GetAll();

        void Clear();

    }
}
