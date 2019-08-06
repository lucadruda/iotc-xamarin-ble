using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;

namespace iotc_xamarin_ble.Services.Permissions
{
    public class Permissions : IPermissions
    {
        public async Task<PermissionStatus> CheckPermissions()
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                return PermissionStatus.Granted;
            }
            var permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
            if (permissionStatus == PermissionStatus.Granted)
            {
                return permissionStatus;
            }
            var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
            if (results.ContainsKey(Permission.Location))
                permissionStatus = results[Permission.Location];
            return permissionStatus;
        }
    }
}
