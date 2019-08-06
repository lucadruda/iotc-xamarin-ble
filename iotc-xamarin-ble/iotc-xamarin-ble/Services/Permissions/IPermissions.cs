using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace iotc_xamarin_ble.Services.Permissions
{
    public interface IPermissions
    {
        Task<PermissionStatus> CheckPermissions();
    }
}
