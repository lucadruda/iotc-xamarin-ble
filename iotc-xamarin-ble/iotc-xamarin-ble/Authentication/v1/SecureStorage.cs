using iotc_ble_xamarin;
using iotc_xamarin_ble.Services.Storage;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamarinSecureStorage = Xamarin.Essentials.SecureStorage;

namespace iotc_xamarin_ble.Authentication.v1
{
    public class SecureStorage : IStorage
    {
        private static SecureStorage secureStorage;

        public static SecureStorage Current
        {
            get
            {
                if (secureStorage == null)
                {
                    secureStorage = new SecureStorage();
                }
                return secureStorage;
            }
        }

        private SecureStorage()
        {
            Task.Run(async () =>
            {
                string storage = await XamarinSecureStorage.GetAsync(Constants.USER_ID);

                tokens = new Dictionary<string, string>();
                if (!string.IsNullOrEmpty(storage))
                {
                    JObject obj = JObject.Parse(storage);
                    foreach (var token in obj)
                    {
                        tokens.Add(token.Key, token.Value.Value<string>());
                    }
                }
            }).Wait();
        }

        private Dictionary<string, string> tokens;
        public string this[string key]
        {
            get
            {
                if (tokens.ContainsKey(key))
                {
                    return tokens[key];
                }
                return null;
            }
            set
            {
                tokens[key] = value;
            }
        }

        public void Add(string key, string value)
        {
            if (tokens.ContainsKey(key))
            {
                tokens[key] = value;
                return;
            }
            tokens.Add(key, value);
        }

        public Dictionary<string, string> GetAll()
        {
            return tokens;
        }

        public void Remove(string key)
        {
            tokens.Remove(key);

        }

        public void Save()
        {
            Task.Run(async () =>
            {
                await XamarinSecureStorage.SetAsync(Constants.USER_ID, JsonConvert.SerializeObject(tokens));
            });
        }

        public void Clear()
        {
            tokens.Clear();
            XamarinSecureStorage.Remove(Constants.USER_ID);

        }
    }
}
