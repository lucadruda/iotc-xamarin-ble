using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace iotc_xamarin_ble.ViewModels
{
    public interface IScanViewModel
    {
        Task Scan();
        Task Stop();

        void Reset();
    }
}
