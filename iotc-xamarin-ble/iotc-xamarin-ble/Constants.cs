using System;
using System.Collections.Generic;
using System.Text;

namespace iotc_ble_xamarin
{
    public static class Constants
    {
        public const string CLIENT_ID = "04b07795-8ddb-461a-bbee-02f9e1bf7b46";
        public const string REDIRECT_URL = "http://localhost";
        public const string DEFAULT_AUTHORITY = "login.microsoftonline.com";
        public const string DEFAULT_TENANT = "common";

        //public static string CLIENT_ID = "6df42503-a8e6-4333-bd5e-807c4380ba6e";
        //public static string CLIENT_ID = "a7d8cef0-4145-49b2-a91d-95c54051fa3f";

        public const string USER_ID = "user_id";
        public const string IOTC_ACCESS_TOKEN = "IOTC_ACCESS_TOKEN";
        public const string RM_ACCESS_TOKEN = "RM_ACCESS_TOKEN";
        public const string REFRESH_TOKEN = "REFRESH_TOKEN";


        public const string IOTC_TOKEN_AUDIENCE_v1 = "https://apps.azureiotcentral.com/";
        public const string RM_TOKEN_AUDIENCE_v1 = "https://management.azure.com";

        public static string[] RM_REGIONS = { "EastUS", "WestUS", "WestEurope", "NorthEurope" };
        public static string[] IOTC_TOKEN_AUDIENCE_v2 = { "https://apps.azureiotcentral.com/user_impersonation" };
        public static string[] RM_TOKEN_AUDIENCE_v2 = { "https://management.azure.com/user_impersonation" };
        public static string[] GRAPH_SCOPE = { "User.Read" };
        public const string IOTC_DATA_URL = "https://api.azureiotcentral.com/v1-beta";
        public const string IOTC_TEMPLATE_ID = "iotc-devkit-sample:1.0.0";


        public const int PORTRAIT_COLUMNS = 3;
        public const int LANDSCAPE_COLUMNS = 5;



        public const string SERVICE_START = "SERVICE_START";
        public const string SERVICE_STOP = "SERVICE_STOP";


        public const string DEVICE_ID = "DEVICE_ID";
        public const string SCOPE_ID = "SCOPE_ID";
        public const string SYM_KEY = "SYM_KEY";

        public const string IOTC_DEVICE_CLIENT = "IOTC_DEVICE_CLIENT";
        public const string IOTC_DEVICE_CLIENT_CONNECTED = "IOTC_DEVICE_CLIENT_CONNECTED";
        public const string IOTC_DEVICE_CREDENTIALS = "IOTC_DEVICE_CREDENTIALS";



        public const string BLE_MAPPING = "BLE_MAPPING";
        public const string BLE_DEVICE = "BLE_DEVICE";


        public const string BLE_SCAN_START = "BLE_SCAN_START";
        public const string BLE_SCAN_STOP = "BLE_SCAN_STOP";
        public const string BLE_DEVICE_FOUND = "BLE_DEVICE_FOUND";
        public const string BLE_SCAN_STOPPED = "BLE_SCAN_STOPPED";
        public const string BLE_DEVICE_READY = "BLE_DEVICE_READY";

        public const string BLE_SERVICES_FETCHED = "BLE_SERVICES_FETCHED";








    }
}
