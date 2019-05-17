using System;
using System.Collections.Generic;
using System.Text;

namespace iotc_ble_xamarin
{
    public static class Constants
    {
        //public static string CLIENT_ID = "04b07795-8ddb-461a-bbee-02f9e1bf7b46";
        //public static string CLIENT_ID = "6df42503-a8e6-4333-bd5e-807c4380ba6e";
        //public static string CLIENT_ID = "a7d8cef0-4145-49b2-a91d-95c54051fa3f";
        public const string CLIENT_ID = "869a01f0-7af0-44c1-abff-3ee12b625499";
        public const string IOTC_TOKEN_AUDIENCE_v1 = "https://apps.azureiotcentral.com/";
        public const string RM_TOKEN_AUDIENCE_v1 = "https://management.azure.com";
        public static string[] IOTC_TOKEN_AUDIENCE_v2 = { "https://apps.azureiotcentral.com/user_impersonation" };
        public static string[] RM_TOKEN_AUDIENCE_v2 = { "https://management.azure.com/user_impersonation" };
        public static string[] GRAPH_SCOPE = { "User.Read" };
        public const string IOTC_DATA_URL = "https://api.azureiotcentral.com/v1-beta";
        public const string AUTHORITY_BASE = "https://login.microsoftonline.com/";
        public const string IOTC_TEMPLATE_ID = "iotc-devkit-sample:1.0.0";


        public const int PORTRAIT_COLUMNS = 3;
        public const int LANDSCAPE_COLUMNS = 5;
    }
}
