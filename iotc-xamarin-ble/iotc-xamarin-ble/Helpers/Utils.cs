using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iotc_xamarin_ble.Helpers
{
    public static class Utils
    {
        private static Random random = new Random();
        public static string GetRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string GetRandomMac(string separator)
        {
            const string chars = "ABCDEF0123456789";
            var res = new string(Enumerable.Repeat(chars, 12)
              .Select(s => s[random.Next(s.Length)]).ToArray());
            return res.Insert(2, separator).Insert(5, separator).Insert(8, separator).Insert(11, separator).Insert(14, separator);
        }
    }
}
